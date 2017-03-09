using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//codigo adaptado de https://github.com/doom-desire
namespace PokemonGBAFrameWork
{
    //mirar de hacer lo que hace Sappy :3 y poder importar desde wave,midi y exportar tambien :D
    //poder añadir instrumentos,quitarlos y editarlos :D
    public class BloqueSonido : ObjectAutoId
    {
        enum Posicion
        {
            EstaComprimido = 0,
            RepetirCiclicamente = 2,
            SampleRate = 4,
            InicioRepeticion = 8,
            Length = 12,
            Data = 16,
            PointerHeader = 4,
            EndHeader = 8,
        }
        public static readonly Color[] ColoresOndaPorDefecto = { Color.Green, Color.Blue, Color.Red, Color.Yellow, Color.Violet, Color.Blue, Color.Brown, Color.HotPink, Color.Gray, Color.Magenta };
        static readonly sbyte[] Lookup = new sbyte[] { 0, 1, 4, 9, 16, 25, 36, 49, -64, -49, -36, -25, -16, -9, -4, -1 };
        public static TwoKeysList<string, string, ushort> DiccionarioHeaderSignificadoCanalesMax = new TwoKeysList<string, string, ushort>();
        static readonly byte[] EndHeader = { 0xFF, 0x0, 0xFF, 0x0 };
        const int LENGTHIDHEADER = 4;

        string header;
        Hex offset;
        bool estaComprimido;
        bool repetirCiclicamente;
        int sampleRate;
        int inicioRepeticion;
        int length;
        SByte[][] datos;
        ushort numeroDeCanalesMaximos;

        private BloqueSonido()
        {
            Header = null;
        }

        public BloqueSonido(MemoryStream msWaveFile, bool repetirCiclicamente = false, int inicioRepeticion = 0) : this()
        {
            const UInt16 PCMFORMAT = 1;
            BinaryReader brOnda = new BinaryReader(msWaveFile);
            ushort bitsPerSample;
            //  ' read RIFF header
            if (brOnda.ReadUInt32() != 0x46464952)
                throw new Exception("This is not a WAVE file!");
            if (brOnda.ReadInt32() + 8 != brOnda.BaseStream.Length)
                throw new Exception("Invalid file length!");

            if (brOnda.ReadUInt32() != 0x45564157)
                throw new Exception("This is not a WAVE file!");


            //   ' read fmt chunk
            if (brOnda.ReadUInt32() != 0x20746D66)
                throw new Exception("Expected fmt chunk!");

            if (brOnda.ReadUInt32() != 16)
                throw new Exception("Invalid fmt chunk!");

            if (brOnda.ReadUInt16() != PCMFORMAT)
                //' only PCM format allowed
                throw new Exception("Cry must be in PCM format!");

            numeroDeCanalesMaximos = brOnda.ReadUInt16();
            datos = new sbyte[numeroDeCanalesMaximos][];
            sampleRate = brOnda.ReadInt32();

            if (brOnda.ReadUInt32() != sampleRate)
                throw new Exception("Invalid fmt chunk!");

            brOnda.ReadUInt16();
            bitsPerSample = brOnda.ReadUInt16();
            if (bitsPerSample != 8)
                // ' for now, only 8 bit PCM data
                throw new Exception("Cries must be 8-bit WAVE files! Got {bitsPerSample}-bit instead.");


            // ' data chunk
            if (brOnda.ReadUInt32() != 0x61746164)
                throw new Exception("Expected data chunk!!");

            for (int j = 0; j < numeroDeCanalesMaximos; j++)//mirar si es asi :D
            {
                length = brOnda.ReadInt32();

                datos[j] = new SByte[length - 1];
                for (int i = 0; i < length; i++)
                    // ' read 8-bit unsigned PCM and convert to GBA signed form
                    datos[j][i] = (SByte)(brOnda.ReadByte() - 128);
            }
            //resetting some other properties just in case
            this.repetirCiclicamente = repetirCiclicamente;
            this.inicioRepeticion = inicioRepeticion;
        }
        public Hex Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public bool EstaComprimido
        {
            get
            {
                return estaComprimido;
            }

            set
            {
                estaComprimido = value;
            }
        }

        public bool RepetirCiclicamente
        {
            get
            {
                return repetirCiclicamente;
            }

            set
            {
                repetirCiclicamente = value;
            }
        }

        public int SampleRate
        {
            get
            {
                return sampleRate;
            }

            set
            {
                sampleRate = value;
            }
        }

        public int InicioRepeticion
        {
            get
            {
                return inicioRepeticion;
            }

            set
            {
                inicioRepeticion = value;
            }
        }

        public int Length//mirar el uso que tiene realmente :D
        {
            get
            {
                return length;
            }

            private set
            {
                length = value;
            }
        }

