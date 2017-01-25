using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//codigo adaptado de CryFunctions.vb de Gamer2020;
namespace PokemonGBAFrameWork
{
   public class BloqueSonido
    {
        enum Posicion
        {
            EstaComprimido = 0,
            RepetirCiclicamente = 2,
            SampleRate = 4,
            InicioRepeticion = 8,
            Length = 12,
            Data = 16,
        }
        public static Color ColorOndaPorDefecto = Color.Green;
        static readonly sbyte[] Lookup = new sbyte[] { 0, 1, 4, 9, 16, 25,36, 49, -64, -49, -36, -25, -16, -9, -4, -1};
        Hex offset;
        bool estaComprimido;
        bool repetirCiclicamente;
        int sampleRate;
        int inicioRepeticion;
        int length;
        SByte[] datos;

        private BloqueSonido() { }
        public BloqueSonido(MemoryStream msWaveFile,bool repetirCiclicamente=false,int inicioRepeticion=0)
        {
            BinaryReader brOnda = new BinaryReader(msWaveFile);
            ushort bitsPerSample;
            //  ' read RIFF header
            if (brOnda.ReadUInt32() != 0x46464952)
                throw new Exception("This is not a WAVE file!");
            if (brOnda.ReadInt32() + 8 != brOnda.BaseStream.Length)
                throw new Exception("Invalid file length!");

            if (brOnda.ReadUInt32() != 0x45564157 )
                throw new Exception("This is not a WAVE file!");


            //   ' read fmt chunk
            if (brOnda.ReadUInt32() != 0x20746D66 )
                throw new Exception("Expected fmt chunk!");

            if (brOnda.ReadUInt32() != 16 )
                throw new Exception("Invalid fmt chunk!");

            if (brOnda.ReadUInt16() != 1 )
                //' only PCM format allowed
                throw new Exception("Cry must be in PCM format!");

            if (brOnda.ReadUInt16() != 1 )
              //  ' only 1 channel allowed
                throw new  Exception("Cry cannot have more than one channel!");

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

            length = brOnda.ReadInt32();

            datos = new SByte[length - 1];
            for (int i = 0; i < length; i++)
                // ' read 8-bit unsigned PCM and convert to GBA signed form
                datos[i] = (SByte)(brOnda.ReadByte() - 128);

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
                return  datos==null? length:datos.Length;
            }

           private set
            {
                length = value;
            }
        }

        public sbyte[] Datos
        {
            get
            {
                return datos;
            }

            set
            {
                datos = value;
            }
        }
        public Bitmap DibujarOndaSonido()
        {
            return DibujarOndaSonido(ColorOndaPorDefecto);
        }
        public Bitmap DibujarOndaSonido(Color color)
        {
            const int HEIGHTWAVEIMAGE = 128;
            const int FIX = 64;//buscar nombre mas descriptivo
            Pen penOnda = new Pen(color);
            Bitmap bmpOnda=new Bitmap(datos.Length, HEIGHTWAVEIMAGE);
            Graphics gOnda = Graphics.FromImage(bmpOnda);
            //pongo las lineas :D
            for(int i=1;i<datos.Length;i++)
            {
                gOnda.DrawLine(penOnda, i - 1, FIX + datos[i - 1], i, FIX + datos[i]);
            }
            gOnda.Save();//no se si hace falta...

            return bmpOnda;

        }
        public MemoryStream ToWaveFileStream()
        {
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
            bwSonido.Write((ushort)1);
            bwSonido.Write((ushort)1);
            bwSonido.Write(sampleRate);
            bwSonido.Write(sampleRate);
            bwSonido.Write((ushort)1);
            bwSonido.Write((ushort)8);

            // data chunk
            bwSonido.Write(Encoding.ASCII.GetBytes("data"));
            bwSonido.Write(datos.Length);
            for (int i = 0; i < datos.Length; i++)
                bwSonido.Write((byte)(datos[i] + FIX));
               

                // fix header
            bwSonido.Seek(4, SeekOrigin.Begin);
            bwSonido.Write((int)(bwSonido.BaseStream.Length) - 8);
            return msSonido;
        }

        public static BloqueSonido GetBloqueSonido(RomData rom, Hex offsetSound, bool repetirCiclicamente = false, int inicioRepeticion = 0)
        {
            
            List<sbyte> lstDatos;
            int alignment = 0;
            int size = 0;
            sbyte pcmLevel = 0;
            byte input;
            BloqueSonido bloqueACargar = new BloqueSonido();

            bloqueACargar.EstaComprimido = Serializar.ToShort(rom.RomGBA.Datos.SubArray((int)offsetSound +(int)Posicion.EstaComprimido, sizeof(short)).ReverseArray()) == 0x1;
            bloqueACargar.RepetirCiclicamente = Serializar.ToShort(rom.RomGBA.Datos.SubArray((int)offsetSound + (int)Posicion.RepetirCiclicamente, sizeof(short)).ReverseArray())== 0x4000;
            bloqueACargar.SampleRate = Serializar.ToInt(rom.RomGBA.Datos.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).ReverseArray()) >> 10;
            bloqueACargar.InicioRepeticion=Serializar.ToInt(rom.RomGBA.Datos.SubArray((int)offsetSound + (int)Posicion.SampleRate, sizeof(int)).ReverseArray());
            bloqueACargar.Length = Serializar.ToInt(rom.RomGBA.Datos.SubArray((int)offsetSound+(int)Posicion.Length,sizeof(int)));
            unsafe
            {
                sbyte* ptrDatosBloque;
                byte* ptrDatosRom;

                fixed (byte* ptDatosRom = rom.RomGBA.Datos)
                {

                    ptrDatosRom = ptDatosRom + offsetSound + (int)Posicion.Data;
                    if (!bloqueACargar.EstaComprimido)
                    {
                        fixed (sbyte* ptDatosBloque = bloqueACargar.Datos)
                        {
                            ptrDatosBloque = ptDatosBloque;
                            bloqueACargar.Datos = new sbyte[bloqueACargar.Length - 1];

                            for (int i = 0; i < bloqueACargar.Length; i++)
                            {
                    
                                *ptrDatosBloque = (sbyte)*ptrDatosRom;
                                ptrDatosBloque++;
                                ptrDatosRom++;
                            }

                        }
                    }
                    else
                    {
                        lstDatos = new List<sbyte>();
                        //descomprimo :D
                        while (size >= bloqueACargar.Length)
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
                            size += 2;
                            alignment--;
                        }
                        bloqueACargar.Datos = lstDatos.ToArray();
                        bloqueACargar.Length = (int)(ptrDatosRom-ptDatosRom);
                    }
                }
            }
            
            return bloqueACargar;
        }
    }
}
