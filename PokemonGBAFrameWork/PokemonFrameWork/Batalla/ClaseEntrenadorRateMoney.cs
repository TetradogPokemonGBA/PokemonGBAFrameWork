using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Poke;
using PokemonGBAFramework;
using PokemonGBAFramework.Batalla;
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

        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<RateMoney>();
        public static readonly Zona ZonaRatesMoney;
        public const byte ID = 0x2;
        byte rate;



        static RateMoney()
        {
            ZonaRatesMoney = new Zona("Rate Money Entrenador");
            //añado las zonas :)
            ZonaRatesMoney.Add(0x4E6A8, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
            ZonaRatesMoney.Add(0x2593C, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
            ZonaRatesMoney.Add(EdicionPokemon.RojoFuegoUsa10, 0x259CC, 0x259E0);
            ZonaRatesMoney.Add(EdicionPokemon.VerdeHojaUsa10, 0x259CC, 0x259E0);
            //falta rate money Rubi y Zafiro???no hay?????
        }
        public byte Rate { get => rate; set => rate = value; }



        public static Paquete GetRateMoney(RomGba rom)
        {
            return rom.GetPaquete("Nombres Clases Entrenador", (r, i) => GetRateMoney(r, i), ClaseEntrenador.Sprite.GetTotal(rom));
        }
        public static RateMoneyClaseEntrenador GetRateMoney(RomGba rom, int index)
        {
            int rate;
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetRateMoney;
            if (!edicion.EsRubiOZafiro)
            {
                offsetRateMoney = Zona.GetOffsetRom(ZonaRatesMoney, rom).Offset + index * (int)Longitud.RateMoney;
                rate = rom.Data[offsetRateMoney];

            }
            else rate = -1;

            return new RateMoneyClaseEntrenador() { Rate = rate };
        }

    }
}
