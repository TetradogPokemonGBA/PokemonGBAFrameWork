/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 15:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
//informacion de stats sacada de Pokemon Game Editor ->Créditos a Gamer2020
namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of Pokemon.
    /// </summary>
    public class Pokemon : IComparable, IComparable<Pokemon>
    {
        //en construccion
        public enum LongitudCampos
        {
            NombreCompilado = 11,
            Nombre = NombreCompilado + 2,//para los Nidoran[f] se guarda [f] como un caracter...de alli que le sume 2 '[',']'
            TotalStats = 28,
        }
        public enum OrdenPokemon
        {
            GameFreak,
            Local,
            Nacional
        }
        public enum NivelEvs
        {
            Cero,
            Uno,
            Dos,
            Tres
        }
        public enum StatEvs
        {
            Hp,
            Ataque,
            Velocidad,
            Defensa,
            AtaqueEspecial,
            DefensaEspecial
        }

        public enum RatioCrecimiento
        {
            Exp1000000,
            Exp600000,
            Exp1640000,
            Exp1059860,
            Exp800000,
            Exp1250000
        }

        public enum RatioGenero
        {
            Macho100 = 0,
            //no se puede poner el caracter %
            Macho87 = 31,
            Macho75 = 63,
            Macho65 = 89,
            Macho50Hembra = 127,
            Hembra65 = 165,
            Hembra75 = 191,
            Hembra87 = 223,
            Hembra100 = 254,
            SinGenero = 255
        }
        public enum Felicidad
        {
            Minima = 0,
            Baja = 35,
            Normal = 70,
            MediAlta = 90,
            Alta = 100,
            MuyAlta = 140,
            Maxima = 255

        }
        public enum Color
        {
            Rojo,
            Azul,
            Amarillo,
            Verde,
            Negro,
            Marron,
            Púrpura,
            Gris,
            Blanco,
            Rosa

        }
        public enum Variables
        {
            OrdenLocal,
            OrdenNacional,
            Nombre,
            Stats
        }
        public static OrdenPokemon Orden = OrdenPokemon.Local;
        const int PASOSCICLOECLOSION = 256, MAXIMOSPASOSECLOSION = PASOSCICLOECLOSION * PASOSCICLOECLOSION;
        const int LENGTHNIVELEVS = 4;
        /// <summary>
        /// forma parte de un stat junto con la de color en el mismo byte numero 25
        /// </summary>
        const byte FACELEFT = 128;


        BloqueString nombre;
        byte[] stats;
        int objeto1, objeto2;
        int ordenPokedexLocal;
        int ordenPokedexNacional;
        int ordenGameFreak;
        DescripcionPokedex descripcion;
        Sprite sprites;
        /*por desarrollar
		//falta huella
		//falts miniSprites
		//falta Cry
		//falta ataques, mt y mo
		*/
        static Pokemon()
        {
            Zona zonaOrdenLocal = new Zona(Variables.OrdenLocal);
            Zona zonaOrdenNacional = new Zona(Variables.OrdenNacional);
            Zona zonaNombre = new Zona(Variables.Nombre);
            Zona zonaStats = new Zona(Variables.Stats);

            //orden local
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x3F98BC);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x3F98BC);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x3F7F0);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x3F7F0);

            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x430DC);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x430DC);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x431F0, 0x43204);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x431F0, 0x43204);

            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x6d3fc);
            zonaOrdenLocal.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x6d3fc);
            //orden nacional
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x3FA08);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x3FA08);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x3F83C);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x3F83C);

            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x43128);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x43128);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x4323C, 0x43250);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x4323C, 0x43250);

            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x6D448);
            zonaOrdenNacional.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x6D448);

            //nombre
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xFA58);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xFA58);

            zonaNombre.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x144);

            zonaNombre.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x144);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x144);


            //stats
            zonaStats.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x10B64);
            zonaStats.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x10B64);

            zonaStats.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1BC);

            zonaStats.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1BC);
            zonaStats.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1BC);

            //añado las zonas
            Zona.DiccionarioOffsetsZonas.Añadir(new Zona[] {
                zonaNombre,
                zonaOrdenLocal,
                zonaOrdenNacional,
                zonaStats
            });
        }
        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Texto.Length > (int)LongitudCampos.Nombre)
                    throw new ArgumentOutOfRangeException("value", "el nombre no puede superar el maximo de caracteres que es " + (int)LongitudCampos.Nombre);
                nombre = value;
                nombre.MaxCaracteres = (int)LongitudCampos.Nombre;//por si tiene otro
            }
        }
        public int OrdenPokedexLocal
        {
            get
            {
                return ordenPokedexLocal;
            }
            set
            {
                if (value < 0 || value > short.MaxValue)
                    throw new ArgumentOutOfRangeException();
                ordenPokedexLocal = value;
            }
        }
        public int OrdenPokedexNacional
        {
            get
            {
                return ordenPokedexNacional;
            }
            set
            {
                if (value < 0 || value > short.MaxValue)
                    throw new ArgumentOutOfRangeException();
                ordenPokedexNacional = value;
            }
        }

        public DescripcionPokedex Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                descripcion = value;
            }
        }



        public Sprite Sprites
        {
            get
            {
                return sprites;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                sprites = value;
            }
        }


        //para poder hacer el set necesito saber el total de objetos para poder guardar los objetos que puede llevar...
        /// <summary>
        /// para obtener y asignar todos los stats de golpe :)
        /// </summary>
        public byte[] Stats
        {
            get
            {
                return stats;

            }

            set
            {
                if (value == null || value.Length != (int)LongitudCampos.TotalStats)
                    throw new ArgumentException();
                stats = value;
            }
        }
        /// <summary>
        /// Es el orden en el que se encuentran los pokemon en la ROM
        /// </summary>
        public int OrdenGameFreak
        {
            get { return ordenGameFreak; }
            set
            {
                if (value < 0 || value > ushort.MaxValue) throw new ArgumentOutOfRangeException();//el maximo no lo se...supongo que son dos bytes
                ordenGameFreak = value;
            }
        }
        #region Interpreta	Stats
        public int TotalStatsBase
        {
            get
            {
                int totalStatsBase = 0;
                for (int i = 0; i < 6; i++)
                    totalStatsBase += stats[i];
                return totalStatsBase;

            }
        }


        public byte Hp
        {
            get { return Stats[0]; }
            set
            {
                stats[0] = value;

            }
        }
        public byte Ataque
        {
            get { return Stats[1]; }
            set
            {
                stats[1] = value;
            }
        }


        public byte Defensa
        {
            get { return Stats[2]; }
            set
            {
                stats[2] = value;
            }
        }
        public byte Velocidad
        {
            get { return Stats[3]; }
            set
            {
                stats[3] = value;
            }
        }
        public byte AtaqueEspecial
        {
            get { return Stats[4]; }
            set
            {
                stats[4] = value;
            }
        }
        public byte DefensaEspecial
        {
            get { return Stats[5]; }
            set
            {
                stats[5] = value;
            }
        }
        public byte Tipo1
        {
            get
            {

                return stats[6];
            }
            set
            {

                stats[6] = value;

            }
        }
        public byte Tipo2
        {
            get
            {
                return stats[7];
            }
            set
            {

                stats[7] = value;

            }
        }
        public byte RatioCaptura
        {
            get { return stats[8]; }
            set
            {
                stats[8] = value;
            }
        }
        public byte ExperienciaBase
        {
            get { return stats[9]; }
            set
            {
                stats[9] = value;
            }
        }

        //SPE,DEF,ATK,HP
        #region EvsStats se tiene que testear :)
        public NivelEvs HpEvs
        {
            get
            {
                int posicion = (stats[10] - ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) - ((int)AtaqueEvs) * LENGTHNIVELEVS) / LENGTHNIVELEVS;//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                stats[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)value);

            }
        }
        public NivelEvs AtaqueEvs
        {
            get
            {
                int posicion = ((stats[10] - (((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)))) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3))) / LENGTHNIVELEVS;//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                stats[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)value) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs DefensaEvs
        {
            get
            {
                int posicion = (stats[10] - (((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)))) / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2));//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                stats[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs VelocidadEvs
        {
            get
            {
                int posicion = stats[10] / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3));//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                stats[11] = (byte)(((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs AtaqueEspecialEvs
        {
            get
            {
                int posicion = stats[11] - ((int)DefensaEspecialEvs) * LENGTHNIVELEVS;
                return (NivelEvs)posicion;
            }
            set
            {
                stats[11] = (byte)(((int)DefensaEspecialEvs) * LENGTHNIVELEVS + (int)value);
            }
        }
        public NivelEvs DefensaEspecialEvs
        {
            get
            {
                int posicion = stats[11] / LENGTHNIVELEVS;
                return (NivelEvs)posicion;
            }
            set
            {
                stats[11] = (byte)(((int)value) * LENGTHNIVELEVS + (int)AtaqueEspecialEvs);
            }
        }
        #endregion

        //item1 indexItems([12](%)+[13](/)*256);
        public int Objeto1
        {
            get
            {
                return objeto1;
            }
            set
            {
                if (value < 0 || value > short.MaxValue)
                    throw new ArgumentOutOfRangeException();
                objeto1 = value;
            }
        }
        //item2 indexItems([14](%)+[15](/)*256);
        public int Objeto2
        {
            get
            {
                return objeto2;
            }
            set
            {
                if (value < 0 || value > short.MaxValue)
                    throw new ArgumentOutOfRangeException();
                objeto2 = value;
            }
        }

        public RatioGenero RatioSexo
        {
            get { return (RatioGenero)(int)stats[16]; }
            set
            {
                if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
                    throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
                stats[16] = (byte)(int)value;
            }
        }
        /// <summary>
        /// Se usan multiplos de 256 ya que se guarda en un byte
        /// </summary>
        public int PasosParaEclosionarHuevo
        {
            get { return (int)stats[17] * PASOSCICLOECLOSION; }
            set
            {
                if ((int)value > MAXIMOSPASOSECLOSION || (int)value < 0)
                    throw new ArgumentOutOfRangeException("value", "el valor no puede más pequeño que 0 o ser más grande que " + MAXIMOSPASOSECLOSION);
                stats[17] = (byte)(value / PASOSCICLOECLOSION);
            }
        }
        /// <summary>
        /// Se puede poner valores de 0 a FF
        /// </summary>
        public Felicidad BaseAmistad
        {
            get { return (Felicidad)(int)stats[18]; }
            set
            {
                if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
                    throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
                stats[18] = (byte)(int)value;
            }
        }
        public RatioCrecimiento Crecimiento
        {
            get { return (RatioCrecimiento)(int)stats[19]; }//solo se usa la posicion de la enumeracion para determinar su crecimiento
            set
            {
                if (value < RatioCrecimiento.Exp1000000 || value > RatioCrecimiento.Exp1250000)
                    throw new ArgumentOutOfRangeException(String.Format("el valor no puede superar el numero {0} y tiene que ser positivo", Enum.GetNames(typeof(RatioCrecimiento)).Length - 1));
                stats[19] = (byte)(int)value;
            }
        }
        public byte GrupoHuevo1
        {
            get { return stats[20]; }
            set
            {
                stats[20] = value;
            }
        }
        public byte GrupoHuevo2
        {
            get { return stats[21]; }
            set
            {
                stats[21] = value;
            }
        }
        public byte Habilidad1
        {
            get { return stats[22]; }
            set
            {
                stats[22] = value;
            }
        }



        public byte Habilidad2
        {
            get { return stats[23]; }
            set
            {
                stats[23] = value;
            }
        }
        public byte RatioDeEscapar
        {
            get { return stats[24]; }
            set
            {
                stats[24] = value;
            }
        }
        #region Por mirar InGame
        //Color parece que se usa en la pokedex...
        public Color ColorBaseStat
        {//Hex(128 + Clr1.SelectedIndex) FaceLeft se el suma 128 al stat del color...si no esta es FaceRight
            get { return IsFaceRight ? (Color)(int)stats[25] : (Color)(int)(FACELEFT - stats[25]); }
            set
            {
                if (value < Color.Rojo || value > Color.Rosa)
                    throw new ArgumentOutOfRangeException();
                bool isFaceLeft = !IsFaceRight;
                stats[25] = (byte)(int)value;
                if (isFaceLeft)
                    stats[25] += FACELEFT;

            }
        }
        //creo que se usa en la imagen de los stats...
        public bool IsFaceRight
        {
            get { return stats[25] < FACELEFT; }
            set
            {
                if (value)
                {
                    if (!IsFaceRight)
                        stats[25] -= FACELEFT;
                }
                else
                {
                    if (IsFaceRight)
                        stats[25] += FACELEFT;
                }
            }
        }
        //PadBase 26,27??que es eso?? no se usa???
        public ushort PadBase
        {
            get
            {
                return Gabriel.Cat.Serializar.ToUShort(new byte[] {
                    stats[26],
                    stats[27]
                });
            }
            set
            {
                byte[] bytesPadBase = Gabriel.Cat.Serializar.GetBytes(value);
                stats[26] = bytesPadBase[0];
                stats[27] = bytesPadBase[1];
            }
        }

        public void SetObjetosEnLosStats(int totalObjetos)
        {
            if (totalObjetos < 0 || totalObjetos > short.MaxValue)
                throw new ArgumentOutOfRangeException();

            stats[12] = (byte)(Objeto1 % totalObjetos);
            stats[14] = (byte)(Objeto2 % totalObjetos);

            stats[13] = (byte)(Objeto1 / totalObjetos);
            stats[13] = (byte)(Objeto2 / totalObjetos);
        }
        public void GetObjetosDeLosStats()
        {
            Objeto1 = stats[12] + stats[13] * 256;
            Objeto2 = stats[14] + stats[15] * 256;
        }
        public NivelEvs GetEvs(StatEvs stat)
        {
            NivelEvs nivel;
            switch (stat)
            {
                case StatEvs.Hp:nivel = HpEvs;break;
                case StatEvs.Ataque: nivel = AtaqueEvs; break;
                case StatEvs.Defensa: nivel = DefensaEspecialEvs; break;
                case StatEvs.Velocidad: nivel = VelocidadEvs; break;
                case StatEvs.AtaqueEspecial: nivel = AtaqueEspecialEvs; break;
                case StatEvs.DefensaEspecial: nivel = DefensaEspecialEvs; break;
                default:throw new ArgumentOutOfRangeException();
            }
            return nivel;
        }
        public void SetEvs(StatEvs stat,NivelEvs nivel)
        {

            switch (stat)
            {
                case StatEvs.Hp:   HpEvs= nivel; break;
                case StatEvs.Ataque:AtaqueEvs= nivel; break;
                case StatEvs.Defensa: DefensaEspecialEvs = nivel; break;
                case StatEvs.Velocidad:  VelocidadEvs = nivel; break;
                case StatEvs.AtaqueEspecial:  AtaqueEspecialEvs = nivel; break;
                case StatEvs.DefensaEspecial:  DefensaEspecialEvs = nivel; break;

            }
     
        }
        #endregion
        #endregion
        public int CompareTo(object obj)
        {
            return CompareTo(obj as Pokemon);
        }

        public int CompareTo(Pokemon other)
        {
            int compareTo;
            if (other != null)
            {
                switch (Orden)
                {
                    case OrdenPokemon.GameFreak: compareTo = OrdenGameFreak.CompareTo(other.OrdenGameFreak); break;
                    case OrdenPokemon.Local: compareTo = OrdenPokedexLocal.CompareTo(other.OrdenPokedexLocal); break;
                    case OrdenPokemon.Nacional: compareTo = OrdenPokedexNacional.CompareTo(other.OrdenPokedexNacional); break;
                    default: throw new ArgumentException();
                }
            }
            else compareTo = -1;
            return compareTo;
        }
        //aun falta acabar pero de momento pongo lo que tengo
        public static void SetPokemon(RomPokemon rom, Pokemon pokemon)
        {
            SetPokemon(rom, pokemon, Objeto.TotalObjetos(rom));
        }
        public static void SetPokemon(RomPokemon rom, Pokemon pokemon, Hex totalObjetos)
        {
            SetPokemon(rom, Edicion.GetEdicion(rom), pokemon, totalObjetos);
        }
        public static void SetPokemon(RomPokemon rom, CompilacionRom.Compilacion compilacion, Pokemon pokemon)
        {
            SetPokemon(rom, Edicion.GetEdicion(rom), compilacion, pokemon, Objeto.TotalObjetos(rom));
        }
        public static void SetPokemon(RomPokemon rom, CompilacionRom.Compilacion compilacion, Pokemon pokemon, Hex totalObjetos)
        {
            SetPokemon(rom, Edicion.GetEdicion(rom), compilacion, pokemon, totalObjetos);
        }
        public static void SetPokemon(RomPokemon rom, Edicion edicion, Pokemon pokemon)
        {
            SetPokemon(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), pokemon, Objeto.TotalObjetos(rom));
        }
        public static void SetPokemon(RomPokemon rom, Edicion edicion, Pokemon pokemon, Hex totalObjetos)
        {
            SetPokemon(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), pokemon, totalObjetos);
        }
        public static void SetPokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Pokemon pokemon)
        {
            SetPokemon(rom, edicion, compilacion, pokemon, Objeto.TotalObjetos(rom));
        }
        public static void SetPokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Pokemon pokemon, Hex totalObjetos)
        {
            pokemon.SetObjetosEnLosStats(totalObjetos);
            Sprite.SetSprite(rom, pokemon.Sprites);
            BloqueBytes.SetBytes(rom, Zona.GetOffset(rom, Variables.Stats, edicion, compilacion) + pokemon.OrdenGameFreak * (int)LongitudCampos.TotalStats, pokemon.Stats);
            pokemon.Nombre.OffsetInicio = Zona.GetOffset(rom, Variables.Nombre, edicion, compilacion) + pokemon.OrdenGameFreak * (int)LongitudCampos.NombreCompilado;
            BloqueString.SetString(rom, pokemon.Nombre);
            DescripcionPokedex.SetDescripcionPokedex(rom, edicion, compilacion, pokemon.OrdenGameFreak, pokemon.Descripcion);
            //pongo el orden local y nacional...
            Word.SetWord(rom, Zona.GetOffset(rom, Variables.OrdenLocal, edicion, compilacion) - 2 + pokemon.OrdenGameFreak * 2, pokemon.OrdenPokedexLocal);

            Word.SetWord(rom, Zona.GetOffset(rom, Variables.OrdenNacional, edicion, compilacion) - 2 + pokemon.OrdenGameFreak * 2, pokemon.OrdenPokedexNacional);

        }
        public static int TotalPokemon(RomPokemon rom)
        {
            return TotalPokemon(rom, Edicion.GetEdicion(rom));
        }
        private static int TotalPokemon(RomPokemon rom, Edicion edicion)
        {
            return TotalPokemon(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        private static int TotalPokemon(RomPokemon rom,  CompilacionRom.Compilacion compilacion)
        {
            return TotalPokemon(rom, Edicion.GetEdicion(rom), compilacion);
        }
        public static int TotalPokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            int total = 440;
            try
            {
                while (false)//en un futuro optimizarlo un poco mas
                {
                    Sprite.GetSprite(rom, edicion, compilacion, total);
                    total++;
                }
            }
            catch { }
            return total;
        }
        public static Pokemon GetPokemon(RomPokemon rom, Hex ordenGameFreak)
        {
            return GetPokemon(rom, Edicion.GetEdicion(rom), ordenGameFreak);
        }
        public static Pokemon GetPokemon(RomPokemon rom, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            return GetPokemon(rom, Edicion.GetEdicion(rom), compilacion, ordenGameFreak);
        }
        public static Pokemon GetPokemon(RomPokemon rom, Edicion edicion, Hex ordenGameFreak)
        {
            return GetPokemon(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), ordenGameFreak);
        }
        public static Pokemon GetPokemon(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex ordenGameFreak)
        {
            Pokemon pokemon = new Pokemon();
            pokemon.OrdenGameFreak = ordenGameFreak;
            pokemon.Sprites=Sprite.GetSprite(rom,edicion,compilacion,ordenGameFreak);
            pokemon.Stats=  BloqueBytes.GetBytes(rom, Zona.GetOffset(rom, Variables.Stats, edicion, compilacion) + pokemon.OrdenGameFreak * (int)LongitudCampos.TotalStats, (int)LongitudCampos.TotalStats).Bytes;
            pokemon.GetObjetosDeLosStats();
            pokemon.Nombre= BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.Nombre, edicion, compilacion) + pokemon.OrdenGameFreak * (int)LongitudCampos.NombreCompilado, (int)LongitudCampos.NombreCompilado);
            pokemon.Nombre.MaxCaracteres = (int)LongitudCampos.Nombre;
         //   pokemon.Descripcion= DescripcionPokedex.GetDescripcionPokedex(rom, edicion, compilacion, pokemon.OrdenGameFreak);
            //pongo el orden local y nacional...
            pokemon.OrdenPokedexLocal=  Word.GetWord(rom, Zona.GetOffset(rom, Variables.OrdenLocal, edicion, compilacion) - 2 + pokemon.OrdenGameFreak * 2);
            pokemon.OrdenPokedexNacional=     Word.GetWord(rom, Zona.GetOffset(rom, Variables.OrdenNacional, edicion, compilacion) - 2 + pokemon.OrdenGameFreak * 2);
            return pokemon;
        }
        public static Pokemon[] GetPokemons(RomPokemon rom)
        {
            return GetPokemons(rom, Edicion.GetEdicion(rom));
        }
        public static Pokemon[] GetPokemons(RomPokemon rom, CompilacionRom.Compilacion compilacion)
        {
            return GetPokemons(rom, Edicion.GetEdicion(rom), compilacion);
        }
        public static Pokemon[] GetPokemons(RomPokemon rom, Edicion edicion)
        {
            return GetPokemons(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        public static Pokemon[] GetPokemons(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Pokemon[] pokemons = new Pokemon[TotalPokemon(rom, edicion, compilacion)];
            for (int i = 0; i < pokemons.Length; i++)
                pokemons[i] = GetPokemon(rom, edicion, compilacion, i);
            return pokemons;


        }

    

        //falta ponerlo donde toca



    }
}
