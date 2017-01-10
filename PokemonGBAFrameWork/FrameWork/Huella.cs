using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
   public class Huella
    {
        enum Variables { ImgHuella}
        static readonly Color[] PaletaHuella=new Color[] {Color.Transparent,Color.Black };
        static Huella()
        {
            Zona zonaHuellas = new Zona(Variables.ImgHuella);

            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x105E14,0x105E8C);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x105DEC, 0x105E64);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xC0DBC);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x917C8,0x917E8, 0x917E8);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x917C8, 0x917E8, 0x917E8);

            zonaHuellas.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x105F8C);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x105FB4);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xC0B80);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x919F8);
            zonaHuellas.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x919F8);

            Zona.DiccionarioOffsetsZonas.Add(zonaHuellas);


        }
        BloqueBytes bloqueImgHuella;
        Bitmap imgHuella;

        public Huella(Hex offsetInicio,byte[] datosImagen):this(new BloqueBytes(offsetInicio, datosImagen))
        {
          
        }

        public Huella(BloqueBytes bloqueBytes)
        {
            BloqueImgHuella = bloqueBytes;
        }

        public Bitmap Imagen {
            get {
                return imgHuella; }
        }

        public BloqueBytes BloqueImgHuella
        {
            get
            {
                return bloqueImgHuella;
            }

            set
            {
                bloqueImgHuella = value;
                imgHuella = ReadImage(bloqueImgHuella.Bytes);
            }
        }

        public static Huella GetHuella(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex posicion)
        {
            Hex offsetInicio =Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion)+(posicion-1)*(int)Longitud.Offset);
            return new Huella(BloqueBytes.GetBytes(rom, offsetInicio, "FF"));
        }
        static Bitmap ReadImage(byte[] bytesHuella)
        {//optimizar mas adelante :D
            Bitmap bmpHuella = new Bitmap(16, 16);
            int mitadY = bmpHuella.Height / 2;
            int inicioY = 0;
            int finY = mitadY;
            int pos = 0;
            //tengo 0xFF bytes para formar la imagen
            bool[] bits;
            for (int i = 0; i < 2; i++)
            {
              //  primero arriba y luego abajo
                for (int y = inicioY; y < finY; y++)
                {
                    bits = bytesHuella[pos].ToBits();//hace la parte izquierda 
                    for (int x = 0; x < mitadY; x++)
                        if (bits[x])
                            bmpHuella.SetPixel(x, y, Color.Black);
                    bits = bytesHuella[pos + mitadY].ToBits();//hace la parte derecha
                    for (int x = mitadY, xAux = 0; x < bmpHuella.Width; x++, xAux++)
                        if (bits[xAux])
                            bmpHuella.SetPixel(x, y, Color.Black);
                    pos++;
                }
                //13
                //24
                //un byte arriba otro abajo
                pos += mitadY;
                inicioY += mitadY;
                finY += mitadY;
            }
           
            return bmpHuella;
        }
        public static void SetHuella(RomGBA rom, Huella huella,Hex posicion)
        {
          //  throw new NotImplementedException();
        }
    }
}
