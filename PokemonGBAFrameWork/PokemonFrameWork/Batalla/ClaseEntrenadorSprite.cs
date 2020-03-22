using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Gabriel.Cat.S.Binaris;
using Poke;
using PokemonGBAFramework;
using PokemonGBAFramework.Batalla;
using PokemonGBAFrameWork;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class Sprite 
    {
        public class Data 
        {
            public const byte ID = 0x3;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Data>();
            public static readonly Zona ZonaImgSprite;

            static Data()
            {
                ZonaImgSprite = new Zona("Sprite Entrenador Img");

                //pongo las zonas :D
                //img
                ZonaImgSprite.Add(0x34628, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
                ZonaImgSprite.Add(EdicionPokemon.RojoFuegoUsa10, 0x3473C, 0x34750);
                ZonaImgSprite.Add(EdicionPokemon.VerdeHojaUsa10, 0x3473C, 0x34750);

                ZonaImgSprite.Add(0x31ADC, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
                ZonaImgSprite.Add(0x31CA8, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

                ZonaImgSprite.Add(0x5DF78, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);

            }

            public BloqueImagen Img { get; set; }
     
            public static Data GetData(RomGba rom, int index)
            {
                EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
                int offsetSpriteImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
                Data data= new Data() { Img = BloqueImagen.GetBloqueImagen(rom, offsetSpriteImg) };

                return data;
            }

        }
        public class Paleta 
        {
            public const byte ID = 0x4;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Paleta>();
            public static readonly Zona ZonaPaletaSprite;

            public PokemonGBAFrameWork.Paleta Colores { get; set; }

          
            static Paleta()
            {

                ZonaPaletaSprite = new Zona("Sprite Entrenador Paleta");
                //pongo las zonas :D

                //paletas
                ZonaPaletaSprite.Add(0x34638, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
                ZonaPaletaSprite.Add(EdicionPokemon.RojoFuegoUsa10, 0x3474C, 0x34760);
                ZonaPaletaSprite.Add(EdicionPokemon.VerdeHojaUsa10, 0x3474C, 0x34760);

                ZonaPaletaSprite.Add(0x31AF0, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
                ZonaPaletaSprite.Add(0x31CBC, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

                ZonaPaletaSprite.Add(0x5B784, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);

            }

            public static Paleta GetPaleta(RomGba rom, int index)
            {

                int offsetSpritePaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset + index * PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO;
                Paleta paleta = Paleta.GetPaleta(rom, offsetSpritePaleta);

  
                return paleta;
            }
         

        }

        public const byte ID = 0x5;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Sprite>();
        public Data DataImg { get; set; }
        public Paleta PaletaImg { get; set; }
        public Bitmap Imagen { get { return DataImg.Img + PaletaImg.Colores; } }


        public static Paquete GetSprite(RomGba rom)
        {
            return rom.GetPaquete("Sprites Clase Entrenadores",(r,i)=>GetSprite(r,i),GetTotal(rom));
        }
        public static SpriteClaseEntrenador GetSprite(RomGba rom, int index)
        {
            Sprite sprite = new Sprite();
            sprite.DataImg = Data.GetData(rom, index);
            sprite.PaletaImg = Paleta.GetPaleta(rom, index);

            return new SpriteClaseEntrenador() { Imagen = sprite.Imagen };
        }

        public static int GetTotal(RomGba rom)
        {
            int offsetTablaEntrenadorImg = Zona.GetOffsetRom(Data.ZonaImgSprite, rom).Offset;
            int offsetTablaEntrenadorPaleta = Zona.GetOffsetRom(Paleta.ZonaPaletaSprite, rom).Offset;
            int imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
            int numero = 0;
            while (BloqueImagen.IsHeaderOk(rom, imgActual) && PokemonGBAFrameWork.Paleta.IsHeaderOk(rom, paletaActual))
            {
                numero++;
                imgActual += BloqueImagen.LENGTHHEADERCOMPLETO;
                paletaActual += PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO;
            }
            return numero;
        }
    }
}
