﻿using Gabriel.Cat.S.Extension;
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

        Edicion edicion;
        BloqueBytes romData;
        string nombre;
        IdUnico idUnico;

        public event EventHandler UnLoaded;

        #region Constructores
        public RomGba(string pathRom) : this(new FileInfo(pathRom))
        {


        }
        public RomGba(FileInfo romFile) : this()
        {


            if (romFile == null)
                throw new ArgumentNullException();

            nombre = System.IO.Path.GetFileNameWithoutExtension(romFile.FullName);
            path = romFile.FullName.Substring(0, romFile.FullName.Length - System.IO.Path.GetFileName(romFile.FullName).Length);

        }
        public RomGba(byte[] dataRom, string name="rom", string path = "") : this()
        {
            romData = new BloqueBytes(dataRom);
            this.nombre = name;
            if (string.IsNullOrEmpty(path))
                path = Environment.CurrentDirectory;

            this.path = path;
        }
        private RomGba()
        { idUnico = new IdUnico(); }
        #endregion
        #region propiedades
        public Edicion Edicion
        {
            get
            {
                if (this.edicion == null)//si da problemas lo quito otra vez...
                    this.edicion = Edicion.GetEdicion(this);
                return this.edicion;
            }
        }

        public BloqueBytes Data
        {
            get
            {
                if (romData == null)
                    Load();
                return romData;
            }
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
                        nombre = "Hack " + edicion.NombreCompleto;
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
  
        public void LoadEdicion()
        {
            edicion = null;//asi cuando la lean por la propiedad la cargaran...
        }

        public void Load()
        {
            romData = new BloqueBytes(File.ReadAllBytes(FullPath));
        }
        /// <summary>
        /// Descarga los datos de la memoria ram
        /// </summary>
        public void UnLoad()
        {
            romData = null;
            edicion = null;
            if (UnLoaded != null)
                UnLoaded(this, new EventArgs());
        }


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
