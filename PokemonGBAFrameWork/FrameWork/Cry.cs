using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    public class Cry:ObjectAutoId
    {
        enum VariableCry
        {
            Cry1,Cry2,Desconocida
        }
        enum Longitud
        {
            Header=12,//4bytes,Pointer,4bytes //20 3C 00 00,Pointer,FF 00 FF 00 //30 3C 00 00,Pointer,FF 00 FF 00 
        }
        enum Posicion
        {
            PoinerData=4
        }
        static Cry()
        {
            Zona zonaTablaHeaderCry = new Zona(VariableCry.Cry1);
            Zona zonaTablaHeaderCry2 = new Zona(VariableCry.Cry2);
            Zona zonaDesconocida = new Zona(VariableCry.Desconocida);
            //pongo las zonas :D
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x72114, 0X72128);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x72114, 0X72128);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xA35EC);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xBAC18, 0xBAC38);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xBAC18,0x452608 ,0xBAC38);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x7214C);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xA3600);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x7214C);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xBAEA0);
            zonaTablaHeaderCry.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xBAEA0);
            
            //pongo las zonas :D NO SE PORQUE PERO APUNTAN LOS PUNTEROS A LOS MISMOS SITIOS QUE CRY1...SERAN LO MISMO? o quizas solo algunos pokemon tienen diferente...
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x72104, 0x72118);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x72104, 0x72118);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xA35DC);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xBAC08, 0xBAC28);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xBAC08,0xBAC28);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x7213C);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xA34F);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x7213C);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xBAE90);
            zonaTablaHeaderCry2.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xBAE90);

            //pongo las zonas :D Desconozco lo que puede haber quizas me equivoco de sitio
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x6BC674, 0x6BC6D4);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x6BBF50, 0x6BBFC0);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x903190);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x65D10C, 0x65D120);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x65D168, 0x65D180);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x6B2668);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x905744);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x6B2F70);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x6622E0);
            zonaDesconocida.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x6625A4);

            Zona.DiccionarioOffsetsZonas.Add(zonaTablaHeaderCry);
            Zona.DiccionarioOffsetsZonas.Add(zonaTablaHeaderCry2);
            Zona.DiccionarioOffsetsZonas.Add(zonaDesconocida);
        }

        /*public Cry(BloqueSonido blCry)
        {
            Sound = blCry;
        }
        public BloqueSonido Sound { get; set; }

        public static Cry GetCry(RomData rom,Hex posicion)
        {
            
            return new Cry(BloqueSonido.GetBloqueSonido(rom, Offset.GetOffset(rom.RomGBA, Zona.GetOffset(rom.RomGBA, VariableCry.Cry, rom.Edicion, rom.Compilacion) + posicion * (int)Longitud.Header)));
        } 
        public static int GetTotalCry(RomData rom)
        {//creo que se puede saber :)sin tenerlo que calcular de esta forma :)
            int total = 0;
            Hex offsetTabla= Zona.GetOffset(rom.RomGBA, VariableCry.Cry, rom.Edicion, rom.Compilacion);
            Hex offsetNextCry = offsetTabla + (int)Posicion.OffsetData;
            while (Offset.GetOffset(rom.RomGBA,offsetNextCry)>0)//hay mas datos por analizar si fuese necesario :) pero se necesita saber interpretarlos...
            {
                offsetNextCry+= (int)Posicion.OffsetData;
                total++;

            }
            return total;
        }
        public static Cry[] GetCrys(RomData rom)
        {
            Cry[] crys = new Cry[GetTotalCry(rom)];
            for (int i = 0; i < crys.Length; i++)
                crys[i] = GetCry(rom, i);
            return crys;
        }*/
    }
}
