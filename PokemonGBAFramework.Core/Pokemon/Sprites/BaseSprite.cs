using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFramework.Core
{
    public abstract class BaseSprite
    {
        public BaseSprite()
        {
            Sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites { get; private set; }
        protected static T Get<T>(RomGba rom, int posicion, OffsetRom offsetImgTrasera, byte[] muestraAlgoritmo, int index) where T : BaseSprite, new()
        {
            if (Equals(offsetImgTrasera, default))
                offsetImgTrasera = GetOffset(rom, muestraAlgoritmo, index);
            byte[] auxImg;
            T traseros = new T();
            int offsetImgTraseraPokemon = offsetImgTrasera + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgTrasera = BloqueImagen.GetBloqueImagen(rom, offsetImgTraseraPokemon);
            auxImg = bloqueImgTrasera.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                traseros.Sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, Core.Sprites.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }

            return traseros;

        }
        protected static OffsetRom GetOffset(RomGba rom, byte[] muestraAlgoritmo, int index)
        {
            return new OffsetRom(rom, GetZona(rom, muestraAlgoritmo, index));
        }

        protected static int GetZona(RomGba rom, byte[] muestraAlgoritmo, int index)
        {
            return Zona.Search(rom, muestraAlgoritmo, index);
        }
        protected static T[] Get<T>(RomGba rom, byte[] muestraAlgoritmo, int index, OffsetRom offsetSprite = default) where T : BaseSprite, new()
        {
            return Huella.GetAll<T>(rom, (r, pos, offset) => Get<T>(r, pos, offset, muestraAlgoritmo, index),Equals(offsetSprite,default)? GetOffset(rom, muestraAlgoritmo, index):offsetSprite);
        }

        protected static T[] GetOrdenLocal<T>(RomGba rom, byte[] muestraAlgoritmo, int index,OffsetRom offsetSprite=default) where T : BaseSprite, new()
        {
            return OrdenLocal.GetOrdenados<T>(rom, (r, o) => Get<T>(r, muestraAlgoritmo, index,offsetSprite));
        }
        protected static T[] GetOrdenNacional<T>(RomGba rom, byte[] muestraAlgoritmo, int index, OffsetRom offsetSprite = default) where T : BaseSprite, new()
        {
            return OrdenNacional.GetOrdenados<T>(rom, (r, o) => Get<T>(r, muestraAlgoritmo, index,offsetSprite));
        }

    }
}