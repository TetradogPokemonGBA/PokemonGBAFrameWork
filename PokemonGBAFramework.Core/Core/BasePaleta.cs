using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public abstract class BasePaleta : IClonable<BasePaleta>, ICloneable
    {
        public const int LENGTHCOLOR = 2;
        public BasePaleta(int colores) => Colores = new Color[colores];
        public BasePaleta(params Color[] colores) => Colores = colores;
        public Color[] Colores { get; protected set; }
        public Color this[int index]
        {
            get { return Colores[index]; }
            set { Colores[index] = value; }
        }
        public void CambiarPosicion(int colorLeft, int colorRight)
        {
            if (colorLeft < 0 || colorRight < 0 || colorLeft >= Colores.Length || colorRight >= Colores.Length)
                throw new ArgumentOutOfRangeException();

            Color aux = Colores[colorLeft];
            Colores[colorLeft] = Colores[colorRight];
            Colores[colorRight] = aux;
        }
        public object Clone() => Clon();

        public abstract BasePaleta Clon();
        public byte[] GetBytes() => GetBytes(Colores);

        public static byte[] GetBytes(Color color)
        {
            return new byte[] { (byte)((byte)(color.R / 8) + ((byte)((color.G / 8) & 0x7) << 5)),
                                (byte)(((((byte)color.B / 8)) << 2) + ((byte)(color.G / 8) >> 3))
                               };
        }
        public static byte[] GetBytes(Color[] colores)
        {
            byte[] data = new byte[colores.Length * LENGTHCOLOR];
            for (int i = 0; i < colores.Length; i++)
            {
                data.SetArray(i * LENGTHCOLOR, GetBytes(colores[i]));
            }
            return data;
        }
        public static byte[] GetBytesGBA(BasePaleta paleta, bool comprimirLz77 = true)
        {
            byte[] bytesPaleta = paleta.GetBytes();

            //la comprimo
            if (comprimirLz77)
                bytesPaleta = LZ77.Comprimir(bytesPaleta);

            return bytesPaleta;
        }
        public static Color GetColor(byte[] data, int offset = 0)
        {
            ushort tempValue = Serializar.ToUShort(data.SubArray(offset, LENGTHCOLOR));

            byte r = (byte)((tempValue & 0x1f) << 3);
            byte g = (byte)(((tempValue >> 5) & 0x1f) << 3);
            byte b = (byte)(((tempValue >> 10) & 0x1f) << 3);

            return Color.FromArgb(0xFF, r, g, b);
        }
        public static Color[] GetColors(byte[] data, int offset = 0) => GetColors(data, (data.Length - offset) / LENGTHCOLOR, offset);
        public static Color[] GetColors(byte[] data, int numColors, int offset = 0)
        {
            if (data.Length - offset < numColors * LENGTHCOLOR)
                throw new ArgumentOutOfRangeException(nameof(numColors));

            Color[] colors = new Color[numColors];

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = GetColor(data, offset);
                offset += LENGTHCOLOR;
            }
            return colors;
        }

        public static Color ToGBAColor(byte r, byte g, byte b)
        {//estaria bien no tener que usar conversiones y ser lo más simple posible :)
            byte parteA, parteB;
            ushort colorGBA;
            parteA = (byte)((byte)(r / 8) + ((byte)((g / 8) & 0x7) << 5));
            parteB = (byte)((((byte)(b / 8)) << 2) + ((byte)(g / 8) >> 3));
            colorGBA = Serializar.ToUShort(new byte[] { parteA, parteB });
            return Color.FromArgb(byte.MaxValue, (byte)((colorGBA & 0x1f) << 3), (byte)(((colorGBA >> 5) & 0x1f) << 3), (byte)(((colorGBA >> 10) & 0x1f) << 3));
        }


    }
}
