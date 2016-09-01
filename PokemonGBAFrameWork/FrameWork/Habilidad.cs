using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
   // Puntero a los nombres de las habilidades - 0x1c0 fire red 1.0
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
            zonaNombre.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x1C0);
            zonaNombre.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x1C0);

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
            const char MARCAFI = (char)255;
            BloqueString blNombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreHabilidad, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre, (int)LongitudCampo.Nombre);
            if (blNombre.Texto.Contains(MARCAFI + ""))
                blNombre.Texto = blNombre.Texto.Substring(0, blNombre.Texto.IndexOf(MARCAFI));
            return new Habilidad(blNombre);
        }
        public static Hex GetTotal(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            //de momento no se...mas adelante
            return 78;
        }
        public static Habilidad[] GetHabilidades(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Habilidad[] habilidades = new Habilidad[GetTotal(rom, edicion, compilacion)];
            for (int i = 0; i < habilidades.Length; i++)
                habilidades[i] = GetHabilidad(rom, edicion, compilacion, i);
            return habilidades;
        }
        public static void SetHabilidad(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Habilidad habilidad, Hex posicion)
        {
            const char MARCAFI = (char)255;
            Hex offset = Zona.GetOffset(rom, Variables.NombreHabilidad, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre;
            if (habilidad.Nombre.Texto.Length > (int)LongitudCampo.Nombre)
                habilidad.Nombre.Texto = habilidad.Nombre.Texto.Substring(0, (int)LongitudCampo.Nombre);
            BloqueString.SetString(rom, offset, habilidad.Nombre + MARCAFI);

        }
    }
}
