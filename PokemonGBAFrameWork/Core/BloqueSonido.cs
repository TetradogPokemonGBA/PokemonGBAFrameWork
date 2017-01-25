using Gabriel.Cat;
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
        public static Color ColorOndaPorDefecto = Color.Green;
        static readonly byte[] Lookup = new byte[] { 0x0, 0x1, 0x4, 0x9, 0x10, 0x19, 0x24, 0x31, 0xC0, 0xCF, 0xDC, 0xE7, 0xF0, 0xF7, 0xFC, 0xFF };
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

        public int Length
        {
            get
            {
                return length;
            }

            set
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

        public BloqueSonido GetBloqueSonido(RomData rom, int index, bool repetirCiclicamente = false, int inicioRepeticion = 0)
        {
            BloqueSonido bloqueACargar = new BloqueSonido();


            return bloqueACargar;
        }
    }
}
