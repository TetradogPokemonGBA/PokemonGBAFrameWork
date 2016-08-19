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
        protected int NORMAL = 0, SHINY = 1;
        BloqueImagen.Paleta paletaNormal;
        BloqueImagen.Paleta paletaShiny;
        BloqueImagen imagenFrontal;
        BloqueImagen imagenTrasera;

        public Sprite(BloqueImagen.Paleta paletaNormal, BloqueImagen.Paleta paletaShiny,Hex offsetImagenFrontal, Bitmap imagenFrontal,Hex offsetImagenTrasera, Bitmap imagenTrasera)
        {
            this.paletaNormal = paletaNormal;
            this.paletaShiny = paletaShiny;
            this.imagenFrontal =new BloqueImagen(offsetImagenFrontal, imagenFrontal,paletaNormal,paletaShiny);
            this.imagenTrasera = new BloqueImagen(offsetImagenTrasera, imagenTrasera, paletaNormal, paletaShiny);

        }
        public Sprite(RomPokemon rom,Hex offsetImagenFrontal,Hex offsetImagenTrasera,Hex offsetPaletaNormal,Hex offsetPaletaShiny)
        {
            this.paletaNormal = BloqueImagen.Paleta.GetPaleta(rom, offsetPaletaNormal);
            this.paletaShiny = BloqueImagen.Paleta.GetPaleta(rom, offsetPaletaShiny);
            this.imagenFrontal = new BloqueImagen(offsetImagenFrontal, BloqueImagen.GetBloqueImagen(rom, offsetImagenFrontal, paletaNormal).DatosImagenDescomprimida, paletaNormal, paletaShiny);
            this.imagenTrasera= new BloqueImagen(offsetImagenTrasera, BloqueImagen.GetBloqueImagen(rom, offsetImagenTrasera, paletaNormal).DatosImagenDescomprimida, paletaNormal, paletaShiny);
        }

        public BloqueImagen.Paleta PaletaNormal
        {
            get
            {
                return paletaNormal;
            }

            set
            {
                if (value == null) throw new ArgumentNullException();
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
                if (value == null) throw new ArgumentNullException();
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
        public void SetSprite(RomPokemon rom)
        {
            SetSprite(rom, this);
        }
        public static void SetSprite(RomPokemon rom,Sprite sprite)
        {
            if (rom == null || sprite == null)
                throw new ArgumentNullException();
            SpriteEsmeralda spriteEsmeralda=sprite as SpriteEsmeralda;
            byte[] imgEsmeraldaArray;
            //guardo las paletas
            BloqueImagen.Paleta.SetPaleta(rom, sprite.PaletaNormal);
            BloqueImagen.Paleta.SetPaleta(rom, sprite.PaletaShiny);
            //guardo la imagen trasera
            BloqueImagen.SetBloqueImagen(rom, sprite.imagenTrasera,false);
            //guardo la imagen frontal
            if(spriteEsmeralda!=null)
            {
                imgEsmeraldaArray = new byte[spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length * 2];
                unsafe
                {
                    fixed(byte* ptrImgEsmeralda=imgEsmeraldaArray)
                    {
                        fixed(byte* ptrImgParte1=spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida)
                            for (int i = 0; i < spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length; i++)
                               ptrImgEsmeralda[i] = ptrImgParte1[i];
                        fixed (byte* ptrImgParte2 = spriteEsmeralda.imagenFrontal2.DatosImagenDescomprimida)
                            for (int i = spriteEsmeralda.imagenFrontal.DatosImagenDescomprimida.Length,j=0; i <imgEsmeraldaArray.Length ; i++,j++)
                               ptrImgEsmeralda[i] = ptrImgParte2[j];
                    }
                }
                BloqueImagen.SetBloqueImagen(rom, new BloqueImagen(sprite.OffsetImagenFrontal, imgEsmeraldaArray, spriteEsmeralda.PaletaNormal),false);

            }else
            {
                BloqueImagen.SetBloqueImagen(rom, sprite.imagenFrontal,false);
            }

        }
    }
    public class SpriteEsmeralda:Sprite
    {
        internal const int TAMAÑOIMAGEN = 2048;
        const int FOTOGRAMA1=0, FOTOGRAMA2=1;
        internal BloqueImagen imagenFrontal2;
        public SpriteEsmeralda(BloqueImagen.Paleta paletaNormal, BloqueImagen.Paleta paletaShiny, Hex offsetImagenFrontal, Bitmap imagenFrontal, Bitmap imagenFrontal2, Hex offsetImagenTrasera, Bitmap imagenTrasera):base(paletaNormal,paletaShiny,offsetImagenFrontal,imagenFrontal,offsetImagenTrasera,imagenTrasera)
        {
            this.imagenFrontal2 = new BloqueImagen(offsetImagenFrontal, imagenFrontal2, paletaNormal, paletaShiny);

        }
        public SpriteEsmeralda(RomPokemon rom, Hex offsetImagenFrontal, Hex offsetImagenTrasera, Hex offsetPaletaNormal, Hex offsetPaletaShiny):base(rom,offsetImagenFrontal,offsetImagenTrasera,offsetPaletaNormal,offsetPaletaShiny)
        {
            BloqueImagen blFrontal = BloqueImagen.GetBloqueImagen(rom, offsetImagenFrontal, BloqueImagen.Paleta.GetPaleta(rom, offsetPaletaNormal));
            //tengo que dividir la imagen en dos
            byte[] imgBytes = new byte[TAMAÑOIMAGEN];
            unsafe
            {
                fixed (byte* ptrImgBytes = imgBytes)
                    fixed (byte* ptrImg1 = blFrontal.DatosImagenDescomprimida)
                        for (int i = 0; i < TAMAÑOIMAGEN; i++)
                    ptrImgBytes[i] = ptrImg1[i];
            }
            //pongo la imagen 1 sola
            ImagenFrontalNormal = BloqueImagen.GetBloqueImagen(imgBytes, blFrontal.GetPaleta(0))[NORMAL];
            imgBytes = new byte[TAMAÑOIMAGEN];
            unsafe
            {
                fixed (byte* ptrImgBytes = imgBytes)
                    fixed (byte* ptrImg2 = blFrontal.DatosImagenDescomprimida)
                        for (int i = TAMAÑOIMAGEN, j = 0; i < blFrontal.DatosImagenDescomprimida.Length; i++, j++)
                    ptrImgBytes[j] = ptrImg2[i];
            }
            //pongo la imagen 2 sola :D
            this.imagenFrontal2 = BloqueImagen.GetBloqueImagen(imgBytes,new BloqueImagen.Paleta[] { PaletaNormal, PaletaShiny });


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
        //tener en cuenta a la hora de poner las imagenes frontales que van seguidas y como una sola imagen
    }
}
