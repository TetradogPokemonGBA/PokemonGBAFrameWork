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
	/// <summary>
	/// Esta clase sirve para tratar con offsets
	/// </summary>
	public class Offsets
	{
		public enum Longitud{
		Offset=4,
		}
		public static readonly uint DieciseisMegas = (Hex)"FFFFFF";
		public static readonly uint TrentaYDosMegas = DieciseisMegas*2;
		
	}
	public enum Compilacion
	{
		Primera,
		Segunda
		//si hay mas se ponen cuando sean necesarias
	}
	/// <summary>
	/// las zonas son offsets donde se guardan offsets permutados.
	/// </summary>
	public class Zona
	{
		Compilacion compilacion;
		Edicion edicion;
		string variable;
		Hex offsetZona;

		public Compilacion Compilacion {
			get {
				return compilacion;
			}
			set {
				compilacion = value;
			}
		}
		public Edicion Edicion {
			get {
				return edicion;
			}
			set {
				if (value == null)
					throw new ArgumentNullException();
				edicion = value;
			}
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

		public Hex OffsetZona {
			get {
				return offsetZona;
			}
			set {
				if(value<0)throw new ArgumentOutOfRangeException();
				offsetZona = value;
			}
		}
		public static Hex GetOffset(RomPokemon rom,Zona zona)
		{
			return GetOffset(rom,zona.OffsetZona);
		}
		public static Hex GetOffset(RomPokemon rom,Hex offsetZona)
		{
			byte[] bytesPointer=BloqueBytes.GetBytes(rom,offsetZona,(int)Offsets.Longitud.Offset).Bytes;
			return ((Hex)(new byte[] { bytesPointer[2], bytesPointer[1], bytesPointer[0] })) + (bytesPointer[3] == 9 ? Offsets.DieciseisMegas: 0);
		}
		public static void SetOffset(RomPokemon rom,Zona zona,Hex offsetToSave)
		{
			if(zona==null)throw new ArgumentNullException();
			SetOffset(rom,zona.OffsetZona,offsetToSave);
		}
		public static void SetOffset(RomPokemon rom,Hex offsetZona,Hex offsetToSave)
		{
			if(rom==null)throw new ArgumentNullException();
			if(offsetZona<0||offsetToSave<0||offsetToSave>Offsets.TrentaYDosMegas||offsetZona+(int)Offsets.Longitud.Offset>rom.Datos.Length)
				throw new ArgumentOutOfRangeException();
			
			byte[] bytesOffsetOld;
			uint offset=offsetToSave;
            bool esNueve = offset > Offsets.DieciseisMegas;
            byte[] bytesPointer = new byte[(int)Offsets.Longitud.Offset];
            int posicion = 0;
            if (esNueve)
                offset -= Offsets.DieciseisMegas;
            bytesPointer[posicion++] = Convert.ToByte((offset & 0xff));
            bytesPointer[posicion++] = Convert.ToByte(((offset >> 8) & 0xff));
            bytesPointer[posicion++] = Convert.ToByte(((offset >> 0x10) & 0xff));
            if (esNueve)
                bytesPointer[posicion] = 0x9;
            else
                bytesPointer[posicion] = 0x8;

			bytesOffsetOld=BloqueBytes.GetBytes(rom,offsetZona,(int)Offsets.Longitud.Offset).Bytes;//los bytes del offset a cambiar por el nuevo

			BloqueBytes.SetBytes(rom,offsetZona,bytesPointer);
			//busco todos los offsets que tengan los bytes
		}
		
	}
}
