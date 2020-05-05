using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class EquipoPokemonEntrenador 
    {
        enum Posicion
        {
            Ivs = 0,
            //posible byte en blanco
            Nivel = 1,
            //byte en blanco
            Especie = 4,
            Item = 6,

            Move1 = 8,
            Move2 = 10,
            Move3 = 12,
            Move4 = 14
        }
        enum Longitud
        {
            Nivel = 2,
            Item = 2,
            Ataque = 2,
            PokemonIndex = 2,
        }
        public const int MAXPOKEMONENTRENADOR = 6;

        public EquipoPokemonEntrenador()
        {
            Equipo = new Llista<PokemonEntrenador>();
        }
        public int OffsetToDataPokemon
        {
            get;
            set;
        }

        public Llista<PokemonEntrenador> Equipo { get; set; }

        public int Total => Equipo.Count;
        public int NumeroPokemon
        {
            get
            {
                int num = 0;
                for (int i = 0; i < Equipo.Count; i++)
                    if (Equipo[i] != null)
                        num++;
                return num;
            }
        }

        public bool HayAtaquesCustom()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayAtaquesCustom = false;
            for (int i = 0; i < Equipo.Count && !hayAtaquesCustom; i++)
                if (Equipo[i] != null)
                    hayAtaquesCustom = Equipo[i].Move1 != NOASIGNADO || Equipo[i].Move2 != NOASIGNADO || Equipo[i].Move3 != NOASIGNADO || Equipo[i].Move4 != NOASIGNADO;
            return hayAtaquesCustom;
        }
        public bool HayObjetosEquipados()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayObjetosEquipados = false;
            for (int i = 0; i < Equipo.Count && !hayObjetosEquipados; i++)
                if (Equipo[i] != null)
                    hayObjetosEquipados = Equipo[i].Item != NOASIGNADO;
            return hayObjetosEquipados;
        }

        public static EquipoPokemonEntrenador[] Get(RomGba rom, OffsetRom offsetEntrenador = default, int totalEntrenadores=-1)
        {
            EquipoPokemonEntrenador[] equiposPokemonEntrenador = new EquipoPokemonEntrenador[totalEntrenadores<0?Entrenador.GetTotal(rom):totalEntrenadores];
            offsetEntrenador = Equals(offsetEntrenador, default) ? Entrenador.GetOffset(rom) : offsetEntrenador;
            for (int i = 0; i < equiposPokemonEntrenador.Length; i++)
                equiposPokemonEntrenador[i] = Get(rom, i,offsetEntrenador);
            return equiposPokemonEntrenador;
        }
        public static EquipoPokemonEntrenador Get(RomGba rom, int indexEntrenador, OffsetRom offsetEntrenador=default) =>Get(rom, Entrenador.GetBytes(rom, indexEntrenador,offsetEntrenador));

        public static EquipoPokemonEntrenador Get(RomGba rom, BloqueBytes bloqueEntrenador, int indexEntrenador = -1)
        {
            byte[] bytesPokemonEquipo;
            ushort idEntrenador = (ushort)(indexEntrenador * 10);
            EquipoPokemonEntrenador equipoCargado = new EquipoPokemonEntrenador();
            bool hayItems = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] & 0x2) != 0;
            bool hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
            int tamañoPokemon = hayAtaquesCustom ? 16 : 8;
            BloqueBytes bloqueDatosEquipo = BloqueBytes.GetBytes(rom.Data, new OffsetRom(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData).Offset, bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon);
            equipoCargado.OffsetToDataPokemon = bloqueDatosEquipo.OffsetInicio;


            for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
            {
                bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
                equipoCargado.Equipo.Add(new PokemonEntrenador());
                equipoCargado.Equipo[i].Especie = new Word(bytesPokemonEquipo, (int)Posicion.Especie);//por mirar 
                equipoCargado.Equipo[i].Nivel = new Word(bytesPokemonEquipo, (int)Posicion.Nivel);
                equipoCargado.Equipo[i].Ivs = bytesPokemonEquipo[(int)Posicion.Ivs];
                if (hayItems)
                    equipoCargado.Equipo[i].Item = new Word(bytesPokemonEquipo, (int)Posicion.Item); //por mirar...
                if (hayAtaquesCustom)
                {
                    equipoCargado.Equipo[i].Move1 = new Word(bytesPokemonEquipo, (int)Posicion.Move1);
                    equipoCargado.Equipo[i].Move2 = new Word(bytesPokemonEquipo, (int)Posicion.Move2);
                    equipoCargado.Equipo[i].Move3 = new Word(bytesPokemonEquipo, (int)Posicion.Move3);
                    equipoCargado.Equipo[i].Move4 = new Word(bytesPokemonEquipo, (int)Posicion.Move4);
                }

                }


            return equipoCargado;
        }



    }
}