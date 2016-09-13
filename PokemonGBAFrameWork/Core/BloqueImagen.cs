﻿using Gabriel.Cat;
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
        public enum LongitudImagen
        {
            //mirar de poner los tamaños que hay
            L64 = 64,
            L48=48,//a ver si se usa :D
            L32 = 32,
            L24=24,
            L16 = 16,
            L8 = 8
        }

        
        BloqueBytes bloqueDatosDescomprimidos;
        Llista<Paleta> paletas;//poder crear una imagen sin paletas porque a veces no hay...cuando estas investigando a veces falta la paleta y hay que ir probando
        //campos calculados para ahorrar computación
        LongitudImagen longitud;
        int tamañoImgComprimida;
        //zona constructores
        //hacer el basico y luego la sobrecarga
        BloqueImagen(BloqueBytes bloqueDatosComprimidos, IEnumerable<Paleta> paletas,bool aux)
        {
            bloqueDatosDescomprimidos = new BloqueBytes(bloqueDatosComprimidos.OffsetInicio, UtilsImage.DescomprimirDatosLZ77(bloqueDatosComprimidos.Bytes, 0));
            this.paletas = new Llista<Paleta>(paletas);
            DatosDescomprimidos = bloqueDatosDescomprimidos.Bytes;
        }
        BloqueImagen(Hex offsetInicio, byte[] datosDescomprimidos, IEnumerable<Paleta> paletas, bool aux)
        {
            bloqueDatosDescomprimidos = new BloqueBytes(offsetInicio, datosDescomprimidos);
            this.paletas = new Llista<Paleta>(paletas);
            DatosDescomprimidos = bloqueDatosDescomprimidos.Bytes;
        }
        public BloqueImagen(BloqueBytes bloqueDatosComprimidos, params Paleta[] paletas):this(bloqueDatosComprimidos,paletas,false)
        {
            this.longitud = CalculaLado(bloqueDatosDescomprimidos.Bytes);

        }
        public BloqueImagen(BloqueBytes bloqueDatosComprimidos, LongitudImagen longitud, params Paleta[] paletas) : this(bloqueDatosComprimidos, paletas,false)
        {
            this.longitud = longitud;
        }

        public BloqueImagen(Hex offsetInicio, byte[] datosDescomprimidos, params Paleta[] paletas) : this(offsetInicio,datosDescomprimidos, paletas, false)
        {

            this.longitud = CalculaLado(bloqueDatosDescomprimidos.Bytes);

        }
        public BloqueImagen(Hex offsetInicio, byte[] datosDescomprimidos, LongitudImagen longitud, params Paleta[] paletas) : this(offsetInicio, datosDescomprimidos, paletas, false)
        {
            this.longitud = longitud;
        }
        public BloqueImagen(byte[] bytesImgDescomprimida) : this(0, bytesImgDescomprimida)
        {
        }
        /*cuando no sea necesario longitudImagen quitarlo de todos los lados :D */
        public BloqueImagen(Hex offsetInicio, byte[] datosDescomprimidos,LongitudImagen longitud) : this(offsetInicio, datosDescomprimidos,longitud, new Paleta[] { })
        { }
        public BloqueImagen(Bitmap bmp, params Paleta[] paletas) : this(0, bmp, paletas)
        { }
        public BloqueImagen(Hex offsetInicio,Bitmap bmp,params Paleta[] paletas)
        {
            bloqueDatosDescomprimidos = new BloqueBytes(offsetInicio, GetDatosDescomprimidos(bmp, paletas[0]));
            longitud = CalculaLado(bmp);
            this.paletas = new Llista<Paleta>(paletas);
        }



        public Hex OffsetInicio
        {
            get
            {
                return bloqueDatosDescomprimidos.OffsetInicio;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                bloqueDatosDescomprimidos.OffsetInicio = value;
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

        public LongitudImagen Longitud
        {
            get
            {
                return longitud;
            }

           private set
            {
                longitud = value;
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
            get { return UtilsImage.ComprimirDatosLZ77(bloqueDatosDescomprimidos.Bytes); }
            set {
                try
                {
                    bloqueDatosDescomprimidos.Bytes = UtilsImage.DescomprimirDatosLZ77(value, 0);
                    tamañoImgComprimida = value.Length;
                }
                catch { throw new ArgumentException("Los datos no son validos!", "value"); }
        }
        }
        public byte[] DatosDescomprimidos
        {
            get { return bloqueDatosDescomprimidos.Bytes; }
            set {
                try
                {
                    tamañoImgComprimida = UtilsImage.ComprimirDatosLZ77(value).Length;
                    bloqueDatosDescomprimidos.Bytes = value;
                }
                catch { throw new ArgumentException("Los datos no son validos!","value"); }
            }
        }
        public Bitmap this[int indexPaleta]
        { get { return this[indexPaleta, Longitud]; } }
        public Bitmap this[int indexPaleta, LongitudImagen lado]
        {
            get { return BuildBitmap(DatosDescomprimidos,Paletas[indexPaleta],lado); }
        }

       public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida, Paleta paleta, LongitudImagen longitudLadoImagen = LongitudImagen.L64)
        {
            if (datosImagenDescomprimida == null || paleta == null)
                throw new ArgumentNullException();
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
       public static byte[] GetDatosDescomprimidos(Bitmap img, Paleta paleta)
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
        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetInicioImg,params Hex[] paletas)
        {
            return GetBloqueImagen(rom, offsetInicioImg, Paleta.GetPaletas(rom, paletas));
        }
        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetInicioImg, params Paleta[] paletas)
        {
            return new BloqueImagen(offsetInicioImg, UtilsImage.DescomprimirDatosLZ77(rom.Datos, offsetInicioImg), paletas);
        }
        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetInicioImg, LongitudImagen lado, params Hex[] paletas)
        {
            return GetBloqueImagen(rom, offsetInicioImg, lado, Paleta.GetPaletas(rom,paletas));
        }
        public static BloqueImagen GetBloqueImagen(RomGBA rom, Hex offsetInicioImg, LongitudImagen lado, params Paleta[] paletas)
        {
            return new  BloqueImagen(offsetInicioImg, UtilsImage.DescomprimirDatosLZ77(rom.Datos,offsetInicioImg), lado, paletas);
        }
        //poner los set para ROMGBA
        public static void SetBloqueImagen(RomGBA rom, BloqueImagen bloqueImg,bool hacerTambienSetPaletas=false)
        {
            BloqueBytes.SetBytes(rom, bloqueImg.bloqueDatosDescomprimidos.OffsetInicio, bloqueImg.DatosComprimidos);
            if (hacerTambienSetPaletas)
                for (int i = 0; i < bloqueImg.paletas.Count; i++)
                    Paleta.SetPaleta(rom, bloqueImg.paletas[i]);
        }
        //para poder tener rapidamente el lado
        public static LongitudImagen CalculaLado(Bitmap img)
        {
            return (LongitudImagen)img.Width;
        }
        public static LongitudImagen CalculaLado(byte[] datosImagenDesomprimida)
        {//tiene que ser una campo calculado...de momento se queda pero provisional!!! cuando sea rapido ya no lo pediré porque seria una tonteria para el usuario
            LongitudImagen[] longitudes = (LongitudImagen[])Enum.GetValues(typeof(LongitudImagen));
            LongitudImagen longitud = LongitudImagen.L64;
            Paleta paleta = Paleta.GetPaletaEmpty();
            bool encontrado = false;
            for (int i = longitudes.Length - 1; i >= 0 && !encontrado; i--)
            {
                try
                {
                    BuildBitmap(datosImagenDesomprimida, paleta, longitudes[i]);
                    longitud = longitudes[i];
                    encontrado = true;
                }
                catch { }//si se pasa hace una excepcion

            }
            return longitud;
        }

        public static void SetBloqueImagen(RomGBA rom, Hex offsetImg, byte[] datosDescomprimidosImg)
        {
            BloqueBytes.SetBytes(rom, offsetImg, UtilsImage.ComprimirDatosLZ77(datosDescomprimidosImg));
        }

        //obtener offsets a los datos de la imagen
        public static Hex GetOffsetImg(RomGBA rom, Hex offset, Hex posicion)
        {
            return Offset.GetOffset(rom, (posicion << 3) + offset);
        }

        //para ir rapido :)
        public static implicit operator Bitmap(BloqueImagen bloqueImg)
        {
            return bloqueImg[0];
        }
        //para ir rapido :) a ver como se ve a al practica :D
        public static Bitmap operator +(BloqueImagen bloqueImg,Paleta paleta)
        {
            return BuildBitmap(bloqueImg.DatosDescomprimidos,paleta,bloqueImg.Longitud);
        }
    }
    /// <summary>
    /// Es una clase para manejar la parte en comun entre una imagen y una paleta :)
    /// </summary>
    public static class UtilsImage
    {
        public const byte BYTELZ77TYPE = 0x10;

        //codigo sacado de internet creditos:Jambo
        public static byte[] ComprimirDatosLZ77(byte[] datosDescomprimidos)
        {
            if (datosDescomprimidos == null)
                throw new ArgumentNullException();

            int compressedLength = 4;
            int oldLength, bufferlength, readBytes, bufferedBlocks;
            Stream outstream;
            byte[] inData, outbuffer;
            LZUtil.OffsetAndLenght offsetAndLenght;
            outstream = new MemoryStream();
            inData = new byte[datosDescomprimidos.Length];
            outstream.WriteByte(BYTELZ77TYPE);
            outstream.WriteByte((byte)(datosDescomprimidos.Length & 0xFF));
            outstream.WriteByte((byte)((datosDescomprimidos.Length >> 8) & 0xFF));
            outstream.WriteByte((byte)((datosDescomprimidos.Length >> 16) & 0xFF));
            unsafe
            {
                fixed (byte* instart = &inData[0])
                {
                    outbuffer = new byte[8 * 2 + 1];
                    outbuffer[0] = 0;
                    bufferlength = 1;
                    bufferedBlocks = 0;
                    readBytes = 0;
                    while (readBytes < datosDescomprimidos.Length)
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
                        offsetAndLenght = LZUtil.GetOccurrenceLength(instart + readBytes, (int)Math.Min(datosDescomprimidos.Length - readBytes, 0x12),
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
        // descompresion sacada de https://gist.github.com/Prof9/872e67a08e17081ca00e
        public static byte[] DescomprimirDatosLZ77(byte[] datos, Hex offsetInicio)
        {
            if (datos == null || offsetInicio < 0)
                throw new ArgumentException();
            if (datos[offsetInicio] != BYTELZ77TYPE)
                throw new ArgumentException("La direccion no pertenece al inicio de los datos comprimidos con LZ77!", "offsetInicio");
            BinaryReader brDatos;
            MemoryStream msDatosDescomprimidos;
            int size;
            int flagByte;
            ushort block;
            int count;
            int disp;
            long outPos;
            long copyPos;
            byte b;

            brDatos = new BinaryReader(new MemoryStream(datos));
            brDatos.BaseStream.Position = offsetInicio + 1;//salto el primer byte que sirve para comprovar si esta comprimido :)
            size = brDatos.ReadUInt16() | (brDatos.ReadByte() << 16);



            msDatosDescomprimidos = new MemoryStream(size);

            // Begin decompression.
            while (msDatosDescomprimidos.Length < size)
            {
                // Load flags for the next 8 blocks.
                flagByte = brDatos.ReadByte();

                // Process the next 8 blocks.
                for (int i = 0; i < 8 && msDatosDescomprimidos.Length < size/* If all data has been decompressed, stop.*/; i++)
                {
                    // Check if the block is compressed.
                    if ((flagByte & (0x80 >> i)) == 0)
                    {
                        // Uncompressed block; copy single byte.
                        msDatosDescomprimidos.WriteByte((byte)brDatos.ReadByte());
                    }
                    else
                    {
                        // Compressed block; read block.
                        block = brDatos.ReadUInt16();
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
            brDatos.Close();
            return msDatosDescomprimidos.GetAllBytes();
        }
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
    public class Paleta
    {
        public const int TAMAÑOPALETACOMPRIMIDA = 32;
        public static Color BackgroundColorDefault = Color.Transparent;
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
            Colores = paleta;
        }
        public Color this[int index]
        {
            get { return paleta[index]; }
            set { paleta[index] = value; }
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
        public static Paleta[] GetPaletas(RomGBA rom, Hex[] paletasHex, bool showBackgroundColor = false)
        {
            if (paletasHex == null)
                throw new ArgumentNullException();
            Paleta[] paletas = new Paleta[paletasHex.Length];
            for (int i = 0; i < paletas.Length; i++)
                paletas[i] = GetPaleta(rom, paletasHex[i], showBackgroundColor);
            return paletas;
        }
        public static Paleta GetPaleta(RomGBA rom, Hex offsetInicioPaleta, bool showBackgroundColor = false)
        {
            if (rom == null || offsetInicioPaleta < 0)
                throw new ArgumentException();
            byte[] bytesPaletaDescomprimidos = UtilsImage.DescomprimirDatosLZ77(rom.Datos, offsetInicioPaleta);
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
            BloqueBytes.SetBytes(rom, paleta.OffsetInicio, paletaComprimida);

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
