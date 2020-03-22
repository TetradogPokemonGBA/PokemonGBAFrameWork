using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class PokeballBatalla
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

        public static PokemonGBAFramework.Batalla.Pokeball GetPokeballBatalla(RomGba rom,  int index)
        {
            int offsetSprite = Zona.GetOffsetRom( ZonaSpritePokeballBatalla,rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetPaleta = Zona.GetOffsetRom(ZonaPaletaPokeballBatalla,rom).Offset + index * Paleta.LENGTHHEADERCOMPLETO;
            PokeballBatalla pokeball = new PokeballBatalla();
            pokeball.Sprite = BloqueImagen.GetBloqueImagen(rom, offsetSprite);
            pokeball.Sprite.Paletas.Add(Paleta.GetPaleta(rom, offsetPaleta));
            return new PokemonGBAFramework.Batalla.Pokeball() { Imagen = pokeball.Sprite.GetImg() };
        }

        public static PokemonGBAFramework.Paquete GetPokeballBatalla(RomGba rom)
        {
            return rom.GetPaquete("Pokeballs Batalla",(r,i)=>GetPokeballBatalla(r,i),GetTotal(rom));
        }


    
    }
}
