﻿using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Poke;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class Frontales
    {
        public const int LONGITUDLADO = 64;
        public const int TAMAÑOIMAGENDESCOMPRIMIDA = 2048;
        public const byte ID = 0x24;
        private static readonly Paleta PaletaAnimacion;
        public static readonly Zona ZonaImgFrontal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Frontales>();
        Llista<BloqueImagen> sprites;
        static Frontales()
        {
            ZonaImgFrontal = new Zona("Imagen Frontal Pokemon");
            ZonaImgFrontal.Add(0xD324, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaImgFrontal.Add(0x128, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10);
            PaletaAnimacion = new Paleta();

            for (int i = 1; i < Paleta.LENGTH; i++)
            {

                PaletaAnimacion.Colores[i] = System.Drawing.Color.Black;
            }

            PaletaAnimacion.Colores[0] = System.Drawing.Color.White;

        }
        public Frontales()
        {
            sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites
        {
            get
            {
                return sprites;
            }
        }


        public BitmapAnimated GetAnimacionImagenFrontal(Paleta paleta)
        {
            Bitmap[] gifAnimated = new Bitmap[sprites.Count + 2];
            int[] delay = new int[gifAnimated.Length];

            BitmapAnimated bmpAnimated;
            gifAnimated[1] = sprites[0] + PaletaAnimacion;
            delay[1] = 200;
            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = sprites[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[0] = gifAnimated[2];
            bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);

            return bmpAnimated;
        }
        public static PokemonGBAFramework.Pokemon.Sprites.Frontales GetFrontales(RomGba rom,int posicion)
        {
            byte[] auxImg;
            Frontales frontales = new Frontales();
            int offsetImgFrontalPokemon = Zona.GetOffsetRom(ZonaImgFrontal, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgFrontal = BloqueImagen.GetBloqueImagen(rom, offsetImgFrontalPokemon);
            auxImg = bloqueImgFrontal.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                frontales.sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, TAMAÑOIMAGENDESCOMPRIMIDA))));
            }

            return new PokemonGBAFramework.Pokemon.Sprites.Frontales() { Imagenes=frontales.Sprites.Select((img)=>img.GetImg()).ToList()};
        }
        public static PokemonGBAFramework.Paquete GetFrontales(RomGba rom)
        {
            return rom.GetPaquete("Frontales Pokemon", (r, i) => GetFrontales(r, i), Huella.GetTotal(rom));
        }
 

    }
}
