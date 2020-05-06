using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class ClaseEntrenador
    {
        public RateMoneyClaseEntrenador RateMoney { get; set; }
        public SpriteClaseEntrenador Sprite { get; set; }
        public NombreClaseEntrenador Nombre { get; set; }

        public override string ToString()
        {
            return Nombre.ToString();
        }
        public static explicit operator Bitmap(ClaseEntrenador entrenador)=>entrenador.Sprite;
        public static OffsetRom[] GetOffsets(RomGba rom)
        {
            return new OffsetRom[] { NombreClaseEntrenador.GetOffset(rom), SpriteClaseEntrenador.Data.GetOffset(rom), SpriteClaseEntrenador.Paleta.GetOffset(rom), !rom.Edicion.EsRubiOZafiro ? RateMoneyClaseEntrenador.GetOffset(rom) : default };
        }

        public static ClaseEntrenador Get(RomGba rom, int indexClaseEntrenador, OffsetRom[] offsetsClaseEntrenador = default)
        {
            ClaseEntrenador claseEntrenador = new ClaseEntrenador();

            if (Equals(offsetsClaseEntrenador, default))
                offsetsClaseEntrenador = GetOffsets(rom);

            claseEntrenador.Sprite = SpriteClaseEntrenador.Get(rom, indexClaseEntrenador, offsetsClaseEntrenador[1], offsetsClaseEntrenador[2]);
            claseEntrenador.Nombre = NombreClaseEntrenador.Get(rom, indexClaseEntrenador, offsetsClaseEntrenador[0]);
            claseEntrenador.RateMoney = RateMoneyClaseEntrenador.Get(rom, indexClaseEntrenador, offsetsClaseEntrenador[3]);

            return claseEntrenador;
        }
        public static ClaseEntrenador[] Get(RomGba rom, OffsetRom[] offsetsClaseEntrenador = default, int totalObjetos = -1)
        {
            if (Equals(offsetsClaseEntrenador, default))
                offsetsClaseEntrenador = GetOffsets(rom);

            ClaseEntrenador[] claseEntrenadorList = new ClaseEntrenador[totalObjetos < 0 ? SpriteClaseEntrenador.GetTotal(rom, offsetsClaseEntrenador[1], offsetsClaseEntrenador[2]) : totalObjetos];
            for (int i = 0; i < claseEntrenadorList.Length; i++)
                claseEntrenadorList[i] = Get(rom, i, offsetsClaseEntrenador);
            return claseEntrenadorList;

        }
    }
}
