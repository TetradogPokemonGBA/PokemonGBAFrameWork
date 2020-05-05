using System.Drawing;

namespace PokemonGBAFramework.Core
{
    public class SpriteObjetoBase
    {
        public BloqueImagen Sprite { get; set; }

        public static implicit operator BloqueImagen(SpriteObjetoBase pokeball) => pokeball.Sprite;
        public static implicit operator Bitmap(SpriteObjetoBase pokeball) => pokeball.Sprite[0];

        protected static T[] Get<T>(RomGba rom,byte[] muestraAlgorimo,int indexRelativo, OffsetRom offsetSpriteObjeto = default, int totalObjetos = -1) where T : SpriteObjetoBase, new()
        {
            T[] sprites = new T[totalObjetos];

            if (!rom.Edicion.EsRubiOZafiro)
            {
                if (Equals(offsetSpriteObjeto, default))
                    offsetSpriteObjeto = GetOffset(rom, muestraAlgorimo, indexRelativo);
            }
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = Get<T>(rom, i,muestraAlgorimo,indexRelativo,offsetSpriteObjeto);

            return sprites;
        }
        protected static T Get<T>(RomGba rom, int index, byte[] muestraAlgorimo, int indexRelativo, OffsetRom offsetSpriteObjeto = default) where T:SpriteObjetoBase,new()
        {
            BloqueImagen blImg;
            int offsetImagenYPaleta;
            T sprite = new T();
            if (!rom.Edicion.EsRubiOZafiro)
            {
                offsetImagenYPaleta = (Equals(offsetSpriteObjeto, default) ? GetOffset(rom,muestraAlgorimo,indexRelativo) : offsetSpriteObjeto) + index * (OffsetRom.LENGTH + OffsetRom.LENGTH);
                //esas ediciones no tienen imagen los objetos
                blImg = BloqueImagen.GetBloqueImagenSinHeader(rom, offsetImagenYPaleta);
                blImg.Paletas.Add(Paleta.GetPaletaSinHeader(rom, offsetImagenYPaleta + OffsetRom.LENGTH));
                sprite.Sprite = blImg;
            }
            return sprite;
        }

        protected static OffsetRom GetOffset(RomGba rom, byte[] muestraAlgorimo, int indexRelativo)
        {
            return new OffsetRom(rom, GetZona(rom,muestraAlgorimo,indexRelativo));
        }

        protected static Zona GetZona(RomGba rom, byte[] muestraAlgorimo, int indexRelativo)
        {

            return Zona.Search(rom, muestraAlgorimo,indexRelativo);
        }
    }
}