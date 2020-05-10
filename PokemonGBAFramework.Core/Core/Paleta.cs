using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Paleta :BasePaleta, IComparable, IComparable<Paleta>
    {

        public const int LENGTHHEADER = 4;
        public const int LENGTHHEADERCOMPLETO = OffsetRom.LENGTH + LENGTHHEADER;
        public const int LENGTH = 16;

        public static Color BackgroundColorDefault = Color.Transparent;
        public static Paleta Default { get; set; }= new Paleta();
        
        static readonly byte[] DefaultHeader = { 0x0, 0x0 };

        public Paleta():base(LENGTH)
        {
            Offset = -1;
        }
        public Paleta(params Color[] coloresPaleta) : this()
        {
            if (coloresPaleta == default)
                throw new ArgumentNullException();

            for (int i = 0; i < LENGTH && i < coloresPaleta.Length; i++)
                this.Colores[i] = coloresPaleta[i];

        }

        public int Offset { get; set; }
        public byte SortID
        {
            get
            {
                return Serializar.GetBytes(Id)[0];
            }
        }
        public short Id { get; set; }

        public short Formato { get; set; }
        private byte[] Header
        {
            get { return Serializar.GetBytes(Formato).AddArray(Serializar.GetBytes(Id)); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                if (value.Length < LENGTHHEADER)
                    value = value.AddArray(new byte[LENGTHHEADER - value.Length]);

                Id = Serializar.ToShort(value.SubArray(0, 2));
                Formato = Serializar.ToShort(value.SubArray(2, 2));
            }
        }
        public byte[] HeaderCompleto
        {
            get { return GetHeaderCompleto(Offset); }

        }
        public byte[] GetHeaderCompleto(int pointerData)
        {
            return new OffsetRom(pointerData).BytesPointer.AddArray(Header);
        }
        public GranPaleta ToGranPaleta() => new GranPaleta(Colores);
        public override BasePaleta Clon()
        {
            return new Paleta(Colores);
        }

        #region IComparable implementation

        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as Paleta);
        }
        public int CompareTo(Paleta other)
        {
            int compareTo;
            if (other != null)
            {
                compareTo = SortID.CompareTo(other.SortID);

            }
            else
            {
                compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            }
            return compareTo;
        }


        #endregion

        public static Paleta GetSinHeader(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
        {
            //sacado de Nameless
            if (rom == null || offsetPointerPaleta < 0)
                throw new ArgumentException();

            const int TOTALBYTESPALETA = 32;

            byte[] bytesPaletaDescomprimidos;
            Paleta paleta;
            int offsetPaletaData = new OffsetRom(rom, offsetPointerPaleta).Offset;

            if (LZ77.CheckCompresionLZ77(rom.Data.Bytes, offsetPaletaData))
                bytesPaletaDescomprimidos = LZ77.Descomprimir(rom.Data.Bytes, offsetPaletaData);
            else
                bytesPaletaDescomprimidos = rom.Data.Bytes.SubArray(offsetPaletaData, TOTALBYTESPALETA);//son dos bytes por color

            paleta = Get(bytesPaletaDescomprimidos);
            if (!showBackgroundColor)
            {
                paleta[0] = BackgroundColorDefault;
            }
            paleta.Offset = offsetPointerPaleta;
            return paleta;
        }
        public static Paleta Get(byte[] datosPaletaDescomprimida,int offset=0)
        {
            return new Paleta(BasePaleta.GetColors(datosPaletaDescomprimida,LENGTH,offset));
        }
        public static Paleta Get(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
        {
            byte[] header = rom.Data.SubArray(offsetPointerPaleta + OffsetRom.LENGTH, LENGTHHEADER);//lo posiciono para leer la información que contiene sin el pointer
            Paleta paletaCargada = GetSinHeader(rom, offsetPointerPaleta, showBackgroundColor);
            paletaCargada.Header = header;
            return paletaCargada;
        }


        public static bool IsHeaderOk(RomGba gbaRom, int offsetToCheck)
        {
            return new OffsetRom(gbaRom, offsetToCheck).IsAPointer && gbaRom.Data.Bytes.ArrayEqual(DefaultHeader, offsetToCheck + OffsetRom.LENGTH + 2);
        }


    }

}