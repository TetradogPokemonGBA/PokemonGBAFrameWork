using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Frontales:BaseSprite
    {

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x82, 0x42, 0x02, 0xD0, 0x88 };
        public static readonly int IndexRelativoRubiYZafiro = -32 - MuestraAlgoritmoRubiYZafiro.Length;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x0C, 0x1C, 0x17, 0x1C, 0x1D };
        public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length - 80;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x00, 0x20, 0x09, 0x5E, 0x51, 0x40 };
        public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 64;

        #region Animacion Imagen Frontal Esmeralda
        private static readonly Paleta PaletaAnimacion;

        static Frontales()
        {
               PaletaAnimacion = new Paleta();

            for (int i = 1; i < Paleta.LENGTH; i++)
            {

                PaletaAnimacion.Colores[i] = System.Drawing.Color.Black;
            }

            PaletaAnimacion.Colores[0] = System.Drawing.Color.White;

        }


        public BitmapAnimated GetAnimacionImagenFrontal(Paleta paleta)
        {
            BitmapAnimated bmpAnimated;
            Bitmap[] gifAnimated = new Bitmap[Sprites.Count + 2];
            int[] delay = new int[gifAnimated.Length];

            //para que se vea en negro al empezar
            gifAnimated[0] = Sprites[0] + PaletaAnimacion;
            delay[0] = 450;

            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = Sprites[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[1] = gifAnimated[2];
            delay[1] = 350;
            
            bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);
            bmpAnimated.FrameAlAcabar = 2;
            return bmpAnimated;
        }
        #endregion
        public static explicit operator Bitmap(Frontales frontales)=>frontales.Sprites[0];
        public static Frontales Get(RomGba rom, int posicion, OffsetRom offsetImgFrontal = default)
        {
            return BaseSprite.Get<Frontales>(rom, posicion, offsetImgFrontal, GetMuestra(rom), GetIndex(rom));
        }

        public static Frontales[] Get(RomGba rom, OffsetRom offsetImgFrontal = default)
        {
            return BaseSprite.Get<Frontales>(rom, GetMuestra(rom), GetIndex(rom), offsetImgFrontal);
        }

        public static Frontales[] GetOrdenLocal(RomGba rom, OffsetRom offsetImgFrontal = default)
        {
            return BaseSprite.GetOrdenLocal<Frontales>(rom, GetMuestra(rom), GetIndex(rom), offsetImgFrontal);
        }
        public static Frontales[] GetOrdenNacional(RomGba rom, OffsetRom offsetImgFrontal = default)
        {
            return BaseSprite.GetOrdenNacional<Frontales>(rom, GetMuestra(rom), GetIndex(rom),offsetImgFrontal);
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return BaseSprite.GetOffset(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static Zona GetZona(RomGba rom)
        {
            return BaseSprite.GetZona(rom, GetMuestra(rom), GetIndex(rom));
        }
        static byte[] GetMuestra(RomGba rom)
        {
            byte[] algoritmo;

            if (rom.Edicion.EsKanto)
            {
                algoritmo = MuestraAlgoritmoKanto;

            }
            else if (rom.Edicion.Version == Edicion.Pokemon.Esmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;

            }
            else
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;

            }
            return algoritmo;
        }

        static int GetIndex(RomGba rom)
        {
            int inicio;
            if (rom.Edicion.EsKanto)
            {
                inicio = IndexRelativoKanto;
            }
            else if (rom.Edicion.Version == Edicion.Pokemon.Esmeralda)
            {
                inicio = IndexRelativoEsmeralda;
            }
            else
            {
                inicio = IndexRelativoRubiYZafiro;
            }
            return inicio;
        }
    }
}