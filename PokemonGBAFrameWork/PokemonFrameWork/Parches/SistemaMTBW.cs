/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
      public static readonly Variable VariableOffsetMTBW1;
      public static readonly Variable VariableOffsetMTBW2;
        static readonly byte[] NewSystem;//de momento son estos bytes...si cambian lo haré generico...
        static readonly byte[] OldSystem;
        static SistemaMTBW()
        {
             VariableOffsetMTBW1 = new Variable("OffsetMTBW1");
             VariableOffsetMTBW2 = new Variable("OffsetMTBW2");

            NewSystem = new byte[] { 0x90, 0x20 };
            OldSystem = new byte[] { 0xA9, 0x20 };

            VariableOffsetMTBW1.Add(EdicionPokemon.VerdeHojaUsa, 0x124E78, 0x124EF0);
            VariableOffsetMTBW1.Add(EdicionPokemon.RojoFuegoUsa, 0x124EA0, 0x124F18);
            VariableOffsetMTBW1.Add(EdicionPokemon.VerdeHojaEsp, 0x124FF4);
            VariableOffsetMTBW1.Add(EdicionPokemon.RojoFuegoEsp, 0x12501C);


            VariableOffsetMTBW2.Add(EdicionPokemon.VerdeHojaUsa,0x124F44 ,0x124FBC);
            VariableOffsetMTBW2.Add(EdicionPokemon.RojoFuegoUsa, 0x124F6C, 0x124FE4);
            VariableOffsetMTBW2.Add(EdicionPokemon.VerdeHojaEsp, 0x1250C0);
            VariableOffsetMTBW2.Add(EdicionPokemon.RojoFuegoEsp, 0x1250E8);
        }

        public static bool EstaActivado(RomData rom)
        {
            return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
        }
        public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
        {
            return rom.Data[GetOffset(edicion,compilacion, VariableOffsetMTBW1)] == NewSystem[0] && rom.Data[GetOffset( edicion, compilacion, VariableOffsetMTBW2)] == NewSystem[0];
        }


        public static void Activar(RomData rom)
        {
            Activar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGba rom, EdicionPokemon edicion,Compilacion compilacion)
        {
            rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW1), NewSystem);
            rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW2), NewSystem);
        }
        public static void Desactivar(RomData rom)
        {
             Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
        }
        public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
        {
            rom.Data.SetArray(GetOffset( edicion, compilacion,VariableOffsetMTBW1), OldSystem);
            rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW2), OldSystem);
        }

        private static int GetOffset(EdicionPokemon edicion, Compilacion compilacion, Variable offsetMTBW)
        {
            return Variable.GetVariable(offsetMTBW,  edicion, compilacion);
        }
    }
}
