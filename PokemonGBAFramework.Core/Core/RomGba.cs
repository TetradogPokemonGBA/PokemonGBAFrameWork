using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFramework.Core
{
    public class RomGba:ICloneable,IClonable<RomGba>
    {
        public const byte FreeSpaceFinderByteEsmeralda = byte.MinValue;
        public const byte FreeSpaceFinderByteGeneral = byte.MaxValue;
        public const int MAXLENGTH = 33554432;
        private Edicion edicion;

        public RomGba(byte[] data)
        {
            Data = new BloqueBytes(data);
        }
        public RomGba(FileInfo file) : this(File.ReadAllBytes(file.FullName)) { }
        public RomGba(string pathFile) : this(new FileInfo(pathFile)) { }
        public BloqueBytes Data { get; set; }
        public Edicion Edicion {
            get {
                if (Equals(edicion, default))
                    edicion =Edicion.Get(this);
                return edicion; 
            }
            set => edicion = value;
            
        }

        public int GetOffsetFreeSpace(int length)
        {   
            return Data.SearchEmptyBytes(length, Edicion.EsEsmeralda ? FreeSpaceFinderByteEsmeralda : FreeSpaceFinderByteGeneral);
        }

        public object Clone() => Clon();
        public RomGba Clon()
        {
            return new RomGba((byte[])Data.Bytes.Clone());
        }
    }
}
