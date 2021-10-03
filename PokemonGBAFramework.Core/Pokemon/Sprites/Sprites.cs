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
            return new OffsetRom[] { Frontales.GetOffset(rom), Traseros.GetOffset(rom), PaletaNormal.GetOffset(rom), PaletaShiny.GetOffset(rom) };
        }

        public static Sprites Get(RomGba rom, int ordenGameFreak, OffsetRom[] offsetsSprites = default)
        {
            Paleta[] paletas;
            Sprites sprites = new Sprites();

            if (Equals(offsetsSprites, default))
                offsetsSprites = GetOffsets(rom);

            sprites.Frontales = Frontales.Get(rom, ordenGameFreak, offsetsSprites[0]);
            sprites.Traseros = Traseros.Get(rom, ordenGameFreak, offsetsSprites[1]);
            sprites.PaletaNomal = PaletaNormal.Get(rom, ordenGameFreak, offsetsSprites[2]);
            sprites.PaletaShiny = PaletaShiny.Get(rom, ordenGameFreak, offsetsSprites[3]);
            paletas = new Paleta[] { sprites.PaletaNomal, sprites.PaletaShiny };

            for (int i = 0; i < sprites.Frontales.Sprites.Count; i++)
                sprites.Frontales.Sprites[i].Paletas.AddRange(paletas);
            for (int i = 0; i < sprites.Traseros.Sprites.Count; i++)
                sprites.Traseros.Sprites[i].Paletas.AddRange(paletas);

            return sprites;
        }
        public static void Set(RomGba rom, Pokemon pokemon, OffsetRom[] offsetsSprites = default)
        {
            Set(rom, pokemon.OrdenGameFreak, pokemon.Sprites, offsetsSprites);
        }
        public static void Set(RomGba rom, int ordenGameFreak, Sprites sprites, OffsetRom[] offsetsSprites = default)
        {
            if (Equals(offsetsSprites, default))
                offsetsSprites = GetOffsets(rom);

            Frontales.Set(rom, ordenGameFreak, sprites.Frontales, offsetsSprites[0]);
            Traseros.Set(rom, ordenGameFreak, sprites.Traseros, offsetsSprites[1]);
            PaletaNormal.Set(rom, ordenGameFreak, sprites.PaletaNomal, offsetsSprites[2]);
            PaletaShiny.Set(rom, ordenGameFreak, sprites.PaletaShiny, offsetsSprites[3]);

        }
        public static Sprites[] Get(RomGba rom, OffsetRom[] offsetsSprites = default)
        {
            if (Equals(offsetsSprites, default))
                offsetsSprites = GetOffsets(rom);

            return Huella.GetAll<Sprites>(rom, (r, i, o) => Get(r, i, offsetsSprites));
        }
        public static Sprites[] GetOrdenLocal(RomGba rom, OffsetRom[] offsetsSprites = default, OffsetRom offsetOrdenLocal = default)
        {
            Sprites[] pokemon = Get(rom, offsetsSprites);
            Sprites[] ordenados = new Sprites[pokemon.Length];

            if (Equals(offsetOrdenLocal, default))
                offsetOrdenLocal = OrdenLocal.GetOffset(rom);

            ordenados[0] = pokemon[0];

            for (int i = 1; i < pokemon.Length; i++)
                ordenados[(int)(Word)OrdenLocal.Get(rom, i, offsetOrdenLocal)] = pokemon[i];
            return ordenados;
        }
        public static Sprites[] GetOrdenNacional(RomGba rom, OffsetRom[] offsetsSprites = default, OffsetRom offsetOrdenNacional = default)
        {
            Sprites[] pokemon = Get(rom, offsetsSprites);
            Sprites[] ordenados = new Sprites[pokemon.Length];

            if (Equals(offsetOrdenNacional, default))
                offsetOrdenNacional = OrdenNacional.GetOffset(rom);

            ordenados[0] = pokemon[0];

            for (int i = 1; i < pokemon.Length; i++)
                ordenados[(int)(Word)OrdenNacional.Get(rom, i, offsetOrdenNacional)] = pokemon[i];
            return ordenados;
        }
    }
}
