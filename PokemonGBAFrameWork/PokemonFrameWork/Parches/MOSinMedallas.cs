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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//falta hacerlo universal
namespace PokemonGBAFrameWork
{//creditos a CryStal Kaktus de Wahack por el aporte
    public static class MOSinMedallas
    {
        
        const byte DISSABLEMOSINMEDALLAS = 0x1;
        const byte ENABLEMOSINMEDALLAS = 0x0;
        public static readonly Variable VariableMOSinMedallas;
        static MOSinMedallas()
        {
            VariableMOSinMedallas = new Variable("MOSinMedallasOffset");
            //pongo las zonas :D
            VariableMOSinMedallas.Add(EdicionPokemon.RojoFuegoUsa, 0x12462E,0x146A6);
            VariableMOSinMedallas.Add(EdicionPokemon.RojoFuegoUsa, 0x124606, 0x12467E);
            VariableMOSinMedallas.Add(EdicionPokemon.VerdeHojaEsp, 0x124782);
            VariableMOSinMedallas.Add(EdicionPokemon.RojoFuegoEsp, 0x1247AA);


        }

        public static bool EstaActivado(RomData rom)
        {
            return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static bool EstaActivado(RomGba romGBA, EdicionPokemon edicion,Compilacion compilacion)
        {
            return romGBA.Data[Variable.GetVariable( VariableMOSinMedallas, edicion, compilacion)]==ENABLEMOSINMEDALLAS;
        }
        public static void Activar(RomData rom)
        {
             Activar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
             romGBA.Data[Variable.GetVariable( VariableMOSinMedallas, edicion, compilacion)] = ENABLEMOSINMEDALLAS;
        }
        public static void Desactivar(RomData rom)
        {
            Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
        }

        public static void Desactivar(RomGba romGBA, EdicionPokemon edicion, Compilacion compilacion)
        {
            romGBA.Data[Variable.GetVariable( VariableMOSinMedallas, edicion, compilacion)] = DISSABLEMOSINMEDALLAS;
        }
    }
}
