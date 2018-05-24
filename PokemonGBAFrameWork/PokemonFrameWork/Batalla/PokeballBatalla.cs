using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class PokeballBatalla
    {
        public static readonly Zona ZonaPaletaPokeballBatalla;
        public static readonly Zona ZonaSpritePokeballBatalla;
        BloqueImagen blSprite;
        static PokeballBatalla()
        {
            ZonaSpritePokeballBatalla = new Zona("Zona sprite pokeball batalla");
            ZonaPaletaPokeballBatalla = new Zona("Zona paleta pokeball batalla");

            ZonaSpritePokeballBatalla.Add(0x1D0, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.EsmeraldaUsa);
            ZonaSpritePokeballBatalla.Add(0x477E0, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

            ZonaSpritePokeballBatalla.Add(EdicionPokemon.RubiUsa, 0x473BC, 0x473DC);
            ZonaSpritePokeballBatalla.Add(EdicionPokemon.ZafiroUsa, 0x473BC, 0x473DC);


            ZonaPaletaPokeballBatalla.Add(0x1D4, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.EsmeraldaUsa);
            ZonaPaletaPokeballBatalla.Add(0x477DC, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

            ZonaPaletaPokeballBatalla.Add(EdicionPokemon.RubiUsa, 0x473C0, 0x473E0);
            ZonaPaletaPokeballBatalla.Add(EdicionPokemon.ZafiroUsa, 0x473C0, 0x473E0);

        }
        public PokeballBatalla()
        {
            blSprite = new BloqueImagen();
        }

        public BloqueImagen Sprite
        {
            get
            {
                return blSprite;
            }
            set { blSprite = value; }
        }

        public static int GetTotal(RomGba rom)
        {
            int total = 0;
            int offsetSprite = Zona.GetOffsetRom(ZonaSpritePokeballBatalla,rom).Offset;
            int offsetPaleta = Zona.GetOffsetRom(ZonaPaletaPokeballBatalla,rom).Offset;
            while (BloqueImagen.IsHeaderOk(rom, offsetSprite) && Paleta.IsHeaderOk(rom, offsetPaleta))
            {
                total++;
                offsetPaleta += Paleta.LENGTHHEADERCOMPLETO;
                offsetSprite += BloqueImagen.LENGTHHEADERCOMPLETO;
            }
            return total;
        }

        public static PokeballBatalla GetPokeballBatalla(RomGba rom,  int index)
        {
            int offsetSprite = Zona.GetOffsetRom( ZonaSpritePokeballBatalla,rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetPaleta = Zona.GetOffsetRom(ZonaPaletaPokeballBatalla,rom).Offset + index * Paleta.LENGTHHEADERCOMPLETO;
            PokeballBatalla pokeball = new PokeballBatalla();
            pokeball.Sprite = BloqueImagen.GetBloqueImagen(rom, offsetSprite);
            pokeball.Sprite.Paletas.Add(Paleta.GetPaleta(rom, offsetPaleta));
            return pokeball;
        }

        public static PokeballBatalla[] GetPokeballBatalla(RomGba rom)
        {
            PokeballBatalla[] pokeballs = new PokeballBatalla[GetTotal(rom)];
            for (int i = 0; i < pokeballs.Length; i++)
                pokeballs[i] = GetPokeballBatalla(rom,  i);
            return pokeballs;
        }


        public static void SetPokeballBatalla(RomGba rom,int index, PokeballBatalla pokeball)
        {
            int offsetSprite = Zona.GetOffsetRom(ZonaSpritePokeballBatalla,rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetPaleta = Zona.GetOffsetRom(ZonaPaletaPokeballBatalla,rom).Offset + index * Paleta.LENGTHHEADERCOMPLETO;

            BloqueImagen.SetBloqueImagen(rom, offsetSprite, pokeball.Sprite);
            Paleta.SetPaleta(rom, offsetPaleta, pokeball.Sprite.Paletas[0]);

        }

        public static void SetPokeballBatalla(RomGba rom, IList<PokeballBatalla> pokeballs)
        {
            OffsetRom offsetSprite;
            OffsetRom offsetPaleta;
            int offsetSpriteActual;
            int offsetPaletaActual;
            int totalActual = GetTotal(rom);
            if (totalActual != pokeballs.Count)
            {
                offsetSprite = Zona.GetOffsetRom(ZonaSpritePokeballBatalla,rom);
                offsetPaleta = Zona.GetOffsetRom(ZonaPaletaPokeballBatalla,rom);
                offsetSpriteActual = offsetSprite.Offset;
                offsetPaletaActual = offsetPaleta.Offset;
                for (int i = 0; i < totalActual; i++)
                {
                    BloqueImagen.Remove(rom, offsetSpriteActual);
                    Paleta.Remove(rom, offsetPaletaActual);

                    offsetSpriteActual += BloqueImagen.LENGTHHEADERCOMPLETO;
                    offsetPaletaActual += Paleta.LENGTHHEADERCOMPLETO;

                }
                //borro los datos
                if (totalActual < pokeballs.Count)
                {
                    //reubico
                    OffsetRom.SetOffset(rom, offsetSprite, rom.Data.SearchEmptyBytes(pokeballs.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
                    OffsetRom.SetOffset(rom, offsetPaleta, rom.Data.SearchEmptyBytes(pokeballs.Count * Paleta.LENGTHHEADERCOMPLETO));

                }

            }
            for (int i = 0; i < pokeballs.Count; i++)
                SetPokeballBatalla(rom, i, pokeballs[i]);

        }


    }
}
