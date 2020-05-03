using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public abstract class BaseWord
    {
        class BaseAux : BaseWord
        {
            public unsafe BaseAux(byte* ptrRom, int offset, int length) : base(ptrRom, offset, length)
            {
            }
        }

        public BaseWord(RomGba rom, int offset, int length) : this(rom.Data, offset, length)
        { }
        public BaseWord(BloqueBytes rom, int offset, int length) : this(rom.Bytes, offset, length)
        { }
        public BaseWord(byte[] rom, int offsetWord, int length)
        {
            unsafe
            {
                fixed (byte* ptrRom = rom)
                {
                    Data = new BaseAux(ptrRom, offsetWord, length).Data;
                }

            }
        }
        public unsafe BaseWord(byte* ptrRom, int offsetWord, int length)
        {
            Data = MetodosUnsafe.ReadBytes(ptrRom + offsetWord, length);
        }
        public BaseWord(byte[] data)
        {
            this.Data = data;
        }

        public byte[] Data { get; set; }
        #region IComparable implementation
        public int CompareTo(object obj)
        {
            return CompareTo(obj as BaseWord);
        }
        #endregion
        #region IComparable implementation
        public int CompareTo(BaseWord other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = (int)Data.CompareTo(other.Data);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;

            return compareTo;
        }
        #endregion

        public override string ToString()
        {
            return ((Hex)(byte[])this) + "";
        }
        public override bool Equals(object obj)
        {
            BaseWord other = obj as BaseWord;
            bool isEquals;
            if (other == null)
                isEquals = false;
            else isEquals = this.Data.ArrayEqual(other.Data);
            return isEquals;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (Data != null)
                    hashCode += 1000000007 * Data.GetHashCode();
            }
            return hashCode;
        }
        public static void SetData(RomGba rom, int offset, BaseWord word)
        {
            SetData(rom.Data, offset, word);
        }
        public static void SetData(BloqueBytes datos, int offset, BaseWord word)
        {
            SetData(datos.Bytes, offset, word);
        }
        public static void SetData(byte[] datos, int offset, BaseWord word)
        {
            unsafe
            {
                fixed (byte* ptrDatos = datos)
                    SetData(ptrDatos, offset, word);

            }
        }
        public static unsafe void SetData(byte* ptrDatos, int offset, BaseWord word)
        {
            SetData(ptrDatos + offset, word);
        }
        public static unsafe void SetData(byte* ptrDatosPosicionados, BaseWord word)
        {
            MetodosUnsafe.WriteBytes(ptrDatosPosicionados, word.Data);
        }

        public static bool operator ==(BaseWord lhs, BaseWord rhs)
        {
            bool iguales;
            if (ReferenceEquals(lhs, rhs))
                iguales = true;
            else if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                iguales = false;
            else iguales = lhs.Equals(rhs);

            return iguales;

        }
        public static bool operator !=(BaseWord lhs, BaseWord rhs)
        {
            return !(lhs == rhs);
        }

        public static implicit operator byte[](BaseWord word)
        {
            return word.Data;
        }
        public static explicit operator Hex(BaseWord word)
        {
            return (Hex)word.Data;
        }

    }
}