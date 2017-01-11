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
        public Huella() : this(0, new byte[0xFF]) { }//imagen transparente
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
                if (value.Bytes.Length != 0xFF) throw new ArgumentException("La cantidad de bytes tiene que ser de 255");
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
            if (bloqueImgHuella.Bytes.Length != 0xFF) throw new ArgumentException("La cantidad de bytes tiene que ser de 255");
            Imagen = ReadImage(bloqueImgHuella.Bytes);
        }
        public static Huella GetHuella(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            Hex offsetInicio = Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion) + (posicion - 1) * (int)Longitud.Offset);
            return new Huella(BloqueBytes.GetBytes(rom, offsetInicio, "FF"));
        }
        static Bitmap ReadImage(byte[] bytesHuella)
        {
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
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="huella"></param>
        /// <param name="posicion"></param>
        /// <param name="construirBytesConLaImagen"> si es false usa los bytes del bloque, si es true refresca los bytes del bloque con la imagen</param>
        public static void SetHuella(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Huella huella, Hex posicion, bool construirBytesConLaImagen = true)
        {
            if (huella == null || !construirBytesConLaImagen && huella.BloqueImgHuella.Bytes.Length != 0xFF || rom == null || edicion == null || posicion < 0) throw new ArgumentException();
            Hex offsetInicio = Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion) + (posicion - 1) * (int)Longitud.Offset);
            if (construirBytesConLaImagen)
                huella.RefreshBloqueImgHuella();//aplico los cambios de la imagen en los bytes
           // else huella.RefreshImagen();//aplico los cambios de los bytes en la imagen
            //pongo los bytes en su sitio :D
            BloqueBytes.SetBytes(rom, offsetInicio, huella.BloqueImgHuella.Bytes);
        }
        static byte[] WriteImage(Bitmap bmp)
        {
            return ConvertToGBA(ComprimirBytesImg(bmp.GetBytes()));
        }
        private static byte[] ComprimirBytesImg(byte[] bytesImgDescomprimidosRGBA)
        {
            const int RGBA = 4, BITSBYTE = 8;
            const byte OFF = 0x0;//color off :)
            byte[] bytesImgComprimidos = new byte[bytesImgDescomprimidosRGBA.Length / RGBA * BITSBYTE];//dividir entre 8 porque cada bit es un color y entre 4 porque cada 4 bytes forman un color
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
