using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Objeto
    {
        public SpriteObjeto Sprite { get; set; }
        public DatosObjeto Datos { get; set; }
        public static OffsetRom[] GetOffsets(RomGba rom)
        {
            return new OffsetRom[] { DatosObjeto.GetOffset(rom),!rom.Edicion.EsRubiOZafiro? SpriteObjeto.GetOffset(rom):default};
        }

        public static Objeto Get(RomGba rom, int indexObjeto, OffsetRom[] offsetsObjeto = default)
        {
            Objeto objeto = new Objeto();

            if (Equals(offsetsObjeto, default))
                offsetsObjeto = GetOffsets(rom);

            objeto.Sprite = SpriteObjeto.Get(rom, indexObjeto, offsetsObjeto[1]);
            objeto.Datos = DatosObjeto.Get(rom, indexObjeto, offsetsObjeto[0]);

            return objeto;
        }
        public static Objeto[] Get(RomGba rom, OffsetRom[] offsetsObjetos = default,int totalObjetos=-1)
        {
            if (Equals(offsetsObjetos, default))
                offsetsObjetos = GetOffsets(rom);

            Objeto[] objetos = new Objeto[totalObjetos < 0 ? DatosObjeto.GetTotal(rom, offsetsObjetos[0]) : totalObjetos];
            for (int i = 0; i < objetos.Length; i++)
                objetos[i] = Get(rom, i, offsetsObjetos);
            return objetos;
          
        }
    }
}
