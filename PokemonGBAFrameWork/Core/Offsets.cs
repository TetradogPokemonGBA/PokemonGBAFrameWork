/*
 * Creado por SharpDevelop.
 * Usuario: pc
 * Fecha: 27/08/2016
 * Hora: 19:01
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
//créditos Gamer2020 por el codigo fuente de PokemonGameEditor-3.7 que ha servido para establecer partes de la lógica
//créditos Naren jr. por la parte de obtener el offset con los bytes (lo de parsear y lo del 09 y 08)
namespace PokemonGBAFrameWork
{

	public enum Longitud
	{
		Offset = 4,
		DieciseisMegas = 0xFFFFFF,
		TrentaYDosMegas = DieciseisMegas * 2
			
	}
	
	
	public static class CompilacionRom
	{
		public enum Compilacion
		{
			Primera,
			Segunda
			//si hay mas se ponen cuando sean necesarias
		}
		public static Compilacion GetCompilacion(RomPokemon rom)
		{
			return GetCompilacion(rom, Edicion.GetEdicion(rom));
		}
		public static Compilacion GetCompilacion(RomPokemon rom, Edicion edicion)
		{
			Compilacion compilacion;
			if (rom == null || edicion == null)
				throw new ArgumentNullException();
			compilacion = Compilacion.Primera;//necesito mas desarrollo para determinar la segunda
			return compilacion;
		}
	}
	/// <summary>
	/// las zonas son offsets donde se guardan offsets permutados.
	/// </summary>
	public class Zona:IComparable,IComparable<Zona>,IClauUnicaPerObjecte
	{
		/// <summary>
		/// Sirve para poder usarse entre clases de forma global
		/// </summary>
		public static ListaUnica<Zona> DiccionarioOffsetsZonas = new ListaUnica<Zona>();
		static readonly int NumeroCompilaciones = Enum.GetNames(typeof(CompilacionRom.Compilacion)).Length;
		string variable;
		LlistaOrdenada<Edicion,Hex[]> diccionarioZonas;
		public Zona(string variable)
		{
			this.variable = variable;
			diccionarioZonas = new LlistaOrdenada<Edicion, Hex[]>();
		}
		public string Variable {
			get {
				return variable;
			}
			set {
				if (value == null)
					value = "";
				variable = value;
			}
		}
		public Hex[] this[Edicion edicionZona] {
			get {
				if (!diccionarioZonas.Existeix(edicionZona))
					throw new RomInvestigacionExcepcion();
				return diccionarioZonas[edicionZona];
			}
			set {
				if (!diccionarioZonas.Existeix(edicionZona))
					throw new RomInvestigacionExcepcion();
				diccionarioZonas[edicionZona] = value;
			}
		}
		public Hex this[Edicion edicionZona, CompilacionRom.Compilacion compilacion] {
			get {
				if (compilacion < CompilacionRom.Compilacion.Primera || compilacion > CompilacionRom.Compilacion.Segunda)
					throw new ArgumentOutOfRangeException();
				return this[edicionZona][(int)compilacion];
			}
			set {
				if (compilacion < CompilacionRom.Compilacion.Primera || compilacion > CompilacionRom.Compilacion.Segunda)
					throw new ArgumentOutOfRangeException();
				Hex[] zonasPorCompilacion = this[edicionZona];
				zonasPorCompilacion[(int)compilacion] = value;
				this[edicionZona] = zonasPorCompilacion;
			}
		}
		public void AddOrReplaceZonaOffset(Edicion edicion, params Hex[] zonasOffset)
		{
			AddOrReplaceZonaOffset(edicion, true, zonasOffset);
		}
		/// <summary>
		/// añade o reemplaza las zonas para esa edicion
		/// </summary>
		/// <param name="edicion"></param>
		/// <param name="ponerPrimeraZonaSiFaltan">si es false pone la ultima</param>
		/// <param name="zonasOffset">el orden lo indica la compilacion de destino, en caso de faltar se pondrá la primera hasta tener todas</param>
		public void AddOrReplaceZonaOffset(Edicion edicion, bool ponerPrimeraZonaSiFaltan, params Hex[] zonasOffset)
		{
			Hex[] zonasAPoner;
			
			if (edicion == null || zonasOffset == null)
				throw new ArgumentNullException();
			if (zonasOffset.Length > NumeroCompilaciones)
				zonasAPoner = zonasOffset.SubArray(0, NumeroCompilaciones);
			else if (zonasOffset.Length < NumeroCompilaciones) {
				zonasAPoner = new Hex[NumeroCompilaciones];
				for (int i = zonasOffset.Length; i < NumeroCompilaciones; i++)
					if (ponerPrimeraZonaSiFaltan)
						zonasAPoner[i] = zonasOffset[0];
					else
						zonasAPoner[i] = zonasOffset[zonasOffset.Length - 1];
			} else
				zonasAPoner = (Hex[])zonasOffset.Clone();//asi evito que modifiquen la array desde fuera sin control
			diccionarioZonas.AfegirORemplaçar(edicion, zonasAPoner);
		}
		public bool RemoveZonaOffset(Edicion edicion)
		{
			if (edicion == null)
				throw new ArgumentNullException("edicion");
			return diccionarioZonas.Elimina(edicion);
		}

		#region IClauUnicaPerObjecte implementation
		public IComparable Clau()
		{
			return variable;
		}
		#endregion
		#region IComparable implementation


		public int CompareTo(object obj)
		{
			return CompareTo(obj as Zona);
		}

		#region IComparable implementation
		public int CompareTo(Zona other)
		{
			int compareTo;
			if (other != null)
				compareTo = string.Compare(variable, other.Variable, StringComparison.Ordinal);
			else
				compareTo = -1;
			return compareTo;
		}
		#endregion

		#endregion
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="variableZona">Usa el diccionario para obtener la zona </param>
		/// <returns></returns>
		public static Hex GetOffset(RomPokemon rom, string variableZona)
		{
			if (string.IsNullOrEmpty(variableZona))
				throw new ArgumentNullException();
			return GetOffset(rom, DiccionarioOffsetsZonas[variableZona]);
		}
		public static Hex GetOffset(RomPokemon rom, Zona zona)
		{
			return GetOffset(rom, zona, Edicion.GetEdicion(rom));
		}
		public static Hex GetOffset(RomPokemon rom, Zona zona, Edicion edicion)
		{
			return GetOffset(rom, zona[edicion, CompilacionRom.GetCompilacion(rom, edicion)]);
		}
		public static Hex GetOffset(RomPokemon rom, Zona zona, Edicion edicion, CompilacionRom.Compilacion compilacion)
		{
			if (rom == null || zona == null || edicion == null)
				throw new ArgumentNullException();
			return GetOffset(rom, zona[edicion, compilacion]);
		}
		public static Hex GetOffset(RomPokemon rom, Hex offsetZona)
		{
			byte[] bytesPointer = BloqueBytes.GetBytes(rom, offsetZona, (int)Longitud.Offset).Bytes;
			return ((Hex)(new byte[] {
				bytesPointer[2],
				bytesPointer[1],
				bytesPointer[0]
			})) + (bytesPointer[3] == 9 ? (int)Longitud.DieciseisMegas : 0);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="variableZona">Usa el diccionario para obtener la zona </param>
		/// <param name="offsetToSave"></param>
		public static void SetOffset(RomPokemon rom, string variableZona, Hex offsetToSave)
		{
			if (string.IsNullOrEmpty(variableZona))
				throw new ArgumentNullException();
			SetOffset(rom, DiccionarioOffsetsZonas[variableZona], offsetToSave);
		}
		/// <summary>
		/// Actualiza el pointer de todas las zonas donde estaba el anterior
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="zona"></param>
		/// <param name="offsetToSave"></param>
		public static void SetOffset(RomPokemon rom, Zona zona, Hex offsetToSave)
		{
			if (zona == null)
				throw new ArgumentNullException();
			SetOffset(rom, zona, Edicion.GetEdicion(rom), offsetToSave);
		}
		public static void SetOffset(RomPokemon rom, Zona zona, Edicion edicion, Hex offsetToSave)
		{
			if (zona == null)
				throw new ArgumentNullException();
			SetOffset(rom, zona, edicion, CompilacionRom.GetCompilacion(rom, edicion), offsetToSave);
		}
		public static void SetOffset(RomPokemon rom, Zona zona, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
		{
			if (zona == null)
				throw new ArgumentNullException();
			SetOffset(rom, zona[edicion, compilacion], offsetToSave);
		}
		/// <summary>
		/// Actualiza el pointer de todas las zonas donde estaba el anterior
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="offsetZona"></param>
		/// <param name="offsetToSave"></param>
		public static void SetOffset(RomPokemon rom, Hex offsetZona, Hex offsetToSave)
		{
			if (rom == null)
				throw new ArgumentNullException();
			if (offsetZona < 0 || offsetToSave < 0 || offsetToSave > (int)Longitud.TrentaYDosMegas || offsetZona + (int)Longitud.Offset > rom.Datos.Length)
				throw new ArgumentOutOfRangeException();
			
			Hex offsetZonaAActualizar = 0;//buscar el minimo real porque no hay pointers en el byte 4 (ya que 0+(int)Offsets.Longitud.Offset)
			byte[] bytesOffsetOld;
			uint offset = offsetToSave;
			bool esNueve = offset > (int)Longitud.DieciseisMegas;
			byte[] bytesPointer = new byte[(int)Longitud.Offset];
			int posicion = 0;
			
			if (esNueve)
				offset -= (int)Longitud.DieciseisMegas;
			bytesPointer[posicion++] = Convert.ToByte((offset & 0xff));
			bytesPointer[posicion++] = Convert.ToByte(((offset >> 8) & 0xff));
			bytesPointer[posicion++] = Convert.ToByte(((offset >> 0x10) & 0xff));
			if (esNueve)
				bytesPointer[posicion] = 0x9;
			else
				bytesPointer[posicion] = 0x8;

			bytesOffsetOld = BloqueBytes.GetBytes(rom, offsetZona, (int)Longitud.Offset).Bytes;//los bytes del offset a cambiar por el nuevo
			//busco todos los offsets que tengan los bytes viejos y los sustituyo (desde el principio)
			do {
				offsetZonaAActualizar =	BloqueBytes.SearchBytes(rom, offsetZonaAActualizar + (int)Longitud.Offset, bytesOffsetOld);
				if (offsetZonaAActualizar > -1)
					BloqueBytes.SetBytes(rom, offsetZonaAActualizar, bytesPointer);
			} while(offsetZonaAActualizar > -1);
		}
		//hacer metodo para guardar y cargar zonas desde un xml
		
	}
}
