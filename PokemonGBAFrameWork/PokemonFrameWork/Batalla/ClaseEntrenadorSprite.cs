using System;
using System.Collections.Generic;
using System.Text;
using PokemonGBAFrameWork;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class Sprite
    {
        public static readonly Zona ZonaImgSprite;
        public static readonly Zona ZonaPaletaSprite;

        public BloqueImagen Imagen { get; set; }

        static Sprite()
        {
            ZonaImgSprite = new Zona("Sprite Entrenador Img");
            ZonaPaletaSprite = new Zona("Sprite Entrenador Paleta");
            //pongo las zonas :D
            //img
            ZonaImgSprite.Add(0x34628, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaImgSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3473C, 0x34750);
            ZonaImgSprite.Add(EdicionPokemon.VerdeHojaUsa, 0x3473C, 0x34750);

            ZonaImgSprite.Add(0x31ADC, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaImgSprite.Add(0x31CA8, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

            ZonaImgSprite.Add(0x5DF78, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);


            //paletas
            ZonaPaletaSprite.Add(0x34638, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaPaletaSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3474C, 0x34760);
            ZonaPaletaSprite.Add(EdicionPokemon.VerdeHojaUsa, 0x3474C, 0x34760);

            ZonaPaletaSprite.Add(0x31AF0, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaPaletaSprite.Add(0x31CBC, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

            ZonaPaletaSprite.Add(0x5B784, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

        }

        public static Sprite GetSprite(RomGba rom, int index)
        {
            int offsetSpriteImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
            int offsetSpritePaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset + index * Paleta.LENGTHHEADERCOMPLETO;
            Sprite sprite = new Sprite();
            sprite.Imagen = BloqueImagen.GetBloqueImagen(rom, offsetSpriteImg);
            sprite.Imagen.Paletas.Add(Paleta.GetPaleta(rom, offsetSpritePaleta));
            return sprite;
        }
        public static void SetSprite(RomGba rom, int index, Sprite sprite)
        {

            int offsetInicioImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset + index * OffsetRom.LENGTH;
            int offsetInicioPaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset + index * OffsetRom.LENGTH;

            //pongo los datos

            BloqueImagen.SetBloqueImagen(rom, offsetInicioImg, sprite.Imagen);
            Paleta.SetPaleta(rom, offsetInicioPaleta, sprite.Imagen.Paletas[0]);
        }
        public static void SetSprite(RomGba rom, IList<Sprite> sprites)
        {
            OffsetRom offsetInicioImg;
            OffsetRom offsetInicioPaleta;
            int offsetActualImg;
            int offsetActualPaleta;
            int totalActual = GatTotal(rom);
            offsetInicioImg = Zona.GetOffsetRom(ZonaImgSprite, rom);
            offsetActualImg = offsetInicioImg.Offset;
            offsetInicioPaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom);
            offsetActualPaleta = offsetInicioPaleta.Offset;
            for (int i = 0; i < totalActual; i++)
            {
                BloqueImagen.Remove(rom, offsetActualImg);
                offsetActualImg += BloqueImagen.LENGTHHEADERCOMPLETO;
                Paleta.Remove(rom, offsetActualPaleta);
                offsetActualPaleta += Paleta.LENGTHHEADERCOMPLETO;

            }
            OffsetRom.SetOffset(rom, offsetInicioImg, rom.Data.SearchEmptyBytes(sprites.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
            OffsetRom.SetOffset(rom, offsetInicioPaleta, rom.Data.SearchEmptyBytes(sprites.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
            //pongo los sprites
            for (int i = 0; i < sprites.Count; i++)
                SetSprite(rom, i, sprites[i]);
        }
        public static int GatTotal(RomGba rom)
        {
            int offsetTablaEntrenadorImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset;
            int offsetTablaEntrenadorPaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset;
            int imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
            int numero = 0;
            while (BloqueImagen.IsHeaderOk(rom, imgActual) && Paleta.IsHeaderOk(rom, paletaActual))
            {
                numero++;
                imgActual += BloqueImagen.LENGTHHEADERCOMPLETO;
                paletaActual += Paleta.LENGTHHEADERCOMPLETO;
            }
            return numero;
        }
    }
}
