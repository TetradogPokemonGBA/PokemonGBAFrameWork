using Gabriel.Cat.S.Binaris;
using PokemonGBAFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Descripcion
    {
        public const byte ID = 0x17;


        public static readonly Zona ZonaDescripcion;


        public BloqueString Texto { get; set; }

        static Descripcion()
        {
            ZonaDescripcion = new Zona("DescripciónAtaque");
            //descripcion con ellas calculo el total :D
            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoUsa10, 0xE5440, 0xE5454);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaUsa10, 0xE5418, 0xE542C);
            ZonaDescripcion.Add(EdicionPokemon.RubiUsa10, 0xA0494, 0xA04B4);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroUsa10, 0xA0494, 0xA04B4);
            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaUsa10, 0x1C3EFC);
            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaEsp10, 0x1C3B1C);
            ZonaDescripcion.Add(EdicionPokemon.RubiEsp10, 0xA06C8);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroEsp10, 0xA06C8);
            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoEsp10, 0xE574C);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaEsp10, 0XE5724);

        }
        public static int GetTotal(RomGba rom)
        {
            int offsetDescripciones = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset;
            int total = 1;//el primero no tiene
            while (new OffsetRom(rom, offsetDescripciones).IsAPointer)
            {
                offsetDescripciones += OffsetRom.LENGTH;//avanzo hasta la proxima descripcion :)
                total++;
            }
            return total;
        }
        public static PokemonGBAFramework.Pokemon.Ataque.DescripcionAtaque GetDescripcion(RomGba rom,int posicion)
        {
            Descripcion descripcion;
            int offsetDescripcion;
            if (posicion != 0)//el primero no tiene
            {
                offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + (posicion - 1)*OffsetRom.LENGTH).Offset;
                descripcion = new Descripcion();
                descripcion.Texto = BloqueString.GetString(rom, offsetDescripcion);
        
            }
            else descripcion = new Descripcion() { Texto = new BloqueString("") };

            return new PokemonGBAFramework.Pokemon.Ataque.DescripcionAtaque() { Descripcion = descripcion.Texto.Texto };
        }

        public static Paquete GetDescripcion(RomGba rom)
        {
            return Poke.Extension.GetPaquete(rom, "Descripcion Ataque", (r, i) => GetDescripcion(r, i), GetTotal(rom));
        }
      
    }
}
