using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class OffsetRom : IComparable
    {
        public const int LENGTH = 4;
        public const int POSICIONIDENTIFICADOR = 3;
        public const byte BYTEIDENTIFICADOR16MB = 0x8;
        public const byte BYTEIDENTIFICADOR32MB = 0x9;
        public const int DIECISEISMEGAS = 16777215;
        public const int TREINTAYDOSMEGAS = DIECISEISMEGAS * 2;

        public OffsetRom() : this(0) { }
        public OffsetRom(int offset) : this(Convert.ToUInt32(offset))
        { }

 

        public OffsetRom(uint offset = 0)
        {
            BytesPointer = Serializar.GetBytes(offset);
            BytesPointer[POSICIONIDENTIFICADOR] = offset > DIECISEISMEGAS ? BYTEIDENTIFICADOR32MB : BYTEIDENTIFICADOR16MB;

        }

        //public OffsetRom(RomData datos, int inicioPointer) : this(datos.Rom, inicioPointer)
        //{

        //}
        public OffsetRom(RomGba datos, int inicioPointer) : this(datos.Data, inicioPointer)
        {

        }
        public OffsetRom(BloqueBytes datos, int inicioPointer) : this(datos.Bytes, inicioPointer)
        {

        }
        public OffsetRom(byte[] datos, int inicioPointer) : this(datos.SubArray(inicioPointer, LENGTH))
        {

        }

        public OffsetRom(byte[] bytesPointer)
        {
            if (bytesPointer.Length < LENGTH)
                throw new ArgumentOutOfRangeException();
            this.BytesPointer = bytesPointer.SubArray(0, LENGTH);
        }
        public unsafe OffsetRom(byte* ptrRom, int offset) : this(ptrRom + offset)
        { }
        public unsafe OffsetRom(byte* ptrDatos)
        {
            BytesPointer = MetodosUnsafe.ReadBytes(ptrDatos, LENGTH);
        }

        public byte[] BytesPointer { get; private set; }
        public bool IsAPointer
        {
            get
            {
                return BytesPointer[POSICIONIDENTIFICADOR] == BYTEIDENTIFICADOR16MB || BytesPointer[POSICIONIDENTIFICADOR] == BYTEIDENTIFICADOR32MB;
            }
        }

        public int Offset
        {
            get
            {

                if (!IsAPointer)
                    throw new PointerMalFormadoException();

                int offset = Serializar.ToInt(new byte[] { BytesPointer[0], BytesPointer[1], BytesPointer[2], 0x0 });
                if (BytesPointer[POSICIONIDENTIFICADOR] == BYTEIDENTIFICADOR32MB)
                    offset += DIECISEISMEGAS;
                return offset;
            }
            set
            {

                if (value < 0 || value > TREINTAYDOSMEGAS)
                    throw new ArgumentOutOfRangeException();

                byte identificado = (byte)(value > DIECISEISMEGAS ? 0x9 : 0x8);
                BytesPointer = Serializar.GetBytes(value);
                BytesPointer = new byte[] { BytesPointer[3], BytesPointer[2], BytesPointer[1], identificado };
            }
        }

        public override string ToString()
        {
            const int CARACTERESSTRING = 8;
            return ((Hex)BytesPointer.InvertirClone()).ToString().PadLeft(CARACTERESSTRING, '0');
        }


        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)
        {
            return CompareTo(obj) == 0;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (BytesPointer != null)
                    hashCode += 1000000007 * BytesPointer.GetHashCode();
            }
            return hashCode;
        }

        public static bool operator ==(OffsetRom lhs, OffsetRom rhs)
        {
            bool equals;
            if (ReferenceEquals(lhs, rhs))
                equals = true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                equals = false;
            else equals = lhs.Equals(rhs);
            return equals;
        }

        public static bool operator !=(OffsetRom lhs, OffsetRom rhs)
        {
            return !(lhs == rhs);
        }

        #endregion

        #region IComparable implementation
        public int CompareTo(object obj)
        {
            OffsetRom other = obj as OffsetRom;
            int compareTo;
            if (other != null)
                compareTo = Serializar.ToInt(BytesPointer).CompareTo(Serializar.ToInt(BytesPointer));
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        #endregion
        #region conversion
        public static implicit operator OffsetRom(int offset)
        {
            return offset >= 0 ? new OffsetRom(offset) : default;
        }
        public static implicit operator int(OffsetRom offsetRom)
        {
            return offsetRom.Offset;
        }
        #endregion
        public static bool Check(byte[] rom, int offset)
        {
            return new OffsetRom(rom, offset).IsAPointer;
        }
        public static void SetOffset(RomGba rom, OffsetRom offsetAReemplazar, int offsetAPoner)
        {
            OffsetRom ptrAPoner = new OffsetRom(offsetAPoner);
            int posicion = 0;
            do
            {
                posicion = rom.Data.SearchArray(posicion + 1, offsetAReemplazar.BytesPointer);
                if (posicion > 0)
                    rom.Data.SetArray(posicion, ptrAPoner.BytesPointer);
            } while (posicion > 0);
        }
        public static void SetOffset(RomGba rom, int offsetDatos, OffsetRom offset)
        {
            SetOffset(rom.Data, offsetDatos, offset);
        }
        public static void SetOffset(BloqueBytes data, int offsetDatos, OffsetRom offset)
        {
            SetOffset(data.Bytes, offsetDatos, offset);
        }
        public static void SetOffset(byte[] rom, OffsetRom offsetDatos, OffsetRom offset)
        {
            SetOffset(rom, offsetDatos.Offset, offset);
        }
        public static void SetOffset(byte[] rom, int offsetDatos, OffsetRom offset)
        {
            unsafe
            {
                fixed (byte* ptrRom = rom)
                    SetOffset(ptrRom, offsetDatos, offset);
            }
        }
        public unsafe static void SetOffset(byte* ptrRom, int offsetDatos, OffsetRom offset)
        {
            SetOffset(ptrRom + offsetDatos, offset);
        }
        public unsafe static void SetOffset(byte* ptrDatos, OffsetRom offset)
        {
            const byte ZERO = 0x0;
            const int BYTESOFFSETSINELFIN = 3;
            bool allIs0 = true;

            byte* ptrBytesOffset;
            fixed (byte* ptBytesOffset = offset.BytesPointer)
            {
                ptrBytesOffset = ptBytesOffset;
                for (int i = 0; i < LENGTH; i++)
                {
                    *ptrDatos = *ptrBytesOffset;
                    if (allIs0 && *ptrBytesOffset != ZERO && i < BYTESOFFSETSINELFIN)
                        allIs0 = false;
                    ptrDatos++;
                    ptrBytesOffset++;
                }
                if (allIs0)
                {
                    ptrDatos--;//retrocedo porque habra un byte 0x8
                    *ptrDatos = ZERO;//pongo 0x0 porque los pointers que apuntan al offset 0 es que no estan puestos...
                }
            }
        }

    }
}