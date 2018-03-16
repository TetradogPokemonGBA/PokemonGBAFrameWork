﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
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

        PokemonEntrenador[] equipoPokemon;
        public EquipoPokemonEntrenador()
        {
            equipoPokemon = new PokemonEntrenador[MAXPOKEMONENTRENADOR];
        }
        public int OffsetToDataPokemon
        {
            get;
            set;
        }

        public PokemonEntrenador[] Equipo
        {
            get
            {
                return equipoPokemon;
            }
            set
            {

                if (value == null || value.Length == 0)
                {
                    value = new PokemonEntrenador[MAXPOKEMONENTRENADOR];
                    value[0] = new PokemonEntrenador();
                }
                else if (value.Length > MAXPOKEMONENTRENADOR)
                    value = value.SubList(0, MAXPOKEMONENTRENADOR).ToArray();
                else
                    equipoPokemon.SetIList(value);
                equipoPokemon = value;
            }
        }

        public int Total
        {
            get { return equipoPokemon.Length; }
        }
        public int NumeroPokemon
        {
            get
            {
                int num = 0;
                for (int i = 0; i < equipoPokemon.Length; i++)
                    if (equipoPokemon[i] != null)
                        num++;
                return num;
            }
        }
        public bool HayAtaquesCustom()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayAtaquesCustom = false;
            for (int i = 0; i < equipoPokemon.Length && !hayAtaquesCustom; i++)
                if (equipoPokemon[i] != null)
                    hayAtaquesCustom = equipoPokemon[i].Move1 != NOASIGNADO || equipoPokemon[i].Move2 != NOASIGNADO || equipoPokemon[i].Move3 != NOASIGNADO || equipoPokemon[i].Move4 != NOASIGNADO;
            return hayAtaquesCustom;
        }
        public bool HayObjetosEquipados()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayObjetosEquipados = false;
            for (int i = 0; i < equipoPokemon.Length && !hayObjetosEquipados; i++)
                if (equipoPokemon[i] != null)
                    hayObjetosEquipados = equipoPokemon[i].Item != NOASIGNADO;
            return hayObjetosEquipados;
        }


        public static EquipoPokemonEntrenador GetEquipo(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int indexEntrenador)
        {
            return GetEquipo(rom, Entrenador.GetBytesEntrenador(rom, edicion, compilacion, indexEntrenador));
        }
        public static EquipoPokemonEntrenador GetEquipo(RomGba rom, BloqueBytes bloqueEntrenador)
        {
            if (rom == null || bloqueEntrenador == null)
                throw new ArgumentNullException();

            byte[] bytesPokemonEquipo;
            EquipoPokemonEntrenador equipoCargado = new EquipoPokemonEntrenador();
            bool hayItems = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] & 0x2) != 0;
            bool hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
            int tamañoPokemon = hayAtaquesCustom ? 16 : 8;
            BloqueBytes bloqueDatosEquipo = BloqueBytes.GetBytes(rom.Data, new OffsetRom(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData).Offset, bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon);
            equipoCargado.OffsetToDataPokemon = bloqueDatosEquipo.OffsetInicio;

            for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
            {
                bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
                equipoCargado.Equipo[i] = new PokemonEntrenador();
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
        public static void SetEquipo(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int indexEntrenador, EquipoPokemonEntrenador equipo)
        {
            SetEquipo(rom, Entrenador.GetBytesEntrenador(rom, edicion, compilacion, indexEntrenador), equipo);
        }
        public static void SetEquipo(RomGba rom, BloqueBytes bloqueEntrenador, EquipoPokemonEntrenador equipo)
        {
            if (rom == null || bloqueEntrenador == null || equipo == null)
                throw new ArgumentNullException();
            if (equipo.NumeroPokemon == 0)
                throw new ArgumentException("Se necesita como minimo poner un pokemon en el equipo del entrenador!");

            bool habiaAtaquesCustom;
            bool hayAtaquesCustom;
            BloqueBytes bloqueEquipo;
            BloqueBytes bloquePokemon;
            int tamañoEquipoPokemon;
            int tamañoPokemon;
            int offsetEquipoOffset = new OffsetRom(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData).Offset;
            habiaAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
            tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
            tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
            //borro los datos antiguos de los pokemon :D
            rom.Data.Remove(offsetEquipoOffset, tamañoEquipoPokemon);
            //el numero de pokemon
            bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] = (byte)equipo.NumeroPokemon;
            //me falta saber como va eso de las operaciones AND, XOR,OR y sus negaciones
            //hasHeldItem
            bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] = (byte)(equipo.HayObjetosEquipados() ? 0x2 : 0x0);

            //hasCustmoMoves
            if (equipo.HayAtaquesCustom())
                bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves]++;
            //es calculado aqui :D
            rom.Data.SetArray(bloqueEntrenador.Bytes, bloqueEntrenador.OffsetInicio);//guardo los cambios
            hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
            tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
            tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
            bloqueEquipo = new BloqueBytes(tamañoEquipoPokemon);
            //pongo los pokemon en el bloque :D
            for (int i = 0, index = 0; i < equipo.Equipo.Length; i++)
            {
                if (equipo.equipoPokemon[i] != null)//como pueden ser null pues
                {
                    bloquePokemon = new BloqueBytes(tamañoPokemon);
                    //pongo los datos
                    Word.SetWord(bloqueEntrenador, (int)Posicion.Especie, equipo.Equipo[i].Especie);
                    Word.SetWord(bloqueEntrenador, (int)Posicion.Nivel, equipo.Equipo[i].Nivel);
                    Word.SetWord(bloqueEntrenador, (int)Posicion.Item, equipo.Equipo[i].Item);//por mirar

                    bloquePokemon.Bytes[(int)Posicion.Ivs] = equipo.Equipo[i].Ivs;
                    if (hayAtaquesCustom)
                    {
                        Word.SetWord(bloqueEntrenador, (int)Posicion.Move1, equipo.Equipo[i].Move1);
                        Word.SetWord(bloqueEntrenador, (int)Posicion.Move2, equipo.Equipo[i].Move2);
                        Word.SetWord(bloqueEntrenador, (int)Posicion.Move3, equipo.Equipo[i].Move3);
                        Word.SetWord(bloqueEntrenador, (int)Posicion.Move4, equipo.Equipo[i].Move4);
                    }
                    bloqueEquipo.Bytes.SetArray(index * tamañoPokemon, bloquePokemon.Bytes);
                    index++;
                }
            }

            OffsetRom.SetOffset(rom, new OffsetRom(bloqueEntrenador, bloqueEntrenador.Bytes.Length - OffsetRom.LENGTH), rom.Data.SetArray(bloqueEquipo.Bytes));//actualizo el offset de los datos :D
        }

    }
}
