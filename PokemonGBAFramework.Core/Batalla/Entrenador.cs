using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Entrenador 
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

        public const byte MAXMUSIC = 0x7F;
        public const byte LENGTH = 0x28;

        public static readonly  byte[] MuestraAlgoritmoHoenn = {0x07, 0xD1, 0x02, 0x48, 0x56, 0xE0};
        public static readonly int IndexRelativoHoenn = 0;
        public static readonly byte[] MuestraAlgoritmoKanto = { 0xDC, 0x00, 0x29, 0x07, 0xD0, 0x27, 0xE0 };
        public static readonly int IndexRelativoKanto = 0;
        public Entrenador()
        {
            Nombre = new BloqueString((int)Longitud.Nombre);
        }
        #region Propiedades
        public BloqueString Nombre { get; set; }
        public EquipoPokemonEntrenador EquipoPokemon { get; set; }

        public byte TrainerClass { get; set; }

        public bool EsUnaEntrenadora { get; set; }

        public byte MusicaBatalla { get; set; }

        public byte SpriteIndex { get; set; }

        public Word Item1 { get; set; }

        public Word Item2 { get; set; }

        public Word Item3 { get; set; }

        public Word Item4 { get; set; }

        public DWord Inteligencia { get; set; }
        #endregion
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

        public static int GetTotal(RomGba rom,OffsetRom offsetEntrenador=default)
        {
            const byte POSICIONPOINTERDATOS = 0x24;

            ushort num = 1;
            int posicionEntrenadores =Equals(offsetEntrenador,default)?GetOffset(rom):offsetEntrenador;
            int posicionActual = posicionEntrenadores + POSICIONPOINTERDATOS + LENGTH;
            while (new OffsetRom(rom, posicionActual).IsAPointer)
            {
                num++;
                posicionActual += LENGTH;
            }
            return num - 1;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            return Zona.Search(rom,rom.Edicion.EsHoenn? MuestraAlgoritmoHoenn:MuestraAlgoritmoKanto,rom.Edicion.EsHoenn? IndexRelativoHoenn:IndexRelativoKanto);
        }

        public static Entrenador Get(RomGba rom, int index,OffsetRom offsetEntrenador=default)
        {

            BloqueBytes bytesEntrenador = GetBytes(rom, index,offsetEntrenador);
            Entrenador entranadorCargado = new Entrenador();

            //le pongo los datos
            entranadorCargado.EsUnaEntrenadora = (bytesEntrenador.Bytes[(int)Posicion.EsChica] & 0x80) != 0;
            entranadorCargado.MusicaBatalla = (byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & MAXMUSIC);
            entranadorCargado.TrainerClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];//quizas es la clase de entrenador :D y no el rango de dinero que da...
            entranadorCargado.Nombre.Texto = BloqueString.Get(bytesEntrenador, (int)Posicion.Nombre, (int)Longitud.Nombre);
            entranadorCargado.Inteligencia = new DWord(bytesEntrenador.Bytes, (int)Posicion.Inteligencia);//mirar si es asi :D
            entranadorCargado.Item1 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item1);//mirar si es asi :D
            entranadorCargado.Item2 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item2);//mirar si es asi :D
            entranadorCargado.Item3 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item3);//mirar si es asi :D
            entranadorCargado.Item4 = new Word(bytesEntrenador.Bytes, (int)Posicion.Item4);//mirar si es asi :D
            entranadorCargado.SpriteIndex = bytesEntrenador.Bytes[(int)Posicion.Sprite];
            entranadorCargado.EquipoPokemon = EquipoPokemonEntrenador.Get(rom, bytesEntrenador);

            return entranadorCargado;

        }
        public static BloqueBytes GetBytes(RomGba rom, int index,OffsetRom offsetEntrenador=default)
        {
            const byte TAMAÑOENTRENADOR = 0x28;
            int posicionEntrenadores = Equals(offsetEntrenador,default)?GetOffset(rom):offsetEntrenador;
            int poscionEntrenador = posicionEntrenadores + TAMAÑOENTRENADOR * (index + 1);//el primero me lo salto porque no es un entrenador...
            return BloqueBytes.GetBytes(rom.Data, poscionEntrenador, TAMAÑOENTRENADOR);
        }

        public static Entrenador[] Get(RomGba rom, OffsetRom offsetEntrenador = default,int totalEntrenadores=-1)
        {
            Entrenador[] entrenadores = new Entrenador[totalEntrenadores<0?GetTotal(rom,offsetEntrenador):totalEntrenadores];
            for (int i = 0; i < entrenadores.Length; i++)
                entrenadores[i] = Get(rom, i,offsetEntrenador);
            return entrenadores;
        }


    }
}