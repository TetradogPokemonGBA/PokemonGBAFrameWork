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
		public const string ROJOFUEGO = "BPR";
		public const string VERDEHOJA = "BPG";
		public const string ESMERALDA = "BPE";
		public const string RUBI = "AXV";
		public const string ZAFIRO = "AXP";
		
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
		public static Edicion GetEdicion(RomPokemon rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return new Edicion(Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.NombreCompleto, (int)LongitudCampos.NombreCompleto).Bytes), Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.Abreviacion, (int)LongitudCampos.Abreviacion).Bytes), (char)rom.Datos[(int)OffsetsCampos.Idioma]);
		}
		public static void SetEdicion(RomPokemon rom, Edicion edicion)
		{
			if (rom == null || edicion == null)
				throw new ArgumentNullException();
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.NombreCompleto, Serializar.GetBytes(edicion.NombreCompleto.PadRight((int)LongitudCampos.NombreCompleto)));
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.Abreviacion, Serializar.GetBytes(edicion.Abreviacion.PadRight((int)LongitudCampos.Abreviacion)));
			rom.Datos[(int)OffsetsCampos.Idioma] = (byte)edicion.InicialIdioma;
		}
	}
}
