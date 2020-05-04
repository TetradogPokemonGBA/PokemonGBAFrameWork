using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Sprites
    {
        public const int LONGITUDLADO = 64;
        public const int TAMAÑOIMAGENDESCOMPRIMIDA = 2048;
        
        public Frontales Frontales { get; set; }
        public Traseros Traseros { get; set; }
        public PaletaNormal PaletaNomal { get; set; }
        public PaletaShiny PaletaShiny { get; set; }
        public static OffsetRom[] GetOffsets(RomGba rom)
        {
            return new OffsetRom[] { Frontales.GetOffset(rom), Traseros.GetOffset(rom),PaletaNormal.GetOffset(rom), PaletaShiny.GetOffset(rom) };
        }

        public static Sprites Get(RomGba rom, int ordenGameFreak, OffsetRom[] offsetsSprites=default)
        {
            Sprites sprites = new Sprites();

            if (Equals(offsetsSprites, default))
                offsetsSprites = GetOffsets(rom);

            sprites.Frontales = Frontales.Get(rom, ordenGameFreak, offsetsSprites[0]);
            sprites.Traseros = Traseros.Get(rom, ordenGameFreak, offsetsSprites[1]);
            sprites.PaletaNomal = PaletaNormal.Get(rom, ordenGameFreak, offsetsSprites[2]);
            sprites.PaletaShiny = PaletaShiny.Get(rom, ordenGameFreak, offsetsSprites[3]);

            return sprites;
        }
    }
}
