using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Paleta : IComparable, IComparable<Paleta>
    {
        public static System.Drawing.Color BackgroundColorDefault = System.Drawing.Color.Transparent;
        public const int LENGTHHEADER = 4;
        public const int LENGTHHEADERCOMPLETO = OffsetRom.LENGTH + LENGTHHEADER;
        public const int LENGTH = 16;
        public static readonly Paleta Default = new Paleta();
        static readonly byte[] DefaultHeader = { 0x0, 0x0 };

        public Paleta()
        {
            Colores = new Color[LENGTH];
            Offset = -1;
        }
        public Paleta(params Color[] coloresPaleta) : this()
        {
            if (coloresPaleta == null)
                throw new ArgumentNullException();
            for (int i = 0; i < LENGTH && i < coloresPaleta.Length; i++)
                this.Colores[i] = coloresPaleta[i];

        }

        public int Offset { get; set; }
        public byte SortID
        {
            get
            {
                return Serializar.GetBytes(Id)[0];
            }
        }
        public short Id { get; set; }

        public short Formato { get; set; }
        private byte[] Header
        {
            get { return Serializar.GetBytes(Formato).AddArray(Serializar.GetBytes(Id)); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length < LENGTHHEADER)
                    value = value.AddArray(new byte[LENGTHHEADER - value.Length]);

                Id = Serializar.ToShort(value.SubArray(0, 2));
                Formato = Serializar.ToShort(value.SubArray(2, 2));
            }
        }
        public byte[] HeaderCompleto
        {
            get { return GetHeaderCompleto(Offset); }

        }
        public Color[] Colores { get; private set; }

        public Color this[int index]
        {
            get { return Colores[index]; }
            set { Colores[index] = value; }
        }

        public byte[] GetHeaderCompleto(int pointerData)
        {
            return new OffsetRom(pointerData).BytesPointer.AddArray(Header);
        }

        #region IComparable implementation


        public void CambiarPosicion(int colorLeft, int colorRight)
        {
            Color aux = Colores[colorLeft];
            Colores[colorLeft] = Colores[colorRight];
            Colores[colorRight] = aux;
        }

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as Paleta);
        }
        public int CompareTo(Paleta other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = SortID.CompareTo(other.SortID);

            }
            else
            {
                compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            }
            return compareTo;
        }


        #endregion

        public static Paleta GetPaletaSinHeader(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
        {
            //sacado de Nameless
            if (rom == null || offsetPointerPaleta < 0)
                throw new ArgumentException();

            const int TOTALBYTESPALETA = 32;

            byte[] bytesPaletaDescomprimidos;
            Paleta paleta;
            int offsetPaletaData = new OffsetRom(rom, offsetPointerPaleta).Offset;

            if (LZ77.CheckCompresionLZ77(rom.Data.Bytes, offsetPaletaData))
                bytesPaletaDescomprimidos = LZ77.Descomprimir(rom.Data.Bytes, offsetPaletaData);
            else
                bytesPaletaDescomprimidos = rom.Data.Bytes.SubArray(offsetPaletaData, TOTALBYTESPALETA);//son dos bytes por color

            paleta = GetPaleta(bytesPaletaDescomprimidos);
            if (!showBackgroundColor)
            {
                paleta[0] = BackgroundColorDefault;
            }
            paleta.Offset = offsetPointerPaleta;
            return paleta;
        }
        public static Paleta GetPaleta(byte[] datosPaletaDescomprimida)
        {

            Paleta paleta = new Paleta();

            ushort tempValue;
            byte r, g, b;
            System.Drawing.Color colorPaleta;

            for (int i = 0; i < LENGTH; i++)
            {
                tempValue = Serializar.ToUShort(datosPaletaDescomprimida.SubArray(i * 2, 2));
                r = (byte)((tempValue & 0x1f) << 3);
                g = (byte)(((tempValue >> 5) & 0x1f) << 3);
                b = (byte)(((tempValue >> 10) & 0x1f) << 3);
                colorPaleta = System.Drawing.Color.FromArgb(0xFF, r, g, b);
                paleta[i] = colorPaleta;

            }
            return paleta;
        }
        public static Paleta Get(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
        {
            byte[] header = rom.Data.SubArray(offsetPointerPaleta + OffsetRom.LENGTH, LENGTHHEADER);//lo posiciono para leer la información que contiene sin el pointer
            Paleta paletaCargada = GetPaletaSinHeader(rom, offsetPointerPaleta, showBackgroundColor);
            paletaCargada.Header = header;
            return paletaCargada;
        }
        public static Paleta GetPaleta(Bitmap img)
        {//falta probar
            return new Paleta(img.GetPaleta());
        }





        public static byte[] GetBytesGBA(Paleta paleta, bool comprimirLz77 = true)
        {
            const int BYTESCOLORGBA = 2;
            System.Drawing.Color[] coloresPaleta = paleta.Colores;
            byte[] bytesPaleta = new byte[LENGTH * BYTESCOLORGBA];
            byte[] bytesAux = new byte[BYTESCOLORGBA];


            for (int i = 0; i < LENGTH; i++)
            {
                bytesPaleta[i] = (byte)((byte)(coloresPaleta[i].R / 8) + ((byte)((coloresPaleta[i].G / 8) & 0x7) << 5));
                bytesPaleta[i + 1] = (byte)((((byte)(coloresPaleta[i].B / 8)) << 2) + ((byte)(coloresPaleta[i].G / 8) >> 3));

            }
            //la comprimo
            if (comprimirLz77)
                bytesPaleta = LZ77.Comprimir(bytesPaleta);

            return bytesPaleta;
        }

        public static Color ToGBAColor(Color color)
        {
            return ToGBAColor(color.R, color.G, color.B);
        }
        public static Color ToGBAColor(byte r, byte g, byte b)
        {//estaria bien no tener que usar conversiones y ser lo más simple posible :)
            byte parteA, parteB;
            ushort colorGBA;
            parteA = (byte)((byte)(r / 8) + ((byte)((g / 8) & 0x7) << 5));
            parteB = (byte)((((byte)(b / 8)) << 2) + ((byte)(g / 8) >> 3));
            colorGBA = Serializar.ToUShort(new byte[] { parteA, parteB });
            return System.Drawing.Color.FromArgb(0xFF, (byte)((colorGBA & 0x1f) << 3), (byte)(((colorGBA >> 5) & 0x1f) << 3), (byte)(((colorGBA >> 10) & 0x1f) << 3));
        }

        public static bool IsHeaderOk(RomGba gbaRom, int offsetToCheck)
        {
            return new OffsetRom(gbaRom, offsetToCheck).IsAPointer && gbaRom.Data.Bytes.ArrayEqual(DefaultHeader, offsetToCheck + OffsetRom.LENGTH + 2);
        }



    }

}