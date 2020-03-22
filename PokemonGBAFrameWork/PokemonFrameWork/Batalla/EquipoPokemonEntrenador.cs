using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Poke;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public const byte ID = 0x7;
        public const int MAXPOKEMONENTRENADOR = 6;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<EquipoPokemonEntrenador>();
        Llista<PokemonEntrenador> equipoPokemon;
        public EquipoPokemonEntrenador()
        {
            equipoPokemon =new Llista<PokemonEntrenador>();
        }
        public int OffsetToDataPokemon
        {
            get;
            set;
        }

        public Llista<PokemonEntrenador> Equipo
        {
            get
            {
                return equipoPokemon;
            }

        }

        public int Total
        {
            get { return equipoPokemon.Count; }
        }
        public int NumeroPokemon
        {
            get
            {
                int num = 0;
                for (int i = 0; i < equipoPokemon.Count; i++)
                    if (equipoPokemon[i] != null)
                        num++;
                return num;
            }
        }


        public bool HayAtaquesCustom()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayAtaquesCustom = false;
            for (int i = 0; i < equipoPokemon.Count && !hayAtaquesCustom; i++)
                if (equipoPokemon[i] != null)
                    hayAtaquesCustom = equipoPokemon[i].Move1 != NOASIGNADO || equipoPokemon[i].Move2 != NOASIGNADO || equipoPokemon[i].Move3 != NOASIGNADO || equipoPokemon[i].Move4 != NOASIGNADO;
            return hayAtaquesCustom;
        }
        public bool HayObjetosEquipados()
        {
            const ushort NOASIGNADO = 0x0;
            bool hayObjetosEquipados = false;
            for (int i = 0; i < equipoPokemon.Count && !hayObjetosEquipados; i++)
                if (equipoPokemon[i] != null)
                    hayObjetosEquipados = equipoPokemon[i].Item != NOASIGNADO;
            return hayObjetosEquipados;
        }

        public static PokemonGBAFramework.Paquete GetEquipo(RomGba rom)
        {
            return rom.GetPaquete("Equipos Entrenadores",(r,i)=>GetEquipo(r,i),Entrenador.GetTotal(rom));
        }
        public static PokemonGBAFramework.Batalla.EquipoPokemonEntrenador GetEquipo(RomGba rom, int indexEntrenador)
        {

            EquipoPokemonEntrenador equipo= GetEquipo(rom, Entrenador.GetBytesEntrenador(rom, indexEntrenador));

            return new PokemonGBAFramework.Batalla.EquipoPokemonEntrenador() { Equipo=equipo.Equipo.Select((e)=> {
                PokemonGBAFramework.Batalla.PokemonEntrenador pokemon = new PokemonGBAFramework.Batalla.PokemonEntrenador();
                e.SetValues(pokemon);
                return pokemon;

               }).ToList()};
        }
        public static EquipoPokemonEntrenador GetEquipo(RomGba rom, BloqueBytes bloqueEntrenador,int indexEntrenador=-1)
        {
            if (rom == null || bloqueEntrenador == null)
                throw new ArgumentNullException();

            ushort idEntrenador =(ushort) (indexEntrenador * 10);
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
