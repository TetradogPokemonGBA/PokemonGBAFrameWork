using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    public class Cry
    {
        enum VariableCry
        {
            Cry
        }
        enum Longitud
        {
            Header=12,
        }
        enum Posicion
        {
            OffsetData=4//quizas es 8
        }
        static Cry()
        {
            Zona zonaTablaCry = new Zona(VariableCry.Cry);
            //pongo las zonas :D
            //encontrarlas es mas dificil que de costumbre...
            Zona.DiccionarioOffsetsZonas.Add(zonaTablaCry);
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
