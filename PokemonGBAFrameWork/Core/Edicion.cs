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
			Rubi,
			Zafiro,
			RojoFuego,
			VerdeHoja,
			Esmeralda
				
		}
		public const string ABREVIACIONROJOFUEGO = "BPR";
		public const string ABREVIACIONVERDEHOJA = "BPG";
		public const string ABREVIACIONESMERALDA = "BPE";
		public const string ABREVIACIONRUBI = "AXV";
		public const string ABREVIACIONZAFIRO = "AXP";
		//falta ponerlos
		public const string NOMBRECOMPLETOROJOFUEGO = "BPR";
		public const string NOMBRECOMPLETOVERDEHOJA = "BPG";
		public const string NOMBRECOMPLETOESMERALDA = "BPE";
		public const string NOMBRECOMPLETORUBI = "AXV";
		public const string NOMBRECOMPLETOZAFIRO = "AXP";
		//valores fijos
		public static readonly Edicion RubiUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Rubi, Edicion.Idioma.Ingles);
		public static readonly Edicion ZafiroUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Zafiro, Edicion.Idioma.Ingles);
		public static readonly Edicion EsmeraldaUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Esmeralda, Edicion.Idioma.Ingles);
		public static readonly Edicion RojoFuegoUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.RojoFuego, Edicion.Idioma.Ingles);
		public static readonly Edicion VerdeHojaUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.VerdeHoja, Edicion.Idioma.Ingles);
			
		public static readonly Edicion RubiEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Rubi, Edicion.Idioma.Español);
		public static readonly Edicion ZafiroEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Zafiro, Edicion.Idioma.Español);
		public static readonly Edicion EsmeraldaEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Esmeralda, Edicion.Idioma.Español);
		public static readonly Edicion RojoFuegoEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.RojoFuego, Edicion.Idioma.Español);
		public static readonly Edicion VerdeHojaEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.VerdeHoja, Edicion.Idioma.Español);
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
				inicialIdioma = Char.ToUpper(value);
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

		public static Edicion[] ObtenerTodasLasEdicionesCanon()
		{
			EdicionesPokemon[] ediciones = (EdicionesPokemon[])Enum.GetValues(typeof(EdicionesPokemon));
			Idioma[] idiomas = (Idioma[])Enum.GetValues(typeof(Idioma));
			Edicion[] edicionesCanon = new Edicion[ediciones.Length * idiomas.Length];
			//las pondré en el orden que toque para detectar que edicion mirando la descripcionPokedex   :)
			for (int i = 0; i < ediciones.Length; i++)
				for (int j = 0; j < idiomas.Length; j++)
					edicionesCanon[i * idiomas.Length + j] = GetEdicionCanon(ediciones[i], idiomas[j]);
			return edicionesCanon;
		}

		public static Edicion GetEdicion(RomPokemon rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			Edicion edicion = new Edicion(Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.NombreCompleto, (int)LongitudCampos.NombreCompleto).Bytes), Serializar.ToString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.Abreviacion, (int)LongitudCampos.Abreviacion).Bytes), (char)rom.Datos[(int)OffsetsCampos.Idioma]);
			Edicion aux = edicion;
			//ahora detecto si tiene bien el formato mirando la compilacion
			
			bool valida;
			Edicion[] edicionesCanon;
			int indice;
			try {
				CompilacionRom.GetCompilacion(rom, edicion);//en un futuro si origina una excepcion es que tiene que ver con el formato, de momento es que no se corresponde con la edicion que tiene el formato.
				valida = true;
			} catch {
				valida = false;
			}
			//como de momento no se puede cambiar el formato para crear una edicion noOficial pues tiene que ser una oficial
			if (!valida) {
				edicionesCanon = ObtenerTodasLasEdicionesCanon();
				indice = 0;
				edicion = edicionesCanon[indice++];
				while (!valida && indice < edicionesCanon.Length) {
					
					try {
						CompilacionRom.GetCompilacion(rom, edicion);//si origina una excepcion es que tiene que ver con el formato
						valida = true;
					} catch {
						//pongo la siguiente edicion
						edicion = edicionesCanon[indice++];
					}
					
				}
				if (indice == edicionesCanon.Length)
					throw new InvalidRomFormat();//si no es ninguna edicion canon es que no tiene el formato correcto
				if (edicion.Abreviacion != ABREVIACIONRUBI) {
					//como Rubi/Zafiro  no se pueden diferenciar lo hago aqui
					
					//puede ser zafiro pero no se detectarla...
					
				}
				edicion.NombreCompleto = aux.NombreCompleto;//como no implica nada para escoger zonas pues lo conservo
			}
			
			return edicion;
		}
		/*de momento sera simple  se tiene que poder exportar zonas con la edicion que han dicho que es*/
		/// <summary>
		/// Permite cambiar la edicion, pero si se cambia la Abreviacion luego si no se carga la informacion para leerla originará en el GetEdicion una RomInvestigacionExcepcion.
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="edicion"></param>
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
