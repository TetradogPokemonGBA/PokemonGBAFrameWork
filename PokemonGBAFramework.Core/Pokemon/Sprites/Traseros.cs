using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Traseros 
    {

        public Traseros()
        {
            Sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites { get; private set; }

        public static Traseros Get(RomGba rom, int posicion,OffsetRom offsetImgTrasera)
        {
            if (Equals(offsetImgTrasera, default))
                offsetImgTrasera = GetOffset(rom);
            byte[] auxImg;
            Traseros traseros = new Traseros();
            int offsetImgTraseraPokemon = offsetImgTrasera + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgTrasera = BloqueImagen.GetBloqueImagen(rom, offsetImgTraseraPokemon);
            auxImg = bloqueImgTrasera.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                traseros.Sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }

            return traseros;

        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            throw new NotImplementedException();
        }

        public static Traseros[] Get(RomGba rom) => Huella.GetAll<Traseros>(rom, Traseros.Get, GetOffset(rom));

        public static Traseros[] GetOrdenLocal(RomGba rom) => OrdenLocal.GetOrdenados<Traseros>(rom, (r, o) => Traseros.Get(r), GetOffset(rom));
        public static Traseros[] GetOrdenNacional(RomGba rom) => OrdenNacional.GetOrdenados<Traseros>(rom, (r, o) => Traseros.Get(r), GetOffset(rom));
    }
}