using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Utilitats;
namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Es la rom cargada en la ram
    /// </summary>
    public class RomGba : IComparable<RomGba>
    {

        public const string EXTENSION = ".gba";
        public const int MAXLENGTH = 33554432;
        public const int MINLENGHT = 1;//mirar cual es el minimo
        string path;

        string nombre;
        IdUnico idUnico;

        public event EventHandler UnLoaded;

        #region Constructores
        public RomGba(string pathRom) : this(new FileInfo(pathRom))
        {


        }
        public RomGba(FileInfo romFile) : this(File.ReadAllBytes(romFile.FullName),romFile.Name,romFile.Directory.FullName)
        {
        }
        public RomGba(byte[] dataRom, string name="rom", string path = "") : this()
        {
            Data = new BloqueBytes(dataRom);
            this.nombre = name;
            if (string.IsNullOrEmpty(path))
                path = Environment.CurrentDirectory;

            this.path = path;
            Edicion=Edicion.GetEdicion(this);
        }
        private RomGba()
        { idUnico = new IdUnico(); }
        #endregion
        #region propiedades
        public Edicion Edicion
        {
            get;
            protected set;
        }

        public BloqueBytes Data
        {
            get;
            protected set;
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                string pathAnterior = FullPath;

                if (Data != null)
                {
                    if (String.IsNullOrEmpty(value))//null or ""
                        nombre = "Hack " + Edicion.NombreCompleto;
                    else nombre = value;
                }
                System.IO.File.Move(pathAnterior, FullPath);
            }
        }

        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (!Directory.Exists(value))
                    throw new ArgumentException();
                path = value;
            }
        }

        public string FullPath
        {
            get
            {
                string nombreArchivoConExtension = Nombre + EXTENSION;
                return System.IO.Path.Combine(path, nombreArchivoConExtension);
            }
        }
        public byte this[int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Pone la edicion en los datos en memoria
        /// </summary>
  
   




        #endregion

        #region Overrides
        public override string ToString()
        {
            return string.Format("[RomGba Nombre={0}]", Nombre);
        }
        public override bool Equals(object obj)
        {
            RomGba other = obj as RomGba;
            bool equals = other != null;
            if (equals)
                equals = Data.Bytes.ArrayEqual(other.Data.Bytes);
            return equals;
        }

        int IComparable<RomGba>.CompareTo(RomGba other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = idUnico.CompareTo(other.idUnico);
            }
            else compareTo = (int)CompareTo.Inferior;
            return compareTo;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
