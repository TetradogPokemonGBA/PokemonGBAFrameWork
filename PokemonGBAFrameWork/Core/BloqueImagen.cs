using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    public class BloqueImagen
    {

        Hex offsetPointerImg;
        byte[] datosDescomprimidos;
        Llista<Paleta> paletas;//poder crear una imagen sin paletas porque a veces no hay...cuando estas investigando a veces falta la paleta y hay que ir probando
        //campos calculados para ahorrar computación
        int tamañoImgComprimida;



        //zona constructores
        //hacer el basico y luego la sobrecarga

        BloqueImagen(Hex offsetInicio, byte[] datosDescomprimidos, IEnumerable<Paleta> paletas, bool aux)
        {
            offsetPointerImg = offsetInicio;
            this.paletas = new Llista<Paleta>(paletas);
            DatosDescomprimidos = datosDescomprimidos;
        }

        public BloqueImagen(Hex offsetPointerImg, byte[] bytesImgDescomprimida, params Paleta[] paletas) : this(offsetPointerImg, bytesImgDescomprimida, paletas, false)
        {

        }
        public BloqueImagen(byte[] bytesImgDescomprimida) : this(0, bytesImgDescomprimida, new Paleta[] { }, false)
        {
        }

        public BloqueImagen(Bitmap bmp, params Paleta[] paletas) : this(0, bmp, paletas)
        { }
        public BloqueImagen(Hex offsetInicio, Bitmap bmp, params Paleta[] paletas)
        {
            OffsetPointerImg = offsetInicio;
            DatosDescomprimidos = GetDatosDescomprimidos(bmp);
            this.paletas = new Llista<Paleta>(paletas);
        }



        public Hex OffsetInicio
        {
            get
            {
                return offsetPointerImg;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                offsetPointerImg = value;
            }
        }
        public Hex OffsetFin
        {
            get { return OffsetInicio + tamañoImgComprimida; }
        }

        public int TamañoImgComprimida
        {
            get
            {
                return tamañoImgComprimida;
            }

            private set
            {
                tamañoImgComprimida = value;
            }
        }



        public Llista<Paleta> Paletas
        {
            get
            {
                return paletas;
            }

            private set
            {
                paletas = value;
            }
        }
        public byte[] DatosComprimidos
        {
            get { return Lz77.ComprimirDatosLZ77(datosDescomprimidos); }
            set
            {
                try
                {
                    datosDescomprimidos = Lz77.DescomprimirDatosLZ77(value, 0);
                    tamañoImgComprimida = value.Length;
                }
                catch { throw new ArgumentException("Los datos no son validos!", "value"); }
            }
        }
        public byte[] DatosDescomprimidos
        {
            get { return datosDescomprimidos; }
            set
            {
                try
                {
                    tamañoImgComprimida = Lz77.ComprimirDatosLZ77(value).Length;
                    datosDescomprimidos = value;
                }
                catch { throw new ArgumentException("Los datos no son validos!", "value"); }
            }
        }
        /// <summary>
        /// es el offset donde se guardará el pointer que va a la imagen
        /// </summary>
        public Hex OffsetPointerImg
        {
            get
            {
                return offsetPointerImg;
            }

            set
            {
                offsetPointerImg = value;
            }
        }


        public Bitmap this[int indexPaleta]
        {
            get { return BuildBitmap(DatosDescomprimidos, Paletas[indexPaleta]); }
        }

        public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida, Paleta paleta)
        {
            if (datosImagenDescomprimida == null || paleta == null)
                throw new ArgumentNullException();
            const int BYTESPERPIXEL = 4;
            const int NUM = 8;//poner algo mas descriptivo

            int longitudLado = Convert.ToInt32(Math.Sqrt(datosImagenDescomprimida.Length / 32)) * 8;//sacado de NSE creditos a Link12552
            int bytesPorLado = BYTESPERPIXEL * longitudLado;
            Bitmap bmpTiles = new Bitmap(longitudLado, longitudLado, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Color color;
            byte temp;
            int posByteImgArray = 0, pos;

            unsafe
            {

                bmpTiles.TrataBytes((MetodoTratarBytePointer)((bytesBmp) =>
                {

                    for (int y1 = 0; y1 < longitudLado; y1 += NUM)
                        for (int x1 = 0; x1 < longitudLado; x1 += NUM)
                            for (int y2 = 0; y2 < NUM; y2++)
                                for (int x2 = 0; x2 < NUM; x2 += 2, posByteImgArray++)
                                {
                                    temp = datosImagenDescomprimida[posByteImgArray];
                                    //pongo los pixels de dos en dos porque se leen diferente de la paleta
                                    //pixel izquierdo

                                    pos = (x1 + x2) * BYTESPERPIXEL + (y1 + y2) * bytesPorLado;
                                    color = paleta.Colores[temp & 0xF];

                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = color.A;

                                    //pixel derecho
                                    pos += BYTESPERPIXEL;

                                    color = paleta.Colores[(temp & 0xF0) >> 4];
                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = color.A;



                                }

                }));

            }


            return bmpTiles;
        }
        /// <summary>
        /// Convert Bitmap To 4BPP Byte Array
        /// </summary>
        /// <param name="img"></param>
        /// <param name="paleta"></param>
        /// <returns></returns>
        public static byte[] GetDatosDescomprimidos(Bitmap img)
        {//por testear
            if (img == null)
                throw new ArgumentNullException("img");
            Paleta paleta = Paleta.GetPaletaEmpty();
            byte[] toReturn = new byte[(img.Height * img.Width) >> 1];
            int index = 0;
            Color temp;
            byte outValue = 0, index2;
            bool buscandoPaleta;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width / 2; j++)
                {

                    outValue = 0;
                    index2 = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        temp = img.GetPixel((j * 2) + k, i);

                        buscandoPaleta = true;
                        for (int l = 0; l < paleta.Colores.Length && buscandoPaleta; l++)
                            if (temp.ToArgb().Equals(paleta.Colores[l].ToArgb()))
                            {
                                outValue = (byte)(index2 << (k * 4));
                                buscandoPaleta = false;
                            }
                        index2++;
                    }
                    toReturn[index] = (byte)(toReturn[index] | outValue);
                }
                index++;
            }

            return toReturn;
        }
        /*primero hare el que tiene todos los datos y luego la sobrecarga*/
        //poner los get para RomGBA

        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetPointerImg, params Paleta[] paletas)
        {
            return new BloqueImagen(offsetPointerImg, Lz77.DescomprimirDatosLZ77(rom.Datos, Offset.GetOffset(rom, offsetPointerImg)), paletas);
        }

        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetPointerImg, params Hex[] pointersPaletas)
        {
            return GetBloqueImagen(rom, offsetPointerImg, Paleta.GetPaletas(rom, pointersPaletas));
        }
        //poner los set para ROMGBA
        public static void SetBloqueImagen(RomGBA rom, BloqueImagen bloqueImg, bool hacerTambienSetPaletas = false)
        {
            SetBloqueImagen(rom, bloqueImg.OffsetPointerImg, bloqueImg.DatosDescomprimidos);
            if (hacerTambienSetPaletas)
                for (int i = 0; i < bloqueImg.paletas.Count; i++)
                    Paleta.SetPaleta(rom, bloqueImg.paletas[i]);
        }
        public static void SetBloqueImagen(RomGBA rom, Hex offsetPointerImg, byte[] datosDescomprimidosImg)
        {
            //borro los anteriores datos
            Hex inicioDatos = Offset.GetOffset(rom, offsetPointerImg);
            int lenghtBytesAnteriores;
            byte[] datosNuevos = Lz77.ComprimirDatosLZ77(datosDescomprimidosImg);
            if (inicioDatos > -1)//si hay datos 
            {
                lenghtBytesAnteriores = Lz77.LongitudDatosLZ77(rom.Datos, inicioDatos);
                if (lenghtBytesAnteriores != datosNuevos.Length)
                    throw new ArgumentException("La nueva imagen tiene que tener las medidas de la anterior");
                BloqueBytes.RemoveBytes(rom, inicioDatos, lenghtBytesAnteriores);
            }
            //pongo los nuevos donde quepan
            //actualizo pointer
            Offset.SetOffset(rom, offsetPointerImg, BloqueBytes.SetBytes(rom, datosNuevos));
        }


        //obtener offsets a los datos de la imagen
        public static Hex GetOffsetPointerImg(RomGBA rom, Hex offset, Hex posicion)
        {
            return (posicion << 3) + offset;
        }

        //para ir rapido :)
        public static implicit operator Bitmap(BloqueImagen bloqueImg)
        {
            return bloqueImg[0];
        }
        //para ir rapido :) a ver como se ve a al practica :D
        public static Bitmap operator +(BloqueImagen bloqueImg, Paleta paleta)
        {
            return BuildBitmap(bloqueImg.DatosDescomprimidos, paleta);
        }
    }


    //codigo sacado de NSE_Framework.Data 2.0
    public static class Lz77
    {
        // For picking what type of Compression Look-up we want
        public enum CompressionMode
        {
            Old, // Good
            New  // Perfect!
        }

        public static byte[] DescomprimirDatosLZ77(byte[] datos, Hex offsetInicio)
        {
            const byte BYTECOMPRESSIONLZ77 = 0x10;
            byte[] data;
            StringBuilder strWatch = new StringBuilder();
            int dataLength;
            int offset;
            int i, pos;
            byte[] r;
            int length;
            int start;
            if (datos[offsetInicio] == BYTECOMPRESSIONLZ77)
            {
                dataLength = LongitudDatosLZ77(datos, offsetInicio);
                data = new byte[dataLength];

                offset = offsetInicio + 4;
                i = 0;
                pos = 8;
                unsafe
                {
                    fixed (byte* ptrDatosComprimidos = datos)
                    {
                        fixed (byte* ptrDatosDescomprimidos = data)
                        {
                            while (i < dataLength)
                            {
                                if (pos != 8)
                                {
                                    if (strWatch[pos] == '0')
                                    {

                                        ptrDatosDescomprimidos[i] = ptrDatosComprimidos[offset];
                                    }
                                    else
                                    {

                                        r = new byte[] { ptrDatosComprimidos[offset], ptrDatosComprimidos[offset + 1] };
                                        length = r[0] >> 4;
                                        start = ((r[0] - ((r[0] >> 4) << 4)) << 8) + r[1];
                                        i = AmmendArray(data, i, i - start - 1, length + 3);
                                        offset++;
                                    }
                                    offset++;
                                    i++;
                                    pos++;

                                }
                                else
                                {
                                    strWatch.Clear();
                                    strWatch.Append(Convert.ToString(datos[offset], 2));
                                    if (strWatch.Length < 8)
                                    {
                                        strWatch.Insert(0, "0", 8 - strWatch.Length);
                                    }
                                    offset++;
                                    pos = 0;
                                }
                            }


                        }
                    }
                }

            }
            else
            {
                throw new Exception("This data is not Lz77 compressed!");
            }

            return data;
        }

        public static int LongitudDatosLZ77(byte[] datos, Hex offsetInicio)
        {
            return Serializar.ToInt(new Byte[] { datos[offsetInicio + 1], datos[offsetInicio + 2], datos[offsetInicio + 3], 0x0 });
        }

        static int AmmendArray(byte[] bytes, int index, int start, int length)
        {
            int a = 0; // Act
            int r = 0; // Rel
            byte backup = 0;

            unsafe
            {
                fixed (byte* ptrBytes = bytes)
                {
                    if (index > 0)
                    {
                        backup = ptrBytes[index - 1];
                    }

                    while (a != length)
                    {
                        if (index + r >= 0 && start + r >= 0 && index + a < bytes.Length)
                        {
                            if (start + r >= index)
                            {
                                r = 0;
                                ptrBytes[index + a] = ptrBytes[start + r];
                            }
                            else
                            {
                                ptrBytes[index + a] = ptrBytes[start + r];
                                backup = ptrBytes[index + r];
                            }
                        }
                        a++;
                        r++;
                    }
                }
            }
            index += length - 1;
            return index;
        }




        public static byte[] ComprimirDatosLZ77(byte[] datos, CompressionMode Mode = CompressionMode.New)
        {
            const byte BYTECOMPRESSLZ77= 0x10;
            byte[] header = BitConverter.GetBytes(datos.Length);
            List<byte> bytesComprimidos = new List<byte>();
            List<byte> PreBytes = new List<byte>();
            byte Watch = 0;
            byte ShortPosition = 2;
            int ActualPosition = 2;
            int match = -1;

            int BestLength = 0;

            // Adds the Lz77 header to the bytes 0x10 3 bytes size reversed
            bytesComprimidos.Add(BYTECOMPRESSLZ77);
            bytesComprimidos.Add(header[0]);
            bytesComprimidos.Add(header[1]);
            bytesComprimidos.Add(header[2]);

            // Lz77 Compression requires SOME starting data, so we provide the first 2 bytes
            PreBytes.Add(datos[0]);
            PreBytes.Add(datos[1]);

            // Compress everything
            while (ActualPosition < datos.Length)
            {
                //If we've compressed 8 of 8 bytes
                if (ShortPosition == 8)
                {
                    // Add the Watch Mask
                    // Add the 8 steps in PreBytes
                    bytesComprimidos.Add(Watch);
                    bytesComprimidos.AddRange(PreBytes);

                    Watch = 0;
                    PreBytes.Clear();

                    // Back to 0 of 8 compressed bytes
                    ShortPosition = 0;
                }
                else
                {
                    // If we are approaching the end
                    if (ActualPosition + 1 < datos.Length)
                    {
                        // Old NSE 1.x compression lookup
                        if (Mode == CompressionMode.Old)
                        {
                            match = SearchBytesOld(
                                datos,
                                ActualPosition,
                                Math.Min(4096, ActualPosition));
                        }
                        else
                        {
                            // New NSE 2.x compression lookup
                            match = SearchBytes(
                                        datos,
                                        ActualPosition,
                                        Math.Min(4096, ActualPosition), out BestLength);
                        }
                    }
                    else
                    {
                        match = -1;
                    }

                    // If we have NOT found a match in the compression lookup
                    if (match == -1)
                    {
                        // Add the byte
                        PreBytes.Add(datos[ActualPosition]);
                        // Add a 0 to the mask
                        Watch = BitConverter.GetBytes((int)Watch << 1)[0];

                        ActualPosition++;
                    }
                    else
                    {
                        // How many bytes match
                        int length = -1;

                        int start = match;
                        if (Mode == CompressionMode.Old || BestLength == -1)
                        {
                            // Old look-up technique
                            #region GetLength_Old
                            start = match;

                            bool Compatible = true;

                            while (Compatible == true && length < 18 && length + ActualPosition < datos.Length - 1)
                            {
                                length++;
                                if (datos[ActualPosition + length] != datos[ActualPosition - start + length])
                                {
                                    Compatible = false;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            // New lookup (Perfect Compression!)
                            length = BestLength;
                        }

                        // Add the rel-compression pointer (P) and length of bytes to copy (L)
                        // Format: L P P P
                        byte[] b = BitConverter.GetBytes(((length - 3) << 12) + (start - 1));

                        b = new byte[] { b[1], b[0] };
                        PreBytes.AddRange(b);

                        // Move to the next position
                        ActualPosition += length;

                        // Add a 1 to the bit Mask
                        Watch = BitConverter.GetBytes(((int)Watch << 1) + 1)[0];
                    }

                    // We've just compressed 1 more 8
                    ShortPosition++;
                }


            }

            // Finnish off the compression
            if (ShortPosition != 0)
            {
                //Tyeing up any left-over data compression
                Watch = BitConverter.GetBytes((int)Watch << (8 - ShortPosition))[0];

                bytesComprimidos.Add(Watch);
                bytesComprimidos.AddRange(PreBytes);
            }

            // Return the Compressed bytes as an array!
            return bytesComprimidos.ToArray();
        }

        static int SearchBytesOld(byte[] Data, int Index, int Length)
        {
            int found = -1;
            int pos = 2;

            if (Index + 2 < Data.Length)
            {
                while (pos < Length + 1 && found == -1)
                {
                    if (Data[Index - pos] == Data[Index] && Data[Index - pos + 1] == Data[Index + 1])
                    {

                        if (Index > 2)
                        {
                            if (Data[Index - pos + 2] == Data[Index + 2])
                            {
                                found = pos;
                            }
                            else
                            {
                                pos++;
                            }
                        }
                        else
                        {
                            found = pos;
                        }


                    }
                    else
                    {
                        pos++;
                    }
                }

                return found;
            }
            else
            {
                return -1;
            }

        }

        static int SearchBytes(byte[] Data, int Index, int Length, out int match)
        {

            int pos = 2;
            match = 0;
            int found = -1;

            if (Index + 2 < Data.Length)
            {
                while (pos < Length + 1 && match != 18)
                {
                    if (Data[Index - pos] == Data[Index] && Data[Index - pos + 1] == Data[Index + 1])
                    {

                        if (Index > 2)
                        {
                            if (Data[Index - pos + 2] == Data[Index + 2])
                            {
                                int _match = 2;
                                bool Compatible = true;
                                while (Compatible == true && _match < 18 && _match + Index < Data.Length - 1)
                                {
                                    _match++;
                                    if (Data[Index + _match] != Data[Index - pos + _match])
                                    {
                                        Compatible = false;
                                    }
                                }
                                if (_match > match)
                                {
                                    match = _match;
                                    found = pos;
                                }

                            }
                            pos++;
                        }
                        else
                        {
                            found = pos;
                            match = -1;
                            pos++;
                        }


                    }
                    else
                    {
                        pos++;
                    }
                }

                return found;
            }
            else
            {
                return -1;
            }

        }





    }

    public class Paleta
    {
        public const int TAMAÑOPALETACOMPRIMIDA = 32;
        public static Color BackgroundColorDefault = Color.Transparent;
        public const int TAMAÑOPALETA = 16;

        Hex offsetPointerPaleta;
        Color[] paleta;

        public Paleta(Color[] paleta)
            : this(0, paleta)
        {
        }
        public Paleta(Hex offsetPointerPaleta, Color[] paleta)
        {
            if (paleta == null)
                throw new ArgumentNullException("paleta");
            OffsetPointerPaleta = offsetPointerPaleta;
            Colores = paleta;
        }
        public Color this[int index]
        {
            get { return paleta[index]; }
            set { paleta[index] = value; }
        }


        public Hex OffsetPointerPaleta
        {
            get
            {
                return offsetPointerPaleta;
            }
            set
            {
                if (offsetPointerPaleta < 0)
                    throw new ArgumentOutOfRangeException();
                offsetPointerPaleta = value;
            }
        }

        public Color[] Colores
        {
            get
            {
                return paleta;
            }
            set
            {
                if (value == null || value.Length != TAMAÑOPALETA)
                    throw new ArgumentException("error  con la paleta, puede ser null o el tamaño no es el correcto");
                paleta = value;
            }
        }



        public static void ReemplazaColores(Paleta paletaAReemplazarColores, Paleta paletaCogerColores)
        {
            if (paletaAReemplazarColores == null || paletaCogerColores == null) throw new ArgumentNullException();
            for (int i = 0; i < TAMAÑOPALETA; i++)
                paletaAReemplazarColores.paleta[i] = paletaCogerColores.paleta[i];
        }
        public static Paleta GetPaletaEmpty()
        {
            return new Paleta(new Color[TAMAÑOPALETA]);
        }
        //necesito una paleta para poder ver cualquier img sin su paleta original aunque sea mal...
        public static Paleta[] GetPaletas(RomGBA rom, Hex[] offsetsPointersPaletas, bool showBackgroundColor = false)
        {
            if (offsetsPointersPaletas == null)
                throw new ArgumentNullException();
            Paleta[] paletas = new Paleta[offsetsPointersPaletas.Length];
            for (int i = 0; i < paletas.Length; i++)
                paletas[i] = GetPaleta(rom, offsetsPointersPaletas[i], showBackgroundColor);
            return paletas;
        }
        public static Paleta GetPaleta(RomGBA rom, Hex offsetPointerPaleta, bool showBackgroundColor = false)
        {
            if (rom == null || offsetPointerPaleta < 0)
                throw new ArgumentException();
            byte[] bytesPaletaDescomprimidos = Lz77.DescomprimirDatosLZ77(rom.Datos, Offset.GetOffset(rom, offsetPointerPaleta));
            Color[] paletteColours = new Color[TAMAÑOPALETA];
            int startPoint = showBackgroundColor ? 0 : 1;
            ushort tempValue, r, g, b;
            Color colorPaleta;
            if (!showBackgroundColor)
            {
                paletteColours[0] = BackgroundColorDefault;
            }
            for (int i = startPoint; i < TAMAÑOPALETA; i++)
            {

                tempValue = (ushort)(bytesPaletaDescomprimidos[i * 2] + (bytesPaletaDescomprimidos[i * 2 + 1] << 8));
                r = (ushort)((tempValue & 0x1F) << 3);
                g = (ushort)((tempValue & 0x3E0) >> 2);
                b = (ushort)((tempValue & 0x7C00) >> 7);
                colorPaleta = Color.FromArgb(0xFF, r, g, b);
                paletteColours[i] = colorPaleta;

            }
            return new Paleta(offsetPointerPaleta, paletteColours);
        }


        public static void SetPaleta(RomGBA rom, Paleta paleta)
        {
            if (rom == null || paleta == null)
                throw new ArgumentNullException();
            Hex offset;
            const int LENGHT = 2;
            Color[] coloresPaleta = paleta.Colores;
            byte[] paletaComprimida = new byte[TAMAÑOPALETA << 1];
            int index = 0;
            int r, g, b;
            uint value;
            for (int i = 0; i < TAMAÑOPALETA; i++)
            {
                r = (coloresPaleta[i].R >> 3);
                g = (coloresPaleta[i].G << 2);
                b = (coloresPaleta[i].B << 7);
                value = (uint)(b | g | r);
                for (int j = 0; i < LENGHT; j++)
                {
                    paletaComprimida[index + j] = Convert.ToByte(value.ToString("X8").Substring(6 - (2 * j), 2), 16);
                }

                index += 2;
            }
            offset = Offset.GetOffset(rom, paleta.OffsetPointerPaleta);
            if (offset > -1)
                //borro los datos de la paleta ant
                BloqueBytes.RemoveBytes(rom, offset, paletaComprimida.Length);
            //busco y actualizo el pointer
            Offset.SetOffset(rom, paleta.OffsetPointerPaleta, BloqueBytes.SetBytes(rom, paletaComprimida));

        }



        public static implicit operator Color[] (Paleta paleta)
        {
            return paleta.paleta;
        }
        public static implicit operator Paleta(Color[] paleta)
        {
            return new Paleta(paleta);
        }
    }
}
