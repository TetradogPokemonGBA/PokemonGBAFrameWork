using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PokemonGBAFrameWork
{
    //mirar de hacer lo que hace Sappy :3 y poder importar desde wave,midi y exportar tambien :D
    //poder añadir instrumentos,quitarlos y editarlos :D
    public class BloqueSonido :ObjectAutoId,  IComparable
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
        public static readonly Creditos Creditos;
        public static readonly System.Drawing.Color[] ColoresOndaPorDefecto = { System.Drawing.Color.Green, System.Drawing.Color.Blue, System.Drawing.Color.Red, System.Drawing.Color.Yellow, System.Drawing.Color.Violet, System.Drawing.Color.Blue, System.Drawing.Color.Brown, System.Drawing.Color.HotPink, System.Drawing.Color.Gray, System.Drawing.Color.Magenta };
        static readonly sbyte[] Lookup = new sbyte[] { 0, 1, 4, 9, 16, 25, 36, 49, -64, -49, -36, -25, -16, -9, -4, -1 };
        public static TwoKeysList<string, string, ushort> DiccionarioHeaderSignificadoCanalesMax = new TwoKeysList<string, string, ushort>();
        static readonly byte[] EndHeader = { 0xFF, 0x0, 0xFF, 0x0 };
        const int LENGTHIDHEADER = 4;

        string header;
        int offsetDatos;
        bool estaComprimido;
        bool repetirCiclicamente;
        int sampleRate;
        int inicioRepeticion;
        int length;
        SByte[][] datos;
        ushort numeroDeCanalesMaximos;

        static BloqueSonido()
        {
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.GITHUB], "doom-desire", "sus proyectos GBA");
        }
        protected BloqueSonido(string header = null, int canales = 1)
        {
            Header = header;
            datos = new sbyte[canales][];
        }
        protected BloqueSonido(BloqueSonido bloque)
        {
            Header = bloque.Header;
            offsetDatos = bloque.OffsetDatos;
            estaComprimido = bloque.EstaComprimido;
            repetirCiclicamente = bloque.RepetirCiclicamente;
            sampleRate = bloque.SampleRate;
            inicioRepeticion = bloque.InicioRepeticion;
            length = bloque.Length;
            datos = bloque.Datos;
            numeroDeCanalesMaximos = bloque.NumeroDeCanalesMaximos;
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

            numeroDeCanalesMaximos = brOnda.ReadUInt16();//numero de canales
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
        public int OffsetDatos
        {
            get
            {
                return offsetDatos;
            }

            set
            {
                offsetDatos = value;
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
        /// <returns>si no está devuelve null</returns>
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
        public Bitmap DibujarOndaSonido(System.Drawing.Color[] colores)
        {
            const int HEIGHTWAVEIMAGE = 128;
            const int FIX = 64;//buscar nombre mas descriptivo
            System.Drawing.Color[] coloresOnda;
            if (colores.Length < Datos.Length)
            {
                coloresOnda = new System.Drawing.Color[Datos.Length];
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
            for (int j = 0; j < Datos.Length; j++)
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
            BinaryWriter bwSonido = new BinaryWriter(msSonido, Encoding.ASCII);
            // RIFF header
            bwSonido.Write(Encoding.ASCII.GetBytes("RIFF"));
            bwSonido.Write(0);
            bwSonido.Write(Encoding.ASCII.GetBytes("WAVE"));

            // fmt chunk
            bwSonido.Write(Encoding.ASCII.GetBytes("fmt "));
            bwSonido.Write(16);
            bwSonido.Write(PCMFORMAT);
            bwSonido.Write(Datos.Length);//numero de canales
            bwSonido.Write(sampleRate);
            bwSonido.Write(sampleRate);
            bwSonido.Write((ushort)1);
            bwSonido.Write((ushort)8);

            // data chunk
            bwSonido.Write(Encoding.ASCII.GetBytes("data"));
            for (int j = 0; j < datos.Length; j++)
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

        #region IComparable implementation


        int IComparable.CompareTo(object obj)
        {
            BloqueSonido blSonido = obj as BloqueSonido;
            int compareTo;
            if (blSonido != null)
                compareTo = String.Compare(IdAuto, blSonido.IdAuto);
            else compareTo = (int)CompareTo.Inferior;
            return compareTo;

        }


        #endregion

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
            str.Append((string)(Hex)OffsetDatos);
            str.Append("->");
            str.Append(numeroDeCanalesMaximos);

            return str.ToString();
        }


        public static BloqueSonido GetBloqueSonido(RomGba rom, int offsetSound, ushort numeroDeCanalesMaximo = 1)
        {//por descubrir

            // sacar de la rom :D
            int numeroDeCanales = 1;
            sbyte[] aux;
            List<sbyte> lstDatos = null;
            int alignment = 0;
            sbyte pcmLevel = 0;
            byte input;
            BloqueSonido bloqueACargar = new BloqueSonido();
            bloqueACargar.OffsetDatos = offsetSound;
            bloqueACargar.numeroDeCanalesMaximos = numeroDeCanalesMaximo;
            bloqueACargar.Datos = new sbyte[numeroDeCanales][];//Mas adelante sacarlo de la rom y ponerlos todos :)
            bloqueACargar.EstaComprimido = Serializar.ToShort(rom.Data.SubArray((int)offsetSound + (int)Posicion.EstaComprimido, sizeof(short)).InvertirClone()) == 0x1;
            bloqueACargar.RepetirCiclicamente = Serializar.ToShort(rom.Data.SubArray((int)offsetSound + (int)Posicion.RepetirCiclicamente, sizeof(short)).InvertirClone()) == 0x4000;
            bloqueACargar.SampleRate = Serializar.ToInt(rom.Data.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).InvertirClone()) >> 10;
            bloqueACargar.InicioRepeticion = Serializar.ToInt(rom.Data.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).InvertirClone());
            bloqueACargar.Length = Serializar.ToInt(rom.Data.SubArray((int)offsetSound + (int)Posicion.Length, sizeof(int)));//habrá una lenght para cada canal?

            if (bloqueACargar.Length + offsetSound < rom.Data.Length)
            {
                unsafe
                {
                    sbyte* ptrDatosBloque;
                    byte* ptrDatosRom;
                    sbyte* ptrDatosRomSByte;

                    if (bloqueACargar.EstaComprimido)
                        lstDatos = new List<sbyte>();

                    fixed (byte* ptDatos = rom.Data.Bytes)
                    {
                        ptrDatosRomSByte = (sbyte*)ptDatos;
                        for (int j = 0; j < numeroDeCanales; j++)//por mirar...no se si va así
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
        public static BloqueSonido GetBloqueSonido(RomGba rom, string idHeader, int posicion = 0)
        {
            return GetBloqueSonido(rom, (byte[])(Hex)idHeader, posicion);
        }
        public static BloqueSonido GetBloqueSonido(RomGba rom, byte[] idHeader, int posicion = 0)
        {
            int offset = NextOffsetSound(rom, 0, idHeader, posicion);
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
        /// <param name="comprobarQueSeaValido">si es true devuelve una lista con el primer bloque valido que encuentre</param>
        /// <returns></returns>
        public static Llista<BloqueSonido> GetBloquesSonido(RomGba rom, string idHeader, bool comprobarQueSeaValido = false)
        {
            ushort numeroDeCanalesMax;
            byte[] byteHeader = (Hex)idHeader;
            Llista<BloqueSonido> sonidos = new Llista<BloqueSonido>();
            int offsetSound = 0;
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

                    bloque = GetBloqueSonido(rom, new OffsetRom(rom, offsetSound + LENGTHIDHEADER).Offset, numeroDeCanalesMax);
                    sonidos.Add(bloque);
                    if (bloque != null)
                    {
                        sonidos[sonidos.Count - 1].Header = idHeader;
                    }
                }
            } while (offsetSound > 0 && (!comprobarQueSeaValido || sonidos.Count == 0));

            return sonidos;
        }
        public static LlistaOrdenadaPerGrups<string, BloqueSonido> GetBloquesSonido(RomGba rom)
        {

            LlistaOrdenadaPerGrups<string, BloqueSonido> bloquesRom = new LlistaOrdenadaPerGrups<string, BloqueSonido>();
            string[] idHeaders = GetHeaders(rom);
            for (int i = 0; i < idHeaders.Length; i++)
            {
                bloquesRom.Add(idHeaders[i], GetBloquesSonido(rom, idHeaders[i]));
            }
            return bloquesRom;
        }
        public static bool Compatible(RomGba rom)
        {
            return GetHeaders(rom).Length > 0;
        }
        public static string[] GetHeaders(RomGba rom)
        {
            LlistaOrdenada<string, string> llistaHeaders = new LlistaOrdenada<string, string>();
            LlistaOrdenada<string, string> llistaBannedHeaders = new LlistaOrdenada<string, string>();
            int offset = 0;
            byte[] bytesIdHeader;
            string idHeaderEncontrado;
            int headersCargados = 0;
            do
            {
                offset = rom.Data.SearchArray(offset + 1, EndHeader);
                if (offset > 0)
                {
                    bytesIdHeader = rom.Data.SubArray((int)offset - (int)Posicion.EndHeader, LENGTHIDHEADER);
                    if (new OffsetRom(rom, offset - (int)Posicion.PointerHeader).IsAPointer)
                    {
                        headersCargados++;
                        idHeaderEncontrado = (Hex)bytesIdHeader;
                        if (!llistaHeaders.ContainsKey(idHeaderEncontrado) && !llistaBannedHeaders.ContainsKey(idHeaderEncontrado))
                        {
                            try
                            {//mirar de quitar todos los try catch por rendimiento :)
                                if (GetBloquesSonido(rom, idHeaderEncontrado, true).Count > 0)
                                {
                                    llistaHeaders.Add(idHeaderEncontrado, idHeaderEncontrado);

                                }
                                else llistaBannedHeaders.Add(idHeaderEncontrado, idHeaderEncontrado);
                            }
                            catch { llistaBannedHeaders.Add(idHeaderEncontrado, idHeaderEncontrado); }
                        }

                    }
                    offset = PrimerHeaderDistinto(rom, offset, bytesIdHeader);
                }

            } while (offset > 0);

            return llistaHeaders.Values.ToArray(); 
        }

        private static int PrimerHeaderDistinto(RomGba rom, int offset, byte[] bytesHeader)
        {
            const int NOENCONTRADO = -1;
            int nextHeaderOffset = NOENCONTRADO;
            long header = (Hex)bytesHeader;
            while (offset > 0 && nextHeaderOffset == NOENCONTRADO)
            {
                offset = rom.Data.SearchArray(offset + 1, EndHeader);
                if (offset > 0)
                {
                    if ((Hex)rom.Data.SubArray((int)offset - (int)Posicion.PointerHeader, LENGTHIDHEADER) != header)
                        nextHeaderOffset = offset;
                }
            }
            return nextHeaderOffset;

        }

        static int NextOffsetSound(RomGba rom, int offsetInicio, byte[] header, int numDeOffsetaHaEvitar = 0)
        {
            bool trobat;
            int offsetNextSound = rom.Data.SearchArray(offsetInicio, header);
            for (int i = 0; i <= numDeOffsetaHaEvitar && offsetNextSound > 0; i++)
                do
                {
                    trobat = offsetNextSound > 0;
                    if (trobat)
                    {
                        trobat = new OffsetRom(rom, offsetNextSound + LENGTHIDHEADER).IsAPointer;
                        if (trobat)
                        {
                            trobat = rom.Data.SearchArray(offsetNextSound, EndHeader) == (offsetNextSound + (int)Posicion.EndHeader);

                        }

                        if (!trobat)
                        {
                            offsetNextSound = rom.Data.SearchArray(offsetNextSound + 1, header);
                        }
                    }
                } while (!trobat && offsetNextSound > 0);

            return offsetNextSound;

        }
    }
}
