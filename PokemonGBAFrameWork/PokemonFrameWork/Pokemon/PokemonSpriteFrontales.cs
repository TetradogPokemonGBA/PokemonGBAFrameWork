using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class Frontales:PokemonFrameWorkItem
    {
        public const byte ID = 0x24;
        private static readonly Paleta PaletaAnimacion;
        public static readonly Zona ZonaImgFrontal;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Frontales>();
        Llista<BloqueImagen> sprites;
        static Frontales()
        {
            ZonaImgFrontal = new Zona("Imagen Frontal Pokemon");
            ZonaImgFrontal.Add(0xD324, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaImgFrontal.Add(0x128, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10);
            PaletaAnimacion = new Paleta();

            for (int i = 1; i < Paleta.LENGTH; i++)
            {

                PaletaAnimacion.Colores[i] = System.Drawing.Color.Black;
            }

            PaletaAnimacion.Colores[0] = System.Drawing.Color.White;

        }
        public Frontales()
        {
            sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites
        {
            get
            {
                return sprites;
            }
        }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public BitmapAnimated GetAnimacionImagenFrontal(Paleta paleta)
        {
            Bitmap[] gifAnimated = new Bitmap[sprites.Count + 2];
            int[] delay = new int[gifAnimated.Length];

            BitmapAnimated bmpAnimated;
            gifAnimated[1] = sprites[0] + PaletaAnimacion;
            delay[1] = 200;
            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = sprites[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[0] = gifAnimated[2];
            bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);

            return bmpAnimated;
        }
        public static Frontales GetFrontales(RomGba rom,int posicion)
        {
            byte[] auxImg;
            Frontales frontales = new Frontales();
            int offsetImgFrontalPokemon = Zona.GetOffsetRom(ZonaImgFrontal, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgFrontal = BloqueImagen.GetBloqueImagen(rom, offsetImgFrontalPokemon);
            auxImg = bloqueImgFrontal.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                frontales.sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }
            frontales.IdFuente = EdicionPokemon.IDMINRESERVADO;

            if (!((EdicionPokemon)rom.Edicion).EsEsmeralda)
                frontales.IdFuente -= (int)AbreviacionCanon.BPE;
            frontales.IdElemento = (ushort)posicion;
            return frontales;
        }
        public static Frontales[] GetFrontales(RomGba rom)
        {
            Frontales[] frontales = new Frontales[Huella.GetTotal(rom)];
            for (int i = 0; i < frontales.Length; i++)
                frontales[i] = GetFrontales(rom, i);
            return frontales;
        }
        public static void SetFrontales(RomGba rom,int posicion,Frontales frontales)
        {
            byte[] auxImg;
            BloqueImagen bloqueCompleto;

            int offsetImgFrontalPokemon = Zona.GetOffsetRom(ZonaImgFrontal, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;

            auxImg = new byte[frontales.Sprites.Count *SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA];
            for (int i = 0, pos = 0; i < frontales.Sprites.Count; i++, pos +=SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                auxImg.SetArray(pos, frontales.Sprites[i].DatosDescomprimidos.Bytes);
            }

            bloqueCompleto = new BloqueImagen(new BloqueBytes(auxImg));
            bloqueCompleto.Id = (short)posicion;
            bloqueCompleto.Offset = new OffsetRom(rom, offsetImgFrontalPokemon).Offset;
            //pongo las nuevas imagenes
            BloqueImagen.SetBloqueImagen(rom, offsetImgFrontalPokemon, bloqueCompleto);

        }
        public static void SetFrontales(RomGba rom,IList<Frontales> frontales)
        {            //borro las imagenes
            int total = Huella.GetTotal(rom);
            int offsetImgFrontalPokemon = Zona.GetOffsetRom(ZonaImgFrontal, rom).Offset;
            for (int i = 0; i < total; i++)
            {
                try
                {
                    BloqueImagen.Remove(rom, offsetImgFrontalPokemon);
                }
                catch { }
                rom.Data.Remove(offsetImgFrontalPokemon, BloqueImagen.LENGTHHEADERCOMPLETO);

                offsetImgFrontalPokemon += BloqueImagen.LENGTHHEADERCOMPLETO;
            }
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaImgFrontal, rom), rom.Data.SearchEmptyBytes(frontales.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
            //pongo los datos
            for (int i = 0; i < frontales.Count; i++)
                SetFrontales(rom, i, frontales[i]);
        }


    }
}
