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
            Descripcion=4,//es un pointer al texto
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
            //descripcion con ellas calculo el total :D
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0xE5440, 0xE5454);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0xE5418, 0xE542C);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xA0494,0xA04B4);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0xA0494, 0xA04B4);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1C3EFC);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1C3B1C);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0xA06C8);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0xA06C8);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0xE574C);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0XE5724);
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

        public BloqueString Descripcion
        {
            get
            {
                return descripcion;
            }

           private set
            {
                descripcion = value;
            }
        }
        public override string ToString()
        {
            return Nombre+"\n"+Descripcion;
        }

        public static int GetTotalAtaques(RomData rom)
        {
            return GetTotalAtaques(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }
        public static int GetTotalAtaques(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            Hex offsetDescripciones = Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion);
            int total = 1;//el primero lo salto porque no tiene descripcion :)
            while(Offset.IsAPointer(rom,offsetDescripciones))
            {
                offsetDescripciones += (int)Longitud.Offset;//avanzo hasta la proxima descripcion :)
                total++;
            }
            return total;
        }
        public static Ataque[] GetAtaques(RomData romData)
        {
            return GetAtaques(romData.RomGBA, romData.Edicion, romData.Compilacion);
        }
        public static Ataque[] GetAtaques(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion)
        {
            Ataque[] ataques = new Ataque[GetTotalAtaques(rom,edicion,compilacion)];
            for (int i = 0; i < ataques.Length; i++)
                ataques[i] = GetAtaque(rom, edicion, compilacion,i);
            return ataques;
        }
        public static Ataque GetAtaque(RomData rom, Hex posicion)
        {
            return GetAtaque(rom.RomGBA, rom.Edicion, rom.Compilacion, posicion);
        }
        public static Ataque GetAtaque(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            BloqueString nombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre, (int)LongitudCampos.Nombre,true);
            //la descripcion del primer ataque no existe y todas las descripciones se retrasan 1
            BloqueString descripcion = BloqueString.GetString(rom,Offset.GetOffset( rom,Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion) + (posicion==0?posicion:posicion-1) * (int)LongitudCampos.Descripcion));
            return new Ataque() { Nombre = nombre,Descripcion=descripcion };
        }
        public static void SetAtaque(RomData rom, Hex posicion, Ataque ataque)
        {
            SetAtaque(rom.RomGBA, rom.Edicion, rom.Compilacion, posicion, ataque);
        }
        public static void SetAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion,Ataque ataque)
        {
            Hex offsetNombre = Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre;
            Hex offsetDescripcion = Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion) + posicion * (int)LongitudCampos.Descripcion);
            //nombre
            BloqueBytes.RemoveBytes(rom, offsetNombre, (int)LongitudCampos.Nombre);
            BloqueString.SetString(rom, offsetNombre,ataque.Nombre);
            //descripcion
            BloqueBytes.RemoveBytes(rom, offsetDescripcion, ataque.descripcion.LengthInnerRom);
            BloqueString.SetString(rom, offsetDescripcion, ataque.Descripcion);
           
        }
        public static void SetAtaques(RomData romData, IList<Ataque> ataques)
        {
            SetAtaques(romData.RomGBA, romData.Edicion, romData.Compilacion,ataques);
        }
        public static void SetAtaques(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, IList<Ataque> ataques)
        {
            for(int i=0;i<ataques.Count;i++)
            {
                SetAtaque(rom, edicion, compilacion, i, ataques[i]);
            }
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
