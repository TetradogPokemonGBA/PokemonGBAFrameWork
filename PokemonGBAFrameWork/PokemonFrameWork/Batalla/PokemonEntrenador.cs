using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class PokemonEntrenador:PokemonFrameWorkItem
    {
        public const byte ID = 0xC;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PokemonEntrenador>();

        Word especie;//quinto byte
        byte ivs;//primer byte
        Word level;//segundo byte no se porque son dos bytes...tendria que ser 1...
        Word item;//septimo byte
                  //a partir del byte 9//puede que los movimientos no esten cambiados por lo tanto no estarian...
        Word move1;
        Word move2;
        Word move3;
        Word move4;



        public Word Especie
        {
            get
            {
                return especie;
            }

            set
            {
                especie = value;
            }
        }

        public byte Ivs
        {
            get
            {
                return ivs;
            }

            set
            {
                ivs = value;
            }
        }

        public Word Nivel
        {
            get
            {
                return level;
            }

            set
            {
                level = value;
            }
        }

        public Word Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }

        public Word Move1
        {
            get
            {
                return move1;
            }

            set
            {
                move1 = value;
            }
        }

        public Word Move2
        {
            get
            {
                return move2;
            }

            set
            {
                move2 = value;
            }
        }

        public Word Move3
        {
            get
            {
                return move3;
            }

            set
            {
                move3 = value;
            }
        }

        public Word Move4
        {
            get
            {
                return move4;
            }

            set
            {
                move4 = value;
            }
        }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;
    }
}
