using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Es una clase para trabajar con las imagenes Frontales y Traseras de los pokemon
    /// </summary>
    public class Sprite
    {
        public enum Variables
        {
            SpriteFrontal,
            SpriteTrasero,
            PaletaNormal,
            PaletaShiny
        }
        public const int TAMAÑOIMAGEN = 2048;
        protected static readonly Color[] paletaAnimacion;

        protected int NORMAL = 0, SHINY = 1;
        Paleta paletaNormal;
        Paleta paletaShiny;
        BloqueImagen[] imagenFrontal;
        BloqueImagen[] imagenTrasera;
        static Sprite()
        {
            Zona zonaSpriteFrontal = new Zona(Variables.SpriteFrontal);
            Zona zonaSpriteTrasero = new Zona(Variables.SpriteTrasero);
            Zona zonaPaletaNormal = new Zona(Variables.PaletaNormal);
            Zona zonaPaletaShiny = new Zona(Variables.PaletaShiny);


            //añadiendo rubi y zafiro usa
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xD324);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xD324);

            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xD3D8);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xD3D8);

            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x40954, 0x40974, 0x40974);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x40954, 0x40974, 0x40974);

            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x40954, 0x40974, 0x40974);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x4098C, 0x409AC, 0x409AC);
            //los demás usan los mismos :) usa esp

            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x128);
            zonaSpriteFrontal.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x128);

            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x12C);
            zonaSpriteTrasero.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x12C);

            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x130);
            zonaPaletaNormal.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x130);

            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x134);
            zonaPaletaShiny.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x134);

            Zona.DiccionarioOffsetsZonas.AddRange(new Zona[] {
                zonaPaletaNormal,
                zonaPaletaShiny,
                zonaSpriteFrontal,
                zonaSpriteTrasero
            });
            //parte animacion

            paletaAnimacion = new Color[Paleta.TAMAÑOPALETA];

            for (int i = 1; i < Paleta.TAMAÑOPALETA; i++)
            {

                paletaAnimacion[i] = Color.Black;
            }

            paletaAnimacion[0] = Color.White;
        }
        //no se usan los bloques de imagen para que las paletas esten controladas y sean estas
        public Sprite(Paleta paletaNormal, Paleta paletaShiny, Hex offsetImagenFrontal, Bitmap imagenFrontal, Hex offsetImagenTrasera, Bitmap imagenTrasera)
        {
            this.paletaNormal = paletaNormal;
            this.paletaShiny = paletaShiny;
            this.imagenFrontal = new BloqueImagen[] { new BloqueImagen(offsetImagenFrontal, imagenFrontal, paletaNormal, paletaShiny) };
            this.imagenTrasera = new BloqueImagen[] { new BloqueImagen(offsetImagenTrasera, imagenTrasera, paletaNormal, paletaShiny) };

        }
        public Sprite(RomGBA rom, Hex offsetImagenFrontal, Hex offsetImagenTrasera, Hex offsetPaletaNormal, Hex offsetPaletaShiny)
        {
            byte[] bytesImg;
            this.paletaNormal = Paleta.GetPaleta(rom, offsetPaletaNormal);
            this.paletaShiny = Paleta.GetPaleta(rom, offsetPaletaShiny);
            bytesImg = BloqueImagen.GetBloqueImagen(rom, offsetImagenFrontal, paletaNormal, paletaShiny).DatosDescomprimidos;
            this.imagenFrontal = new BloqueImagen[bytesImg.Length / TAMAÑOIMAGEN];
            for (int i = 0, l = 0; i < imagenFrontal.Length; i++, l += TAMAÑOIMAGEN)
            {
                this.imagenFrontal[i] = new BloqueImagen(offsetImagenFrontal, bytesImg.SubArray(l, TAMAÑOIMAGEN), paletaNormal, paletaShiny);
            }
            bytesImg = BloqueImagen.GetBloqueImagen(rom, offsetImagenTrasera, paletaNormal, paletaShiny).DatosDescomprimidos;
            this.imagenTrasera = new BloqueImagen[bytesImg.Length / TAMAÑOIMAGEN];
            for (int i = 0, l = 0; i < imagenTrasera.Length; i++, l += TAMAÑOIMAGEN)
            {
                this.imagenTrasera[i] = new BloqueImagen(offsetImagenTrasera, bytesImg.SubArray(l, TAMAÑOIMAGEN), paletaNormal, paletaShiny);
            }

        }

        public Paleta PaletaNormal
        {
            get
            {
                return paletaNormal;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                Paleta.ReemplazaColores(paletaNormal, value);
            }
        }

        public Paleta PaletaShiny
        {
            get
            {
                return paletaShiny;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                Paleta.ReemplazaColores(paletaShiny, value);
            }
        }
        public Hex OffsetImagenFrontal
        {
            get { return imagenFrontal[0].OffsetInicio; }
            set { imagenFrontal[0].OffsetInicio = value; }
        }
        public Bitmap ImagenFrontalNormal
        {
            get
            {
                return imagenFrontal[0][NORMAL];
            }

            set
            {
                imagenFrontal[0] = new BloqueImagen(imagenFrontal[0].OffsetInicio, value, imagenFrontal[0].Paletas.ToArray());
            }
        }
        public Bitmap ImagenFrontalShiny
        {
            get
            {
                return imagenFrontal[0][SHINY];
            }

            set
            {
                imagenFrontal[0] = new BloqueImagen(imagenFrontal[0].OffsetInicio, value, imagenFrontal[0].Paletas.ToArray());
            }
        }
        public Hex OffsetImagenTrasera
        {
            get { return imagenTrasera[0].OffsetInicio; }
            set { imagenTrasera[0].OffsetInicio = value; }
        }
        public Bitmap ImagenTraseraNormal
        {
            get
            {
                return imagenTrasera[0][NORMAL];
            }

            set
            {
                imagenTrasera[0] = new BloqueImagen(imagenTrasera[0].OffsetInicio, value, imagenTrasera[0].Paletas.ToArray());
            }
        }
        public Bitmap ImagenTraseraShiny
        {
            get
            {
                return imagenTrasera[0][SHINY];
            }

            set
            {
                imagenTrasera[0] = new BloqueImagen(imagenTrasera[0].OffsetInicio, value, imagenTrasera[0].Paletas.ToArray());
            }
        }

        public BloqueImagen[] ImagenFrontal
        {
            get
            {
                return imagenFrontal;
            }

            protected set
            {
                imagenFrontal = value;
            }
        }

        public BloqueImagen[] ImagenTrasera
        {
            get
            {
                return imagenTrasera;
            }

            protected set
            {
                imagenTrasera = value;
            }
        }



        public void SetSprite(RomGBA rom)
        {
            SetSprite(rom, this);
        }
        public Bitmap GetImagenFrontal(int index = 0)
        {
            return imagenFrontal[index];
        }
        public Bitmap GetImagenTrasera(int index = 0)
        {
            return imagenTrasera[index];
        }
        public Bitmap GetCustomImagenFrontal(Paleta colors, int index = 0)
        {
            return BloqueImagen.BuildBitmap(imagenFrontal[index].DatosDescomprimidos, colors);
        }

        public Bitmap GetCustomImagenTrasera(Paleta colors, int index = 0)
        {
            return BloqueImagen.BuildBitmap(imagenTrasera[index].DatosDescomprimidos, colors);
        }
        public static void SetSprite(RomGBA rom, Sprite sprite)
        {
            byte[] bytesImg = new byte[0];
            //pongo la primeraImg
            bytesImg = bytesImg.AddArray(GetArrays(sprite.ImagenFrontal));
            BloqueImagen.SetBloqueImagen(rom, sprite.ImagenFrontal[0].OffsetInicio, bytesImg);
            bytesImg = new byte[0];
            //pongo la segundaImg
            bytesImg = bytesImg.AddArray(GetArrays(sprite.ImagenTrasera));
            BloqueImagen.SetBloqueImagen(rom, sprite.ImagenTrasera[0].OffsetInicio, bytesImg);
            Paleta.SetPaleta(rom, sprite.PaletaNormal);
            Paleta.SetPaleta(rom, sprite.PaletaShiny);
        }

        private static byte[][] GetArrays(BloqueImagen[] imgs)
        {
            List<byte[]> arrays = new List<byte[]>();
            for (int i = 0; i < imgs.Length; i++)
                arrays.Add(imgs[i].DatosDescomprimidos);
            return arrays.ToArray();
        }

        public static Sprite GetSprite(RomGBA rom, Hex posicion)
        {
            return GetSprite(rom, Edicion.GetEdicion(rom), posicion);
        }
        public static Sprite GetSprite(RomGBA rom, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            return GetSprite(rom, Edicion.GetEdicion(rom), compilacion, posicion);
        }



        public static Sprite GetSprite(RomGBA rom, Edicion edicion, Hex posicion)
        {
            return GetSprite(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), posicion);
        }
        public static Sprite GetSprite(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            Sprite sprite;
            Hex offsetSpriteFrontal = BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.SpriteFrontal, edicion, compilacion), posicion);
            Hex offsetSpriteTrasero = BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.SpriteTrasero, edicion, compilacion), posicion);
            Hex offsetPaletaNormal = BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.PaletaNormal, edicion, compilacion), posicion);//al parecer no guardo bien la direccion de las paletas...y peta...
            Hex offsetPaletaShiny = BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.PaletaShiny, edicion, compilacion), posicion);
            try
            {
                sprite = new Sprite(rom, offsetSpriteFrontal, offsetSpriteTrasero, offsetPaletaNormal, offsetPaletaShiny);
            }
            catch
            {
                throw new InvalidRomFormat();/*si los offsets leidos no estan bien es que hay un problema*/
            }
            return sprite;
        }
        public virtual BitmapAnimated GetAnimacionImagenFrontal(bool isShiny = false)
        {
            Bitmap[] gifAnimated = new Bitmap[imagenFrontal.Length + 2];
            int[] delay = new int[gifAnimated.Length];
            Paleta paleta = isShiny ? PaletaShiny : PaletaNormal;
            gifAnimated[1] = GetCustomImagenFrontal(paletaAnimacion);
            delay[1] = 200;
            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = imagenFrontal[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[0] = gifAnimated[2];
            BitmapAnimated bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);
            return bmpAnimated;
        }
    }
}
