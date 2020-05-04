using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        public BloqueImagen(int longitudLado) : this(new Bitmap(longitudLado, longitudLado), true)
        {

        }
        public BloqueImagen(Bitmap img, bool estaConvertidaAGba = false) : this()
        {
            DatosDescomprimidos = new BloqueBytes(GetDatosDescomprimidos(img, null, estaConvertidaAGba));
            Paletas.Add(Paleta.GetPaleta(img));
        }
        public BloqueImagen(BloqueBytes datosDescomprimidosImg, params Paleta[] paletas) : this()
        {
            if (datosDescomprimidosImg == null || paletas == null)
                throw new ArgumentNullException();

            DatosDescomprimidos = datosDescomprimidosImg;
            Paletas.AddRange(paletas);
        }
        public int Offset { get; set; }
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
                //por mirar
                Formato = Serializar.ToShort(value.SubArray(0, 2));
                Id = Serializar.ToShort(value.SubArray(2, 2));

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
        public Bitmap this[Paleta paleta]
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
        public byte[] DatosComprimidos()
        {
            return LZ77.Comprimir(DatosDescomprimidos.Bytes, 0);
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


        public static BloqueImagen GetBloqueImagenSinHeader(RomGba rom, int offsetPointerData)
        {
            BloqueImagen bloqueCargado = new BloqueImagen();
            bloqueCargado.Offset = new OffsetRom(rom, offsetPointerData).Offset;
            bloqueCargado.DatosDescomprimidos.Bytes = LZ77.Descomprimir(rom.Data.Bytes, bloqueCargado.Offset);
            return bloqueCargado;
        }
        public static BloqueImagen GetBloqueImagen(RomGba rom, int offsetHeader)
        {
            byte[] header = rom.Data.SubArray(offsetHeader + OffsetRom.LENGTH, LENGTHHEADER);
            BloqueImagen bloqueCargado = GetBloqueImagenSinHeader(rom, offsetHeader);
            bloqueCargado.Header = header;
            return bloqueCargado;
        }


        #region Interpretando datos
        public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida, Paleta paleta, bool showBackground = false)
        {
            int longitudLado = Convert.ToInt32(Math.Sqrt(datosImagenDescomprimida.Length / 32)) * 8;
            return BuildBitmap(datosImagenDescomprimida, paleta, longitudLado, longitudLado, showBackground);
        }
        public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida, Paleta paleta, int width, int height, bool showBackground = false)
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

                bmpTiles.TrataBytes((MetodoTratarBytePointer)((bytesBmp) =>
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
        public static byte[] GetDatosDescomprimidos(Bitmap bmp, Paleta paleta = null, bool estaConvertidaAGba = false)
        {//falta probar
            const int PIXELSPERBYTE = 2;

            byte[] bytesBmpGBADescomprimido;
            SortedList<Gabriel.Cat.S.Utilitats.V2.Color, int> dicPosColors;
            Gabriel.Cat.S.Utilitats.V2.Color aux;
            if (bmp == null)
                throw new ArgumentNullException("bmp");
            if (!estaConvertidaAGba)
                bmp = bmp.ToGbaBitmap();
            if (paleta == null)
                paleta = Paleta.GetPaleta(bmp);
            bytesBmpGBADescomprimido = new byte[bmp.Width * bmp.Height / PIXELSPERBYTE];
            dicPosColors = new SortedList<Gabriel.Cat.S.Utilitats.V2.Color, int>();
            for (int i = 0; i < paleta.Colores.Length; i++)
            {
                aux = new Gabriel.Cat.S.Utilitats.V2.Color(paleta.Colores[i].ToArgb());
                if (!dicPosColors.ContainsKey(aux))
                    dicPosColors.Add(aux, i);
            }
            unsafe
            {
                Gabriel.Cat.S.Utilitats.V2.Color* ptrColor;
                byte* ptrBmpGBADescomprimido;
                fixed (byte* ptBmpGBADescomprimido = bytesBmpGBADescomprimido)
                {
                    fixed (byte* ptBytesColorsBmp = bmp.GetBytes())
                    {
                        ptrColor = (Gabriel.Cat.S.Utilitats.V2.Color*)ptBytesColorsBmp;
                        ptrBmpGBADescomprimido = ptBmpGBADescomprimido;

                        for (int i = 0, f = bytesBmpGBADescomprimido.Length; i < f; i++)
                        {
                            *ptrBmpGBADescomprimido = (byte)(dicPosColors[*ptrColor] * 16);
                            //hago un color
                            ptrColor++;
                            //hago el otro
                            *ptrBmpGBADescomprimido += (byte)(dicPosColors[*ptrColor]);
                            ptrBmpGBADescomprimido++;
                        }

                    }
                }

            }

            return bytesBmpGBADescomprimido;
        }
        /// <summary>
        /// Convert Bitmap To 4BPP Byte Array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        static BloqueBytes GetDatosComprimidos(Bitmap img, bool estaConvertidaAGba = false)
        {

            return new BloqueBytes(LZ77.Comprimir(GetDatosDescomprimidos(img, null, estaConvertidaAGba)));
        }
        #endregion
        public static bool IsHeaderOk(RomGba gbaRom, int offsetToCheck)
        {
            //PointerHeaderID
            return new OffsetRom(gbaRom, offsetToCheck).IsAPointer && gbaRom.Data[offsetToCheck + 7] != 0x8;
        }


        #region Conversiones
        public static implicit operator Bitmap(BloqueImagen bloqueImg)
        {
            return bloqueImg[0];
        }
        public static Bitmap operator +(BloqueImagen bloqueImagen, Paleta paleta)
        {
            return bloqueImagen[paleta];
        }
        #endregion


    }
}