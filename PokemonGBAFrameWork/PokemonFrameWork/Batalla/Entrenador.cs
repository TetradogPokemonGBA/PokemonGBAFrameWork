using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Entrenador:PokemonFrameWorkItem 
    {
        public enum Posicion
        {
            HasCustomMoves = 0,
            HasHeldITem = 0,
            MoneyClass = 1,
            EsChica = 2,
            Musica = 2,
            Sprite = 3,
            Nombre = 4,
            //faltan [14,15]
            Item1 = 16,
            Item2 = 18,
            Item3 = 20,
            Item4 = 22,
            //faltan [23,27]
            Inteligencia = 28,
            NumeroPokemons = 32,
            PointerPokemonData = 36

        }
        enum Longitud
        {
            Nombre = 10,
            Inteligencia = 4,
            Item = 2,
        }
        public const byte ID = 0x6;
        public const byte MAXMUSIC = 0x7F;
        public const byte LENGTH = 0x28;
        public static readonly Zona ZonaEntrenador;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Entrenador>();
        byte trainerClass;
        bool esUnaEntrenadora;
        byte musicaBatalla;
        byte spriteIndex;
        BloqueString nombre;//max 10
                            //faltan 2 bytes [14,15]
        Word item1;
        Word item2;
        Word item3;
        Word item4;
        //faltan 4 bytes
        DWord inteligencia;

        EquipoPokemonEntrenador equipo;
        static Entrenador()
        {
            ZonaEntrenador = new Zona("Entrenador");
            //añado las zonas :D
            ZonaEntrenador.Add(0x3587C, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
            ZonaEntrenador.Add(0xD890, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaEntrenador.Add(0xDA5C, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            ZonaEntrenador.Add(EdicionPokemon.VerdeHojaUsa10, 0xFC00, 0xFC14);
            ZonaEntrenador.Add(EdicionPokemon.RojoFuegoUsa10, 0xFC00, 0xFC14);
            ZonaEntrenador.Add(0xFB70, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
        }
        public Entrenador()
        {
            nombre = new BloqueString((int)Longitud.Nombre);
        }

        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }
        }
        public EquipoPokemonEntrenador EquipoPokemon
        {
            get
            {
                return equipo;
            }
            set
            {
                equipo = value;
            }
        }

        public byte TrainerClass
        {
            get
            {
                return trainerClass;
            }
            set
            {
                trainerClass = value;
            }
        }

        public bool EsUnaEntrenadora
        {
            get
            {
                return esUnaEntrenadora;
            }
            set
            {
                esUnaEntrenadora = value;
            }
        }

        public byte MusicaBatalla
        {
            get
            {
                return musicaBatalla;
            }
            set
            {
                musicaBatalla = value;
            }
        }

        public byte SpriteIndex
        {
            get
            {
                return spriteIndex;
            }
            set
            {
                spriteIndex = value;
            }
        }

        public Word Item1
        {
            get
            {
                return item1;
            }
            set
            {
                item1 = value;
            }
        }

        public Word Item2
        {
            get
            {
                return item2;
            }
            set
            {
                item2 = value;
            }
        }

        public Word Item3
        {
            get
            {
                return item3;
            }
            set
            {
                item3 = value;
            }
        }

        public Word Item4
        {
            get
            {
                return item4;
            }
            set
            {
                item4 = value;
            }
        }

        public DWord Inteligencia
        {
            get
            {
                return inteligencia;
            }
            set
            {
                inteligencia = value;
            }
        }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public uint CalcularDinero(RomGba rom)
        {
            uint tamañoPokemonBytes = 8;
            if (EquipoPokemon.HayAtaquesCustom())
            {
                tamañoPokemonBytes = 16;
            }
            return (TrainerClass * (uint)(rom.Data.Bytes[((uint)EquipoPokemon.NumeroPokemon * tamañoPokemonBytes + EquipoPokemon.OffsetToDataPokemon - tamañoPokemonBytes + 2)] << 2));
        }
        public override string ToString()
        {
            return Nombre;
        }

        public static int GetTotal(RomGba rom)
        {
            const byte POSICIONPOINTERDATOS = 0x24;

            ushort num = 1;
            int posicionEntrenadores = Zona.GetOffsetRom(ZonaEntrenador,rom).Offset;
            int posicionActual = posicionEntrenadores + POSICIONPOINTERDATOS + LENGTH;
            while (new OffsetRom(rom, posicionActual).IsAPointer)
            {
                num++;
                posicionActual += LENGTH;
            }
            return num - 1;
        }

        public static Entrenador GetEntrenador(RomGba rom, int index)
        {

            BloqueBytes bytesEntrenador = GetBytesEntrenador(rom, index);
            Entrenador entranadorCargado = new Entrenador();
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;

            //le pongo los datos
            entranadorCargado.EsUnaEntrenadora = (bytesEntrenador.Bytes[(int)Posicion.EsChica] & 0x80) != 0;
            entranadorCargado.MusicaBatalla = (byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & MAXMUSIC);
            entranadorCargado.TrainerClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];//quizas es la clase de entrenador :D y no el rango de dinero que da...
            entranadorCargado.Nombre.Texto = BloqueString.GetString(bytesEntrenador, (int)Posicion.Nombre, (int)Longitud.Nombre);
            entranadorCargado.Inteligencia = new DWord(bytesEntrenador.Bytes, (int)Posicion.Inteligencia);//mirar si es asi :D
            entranadorCargado.Item1 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item1);//mirar si es asi :D
            entranadorCargado.Item2 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item2);//mirar si es asi :D
            entranadorCargado.Item3 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item3);//mirar si es asi :D
            entranadorCargado.Item4 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item4);//mirar si es asi :D
            entranadorCargado.SpriteIndex = bytesEntrenador.Bytes[(int)Posicion.Sprite];
            entranadorCargado.EquipoPokemon = EquipoPokemonEntrenador.GetEquipo(rom, bytesEntrenador);

            if (edicion.EsEsmeralda)
                entranadorCargado.IdFuente = EdicionPokemon.IDESMERALDA;
            else if (edicion.EsRubiOZafiro)
                entranadorCargado.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
            else
                entranadorCargado.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

            entranadorCargado.IdElemento = (ushort)index;

            return entranadorCargado;

        }
        public static BloqueBytes GetBytesEntrenador(RomGba rom, int index)
        {
            const byte TAMAÑOENTRENADOR = 0x28;
            int posicionEntrenadores = Zona.GetOffsetRom(ZonaEntrenador, rom).Offset;
            int poscionEntrenador = posicionEntrenadores + TAMAÑOENTRENADOR * (index + 1);//el primero me lo salto porque no es un entrenador...
            return BloqueBytes.GetBytes(rom.Data, poscionEntrenador, TAMAÑOENTRENADOR);
        }

        public static Entrenador[] GetEntrenador(RomGba rom)
        {
            Entrenador[] entrenadores = new Entrenador[GetTotal(rom)];
            for (int i = 0; i < entrenadores.Length; i++)
                entrenadores[i] = GetEntrenador(rom,  i);


            return entrenadores;
        }

      
    }
}
