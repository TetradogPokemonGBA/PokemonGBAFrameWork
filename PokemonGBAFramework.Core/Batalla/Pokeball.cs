using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PokeballBatalla
    {
        public static readonly byte[] MuestraAlgoritmoPaleta  = { 0xA5, 0x8E, 0xE0, 0x8E, 0x00, 0x04 };
        public static readonly int IndexRelativoPaleta = -MuestraAlgoritmoPaleta.Length - 48;

        public static readonly byte[] MuestraAlgoritmoSprite = { 0x00, 0x0E, 0x80, 0x46, 0xA5 };
        public static readonly int IndexRelativoSprite  = -MuestraAlgoritmoSprite.Length - 48;
        public PokeballBatalla()
        {
            Sprite = new BloqueImagen();
        }

        public BloqueImagen Sprite { get; set; }

        public static implicit operator BloqueImagen(PokeballBatalla pokeball)=>pokeball.Sprite;
        public static implicit operator Bitmap(PokeballBatalla pokeball) => pokeball.Sprite[0];
        public static int GetTotal(RomGba rom,OffsetRom offsetSpritePokeball=default,OffsetRom offsetPaletaPokeball=default)
        {
            int total = 0;
            int offsetSprite = Equals(offsetSpritePokeball,default)?GetOffsetSprite(rom):offsetSpritePokeball;
            int offsetPaleta = Equals(offsetPaletaPokeball, default) ? GetOffsetPaleta(rom) : offsetPaletaPokeball;
            while (BloqueImagen.IsHeaderOk(rom, offsetSprite) && Paleta.IsHeaderOk(rom, offsetPaleta))
            {
                total++;
                offsetPaleta += Paleta.LENGTHHEADERCOMPLETO;
                offsetSprite += BloqueImagen.LENGTHHEADERCOMPLETO;
            }
            return total;
        }

        public static OffsetRom GetOffsetSprite(RomGba rom)
        {
            return new OffsetRom(rom, GetZonaSprite(rom));
        }
        public static OffsetRom GetOffsetPaleta(RomGba rom)
        {
            return new OffsetRom(rom, GetZonaPaleta(rom));
        }

        public static Zona GetZonaSprite(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmoSprite, IndexRelativoSprite);
        }
        public static int GetZonaPaleta(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmoPaleta, IndexRelativoPaleta);
        }

        public static PokeballBatalla Get(RomGba rom, int index, OffsetRom offsetSpritePokeball = default, OffsetRom offsetPaletaPokeball = default)
        {
            int offsetSprite;
            int offsetPaleta;
            PokeballBatalla pokeball;

            offsetSpritePokeball = Equals(offsetSpritePokeball, default) ? GetOffsetSprite(rom) : offsetSpritePokeball;
            offsetPaletaPokeball = Equals(offsetPaletaPokeball, default) ? GetOffsetPaleta(rom) : offsetPaletaPokeball;

            offsetSprite = offsetSpritePokeball + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            offsetPaleta = offsetPaletaPokeball + index * Paleta.LENGTHHEADERCOMPLETO;
            pokeball = new PokeballBatalla();

            pokeball.Sprite = BloqueImagen.GetBloqueImagen(rom, offsetSprite);
            pokeball.Sprite.Paletas.Add(Paleta.GetPaleta(rom, offsetPaleta));
            return pokeball;
        }

        public static PokeballBatalla[] Get(RomGba rom, OffsetRom offsetSpritePokeball = default, OffsetRom offsetPaletaPokeball = default,int totalPokeballs=-1)
        {
            PokeballBatalla[] pokeballs = new PokeballBatalla[totalPokeballs<0?GetTotal(rom,offsetSpritePokeball,offsetPaletaPokeball):totalPokeballs];
            offsetSpritePokeball = Equals(offsetSpritePokeball, default) ? GetOffsetSprite(rom) : offsetSpritePokeball;
            offsetPaletaPokeball = Equals(offsetPaletaPokeball, default) ? GetOffsetPaleta(rom) : offsetPaletaPokeball;
            for (int i = 0; i < pokeballs.Length; i++)
                pokeballs[i] = Get(rom, i,offsetSpritePokeball,offsetPaletaPokeball);
            return pokeballs;
        }



    }
}