/*
 * Creado por SharpDevelop.
 * Usuario: pc
 * Fecha: 27/08/2016
 * Hora: 18:24
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Sirve para identificar la rom y poder obtener los offsets que tocan :)
	/// </summary>
	public class Edicion:IComparable<Edicion>,IComparable
	{
		public enum OffsetsCampos
		{
			NombreCompleto = 160,
			Abreviacion = 172,
			Idioma = Abreviacion + LongitudCampos.Abreviacion
		}
		public enum LongitudCampos
		{
			NombreCompleto = 11,
			Abreviacion = 3,
			Idioma = 1
		}
		public enum Idioma
		{
			Español,
			Ingles,
			Otro
		}
		public enum EdicionesPokemon
		{
			RojoFuego,
VerdeHoja,
Esmeralda,
Rubi,
Zafiro
		}
		public const string ABREVIACIONROJOFUEGO = "BPR";
		public const string ABREVIACIONVERDEHOJA = "BPG";
		public const string ABREVIACIONESMERALDA = "BPE";
		public const string ABREVIACIONRUBI = "AXV";
		public const string ABREVIACIONZAFIRO = "AXP";
		
		public const string NOMBRECOMPLETOROJOFUEGO = "BPR";
		public const string NOMBRECOMPLETOVERDEHOJA = "BPG";
		public const string NOMBRECOMPLETOESMERALDA = "BPE";
		public const string NOMBRECOMPLETORUBI = "AXV";
		public const string NOMBRECOMPLETOZAFIRO = "AXP";
		string nombreCompleto;
		string abreviacion;
		char inicialIdioma;
		
		public Edicion(string nombreCompleto, string abreviacion, char inicialIdioma)
		{
			NombreCompleto = nombreCompleto;
			Abreviacion = abreviacion;
			InicialIdioma = inicialIdioma;
		}
		

		public string NombreCompleto {
			get {
				return nombreCompleto;
			}
			set {
				if (value == null || value.Length > (int)LongitudCampos.NombreCompleto)
					throw new ArgumentException();
				nombreCompleto = value;
			}
		}
		/// <summary>
		/// Se usará para obtener los offsets rápidamente
		/// </summary>
		public string Abreviacion {
			get {
				return abreviacion;
			}
			set {
				if (value == null || value.Length > (int)LongitudCampos.Abreviacion)
					throw new ArgumentException();
				
				abreviacion = value;
			}
		}
		/// <summary>
		/// Se usará para obtener los offsets rápidamente
		/// </summary>
		public char InicialIdioma {
			get {
				return inicialIdioma;
			}
			set {
				inicialIdioma = value;
			}
		}
		public Idioma IdiomaRom {
			get {
				Idioma idioma;
				switch (InicialIdioma) {
					case 'S':
						idioma = Idioma.Español;
						break;
					case 'E':
						idioma = Idioma.Ingles;
						break;
					default:
						idioma = Idioma.Otro;
						break;
				}
				return idioma;
			}
		}
		#region IComparable implementation
		public int CompareTo(object obj)
		{
			return CompareTo(obj as Edicion);
		}
		#endregion
		#region IComparable implementation

		public int CompareTo(Edicion other)
		{
			int compareTo;
			if (other != null)
				compareTo = string.Compare((Abreviacion + InicialIdioma), (other.Abreviacion + other.InicialIdioma), StringComparison.Ordinal);
			else
				compareTo = -1;
			return compareTo;
		}

		#endregion
		public static Edicion GetEdicionCanon(EdicionesPokemon edicion, Idioma idioma)
		{
			char inicialIdioma = char.ToLower(idioma.ToString()[0]);
			Edicion edicionCanon = null;
			switch (edicion) {
				case EdicionesPokemon.RojoFuego:
					edicionCanon = new Edicion(NOMBRECOMPLETOROJOFUEGO, ABREVIACIONROJOFUEGO, inicialIdioma);
					break;
				case EdicionesPokemon.VerdeHoja:
					edicionCanon = new Edicion(NOMBRECOMPLETOVERDEHOJA, ABREVIACIONVERDEHOJA, inicialIdioma);
					break;
				case EdicionesPokemon.Esmeralda:
					edicionCanon = new Edicion(NOMBRECOMPLETOESMERALDA, ABREVIACIONESMERALDA, inicialIdioma);
					break;
				case EdicionesPokemon.Rubi:
					edicionCanon = new Edicion(NOMBRECOMPLETORUBI, ABREVIACIONRUBI, inicialIdioma);
					break;
				case EdicionesPokemon.Zafiro:
					edicionCanon = new Edicion(NOMBRECOMPLETOZAFIRO, ABREVIACIONZAFIRO, inicialIdioma);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			return edicionCanon;
			
		}
		public static Edicion GetEdicion(RomPokemon rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			Edicion edicion = new Edicion(Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.NombreCompleto, (int)LongitudCampos.NombreCompleto).Bytes), Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.Abreviacion, (int)LongitudCampos.Abreviacion).Bytes), (char)rom.Datos[(int)OffsetsCampos.Idioma]);
			//ahora detecto si tiene bien el formato mirando la compilacion
			CompilacionRom.GetCompilacion(rom, edicion);//si origina una excepcion es que tiene que ver con el formato
		
		}
		/*de momento no se puede cambiar porque exige mas cosas aparte de lo que hay actualmente
		static void SetEdicion(RomPokemon rom, Edicion edicion)
		{
			if (rom == null || edicion == null)
				throw new ArgumentNullException();
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.NombreCompleto, Serializar.GetBytes(edicion.NombreCompleto.PadRight((int)LongitudCampos.NombreCompleto)));
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.Abreviacion, Serializar.GetBytes(edicion.Abreviacion.PadRight((int)LongitudCampos.Abreviacion)));
			rom.Datos[(int)OffsetsCampos.Idioma] = (byte)edicion.InicialIdioma;
		}*/
	}
}
