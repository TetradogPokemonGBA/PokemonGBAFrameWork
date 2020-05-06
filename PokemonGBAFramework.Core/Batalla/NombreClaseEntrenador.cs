using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class NombreClaseEntrenador
    {
        public const int LENGTH = 0xD;


        public static readonly byte[] MuestraAlgoritmoKanto = {0x10, 0x49, 0x20, 0x1C, 0x21, 0xF0};
        public static readonly int IndexRelativoKanto = 48 - MuestraAlgoritmoKanto.Length;

        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x09, 0x0F, 0x28, 0x1C, 0xF0 };
        public static readonly int IndexRelativoEsmeralda = - MuestraAlgoritmoEsmeralda.Length-96;

        //al parecer son diferentes...
        public static readonly byte[] MuestraAlgoritmoRubiYZafiroEUR= { 0x70, 0xB5, 0x02, 0x1C, 0x0E };
        public static readonly int IndexRelativoRubyYZafiroEUR = -1 - 80;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiroUSA = { 0x09, 0x49, 0x58, 0x18, 0x04, 0x70 };
        public static readonly int IndexRelativoRubyYZafiroUSA = -MuestraAlgoritmoRubiYZafiroUSA.Length-64;

        public NombreClaseEntrenador()
        {
            Text = new BloqueString(LENGTH);
        }
        public BloqueString Text { get; set; }
        public override string ToString()
        {
            return Text.ToString();
        }
        public static NombreClaseEntrenador[] Get(RomGba rom,OffsetRom offsetNombreClaseEntrenador=default,OffsetRom offsetSpriteClaseEntrenador=default,OffsetRom offsetPaletaSpriteClaseEntrenador=default,int totalClaseEntrenador=-1)
        {
            NombreClaseEntrenador[] nombres = new NombreClaseEntrenador[totalClaseEntrenador<0?SpriteClaseEntrenador.GetTotal(rom,offsetSpriteClaseEntrenador,offsetPaletaSpriteClaseEntrenador):totalClaseEntrenador];
            offsetNombreClaseEntrenador = Equals(offsetNombreClaseEntrenador, default) ? GetOffset(rom) : offsetNombreClaseEntrenador;
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = Get(rom, i,offsetNombreClaseEntrenador);
            return nombres;
        }
        public static NombreClaseEntrenador Get(RomGba rom, int index,OffsetRom offsetNombreClaseEntrenador=default)
        {


            int offsetNombre = (Equals(offsetNombreClaseEntrenador,default)?GetOffset(rom):offsetNombreClaseEntrenador) + (index) * LENGTH;
            NombreClaseEntrenador nombre = new NombreClaseEntrenador();
            nombre.Text = BloqueString.Get(rom, offsetNombre);

            return nombre;
        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int index;

            if (rom.Edicion.EsKanto)
            {
                algoritmo = MuestraAlgoritmoKanto;
                index = IndexRelativoKanto;

            }
            else if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                index = IndexRelativoEsmeralda;
            }
            else
            {
                if (rom.Edicion.RegionVersion == Edicion.Region.Free) 
                { 
                     algoritmo = MuestraAlgoritmoRubiYZafiroEUR;
                     index = IndexRelativoRubyYZafiroEUR; 
                }
                else
                {
                    algoritmo = MuestraAlgoritmoRubiYZafiroUSA;
                    index = IndexRelativoRubyYZafiroUSA;
                }
            }


            return Zona.Search(rom, algoritmo, index);
        }
    }
}