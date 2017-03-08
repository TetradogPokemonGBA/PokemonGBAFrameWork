using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
   public class MOSinMedallas
    {
        enum Variable
        {
            MOSinMedallasOffset
        }
        const byte DISSABLEMOSINMEDALLAS = 0x1;
        const byte ENABLEMOSINMEDALLAS = 0x0;
        static MOSinMedallas()
        {
            Zona zonaMOSinMedallas = new Zona(Variable.MOSinMedallasOffset);
            //pongo las zonas :D
            zonaMOSinMedallas.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x12462E,0x146A6);
            zonaMOSinMedallas.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x124606, 0x12467E);
            zonaMOSinMedallas.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x124782);
            zonaMOSinMedallas.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1247AA);

            Zona.DiccionarioOffsetsZonas.Add(zonaMOSinMedallas);
        }

        public static bool EstaActivado(RomData rom)
        {
            return EstaActivado(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }

        public static bool EstaActivado(RomGBA romGBA, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            return romGBA.Datos[Zona.GetVariable(romGBA, Variable.MOSinMedallasOffset, edicion, compilacion)]==ENABLEMOSINMEDALLAS;
        }
        public static void Activar(RomData rom)
        {
             Activar(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }

        public static void Activar(RomGBA romGBA, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
             romGBA.Datos[Zona.GetVariable(romGBA, Variable.MOSinMedallasOffset, edicion, compilacion)] = ENABLEMOSINMEDALLAS;
        }
        public static void Desactivar(RomData rom)
        {
            Desactivar(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }

        public static void Desactivar(RomGBA romGBA, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            romGBA.Datos[Zona.GetVariable(romGBA, Variable.MOSinMedallasOffset, edicion, compilacion)] = DISSABLEMOSINMEDALLAS;
        }
    }
}
