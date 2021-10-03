using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace PokemonGBAFramework.Core.Extension
{
    public static class ExtensionBitmap
    {
        /// <summary>
        /// Bytes que ocupa un color Convertido con el método de extensión ToGbaBitmap
        /// </summary>
        public const int BYTESPORCOLOR = 4;
        //BGRA
        public const int A = 3;
        public const int R = A - 1;
        public const int G = R - 1;
        public const int B = G - 1;
        public static byte FlipByte(this byte Byte)
        {
            return (byte)((Byte >> 4) + ((Byte << 4) & 0xF0));
        }
        public static BloqueImagen ToBloqueImagen(this Bitmap bmp)
        {
            return new BloqueImagen(bmp);
        }
        public static Bitmap Flip(this Bitmap bmp, bool flipX, bool flipY)
        {
            Bitmap bmpFlip;

            if (flipX && flipY)
                bmpFlip = bmp.HorizontalAndVerticalFlip();
            else if (flipY)
                bmpFlip = bmp.VerticalFlip();
            else if (flipX)
                bmpFlip = bmp.HorizontalFlip();
            else bmpFlip = bmp;

            return bmpFlip;
        }
        public static Bitmap HorizontalFlip(this Bitmap img)
        {
            Bitmap dimg = (Bitmap)img.Clone();
            dimg.RotateFlip(RotateFlipType.RotateNoneFlipX);//mirar que sea asi
            return dimg;
        }

        public static Bitmap VerticalFlip(this Bitmap img)
        {
            Bitmap dimg = (Bitmap)img.Clone();
            dimg.RotateFlip(RotateFlipType.RotateNoneFlipY);//mirar que sea asi
            return dimg;
        }
        public static Bitmap HorizontalAndVerticalFlip(this Bitmap img)
        {
            Bitmap dimg = (Bitmap)img.Clone();
            dimg.RotateFlip(RotateFlipType.RotateNoneFlipXY);//mirar que sea asi
            return dimg;
        }

       static unsafe void ToGbaColor(byte* ptrBytesBmp, int total)
        {
            const byte SINTRANSPARENCIA = 0xFF;
            const int ARGB = 4;
            Color aux;

            for (int i = 0; i < total; i += ARGB)
            {
                aux = Paleta.ToGBAColor(*(ptrBytesBmp + R), *(ptrBytesBmp + G), *(ptrBytesBmp + B));

                *(ptrBytesBmp + A) = SINTRANSPARENCIA;
                *(ptrBytesBmp + R) = aux.R;
                *(ptrBytesBmp + G) = aux.G;
                *(ptrBytesBmp + B) = aux.B;

                ptrBytesBmp += ARGB;
            }


        }
        public static Bitmap SetColor(this Bitmap bmp, Color color)
        {
            Gabriel.Cat.S.Utilitats.V2.Color colorToSet = color;
            unsafe
            {
                Gabriel.Cat.S.Utilitats.V2.Color* ptBmp;

                bmp.TrataBytes((MetodoTratarUnmanagedTypePointer<byte>)((ptrBmp) =>
                {
                    ptBmp = (Gabriel.Cat.S.Utilitats.V2.Color*)ptrBmp;
                    for (int i = 0, f = bmp.Width * bmp.Height; i < f; i++)
                    {
                        *ptBmp = colorToSet;
                        ptBmp++;
                    }
                }));
            }

            return bmp;
        }
        public static int IndexByte(this byte[] bytes,int inicio,byte toFind)
        {
            if (inicio < 0 || inicio > bytes.Length - 1)
                throw new ArgumentOutOfRangeException();
            int index=-1;
            unsafe
            {
                byte* ptBytes;
                fixed(byte* ptrBytes = bytes)
                {
                    ptBytes = ptrBytes+inicio;

                    for(int i = inicio; i < bytes.Length && index<0; i++)
                    {
                        if (Equals(toFind, *ptBytes))
                            index = i;
                        ptBytes++;
                    }

                }
            }
            return index;
        }
        public static Color ToGBAColor(this Color color)
        {
            return BasePaleta.ToGBAColor(color.R, color.G, color.B);
        }

        public static int NextOffsetValido(this int offset)
        {
            return offset + (4 - (offset % 4));//mirar que sea así
        }
    }
}