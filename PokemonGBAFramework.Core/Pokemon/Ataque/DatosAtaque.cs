using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core
{
    public class DatosAtaque:IComparable<DatosAtaque>
    {
        enum Custom
        {
            MakesContact = 1,
            Protect = 2,
            MagicCoat = 4,
            Snatch = 8,
            MirrorMove = 16,
            KingsRock = 32
        }
        enum CamposDatosAtaque
        {
            Effect,
            BasePower,
            Type,
            Accuracy,
            PP,
            EffectAccuracy,
            Target,
            Priority,
            Custom,
            PadByte1,
            Category,
            PadByte3
        }
        public enum Categoria
        {
            Fisico, Especial, Estatus
        }

        public const int LENGTH = 12;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x80, 0x22, 0x08, 0xF0, 0x37, 0xFC };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 32;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x04, 0x22, 0x18, 0x68, 0x00, 0x19 };
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 48;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x00, 0xF0, 0x4A, 0xF8, 0x04, 0x48, 0x00 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - 48;
      
        BloqueBytes blDatosAtaque;
        public DatosAtaque()
        {
            blDatosAtaque = new BloqueBytes(LENGTH);
        }
        #region Propiedades
        public bool MakesContact
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] % 2 != 0;//si es impar es que hay el 1 de make contact
            }
            set
            {
                if (value)
                {
                    if (!MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]++;

                }
                else
                {
                    if (MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]--;
                }
            }
        }
        public bool IsAffectedByProtect
        {
            get
            {
                return IsCustomEnabled(1);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Protect;
                }
                else
                {
                    if (IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Protect;
                }

            }
        }
        public bool IsAffectedByMagicCoat
        {
            get
            {
                return IsCustomEnabled(2);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MagicCoat;
                }
                else
                {
                    if (IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MagicCoat;
                }

            }
        }
        public bool IsAffectedBySnatch
        {
            get
            {
                return IsCustomEnabled(3);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Snatch;
                }
                else
                {
                    if (IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Snatch;
                }

            }
        }
        public bool IsAffectedByMirrorMove
        {
            get
            {
                return IsCustomEnabled(4);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MirrorMove;
                }
                else
                {
                    if (IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MirrorMove;
                }

            }
        }
        public bool IsAffectedByKingsRock
        {
            get
            {
                return IsCustomEnabled(5);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.KingsRock;
                }
                else
                {
                    if (IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.KingsRock;
                }

            }
        }

        public byte Effect
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect] = value;
            }
        }

        public byte BasePower
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower] = value;
            }
        }

        public byte Type
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type] = value;
            }
        }

        public byte Accuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy] = value;
            }
        }

        public byte PP
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PP] = (byte)(value % 30);//el maximo es 30
            }
        }

        public byte EffectAccuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy] = value;
            }
        }

        public byte Target
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target] = value;
            }
        }

        public byte Priority
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority] = value;
            }
        }



        public byte PadByte1
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1] = value;
            }
        }

        public Categoria Category
        {
            get
            {
                return (Categoria)blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category] = (byte)value;
            }
        }

        public byte PadByte3
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3] = value;
            }
        }

        #endregion
        private bool IsCustomEnabled(int indexCustomToCalculate)
        {
            bool isCustomEnabled;
            int aux = blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom];
            Custom[] customs = Enum.GetValues(typeof(Custom)) as Custom[];
            for (int i = customs.Length - 1; i > indexCustomToCalculate; i--)
                if (aux >= (int)customs[i])
                    aux -= (int)customs[i];
            isCustomEnabled = aux >= (int)customs[indexCustomToCalculate];
            return isCustomEnabled;
        }
        public int CompareTo(DatosAtaque other)
        {
            int compareTo;
            if (other != default)
            {
                compareTo = (int)blDatosAtaque.Bytes.CompareTo(other.blDatosAtaque.Bytes);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
        public static DatosAtaque Get(RomGba rom, int posicionAtaque,OffsetRom offsetInicioDatosAtaque=default)
        {
            if (Equals(offsetInicioDatosAtaque, default))
                offsetInicioDatosAtaque = GetOffset(rom);
            DatosAtaque datos = new DatosAtaque();
            datos.blDatosAtaque.Bytes = rom.Data.Bytes.SubArray(offsetInicioDatosAtaque + posicionAtaque * LENGTH, LENGTH);
            return datos;
        }
        public static DatosAtaque[] Get(RomGba rom) => DescripcionAtaque.GetAll<DatosAtaque>(rom, Get, GetOffset(rom));

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int inicio;
            if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                inicio = InicioRelativoEsmeralda;
            }
            else if (rom.Edicion.EsHoenn)
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                inicio = InicioRelativoRubiYZafiro;
            }
            else
            {
                algoritmo = MuestraAlgoritmoKanto;
                inicio = InicioRelativoKanto;
            }
            return Zona.Search(rom, algoritmo, inicio);
        }


    }
}