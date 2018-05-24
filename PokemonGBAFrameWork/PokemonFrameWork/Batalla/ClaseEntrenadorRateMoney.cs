using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class RateMoney
    {
        enum Longitud
        {
            RateMoney = 4,
        }


        public static readonly Zona ZonaRatesMoney;

        byte rate;

        

        static RateMoney()
        {
            ZonaRatesMoney = new Zona("Rate Money Entrenador");
            //añado las zonas :)
            ZonaRatesMoney.Add(0x4E6A8, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);
            ZonaRatesMoney.Add(0x2593C, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaRatesMoney.Add(EdicionPokemon.RojoFuegoUsa, 0x259CC, 0x259E0);
            ZonaRatesMoney.Add(EdicionPokemon.VerdeHojaUsa, 0x259CC, 0x259E0);
            //falta rate money Rubi y Zafiro???no hay?????
        }
        public byte Rate { get => rate; set => rate = value; }

        public static RateMoney[] GetRateMoney(RomGba rom)
        {
            RateMoney[] rates = new RateMoney[ClaseEntrenador.Sprite.GetTotal(rom)];
            for (int i = 0; i < rates.Length; i++)
                rates[i] = GetRateMoney(rom, i);
            return rates;
        }
        public static RateMoney GetRateMoney(RomGba rom,int index)
        {
            RateMoney rateMoney = new RateMoney();
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetRateMoney;
            if (!edicion.EsRubiOZafiro)
            {
                offsetRateMoney = Zona.GetOffsetRom(ZonaRatesMoney, rom).Offset + index * (int)Longitud.RateMoney;
                rateMoney.Rate = rom.Data[offsetRateMoney];
            }
            return rateMoney;
        }
        public static void SetRateMoney(RomGba rom,int index,RateMoney rate)
        {
            int offsetInicioRateMoney;
            if (!((EdicionPokemon)rom.Edicion).EsRubiOZafiro)
            {
                offsetInicioRateMoney = Zona.GetOffsetRom(ZonaRatesMoney, rom).Offset + (index * (int)Longitud.RateMoney);
                rom.Data.SetArray(offsetInicioRateMoney, Serializar.GetBytes(rate.Rate).AddArray(new byte[] { 0x0, 0x0 }));
            }
        }
        public static void SetRateMoney(RomGba rom, IList< RateMoney> rates)
        {
            OffsetRom offsetInicioRateMoney;
            int totalActual = Sprite.GetTotal(rom);
            if (!((EdicionPokemon)rom.Edicion).EsRubiOZafiro)
            {
                offsetInicioRateMoney = Zona.GetOffsetRom(ZonaRatesMoney, rom);
                rom.Data.Remove(offsetInicioRateMoney.Offset, totalActual * (int)Longitud.RateMoney);
                OffsetRom.SetOffset(rom, offsetInicioRateMoney, rom.Data.SearchEmptyBytes(rates.Count * (int)Longitud.RateMoney));
                for (int i = 0; i < rates.Count; i++)
                    SetRateMoney(rom, i, rates[i]);
            }
        }
    }
}