        public sbyte[][] Datos
        {
            get
            {
                return datos;
            }

            set
            {
                if (value.Length > NumeroDeCanalesMaximos)
                    throw new ArgumentOutOfRangeException();
                datos = value;
            }
        }

        public string Header
        {
            get
            {
                return header;
            }

            set
            {
                if (value == null)
                    value = "-Sin Header-";
                header = value;
            }
        }

        public ushort NumeroDeCanalesMaximos
        {
            get
            {
                ushort numCanalesMax;

                if (numeroDeCanalesMaximos == 0 && DiccionarioHeaderSignificadoCanalesMax.ContainsKey1(Header))
                    numCanalesMax = DiccionarioHeaderSignificadoCanalesMax.GetValueWithKey1(Header);
                else numCanalesMax = numeroDeCanalesMaximos;

                return numCanalesMax;
            }

            set
            {
                if (value == 0)
                    throw new ArgumentException("El máximo no puede ser 0");
                else if (value > 10)
                    throw new ArgumentException("El máximo de canales que puede tener la gba es 10");
                numeroDeCanalesMaximos = value;
            }
        }

        /// <summary>
        /// Devuelve lo que significa el Header si se ha registrado
        /// </summary>
        /// <returns>si no esta devuelve null</returns>
        public string Significado()
        {
            string strSignificado;
            if (DiccionarioHeaderSignificadoCanalesMax.ContainsKey1(Header))
                strSignificado = Header;
            else strSignificado = null;

            return strSignificado;

        }
        public Bitmap DibujarOndaSonido()
        {
            return DibujarOndaSonido(ColoresOndaPorDefecto);
        }
        public Bitmap DibujarOndaSonido(Color[] colores)
        {
            const int HEIGHTWAVEIMAGE = 128;
            const int FIX = 64;//buscar nombre mas descriptivo
            Color[] coloresOnda;
            if (colores.Length < numeroDeCanalesMaximos)
            {
                coloresOnda = new Color[numeroDeCanalesMaximos];
                for (int i = 0; i < colores.Length; i++)
                    coloresOnda[i] = colores[i];
                for (int i = colores.Length; i < coloresOnda.Length; i++)
                    coloresOnda[i] = ColoresOndaPorDefecto[i];
            }
            else coloresOnda = colores;

            Pen penOnda;
            Bitmap bmpOnda = new Bitmap(datos.Length, HEIGHTWAVEIMAGE);
            Graphics gOnda = Graphics.FromImage(bmpOnda);
            //pongo las lineas :D
            for (int j = 0; j < numeroDeCanalesMaximos; j++)
            {
                penOnda = new Pen(coloresOnda[j]);
                for (int i = 1; i < datos.Length; i++)
                {
                    gOnda.DrawLine(penOnda, i - 1, FIX + datos[j][i - 1], i, FIX + datos[j][i]);
                }
            }
            gOnda.Save();//no se si hace falta...

            return bmpOnda;

        }


        public MemoryStream ToWaveFileStream()
        {
            const UInt16 PCMFORMAT = 1;
            const byte FIX = 0x80;//buscar nombre mas descriptivo
            MemoryStream msSonido = new MemoryStream();
            BinaryWriter bwSonido = new BinaryWriter(msSonido, Encoding.ASCII, true);
            // RIFF header
            bwSonido.Write(Encoding.ASCII.GetBytes("RIFF"));
            bwSonido.Write(0);
            bwSonido.Write(Encoding.ASCII.GetBytes("WAVE"));

            // fmt chunk
            bwSonido.Write(Encoding.ASCII.GetBytes("fmt "));
            bwSonido.Write(16);
            bwSonido.Write(PCMFORMAT);
            bwSonido.Write(numeroDeCanalesMaximos);
            bwSonido.Write(sampleRate);
            bwSonido.Write(sampleRate);
            bwSonido.Write((ushort)1);
            bwSonido.Write((ushort)8);

            // data chunk
            bwSonido.Write(Encoding.ASCII.GetBytes("data"));
            for (int j = 0; j < numeroDeCanalesMaximos; j++)
            {
                bwSonido.Write(datos[j].Length);
                for (int i = 0; i < datos[j].Length; i++)
                    bwSonido.Write((byte)(datos[j][i] + FIX));
            }

            // fix header
            bwSonido.Seek(4, SeekOrigin.Begin);
            bwSonido.Write((int)(bwSonido.BaseStream.Length) - 8);
            return msSonido;
        }
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();


            if (DiccionarioHeaderSignificadoCanalesMax.ContainsKey1(Header))
            {
                str.Append(DiccionarioHeaderSignificadoCanalesMax.GetTkey2WhithTkey1(Header));
            }
            else
            {
                str.Append(Header);
            }
            str.Append(":");
            str.Append((string)Offset);
            str.Append("->");
            str.Append(numeroDeCanalesMaximos);

