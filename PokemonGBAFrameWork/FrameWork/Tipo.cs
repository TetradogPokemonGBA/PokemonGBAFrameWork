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
        { NombreTipo,ImagenTipo}
        public enum LongitudCampo
        { Nombre=7 }
        static Tipo()
        {
            Zona zonaNombreTipo = new Zona(Variables.NombreTipo);
            Zona zonaImagenTipo = new Zona(Variables.ImagenTipo);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x2E574);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x2E574);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x2E3A8);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x2E3A8);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x166F4);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x166F4);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x308B4);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x308B4);

            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x309C8, 0x309DC);
            zonaNombreTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x309C8, 0x309DC);

            //añado las imagenes son algo pero no se que son...
            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x107d88, 0x107e00);
            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x107db0, 0x107e28);
            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x19a340);

            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x107f30);
            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x107f58);
            zonaImagenTipo.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x199F44);
            Zona.DiccionarioOffsetsZonas.Add(zonaImagenTipo);
            Zona.DiccionarioOffsetsZonas.Add(zonaNombreTipo);
        }
        BloqueString nombre;


        public Tipo(BloqueString nombre)
        {
            if (nombre == null) throw new ArgumentNullException();
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
        public static Tipo GetTipo(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            if (rom == null || edicion == null ||  posicion < 0) throw new ArgumentException();
            BloqueString blNombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreTipo, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre);
            return new Tipo(blNombre);
        }
        public static Hex GetTotal(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            //de momento no se...mas adelante
            return 18;
        }
        public static Tipo[] GetTipos(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Tipo[] tipos = new Tipo[GetTotal(rom, edicion, compilacion)];
            for (int i = 0; i < tipos.Length;i++)
                tipos[i] = GetTipo(rom, edicion, compilacion, i);
            return tipos;
        }
        public static void SetTipo(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Tipo tipo, Hex posicion)
        {
            if (rom == null || edicion == null || tipo == null || tipo.Nombre.Texto.Length > (int)LongitudCampo.Nombre || posicion < 0) throw new ArgumentException();
            Hex offset = Zona.GetOffset(rom, Variables.NombreTipo, edicion, compilacion) + posicion * (int)LongitudCampo.Nombre;
           
            BloqueString.SetString(rom, offset, tipo.Nombre);

        }

        public static void SetTipos(RomGBA rom, IList<Tipo> tipos)
        {
            if (rom == null || tipos == null) throw new ArgumentNullException();
            Edicion edicion = Edicion.GetEdicion(rom);
            CompilacionRom.Compilacion compilacion = CompilacionRom.GetCompilacion(rom, edicion);
            if (tipos.Count != GetTotal(rom, edicion, compilacion))
            {
                BloqueBytes.RemoveBytes(rom, Zona.GetOffset(rom, Variables.NombreTipo, edicion, compilacion), GetTotal(rom, edicion, compilacion) * (int)LongitudCampo.Nombre);
                Zona.SetOffset(rom, Variables.NombreTipo, edicion, compilacion, BloqueBytes.SearchEmptyBytes(rom, tipos.Count * (int)LongitudCampo.Nombre));//actualizo el offset
            }
            for (int i = 0; i < tipos.Count; i++)
                SetTipo(rom, edicion, compilacion, tipos[i], i);
        }
    }
}
