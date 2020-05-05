using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class SpriteObjeto:SpriteObjetoBase
    {

        public static readonly  byte[] MuestraAlgoritmoKanto = {0x09, 0x0E, 0x06, 0x48, 0x83, 0x42 };
        public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length + 32;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x09, 0x0E, 0x02, 0x48, 0x83 };
        public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length + 48;


        public static SpriteObjeto[] Get(RomGba rom, OffsetRom offsetSpriteObjeto = default,int totalObjetos=-1)
        {
            return SpriteObjetoBase.Get<SpriteObjeto>(rom, GetMuestra(rom), GetIndex(rom), offsetSpriteObjeto, totalObjetos < 0 ? DatosObjeto.GetTotal(rom) : totalObjetos);
        }


        public static SpriteObjeto Get(RomGba rom, int index,OffsetRom offsetSpriteObjeto=default)
        {
            return SpriteObjetoBase.Get<SpriteObjeto>(rom, index, GetMuestra(rom), GetIndex(rom), offsetSpriteObjeto);
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static Zona GetZona(RomGba rom)
        {
            if (rom.Edicion.EsRubiOZafiro)
                throw new Exception("Rubi Y Zafiro no tienen imagenes en los objetos!");
            return SpriteObjetoBase.GetZona(rom,rom.Edicion.EsEsmeralda ? MuestraAlgoritmoEsmeralda : MuestraAlgoritmoKanto, rom.Edicion.EsEsmeralda ? IndexRelativoEsmeralda : IndexRelativoKanto);
        }

        private static int GetIndex(RomGba rom)
        {
            return rom.Edicion.EsEsmeralda ? IndexRelativoEsmeralda : IndexRelativoKanto;
        }

        private static byte[] GetMuestra(RomGba rom)
        {
            return rom.Edicion.EsEsmeralda ? MuestraAlgoritmoEsmeralda : MuestraAlgoritmoKanto;
        }

    }
}