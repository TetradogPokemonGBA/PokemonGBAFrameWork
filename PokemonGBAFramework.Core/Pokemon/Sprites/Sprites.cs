using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Sprites
    {
        [Flags]
        public enum Sprite
        {
            Trasero=2,Frontal=4,Shiny=8
        }
        public const int LONGITUDLADO = 64;
        public const int TAMAÑOIMAGENDESCOMPRIMIDA = 2048;

        public Frontales Frontales { get; set; }
        public Traseros Traseros { get; set; }
        public PaletaNormal PaletaNomal { get; set; }
        public PaletaShiny PaletaShiny { get; set; }

        public Bitmap Get(Sprite sprite, int frame = 0)
        {
            IList<BloqueImagen> sprites;


            if (sprite.HasFlag(Sprite.Frontal))
                sprites = Frontales.Sprites;
            else if (sprite.HasFlag(Sprite.Trasero))
                sprites = Traseros.Sprites;
            else throw new Exception("Se tiene que especificar si se quiere Frontal o Trasero");

            if (frame < 0)
                frame = sprites.Count - (frame * -1 % sprites.Count);//mirar que sea lo mismo que ir marcha atrás

            return sprites[frame % sprites.Count] + (sprite.HasFlag(Sprite.Shiny) ? PaletaShiny.Paleta : PaletaNomal.Paleta);
        }
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
