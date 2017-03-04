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
        public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479
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
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xA0494,0xA04B4);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xA0494, 0xA04B4);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1C3EFC);
            //script batalla
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x162D4);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x16364, 0x16378);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x162D4);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x16364, 0x16378);
            //animacion CON ESTO PUEDO DIFERENCIAR LAS VERSIONES ZAFIRO Y RUBI Y SUS COMPILACIONES :D
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x72608);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x7250D0, 0x725E4);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x72608);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x7250D0, 0x725E4);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x75734, 0x75754);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x75738, 0x75758);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0xA3A44);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xA3A58);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x75BF0);
            zonaAnimacion.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x75BF4);
        }

        BloqueString nombre;
        BloqueString descripcion;

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
            BloqueString nombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre, (int)LongitudCampos.Nombre,true);
            return new Ataque() { Nombre = nombre };
        }
        public static void SetAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion,Ataque ataque)
        {
            Hex offsetNombre = Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre;
            BloqueBytes.RemoveBytes(rom, offsetNombre, (int)LongitudCampos.Nombre);
            BloqueString.SetString(rom, offsetNombre,ataque.Nombre);
           
        }
        /// <summary>
        /// Sirve para encontrar la edicion facilmente :D
        /// </summary>
        /// <param name="rom"></param>
        /// <returns></returns>
        internal static Edicion GetEdicion(RomGBA rom)
        {
            Edicion edicion=Edicion.ZafiroEsp;
            if(!Offset.IsAPointer( Zona.GetOffset(rom,Variables.Animacion,edicion)))
            {
                edicion = Edicion.RubiEsp;
                if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion)))
                {
                    edicion = Edicion.RubiUsa;
                    if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion,CompilacionRom.Compilacion.Primera)))
                    {
                        if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion,CompilacionRom.Compilacion.Segunda)))
                        {
                            edicion = Edicion.ZafiroUsa;
                            if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion, CompilacionRom.Compilacion.Primera)))
                            {
                                if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion, CompilacionRom.Compilacion.Segunda)))
                                {
                                    throw new Exception("Solo se puede obtener con este metodo las ediciones Rubi y zafiro ESP y USA");
                                }
                            }
                        }
                    }
                }
            }
            return edicion;
        }
    }
}
