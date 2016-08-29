/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 20:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
using Gabriel.Cat;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TratarString.
	/// </summary>
	public class BloqueString
	{
		enum CaracteresEspeciales
        {
            Lv,//[Lv]
            pk,//'[pk]'
            mn,//'[mn]'
            po,//'[po]'
            ké,//'[ké]'
            bl,//'[bl]'
            oc,//'[oc]'
            k,//'[k]'
            U,//'[U]'
            Punto,//'[.]'
            R,//'[R]'
            L,//'[L]'
            D,//'[D]'
            Comitas,//'[\"]'
            Comita,//'[']'
            m,//'[m]'
            f,//'[f]'
            MasGrande,//'[>]'
            Dolar,//'[$]'
            x,//'[x]'
            u,//'[u]'
            d,//'[d]'
            l,//'[l]'
            EspL,//'\\l'
            EspP,//'\\p'
            EspC,//'\\c'
            EspV,//'\\v'
            EspN,//'\\n'
            Vacio,//''
        }
		
		public const int MAXIMODECARACTERESDESHABILITADO=0;
		int maxCaracteres;
		Hex offsetInicio;
		string texto;
		public BloqueString(int maxCaracteres):this("",maxCaracteres){}
		public BloqueString(string texto):this(0,texto,MAXIMODECARACTERESDESHABILITADO){}
		public BloqueString(string texto,int maxCaracteres):this(0,texto,maxCaracteres){}
		public BloqueString(Hex offsetInicio,string texto):this(offsetInicio,texto,MAXIMODECARACTERESDESHABILITADO){}
		public BloqueString(Hex offsetInicio,string texto,int maxCaracteres)
		{
			this.maxCaracteres=maxCaracteres;
			Texto=texto;
			OffsetInicio=offsetInicio;
		}

		public int MaxCaracteres {
			get {
				return maxCaracteres;
			}
			set {
				if(value<MAXIMODECARACTERESDESHABILITADO)value=MAXIMODECARACTERESDESHABILITADO;
				maxCaracteres = value;
			}
		}
		public string Texto {
			get {
				return texto;
			}
			set {
				if(value==null)value="";
				if(maxCaracteres!=MAXIMODECARACTERESDESHABILITADO&&value.Length>maxCaracteres)
					throw new ArgumentOutOfRangeException();
				texto = value;
			}
		}

		public Hex OffsetInicio {
			get {
				return offsetInicio;
			}
			set {
				if(value<0)throw new ArgumentOutOfRangeException();
				offsetInicio = value;
			}
		}
		public Hex OffsetFin{
			get{return OffsetInicio+Texto.Length;}
		}
		public static void SetString(RomPokemon rom,Hex offsetInicio,string str){
			SetString(rom,new BloqueString(offsetInicio,str));
		}
		public static void SetString(RomPokemon rom,BloqueString str){
		
			BloqueBytes bytesString=new BloqueBytes(str.OffsetInicio,GetBytes(str.Texto));
			BloqueBytes.SetBytes(rom,bytesString);
		}
		public static BloqueString GetString(RomPokemon rom,Hex offsetInicio,Hex longitud){
		
			return new BloqueString(offsetInicio,GetText(BloqueBytes.GetBytes(rom,offsetInicio,longitud).Bytes));
		}
		#region Tratar String Pokemon
        private static string GetText(byte[] bytesGBA)
        {
            StringBuilder texto = new StringBuilder();
            for (int i = 0; i < bytesGBA.Length; i++)
            {
                switch (bytesGBA[i])
                {
                    #region letters
                    case 0:
                        texto .Append( " ");
                        break;
                    case 1:
                        texto .Append( "À");
                        break;
                    case 2:
                        texto .Append( "Á");
                        break;
                    case 3:
                        texto .Append( "Â");
                        break;
                    case 4:
                        texto .Append( "Ç");
                        break;
                    case 5:
                        texto .Append( "È");
                        break;
                    case 6:
                        texto .Append( "É");
                        break;
                    case 7:
                        texto .Append( "Ê");
                        break;
                    case 8:
                        texto .Append( "Ë");
                        break;
                    case 9:
                        texto .Append( "Ì");
                        break;
                    case 11:
                        texto .Append( "Î");
                        break;
                    case 12:
                        texto .Append( "Ï");
                        break;
                    case 13:
                        texto .Append( "Ò");
                        break;
                    case 14:
                        texto .Append( "Ó");
                        break;
                    case 15:
                        texto .Append( "Ô");
                        break;
                    case 16:
                        texto .Append( "Œ");
                        break;
                    case 17:
                        texto .Append( "Ù");
                        break;
                    case 18:
                        texto .Append( "Ú");
                        break;
                    case 19:
                        texto .Append( "Û");
                        break;
                    case 20:
                        texto .Append( "Ñ");
                        break;
                    case 21:
                        texto .Append( "ß");
                        break;
                    case 22:
                        texto .Append( "à");
                        break;
                    case 23:
                        texto .Append( "á");
                        break;
                    case 25:
                        texto .Append( "ç");
                        break;
                    case 26:
                        texto .Append( "è");
                        break;
                    case 27:
                        texto .Append( "é");
                        break;
                    case 28:
                        texto .Append( "ê");
                        break;
                    case 29:
                        texto .Append( "ë");
                        break;
                    case 30:
                        texto .Append( "ì");
                        break;
                    case 32:
                        texto .Append( "î");
                        break;
                    case 33:
                        texto .Append( "ï");
                        break;
                    case 34:
                        texto .Append( "ò");
                        break;
                    case 35:
                        texto .Append( "ó");
                        break;
                    case 36:
                        texto .Append( "ô");
                        break;
                    case 37:
                        texto .Append( "œ");
                        break;
                    case 38:
                        texto .Append( "ù");
                        break;
                    case 39:
                        texto .Append( "ú");
                        break;
                    case 40:
                        texto .Append( "û");
                        break;
                    case 41:
                        texto .Append( "ñ");
                        break;
                    case 42:
                        texto .Append( "º");
                        break;
                    case 43:
                        texto .Append( "ª");
                        break;
                    case 45:
                        texto .Append( "&");
                        break;
                    case 46:
                        texto .Append( "&");
                        break;
                    case 52:
                        texto .Append( "[Lv]");
                        break;
                    case 53:
                        texto .Append( "=");
                        break;
                    case 54:
                        texto .Append( ");");
                        break;
                    case 81:
                        texto .Append( "¿");
                        break;
                    case 82:
                        texto .Append( "¡");
                        break;
                    case 83:
                        texto .Append( "[pk]");
                        break;
                    case 84:
                        texto .Append( "[mn]");
                        break;
                    case 85:
                        texto .Append( "[po]");
                        break;
                    case 86:
                        texto .Append( "[ké]");
                        break;
                    case 87:
                        texto .Append( "[bl]");
                        break;
                    case 88:
                        texto .Append( "[oc]");
                        break;
                    case 89:
                        texto .Append( "[k]");
                        break;
                    case 90:
                        texto .Append( "Í");
                        break;
                    case 91:
                        texto .Append( "%");
                        break;
                    case 92:
                        texto .Append( "(");
                        break;
                    case 93:
                        texto .Append( ")");
                        break;
                    case 104:
                        texto .Append( "â");
                        break;
                    case 111:
                        texto .Append( "í");
                        break;
                    case 121:
                        texto .Append( "[U]");
                        break;
                    case 122:
                        texto .Append( "[D]");
                        break;
                    case 123:
                        texto .Append( "[L]");
                        break;
                    case 124:
                        texto .Append( "[R]");
                        break;
                    case 133:
                        texto .Append( "<");
                        break;
                    case 134:
                        texto .Append( ">");
                        break;
                    case 161:
                        texto .Append( "0");
                        break;
                    case 162:
                        texto .Append( "1");
                        break;
                    case 163:
                        texto .Append( "2");
                        break;
                    case 164:
                        texto .Append( "3");
                        break;
                    case 165:
                        texto .Append( "4");
                        break;
                    case 166:
                        texto .Append( "5");
                        break;
                    case 167:
                        texto .Append( "6");
                        break;
                    case 168:
                        texto .Append( "7");
                        break;
                    case 169:
                        texto .Append( "8");
                        break;
                    case 170:
                        texto .Append( "9");
                        break;
                    case 171:
                        texto .Append( "!");
                        break;
                    case 172:
                        texto .Append( "?");
                        break;
                    case 173:
                        texto .Append( ".");
                        break;
                    case 174:
                        texto .Append( "-");
                        break;
                    case 175:
                        texto .Append( "·");
                        break;
                    case 176:
                        texto .Append( "[.]");
                        break;
                    case 177:
                        texto .Append( "[\"]");
                        break;
                    case 178:
                        texto .Append( "\"");
                        break;
                    case 179:
                        texto .Append( "[']");
                        break;
                    case 180:
                        texto .Append( "'");
                        break;
                    case 181:
                        texto .Append( "[m]");
                        break;
                    case 182:
                        texto .Append( "[f]");
                        break;
                    case 183:
                        texto .Append( "[$]");
                        break;
                    case 184:
                        texto .Append( ",");
                        break;
                    case 185:
                        texto .Append( "[x]");
                        break;
                    case 186:
                        texto .Append( "/");
                        break;
                    case 187:
                        texto .Append( "A");
                        break;
                    case 188:
                        texto .Append( "B");
                        break;
                    case 189:
                        texto .Append( "C");
                        break;
                    case 190:
                        texto .Append( "D");
                        break;
                    case 191:
                        texto .Append( "E");
                        break;
                    case 192:
                        texto .Append( "F");
                        break;
                    case 193:
                        texto .Append( "G");
                        break;
                    case 194:
                        texto .Append( "H");
                        break;
                    case 195:
                        texto .Append( "I");
                        break;
                    case 196:
                        texto .Append( "J");
                        break;
                    case 197:
                        texto .Append( "K");
                        break;
                    case 198:
                        texto .Append( "L");
                        break;
                    case 199:
                        texto .Append( "M");
                        break;
                    case 200:
                        texto .Append( "N");
                        break;
                    case 201:
                        texto .Append( "O");
                        break;
                    case 202:
                        texto .Append( "P");
                        break;
                    case 203:
                        texto .Append( "Q");
                        break;
                    case 204:
                        texto .Append( "R");
                        break;
                    case 205:
                        texto .Append( "S");
                        break;
                    case 206:
                        texto .Append( "T");
                        break;
                    case 207:
                        texto .Append( "U");
                        break;
                    case 208:
                        texto .Append( "V");
                        break;
                    case 209:
                        texto .Append( "W");
                        break;
                    case 210:
                        texto .Append( "X");
                        break;
                    case 211:
                        texto .Append( "Y");
                        break;
                    case 212:
                        texto .Append( "Z");
                        break;
                    case 213:
                        texto .Append( "a");
                        break;
                    case 214:
                        texto .Append( "b");
                        break;
                    case 215:
                        texto .Append( "c");
                        break;
                    case 216:
                        texto .Append( "d");
                        break;
                    case 217:
                        texto .Append( "e");
                        break;
                    case 218:
                        texto .Append( "f");
                        break;
                    case 219:
                        texto .Append( "g");
                        break;
                    case 220:
                        texto .Append( "h");
                        break;
                    case 221:
                        texto .Append( "i");
                        break;
                    case 222:
                        texto .Append( "j");
                        break;
                    case 223:
                        texto .Append( "k");
                        break;
                    case 224:
                        texto .Append( "l");
                        break;
                    case 225:
                        texto .Append( "m");
                        break;
                    case 226:
                        texto .Append( "n");
                        break;
                    case 227:
                        texto .Append( "o");
                        break;
                    case 228:
                        texto .Append( "p");
                        break;
                    case 229:
                        texto .Append( "q");
                        break;
                    case 230:
                        texto .Append( "r");
                        break;
                    case 231:
                        texto .Append( "s");
                        break;
                    case 232:
                        texto .Append( "t");
                        break;
                    case 233:
                        texto .Append( "u");
                        break;
                    case 234:
                        texto .Append( "v");
                        break;
                    case 235:
                        texto .Append( "w");
                        break;
                    case 236:
                        texto .Append( "x");
                        break;
                    case 237:
                        texto .Append( "y");
                        break;
                    case 238:
                        texto .Append( "z");
                        break;
                    case 239:
                        texto .Append( "[>]");
                        break;
                    case 240:
                        texto .Append( ":");
                        break;
                    case 241:
                        texto .Append( "Ä");
                        break;
                    case 242:
                        texto .Append( "Ö");
                        break;
                    case 243:
                        texto .Append( "Ü");
                        break;
                    case 244:
                        texto .Append( "ä");
                        break;
                    case 245:
                        texto .Append( "ö");
                        break;
                    case 246:
                        texto .Append( "ü");
                        break;
                    case 247:
                        texto .Append( "[u]");
                        break;
                    case 248:
                        texto .Append( "[d]");
                        break;
                    case 249:
                        texto .Append( "[l]");
                        break;
                    case 250:
                        texto .Append( "\\l");
                        break;
                    case 251:
                        texto .Append( "\\p");
                        break;
                    case 252:
                        texto .Append( "\\c");
                        break;
                    case 253:
                        texto .Append( "\\v");
                        break;
                    case 254:
                        texto .Append( "\\n");
                        break;
                    case 255:
                        texto .Append( "");
                        break;
                    default:
                        texto .Append( " ");
                        break;
                        #endregion
                }
            }
            texto.Replace("\\n", "\r\n");
            texto.Replace("\\v", "\v");
            return texto.ToString();
        }
        private static byte[] GetBytes(string texto)
        {
            if (texto == null)
                throw new ArgumentNullException();
            byte[] bytesTexto;
            texto = texto.Replace("\r","").Replace("\n", "\\n").Replace("\v","\\v");
            texto = ParseaTexto(texto);
            bytesTexto = new byte[texto.Length];
            for (int i = 0; i < texto.Length; i++)
            {
                       switch(texto[i])
                {
                    #region letters
                    case ' ':
                        bytesTexto[i] = 0;
                        break;
                    case 'À':
                        bytesTexto[i] = 1;
                        break;
                    case 'Á':
                        bytesTexto[i] = 2;
                        break;
                    case 'Â':
                        bytesTexto[i] = 3;
                        break;
                    case 'Ç':
                        bytesTexto[i] = 4;
                        break;
                    case 'È':
                        bytesTexto[i] = 5;
                        break;
                    case 'É':
                        bytesTexto[i] = 6;
                        break;
                    case 'Ê':
                        bytesTexto[i] = 7;
                        break;
                    case 'Ë':
                        bytesTexto[i] = 8;
                        break;
                    case 'Ì':
                        bytesTexto[i] = 9;
                        break;
                    case 'Î':
                        bytesTexto[i] = 11;
                        break;
                    case 'Ï':
                        bytesTexto[i] = 12;
                        break;
                    case 'Ò':
                        bytesTexto[i] = 13;
                        break;
                    case 'Ó':
                        bytesTexto[i] = 14 ;
                        break;
                    case 'Ô':
                        bytesTexto[i] = 15 ;
                        break;
                    case 'Œ':
                        bytesTexto[i] = 16;
                        break;
                    case 'Ù':
                        bytesTexto[i] = 17;
                        break;
                    case 'Ú':
                        bytesTexto[i] = 18;
                        break;
                    case 'Û':
                        bytesTexto[i] = 19;
                        break;
                    case 'Ñ':
                        bytesTexto[i] = 20;
                        break;
                    case 'ß':
                        bytesTexto[i] = 21;
                        break;
                    case 'à':
                        bytesTexto[i] = 22;
                        break;
                    case 'á':
                        bytesTexto[i] = 23;
                        break;
                    case 'ç':
                        bytesTexto[i] = 25;
                        break;
                    case 'è':
                        bytesTexto[i] = 26;
                        break;
                    case 'é':
                        bytesTexto[i] = 27;
                        break;
                    case 'ê':
                        bytesTexto[i] = 28;
                        break;
                    case 'ë':
                        bytesTexto[i] = 29;
                        break;
                    case 'ì':
                        bytesTexto[i] = 30;
                        break;
                    case 'î':
                        bytesTexto[i] = 32;
                        break;
                    case 'ï':
                        bytesTexto[i] = 33;
                        break;
                    case 'ò':
                        bytesTexto[i] = 34;
                        break;
                    case 'ó':
                        bytesTexto[i] = 35;
                        break;
                    case 'ô':
                        bytesTexto[i] = 36;
                        break;
                    case 'œ':
                        bytesTexto[i] = 37;
                        break;
                    case 'ù':
                        bytesTexto[i] = 38;
                        break;
                    case 'ú':
                        bytesTexto[i] = 39;
                        break;
                    case 'û':
                        bytesTexto[i] = 40;
                        break;
                    case 'ñ':
                        bytesTexto[i] = 41;
                        break;
                    case 'º':
                        bytesTexto[i] = 42;
                        break;
                    case 'ª':
                        bytesTexto[i] = 43;
                        break;
                    case '&':
                        bytesTexto[i] = 46;
                        break;

                    case (char)CaracteresEspeciales.Lv:
                        bytesTexto[i] = 52;
                        break;
                    case '=':
                        bytesTexto[i] = 53;
                        break;
                    case ';':
                        bytesTexto[i] = 54;
                        break;
                    case '¿':
                        bytesTexto[i] = 81;
                        break;
                    case '¡':
                        bytesTexto[i] = 82;
                        break;
                    case (char)CaracteresEspeciales.pk:
                        bytesTexto[i] = 83;
                        break;
                    case (char)CaracteresEspeciales.mn:
                        bytesTexto[i] = 84;
                        break;
                    case (char)CaracteresEspeciales.po:
                        bytesTexto[i] = 85;
                        break;
                    case (char)CaracteresEspeciales.ké:
                        bytesTexto[i] = 86;
                        break;
                    case (char)CaracteresEspeciales.bl:
                        bytesTexto[i] = 87;
                        break;
                    case (char)CaracteresEspeciales.oc:
                        bytesTexto[i] = 88;
                        break;
                    case (char)CaracteresEspeciales.k:
                        bytesTexto[i] = 89;
                        break;
                    case 'Í':
                        bytesTexto[i] = 90;
                        break;
                    case '%':
                        bytesTexto[i] = 91;
                        break;
                    case '(':
                        bytesTexto[i] = 92;
                        break;
                    case ')':
                        bytesTexto[i] = 93;
                        break;
                    case 'â':
                        bytesTexto[i] = 104;
                        break;
                    case 'í':
                        bytesTexto[i] = 111;
                        break;
                    case (char)CaracteresEspeciales.U:
                        bytesTexto[i] = 131;
                        break;
                    case (char)CaracteresEspeciales.D:
                        bytesTexto[i] = 122;
                        break;
                    case (char)CaracteresEspeciales.L:
                        bytesTexto[i] = 123;
                        break;
                    case (char)CaracteresEspeciales.R:
                        bytesTexto[i] = 124;
                        break;
                    case '<':
                        bytesTexto[i] = 133;
                        break;
                    case '>':
                        bytesTexto[i] = 134;
                        break;
                    case '0':
                        bytesTexto[i] = 161;
                        break;
                    case '1':
                        bytesTexto[i] = 162;
                        break;
                    case '2':
                        bytesTexto[i] = 163;
                        break;
                    case '3':
                        bytesTexto[i] = 164;
                        break;
                    case '4':
                        bytesTexto[i] = 165;
                        break;
                    case '5':
                        bytesTexto[i] = 166;
                        break;
                    case '6':
                        bytesTexto[i] = 167;
                        break;
                    case '7':
                        bytesTexto[i] = 168;
                        break;
                    case '8':
                        bytesTexto[i] = 169;
                        break;
                    case '9':
                        bytesTexto[i] = 170;
                        break;
                    case '!':
                        bytesTexto[i] = 171;
                        break;
                    case '?':
                        bytesTexto[i] = 172;
                        break;
                    case '.':
                        bytesTexto[i] = 173;
                        break;
                    case '-':
                        bytesTexto[i] = 174;
                        break;
                    case '·':
                        bytesTexto[i] = 175;
                        break;
                    case (char)CaracteresEspeciales.Punto:
                        bytesTexto[i] = 176;
                        break;
                    case (char)CaracteresEspeciales.Comitas:
                        bytesTexto[i] = 177;
                        break;
                    case '\"':
                        bytesTexto[i] = 178;
                        break;
                    case (char)CaracteresEspeciales.Comita:
                        bytesTexto[i] = 179;
                break;
                    case '\'':
                        bytesTexto[i] = 180;
                break;
                    
                    case (char)CaracteresEspeciales.m:
                        bytesTexto[i] = 181;
                        break;
                    case (char)CaracteresEspeciales.f:
                        bytesTexto[i] = 182;
                        break;
                    case (char)CaracteresEspeciales.Dolar:
                        bytesTexto[i] = 183;
                        break;
                    case ',':
                        bytesTexto[i] = 184;
                        break;
                    case (char)CaracteresEspeciales.x:
                        bytesTexto[i] = 185;
                        break;
                    case '/':
                        bytesTexto[i] = 186;
                        break;
                    case 'A':
                        bytesTexto[i] = 187;
                        break;
                    case 'B':
                        bytesTexto[i] = 188;
                        break;
                    case 'C':
                        bytesTexto[i] = 189;
                        break;
                    case 'D':
                        bytesTexto[i] = 190;
                        break;
                    case 'E':
                        bytesTexto[i] = 191;
                        break;
                    case 'F':
                        bytesTexto[i] = 192;
                        break;
                    case 'G':
                        bytesTexto[i] = 193;
                        break;
                    case 'H':
                        bytesTexto[i] = 194;
                        break;
                    case 'I':
                        bytesTexto[i] = 195;
                        break;
                    case 'J':
                        bytesTexto[i] = 196;
                        break;
                    case 'K':
                        bytesTexto[i] = 197;
                        break;
                    case 'L':
                        bytesTexto[i] = 198;
                        break;
                    case 'M':
                        bytesTexto[i] = 199;
                        break;
                    case 'N':
                        bytesTexto[i] = 200;
                        break;
                    case 'O':
                        bytesTexto[i] = 201;
                        break;
                    case 'P':
                        bytesTexto[i] = 202;
                        break;
                    case 'Q':
                        bytesTexto[i] = 203;
                        break;
                    case 'R':
                        bytesTexto[i] = 204;
                        break;
                    case 'S':
                        bytesTexto[i] = 205;
                        break;
                    case 'T':
                        bytesTexto[i] = 206;
                        break;
                    case 'U':
                        bytesTexto[i] = 207;
                        break;
                    case 'V':
                        bytesTexto[i] = 208;
                        break;
                    case 'W':
                        bytesTexto[i] = 209;
                        break;
                    case 'X':
                        bytesTexto[i] = 210;
                        break;
                    case 'Y':
                        bytesTexto[i] = 211;
                        break;
                    case 'Z':
                        bytesTexto[i] = 212;
                        break;
                    case 'a':
                        bytesTexto[i] = 213;
                        break;
                    case 'b':
                        bytesTexto[i] = 214;
                        break;
                    case 'c':
                        bytesTexto[i] = 215;
                        break;
                    case 'd':
                        bytesTexto[i] = 216;
                        break;
                    case 'e':
                        bytesTexto[i] = 217;
                        break;
                    case 'f':
                        bytesTexto[i] = 218;
                        break;
                    case 'g':
                        bytesTexto[i] = 219;
                        break;
                    case 'h':
                        bytesTexto[i] = 220;
                        break;
                    case 'i':
                        bytesTexto[i] = 221;
                        break;
                    case 'j':
                        bytesTexto[i] = 222;
                        break;
                    case 'k':
                        bytesTexto[i] = 223;
                        break;
                    case 'l':
                        bytesTexto[i] = 224;
                        break;
                    case 'm':
                        bytesTexto[i] = 225;
                        break;
                    case 'n':
                        bytesTexto[i] = 226;
                        break;
                    case 'o':
                        bytesTexto[i] = 227;
                        break;
                    case 'p':
                        bytesTexto[i] = 228;
                        break;
                    case 'q':
                        bytesTexto[i] = 229;
                        break;
                    case 'r':
                        bytesTexto[i] = 230;
                        break;
                    case 's':
                        bytesTexto[i] = 231;
                        break;
                    case 't':
                        bytesTexto[i] = 232;
                        break;
                    case 'u':
                        bytesTexto[i] = 233;
                        break;
                    case 'v':
                        bytesTexto[i] = 234;
                        break;
                    case 'w':
                        bytesTexto[i] = 235;
                        break;
                    case 'x':
                        bytesTexto[i] = 236;
                        break;
                    case 'y':
                        bytesTexto[i] = 237;
                        break;
                    case 'z':
                        bytesTexto[i] =238 ;
                        break;
                    case (char)CaracteresEspeciales.MasGrande:
                        bytesTexto[i] = 239;
                        break;
                    case ':':
                        bytesTexto[i] = 240;
                        break;
                    case 'Ä':
                        bytesTexto[i] = 241;
                        break;
                    case 'Ö':
                        bytesTexto[i] = 242;
                        break;
                    case 'Ü':
                        bytesTexto[i] = 243;
                        break;
                    case 'ä':
                        bytesTexto[i] = 244;
                        break;
                    case 'ö':
                        bytesTexto[i] = 245;
                        break;
                    case 'ü':
                        bytesTexto[i] = 246;
                        break;
                    case (char)CaracteresEspeciales.u:      
                        bytesTexto[i] = 247;
                        break;
                    case (char)CaracteresEspeciales.d:
                        bytesTexto[i] = 248;
                        break;
                    case (char)CaracteresEspeciales.l:
                        bytesTexto[i] = 247;
                        break;
                    case (char)CaracteresEspeciales.EspL:
                        bytesTexto[i] = 250;
                        break;
                    case (char)CaracteresEspeciales.EspP:
                        bytesTexto[i] = 251;
                        break;
                    case (char)CaracteresEspeciales.EspC:
                        bytesTexto[i] = 252;
                        break;
                    case (char)CaracteresEspeciales.EspV:
                        bytesTexto[i] = 253;
                        break;
                    case (char)CaracteresEspeciales.EspN:
                        bytesTexto[i] = 254;
                        break;
                    case (char)CaracteresEspeciales.Vacio:
                        bytesTexto[i] = 255;
                        break;
                    default:
                        bytesTexto[i] = 0;
                        break;
                        #endregion
                }

            }
            return bytesTexto;
        }

        private static string ParseaTexto(string texto)
        {
            if (texto == null) texto = "";
            StringBuilder textoParseado = new StringBuilder(texto);
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
            return textoParseado.ToString();
        }
        #endregion
	}
}
