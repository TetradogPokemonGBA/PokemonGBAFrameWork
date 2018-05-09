using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class BloqueString : IComparable, IBloqueConNombre
    {
        enum CaracteresEspeciales
        {
            Lv,
            //[Lv]
            pk,
            //'[pk]'
            mn,
            //'[mn]'
            po,
            //'[po]'
            ké,
            //'[ké]'
            bl,
            //'[bl]'
            oc,
            //'[oc]'
            k,
            //'[k]'
            U,
            //'[U]'
            Punto,
            //'[.]'
            R,
            //'[R]'
            L,
            //'[L]'
            D,
            //'[D]'
            Comitas,
            //'[\"]'
            Comita,
            //'[']'
            m,
            //'[m]'
            f,
            //'[f]'
            MasGrande,
            //'[>]'
            Dolar,
            //'[$]'
            x,
            //'[x]'
            u,
            //'[u]'
            d,
            //'[d]'
            l,
            //'[l]'
            EspL,
            //'\\l'
            EspP,
            //'\\p'
            EspC,
            //'\\c'
            EspV,
            //'\\v'
            EspN,
            //'\\n'
            Vacio,
            //''
        }

        public const int MAXIMODECARACTERESDESHABILITADO = 0;
        static readonly string MARCAFIN = ((char)255) + "";

        int maxCaracteres;
        int offsetInicio;
        string texto;
        bool acabaEnFFByte;

        string idUnico;

        public BloqueString(int maxCaracteres)
            : this("", maxCaracteres, true)
        {
         
        }
        public BloqueString(string texto)
            : this(-1, texto, MAXIMODECARACTERESDESHABILITADO, true)
        {
        }
        public BloqueString(string texto, int maxCaracteres)
            : this(-1, texto, maxCaracteres, true)
        {
        }
        public BloqueString(int maxCaracteres, bool acabaEnFFByte)
            : this("", maxCaracteres, acabaEnFFByte)
        {
        }
        public BloqueString(string texto, bool acabaEnFFByte)
            : this(-1, texto, MAXIMODECARACTERESDESHABILITADO, acabaEnFFByte)
        {
        }
        public BloqueString(string texto, int maxCaracteres, bool acabaEnFFByte)
            : this(-1, texto, maxCaracteres, acabaEnFFByte)
        {
        }
        private BloqueString(int offsetInicio, string texto, bool acabaEnFFByte)
            : this(offsetInicio, texto, MAXIMODECARACTERESDESHABILITADO, acabaEnFFByte)
        {
        }
        private BloqueString(int offsetInicio, string texto, int maxCaracteres, bool acabaEnFFByte)
        {
            this.acabaEnFFByte = acabaEnFFByte;
            this.maxCaracteres = maxCaracteres;
            if (texto.Contains(MARCAFIN))
                texto = texto.Substring(0, texto.IndexOf(MARCAFIN));
            Texto = texto;
            this.offsetInicio = offsetInicio;
        }

        public BloqueString()
            : this(MAXIMODECARACTERESDESHABILITADO)
        {
        }

        public int MaxCaracteres
        {
            get
            {
                return maxCaracteres;
            }
            set
            {
                if (value < MAXIMODECARACTERESDESHABILITADO)
                    value = MAXIMODECARACTERESDESHABILITADO;
                maxCaracteres = value;
                if (texto.Length > maxCaracteres)
                    texto = texto.Substring(0, maxCaracteres);
            }
        }
        public string Texto
        {
            get
            {
                return texto;
            }
            set
            {
                if (value == null)
                    value = "";
                if (maxCaracteres != MAXIMODECARACTERESDESHABILITADO && value.Length > maxCaracteres)
                    value = value.Substring(0, maxCaracteres);
                texto = value;
            }
        }

        public int OffsetInicio
        {
            get
            {
                return offsetInicio;
            }
        }
        public bool AcabaEnFFByte
        {
            get
            {
                return acabaEnFFByte;
            }

            set
            {
                acabaEnFFByte = value;
            }
        }

        public int LengthInnerRom
        {
            get
            {
                return ToByteArray(Texto).Length;
            }
        }

        #region IBloqueConNombre implementation
        string IBloqueConNombre.NombreBloque
        {
            get
            {
                if (idUnico == null)
                {
                    idUnico = "str" + DateTime.Now.Ticks;
                }
                return idUnico;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region IComparable implementation
        public int CompareTo(object obj)
        {
            BloqueString bloque = obj as BloqueString;
            int compareTo;
            if (bloque != null)
                compareTo = Texto.CompareTo(bloque.Texto);
            else compareTo = -1;
            return compareTo;
        }
        #endregion
        public override string ToString()
        {
            return Texto;
        }

        public static int SetString(RomGba rom, string str, bool acabaEnFFByte = true)
        {
            int offsetEmpty = rom.Data.SearchEmptyBytes(ToByteArray(str).Length + 1);
            SetString(rom, offsetEmpty, str, acabaEnFFByte);
            return offsetEmpty;
        }
        public static void SetString(RomGba rom, int offsetInicio, string str, bool acabaEnFFByte = true)
        {

            byte[] bytesGbaString = ToByteArray(str, acabaEnFFByte);
            rom.Data.SetArray(offsetInicio, bytesGbaString);

        }
        public static void SetString(RomGba rom, int offsetInicio, BloqueString str)
        {

            SetString(rom, offsetInicio, str.Texto, str.AcabaEnFFByte);
        }

        public static int SetString(RomGba rom, BloqueString str)
        {
            int offsetEmpty = rom.Data.SearchEmptyBytes(str.LengthInnerRom);

            SetString(rom, offsetEmpty, str.Texto, str.AcabaEnFFByte);
            return offsetEmpty;
        }

        public static BloqueString GetString(RomGba rom, int offsetInicio, int longitud, bool acabaEnFFByte = true)
        {

            return GetString(rom.Data, offsetInicio, longitud, acabaEnFFByte);
        }

        public static BloqueString GetString(BloqueBytes blData, int offsetInicio, int longitud, bool acabaEnFFByte = true)
        {

            return GetString(blData.Bytes, offsetInicio, longitud, acabaEnFFByte);
        }
        public static BloqueString GetString(byte[] bStringGBA, int offsetInicio, int longitud, bool acabaEnFFByte = true)
        {
            if (bStringGBA == null || offsetInicio + longitud > bStringGBA.Length || offsetInicio < 0 || longitud < 0)
                throw new ArgumentException();
            return new BloqueString(offsetInicio, ToString(bStringGBA.SubArray(offsetInicio, longitud)), acabaEnFFByte);
        }

        public static BloqueString GetString(RomGba rom, int offsetInicio, byte marcaFin = 0xFF)
        {
            return GetString(rom.Data, offsetInicio, marcaFin);
        }
        public static BloqueString GetString(BloqueBytes blDatos, int offsetInicio, byte marcaFin = 0xFF)
        {//por revisar
            return GetString(blDatos.Bytes, offsetInicio, marcaFin);
        }
        public static BloqueString GetString(byte[] blDatos, int offsetInicio, byte marcaFin = 0xFF)
        {//por revisar
            return GetString(blDatos, offsetInicio, blDatos.IndexByte(offsetInicio, marcaFin) - offsetInicio);
        }
        public static void Remove(RomGba rom, int offsetString)
        {
            rom.Data.Remove(offsetString, rom.Data.Bytes.IndexByte(offsetString, 0xFF) - offsetString);
        }

        #region Tratar String Pokemon
        public static string ToString(byte[] bytesGBA)
        {
            const int MARCAFIN = 1;
            int longitudAdemas = 0;
            char[] chString;
            unsafe
            {
                byte* ptrBytesGBA;
                char* ptrChString;
                fixed (byte* ptBytesGBA = bytesGBA)
                {
                    ptrBytesGBA = ptBytesGBA;
                    for (int i = 0; i < bytesGBA.Length; i++)
                    {
                        switch (*ptrBytesGBA)
                        {
                            case 88:
                            case 87:
                            case 86:
                            case 85:
                            case 84:
                            case 83:
                            case 52:
                                longitudAdemas += 3;
                                break;
                            case 254:
                            case 252:
                            case 251:
                            case 250:
                            case 54:
                                longitudAdemas++;
                                break;
                            case 249:
                            case 248:
                            case 247:
                            case 239:
                            case 185:
                            case 183:
                            case 182:
                            case 181:
                            case 179:
                            case 177:
                            case 176:
                            case 124:
                            case 123:
                            case 122:
                            case 121:
                            case 89:
                                longitudAdemas += 2;
                                break;



                        }
                        ptrBytesGBA++;
                    }
                    chString = new char[bytesGBA.Length + longitudAdemas-MARCAFIN];
                    ptrBytesGBA = ptBytesGBA;
                    fixed (char* ptChString = chString)
                    {
                        ptrChString = ptChString;
                        for (int i = 0; i < bytesGBA.Length; i++)
                        {
                            switch (*ptrBytesGBA)
                            {
                                #region letters
                                case 0:
                                    *ptrChString = ' ';
                                    break;
                                case 1:
                                    *ptrChString = 'À';
                                    break;
                                case 2:
                                    *ptrChString = 'Á';
                                    break;
                                case 3:
                                    *ptrChString = 'Â';
                                    break;
                                case 4:
                                    *ptrChString = 'Ç';
                                    break;
                                case 5:
                                    *ptrChString = 'È';
                                    break;
                                case 6:
                                    *ptrChString = 'É';
                                    break;
                                case 7:
                                    *ptrChString = 'Ê';
                                    break;
                                case 8:
                                    *ptrChString = 'Ë';
                                    break;
                                case 9:
                                    *ptrChString = 'Ì';
                                    break;
                                case 11:
                                    *ptrChString = 'Î';
                                    break;
                                case 12:
                                    *ptrChString = 'Ï';
                                    break;
                                case 13:
                                    *ptrChString = 'Ò';
                                    break;
                                case 14:
                                    *ptrChString = 'Ó';
                                    break;
                                case 15:
                                    *ptrChString = 'Ô';
                                    break;
                                case 16:
                                    *ptrChString = 'Œ';
                                    break;
                                case 17:
                                    *ptrChString = 'Ù';
                                    break;
                                case 18:
                                    *ptrChString = 'Ú';
                                    break;
                                case 19:
                                    *ptrChString = 'Û';
                                    break;
                                case 20:
                                    *ptrChString = 'Ñ';
                                    break;
                                case 21:
                                    *ptrChString = 'ß';
                                    break;
                                case 22:
                                    *ptrChString = 'à';
                                    break;
                                case 23:
                                    *ptrChString = 'á';
                                    break;
                                case 25:
                                    *ptrChString = 'ç';
                                    break;
                                case 26:
                                    *ptrChString = 'è';
                                    break;
                                case 27:
                                    *ptrChString = 'é';
                                    break;
                                case 28:
                                    *ptrChString = 'ê';
                                    break;
                                case 29:
                                    *ptrChString = 'ë';
                                    break;
                                case 30:
                                    *ptrChString = 'ì';
                                    break;
                                case 32:
                                    *ptrChString = 'î';
                                    break;
                                case 33:
                                    *ptrChString = 'ï';
                                    break;
                                case 34:
                                    *ptrChString = 'ò';
                                    break;
                                case 35:
                                    *ptrChString = 'ó';
                                    break;
                                case 36:
                                    *ptrChString = 'ô';
                                    break;
                                case 37:
                                    *ptrChString = 'œ';
                                    break;
                                case 38:
                                    *ptrChString = 'ù';
                                    break;
                                case 39:
                                    *ptrChString = 'ú';
                                    break;
                                case 40:
                                    *ptrChString = 'û';
                                    break;
                                case 41:
                                    *ptrChString = 'ñ';
                                    break;
                                case 42:
                                    *ptrChString = 'º';
                                    break;
                                case 43:
                                    *ptrChString = 'ª';
                                    break;
                                case 45:
                                    *ptrChString = '&';
                                    break;
                                case 46:
                                    *ptrChString = '&';
                                    break;
                                case 52:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'L';
                                    ptrChString++;
                                    *ptrChString = 'v';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 53:
                                    *ptrChString = '=';
                                    break;
                                case 54:
                                    *ptrChString = ')';
                                    ptrChString++;
                                    *ptrChString = ';';
                                    break;
                                case 81:
                                    *ptrChString = '¿';
                                    break;
                                case 82:
                                    *ptrChString = '¡';
                                    break;
                                case 83:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'p';
                                    ptrChString++;
                                    *ptrChString = 'k';
                                    ptrChString++;
                                    *ptrChString = ']';

                                    break;
                                case 84:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'm';
                                    ptrChString++;
                                    *ptrChString = 'n';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 85:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'p';
                                    ptrChString++;
                                    *ptrChString = 'o';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 86:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'k';
                                    ptrChString++;
                                    *ptrChString = 'é';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 87:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'b';
                                    ptrChString++;
                                    *ptrChString = 'l';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 88:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'o';
                                    ptrChString++;
                                    *ptrChString = 'c';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 89:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'k';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 90:
                                    *ptrChString = 'Í';
                                    break;
                                case 91:
                                    *ptrChString = '%';
                                    break;
                                case 92:
                                    *ptrChString = '(';
                                    break;
                                case 93:
                                    *ptrChString = ')';
                                    break;
                                case 104:
                                    *ptrChString = 'â';
                                    break;
                                case 111:
                                    *ptrChString = 'í';
                                    break;
                                case 121:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'U';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 122:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'D';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 123:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'L';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 124:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'R';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 133:
                                    *ptrChString = '<';
                                    break;
                                case 134:
                                    *ptrChString = '>';
                                    break;
                                case 161:
                                    *ptrChString = '0';
                                    break;
                                case 162:
                                    *ptrChString = '1';
                                    break;
                                case 163:
                                    *ptrChString = '2';
                                    break;
                                case 164:
                                    *ptrChString = '3';
                                    break;
                                case 165:
                                    *ptrChString = '4';
                                    break;
                                case 166:
                                    *ptrChString = '5';
                                    break;
                                case 167:
                                    *ptrChString = '6';
                                    break;
                                case 168:
                                    *ptrChString = '7';
                                    break;
                                case 169:
                                    *ptrChString = '8';
                                    break;
                                case 170:
                                    *ptrChString = '9';
                                    break;
                                case 171:
                                    *ptrChString = '!';
                                    break;
                                case 172:
                                    *ptrChString = '?';
                                    break;
                                case 173:
                                    *ptrChString = '.';
                                    break;
                                case 174:
                                    *ptrChString = '-';
                                    break;
                                case 175:
                                    *ptrChString = '·';
                                    break;
                                case 176:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = '.';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 177:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = '\"';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 178:
                                    *ptrChString = '\"';
                                    break;
                                case 179:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = '\'';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 180:
                                    *ptrChString = '\'';

                                    break;
                                case 181:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'm';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 182:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'f';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 183:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = '$';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 184:
                                    *ptrChString = ',';
                                    break;
                                case 185:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'x';
                                    ptrChString++;
                                    *ptrChString = ']';
                                    break;
                                case 186:
                                    *ptrChString = '/';
                                    break;
                                case 187:
                                    *ptrChString = 'A';
                                    break;
                                case 188:
                                    *ptrChString = 'B';
                                    break;
                                case 189:
                                    *ptrChString = 'C';
                                    break;
                                case 190:
                                    *ptrChString = 'D';
                                    break;
                                case 191:
                                    *ptrChString = 'E';
                                    break;
                                case 192:
                                    *ptrChString = 'F';
                                    break;
                                case 193:
                                    *ptrChString = 'G';
                                    break;
                                case 194:
                                    *ptrChString = 'H';
                                    break;
                                case 195:
                                    *ptrChString = 'I';
                                    break;
                                case 196:
                                    *ptrChString = 'J';
                                    break;
                                case 197:
                                    *ptrChString = 'K';
                                    break;
                                case 198:
                                    *ptrChString = 'L';
                                    break;
                                case 199:
                                    *ptrChString = 'M';
                                    break;
                                case 200:
                                    *ptrChString = 'N';
                                    break;
                                case 201:
                                    *ptrChString = 'O';
                                    break;
                                case 202:
                                    *ptrChString = 'P';
                                    break;
                                case 203:
                                    *ptrChString = 'Q';
                                    break;
                                case 204:
                                    *ptrChString = 'R';
                                    break;
                                case 205:
                                    *ptrChString = 'S';
                                    break;
                                case 206:
                                    *ptrChString = 'T';
                                    break;
                                case 207:
                                    *ptrChString = 'U';
                                    break;
                                case 208:
                                    *ptrChString = 'V';
                                    break;
                                case 209:
                                    *ptrChString = 'W';
                                    break;
                                case 210:
                                    *ptrChString = 'X';
                                    break;
                                case 211:
                                    *ptrChString = 'Y';
                                    break;
                                case 212:
                                    *ptrChString = 'Z';
                                    break;
                                case 213:
                                    *ptrChString = 'a';
                                    break;
                                case 214:
                                    *ptrChString = 'b';
                                    break;
                                case 215:
                                    *ptrChString = 'c';
                                    break;
                                case 216:
                                    *ptrChString = 'd';
                                    break;
                                case 217:
                                    *ptrChString = 'e';
                                    break;
                                case 218:
                                    *ptrChString = 'f';
                                    break;
                                case 219:
                                    *ptrChString = 'g';
                                    break;
                                case 220:
                                    *ptrChString = 'h';
                                    break;
                                case 221:
                                    *ptrChString = 'i';
                                    break;
                                case 222:
                                    *ptrChString = 'j';
                                    break;
                                case 223:
                                    *ptrChString = 'k';
                                    break;
                                case 224:
                                    *ptrChString = 'l';
                                    break;
                                case 225:
                                    *ptrChString = 'm';
                                    break;
                                case 226:
                                    *ptrChString = 'n';
                                    break;
                                case 227:
                                    *ptrChString = 'o';
                                    break;
                                case 228:
                                    *ptrChString = 'p';
                                    break;
                                case 229:
                                    *ptrChString = 'q';
                                    break;
                                case 230:
                                    *ptrChString = 'r';
                                    break;
                                case 231:
                                    *ptrChString = 's';
                                    break;
                                case 232:
                                    *ptrChString = 't';
                                    break;
                                case 233:
                                    *ptrChString = 'u';
                                    break;
                                case 234:
                                    *ptrChString = 'v';
                                    break;
                                case 235:
                                    *ptrChString = 'w';
                                    break;
                                case 236:
                                    *ptrChString = 'x';
                                    break;
                                case 237:
                                    *ptrChString = 'y';
                                    break;
                                case 238:
                                    *ptrChString = 'z';
                                    break;
                                case 239:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = '>';
                                    ptrChString++;
                                    *ptrChString = ']';

                                    break;
                                case 240:
                                    *ptrChString = ':';
                                    break;
                                case 241:
                                    *ptrChString = 'Ä';
                                    break;
                                case 242:
                                    *ptrChString = 'Ö';
                                    break;
                                case 243:
                                    *ptrChString = 'Ü';
                                    break;
                                case 244:
                                    *ptrChString = 'ä';
                                    break;
                                case 245:
                                    *ptrChString = 'ö';
                                    break;
                                case 246:
                                    *ptrChString = 'ü';
                                    break;
                                case 247:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'u';
                                    ptrChString++;
                                    *ptrChString = ']';

                                    break;
                                case 248:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'd';
                                    ptrChString++;
                                    *ptrChString = ']';

                                    break;
                                case 249:
                                    *ptrChString = '[';
                                    ptrChString++;
                                    *ptrChString = 'l';
                                    ptrChString++;
                                    *ptrChString = ']';

                                    break;
                                case 250:
                                    *ptrChString = '\\';
                                    ptrChString++;
                                    *ptrChString = 'l';
                                    break;
                                case 251:
                                    *ptrChString = '\\';
                                    ptrChString++;
                                    *ptrChString = 'p';
                                    break;
                                case 252:
                                    *ptrChString = '\\';
                                    ptrChString++;
                                    *ptrChString = 'c';
                                    break;
                                case 253:
                                    *ptrChString = '\v';

                                    break;
                                case 254:
                                    *ptrChString = '\r';
                                    ptrChString++;
                                    *ptrChString = '\n';
                                    break;
                                case 255:
                                    *ptrChString = (char)255;
                                    break;
                                default:
                                    *ptrChString = ' ';
                                    break;
                                    #endregion
                            }
                            ptrBytesGBA++;
                            ptrChString++;
                        }
                    }
                }
            }
            return new string(chString);
        }


        public static byte[] ToByteArray(string texto, bool acabaEnFF = true)
        {

            if (texto == null)
                throw new ArgumentNullException();

            const byte MARCAFIN = 0xFF;

            byte[] bytesTexto;
            StringBuilder strTexto = new StringBuilder(texto);

            strTexto.Replace("\r", "");
            strTexto.Replace("\n", "\\n");
            strTexto.Replace("\v", "\\v");
            ParseaTexto(strTexto);

            bytesTexto = new byte[strTexto.Length + (acabaEnFF ? 1 : 0)];
            unsafe
            {
                char* ptrTexto;
                byte* ptrBytesTexto;
                fixed (char* ptTexto = strTexto.ToString())
                {
                    fixed (byte* ptBytesTexto = bytesTexto)
                    {
                        ptrBytesTexto = ptBytesTexto;
                        ptrTexto = ptTexto;
                        for (int i = 0; i < strTexto.Length; i++)
                        {
                            switch (*ptrTexto)
                            {
                                #region letters
                                case ' ':
                                    *ptrBytesTexto = 0;
                                    break;
                                case 'À':
                                    *ptrBytesTexto = 1;
                                    break;
                                case 'Á':
                                    *ptrBytesTexto = 2;
                                    break;
                                case 'Â':
                                    *ptrBytesTexto = 3;
                                    break;
                                case 'Ç':
                                    *ptrBytesTexto = 4;
                                    break;
                                case 'È':
                                    *ptrBytesTexto = 5;
                                    break;
                                case 'É':
                                    *ptrBytesTexto = 6;
                                    break;
                                case 'Ê':
                                    *ptrBytesTexto = 7;
                                    break;
                                case 'Ë':
                                    *ptrBytesTexto = 8;
                                    break;
                                case 'Ì':
                                    *ptrBytesTexto = 9;
                                    break;
                                case 'Î':
                                    *ptrBytesTexto = 11;
                                    break;
                                case 'Ï':
                                    *ptrBytesTexto = 12;
                                    break;
                                case 'Ò':
                                    *ptrBytesTexto = 13;
                                    break;
                                case 'Ó':
                                    *ptrBytesTexto = 14;
                                    break;
                                case 'Ô':
                                    *ptrBytesTexto = 15;
                                    break;
                                case 'Œ':
                                    *ptrBytesTexto = 16;
                                    break;
                                case 'Ù':
                                    *ptrBytesTexto = 17;
                                    break;
                                case 'Ú':
                                    *ptrBytesTexto = 18;
                                    break;
                                case 'Û':
                                    *ptrBytesTexto = 19;
                                    break;
                                case 'Ñ':
                                    *ptrBytesTexto = 20;
                                    break;
                                case 'ß':
                                    *ptrBytesTexto = 21;
                                    break;
                                case 'à':
                                    *ptrBytesTexto = 22;
                                    break;
                                case 'á':
                                    *ptrBytesTexto = 23;
                                    break;
                                case 'ç':
                                    *ptrBytesTexto = 25;
                                    break;
                                case 'è':
                                    *ptrBytesTexto = 26;
                                    break;
                                case 'é':
                                    *ptrBytesTexto = 27;
                                    break;
                                case 'ê':
                                    *ptrBytesTexto = 28;
                                    break;
                                case 'ë':
                                    *ptrBytesTexto = 29;
                                    break;
                                case 'ì':
                                    *ptrBytesTexto = 30;
                                    break;
                                case 'î':
                                    *ptrBytesTexto = 32;
                                    break;
                                case 'ï':
                                    *ptrBytesTexto = 33;
                                    break;
                                case 'ò':
                                    *ptrBytesTexto = 34;
                                    break;
                                case 'ó':
                                    *ptrBytesTexto = 35;
                                    break;
                                case 'ô':
                                    *ptrBytesTexto = 36;
                                    break;
                                case 'œ':
                                    *ptrBytesTexto = 37;
                                    break;
                                case 'ù':
                                    *ptrBytesTexto = 38;
                                    break;
                                case 'ú':
                                    *ptrBytesTexto = 39;
                                    break;
                                case 'û':
                                    *ptrBytesTexto = 40;
                                    break;
                                case 'ñ':
                                    *ptrBytesTexto = 41;
                                    break;
                                case 'º':
                                    *ptrBytesTexto = 42;
                                    break;
                                case 'ª':
                                    *ptrBytesTexto = 43;
                                    break;
                                case '&':
                                    *ptrBytesTexto = 46;
                                    break;

                                case (char)CaracteresEspeciales.Lv:
                                    *ptrBytesTexto = 52;
                                    break;
                                case '=':
                                    *ptrBytesTexto = 53;
                                    break;
                                case ';':
                                    *ptrBytesTexto = 54;
                                    break;
                                case '¿':
                                    *ptrBytesTexto = 81;
                                    break;
                                case '¡':
                                    *ptrBytesTexto = 82;
                                    break;
                                case (char)CaracteresEspeciales.pk:
                                    *ptrBytesTexto = 83;
                                    break;
                                case (char)CaracteresEspeciales.mn:
                                    *ptrBytesTexto = 84;
                                    break;
                                case (char)CaracteresEspeciales.po:
                                    *ptrBytesTexto = 85;
                                    break;
                                case (char)CaracteresEspeciales.ké:
                                    *ptrBytesTexto = 86;
                                    break;
                                case (char)CaracteresEspeciales.bl:
                                    *ptrBytesTexto = 87;
                                    break;
                                case (char)CaracteresEspeciales.oc:
                                    *ptrBytesTexto = 88;
                                    break;
                                case (char)CaracteresEspeciales.k:
                                    *ptrBytesTexto = 89;
                                    break;
                                case 'Í':
                                    *ptrBytesTexto = 90;
                                    break;
                                case '%':
                                    *ptrBytesTexto = 91;
                                    break;
                                case '(':
                                    *ptrBytesTexto = 92;
                                    break;
                                case ')':
                                    *ptrBytesTexto = 93;
                                    break;
                                case 'â':
                                    *ptrBytesTexto = 104;
                                    break;
                                case 'í':
                                    *ptrBytesTexto = 111;
                                    break;
                                case (char)CaracteresEspeciales.U:
                                    *ptrBytesTexto = 131;
                                    break;
                                case (char)CaracteresEspeciales.D:
                                    *ptrBytesTexto = 122;
                                    break;
                                case (char)CaracteresEspeciales.L:
                                    *ptrBytesTexto = 123;
                                    break;
                                case (char)CaracteresEspeciales.R:
                                    *ptrBytesTexto = 124;
                                    break;
                                case '<':
                                    *ptrBytesTexto = 133;
                                    break;
                                case '>':
                                    *ptrBytesTexto = 134;
                                    break;
                                case '0':
                                    *ptrBytesTexto = 161;
                                    break;
                                case '1':
                                    *ptrBytesTexto = 162;
                                    break;
                                case '2':
                                    *ptrBytesTexto = 163;
                                    break;
                                case '3':
                                    *ptrBytesTexto = 164;
                                    break;
                                case '4':
                                    *ptrBytesTexto = 165;
                                    break;
                                case '5':
                                    *ptrBytesTexto = 166;
                                    break;
                                case '6':
                                    *ptrBytesTexto = 167;
                                    break;
                                case '7':
                                    *ptrBytesTexto = 168;
                                    break;
                                case '8':
                                    *ptrBytesTexto = 169;
                                    break;
                                case '9':
                                    *ptrBytesTexto = 170;
                                    break;
                                case '!':
                                    *ptrBytesTexto = 171;
                                    break;
                                case '?':
                                    *ptrBytesTexto = 172;
                                    break;
                                case '.':
                                    *ptrBytesTexto = 173;
                                    break;
                                case '-':
                                    *ptrBytesTexto = 174;
                                    break;
                                case '·':
                                    *ptrBytesTexto = 175;
                                    break;
                                case (char)CaracteresEspeciales.Punto:
                                    *ptrBytesTexto = 176;
                                    break;
                                case (char)CaracteresEspeciales.Comitas:
                                    *ptrBytesTexto = 177;
                                    break;
                                case '\"':
                                    *ptrBytesTexto = 178;
                                    break;
                                case (char)CaracteresEspeciales.Comita:
                                    *ptrBytesTexto = 179;
                                    break;
                                case '\'':
                                    *ptrBytesTexto = 180;
                                    break;

                                case (char)CaracteresEspeciales.m:
                                    *ptrBytesTexto = 181;
                                    break;
                                case (char)CaracteresEspeciales.f:
                                    *ptrBytesTexto = 182;
                                    break;
                                case (char)CaracteresEspeciales.Dolar:
                                    *ptrBytesTexto = 183;
                                    break;
                                case ',':
                                    *ptrBytesTexto = 184;
                                    break;
                                case (char)CaracteresEspeciales.x:
                                    *ptrBytesTexto = 185;
                                    break;
                                case '/':
                                    *ptrBytesTexto = 186;
                                    break;
                                case 'A':
                                    *ptrBytesTexto = 187;
                                    break;
                                case 'B':
                                    *ptrBytesTexto = 188;
                                    break;
                                case 'C':
                                    *ptrBytesTexto = 189;
                                    break;
                                case 'D':
                                    *ptrBytesTexto = 190;
                                    break;
                                case 'E':
                                    *ptrBytesTexto = 191;
                                    break;
                                case 'F':
                                    *ptrBytesTexto = 192;
                                    break;
                                case 'G':
                                    *ptrBytesTexto = 193;
                                    break;
                                case 'H':
                                    *ptrBytesTexto = 194;
                                    break;
                                case 'I':
                                    *ptrBytesTexto = 195;
                                    break;
                                case 'J':
                                    *ptrBytesTexto = 196;
                                    break;
                                case 'K':
                                    *ptrBytesTexto = 197;
                                    break;
                                case 'L':
                                    *ptrBytesTexto = 198;
                                    break;
                                case 'M':
                                    *ptrBytesTexto = 199;
                                    break;
                                case 'N':
                                    *ptrBytesTexto = 200;
                                    break;
                                case 'O':
                                    *ptrBytesTexto = 201;
                                    break;
                                case 'P':
                                    *ptrBytesTexto = 202;
                                    break;
                                case 'Q':
                                    *ptrBytesTexto = 203;
                                    break;
                                case 'R':
                                    *ptrBytesTexto = 204;
                                    break;
                                case 'S':
                                    *ptrBytesTexto = 205;
                                    break;
                                case 'T':
                                    *ptrBytesTexto = 206;
                                    break;
                                case 'U':
                                    *ptrBytesTexto = 207;
                                    break;
                                case 'V':
                                    *ptrBytesTexto = 208;
                                    break;
                                case 'W':
                                    *ptrBytesTexto = 209;
                                    break;
                                case 'X':
                                    *ptrBytesTexto = 210;
                                    break;
                                case 'Y':
                                    *ptrBytesTexto = 211;
                                    break;
                                case 'Z':
                                    *ptrBytesTexto = 212;
                                    break;
                                case 'a':
                                    *ptrBytesTexto = 213;
                                    break;
                                case 'b':
                                    *ptrBytesTexto = 214;
                                    break;
                                case 'c':
                                    *ptrBytesTexto = 215;
                                    break;
                                case 'd':
                                    *ptrBytesTexto = 216;
                                    break;
                                case 'e':
                                    *ptrBytesTexto = 217;
                                    break;
                                case 'f':
                                    *ptrBytesTexto = 218;
                                    break;
                                case 'g':
                                    *ptrBytesTexto = 219;
                                    break;
                                case 'h':
                                    *ptrBytesTexto = 220;
                                    break;
                                case 'i':
                                    *ptrBytesTexto = 221;
                                    break;
                                case 'j':
                                    *ptrBytesTexto = 222;
                                    break;
                                case 'k':
                                    *ptrBytesTexto = 223;
                                    break;
                                case 'l':
                                    *ptrBytesTexto = 224;
                                    break;
                                case 'm':
                                    *ptrBytesTexto = 225;
                                    break;
                                case 'n':
                                    *ptrBytesTexto = 226;
                                    break;
                                case 'o':
                                    *ptrBytesTexto = 227;
                                    break;
                                case 'p':
                                    *ptrBytesTexto = 228;
                                    break;
                                case 'q':
                                    *ptrBytesTexto = 229;
                                    break;
                                case 'r':
                                    *ptrBytesTexto = 230;
                                    break;
                                case 's':
                                    *ptrBytesTexto = 231;
                                    break;
                                case 't':
                                    *ptrBytesTexto = 232;
                                    break;
                                case 'u':
                                    *ptrBytesTexto = 233;
                                    break;
                                case 'v':
                                    *ptrBytesTexto = 234;
                                    break;
                                case 'w':
                                    *ptrBytesTexto = 235;
                                    break;
                                case 'x':
                                    *ptrBytesTexto = 236;
                                    break;
                                case 'y':
                                    *ptrBytesTexto = 237;
                                    break;
                                case 'z':
                                    *ptrBytesTexto = 238;
                                    break;
                                case (char)CaracteresEspeciales.MasGrande:
                                    *ptrBytesTexto = 239;
                                    break;
                                case ':':
                                    *ptrBytesTexto = 240;
                                    break;
                                case 'Ä':
                                    *ptrBytesTexto = 241;
                                    break;
                                case 'Ö':
                                    *ptrBytesTexto = 242;
                                    break;
                                case 'Ü':
                                    *ptrBytesTexto = 243;
                                    break;
                                case 'ä':
                                    *ptrBytesTexto = 244;
                                    break;
                                case 'ö':
                                    *ptrBytesTexto = 245;
                                    break;
                                case 'ü':
                                    *ptrBytesTexto = 246;
                                    break;
                                case (char)CaracteresEspeciales.u:
                                    *ptrBytesTexto = 247;
                                    break;
                                case (char)CaracteresEspeciales.d:
                                    *ptrBytesTexto = 248;
                                    break;
                                case (char)CaracteresEspeciales.l:
                                    *ptrBytesTexto = 247;
                                    break;
                                case (char)CaracteresEspeciales.EspL:
                                    *ptrBytesTexto = 250;
                                    break;
                                case (char)CaracteresEspeciales.EspP:
                                    *ptrBytesTexto = 251;
                                    break;
                                case (char)CaracteresEspeciales.EspC:
                                    *ptrBytesTexto = 252;
                                    break;
                                case (char)CaracteresEspeciales.EspV:
                                    *ptrBytesTexto = 253;
                                    break;
                                case (char)CaracteresEspeciales.EspN:
                                    *ptrBytesTexto = 254;
                                    break;
                                case (char)CaracteresEspeciales.Vacio:
                                case (char)255:
                                    *ptrBytesTexto = 255;
                                    break;
                                default:
                                    *ptrBytesTexto = 0;
                                    break;
                                    #endregion
                            }
                            ptrTexto++;
                            ptrBytesTexto++;

                        }
                    }
                }
            }
            if (acabaEnFF)
                bytesTexto[bytesTexto.Length - 1] = MARCAFIN;
            return bytesTexto;
        }

        private static void ParseaTexto(StringBuilder textoParseado)
        {

            if (textoParseado.Length == 0)
            {
                textoParseado.Clear();
                textoParseado.Append((char)CaracteresEspeciales.Vacio);
            }
            else
            {
                textoParseado.Replace("[Lv]", (char)CaracteresEspeciales.Lv + "");
                textoParseado.Replace("[pk]", (char)CaracteresEspeciales.pk + "");
                textoParseado.Replace("[mn]", (char)CaracteresEspeciales.mn + "");
                textoParseado.Replace("[po]", (char)CaracteresEspeciales.po + "");
                textoParseado.Replace("[ké]", (char)CaracteresEspeciales.ké + "");
                textoParseado.Replace("[bl]", (char)CaracteresEspeciales.bl + "");
                textoParseado.Replace("[oc]", (char)CaracteresEspeciales.oc + "");
                textoParseado.Replace("[k]", (char)CaracteresEspeciales.k + "");
                textoParseado.Replace("[U]", (char)CaracteresEspeciales.U + "");
                textoParseado.Replace("[.]", (char)CaracteresEspeciales.Punto + "");
                textoParseado.Replace("[R]", (char)CaracteresEspeciales.R + "");
                textoParseado.Replace("[L]", (char)CaracteresEspeciales.L + "");
                textoParseado.Replace("[D]", (char)CaracteresEspeciales.D + "");
                textoParseado.Replace("[\"]", (char)CaracteresEspeciales.Comitas + "");
                textoParseado.Replace("[']", (char)CaracteresEspeciales.Comita + "");
                textoParseado.Replace("[m]", (char)CaracteresEspeciales.m + "");
                textoParseado.Replace("[f]", (char)CaracteresEspeciales.f + "");
                textoParseado.Replace("[>]", (char)CaracteresEspeciales.MasGrande + "");
                textoParseado.Replace("[$]", (char)CaracteresEspeciales.Dolar + "");
                textoParseado.Replace("[x]", (char)CaracteresEspeciales.x + "");
                textoParseado.Replace("[u]", (char)CaracteresEspeciales.u + "");
                textoParseado.Replace("[d]", (char)CaracteresEspeciales.d + "");
                textoParseado.Replace("[l]", (char)CaracteresEspeciales.l + "");
                textoParseado.Replace("\\l", (char)CaracteresEspeciales.EspL + "");
                textoParseado.Replace("\\p", (char)CaracteresEspeciales.EspP + "");
                textoParseado.Replace("\\c", (char)CaracteresEspeciales.EspC + "");
                textoParseado.Replace("\\v", (char)CaracteresEspeciales.EspV + "");
                textoParseado.Replace("\\n", (char)CaracteresEspeciales.EspN + "");

            }
          
        }

        #endregion
        public static implicit operator string(BloqueString bloque)
        {
            return bloque.Texto;
        }
    }
}
