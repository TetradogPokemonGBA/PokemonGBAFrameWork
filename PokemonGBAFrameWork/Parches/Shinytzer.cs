using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
   public class Shinytzer
    {
        public static readonly byte[] RutinaShinytzer = {/*creditos a HackMew por la rutina :)*/
                                                           0x20, 0x0E, 0x03, 0x28, 0x03, 0xD1, 0x20, 0x48, 0x00, 0x78, 0x00, 0x28,
                                                           0x00, 0xD1, 0x70, 0x47, 0x3C, 0xB5, 0x43, 0x1E, 0x1C, 0x48, 0x03, 0x70,
                                                           0x44, 0x78, 0x00, 0x2C, 0x26, 0xD1, 0x0C, 0x1C, 0x1A, 0x4A, 0x00, 0xF0,
                                                           0x2A, 0xF8, 0x07, 0x23, 0x18, 0x40, 0x03, 0x1C, 0x17, 0x4A, 0x00, 0xF0,
                                                           0x24, 0xF8, 0x05, 0x04, 0x05, 0x43, 0x5D, 0x40, 0x65, 0x40, 0x70, 0xB4,
                                                           0x29, 0x0C, 0x28, 0x04, 0xDB, 0x43, 0x1B, 0x0C, 0x0D, 0x4C, 0x0E, 0x4D,
                                                           0x06, 0x1C, 0x66, 0x43, 0x76, 0x19, 0x32, 0x0C, 0x8A, 0x42, 0x04, 0xD0,
                                                           0x01, 0x30, 0x01, 0x3B, 0x00, 0x2B, 0xF5, 0xD1, 0x04, 0xE0, 0x09, 0x4A,
                                                           0x16, 0x60, 0x62, 0xBC, 0x3D, 0x60, 0x3C, 0xBD, 0x70, 0xBC, 0xD9, 0xE7,
                                                           0x01, 0x25, 0x9D, 0x40, 0x2C, 0x40, 0x00, 0x2C, 0x00, 0xD0, 0x39, 0x68,
                                                           0xF5, 0xE7, 0x10, 0x47, 0x6D, 0x4E, 0xC6, 0x41, 0x73, 0x60, 0x00, 0x00,
                                                           0x18, 0x48, 0x00, 0x03, 0xCA, 0xE8, 0x02, 0x02, 0x85, 0x0E, 0x04, 0x08
                                                        };

        public static string VariableShinytzer = "0x8003";

        public static bool EstaActivado(RomGBA rom)
        {
            return PosicionRutinaShinytzer(rom) > 0;
        }

        public static Hex Activar(RomGBA rom)
        {
            Hex posicion = PosicionRutinaShinytzer(rom);
            if(posicion<0)
            {
                posicion = BloqueBytes.SetBytes(rom, RutinaShinytzer);
            }
            return posicion;

        }
        public static void Desactivar(RomGBA rom)
        {
            Hex posicion = PosicionRutinaShinytzer(rom);
            if (posicion > 0)
            {
                BloqueBytes.RemoveBytes(rom, posicion, RutinaShinytzer.Length);
            }
        }
        /// <summary>
        /// Permite saber donde esta la rutina insertada
        /// </summary>
        /// <param name="rom"></param>
        /// <returns>devuelve -1 si no lo ha encontrado</returns>
       public  static Hex PosicionRutinaShinytzer(RomGBA rom)
        {
            if (rom == null)
                throw new ArgumentNullException();
            return rom.Datos.BuscarArray(RutinaShinytzer);
        }
        public static string ScriptLineaPokemonShiny(short numPokemon)
        {
            if (numPokemon<0)
                throw new ArgumentOutOfRangeException();

            return "setvar " + VariableShinytzer + ((Hex)numPokemon).ByteString;
        }
        /// <summary>
        /// Crear fácilmente el script shinytzer para que el entrenador al ser llamado tenga esos pokemon
        /// </summary>
        /// <param name="entrenador"></param>
        /// <param name="pokemon">primer,segundo,tercero,cuarto...</param>
        /// <returns></returns>
        public static string ScriptLineaPokemonShinyEntrenador(Entrenador entrenador,params bool[] pokemon)
        {
            if (entrenador == null)
                throw new ArgumentNullException();

            const int MAXPOKEMONENTRENADOR = 6,BITSBYTE=8;

            Hex numPokemonShiny;
            bool[] pokemonFinal;

            if (pokemon.Length < MAXPOKEMONENTRENADOR)
            {
                pokemonFinal = new bool[MAXPOKEMONENTRENADOR];
                for (int i =0; i< pokemon.Length;  i++)
                    pokemonFinal[i] = pokemon[i];
                
            }
            else if (pokemon.Length > MAXPOKEMONENTRENADOR)
                 pokemonFinal = pokemon.SubArray(0, MAXPOKEMONENTRENADOR);
            else pokemonFinal = pokemon;

            pokemon = pokemonFinal;
            pokemonFinal = new bool[BITSBYTE];
            for (int i = 2,j=0; j < pokemon.Length; i++,j++)
                pokemonFinal[i] = pokemon[j];

            numPokemonShiny = pokemonFinal.ToByte();
     
            return "setvar " + VariableShinytzer + " 0x" + numPokemonShiny.ToString().PadLeft(2, '0') + entrenador.Pokemon.NumeroPokemon.ToString().PadLeft(2,'0');
        }
    }
}
