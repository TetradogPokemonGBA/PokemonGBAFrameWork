using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Gabriel.Cat.S.Binaris;
using PokemonGBAFrameWork;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class Sprite:IElementoBinarioComplejo
    {
        public class Data:IElementoBinarioComplejo
        {
            public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(Data));
            public static readonly Zona ZonaImgSprite;

            static Data()
            {
                ZonaImgSprite = new Zona("Sprite Entrenador Img");

                //pongo las zonas :D
                //img
                ZonaImgSprite.Add(0x34628, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
                ZonaImgSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3473C, 0x34750);
                ZonaImgSprite.Add(EdicionPokemon.VerdeHojaUsa, 0x3473C, 0x34750);

                ZonaImgSprite.Add(0x31ADC, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
                ZonaImgSprite.Add(0x31CA8, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

                ZonaImgSprite.Add(0x5DF78, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

            }

            public BloqueImagen Img { get; set; }

            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

            public static Data GetData(RomGba rom, int index)
            {
                int offsetSpriteImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset + index * BloqueImagen.LENGTHHEADERCOMPLETO;
                return new Data() { Img = BloqueImagen.GetBloqueImagen(rom, offsetSpriteImg) };
            }
            public static void SetData(RomGba rom, int index, Data data)
            {
                int offsetInicioImg = Zona.GetOffsetRom(ZonaImgSprite, rom).Offset + index * OffsetRom.LENGTH;
                BloqueImagen.SetBloqueImagen(rom, offsetInicioImg, data.Img);
            }
            public static void SetData(RomGba rom,IList<Data> datas)
            {
                OffsetRom offsetInicioImg;
                int offsetActualImg;
                int totalActual = GetTotal(rom);
                offsetInicioImg = Zona.GetOffsetRom(Data.ZonaImgSprite, rom);
                offsetActualImg = offsetInicioImg.Offset;
                for (int i = 0; i < totalActual; i++)
                {
                    BloqueImagen.Remove(rom, offsetActualImg);
                    offsetActualImg += BloqueImagen.LENGTHHEADERCOMPLETO;
                }
                OffsetRom.SetOffset(rom, offsetInicioImg, rom.Data.SearchEmptyBytes(datas.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
                for (int i = 0; i < datas.Count; i++)
                    SetData(rom, i, datas[i]);
            }
        }
        public class Paleta:IElementoBinarioComplejo
        {
            public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(Paleta));
            public static readonly Zona ZonaPaletaSprite;

            public PokemonGBAFrameWork.Paleta Colores { get; set; }

            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

            static Paleta()
            {

                ZonaPaletaSprite = new Zona("Sprite Entrenador Paleta");
                //pongo las zonas :D

                //paletas
                ZonaPaletaSprite.Add(0x34638, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
                ZonaPaletaSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3474C, 0x34760);
                ZonaPaletaSprite.Add(EdicionPokemon.VerdeHojaUsa, 0x3474C, 0x34760);

                ZonaPaletaSprite.Add(0x31AF0, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
                ZonaPaletaSprite.Add(0x31CBC, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

                ZonaPaletaSprite.Add(0x5B784, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

            }

            public static Paleta GetPaleta(RomGba rom,int index)
            {
                Paleta paleta = new Paleta();
                int offsetSpritePaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset + index * PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO;
                return Paleta.GetPaleta(rom, offsetSpritePaleta);
            }
            public static void SetPaleta(RomGba rom,int index,Paleta paleta)
            {

                int offsetInicioPaleta = Zona.GetOffsetRom(ZonaPaletaSprite, rom).Offset + index * OffsetRom.LENGTH;
                //pongo la paleta
                PokemonGBAFrameWork.Paleta.SetPaleta(rom, offsetInicioPaleta,paleta.Colores);
            }
            public static void SetPaleta(RomGba rom,IList<Paleta> paletas)
            {

                OffsetRom offsetInicioPaleta;

                int offsetActualPaleta;
                int totalActual = GetTotal(rom);
                offsetInicioPaleta = Zona.GetOffsetRom(Paleta.ZonaPaletaSprite, rom);
                offsetActualPaleta = offsetInicioPaleta.Offset;
                for (int i = 0; i < totalActual; i++)
                {
                    PokemonGBAFrameWork.Paleta.Remove(rom, offsetActualPaleta);
                    offsetActualPaleta += PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO;

                }

                OffsetRom.SetOffset(rom, offsetInicioPaleta, rom.Data.SearchEmptyBytes(paletas.Count * PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO));
                for (int i = 0; i < paletas.Count; i++)
                    SetPaleta(rom, i, paletas[i]);
            }

        }

        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(Sprite));
        public Data DataImg { get; set; }
        public Paleta PaletaImg { get; set; }
        public Bitmap Imagen { get { return DataImg.Img + PaletaImg.Colores; } }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public static Sprite[] GetSprite(RomGba rom)
        {
            Sprite[] sprites = new Sprite[GetTotal(rom)];
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = GetSprite(rom, i);
            return sprites;
        }
        public static Sprite GetSprite(RomGba rom, int index)
        {

           
            Sprite sprite = new Sprite();
            sprite.DataImg = Data.GetData(rom, index);
            sprite.PaletaImg = Paleta.GetPaleta(rom, index);

            return sprite;
        }
        public static void SetSprite(RomGba rom, int index, Sprite sprite)
        {
            Data.SetData(rom, index, sprite.DataImg);
            Paleta.SetPaleta(rom, index, sprite.PaletaImg);

        }
        public static void SetSprite(RomGba rom, IList<Sprite> sprites)
        {
            List<Data> datas = new List<Data>();
            List<Paleta> paletas = new List<Paleta>();

            //pongo los sprites
            for (int i = 0; i < sprites.Count; i++)
            {
                datas.Add(sprites[i].DataImg);
                paletas.Add(sprites[i].PaletaImg);
            }
            Data.SetData(rom, datas);
            Paleta.SetPaleta(rom, paletas);
        }
        public static int GetTotal(RomGba rom)
        {
            int offsetTablaEntrenadorImg = Zona.GetOffsetRom(Data.ZonaImgSprite, rom).Offset;
            int offsetTablaEntrenadorPaleta = Zona.GetOffsetRom(Paleta.ZonaPaletaSprite, rom).Offset;
            int imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
            int numero = 0;
            while (BloqueImagen.IsHeaderOk(rom, imgActual) &&PokemonGBAFrameWork.Paleta.IsHeaderOk(rom, paletaActual))
            {
                numero++;
                imgActual += BloqueImagen.LENGTHHEADERCOMPLETO;
                paletaActual += PokemonGBAFrameWork.Paleta.LENGTHHEADERCOMPLETO;
            }
            return numero;
        }
    }
}