            return str.ToString();
        }


        public static BloqueSonido GetBloqueSonido(RomGBA rom, Hex offsetSound, ushort numeroDeCanalesMaximo = 1)
        {//por descubrir

            // sacar de la rom :D
            int numeroDeCanales = 1;
            sbyte[] aux;
            List<sbyte> lstDatos = null;
            int alignment = 0;
            sbyte pcmLevel = 0;
            byte input;
            BloqueSonido bloqueACargar = new BloqueSonido();
            bloqueACargar.Offset = offsetSound;
            bloqueACargar.numeroDeCanalesMaximos = numeroDeCanalesMaximo;
            bloqueACargar.Datos = new sbyte[numeroDeCanales][];//Mas adelante sacarlo de la rom y ponerlos todos :)
            bloqueACargar.EstaComprimido = Serializar.ToShort(rom.Datos.SubArray((int)offsetSound + (int)Posicion.EstaComprimido, sizeof(short)).ReverseArray()) == 0x1;
            bloqueACargar.RepetirCiclicamente = Serializar.ToShort(rom.Datos.SubArray((int)offsetSound + (int)Posicion.RepetirCiclicamente, sizeof(short)).ReverseArray()) == 0x4000;
            bloqueACargar.SampleRate = Serializar.ToInt(rom.Datos.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).ReverseArray()) >> 10;
            bloqueACargar.InicioRepeticion = Serializar.ToInt(rom.Datos.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).ReverseArray());
            bloqueACargar.Length = Serializar.ToInt(rom.Datos.SubArray((int)offsetSound + (int)Posicion.Length, sizeof(int)));//habrá una lenght para cada canal?
            if (bloqueACargar.Length + offsetSound < rom.Datos.Length)
            {
                unsafe
                {
                    sbyte* ptrDatosBloque;
                    byte* ptrDatosRom;
                    sbyte* ptrDatosRomSByte;
                    if (bloqueACargar.EstaComprimido)
                        lstDatos = new List<sbyte>();
                    fixed (byte* ptDatos = rom.Datos)
                    {
                        ptrDatosRomSByte = (sbyte*)ptDatos;
                        for (int j = 0; j < numeroDeCanales; j++)
                        {

                            ptrDatosRomSByte = ptrDatosRomSByte + offsetSound + (int)Posicion.Data;

                            if (!bloqueACargar.EstaComprimido)
                            {
                                aux = new sbyte[bloqueACargar.Length];
                                fixed (sbyte* ptDatosBloque = aux)
                                {
                                    ptrDatosBloque = ptDatosBloque;
                                    for (int i = 0; i < bloqueACargar.Length; i++)
                                    {

                                        *ptrDatosBloque = (sbyte)*ptrDatosRomSByte;
                                        ptrDatosBloque++;
                                        ptrDatosRomSByte++;
                                    }

                                }
                                bloqueACargar.Datos[j] = aux;
                            }
                            else
                            {
                                ptrDatosRom = ptDatos;
                                lstDatos.Clear();
                                //descomprimo :D
                                for (int k = 0, kF = bloqueACargar.Length / 2; k < kF; k++)
                                {
                                    if (alignment == 0)
                                    {
                                        pcmLevel = (sbyte)*ptrDatosRom;
                                        ptrDatosRom++;
                                        lstDatos.Add(pcmLevel);
                                        alignment = 0x20;

                                    }
                                    input = *ptrDatosRom;
                                    ptrDatosRom++;
                                    if (alignment < 0x20)
                                    {
                                        pcmLevel += Lookup[input >> 4];
                                        lstDatos.Add(pcmLevel);
                                    }
                                    pcmLevel += Lookup[input & 0xF];
                                    lstDatos.Add(pcmLevel);
                                    alignment--;
                                }
                                bloqueACargar.Datos[j] = lstDatos.ToArray();
                                bloqueACargar.Length = (int)(ptrDatosRom - ptDatos);
                            }
                        }
                    }
                }
            }
            else
            {
                bloqueACargar = null;
            }
            return bloqueACargar;
        }
        public static BloqueSonido GetBloqueSonido(RomGBA rom, string idHeader, int posicion = 0)
        {
            return GetBloqueSonido(rom,(byte[])(Hex)idHeader, posicion);
        }
        public static BloqueSonido GetBloqueSonido(RomGBA rom, byte[] idHeader, int posicion = 0)
        {
            Hex offset = NextOffsetSound(rom, 0, idHeader, posicion);
            BloqueSonido bloque;
            if (offset > 0)
            {
                bloque = GetBloqueSonido(rom, offset + LENGTHIDHEADER);
            }
            else bloque = null;
            return bloque;
        }
        /// <summary>
        /// Obtiene todos los sonidos que tenga asociado ese id
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="idHeader">es el codigo que va antes del pointer</param>
        /// <returns></returns>
        public static Llista<BloqueSonido> GetBloquesSonido(RomGBA rom, string idHeader, bool comprobarQueSeaValido = false)
        {
            ushort numeroDeCanalesMax;
            byte[] byteHeader = (Hex)idHeader;
            Llista<BloqueSonido> sonidos = new Llista<BloqueSonido>();
            Hex offsetSound = 0;
            Hex endHeader = (Hex)EndHeader;
            BloqueSonido bloque;
            do
            {
                offsetSound = NextOffsetSound(rom, offsetSound + 1, byteHeader);
                if (offsetSound > 0)
                {
                    if (DiccionarioHeaderSignificadoCanalesMax.ContainsKey1(idHeader))
                        numeroDeCanalesMax = DiccionarioHeaderSignificadoCanalesMax.GetValueWithKey1(idHeader);
                    else numeroDeCanalesMax = 1;

                    bloque = GetBloqueSonido(rom, PokemonGBAFrameWork.Offset.GetOffset(rom, offsetSound + LENGTHIDHEADER), numeroDeCanalesMax);
                    sonidos.Add(bloque);
                    if (bloque != null)
                    {
                        sonidos[sonidos.Count - 1].Header = idHeader;
                    }
                }
            } while (offsetSound > 0 && (!comprobarQueSeaValido || sonidos.Count == 0));

            return sonidos;
        }
        public static LlistaOrdenadaPerGrups<string, BloqueSonido> GetBloquesSonido(RomGBA rom)
        {
            LlistaOrdenadaPerGrups<string, BloqueSonido> bloquesRom = new LlistaOrdenadaPerGrups<string, BloqueSonido>();
            string[] idHeaders = GetHeaders(rom);
            for (int i = 0; i < idHeaders.Length; i++)
            {
                bloquesRom.Add(idHeaders[i], GetBloquesSonido(rom, idHeaders[i]));
            }
            return bloquesRom;
        }

        public static string[] GetHeaders(RomGBA rom)
        {
            LlistaOrdenada<string, string> llistaHeaders = new LlistaOrdenada<string, string>();
            LlistaOrdenada<string, string> llistaBannedHeaders = new LlistaOrdenada<string, string>();
            Hex offset = 0;
            byte[] bytesIdHeader;
            string idHeaderEncontrado;
            do
            {
                offset = rom.Datos.BuscarArray(offset + 1, EndHeader);
                if (offset > 0)
                {
                    bytesIdHeader = rom.Datos.SubArray((int)offset - (int)Posicion.EndHeader, LENGTHIDHEADER);
                    if (PokemonGBAFrameWork.Offset.IsAPointer(rom, offset - (int)Posicion.PointerHeader))
                    {
                        idHeaderEncontrado = (Hex)bytesIdHeader;
                        if (!llistaHeaders.ContainsKey(idHeaderEncontrado) && !llistaBannedHeaders.ContainsKey(idHeaderEncontrado))
                        {
                            try
                            {
                                if (GetBloquesSonido(rom, idHeaderEncontrado, true).Count > 0)
                                    llistaHeaders.Add(idHeaderEncontrado, idHeaderEncontrado);
                                else llistaBannedHeaders.Add(idHeaderEncontrado, idHeaderEncontrado);
                            }
                            catch { llistaBannedHeaders.Add(idHeaderEncontrado, idHeaderEncontrado); }
                        }
                    }
                }

            } while (offset > 0);

            return llistaHeaders.ValuesToArray();
        }

        static Hex NextOffsetSound(RomGBA rom, Hex offsetInicio, byte[] header, int numDeOffsetaHaEvitar = 0)
        {
            bool trobat;
            Hex offsetNextSound = rom.Datos.BuscarArray(offsetInicio, header);
            for (int i = 0; i <= numDeOffsetaHaEvitar && offsetNextSound > 0; i++)
                do
                {
                    trobat = offsetNextSound > 0;
                    if (trobat)
                    {
                        trobat = PokemonGBAFrameWork.Offset.IsAPointer(rom, offsetNextSound + LENGTHIDHEADER);
                        if (trobat)
                        {
                            trobat = rom.Datos.BuscarArray(offsetNextSound, EndHeader) == (offsetNextSound + (int)Posicion.EndHeader);

                        }

                        if (!trobat)
                        {
                            offsetNextSound = rom.Datos.BuscarArray(offsetNextSound + 1, header);
                        }
                    }
                } while (!trobat && offsetNextSound > 0);
            return offsetNextSound;

        }
    }
}

