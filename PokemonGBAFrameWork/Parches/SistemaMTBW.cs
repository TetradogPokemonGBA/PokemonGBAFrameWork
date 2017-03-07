using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//sacado de aqui http://wahackforo.com/t-25561/fr-en-proceso-sistema-mt-bw
namespace PokemonGBAFrameWork
{
    public static class SistemaMTBW
    {
        //se tienen que editar 2 bytes de dos sitios diferentes cambiar A9 por 90  y al lado tiene que haber 20; si cancelas el aprendizaje pierdes la MT si no no...tiene que acabarse la investigacion hecha por el autor...
        enum Variable
        {
            OffsetMTBW1,OffsetMTBW2
        }
        static readonly byte[] NewSystem;//de momento son estos bytes...si cambian lo haré generico...
        static readonly byte[] OldSystem;
        static SistemaMTBW()
        {
            Zona zonaOffset1 = new Zona(Variable.OffsetMTBW1);
            Zona zonaOffset2 = new Zona(Variable.OffsetMTBW2);

            NewSystem = new byte[] { 0x90, 0x20 };
            OldSystem = new byte[] { 0xA9, 0x20 };

            zonaOffset1.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x124E78, 0x124EF0);
            zonaOffset1.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x124EA0, 0x124F18);
            zonaOffset1.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x124FF4);
            zonaOffset1.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x12501C);


            zonaOffset2.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa,0x124F44 ,0x124FBC);
            zonaOffset2.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x124F6C, 0x124FE4);
            zonaOffset2.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1250C0);
            zonaOffset2.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1250E8);


            Zona.DiccionarioOffsetsZonas.Add(zonaOffset1);
            Zona.DiccionarioOffsetsZonas.Add(zonaOffset2);

        }

        public static bool EstaActivadoElNuevoSistema(RomData rom)
        {
            return EstaActivadoElNuevoSistema(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }
        public static bool EstaActivadoElNuevoSistema(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            return rom.Datos[GetOffset(rom,edicion,compilacion, Variable.OffsetMTBW1)] == NewSystem[0] && rom.Datos[GetOffset(rom, edicion, compilacion, Variable.OffsetMTBW2)] == NewSystem[0];
        }


        public static void ActivarNuevoSistema(RomData rom)
        {
            ActivarNuevoSistema(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }

        public static void ActivarNuevoSistema(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            rom.Datos.SetArray(GetOffset(rom, edicion, compilacion, Variable.OffsetMTBW1), NewSystem);
            rom.Datos.SetArray(GetOffset(rom, edicion, compilacion, Variable.OffsetMTBW2), NewSystem);
        }
        public static void DesactivarNuevoSistema(RomData rom)
        {
             DesactivarNuevoSistema(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }
        public static void DesactivarNuevoSistema(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            rom.Datos.SetArray(GetOffset(rom, edicion, compilacion, Variable.OffsetMTBW1), OldSystem);
            rom.Datos.SetArray(GetOffset(rom, edicion, compilacion, Variable.OffsetMTBW2), OldSystem);
        }

        private static Hex GetOffset(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Variable offsetMTBW)
        {
            return Zona.GetVariable(rom, offsetMTBW,edicion, compilacion);
        }
    }
}
