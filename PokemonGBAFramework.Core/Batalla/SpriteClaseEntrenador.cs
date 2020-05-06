using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
   public class SpriteClaseEntrenador
    {
        public class Data
        {

            public static readonly byte[] MuestraAlgoritmoKanto = { 0xF0, 0xB5, 0x0A, 0x4C, 0x20, 0x1C };
            public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length-OffsetRom.LENGTH;

            public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0xF0, 0xB5, 0x0A, 0x4C, 0x20, 0x1C, 0xD0 };
            public static readonly int IndexRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length -OffsetRom.LENGTH;

            public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x24, 0x18, 0x20, 0x1C, 0xD6, 0xF7, 0x0E, 0xFB };
            public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length +16;

            public BloqueImagen Sprite { get; set; }

            public static Data Get(RomGba rom, int index,OffsetRom offsetSpriteClaseEntrenador=default)
            {
                int offsetSpriteImg =(Equals(offsetSpriteClaseEntrenador,default)?GetOffset(rom):offsetSpriteClaseEntrenador) + index * BloqueImagen.LENGTHHEADERCOMPLETO;
                Data data = new Data() { Sprite = BloqueImagen.GetBloqueImagen(rom, offsetSpriteImg) };

                return data;
            }

            public static OffsetRom GetOffset(RomGba rom)
            {
                return new OffsetRom(rom, GetZona(rom));
            }

            public static Zona GetZona(RomGba rom)
            {
                byte[] algoritmo;
                int index;
                if (rom.Edicion.EsEsmeralda)
                {
                    algoritmo = MuestraAlgoritmoEsmeralda;
                    index = IndexRelativoEsmeralda;
                }
                else if (rom.Edicion.EsHoenn)
                {
                    algoritmo = MuestraAlgoritmoRubiYZafiro;
                    index = IndexRelativoRubiYZafiro;
                }
                else
                {
                    algoritmo = MuestraAlgoritmoKanto;
                    index = IndexRelativoKanto;
                }
                return Zona.Search(rom, algoritmo, index);
            }
        }
        public class Paleta 
        {
            public static readonly byte[] MuestraAlgoritmoKanto = { 0x00, 0xB5, 0x00, 0x04, 0x09, 0x06, 0x07};
            public static readonly int IndexRelativoKanto = MuestraAlgoritmoKanto.Length;

            public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0xE1, 0x00, 0x88, 0x46, 0x12, 0x4E };
            public static readonly int IndexRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length-32;

            public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x01, 0x70, 0x60, 0xE0 };
            public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 32;

            public Paleta Colores { get; set; }
            public static implicit operator Core.Paleta(Paleta paleta)=>paleta.Colores;

            public static Paleta Get(RomGba rom, int index,OffsetRom offsetPaletaSpriteClaseEntrenador=default)
            {
          
                int offsetSpritePaleta = Equals(offsetPaletaSpriteClaseEntrenador,default)?GetOffset(rom):offsetPaletaSpriteClaseEntrenador + index * Core.Paleta.LENGTHHEADERCOMPLETO;
                Paleta paleta = Paleta.Get(rom, offsetSpritePaleta);
             
                return paleta;
            }

            public static OffsetRom GetOffset(RomGba rom)
            {
                return new OffsetRom(rom, GetZona(rom));
            }

            public static Zona GetZona(RomGba rom)
            {
                byte[] algoritmo;
                int index;
                if (rom.Edicion.EsEsmeralda)
                {
                    algoritmo = MuestraAlgoritmoEsmeralda;
                    index = IndexRelativoEsmeralda;
                }else if (rom.Edicion.EsHoenn)
                {
                    algoritmo = MuestraAlgoritmoRubiYZafiro;
                    index = IndexRelativoRubiYZafiro;
                }
                else
                {
                    algoritmo = MuestraAlgoritmoKanto;
                    index = IndexRelativoKanto;
                }
                return Zona.Search(rom, algoritmo, index);
            }
        }


        public Data DataImg { get; set; }
        public Paleta PaletaImg { get; set; }

        public static implicit operator Bitmap(SpriteClaseEntrenador sprite)=>sprite.DataImg.Sprite+sprite.PaletaImg;

        public static SpriteClaseEntrenador[] Get(RomGba rom,OffsetRom offsetSpriteClaseEntrenador = default, OffsetRom offsetPaletaSpriteClaseEntrenador = default,int totalClaseEntrenador=-1)
        {
            SpriteClaseEntrenador[] sprites;
            offsetSpriteClaseEntrenador = Equals(offsetSpriteClaseEntrenador, default) ? Data.GetOffset(rom) : offsetSpriteClaseEntrenador;
            offsetPaletaSpriteClaseEntrenador = Equals(offsetPaletaSpriteClaseEntrenador, default) ? Paleta.GetOffset(rom) : offsetPaletaSpriteClaseEntrenador;
            sprites = new SpriteClaseEntrenador[totalClaseEntrenador < 0 ? GetTotal(rom, offsetSpriteClaseEntrenador, offsetPaletaSpriteClaseEntrenador) : totalClaseEntrenador];
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = Get(rom, i,offsetSpriteClaseEntrenador,offsetPaletaSpriteClaseEntrenador);
            return sprites;
        }
        public static SpriteClaseEntrenador Get(RomGba rom, int index,OffsetRom offsetSpriteClaseEntrenador=default,OffsetRom offsetPaletaSpriteClaseEntrenador=default)
        {
            SpriteClaseEntrenador sprite = new SpriteClaseEntrenador();
            sprite.DataImg = Data.Get(rom, index,offsetSpriteClaseEntrenador);
            sprite.PaletaImg = Paleta.Get(rom, index,offsetPaletaSpriteClaseEntrenador);

            return sprite;
        }

        public static int GetTotal(RomGba rom,OffsetRom offsetSpriteClaseEntrenador=default,OffsetRom offsetPaletaSpriteClaseEntrenador=default)
        {
            int offsetTablaEntrenadorImg = Equals(offsetSpriteClaseEntrenador,default)?Data.GetOffset(rom):offsetSpriteClaseEntrenador;
            int offsetTablaEntrenadorPaleta = Equals(offsetPaletaSpriteClaseEntrenador, default) ? Paleta.GetOffset(rom) : offsetPaletaSpriteClaseEntrenador;
            int imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
            int numero = 0;
            while (BloqueImagen.IsHeaderOk(rom, imgActual) && Core.Paleta.IsHeaderOk(rom, paletaActual))
            {
                numero++;
                imgActual += BloqueImagen.LENGTHHEADERCOMPLETO;
                paletaActual += Core.Paleta.LENGTHHEADERCOMPLETO;
            }
            return numero;
        }
    }
}