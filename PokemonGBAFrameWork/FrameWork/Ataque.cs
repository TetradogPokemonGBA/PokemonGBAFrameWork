using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{//es muy extenso..por acabar de desarrollar (hacer clase para trabajar los efectos cómodamente y las demás partes que lo requieran
   public class Ataque
    {
        const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479
        enum LongitudCampos
        {
            Nombre=13,
            Datos=12,
            PointerEfecto=4,
            Efecto,//de momento no sé el máximo...
            Descripcion,//de momento no se cual es el maximo acaba en FE
            ScriptBatalla,
            Animacion
        }
        enum Variables
        {
            NombreAtaque,DatosAtaque,EfectoAtaque,Descripción,ScriptBatalla,Animacion
        }
        static Ataque()
        {
            Zona zonaNombresAtaques = new Zona(Variables.NombreAtaque);
            Zona zonaDatosAtaques = new Zona(Variables.DatosAtaque);
            Zona zonaEfectoAtaque = new Zona(Variables.EfectoAtaque);
            Zona zonaDescripcion = new Zona(Variables.Descripción);
            Zona zonaScriptBatalla = new Zona(Variables.ScriptBatalla);
            Zona zonaAnimacion = new Zona(Variables.Animacion);
            //añado las zonas al diccionario
            Zona.DiccionarioOffsetsZonas.Add(zonaAnimacion);
            Zona.DiccionarioOffsetsZonas.Add(zonaScriptBatalla);
            Zona.DiccionarioOffsetsZonas.Add(zonaDescripcion);
            Zona.DiccionarioOffsetsZonas.Add(zonaEfectoAtaque);
            Zona.DiccionarioOffsetsZonas.Add(zonaDatosAtaques);
            Zona.DiccionarioOffsetsZonas.Add(zonaNombresAtaques);
            //nombres
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x148);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x2e18c);
            zonaNombresAtaques.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x2e18c);
            //datos los pp es el offset de los datos+4 si se cambia el offset de los datos hay que cambiar el de los pps tambien!!!
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1CC);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xCC20);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xCC20);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xCA54);
            zonaDatosAtaques.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xCA54);
            //por investigar!!!
            //efectos el offset tiene que acabar en 0,4,8,C
            zonaEfectoAtaque.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x72608);
            zonaEfectoAtaque.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x725D0, 0x725E4);
            zonaEfectoAtaque.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x72608);
            zonaEfectoAtaque.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x725D0, 0x725E4);
            //descripcion
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0xe5440,0xe5454);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0xe5440, 0xe5454);
            //script batalla
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x162D4);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x16364, 0x16378);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x162D4);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x16364, 0x16378);
            //animacion
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x72608);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x7250D0, 0x725E4);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x72608);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x7250D0, 0x725E4);
        }

        BloqueString nombre;

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
        public static Ataque GetAtaque(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            BloqueString nombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre, (int)LongitudCampos.Nombre);
            return new Ataque() { Nombre = nombre };
        }
        public static void SetAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion,Ataque ataque)
        {
            Hex offsetNombre = Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre;
            BloqueBytes.RemoveBytes(rom, offsetNombre, (int)LongitudCampos.Nombre);
            BloqueString.SetString(rom, offsetNombre,ataque.Nombre);
           
        }
    }
}
