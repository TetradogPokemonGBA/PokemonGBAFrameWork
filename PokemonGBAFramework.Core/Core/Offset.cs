using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool IsEmpty => !BytesPointer.Any((p) => p != 0x0);
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
        #region Por mirar
        public int FullPointer => Offset;
        public int Integer => Serializar.ToInt(new byte[] { BytesPointer[0], BytesPointer[1], BytesPointer[2], 0x0 }) & 0x1FFFFFF;
        public int SignedLongTrue => (BytesPointer[3] > 0x7F ? ~FullPointer : FullPointer);
        public int SignedLongFalse => (BytesPointer[3] > 0x7F ? ~Integer : Integer);
        #endregion

        public void Fix(bool is08 = true)
        {
            if(!IsAPointer)
               BytesPointer[POSICIONIDENTIFICADOR] = (byte)(is08 ? BYTEIDENTIFICADOR16MB : BYTEIDENTIFICADOR32MB);
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
        public static explicit operator OffsetRom(int offset)
        {
            return offset >= 0 ? new OffsetRom(offset) : default;
        }
        public static implicit operator int(OffsetRom offsetRom)
        {
            return offsetRom.Offset;
        }
        #endregion

        public static bool Check(RomGba rom, int offset) => Check(rom.Data, offset);
        public static bool Check(BloqueBytes bloque, int offset) => Check(bloque.Bytes, offset);
        public static bool Check(byte[] bytes, int offset)
        {
            offset += POSICIONIDENTIFICADOR;
            return bytes[offset]==BYTEIDENTIFICADOR16MB||bytes[offset] == BYTEIDENTIFICADOR32MB;
        }
        public static void Set(RomGba rom, OffsetRom offsetAReemplazar, int offsetAPoner)
        {
            Set(rom.Data, offsetAReemplazar, offsetAPoner);
        }
        public static void Set(BloqueBytes rom, OffsetRom offsetAReemplazar, int offsetAPoner)
        {
            OffsetRom ptrAPoner = new OffsetRom(offsetAPoner);
            int posicion = 0;
            do
            {
                posicion = rom.SearchArray(posicion + 1, offsetAReemplazar.BytesPointer);
                if (posicion > 0)
                    rom.SetArray(posicion, ptrAPoner.BytesPointer);
            } while (posicion > 0);
        }
        public static void Set(RomGba rom, int offsetDatos, OffsetRom offset)
        {
            Set(rom.Data, offsetDatos, offset);
        }
        public static void Set(BloqueBytes data, int offsetDatos, OffsetRom offset)
        {
            Set(data.Bytes, offsetDatos, offset);
        }
        public static void Set(byte[] rom, OffsetRom offsetDatos, OffsetRom offset)
        {
            Set(rom, offsetDatos.Offset, offset);
        }
        public static void Set(byte[] rom, int offsetDatos, OffsetRom offset)
        {
            unsafe
            {
                fixed (byte* ptrRom = rom)
                    Set(ptrRom, offsetDatos, offset);
            }
        }
        public unsafe static void Set(byte* ptrRom, int offsetDatos, OffsetRom offset)
        {
            Set(ptrRom + offsetDatos, offset);
        }
        public unsafe static void Set(byte* ptrDatos, OffsetRom offset)
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
        public static int GetOffsetPointerSiguiente(byte[] rom, int offset)
        {
            const byte FINPOINTER = 0x8;
            do
            {
                offset = rom.IndexByte(offset, FINPOINTER) - OffsetRom.LENGTH+1;
            }
            while (offset>=0&&offset<rom.Length&&!Check(rom, offset));
            if (offset == rom.Length)
                offset = -1;
            return offset;
        }
        public static int GetOffsetPointerAnterior(byte[] rom, int offset)
        {
            const byte FINPOINTER = 0x8;
            do
            {
                while(rom[offset]!=FINPOINTER)
                  offset--;
                offset -= OffsetRom.LENGTH - 1;
            }
            while (offset >=0 && !Check(rom, offset));
            return offset;
        }
        public static int GetTotalPointers(RomGba rom, OffsetRom offsetInicioTablaPointers)
        {
            OffsetRom offset;
            int total = 0;
            int offsetTabla = offsetInicioTablaPointers;
            do
            {
                offset = new OffsetRom(rom, offsetTabla);
                offsetTabla += LENGTH;
                if (offset.IsAPointer)
                    total++;

            } while (offset.IsAPointer);
            return total;


        }

    }
}