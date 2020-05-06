using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class RateMoneyClaseEntrenador 
    {
        enum Longitud
        {
            RateMoney = 4,
        }

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x48, 0x44, 0x40, 0x78, 0x40, 0x00, 0x0F, 0xE0 };
        public static readonly int IndexRelativoEsmeralda = 0;
        public static readonly byte[] MuestraAlgoritmoKanto = { 0x00, 0x1C, 0x1C, 0x44, 0x43, 0x0D, 0xE0 };
        public static readonly int IndexRelativoKanto = 0;

        public byte Rate { get; set; }
        public override string ToString()
        {
            return Rate.ToString();
        }

        public static implicit operator int(RateMoneyClaseEntrenador rate)=>rate.Rate;
        public static RateMoneyClaseEntrenador[] Get(RomGba rom,OffsetRom offsetRateMoneyClaseEntrenador=default,OffsetRom offsetSpriteClaseEntrenador=default,OffsetRom offsetPaletaSpriteClaseEntrenador=default,int totalClaseEntrenador=-1)
        {
            RateMoneyClaseEntrenador[] rates = new RateMoneyClaseEntrenador[totalClaseEntrenador<0?SpriteClaseEntrenador.GetTotal(rom,offsetSpriteClaseEntrenador,offsetPaletaSpriteClaseEntrenador):totalClaseEntrenador];
           if(!rom.Edicion.EsRubiOZafiro)
                offsetRateMoneyClaseEntrenador = Equals(offsetRateMoneyClaseEntrenador, default) ? GetOffset(rom) : offsetRateMoneyClaseEntrenador;
           
            for (int i = 0; i < rates.Length; i++)
                rates[i] = Get(rom, i,offsetRateMoneyClaseEntrenador);
          
            return rates;
        }
        public static RateMoneyClaseEntrenador Get(RomGba rom, int index,OffsetRom offsetRateMoneyClaseEntrenador=default)
        {
            int offsetRateMoney;
            RateMoneyClaseEntrenador rateMoney = new RateMoneyClaseEntrenador();

            if (!rom.Edicion.EsRubiOZafiro)
            {
                offsetRateMoney = (Equals(offsetRateMoneyClaseEntrenador,default)?GetOffset(rom):offsetRateMoneyClaseEntrenador) + index * (int)Longitud.RateMoney;
                rateMoney.Rate = rom.Data[offsetRateMoney];

            }

            return rateMoney;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            if (rom.Edicion.EsRubiOZafiro)
                throw new Exception("No hay RateMoney en Rubi y Zafiro");
            return Zona.Search(rom, rom.Edicion.EsEsmeralda ? MuestraAlgoritmoEsmeralda : MuestraAlgoritmoKanto, rom.Edicion.EsEsmeralda ? IndexRelativoEsmeralda : IndexRelativoKanto);
        }
    }
}