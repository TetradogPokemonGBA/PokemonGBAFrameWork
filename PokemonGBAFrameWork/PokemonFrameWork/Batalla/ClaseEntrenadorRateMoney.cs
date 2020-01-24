using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class RateMoney : PokemonFrameWorkItem
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

        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static RateMoney[] GetRateMoney(RomGba rom)
        {
            RateMoney[] rates = new RateMoney[ClaseEntrenador.Sprite.GetTotal(rom)];
            for (int i = 0; i < rates.Length; i++)
                rates[i] = GetRateMoney(rom, i);
            return rates;
        }
        public static RateMoney GetRateMoney(RomGba rom, int index)
        {
            RateMoney rateMoney = new RateMoney();
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetRateMoney;
            if (!edicion.EsRubiOZafiro)
            {
                offsetRateMoney = Zona.GetOffsetRom(ZonaRatesMoney, rom).Offset + index * (int)Longitud.RateMoney;
                rateMoney.Rate = rom.Data[offsetRateMoney];
                if (edicion.EsEsmeralda)
                    rateMoney.IdFuente = EdicionPokemon.IDESMERALDA;
                else if (edicion.EsRubiOZafiro)
                    rateMoney.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
                else
                    rateMoney.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

                rateMoney.IdElemento = (ushort)index;
            }

            return rateMoney;
        }

    }
}
