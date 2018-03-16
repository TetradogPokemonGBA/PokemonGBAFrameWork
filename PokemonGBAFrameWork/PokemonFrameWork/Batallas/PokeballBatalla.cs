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
        public static int GetTotal(RomData rom)
        {
            return GetTotal(rom.Rom, rom.Edicion, rom.Compilacion);
        }
        public static int GetTotal(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
        {
            int total = 0;
            int offsetSprite = Zona.GetOffsetRom(rom, ZonaSpritePokeballBatalla, edicion, compilacion).Offset;
            int offsetPaleta = Zona.GetOffsetRom(rom, ZonaPaletaPokeballBatalla, edicion, compilacion).Offset;
            while (BloqueImagen.IsHeaderOk(rom, offsetSprite) && Paleta.IsHeaderOk(rom, offsetPaleta))
            {
                total++;
                offsetPaleta += Paleta.LENGTHHEADERCOMPLETO;
                offsetSprite += BloqueImagen.LENGTHHEADERCOMPLETO;
            }
            return total;
        }

        public static PokeballBatalla GetPokeballBatalla(RomData rom, int index)
        {
            return GetPokeballBatalla(rom.Rom, rom.Edicion, rom.Compilacion, index);
        }
        public static PokeballBatalla GetPokeballBatalla(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int index)
        {
            int offsetSprite = Zona.GetOffsetRom(rom, ZonaSpritePokeballBatalla, edicion, compilacion).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetPaleta = Zona.GetOffsetRom(rom, ZonaPaletaPokeballBatalla, edicion, compilacion).Offset + index * Paleta.LENGTHHEADERCOMPLETO;
            PokeballBatalla pokeball = new PokeballBatalla();
            pokeball.Sprite = BloqueImagen.GetBloqueImagen(rom, offsetSprite);
            pokeball.Sprite.Paletas.Add(Paleta.GetPaleta(rom, offsetPaleta));
            return pokeball;
        }
        public static PokeballBatalla[] GetPokeballsBatalla(RomData rom)
        {
            return GetPokeballsBatalla(rom.Rom, rom.Edicion, rom.Compilacion);
        }
        public static PokeballBatalla[] GetPokeballsBatalla(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
        {
            PokeballBatalla[] pokeballs = new PokeballBatalla[GetTotal(rom, edicion, compilacion)];
            for (int i = 0; i < pokeballs.Length; i++)
                pokeballs[i] = GetPokeballBatalla(rom, edicion, compilacion, i);
            return pokeballs;
        }

        public static void SetPokeballBatalla(RomData rom, int index, PokeballBatalla pokeball)
        {
            SetPokeballBatalla(rom.Rom, rom.Edicion, rom.Compilacion, index, pokeball);
        }
        public static void SetPokeballBatalla(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int index, PokeballBatalla pokeball)
        {
            int offsetSprite = Zona.GetOffsetRom(rom, ZonaSpritePokeballBatalla, edicion, compilacion).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetPaleta = Zona.GetOffsetRom(rom, ZonaPaletaPokeballBatalla, edicion, compilacion).Offset + index * Paleta.LENGTHHEADERCOMPLETO;

            BloqueImagen.SetBloqueImagen(rom, offsetSprite, pokeball.Sprite);
            Paleta.SetPaleta(rom, offsetPaleta, pokeball.Sprite.Paletas[0]);

        }
        public static void SetPokeballsBatalla(RomData rom)
        {
            SetPokeballsBatalla(rom.Rom, rom.Edicion, rom.Compilacion, rom.PokeballsBatalla);
        }
        public static void SetPokeballsBatalla(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, IList<PokeballBatalla> pokeballs)
        {
            OffsetRom offsetSprite;
            OffsetRom offsetPaleta;
            int offsetSpriteActual;
            int offsetPaletaActual;
            int totalActual = GetTotal(rom, edicion, compilacion);
            if (totalActual != pokeballs.Count)
            {
                offsetSprite = Zona.GetOffsetRom(rom, ZonaSpritePokeballBatalla, edicion, compilacion);
                offsetPaleta = Zona.GetOffsetRom(rom, ZonaPaletaPokeballBatalla, edicion, compilacion);
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
                SetPokeballBatalla(rom, edicion, compilacion, i, pokeballs[i]);

        }


    }
}
