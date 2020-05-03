using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace PokemonGBAFramework.Core
{
    public class RomGba
    {
        public const int MAXLENGTH = 33554432;
        public RomGba(byte[] data)
        {
            Data = new BloqueBytes(data);
            Edicion = new Edicion() { Version = Edicion.Pokemon.RojoFuego };
        }
        public RomGba(FileInfo file) : this(File.ReadAllBytes(file.FullName)) { }
        public RomGba(string pathFile):this(new FileInfo(pathFile)) { }
        public BloqueBytes Data { get;  set; }
        public Edicion Edicion { get; set; }
    }
}
