using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class PokeballBatalla:PokemonFrameWorkItem
    {
        public const byte ID = 0xB;
        public static readonly Zona ZonaPaletaPokeballBatalla;
        public static readonly Zona ZonaSpritePokeballBatalla;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PokeballBatalla>();

        static PokeballBatalla()
        {
            ZonaSpritePokeballBatalla = new Zona("Zona sprite pokeball batalla");
            ZonaPaletaPokeballBatalla = new Zona("Zona paleta pokeball batalla");

            ZonaSpritePokeballBatalla.Add(0x1D0, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.EsmeraldaUsa10);
            ZonaSpritePokeballBatalla.Add(0x477E0, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

            ZonaSpritePokeballBatalla.Add(EdicionPokemon.RubiUsa10, 0x473BC, 0x473DC);
            ZonaSpritePokeballBatalla.Add(EdicionPokemon.ZafiroUsa10, 0x473BC, 0x473DC);


            ZonaPaletaPokeballBatalla.Add(0x1D4, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.EsmeraldaUsa10);
            ZonaPaletaPokeballBatalla.Add(0x477DC, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

            ZonaPaletaPokeballBatalla.Add(EdicionPokemon.RubiUsa10, 0x473C0, 0x473E0);
            ZonaPaletaPokeballBatalla.Add(EdicionPokemon.ZafiroUsa10, 0x473C0, 0x473E0);

        }
        public PokeballBatalla()
        {
            Sprite = new BloqueImagen();
        }

        public BloqueImagen Sprite { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

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
            pokeball.IdElemento = (ushort)index;
            pokeball.IdFuente = EdicionPokemon.IDMINRESERVADO;//en todas las roms hay las mismas pokeballs
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
