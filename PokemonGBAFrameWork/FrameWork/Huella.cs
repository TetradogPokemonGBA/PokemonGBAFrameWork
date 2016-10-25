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
                imgHuella = Load2BPSprite16By16(bloqueImgHuella.Bytes);
            }
        }

        public static Huella GetHuella(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion,Hex posicion)
        {
            Hex offsetInicio =Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.ImgHuella, edicion, compilacion)+(posicion-1)*(int)Longitud.Offset);
            return new Huella(BloqueBytes.GetBytes(rom, offsetInicio, "FF"));
        }
        //sacado de Pokemon Game Editor gamer2020
         static Bitmap Load2BPSprite16By16( byte[] Bits)
        {//no funciona...

            Bitmap bmpTiles = new Bitmap(16, 16);

            int sideways = 0;
            int updown = 0;
            int bittrack = 0;
            int bytetrack = 0;
            bool curbit;
            bool[] bitsarray;
            int CurSquare = 0;

            while (updown < 16)
            {

                while (sideways < 8)
                {
                    bitsarray =  Bits[bittrack].ToBits();

                    curbit = bitsarray[bittrack];


                    if (!curbit)
                    {

                        if (CurSquare == 0)
                        {
                            bmpTiles.SetPixel(sideways, updown, PaletaHuella[0]);

                        }


                        else if (CurSquare == 1)
                        {
                            bmpTiles.SetPixel((CurSquare * 8) + sideways, updown - (CurSquare * 8), PaletaHuella[0]);

                        }


                        else if (CurSquare == 2)
                        {
                            bmpTiles.SetPixel(sideways, updown + (8), PaletaHuella[0]);

                        }


                        else if (CurSquare == 3)
                        {
                            bmpTiles.SetPixel((8) + sideways, updown, PaletaHuella[0]);

                        }


                    }
                    else
                    {

                        if (CurSquare == 0)
                        {
                            bmpTiles.SetPixel(sideways, updown, PaletaHuella[1]);

                        }


                       else if (CurSquare == 1)
                        {
                            bmpTiles.SetPixel((CurSquare * 8) + sideways, updown - (CurSquare * 8), PaletaHuella[1]);

                        }


                        else if (CurSquare == 2)
                        {
                            bmpTiles.SetPixel(sideways, updown + (8), PaletaHuella[1]);

                        }


                        else if (CurSquare == 3)
                        {
                            bmpTiles.SetPixel((8) + sideways, updown, PaletaHuella[1]);

                        }

                    }
                    bittrack = bittrack + 1;
                    if (bittrack == 8)
                    {
                        bittrack = 0;
                        bytetrack = bytetrack + 1;
                    }

                    sideways = sideways + 1;
                }
                sideways = 0;

                if (updown == 7 & CurSquare == 0)
                {
                    CurSquare = CurSquare + 1;
                }

                if (updown == 15 & CurSquare == 1)
                {
                    CurSquare = CurSquare + 1;
                    updown = -1;
                }

                if (updown == 7 & CurSquare == 2)
                {
                    CurSquare = CurSquare + 1;

                }

                if (updown == 15 & CurSquare == 3)
                {
                    CurSquare = CurSquare + 1;
                }

                updown = updown + 1;

            }
            return bmpTiles;
        }
        public static void SetHuella(RomGBA rom, Huella huella,Hex posicion)
        {
          //  throw new NotImplementedException();
        }
    }
}
