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
        internal const int TAMAÑOIMAGEN = 2048;
        protected static readonly Color[] paletaAnimacion1;
        protected static readonly Color[] paletaAnimacion2;
        protected int NORMAL = 0, SHINY = 1;
        BloqueImagen.Paleta paletaNormal;
        BloqueImagen.Paleta paletaShiny;
        BloqueImagen imagenFrontal;
        BloqueImagen imagenTrasera;
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

            Zona.DiccionarioOffsetsZonas.Añadir(new Zona[] {
                zonaPaletaNormal,
                zonaPaletaShiny,
                zonaSpriteFrontal,
                zonaSpriteTrasero
            });
            //parte animacion

            paletaAnimacion1 = new Color[BloqueImagen.Paleta.TAMAÑOPALETA];
            paletaAnimacion2 = new Color[BloqueImagen.Paleta.TAMAÑOPALETA];
            for (int i = 1; i < BloqueImagen.Paleta.TAMAÑOPALETA; i++)
            {
                paletaAnimacion1[i] = Color.White;
                paletaAnimacion2[i] = Color.Black;
            }
            paletaAnimacion1[0] = Color.White;
            paletaAnimacion2[0] = Color.White;
        }
        //no se usan los bloques de imagen para que las paletas esten controladas y sean estas
        public Sprite(BloqueImagen.Paleta paletaNormal, BloqueImagen.Paleta paletaShiny, Hex offsetImagenFrontal, Bitmap imagenFrontal, Hex offsetImagenTrasera, Bitmap imagenTrasera)
        {
            this.paletaNormal = paletaNormal;
            this.paletaShiny = paletaShiny;
            this.imagenFrontal = new BloqueImagen(offsetImagenFrontal, imagenFrontal, BloqueImagen.LongitudImagen.L64, paletaNormal, paletaShiny);
            this.imagenTrasera = new BloqueImagen(offsetImagenTrasera, imagenTrasera, BloqueImagen.LongitudImagen.L64, paletaNormal, paletaShiny);

        }
        public Sprite(RomGBA rom, Hex offsetImagenFrontal, Hex offsetImagenTrasera, Hex offsetPaletaNormal, Hex offsetPaletaShiny)
        {
            byte[] imgBytes;
            this.paletaNormal = BloqueImagen.Paleta.GetPaleta(rom, offsetPaletaNormal);
            this.paletaShiny = BloqueImagen.Paleta.GetPaleta(rom, offsetPaletaShiny);
            imgBytes = BloqueImagen.GetBloqueImagen(rom, offsetImagenFrontal,BloqueImagen.LongitudImagen.L64 ,paletaNormal).DatosImagenDescomprimida;
            this.imagenFrontal = new BloqueImagen(offsetImagenFrontal, imgBytes.Length > TAMAÑOIMAGEN ? imgBytes.SubArray(0,TAMAÑOIMAGEN) : imgBytes, BloqueImagen.LongitudImagen.L64, paletaNormal, paletaShiny);

            this.imagenTrasera = new BloqueImagen(offsetImagenTrasera, BloqueImagen.GetBloqueImagen(rom, offsetImagenTrasera, BloqueImagen.LongitudImagen.L64, paletaNormal).DatosImagenDescomprimida, BloqueImagen.LongitudImagen.L64, paletaNormal, paletaShiny);
        }

        public BloqueImagen.Paleta PaletaNormal
        {
            get
            {
                return paletaNormal;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                BloqueImagen.Paleta.ReemplazaColores(paletaNormal, value);
            }
        }

        public BloqueImagen.Paleta PaletaShiny
        {
            get
            {
                return paletaShiny;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                BloqueImagen.Paleta.ReemplazaColores(paletaShiny, value);
            }
        }
        public Hex OffsetImagenFrontal
        {
            get { return imagenFrontal.OffsetInicio; }
            set { imagenFrontal.OffsetInicio = value; }
        }
        public Bitmap ImagenFrontalNormal
        {
            get
            {
                return imagenFrontal[NORMAL];
            }

            set
            {
                imagenFrontal[NORMAL] = value;
            }
        }
        public Bitmap ImagenFrontalShiny
        {
            get
            {
                return imagenFrontal[SHINY];
            }

            set
            {
                imagenFrontal[SHINY] = value;
            }
        }
        public Hex OffsetImagenTrasera
        {
            get { return imagenTrasera.OffsetInicio; }
            set { imagenTrasera.OffsetInicio = value; }
        }
        public Bitmap ImagenTraseraNormal
        {
            get
            {
                return imagenTrasera[NORMAL];
            }

            set
            {
                imagenTrasera[NORMAL] = value;
            }
        }
        public Bitmap ImagenTraseraShiny
        {
            get
            {
                return imagenTrasera[SHINY];
            }

            set
            {
                imagenTrasera[SHINY] = value;
            }
        }

        internal BloqueImagen ImagenFrontal
        {
            get
            {
                return imagenFrontal;
            }

          private  set
            {
                imagenFrontal = value;
            }
        }

        internal BloqueImagen ImagenTrasera
        {
            get
            {
                return imagenTrasera;
            }

          private  set
            {
                imagenTrasera = value;
            }
        }

        public void SetSprite(RomGBA rom)
        {
            SetSprite(rom, this);
        }

        public Bitmap GetCustomImagenFrontal(Color[] colors)
        {
            if (colors.Length != BloqueImagen.Paleta.TAMAÑOPALETA)
                throw new ArgumentException("paleta incorrecta");

            return imagenFrontal.GetBitmap(colors);
        }

        public Bitmap GetCustomImagenTrasera(Color[] colors)
        {
            if (colors.Length != BloqueImagen.Paleta.TAMAÑOPALETA)
                throw new ArgumentException("paleta incorrecta");
            return imagenTrasera.GetBitmap(colors);
        }
        public static void SetSprite(RomGBA rom, Sprite sprite)
        {
            if (rom == null || sprite == null)
                throw new ArgumentNullException();
            SpriteEsmeralda spriteEsmeralda = sprite as SpriteEsmeralda;
            byte[] imgEsmeraldaArray;
            //guardo las paletas
            BloqueImagen.Paleta.SetPaleta(rom, sprite.PaletaNormal);
            BloqueImagen.Paleta.SetPaleta(rom, sprite.PaletaShiny);
            //guardo la imagen trasera
            BloqueImagen.SetBloqueImagen(rom, sprite.imagenTrasera, false);
            //guardo la imagen frontal
            if (spriteEsmeralda != null)
            {
                imgEsmeraldaArray = new byte[spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length * 2];
                unsafe
                {
                    fixed (byte* ptrImgEsmeralda = imgEsmeraldaArray)
                    {
                        fixed (byte* ptrImgParte1 = spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida)
                            for (int i = 0; i < spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length; i++)
                            ptrImgEsmeralda[i] = ptrImgParte1[i];
                        fixed (byte* ptrImgParte2 = spriteEsmeralda.imagenFrontal2.DatosImagenDescomprimida)
                            for (int i = spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length, j = 0; i < imgEsmeraldaArray.Length; i++, j++)
                            ptrImgEsmeralda[i] = ptrImgParte2[j];
                    }
                }
                BloqueImagen.SetBloqueImagen(rom, new BloqueImagen(sprite.OffsetImagenFrontal, imgEsmeraldaArray, spriteEsmeralda.PaletaNormal), false);

            }
            else
            {
                BloqueImagen.SetBloqueImagen(rom, sprite.imagenFrontal, false);
            }

        }
        public static Sprite GetSprite(RomGBA rom, Hex posicion)
        {
            return GetSprite(rom, Edicion.GetEdicion(rom), posicion);
        }
        public static Sprite GetSprite(RomGBA rom, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            return GetSprite(rom, Edicion.GetEdicion(rom), compilacion, posicion);
        }

        public virtual BitmapAnimated GetAnimacionImagenFrontal(bool isShiny = false)
        {
            Bitmap[] gifAnimated = new Bitmap[] {
                isShiny?ImagenFrontalShiny: ImagenFrontalNormal,
                GetCustomImagenFrontal(paletaAnimacion1),
                GetCustomImagenFrontal(paletaAnimacion2),
                isShiny?ImagenFrontalShiny: ImagenFrontalNormal
            };
            BitmapAnimated bmpAnimated = gifAnimated.ToAnimatedBitmap(false, 0, 200, 200, 500);
            return bmpAnimated;
        }

        public static Sprite GetSprite(RomGBA rom, Edicion edicion, Hex posicion)
        {
            return GetSprite(rom, edicion, CompilacionRom.GetCompilacion(rom, edicion), posicion);
        }
        public static Sprite GetSprite(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            Sprite sprite;
            Hex offsetSpriteFrontal =BloqueImagen.GetOffsetImg(rom, Zona.GetOffset(rom, Variables.SpriteFrontal, edicion, compilacion), posicion);
            Hex offsetSpriteTrasero = BloqueImagen.GetOffsetImg(rom, Zona.GetOffset(rom, Variables.SpriteTrasero, edicion, compilacion), posicion);
            Hex offsetPaletaNormal = BloqueImagen.GetOffsetImg(rom, Zona.GetOffset(rom, Variables.PaletaNormal, edicion, compilacion), posicion);//al parecer no guardo bien la direccion de las paletas...y peta...
            Hex offsetPaletaShiny = BloqueImagen.GetOffsetImg(rom, Zona.GetOffset(rom, Variables.PaletaShiny, edicion, compilacion), posicion);
            try
            {
                if (edicion.Abreviacion == Edicion.ABREVIACIONESMERALDA)
                {
                    //leer sprite esmeralda
                    sprite = new SpriteEsmeralda(rom, offsetSpriteFrontal, offsetSpriteTrasero, offsetPaletaNormal, offsetPaletaShiny);
                }
                else
                {
                    //leer sprite generico
                    sprite = new Sprite(rom, offsetSpriteFrontal, offsetSpriteTrasero, offsetPaletaNormal, offsetPaletaShiny);
                }
            }
            catch
            {
                throw new InvalidRomFormat();/*si los offsets leidos no estan bien es que hay un problema*/
            }
            return sprite;
        }

    }
    public class SpriteEsmeralda : Sprite
    {

        const int FOTOGRAMA1 = 0, FOTOGRAMA2 = 1;
        internal BloqueImagen imagenFrontal2;
        public SpriteEsmeralda(BloqueImagen.Paleta paletaNormal, BloqueImagen.Paleta paletaShiny, Hex offsetImagenFrontal, Bitmap imagenFrontal, Bitmap imagenFrontal2, Hex offsetImagenTrasera, Bitmap imagenTrasera)
            : base(paletaNormal, paletaShiny, offsetImagenFrontal, imagenFrontal, offsetImagenTrasera, imagenTrasera)
        {
            this.imagenFrontal2 = new BloqueImagen(offsetImagenFrontal, imagenFrontal2, BloqueImagen.LongitudImagen.L64, paletaNormal, paletaShiny);

        }
        public SpriteEsmeralda(RomGBA rom, Hex offsetImagenFrontal, Hex offsetImagenTrasera, Hex offsetPaletaNormal, Hex offsetPaletaShiny)
            : base(rom, offsetImagenFrontal, offsetImagenTrasera, offsetPaletaNormal, offsetPaletaShiny)
        {
            //tener en cuenta a la hora de poner las imagenes frontales que van seguidas y como una sola imagen
            //pongo la imagen 2 sola :D
            imagenFrontal2 = new BloqueImagen(offsetImagenFrontal, BloqueImagen.GetBloqueImagen(rom, offsetImagenFrontal,BloqueImagen.LongitudImagen.L64, PaletaNormal).DatosImagenDescomprimida.SubArray(TAMAÑOIMAGEN,TAMAÑOIMAGEN), BloqueImagen.LongitudImagen.L64, PaletaNormal, PaletaShiny);
        }

        public Bitmap ImagenFrontal2Normal
        {
            get
            {
                return imagenFrontal2[NORMAL];
            }

            set
            {
                imagenFrontal2[NORMAL] = value;
            }
        }
        public Bitmap ImagenFrontal2Shiny
        {
            get
            {
                return imagenFrontal2[SHINY];
            }

            set
            {
                imagenFrontal2[SHINY] = value;
            }
        }
        public override BitmapAnimated GetAnimacionImagenFrontal(bool isShiny = false)
        {
            Bitmap[] gifAnimated = new Bitmap[] {
                GetCustomImagenFrontal(paletaAnimacion2),
                isShiny?ImagenFrontalShiny:ImagenFrontalNormal,
                isShiny?ImagenFrontal2Shiny:ImagenFrontal2Normal
            };
            BitmapAnimated bmpAnimated = gifAnimated.ToAnimatedBitmap(false,500, 500,500);
            bmpAnimated.FrameAlAcabar = 1;
            return bmpAnimated;
        }
        public Bitmap GetCustomImagenFrontal2(Color[] colors)
        {
            if (colors.Length != BloqueImagen.Paleta.TAMAÑOPALETA)
                throw new ArgumentException("paleta incorrecta");
            return imagenFrontal2.GetBitmap(colors);
        }

    }
}
