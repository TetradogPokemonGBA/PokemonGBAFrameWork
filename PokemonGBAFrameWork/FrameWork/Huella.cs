using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
    public class Huella
    {
        enum Variables { ImgHuella }
        enum LongitudHuella
        {
            Imagen=32,
        }
        static readonly Color[] PaletaHuella = new Color[] { Color.Transparent, Color.Black };
        static Huella()
        {
            Zona zonaHuellas = new Zona(Variables.ImgHuella);

            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x105E14, 0x105E8C);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x105DEC, 0x105E64);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xC0DBC);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x917C8, 0x917E8, 0x917E8);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x917C8, 0x917E8, 0x917E8);

            zonaHuellas.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x105F8C);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x105FB4);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xC0B80);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x919F8);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x919F8);

            Zona.DiccionarioOffsetsZonas.Add(zonaHuellas);


        }
        BloqueBytes bloqueImgHuella;
        Bitmap imgHuella;
        public Huella() : this(0, new byte[(int)LongitudHuella.Imagen]) { }//imagen transparente
        public Huella(Hex offsetInicio, byte[] datosImagen) : this(new BloqueBytes(offsetInicio, datosImagen))
        {

        }
        public Huella(BloqueBytes bloqueBytes)
        {
            BloqueImgHuella = bloqueBytes;
        }

        public Bitmap Imagen
        {
            get
            {
                return imgHuella;
            }
            set
            {
                if (value == null) throw new ArgumentNullException();
                if (value.Height != 16 || value.Width != 16) throw new ArgumentException("Las medidas son 16x16");
                imgHuella = value;
                RefreshBloqueImgHuella();
            }
        }

        public BloqueBytes BloqueImgHuella
        {
            get
            {
                return bloqueImgHuella;
            }

            set
            {
                if (value == null) throw new ArgumentNullException();
                if (value.Bytes.Length != (int)LongitudHuella.Imagen) throw new ArgumentException("La cantidad de bytes tiene que ser de "+(int)LongitudHuella.Imagen);
                bloqueImgHuella = value;
                RefreshImagen();
            }
        }
        /// <summary>
        /// Construye los bytes con la imagen
        /// </summary>
        public void RefreshBloqueImgHuella()
        {
            bloqueImgHuella.Bytes = WriteImage(Imagen);
        }
        /// <summary>
        /// Construye la imagen a partir del bloque de bytes
        /// </summary>
        public void RefreshImagen()
        {
            if (bloqueImgHuella.Bytes.Length != (int)LongitudHuella.Imagen) throw new ArgumentException("La cantidad de bytes tiene que ser de "+ (int)LongitudHuella.Imagen);
            imgHuella = ReadImage(bloqueImgHuella.Bytes);
        }
        public static Huella GetHuella(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            //le el offset del pointer que toca
            Hex offsetInicio = GetOffsetHuella(rom, edicion, compilacion, posicion);
            //lee los bytes de la imagen del offset leido
            return new Huella(BloqueBytes.GetBytes(rom, offsetInicio, (int)LongitudHuella.Imagen));
        }
        /// <summary>
        /// Obtiene el offset de la lista de pointers.
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="posicion"></param>
        /// <returns></returns>
        public static Hex GetOffsetHuella(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
           return Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion) + posicion * (int)Longitud.Offset);
        }
        public static bool PoscionIsOK(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            //asi puedo calcular el numero de pokemons que hay sin hacer faena en vano.
            const string MARCAFIN = "FFFFFFFFFFFFFFFF";
            return GetOffsetHuella(rom, edicion, compilacion, posicion) != MARCAFIN;
        }

        static Bitmap ReadImageFunciona(byte[] bytesHuella)
        {//optimizar mas adelante :D
            Bitmap bmpHuella = new Bitmap(16, 16);
            int mitadY = bmpHuella.Height / 2;
            int inicioY = 0;
            int finY = mitadY;
            int pos = 0;
            //tengo 0xFF bytes para formar la imagen
            bool[] bits;
            for (int i = 0; i < 2; i++)
            {
                //  primero arriba y luego abajo
                for (int y = inicioY; y < finY; y++)
                {
                    bits = bytesHuella[pos].ToBits();//hace la parte izquierda 
                    for (int x = 0; x < mitadY; x++)
                        if (bits[x])
                            bmpHuella.SetPixel(x, y, Color.Black);
                    bits = bytesHuella[pos + mitadY].ToBits();//hace la parte derecha
                    for (int x = mitadY, xAux = 0; x < bmpHuella.Width; x++, xAux++)
                        if (bits[xAux])
                            bmpHuella.SetPixel(x, y, Color.Black);
                    pos++;
                }
                //13
                //24
                //un byte arriba otro abajo
                pos += mitadY;
                inicioY += mitadY;
                finY += mitadY;
            }

            return bmpHuella;
        }
        static Bitmap ReadImage(byte[] bytesHuella)
        {//no funciona...
            Bitmap bmpHuella = new Bitmap(16, 16);
            bmpHuella.SetBytes(DescomprimirBytesImgRGBA(ConvertToImgBytes(bytesHuella)));
            return bmpHuella;
        }
        static byte[] ConvertToImgBytes(byte[] bytesGBA)
        {
            const int XMEDIO = 8;
            const int XFIN = 16;
            byte[] bytesImgComprimidos = new byte[bytesGBA.Length];

            //los pone por orden natural
            unsafe
            {
                byte* ptrBytesGBAIzquierda, ptrBytesGBADerecha, ptrBytesImg;
                fixed (byte* ptBytesGBA = bytesGBA, ptBytesImg = bytesImgComprimidos)
                {
                    ptrBytesGBAIzquierda = ptBytesGBA;
                    ptrBytesGBADerecha = ptBytesGBA;
                    ptrBytesImg = ptBytesImg;
                    //los pone por orden natural
                    for (int i = 0; i < 2; i++)//primero arriba y luego abajo :D
                    {
                        ptrBytesGBADerecha += XMEDIO;
                        for (int x = 0; x < XFIN; x += 2)
                        {
                            *ptrBytesImg = *ptrBytesGBAIzquierda;
                            ptrBytesImg++;
                            ptrBytesGBAIzquierda++;
                            *ptrBytesImg = *ptrBytesGBADerecha;
                            ptrBytesImg++;
                            ptrBytesGBADerecha++;
                        }
                        ptrBytesGBAIzquierda += XMEDIO;//asi empieza la segunda mitad ya que estaba por un cuarto
                    }

                }
            }
            return bytesImgComprimidos;
        }

        private static byte[] DescomprimirBytesImgRGBA(byte[] bytesImgComprimidos)
        {
            const int RGBA = 4;
            const byte ON = 0xFF;//color on :)
            byte[] bytesImgDescomprimidosRGBA = new byte[bytesImgComprimidos.Length * RGBA * 8];//RGBA* 8bits
            bool[] bitsColor;
            unsafe
            {
                byte* ptrBytesImgComprimidos, ptrBytesImgDescompridos;
                bool* ptrBitsColor;
                fixed (byte* ptBytesImgComprimidos = bytesImgComprimidos, ptBytesImgDescomprimidos = bytesImgDescomprimidosRGBA)
                {
                    ptrBytesImgComprimidos = ptBytesImgComprimidos;
                    ptrBytesImgDescompridos = ptBytesImgDescomprimidos;
                    for (int i = 0; i < bytesImgComprimidos.Length; i++)
                    {
                        bitsColor = (*ptrBytesImgComprimidos).ToBits();
                        fixed (bool* ptBitsColor = bitsColor)
                        {
                            ptrBitsColor = ptBitsColor;
                            for (int j = 0; j < bitsColor.Length; j++)
                            {
                                if (*ptrBitsColor)
                                {
                                    for (int k = 0; k < RGBA; k++)
                                    {
                                        *ptrBytesImgDescompridos = ON;
                                        ptrBytesImgDescompridos++;
                                    }
                                }
                                else ptrBytesImgDescompridos += RGBA;//transparente
                                ptrBitsColor++;
                            }
                        }
                        ptrBytesImgComprimidos++;
                    }
                }
            }
            return bytesImgDescomprimidosRGBA;
        }

        /// <summary>
        /// Escribe los datos de la huella en la rom
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="huella">se usara el offset para escribir los bytes que contiene en la rom y actualizar el pointer con la direccion que tenga el bloque</param>
        /// <param name="posicion"></param>
        /// <param name="construirBytesConLaImagen"> si es false usa los bytes del bloque, si es true refresca los bytes del bloque con la imagen</param>
        public static void SetHuella(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Huella huella, Hex posicion, bool construirBytesConLaImagen = true)
        {
            if (huella == null || !construirBytesConLaImagen && huella.BloqueImgHuella.Bytes.Length != (int)LongitudHuella.Imagen || rom == null || edicion == null || posicion < 0) throw new ArgumentException();
            //actualizo el pointer
            Offset.SetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion) + posicion * (int)Longitud.Offset, huella.BloqueImgHuella.OffsetInicio);

            if (construirBytesConLaImagen)
                huella.RefreshBloqueImgHuella();//aplico los cambios de la imagen en los bytes
           // else huella.RefreshImagen();//aplico los cambios de los bytes en la imagen
            //pongo los bytes en su sitio :D
            BloqueBytes.SetBytes(rom,huella.BloqueImgHuella);
        }
        static byte[] WriteImage(Bitmap bmp)
        {
            return ConvertToGBA(ComprimirBytesImg(bmp.GetBytes()));
        }
        private static byte[] ComprimirBytesImg(byte[] bytesImgDescomprimidosRGBA)
        {
            const int RGBA = 4, BITSBYTE = 8;
            const byte OFF = 0x0;//color off :)
            byte[] bytesImgComprimidos = new byte[(int)LongitudHuella.Imagen];
            bool[] bitsColor;

            unsafe
            {
                byte* ptrBytesImgComprimidos, ptrBytesImgDescompridos;
                bool* ptrBitsColor;
                fixed (byte* ptBytesImgComprimidos = bytesImgComprimidos, ptBytesImgDescomprimidos = bytesImgDescomprimidosRGBA)
                {
                    ptrBytesImgComprimidos = ptBytesImgComprimidos;
                    ptrBytesImgDescompridos = ptBytesImgDescomprimidos;
                    for (int i = 0; i < bytesImgComprimidos.Length; i++)
                    {
                        //formo el color
                        bitsColor = new bool[BITSBYTE];
                        fixed (bool* ptBitsColor = bitsColor)
                        {
                            ptrBitsColor = ptBitsColor;
                            for (int j = 0; j < BITSBYTE; j++)
                            {
                                //R is != OFF then is ON
                                if (*ptrBytesImgDescompridos != OFF)
                                {
                                    *ptrBitsColor = true;
                                    ptrBytesImgDescompridos += RGBA;//avanzo al siguiente color
                                }
                                else
                                {
                                    ptrBytesImgDescompridos++;
                                    //G is != OFF then is ON
                                    if (*ptrBytesImgDescompridos != OFF)
                                    {
                                        *ptrBitsColor = true;
                                        ptrBytesImgDescompridos += RGBA - 1;//avanzo al siguiente color
                                    }
                                    else
                                    {
                                        ptrBytesImgDescompridos++;
                                        //B is != OFF then is ON
                                        if (*ptrBytesImgDescompridos != OFF)
                                        {
                                            *ptrBitsColor = true;
                                            ptrBytesImgDescompridos += RGBA - 2;//avanzo al siguiente color
                                        }
                                        else
                                        {
                                            ptrBytesImgDescompridos += 2;//A y siguiente color
                                            //WHITE or TRANSPERENT me salto A porque no influye

                                        }

                                    }

                                }

                                ptrBitsColor++;
                            }
                        }
                        //lo asigno
                        *ptrBytesImgComprimidos = bitsColor.ToByte();
                        ptrBytesImgComprimidos++;
                    }
                }
            }
            return bytesImgComprimidos;
        }
        static byte[] ConvertToGBA(byte[] bytesImgComprimidos)
        {
            const int XMEDIO = 8;
            const int XFIN = 16;
            byte[] bytesGBA = new byte[bytesImgComprimidos.Length];
            //los pone por orden en el cuadrado que le toca :D
            unsafe
            {
                byte* ptrBytesGBAIzquierda, ptrBytesGBADerecha, ptrBytesImg;
                fixed (byte* ptBytesGBA = bytesGBA, ptBytesImg = bytesImgComprimidos)
                {
                    ptrBytesGBAIzquierda = ptBytesGBA;
                    ptrBytesGBADerecha = ptBytesGBA;
                    ptrBytesImg = ptBytesImg;
                    //los pone por orden natural
                    for (int i = 0; i < 2; i++)//primero arriba y luego abajo :D
                    {
                        ptrBytesGBADerecha += XMEDIO;
                        for (int x = 0; x < XFIN; x += 2)
                        {
                            *ptrBytesGBAIzquierda = *ptrBytesImg;
                            ptrBytesImg++;
                            ptrBytesGBAIzquierda++;
                            *ptrBytesGBADerecha = *ptrBytesImg;
                            ptrBytesImg++;
                            ptrBytesGBADerecha++;
                        }
                        ptrBytesGBAIzquierda += XMEDIO;
                    }

                }
            }
            return bytesGBA;
        }
    }
}
