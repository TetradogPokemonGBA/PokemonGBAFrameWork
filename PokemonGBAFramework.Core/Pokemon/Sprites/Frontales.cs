using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Frontales 
    {
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
        public Frontales()
        {
            Sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites { get; private set; }

        public BitmapAnimated GetAnimacionImagenFrontal(Paleta paleta)
        {
            BitmapAnimated bmpAnimated;
            Bitmap[] gifAnimated = new Bitmap[Sprites.Count + 2];
            int[] delay = new int[gifAnimated.Length];


            gifAnimated[1] = Sprites[0] + PaletaAnimacion;
            delay[1] = 200;
            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = Sprites[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[0] = gifAnimated[2];
            bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);

            return bmpAnimated;
        }
        public static Frontales Get(RomGba rom, int posicion,OffsetRom offsetImgFrontal=default)
        {
            if (Equals(offsetImgFrontal, default))
                offsetImgFrontal = GetOffset(rom);

            byte[] auxImg;
            Frontales frontales = new Frontales();
            int offsetImgFrontalPokemon = offsetImgFrontal + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgFrontal = BloqueImagen.GetBloqueImagen(rom, offsetImgFrontalPokemon);

            auxImg = bloqueImgFrontal.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                frontales.Sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }

            return frontales;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            throw new NotImplementedException();
        }

        public static Frontales[] Get(RomGba rom) => Huella.GetAll<Frontales>(rom, Get, GetOffset(rom));

        public static Frontales[] GetOrdenLocal(RomGba rom) => OrdenLocal.GetOrdenados<Frontales>(rom, (r, o) => Frontales.Get(r), GetOffset(rom));
        public static Frontales[] GetOrdenNacional(RomGba rom) => OrdenNacional.GetOrdenados<Frontales>(rom, (r, o) => Frontales.Get(r), GetOffset(rom));

    }
}