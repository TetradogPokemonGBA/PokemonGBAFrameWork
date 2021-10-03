using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Text;
namespace PokemonGBAFramework.Core
{
    public class BloqueImagen
    {
        public const int LENGTHHEADER = 4;
        public const int LENGTHHEADERCOMPLETO = OffsetRom.LENGTH + LENGTHHEADER;
        public static readonly Creditos Creditos;
        static readonly byte[] DefaultHeader = { 0x0, 0x8 };
        static readonly byte[] DefaultHeader2 = { 0x0, 0x10 };

        static BloqueImagen()
        {
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.GITHUB], "Link12552", "NSE->Calcular lado imagen a partir de los bytes descomprimidos");
        }
        public BloqueImagen()
        {
            Paletas = new Llista<Paleta>();
            DatosDescomprimidos = new BloqueBytes(0);
            Offset = -1;
            Id = -1;
            Formato = -1;

        }

        public BloqueImagen([NotNull] byte[] datosImg, [NotNull] Paleta[] paletas, bool estaComprimido = false) : this(new BloqueBytes(estaComprimido ? LZ77.Descomprimir(datosImg) : datosImg), paletas)
        {

        }
        public BloqueImagen([NotNull] BloqueBytes datosDescomprimidosImg, [NotNull] params Paleta[] paletas) : this()
        {
            if (datosDescomprimidosImg == default || paletas == default)
                throw new ArgumentNullException();

            DatosDescomprimidos = datosDescomprimidosImg;
            Paletas.AddRange(paletas);
        }
        public BloqueImagen([NotNull] Bitmap bitmap) :this()
        {//source NSE
            byte aux;
            Rectangle rect;
            System.Drawing.Imaging.BitmapData bmpData;
            IntPtr ptr;
            int totalRgbValues;
            byte[] rgbValues;
            Paleta paleta = new Paleta();

            for (int p = 0; p < paleta.Colores.Length && p < bitmap.Palette.Entries.Length; p++)
            {
           
                   paleta.Colores[p] = bitmap.Palette.Entries[p];
                
            }
            Paletas.Add(paleta);
            DatosDescomprimidos.Bytes = new byte[bitmap.Width * bitmap.Height / 2];
            #region lockbits
            rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            bmpData =
                bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);


            // Get the address of the first line.
            ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            totalRgbValues = Math.Abs(bmpData.Stride) * bitmap.Height;
            rgbValues = new byte[totalRgbValues];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, totalRgbValues);

            #endregion

            if (rgbValues.Length > bitmap.Width * bitmap.Height / 2)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x + 1 < bitmap.Width; x++)
                    {
                        aux = (byte)(rgbValues[y * bitmap.Width + x] + (rgbValues[y * bitmap.Width + x + 1] << 4));
                        if (aux != 0)
                        {

                            DatosDescomprimidos.Bytes[Position2Index(new Size(bitmap.Width, bitmap.Height), new Point(x, y))] = aux;

                        }
                    }

                }
            }
            else
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width / 2; x++)
                    {
                       aux = rgbValues[y * (bitmap.Width / 2) + x].FlipByte();
                        if (aux != 0)
                        {
                            DatosDescomprimidos.Bytes[Position2Index(new Size(bitmap.Width, bitmap.Height), new Point(x * 2, y))] = aux;
                        }
                    }

                }
            }
            bitmap.UnlockBits(bmpData);

        }
        public int Offset { get; set; }
        public short Id { get; set; }

        public short Formato { get; set; }
        private byte[] Header
        {
            get { return Serializar.GetBytes(Formato).AddArray(Serializar.GetBytes(Id)); }
            set
            {
                if (value == default)
                    throw new ArgumentNullException();
                if (value.Length < LENGTHHEADER)
                    value = value.AddArray(new byte[LENGTHHEADER - value.Length]);
                //por mirar
                Formato = Serializar.ToShort(value.SubArray(0, sizeof(short)));
                Id = Serializar.ToShort(value.SubArray(sizeof(short), sizeof(short)));

            }
        }
        public byte[] HeaderCompleto
        {
            get { return GetHeaderCompleto(Offset); }

        }
        public BloqueBytes DatosDescomprimidos { get; set; }

        public Llista<Paleta> Paletas { get; private set; }


        public Bitmap this[int indexPaleta]
        {
            get { return this[Paletas[indexPaleta]]; }
        }
        public Bitmap this[[NotNull] Paleta paleta]
        {
            get { return BuildBitmap(DatosDescomprimidos.Bytes, paleta); }
        }
        public byte[] GetHeaderCompleto(int pointerData)
        {
            return new OffsetRom(pointerData).BytesPointer.AddArray(Header);
        }
        public Bitmap GetImg(int indexPaleta = 0, bool showBackGround = false)
        {
            return BuildBitmap(DatosDescomprimidos.Bytes, Paletas[indexPaleta], showBackGround);
        }
        public void ExportToBMP([NotNull] string fileName, int indexPaleta = 0)
        {
            //source NSE
            if (indexPaleta >= Paletas.Count)
                indexPaleta = Paletas.Count - 1;
            //4bb bitmap http://en.wikipedia.org/wiki/BMP_file_format
            //header creation
            byte[] data; //= new byte[54 + 4 * Sprite.Palette.Colors.Length + (32 * (Sprite.Width * Sprite.Height))];
            Stream stream;
            byte[] ilength;
            byte[] flength;
            BinaryWriter bw;
            int lado = GetLadoBmp(DatosDescomprimidos.Bytes);

            data = new byte[118 + (4* (lado * lado))];

            //1
            //0xF at offset 0x9 will mean 16 colors (this is not part of the bitmap format)
            data[9] = 0XF;
            data[0xA] = 0x76;

            //2
            data[0x1C] = 4;
            ilength = BitConverter.GetBytes(4 * (lado * lado));


            data[0] = 0x42;
            data[1] = 0x4D;

            //Write Length                 
            flength = BitConverter.GetBytes(data.Length);
            flength.CopyTo(data, 2);

            //Add "NSE" to the bitmap
            data[6] = 0x4E;
            data[7] = 0x53;
            data[8] = 0x45;

            //1
            data[0xE] = 40;

            byte[] width = BitConverter.GetBytes(lado);
            width.CopyTo(data, 0x12);

            byte[] height = BitConverter.GetBytes(lado);
            height.CopyTo(data, 0x16);

            data[0x1A] = 1;

            //2
            ilength.CopyTo(data, 0x22);

            // Write Palette Table

            for(int i=0;i< Paletas[indexPaleta].Colores.Length;i++)
            {
                data[0x36 + i * 4] = Paletas[indexPaleta].Colores[i].B;
                data[0x37 + i * 4] = Paletas[indexPaleta].Colores[i].G;
                data[0x38 + i * 4] = Paletas[indexPaleta].Colores[i].R;

            }


            for (int y = 0,xF=lado/2; y < lado; y++)
            {
                for (int x = 0; x < xF; x++)
                {
                    data[118 + (lado - y - 1) * xF + x] = DatosDescomprimidos[Position2Index(new Size(lado, lado), new Point(x * 2, y))].FlipByte();
                }
            }






            stream = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            bw = new BinaryWriter(stream);

            bw.Write(data);
            bw.Close();
            stream.Close();

        }

        static int Position2Index(Size Size, Point Position)
        {
            //source NSE

            int r = (Position.Y - (Position.Y % 8)) / 2 * Size.Width;
            r += (Position.X - (Position.X % 8)) * 4;
            r += (Position.Y % 8) * 4;
            r += (Position.X % 8) / 2;

            return r;
        }
        public byte[] DatosComprimidos(LZ77.CompressionMode compressionMode= LZ77.CompressionMode.Old)
        {
            return LZ77.Comprimir(DatosDescomprimidos.Bytes, compressionMode);
        }
        public void CambiarPosicionColor(int colorLeft, int colorRight, bool cambiarPaletas = true)
        {//por probar :D
            if (colorLeft < 0 || colorLeft > Paleta.LENGTH || colorRight < 0 || colorRight > Paleta.LENGTH)
                throw new ArgumentOutOfRangeException();
            byte bColorLeft = (byte)colorLeft;
            byte bColorRight = (byte)colorRight;
            byte left;
            byte right;
            byte aux;
            unsafe
            {
                byte* ptrBytesImg;
                fixed (byte* ptBytesImg = DatosDescomprimidos.Bytes)
                {
                    ptrBytesImg = ptBytesImg;
                    for (int i = 0, f = DatosDescomprimidos.Bytes.Length; i < f; i++)
                    {
                        left = (*ptrBytesImg).GetHalfByte();
                        right = (*ptrBytesImg).GetHalfByte(false);
                        //si  es el colorleft pongo colorRight y si el colorRight pongo colorLeft

                        if (left == bColorLeft)
                            left = bColorRight;
                        else if (left == bColorRight)
                            left = bColorLeft;

                        if (right == bColorLeft)
                            right = bColorRight;
                        else if (right == bColorRight)
                            right = bColorLeft;
                        //formo el byte
                        aux = left.SetHalfByte(right);
                        //aplico los cambios
                        *ptrBytesImg = aux;
                        ptrBytesImg++;

                    }
                }

            }
            if (cambiarPaletas)
                for (int i = 0; i < Paletas.Count; i++)
                    Paletas[i].CambiarPosicion(colorLeft, colorRight);
        }


        public static BloqueImagen GetBloqueImagenSinHeader([NotNull] RomGba rom, int offsetPointerData)
        {
            BloqueImagen bloqueCargado = new BloqueImagen();
            bloqueCargado.Offset = new OffsetRom(rom, offsetPointerData).Offset;
            bloqueCargado.DatosDescomprimidos.Bytes = LZ77.Descomprimir(rom.Data.Bytes, bloqueCargado.Offset);
            return bloqueCargado;
        }
        public static BloqueImagen GetBloqueImagen([NotNull] RomGba rom, int offsetHeader)
        {
            byte[] header = rom.Data.SubArray(offsetHeader + OffsetRom.LENGTH, LENGTHHEADER);
            BloqueImagen bloqueCargado = GetBloqueImagenSinHeader(rom, offsetHeader);
            bloqueCargado.Header = header;
            return bloqueCargado;
        }


        #region Interpretando datos
        public static Bitmap BuildBitmap([NotNull] byte[] datosImagenDescomprimida, [NotNull] Paleta paleta, bool showBackground = false)
        {
            int longitudLado = GetLadoBmp(datosImagenDescomprimida);
            return BuildBitmap(datosImagenDescomprimida, paleta, longitudLado, longitudLado, showBackground);
        }

        private static int GetLadoBmp([NotNull]  byte[] datosImagenDescomprimida)
        {//source NSE
            return Convert.ToInt32(Math.Sqrt(datosImagenDescomprimida.Length / 32)) * 8;
        }

        public static byte[] GetDatosDescomprimidos([NotNull] Bitmap bmp, [NotNull] Paleta paleta)
        {
            byte[] data = new byte[bmp.Width * bmp.Height / 2];

            unsafe
            {
                Gabriel.Cat.S.Utilitats.V2.Color* ptrBmp;
                fixed (byte* ptBytesBmp = bmp.GetBytes())
                {
                    ptrBmp = (Gabriel.Cat.S.Utilitats.V2.Color*)ptBytesBmp;
                    for (int i = 0; i < data.Length; i++)
                    {
                        data[i] = (byte)(paleta.IndexOf(*ptrBmp) << 4);
                        ptrBmp++;
                        data[i] += (byte)paleta.IndexOf(*ptrBmp);
                    }
                }
            }


            return data;
        }

        public static Bitmap BuildBitmap([NotNull] byte[] datosImagenDescomprimida, [NotNull] Paleta paleta, int width, int height, bool showBackground = false)
        {
            if (datosImagenDescomprimida == null || paleta == null)
                throw new ArgumentNullException();
            const int BYTESPERPIXEL = 4;
            const int NUM = 8;//poner algo mas descriptivo


            int bytesPorLado = BYTESPERPIXEL * width;
            Bitmap bmpTiles = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Color[] colores = paleta.Colores;
            System.Drawing.Color color;
            byte temp;
            int posByteImgArray = 0, pos;

            if (!showBackground)
                colores[0] = Color.Transparent;
            unsafe
            {

                bmpTiles.TrataBytes((MetodoTratarUnmanagedTypePointer<byte>)((bytesBmp) =>
                {

                    for (int y1 = 0; y1 < height; y1 += NUM)
                        for (int x1 = 0; x1 < width; x1 += NUM)
                            for (int y2 = 0; y2 < NUM; y2++)
                                for (int x2 = 0; x2 < NUM; x2 += 2, posByteImgArray++)
                                {
                                    temp = datosImagenDescomprimida[posByteImgArray];
                                    //pongo los pixels de dos en dos porque se leen diferente de la paleta
                                    //pixel derecho

                                    pos = (x1 + x2) * BYTESPERPIXEL + (y1 + y2) * bytesPorLado;
                                    color = colores[temp.GetHalfByte(false)];

                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = color.A;

                                    //pixel izquierdo
                                    pos += BYTESPERPIXEL;

                                    color = colores[temp.GetHalfByte(true)];
                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = color.A;



                                }

                }));

            }


            return bmpTiles;
        }


        #endregion
        public static bool IsHeaderOk([NotNull] RomGba gbaRom, int offsetToCheck)
        {
            //PointerHeaderID
            return OffsetRom.Check(gbaRom, offsetToCheck) && (gbaRom.Data[offsetToCheck + 7] != OffsetRom.BYTEIDENTIFICADOR16MB || gbaRom.Data[offsetToCheck + 7] != OffsetRom.BYTEIDENTIFICADOR32MB);
        }


        #region Conversiones
        public static implicit operator Bitmap([NotNull] BloqueImagen bloqueImg)
        {
            return bloqueImg[0];
        }
        public static Bitmap operator +([NotNull] BloqueImagen bloqueImagen, [NotNull] Paleta paleta)
        {
            return bloqueImagen[paleta];
        }
        #endregion


    }
}