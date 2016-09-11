/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 21:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of BloqueImagen.
    /// </summary>
    public class BloqueImagen
    {
        public class Paleta
        {
            public const int TAMAÑOPALETACOMPRIMIDA = 32;
            public static Color BackgroundColorDefault = Color.White;
            public const int TAMAÑOPALETA = 16;
            Hex offsetInicio;
            Color[] paleta;
            public Paleta(Color[] paleta)
                : this(0, paleta)
            {
            }
            public Paleta(Hex offsetInicio, Color[] paleta)
            {
                if (paleta == null)
                    throw new ArgumentNullException("paleta");
                OffsetInicio = offsetInicio;
                ColoresPaleta = paleta;
            }
            public Color this[int index]
            {
                get { return paleta[index]; }
                set { paleta[index] = value; }
            }

            public static void ReemplazaColores(Paleta paletaAReemplazarColores, Paleta paletaCogerColores)
            {
                if (paletaAReemplazarColores == null || paletaCogerColores == null) throw new ArgumentNullException();
                for (int i = 0; i < TAMAÑOPALETA; i++)
                    paletaAReemplazarColores.paleta[i] = paletaCogerColores.paleta[i];
            }

            public Hex OffsetInicio
            {
                get
                {
                    return offsetInicio;
                }
                set
                {
                    if (offsetInicio < 0)
                        throw new ArgumentOutOfRangeException();
                    offsetInicio = value;
                }
            }
            public Hex OffsetFin
            { get { return OffsetInicio + TAMAÑOPALETACOMPRIMIDA; } }


            public Color[] ColoresPaleta
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
            /// <summary>
            /// Crea una nueva imagen con la paleta que hay actualmente.
            /// </summary>
            /// <param name="img"></param>
            /// <returns></returns>
            public Bitmap SetPaleta(Bitmap img)
            {
                //le pongo la paleta
                return BloqueImagen.GetImagen(BloqueImagen.GetDatosDescomprimidosImagen(img, this), this);

            }
            public static Paleta GetPaletaEmpty()
            {
                return new Paleta(new Color[TAMAÑOPALETA]);
            }


            public static Paleta GetPaleta(RomGBA rom, Hex offsetInicioPaleta, bool showBackgroundColor = false)
            {
                if (rom == null || offsetInicioPaleta < 0)
                    throw new ArgumentException();
                byte[] bytesPaletaDescomprimidos = BloqueImagen.GetDatosDescomprimidos(rom, offsetInicioPaleta);
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
                return new Paleta(offsetInicioPaleta, paletteColours);
            }


            public static void SetPaleta(RomGBA rom, Paleta paleta)
            {
                if (rom == null || paleta == null)
                    throw new ArgumentNullException();
                const int LENGHT = 2;
                Color[] coloresPaleta = paleta.ColoresPaleta;
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
                BloqueBytes.SetBytes(rom, paleta.OffsetInicio, paletaComprimida);

            }
            public static implicit operator Color[] (Paleta paleta)
            {
                return paleta.paleta;
            }
        }



        public enum LongitudImagen
        {
            //mirar de poner los tamaños que hay
            L64 = 64,
            //no se si existen...por mirar
            L32 = 32,
            L16 = 16,
            L8 = 8
        }
        const byte BYTELZ77TYPE = 0x10;
        Hex offsetInicio;
        byte[] datosImagenDescomprimida;
        List<Paleta> paletas;
        LongitudImagen longitud;
        int totalDatosComprimidos;
        public BloqueImagen(Hex offsetInicio, byte[] datosImagenDesomprimida,params Paleta[] paletas):this(offsetInicio,datosImagenDesomprimida,DameLongitud(datosImagenDesomprimida,paletas),paletas)
        {
        }

        private static LongitudImagen DameLongitud(byte[] datosImagenDesomprimida, Paleta[] paletas)
        {
            LongitudImagen[] longitudes = (LongitudImagen[])Enum.GetValues(typeof(LongitudImagen));
            LongitudImagen longitud=LongitudImagen.L64;
            bool encontrado=false;
            for (int i = longitudes.Length-1; i >=0&&!encontrado ; i--)
            {
                try
                {
                    GetImagen(datosImagenDesomprimida, paletas[0], longitudes[i]);
                    longitud = longitudes[i];
                    encontrado = true;
                }
                catch { }//si se pasa hace una excepcion

            } 
            return longitud;
        }

        public BloqueImagen(Hex offsetInicio, byte[] datosImagenDesomprimida,LongitudImagen longitud, params Paleta[] paletas)
        {
            if (datosImagenDesomprimida == null || paletas == null)
                throw new ArgumentNullException();
            if (paletas.Length == 0)
                throw new ArgumentException("Se necesita al menos una paleta");
            if (!ValidarDatosImagenDescomprimida(datosImagenDesomprimida))
                throw new ArgumentException("Los bytes no son de una imagen correcta");
            this.longitud = longitud;
            OffsetInicio = offsetInicio;
            this.datosImagenDescomprimida = datosImagenDesomprimida;
            this.paletas = new List<Paleta>(paletas);
            totalDatosComprimidos = -1;
        }
        public BloqueImagen(Hex offsetInicio, Bitmap img, params Paleta[] paletas) : this(offsetInicio, img, CalculaLado(img), paletas) { }



        public BloqueImagen(Hex offsetInicio, Bitmap img,LongitudImagen longitud, params Paleta[] paletas)
            : this(offsetInicio, BloqueImagen.GetDatosDescomprimidosImagen(img, paletas[0]),longitud, paletas)
        {
        }

        public Hex OffsetInicio
        {
            get
            {
                return offsetInicio;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                offsetInicio = value;
            }
        }
        public Hex OffsetFin
        {
            get { return OffsetInicio + TotalDatosComprimidos; }
        }
        public Bitmap this[int index]
        {
            get { return this[index, longitud]; }
            set { this[index, longitud]=value; }
        }
        /// <summary>
        /// Obtener la imagen con la paleta del index /establecer la imagen y añadir(al final de la lista) o reemplazar la paleta que sea
        /// </summary>
        public Bitmap this[int index,BloqueImagen.LongitudImagen longitud]
        {
            get
            {
                if (index < 0 || paletas.Count < index)
                    throw new ArgumentOutOfRangeException("index");
                return GetImagen(datosImagenDescomprimida, paletas[index],longitud);
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (index >= paletas.Count)
                    throw new ArgumentOutOfRangeException("index");
                //la valido
                datosImagenDescomprimida = GetDatosDescomprimidosImagen(value, paletas[index]);
                totalDatosComprimidos = -1;

            }
        }
        public Paleta GetPaleta(int index)
        {
            if (index < 0 || index >= paletas.Count)
                throw new ArgumentOutOfRangeException("index");
            return paletas[index];
        }

        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetDatos, Paleta paleta)
        {
            byte[] datosImg = GetDatosDescomprimidos(rom, offsetDatos); 
            return GetBloqueImagen(rom, offsetDatos, DameLongitud(datosImg, new Paleta[] { paleta }),paleta);
        }

        /// <summary>
        /// Pone la paleta al final a no ser que se diga la posición
        /// </summary>
        /// <param name="paleta"></param>
        /// <param name="index">si es negativo se pondrá al final</param>
        /// <returns></returns>
        public void AddPaleta(Paleta paleta, int index = -1)
        {
            if (paleta == null)
                throw new ArgumentNullException();
            if (index > paletas.Count)
                index = -1;
            if (index < 0)
                paletas.Insert(paletas.Count, paleta);
            else
                paletas.Insert(index, paleta);
        }
        public void ReplacePaleta(Paleta paletaNueva, int indexPaletaAReemplazar)
        {
            if (paletas.Count < indexPaletaAReemplazar || indexPaletaAReemplazar < 0)
                throw new ArgumentOutOfRangeException();
            if (paletaNueva == null)
                throw new ArgumentNullException();
            paletas.RemoveAt(indexPaletaAReemplazar);
            paletas.Insert(indexPaletaAReemplazar, paletaNueva);
        }

        public Bitmap GetBitmap(Color[] colors)
        {
            return GetImagen(datosImagenDescomprimida, new Paleta(colors));
        }

        public void RemovePaleta(int indexPaletaAEliminar)
        {
            paletas.RemoveAt(indexPaletaAEliminar);
        }

        public byte[] DatosImagenDescomprimida
        {
            get { return datosImagenDescomprimida; }
            set
            {
                if (!ValidarDatosImagenDescomprimida(value))
                    throw new ArgumentException("Los datos no son validos!!");
                datosImagenDescomprimida = value;
                totalDatosComprimidos = -1;

            }
        }

        internal int TotalDatosComprimidos
        {
            get
            {
                if (totalDatosComprimidos == -1)
                    totalDatosComprimidos = ComprimirDatosLZ77(DatosImagenDescomprimida).Length;
                return totalDatosComprimidos;
            }

            private set
            {
                totalDatosComprimidos = value;
            }
        }

        public LongitudImagen Longitud
        {
            get
            {
                return longitud;
            }

            set
            {
                longitud = value;
            }
        }

        //de momento no se como validar asi que lo tendré private
        static bool ValidarDatosImagenDescomprimida(byte[] datosImagenDescomprimida)
        {
            if (datosImagenDescomprimida == null)
                throw new ArgumentNullException("datosImagenDescomprimida");
            bool valida = true;//de momento no se como se validan pero lo dejo para tenerlo :)
            return valida;
        }
        /// <summary>
        /// Convert Bitmap To 4BPP Byte Array
        /// </summary>
        /// <param name="img"></param>
        /// <param name="paleta"></param>
        /// <returns></returns>
        static byte[] GetDatosDescomprimidosImagen(Bitmap img, Paleta paleta)
        {
            if (img == null)
                throw new ArgumentNullException("img");
            if (paleta == null)
                throw new ArgumentNullException("paleta");

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
                        for (int l = 0; l < paleta.ColoresPaleta.Length && buscandoPaleta; l++)
                            if (temp == paleta.ColoresPaleta[l])
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
        static Bitmap GetImagen(byte[] datosImagenDescomprimida, Paleta paleta, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            if (datosImagenDescomprimida == null || paleta == null)
                throw new ArgumentNullException();
            const byte SINTRANSPARENCIA = 255;//no puede tener
            const int BYTESPERPIXEL = 4;
            const int NUM = 8;//poner algo mas descriptivo
            
            int longitudLado = (int)longitudLadoImagen;
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
                                    color = paleta.ColoresPaleta[temp & 0xF];

                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = SINTRANSPARENCIA;

                                    //pixel derecho
                                    pos += BYTESPERPIXEL;

                                    color = paleta.ColoresPaleta[(temp & 0xF0) >> 4];
                                    bytesBmp[pos] = color.B;
                                    bytesBmp[pos + 1] = color.G;
                                    bytesBmp[pos + 2] = color.R;
                                    bytesBmp[pos + 3] = SINTRANSPARENCIA;



                                }

                }));

            }


            return bmpTiles;
        }
        public static BloqueImagen GetBloqueImagen(byte[] datosImagenDescomprimida, Paleta paleta, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            return GetBloqueImagen(0, datosImagenDescomprimida, paleta, longitudLadoImagen);
        }
        public static BloqueImagen GetBloqueImagen(Hex offsetInicio, byte[] datosImagenDescomprimida, Paleta paleta, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            return GetBloqueImagen(offsetInicio, datosImagenDescomprimida, new Paleta[] { paleta }, longitudLadoImagen);
        }
        public static BloqueImagen GetBloqueImagen(byte[] datosImagenDescomprimida, IEnumerable<Paleta> paletas, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            return GetBloqueImagen(0, datosImagenDescomprimida, paletas, longitudLadoImagen);
        }
        public static BloqueImagen GetBloqueImagen(Hex offsetInicio, byte[] datosImagenDescomprimida, IEnumerable<Paleta> paletas, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            Paleta[] paletasArray = paletas.ToTaula();
            return new BloqueImagen(offsetInicio, GetImagen(datosImagenDescomprimida, paletasArray[0], longitudLadoImagen), paletasArray);
        }
        static byte[] GetDatosDescomprimidos(RomGBA rom, Hex offsetInicio)
        {
            if (rom == null || offsetInicio < 0)
                throw new ArgumentException();
            if (rom.Datos[offsetInicio] != BYTELZ77TYPE)
                throw new ArgumentException("La direccion no pertenece al inicio de los datos de la imagen!", "offsetInicio");
            BinaryReader brRom = new BinaryReader(new MemoryStream(rom.Datos));
            MemoryStream msDatosDescomprimidos;
            int size;
            int flagByte;
            ushort block;
            int count;
            int disp;
            long outPos;
            long copyPos;
            byte b;

            brRom.BaseStream.Position = offsetInicio + 1;
            size = brRom.ReadUInt16() | (brRom.ReadByte() << 16);



            msDatosDescomprimidos = new MemoryStream(size);

            // Begin decompression.
            while (msDatosDescomprimidos.Length < size)
            {
                // Load flags for the next 8 blocks.
                flagByte = brRom.ReadByte();

                // Process the next 8 blocks.
                for (int i = 0; i < 8 && msDatosDescomprimidos.Length < size/* If all data has been decompressed, stop.*/; i++)
                {
                    // Check if the block is compressed.
                    if ((flagByte & (0x80 >> i)) == 0)
                    {
                        // Uncompressed block; copy single byte.
                        msDatosDescomprimidos.WriteByte((byte)brRom.ReadByte());
                    }
                    else
                    {
                        // Compressed block; read block.
                        block = brRom.ReadUInt16();
                        // Get byte count.
                        count = ((block >> 4) & 0xF) + 3;
                        // Get displacement.
                        disp = ((block & 0xF) << 8) | ((block >> 8) & 0xFF);

                        // Save current position and copying position.
                        outPos = msDatosDescomprimidos.Position;
                        copyPos = outPos - disp - 1;

                        // Copy all bytes.
                        for (int j = 0; j < count; j++)
                        {
                            // Read byte to be copied.
                            msDatosDescomprimidos.Position = copyPos++;
                            b = (byte)msDatosDescomprimidos.ReadByte();

                            // Write byte to be copied.
                            msDatosDescomprimidos.Position = outPos++;
                            msDatosDescomprimidos.WriteByte(b);
                        }
                    }



                }

            }
            brRom.Close();
            return msDatosDescomprimidos.GetAllBytes();
        }

        // descompresion sacada de https://gist.github.com/Prof9/872e67a08e17081ca00e
        /// <summary>
        /// Lee y devuelve los datos comprimidos LZ77 de la rom descomprimidos
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="offsetInicioDatos"></param>
        /// <param name="paletas"></param>
        /// <returns></returns>
        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetInicioDatos, LongitudImagen longitudLadoImagen = LongitudImagen.L64, params Paleta[] paletas)
        {
            if (paletas == null || paletas.Length == 0)
                throw new ArgumentException();

            return new BloqueImagen(offsetInicioDatos, GetDatosDescomprimidos(rom, offsetInicioDatos),longitudLadoImagen, paletas);
        }

        public static void SetBloqueImagen(RomGBA rom, BloqueImagen img, bool añadirPaletasImg = true)
        {

            if (rom == null || img == null)
                throw new ArgumentNullException();
            BloqueBytes.SetBytes(rom, img.OffsetInicio, ComprimirDatosLZ77(img.DatosImagenDescomprimida));
            if (añadirPaletasImg)
                for (int i = 0; i < img.paletas.Count; i++)
                    Paleta.SetPaleta(rom, img.paletas[i]);
        }
        public static void SetBloqueImagen(RomGBA rom, Hex offsetInicio, Bitmap img, params Paleta[] paletas)
        {
            if (img == null || paletas == null)
                throw new ArgumentNullException();
            if (paletas.Length == 0)
                throw new ArgumentException("paletas");
            SetBloqueImagen(rom, new BloqueImagen(offsetInicio, GetDatosDescomprimidosImagen(img, paletas[0]), paletas));
        }
        public static void SetBloqueImagen(RomGBA rom, Hex offsetInicio, byte[] datosImgDescomprimida, params Paleta[] paletas)
        {
            SetBloqueImagen(rom, new BloqueImagen(offsetInicio, datosImgDescomprimida, paletas));
        }
        public static LongitudImagen CalculaLado(Bitmap img)
        {
            return (LongitudImagen)img.Width;
        }
        //codigo sacado de internet creditos:Jambo
        static byte[] ComprimirDatosLZ77(BloqueImagen bloqueImg)
        {
            return ComprimirDatosLZ77(bloqueImg.DatosImagenDescomprimida);
        }
        //codigo sacado de internet creditos:Jambo
        static byte[] ComprimirDatosLZ77(byte[] datosImagenDescomprimida)
        {
            if (datosImagenDescomprimida == null)
                throw new ArgumentNullException();

            int compressedLength = 4;
            int oldLength, bufferlength, readBytes, bufferedBlocks;
            Stream outstream;
            byte[] inData, outbuffer;
            LZUtil.OffsetAndLenght offsetAndLenght;
            outstream = new MemoryStream();
            inData = new byte[datosImagenDescomprimida.Length];
            outstream.WriteByte(BYTELZ77TYPE);
            outstream.WriteByte((byte)(datosImagenDescomprimida.Length & 0xFF));
            outstream.WriteByte((byte)((datosImagenDescomprimida.Length >> 8) & 0xFF));
            outstream.WriteByte((byte)((datosImagenDescomprimida.Length >> 16) & 0xFF));
            unsafe
            {
                fixed (byte* instart = &inData[0])
                {
                    outbuffer = new byte[8 * 2 + 1];
                    outbuffer[0] = 0;
                    bufferlength = 1;
                    bufferedBlocks = 0;
                    readBytes = 0;
                    while (readBytes < datosImagenDescomprimida.Length)
                    {
                        if (bufferedBlocks == 8)
                        {
                            outstream.Write(outbuffer, 0, bufferlength);
                            compressedLength += bufferlength;
                            outbuffer[0] = 0;
                            bufferlength = 1;
                            bufferedBlocks = 0;
                        }

                        oldLength = Math.Min(readBytes, 0x1000);
                        offsetAndLenght = LZUtil.GetOccurrenceLength(instart + readBytes, (int)Math.Min(datosImagenDescomprimida.Length - readBytes, 0x12),
                                                                     instart + readBytes - oldLength, oldLength);
                        if (offsetAndLenght.Lenght < 3)
                        {
                            outbuffer[bufferlength++] = *(instart + (readBytes++));
                        }
                        else
                        {
                            readBytes += offsetAndLenght.Lenght;
                            outbuffer[0] |= (byte)(1 << (7 - bufferedBlocks));
                            outbuffer[bufferlength] = (byte)(((offsetAndLenght.Lenght - 3) << 4) & 0xF0);
                            outbuffer[bufferlength] |= (byte)(((offsetAndLenght.Offset - 1) >> 8) & 0x0F);
                            bufferlength++;
                            outbuffer[bufferlength] = (byte)((offsetAndLenght.Offset - 1) & 0xFF);
                            bufferlength++;
                        }
                        bufferedBlocks++;
                    }
                    if (bufferedBlocks > 0)
                    {
                        outstream.Write(outbuffer, 0, bufferlength);
                        compressedLength += bufferlength;
                        while ((compressedLength % 4) != 0)
                        {
                            outstream.WriteByte(0);
                            compressedLength++;
                        }
                    }
                }
            }
            return outstream.GetAllBytes();

        }
        #region por entender
        /*falta entender que hace y ponerlos en su sitio!!!*/
        public static Hex GetOffsetImg(RomGBA rom, Hex offset, Hex posicion)
        {
            return Offset.GetOffset(rom,(posicion << 3) + offset);
        }
      

        #endregion
        //codigo sacado de internet creditos:Jambo
        static class LZUtil
        {
            public struct OffsetAndLenght
            {
                int offset;
                int lenght;

                public OffsetAndLenght(int offset, int lenght)
                {
                    this.offset = offset;
                    this.lenght = lenght;
                }
                public int Offset
                {
                    get
                    {
                        return offset;
                    }

                }

                public int Lenght
                {
                    get
                    {
                        return lenght;
                    }

                }
            }
            /// <summary>
            /// Determine the maximum size of a LZ-compressed block starting at newPtr, using the already compressed data
            /// starting at oldPtr. Takes O(inLength * oldLength) = O(n^2) time.
            /// </summary>
            /// <param name="newPtr">The start of the data that needs to be compressed.</param>
            /// <param name="newLength">The number of bytes that still need to be compressed.
            /// (or: the maximum number of bytes that _may_ be compressed into one block)</param>
            /// <param name="oldPtr">The start of the raw file.</param>
            /// <param name="oldLength">The number of bytes already compressed.</param>
            /// <param name="disp">The offset of the start of the longest block to refer to.</param>
            /// <param name="minDisp">The minimum allowed value for 'disp'.</param>
            /// <returns>The length of the longest sequence of bytes that can be copied from the already decompressed data.</returns>
            public static unsafe OffsetAndLenght GetOccurrenceLength(byte* newPtr, int newLength, byte* oldPtr, int oldLength, int minDisp = 1)
            {
                int disp;
                bool continua = newLength != 0;
                int maxLength = 0;
                byte* currentOldStart;
                int currentLength;
                disp = 0;

                // try every possible 'disp' value (disp = oldLength - i)
                for (int i = 0; i < oldLength - minDisp && continua; i++)
                {
                    // work from the start of the old data to the end, to mimic the original implementation's behaviour
                    // (and going from start to end or from end to start does not influence the compression ratio anyway)
                    currentOldStart = oldPtr + i;
                    currentLength = 0;
                    continua = true;
                    // determine the length we can copy if we go back (oldLength - i) bytes
                    // always check the next 'newLength' bytes, and not just the available 'old' bytes,
                    // as the copied data can also originate from what we're currently trying to compress.
                    for (int j = 0; j < newLength && continua; j++)
                    {

                        // stop when the bytes are no longer the same
                        if (*(currentOldStart + j) != *(newPtr + j))
                            continua = false;
                        else
                            currentLength++;
                    }

                    // update the optimal value
                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        disp = oldLength - i;

                        // if we cannot do better anyway, stop trying.
                        if (maxLength == newLength)
                            continua = false;
                    }
                }
                return new OffsetAndLenght(disp, maxLength);
            }

        }
    }

}
