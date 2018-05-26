using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
   public class Stats:IElementoBinarioComplejo
    {
        public enum LongitudCampos
        {
            TotalStats = 28,
        }
        public enum NivelEvs
        {
            Cero,
            Uno,
            Dos,
            Tres
        }
        public enum GrupoHuevo
        { Ninguno, Monstruo, Agua1, Bicho, Volador, Campo, Hada, Hierba, FormaHumana, Agua3, Mineral, Amorfo, Agua2, Ditto, Dragón, Desconocido }
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
        public const byte ID = 0x28;
        const int PASOSCICLOECLOSION = 256;
        const int MAXIMOSPASOSECLOSION = PASOSCICLOECLOSION * PASOSCICLOECLOSION;
        const int LENGTHNIVELEVS = 4;
        /// <summary>
        /// forma parte de un stat junto con la de color en el mismo byte numero 25
        /// </summary>
        const byte FACELEFT = 128;
        public static readonly Zona ZonaStats;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Stats>();


        Word objeto1;
        Word objeto2;

        static Stats()
        {
            ZonaStats = new Zona("Stats Pokemon");
            //stats
            ZonaStats.Add(0x1BC, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);
            ZonaStats.Add(0x10B64, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaStats.Add(0x10D30, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

        }
        public Stats()
        {
            Datos = new BloqueBytes((int)LongitudCampos.TotalStats);
        }
        public BloqueBytes Datos { get; private set; }
        #region Interpreta	Stats
        public int TotalStatsBase
        {
            get
            {
                int totalStatsBase = 0;
                for (int i = 0; i < 6; i++)
                    totalStatsBase += Datos.Bytes[i];
                return totalStatsBase;

            }
        }


        public byte Hp
        {
            get { return Datos.Bytes[0]; }
            set
            {
                Datos.Bytes[0] = value;

            }
        }
        public byte Ataque
        {
            get { return Datos.Bytes[1]; }
            set
            {
                Datos.Bytes[1] = value;
            }
        }


        public byte Defensa
        {
            get { return Datos.Bytes[2]; }
            set
            {
                Datos.Bytes[2] = value;
            }
        }
        public byte Velocidad
        {
            get { return Datos.Bytes[3]; }
            set
            {
                Datos.Bytes[3] = value;
            }
        }
        public byte AtaqueEspecial
        {
            get { return Datos.Bytes[4]; }
            set
            {
                Datos.Bytes[4] = value;
            }
        }
        public byte DefensaEspecial
        {
            get { return Datos.Bytes[5]; }
            set
            {
                Datos.Bytes[5] = value;
            }
        }
        public byte Tipo1
        {
            get
            {

                return Datos.Bytes[6];
            }
            set
            {

                Datos.Bytes[6] = value;

            }
        }
        public byte Tipo2
        {
            get
            {
                return Datos.Bytes[7];
            }
            set
            {

                Datos.Bytes[7] = value;

            }
        }
        public byte RatioCaptura
        {
            get { return Datos.Bytes[8]; }
            set
            {
                Datos.Bytes[8] = value;
            }
        }
        public byte ExperienciaBase
        {
            get { return Datos.Bytes[9]; }
            set
            {
                Datos.Bytes[9] = value;
            }
        }

        //SPE,DEF,ATK,HP
        #region EvsStats se tiene que testear :)
        public NivelEvs HpEvs
        {
            get
            {
                int posicion = (Datos.Bytes[10] - ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) - ((int)AtaqueEvs) * LENGTHNIVELEVS) / LENGTHNIVELEVS;//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                Datos.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)value);

            }
        }
        public NivelEvs AtaqueEvs
        {
            get
            {
                int posicion = ((Datos.Bytes[10] - (((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)))) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3))) / LENGTHNIVELEVS;//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                Datos.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)value) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs DefensaEvs
        {
            get
            {
                int posicion = (Datos.Bytes[10] - (((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)))) / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2));//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                Datos.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs VelocidadEvs
        {
            get
            {
                int posicion = Datos.Bytes[10] / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3));//le quito lo anterior
                return (NivelEvs)posicion;
            }
            set
            {
                if (value < NivelEvs.Uno || value > NivelEvs.Tres)
                    throw new ArgumentOutOfRangeException("value");
                Datos.Bytes[11] = (byte)(((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

            }

        }
        public NivelEvs AtaqueEspecialEvs
        {
            get
            {
                int posicion = Datos.Bytes[11] - ((int)DefensaEspecialEvs) * LENGTHNIVELEVS;
                return (NivelEvs)posicion;
            }
            set
            {
                Datos.Bytes[11] = (byte)(((int)DefensaEspecialEvs) * LENGTHNIVELEVS + (int)value);
            }
        }
        public NivelEvs DefensaEspecialEvs
        {
            get
            {
                int posicion = Datos.Bytes[11] / LENGTHNIVELEVS;
                return (NivelEvs)posicion;
            }
            set
            {
                Datos.Bytes[11] = (byte)(((int)value) * LENGTHNIVELEVS + (int)AtaqueEspecialEvs);
            }
        }
        #endregion

        //item1 indexItems([12](%)+[13](/)*256);
        public Word Objeto1
        {
            get
            {
                return objeto1;
            }
            set
            {
                if (value == null)
                    value = new Word(0);
                objeto1 = value;
            }
        }
        //item2 indexItems([14](%)+[15](/)*256);
        public Word Objeto2
        {
            get
            {
                return objeto2;
            }
            set
            {
                if (value == null)
                    value = new Word(0);
                objeto2 = value;
            }
        }

        public RatioGenero RatioSexo
        {
            get { return (RatioGenero)(int)Datos.Bytes[16]; }
            set
            {
                if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
                    throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
                Datos.Bytes[16] = (byte)(int)value;
            }
        }
        /// <summary>
        /// Se usan multiplos de 256 ya que se guarda en un byte
        /// </summary>
        public int PasosParaEclosionarHuevo
        {
            get { return (int)Datos.Bytes[17] * PASOSCICLOECLOSION; }
            set
            {
                if ((int)value > MAXIMOSPASOSECLOSION || (int)value < 0)
                    throw new ArgumentOutOfRangeException("value", "el valor no puede más pequeño que 0 o ser más grande que " + MAXIMOSPASOSECLOSION);
                Datos.Bytes[17] = (byte)(value / PASOSCICLOECLOSION);
            }
        }


        /// <summary>
        /// Se puede poner valores de 0 a FF
        /// </summary>
        public Felicidad BaseAmistad
        {
            get { return (Felicidad)(int)Datos.Bytes[18]; }
            set
            {
                if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
                    throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
                Datos.Bytes[18] = (byte)(int)value;
            }
        }
        public RatioCrecimiento Crecimiento
        {
            get { return (RatioCrecimiento)(int)Datos.Bytes[19]; }//solo se usa la posicion de la enumeracion para determinar su crecimiento
            set
            {
                if (value < RatioCrecimiento.Exp1000000 || value > RatioCrecimiento.Exp1250000)
                    throw new ArgumentOutOfRangeException(String.Format("el valor no puede superar el numero {0} y tiene que ser positivo", Enum.GetNames(typeof(RatioCrecimiento)).Length - 1));
                Datos.Bytes[19] = (byte)(int)value;
            }
        }
        public GrupoHuevo GrupoHuevo1
        {
            get { return (GrupoHuevo)Datos.Bytes[20]; }
            set
            {
                Datos.Bytes[20] = (byte)value;
            }
        }
        public GrupoHuevo GrupoHuevo2
        {
            get { return (GrupoHuevo)Datos.Bytes[21]; }
            set
            {
                Datos.Bytes[21] = (byte)value;
            }
        }
        public byte Habilidad1
        {
            get { return Datos.Bytes[22]; }
            set
            {
                Datos.Bytes[22] = value;
            }
        }



        public byte Habilidad2
        {
            get { return Datos.Bytes[23]; }
            set
            {
                Datos.Bytes[23] = value;
            }
        }
        public byte RatioDeEscaparZonaSafari
        {
            get { return Datos.Bytes[24]; }
            set
            {
                Datos.Bytes[24] = value;
            }
        }
        #region Por mirar InGame
        //Color parece que se usa en la pokedex...
        public Color ColorBaseStat
        {//Hex(128 + Clr1.SelectedIndex) FaceLeft se el suma 128 al stat del color...si no esta es FaceRight
            get { return IsFaceRight ? (Color)(int)Datos.Bytes[25] : (Color)(int)(FACELEFT - Datos.Bytes[25]); }
            set
            {
                if (value < Color.Rojo || value > Color.Rosa)
                    throw new ArgumentOutOfRangeException();
                bool isFaceLeft = !IsFaceRight;
                Datos.Bytes[25] = (byte)(int)value;
                if (isFaceLeft)
                    Datos.Bytes[25] += FACELEFT;

            }
        }
        /// <summary>
        /// Dirección de la imagen en la pantalla de estado
        /// </summary>
        public bool IsFaceRight
        {
            get { return Datos.Bytes[25] < FACELEFT; }
            set
            {
                if (value)
                {
                    if (!IsFaceRight)
                        Datos.Bytes[25] -= FACELEFT;
                }
                else
                {
                    if (IsFaceRight)
                        Datos.Bytes[25] += FACELEFT;
                }
            }
        }
        //PadBase 26,27??que es eso?? no se usa???
        public Word PadBase
        {
            get
            {
                return new Word(Datos, 26);
            }
            set
            {
                if (value == null)
                    value = new Word(0);

                Datos.Bytes[26] = value.Data[0];
                Datos.Bytes[27] = value.Data[1];
            }
        }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;


        #endregion

        #endregion

        public void SetObjetosEnLosStats(int totalObjetos)
        {
            if (totalObjetos < 0 || totalObjetos > ushort.MaxValue)
                throw new ArgumentOutOfRangeException();

            Datos.Bytes[12] = (byte)(Objeto1 % totalObjetos);
            Datos.Bytes[14] = (byte)(Objeto2 % totalObjetos);

            Datos.Bytes[13] = (byte)(Objeto1 / totalObjetos);
            Datos.Bytes[15] = (byte)(Objeto2 / totalObjetos);
        }
        public void GetObjetosDeLosStats()
        {
            Objeto1 = new Word(Datos.Bytes, 12);
            Objeto2 = new Word(Datos.Bytes, 14);
        }
        public NivelEvs GetEvs(StatEvs stat)
        {
            NivelEvs nivel;
            switch (stat)
            {
                case StatEvs.Hp: nivel = HpEvs; break;
                case StatEvs.Ataque: nivel = AtaqueEvs; break;
                case StatEvs.Defensa: nivel = DefensaEspecialEvs; break;
                case StatEvs.Velocidad: nivel = VelocidadEvs; break;
                case StatEvs.AtaqueEspecial: nivel = AtaqueEspecialEvs; break;
                case StatEvs.DefensaEspecial: nivel = DefensaEspecialEvs; break;
                default: throw new ArgumentOutOfRangeException();
            }
            return nivel;
        }
        public void SetEvs(StatEvs stat, NivelEvs nivel)
        {

            switch (stat)
            {
                case StatEvs.Hp: HpEvs = nivel; break;
                case StatEvs.Ataque: AtaqueEvs = nivel; break;
                case StatEvs.Defensa: DefensaEspecialEvs = nivel; break;
                case StatEvs.Velocidad: VelocidadEvs = nivel; break;
                case StatEvs.AtaqueEspecial: AtaqueEspecialEvs = nivel; break;
                case StatEvs.DefensaEspecial: DefensaEspecialEvs = nivel; break;

            }

        }
        public int CalculaHp(int nivel, int evs = 0, int ivs = 0)
        {
            return (((ivs + 2 * Hp + (evs / 4) + 100) * nivel) / 100) + 10;
        }
        public static Stats GetStats(RomGba rom,int posicion)
        {
            Stats stats= new Stats() { Datos = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(ZonaStats, rom).Offset + (posicion * (int)LongitudCampos.TotalStats), (int)LongitudCampos.TotalStats) };
            stats.GetObjetosDeLosStats();
            return stats;
        }
        public static Stats[] GetStats(RomGba rom)
        {
            Stats[] stats = new Stats[Huella.GetTotal(rom)];
            for (int i = 0; i < stats.Length; i++)
                stats[i] = GetStats(rom, i);
            return stats;
        }
        /// <summary>
        /// Poner después del SetObjetos si no se especifica el total
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="posicion"></param>
        /// <param name="stats"></param>
        /// <param name="totalObjetos"></param>
        public static void SetStats(RomGba rom,int posicion,Stats stats,int totalObjetos=-1)
        {
            if (totalObjetos < 0)
                totalObjetos = Objeto.Datos.GetTotal(rom);
            stats.SetObjetosEnLosStats(totalObjetos);
            rom.Data.SetArray(new OffsetRom(Zona.GetOffsetRom(ZonaStats, rom).Offset + posicion * OffsetRom.LENGTH).Offset, stats.Datos.Bytes);
        }
        public static void SetStats(RomGba rom,IList<Stats> stats, int totalObjetos = -1)
        {
            if (totalObjetos < 0)
                totalObjetos = Objeto.Datos.GetTotal(rom);
            rom.Data.Remove(Zona.GetOffsetRom(ZonaStats, rom).Offset, Huella.GetTotal(rom) * (int)LongitudCampos.TotalStats);
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaStats, rom),rom.Data.SearchEmptyBytes(stats.Count* (int)LongitudCampos.TotalStats));
            for (int i = 0; i < stats.Count; i++)
                SetStats(rom, i, stats[i],totalObjetos);
        }

    }
}
