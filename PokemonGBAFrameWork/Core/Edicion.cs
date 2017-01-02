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
			NombreCompleto = 12,
			Abreviacion = 3,
			Idioma = 1
		}
		public enum Idioma
		{
			Spanish,
			English,
			Other
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
		public const string NOMBRECOMPLETOROJOFUEGO = "POKEMON FIRE";
		public const string NOMBRECOMPLETOVERDEHOJA = "POKEMON LEAF";
		public const string NOMBRECOMPLETOESMERALDA = "POKEMON EMER";
		public const string NOMBRECOMPLETORUBI = "POKEMON RUBY";
		public const string NOMBRECOMPLETOZAFIRO = "POKEMON SAPP";
		//valores fijos
		public static readonly Edicion RubiUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Rubi, Edicion.Idioma.English);
		public static readonly Edicion ZafiroUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Zafiro, Edicion.Idioma.English);
		public static readonly Edicion EsmeraldaUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Esmeralda, Edicion.Idioma.English);
		public static readonly Edicion RojoFuegoUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.RojoFuego, Edicion.Idioma.English);
		public static readonly Edicion VerdeHojaUsa = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.VerdeHoja, Edicion.Idioma.English);
			
		public static readonly Edicion RubiEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Rubi, Edicion.Idioma.Spanish);
		public static readonly Edicion ZafiroEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Zafiro, Edicion.Idioma.Spanish);
		public static readonly Edicion EsmeraldaEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.Esmeralda, Edicion.Idioma.Spanish);
		public static readonly Edicion RojoFuegoEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.RojoFuego, Edicion.Idioma.Spanish);
		public static readonly Edicion VerdeHojaEsp = Edicion.GetEdicionCanon(Edicion.EdicionesPokemon.VerdeHoja, Edicion.Idioma.Spanish);
		string nombreCompleto;
		string abreviacion;
		char inicialIdioma;
        string abreviacionOffetsRom;
        Idioma idiomaOffsetsRom;
        string abreviacionMasIdiomaOffsetRom;
        public Edicion(string nombreCompleto, string abreviacion, char inicialIdioma)
		{
			NombreCompleto = nombreCompleto;
			Abreviacion = abreviacion;
			InicialIdioma = inicialIdioma;
            abreviacionOffetsRom = "";
            idiomaOffsetsRom = Idioma.Other;
            abreviacionMasIdiomaOffsetRom = MiRandom.Next() + "";//los que no se saquen de una rom que puedan ponerse en listas ordenadas :D
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
        public string AbreviacionRom
        {
            get { return abreviacionOffetsRom; }
            private set {
                abreviacionOffetsRom = value;
                abreviacionMasIdiomaOffsetRom = AbreviacionRom + idiomaOffsetsRom.ToString()[0];
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
						idioma = Idioma.Spanish;
						break;
					case 'E':
						idioma = Idioma.English;
						break;
					default:
						idioma = Idioma.Other;
						break;
				}
				return idioma;
			}
            set
            {
                if (value < Idioma.Spanish || value > Idioma.Other) throw new ArgumentOutOfRangeException();
                InicialIdioma = value.ToString()[0];
            }
		}
        /// <summary>
		/// Se usará para obtener los offsets rápidamente
		/// </summary>
        public Idioma IdiomaOffsets
        {
            get
            {
                return idiomaOffsetsRom;
            }

          private  set
            {
                idiomaOffsetsRom = value;
                abreviacionMasIdiomaOffsetRom = AbreviacionRom + idiomaOffsetsRom.ToString()[0];
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
				compareTo = string.Compare(abreviacionMasIdiomaOffsetRom, other.abreviacionMasIdiomaOffsetRom, StringComparison.Ordinal);
			else
				compareTo = -1;
			return compareTo;
		}

		#endregion
		public static Edicion GetEdicionCanon(EdicionesPokemon edicion, Idioma idioma)
		{
			char inicialIdioma = char.ToUpper(idioma.ToString()[0]);
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
            edicionCanon.AbreviacionRom = edicionCanon.Abreviacion;
            edicionCanon.IdiomaOffsets = edicionCanon.IdiomaRom;
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

		public static Edicion GetEdicion(RomGBA rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			Edicion edicion = new Edicion(System.Text.ASCIIEncoding.ASCII.GetString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.NombreCompleto, (int)LongitudCampos.NombreCompleto).Bytes), System.Text.ASCIIEncoding.ASCII.GetString(BloqueBytes.GetBytes(rom, (int)OffsetsCampos.Abreviacion, (int)LongitudCampos.Abreviacion).Bytes), (char)rom.Datos[(int)OffsetsCampos.Idioma]);
			Edicion aux;
			//ahora detecto si tiene bien el formato mirando la compilacion
			
			bool valida;
			Edicion[] edicionesCanon;
			int indice;
			try {
				CompilacionRom.GetCompilacion(rom, edicion);//en un futuro si origina una excepcion es que tiene que ver con el formato, de momento es que no se corresponde con la edicion que tiene el formato.
				valida = true;
                //como es valida le pongo los datos correctos :)
                edicion.IdiomaOffsets = edicion.IdiomaRom;
                edicion.AbreviacionRom = edicion.Abreviacion;
			} catch {
				valida = false;
			}
			//como de momento no se puede cambiar el formato para crear una edicion noOficial pues tiene que ser una oficial
			if (!valida) {
				edicionesCanon = ObtenerTodasLasEdicionesCanon();
				indice = 0;
                aux = edicionesCanon[indice++];
				while (!valida && indice < edicionesCanon.Length) {
					
					try {
						CompilacionRom.GetCompilacion(rom, aux);//si origina una excepcion es que tiene que ver con el formato
						valida = true;
					} catch {
                        //pongo la siguiente edicion
                        aux = edicionesCanon[indice++];
					}
					
				}
				if (indice == edicionesCanon.Length)
					throw new InvalidRomFormat();//si no es ninguna edicion canon es que no tiene el formato correcto
				if (aux.Abreviacion != ABREVIACIONRUBI) {
					//como Rubi/Zafiro  no se pueden diferenciar lo hago aqui
					
					//puede ser zafiro pero no se detectarla...
					
				}
				edicion.AbreviacionRom = aux.AbreviacionRom;
                edicion.IdiomaOffsets = aux.IdiomaRom;
			}
			
			return edicion;
		}
		/// <summary>
		/// Permite cambiar la Edicion en apariencia porque implica muchos cambios un cambio de edicion completo y no se puede todavia 
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="edicion"></param>
		public static void SetEdicion(RomGBA rom, Edicion edicion)
		{
			if (rom == null || edicion == null)
				throw new ArgumentNullException();
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.NombreCompleto, System.Text.ASCIIEncoding.ASCII.GetBytes(edicion.NombreCompleto.PadRight((int)LongitudCampos.NombreCompleto)));
			BloqueBytes.SetBytes(rom, (int)OffsetsCampos.Abreviacion, System.Text.ASCIIEncoding.ASCII.GetBytes(edicion.Abreviacion.PadRight((int)LongitudCampos.Abreviacion)));
			rom.Datos[(int)OffsetsCampos.Idioma] = (byte)edicion.InicialIdioma;
		}
	}
}
