using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//información obtenida de https://github.com/Jambo51/Trainer_Editor
namespace PokemonGBAFrameWork
{
    public class Entrenador//ocupa 32bytes
    {
        public class Equipo//5A??max
        {
            public class Pokemon//ocupa 8 o 16 bytes dependiendo de si tiene o no movimientos custom el entrenador activado
            {
                ushort pokemonIndex;//cuarto byte
                byte ivs;//primer byte
                ushort level;//segundo byte no se porque son dos bytes...tendria que ser 1...
                ushort item;//sexto byte
                //a partir del byte 8//puede que los movimientos no esten cambiados por lo tanto no estarian...
                ushort move1;
                ushort move2;
                ushort move3;
                ushort move4;

                public ushort PokemonIndex
                {
                    get
                    {
                        return pokemonIndex;
                    }

                    set
                    {
                        pokemonIndex = value;
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

                public ushort Nivel
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

                public ushort Item
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

                public ushort Move1
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

                public ushort Move2
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

                public ushort Move3
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

                public ushort Move4
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
                //falta los evs....o va con los ivs...quizas hace falta poner en la rom una rutina...
            }
            bool movimientosPokemonCustom;//si es true ocuparan más los pokemon de 8 a 16
            bool heldItems;//si es true llevan objeto los pokemon
            Hex offsetToDataPokemon;
            //uint numeroDePokemons;
            Pokemon[] pokemonEquipo;//inicializarla a 6 para que sea el maximo :D luego el total será contar !=null

            public bool MovimientosPokemonCustom
            {
                get
                {
                    return movimientosPokemonCustom;
                }

                set
                {
                    movimientosPokemonCustom = value;
                }
            }

            public bool HeldItems
            {
                get
                {
                    return heldItems;
                }

                set
                {
                    heldItems = value;
                }
            }

            public Hex OffsetToDataPokemon
            {
                get
                {
                    return offsetToDataPokemon;//se guarda como pointer pero se usa como offset :D
                }

                set
                {
                    if (value < 0||value>(int)Longitud.TreintaYDosMegas) throw new ArgumentOutOfRangeException();
                    offsetToDataPokemon = value;
                }
            }

            public Pokemon[] PokemonEquipo
            {
                get
                {
                    return pokemonEquipo;
                }
            }
            public int NumeroPokemon
            {
                get
                {
                    int num = 0;
                    for (int i = 0; i < pokemonEquipo.Length; i++)
                        if (pokemonEquipo[i] != null)
                            num++;
                    return num;
                }
            }
            public bool HayAtaquesCustom()
            {
                bool hayAtaquesCustom = false;
                for (int i = 0; i < pokemonEquipo.Length && !hayAtaquesCustom; i++)
                    if (pokemonEquipo[i] != null)
                        hayAtaquesCustom = pokemonEquipo[i].Move1 != 0x0 || pokemonEquipo[i].Move2 != 0x0 || pokemonEquipo[i].Move3 != 0x0 || pokemonEquipo[i].Move4 != 0x0;
                return hayAtaquesCustom;
            }
        }

        byte moneyClass;
        bool esUnaEntrenadora;
        byte musicaBatalla;
        byte spriteIndex;
        BloqueString nombre;//max 10
        //faltan 2 bytes [14,15]
        ushort item1;
        ushort item2;
        ushort item3;
        ushort item4;
        //faltan 4 bytes
        uint inteligencia;
        Equipo equipo;//mirar si como minimo se tiene que tener un pokemon!

        public byte MoneyClass
        {
            get
            {
                return moneyClass;
            }

            set
            {
                moneyClass = value;
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

        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }

           private set
            {
                nombre = value;
            }
        }

        public ushort Item1
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

        public ushort Item2
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

        public ushort Item3
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

        public ushort Item4
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

        public uint Inteligencia
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

        public Equipo Pokemon
        {
            get
            {
                return equipo;
            }

           private set
            {
                equipo = value;
            }
        }

        public uint CalcularDinero(RomGBA rom)
        {
            uint tamañoPokemonBytes = 8;
            if (Pokemon.MovimientosPokemonCustom)
            {
                tamañoPokemonBytes = 16;
            }
            return (MoneyClass * (uint)(rom.Datos[((uint)Pokemon.NumeroPokemon * tamañoPokemonBytes + Pokemon.OffsetToDataPokemon - tamañoPokemonBytes + 2)] << 2));
        }
    }
    public class SpriteEntrenador
    {
        //PointerImagen sprite tablaSprite +spriteIndex << 3
        //PointerPaleta sprite tablaPaleta +spriteIndex << 3
    }
    /*
     total 36 bytes
pos->rom[byte]&0x1!=0 si es true hay movesets y cambia de 8 a 16  movesetIndex
pos->rom[byte]&0x2!=0 si es true "heldItems"
pos+1->clase(MoneyClass) entrenador byte
pos+2->rom[byte]&0x80!=0 pregunta si es chica
pos+2->rom[byte]&0x7F  musica
pos+3->entrenador sprite byte
pos+4->NombreEntrenador->10bytes
//faltan 2bytes pos+14,pos+15
pos+16->item1 2bytes
pos+18->item2 2bytes
pos+20->item3 2bytes
pos+22->item4 2bytes
//faltan 4bytes 
pos+28->AI entrenador 4 bytes
pos+32->Numero de pokemons (uint...) 4 bytes
pos+36->Pointer to pokemon data 4bytes
         */

    /*
     class money se tiene que buscar....
primero se lee un byte de la rom en la posicion de la classMoney luego mientras ser diferente a 0xFF mirara si coincide con el entrenador si coincide la ha encontrado y el dinero se saca de la classMoney+index+1 el index empieza con 0 y cada vez que no encuentra el index suma 4 y el byte cambia classLocation+index


trainerCash ->1byte->rom[classMoney+index+1]

si no lo encuentra el trainerCash es 0

     */
    /*
     NumeroPokemonEntrenador*moveSetIndex=datalength

cada pokemon se saca de datalocation+4+(iPokemonQueVa*moveSetIndex) para sacar ushort para saber que pokemon es (de la lista cargada...osea la posicion del pokemon, falta saber si es el orden de la gameFreak u otro...)

para saber el nivel se mira con dataLocation+2+(iPokemonQueVa*moveSetIndex)

        */


}
