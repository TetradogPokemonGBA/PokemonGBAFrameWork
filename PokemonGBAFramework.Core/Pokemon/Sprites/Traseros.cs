using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Traseros:BaseSprite
    {
        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x01, 0x0C, 0x00, 0x2F, 0x06 };
        public static readonly int IndexRelativoRubiYZafiro = 16 - MuestraAlgoritmoRubiYZafiro.Length;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0xF8, 0x05, 0x00, 0x00, 0x18, 0x3A, 0x00 };
        public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length - 48;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x40, 0x18, 0x29, 0x1C, 0x3A };
        public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 32;

        public static Traseros Get(RomGba rom, int posicion, OffsetRom offsetImgFrontal = default)
        {
            return BaseSprite.Get<Traseros>(rom,posicion,offsetImgFrontal, GetMuestra(rom), GetIndex(rom));
        }

        public static Traseros[] Get(RomGba rom, OffsetRom offsetTraseros = default)
        {
            return BaseSprite.Get<Traseros>(rom, GetMuestra(rom), GetIndex(rom),offsetTraseros);
        }

        public static Traseros[] GetOrdenLocal(RomGba rom, OffsetRom offsetTraseros = default)
        {
            return BaseSprite.GetOrdenLocal<Traseros>(rom, GetMuestra(rom), GetIndex(rom), offsetTraseros);
        }
        public static Traseros[] GetOrdenNacional(RomGba rom,OffsetRom offsetTraseros=default)
        {
            return BaseSprite.GetOrdenNacional<Traseros>(rom, GetMuestra(rom), GetIndex(rom), offsetTraseros);
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return BaseSprite.GetOffset(rom, GetMuestra(rom), GetIndex(rom));
        }
        public static Zona GetZona(RomGba rom)
        {
            return BaseSprite.GetZona(rom, GetMuestra(rom), GetIndex(rom));
        }
        static byte[] GetMuestra(RomGba rom)
        {
            byte[] algoritmo;

            if (rom.Edicion.EsKanto)
            {
                algoritmo = MuestraAlgoritmoKanto;

            }
            else if (rom.Edicion.Version == Edicion.Pokemon.Esmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;

            }
            else
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;

            }
            return algoritmo;
        }

        static int GetIndex(RomGba rom)
        {
            int inicio;
            if (rom.Edicion.EsKanto)
            {
                inicio = IndexRelativoKanto;
            }
            else if (rom.Edicion.Version == Edicion.Pokemon.Esmeralda)
            {
                inicio = IndexRelativoEsmeralda;
            }
            else
            {
                inicio = IndexRelativoRubiYZafiro;
            }
            return inicio;
        }

    }
}