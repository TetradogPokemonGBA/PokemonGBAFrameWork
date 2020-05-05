using System;

namespace PokemonGBAFramework.Core
{//de momento no funciona
    public class SpriteObjetoBaseSecreta : SpriteObjetoBase
    {
        //objetos base secreta
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x11, 0x1C, 0x1A, 0x1C, 0x12 };
        public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 64;

        public static int GetTotal(RomGba rom, OffsetRom offsetSpritesObjetosBaseSecreta = default) => 1;//de momento
        public static SpriteObjetoBaseSecreta[] Get(RomGba rom, OffsetRom offsetSpriteObjeto = default, int totalObjetos = -1)
        {
            return SpriteObjetoBase.Get<SpriteObjetoBaseSecreta>(rom, MuestraAlgoritmoEsmeralda, IndexRelativoEsmeralda, offsetSpriteObjeto, totalObjetos < 0 ? GetTotal(rom) : totalObjetos);
        }


        public static SpriteObjetoBaseSecreta Get(RomGba rom, int index, OffsetRom offsetSpriteObjeto = default)
        {
            return SpriteObjetoBase.Get<SpriteObjetoBaseSecreta>(rom, index, MuestraAlgoritmoEsmeralda, IndexRelativoEsmeralda, offsetSpriteObjeto);
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static Zona GetZona(RomGba rom)
        {
            if (rom.Edicion.EsKanto)
                throw new Exception("No hay bases secretas en Kanto!");
            if (!rom.Edicion.EsEsmeralda)
                throw new Exception("No se ha investigado");
            return SpriteObjetoBase.GetZona(rom, MuestraAlgoritmoEsmeralda, IndexRelativoEsmeralda);
        }
    }
}