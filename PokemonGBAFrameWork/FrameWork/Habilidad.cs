using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//créditos Wahackforo (los usuarios que han contribuido para hacer el mapa de la rom pokemon fire red 1.0)
namespace PokemonGBAFrameWork
{
   
   public class Habilidad
    {
        public enum Variables { NombreHabilidad}
        enum LongitudCampo { Nombre=13}
        static Habilidad()
        {
            Zona zonaNombre = new Zona(Variables.NombreHabilidad);
            //añado las zonas de los nombres :)
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x9FE64,0x9FE84, 0x9FE84);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x9FE64, 0x9FE84, 0x9FE84);

            zonaNombre.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xA0098);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xA0098);

            Zona.DiccionarioOffsetsZonas.Añadir(zonaNombre);

        }
        BloqueString nombre;
        public Habilidad(BloqueString nombre)
        {
            if (nombre == null) throw new ArgumentNullException();
            this.nombre = nombre;
        }

        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }

          private  set
            {
                nombre = value;
            }
        }
        public override string ToString()
        {
            return Nombre;
        }
        public static Habilidad GetHabilidad(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            if (rom == null || edicion == null || posicion < 0) throw new ArgumentException();
            BloqueString blNombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreHabilidad, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre);
            return new Habilidad(blNombre);
        }
        public static Hex GetTotal(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            //de momento no se...mas adelante
            return 78;
        }
        public static Habilidad[] GetHabilidades(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            if (rom == null || edicion == null ) throw new ArgumentException();
            Habilidad[] habilidades = new Habilidad[GetTotal(rom, edicion, compilacion)];
            for (int i = 0; i < habilidades.Length; i++)
                habilidades[i] = GetHabilidad(rom, edicion, compilacion, i);
            return habilidades;
        }
        public static void SetHabilidad(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Habilidad habilidad, Hex posicion)
        {
            if (rom == null || edicion == null || habilidad == null || habilidad.Nombre.Texto.Length > (int)LongitudCampo.Nombre || posicion < 0) throw new ArgumentException();
            Hex offset = Zona.GetOffset(rom, Variables.NombreHabilidad, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre;
            BloqueString.SetString(rom, offset, habilidad.Nombre);

        }
    }
}
