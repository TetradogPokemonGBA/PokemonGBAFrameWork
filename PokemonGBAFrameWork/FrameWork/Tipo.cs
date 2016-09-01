using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    
  public  class Tipo
    {
        public enum Variables
        { NombreTipo}
        public enum LongitudCampo
        { Nombre=7 }
        static Tipo()
        {
            Zona zonaNombreTipo = new Zona(Variables.NombreTipo);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x2E574);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x2E574);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x309C8);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x309C8);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x166F4);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x166F4);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x308B4);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x308B4);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x309C8, 0x309DC);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x309C8, 0x309DC);
            Zona.DiccionarioOffsetsZonas.Añadir(zonaNombreTipo);
        }
        BloqueString nombre;


        public Tipo(BloqueString nombre)
        {
            this.nombre = nombre;
            Nombre.MaxCaracteres = (int)LongitudCampo.Nombre;
        }

        public BloqueString Nombre
        {
            get
            {
                return nombre;
            }

            private set
            {
                nombre = value;
            }
        }
        public override string ToString()
        {
            return Nombre;
        }
        public static Tipo GetTipo(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            const char MARCAFI = (char)255;
            BloqueString blNombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreTipo, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre, (int)LongitudCampo.Nombre);
            if (blNombre.Texto.Contains(MARCAFI + ""))
                blNombre.Texto = blNombre.Texto.Substring(0, blNombre.Texto.IndexOf(MARCAFI));
            return new Tipo(blNombre);
        }
        public static Hex GetTotal(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            //de momento no se...mas adelante
            return 18;
        }
        public static Tipo[] GetTipos(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Tipo[] tipos = new Tipo[GetTotal(rom, edicion, compilacion)];
            for (int i = 0; i < tipos.Length;i++)
                tipos[i] = GetTipo(rom, edicion, compilacion, i);
            return tipos;
        }
        public static void SetTipo(RomPokemon rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Tipo tipo, Hex posicion)
        {
            const char MARCAFI = (char)255;
            Hex offset = Zona.GetOffset(rom, Variables.NombreTipo, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre;
            if (tipo.Nombre.Texto.Length > (int)LongitudCampo.Nombre)
                tipo.Nombre.Texto = tipo.Nombre.Texto.Substring(0, (int)LongitudCampo.Nombre);
            BloqueString.SetString(rom, offset, tipo.Nombre + MARCAFI);

        }

    }
}
