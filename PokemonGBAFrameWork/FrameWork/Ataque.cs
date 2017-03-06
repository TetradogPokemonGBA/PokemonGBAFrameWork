using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{//es muy extenso..por acabar de desarrollar (hacer clase para trabajar los efectos cómodamente y las demás partes que lo requieran
    public class DatosAtaque : ObjectAutoId
    {
       
        enum Custom
        {
            MakesContact = 1,
            Protect = 2,
            MagicCoat = 4,
            Snatch = 8,
            MirrorMove = 16,
            KingsRock = 32
        }
        enum CamposDatosAtaque
        {
         Effect,
         BasePower,
         Type,
         Accuracy,
         Pp,
         EffectAccuracy,
         Target,
         Priority,
         Custom,
         PadByte1,
         Category,
         PadByte3
    }
       public enum Categoria
        {
            Fisico,Especial,Estatus
        }
        enum Variable
        {
            DatosAtaque
        }
        public const int Longitud = 12;
        static DatosAtaque()
        {
            Zona zonaDatosAtaques = new Zona(Variable.DatosAtaque);
            Zona.DiccionarioOffsetsZonas.Add(zonaDatosAtaques);
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
        }
        BloqueBytes blDatosAtaque;
        /* custom
          el primero 1 Makes contact
          el segundo 2 Is affected by Protect
          el tercero 4 is affected by magic coat
          el cuarto 8 is affected by snatch
          el quinto 10 is affected by mirror move
          el sexto 20 is affected by king's rock
         */
        //los ultimos bytes puede ser la parte "Extra" de PGE -Attack Editor

        public DatosAtaque()
        {
            blDatosAtaque = new BloqueBytes(0,new byte[Longitud]);
        }
        public DatosAtaque(byte effect, byte basePower, byte type, byte accuracy, byte pp, byte effectAccuracy, byte target, byte priority,bool makeContact,bool isAffectedByProtect,bool isAffectedByMagicCoat,bool isAffectedBySnatch,bool isAffectedByMirrorMove,bool isAffectedByKingsRock, byte padByte1, Categoria category, byte padByte3):this()
        {
            Effect = effect;
            BasePower = basePower;
            Type = type;
            Accuracy = accuracy;
            Pp = pp;
            EffectAccuracy = effectAccuracy;
            Target = target;
            Priority = priority;
            MakesContact = makeContact;
            IsAffectedByProtect = isAffectedByProtect;
            IsAffectedBySnatch = isAffectedBySnatch;
            IsAffectedByMagicCoat = isAffectedByMagicCoat;
            IsAffectedByMirrorMove = isAffectedByMirrorMove;
            IsAffectedByKingsRock = isAffectedByKingsRock;
            PadByte1 = padByte1;
            Category = category;
            PadByte3 = padByte3;
        }

        public bool MakesContact
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] % 2 != 0;//si es impar es que hay el 1 de make contact
            }
            set
            {
                if (value)
                {
                    if (!MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]++;

                }
                else
                {
                    if (MakesContact)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom]--;
                }
            }
        }
        public bool IsAffectedByProtect
        {
            get
            {
                return IsCustomEnabled(1);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Protect;
                }
                else
                {
                    if (IsAffectedByProtect)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Protect;
                }

            }
        }
        public bool IsAffectedByMagicCoat
        {
            get
            {
                return IsCustomEnabled(2);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MagicCoat;
                }
                else
                {
                    if (IsAffectedByMagicCoat)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MagicCoat;
                }

            }
        }
        public bool IsAffectedBySnatch
        {
            get
            {
                return IsCustomEnabled(3);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.Snatch;
                }
                else
                {
                    if (IsAffectedBySnatch)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.Snatch;
                }

            }
        }
        public bool IsAffectedByMirrorMove
        {
            get
            {
                return IsCustomEnabled(4);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.MirrorMove;
                }
                else
                {
                    if (IsAffectedByMirrorMove)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.MirrorMove;
                }

            }
        }
        public bool IsAffectedByKingsRock
        {
            get
            {
                return IsCustomEnabled(5);
            }
            set
            {
                if (value)
                {
                    if (!IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] += (int)Custom.KingsRock;
                }
                else
                {
                    if (IsAffectedByKingsRock)
                        blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom] -= (int)Custom.KingsRock;
                }

            }
        }

        public byte Effect
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Effect] = value;
            }
        }

        public byte BasePower
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.BasePower] = value;
            }
        }

        public byte Type
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Type] = value;
            }
        }

        public byte Accuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Accuracy] = value;
            }
        }

        public byte Pp
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Pp];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Pp] = value;
            }
        }

        public byte EffectAccuracy
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.EffectAccuracy] = value;
            }
        }

        public byte Target
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Target] = value;
            }
        }

        public byte Priority
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Priority] = value;
            }
        }

    

        public byte PadByte1
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte1] = value;
            }
        }

        public Categoria Category
        {
            get
            {
                return (Categoria)blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.Category] = (byte)value;
            }
        }

        public byte PadByte3
        {
            get
            {
                return blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3];
            }

            set
            {
                blDatosAtaque.Bytes[(int)CamposDatosAtaque.PadByte3] = value;
            }
        }

        private bool IsCustomEnabled(int indexCustomToCalculate)
        {
            bool isCustomEnabled;
            int aux = blDatosAtaque.Bytes[(int)CamposDatosAtaque.Custom];
            Custom[] customs = Enum.GetValues(typeof(Custom)) as Custom[];
            for (int i = customs.Length - 1; i > indexCustomToCalculate; i--)
                if(aux>= (int)customs[i])
                   aux -= (int)customs[i];
            isCustomEnabled = aux >= (int)customs[indexCustomToCalculate];
            return isCustomEnabled;
        }

        public static DatosAtaque GetDatosAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            return new DatosAtaque() { blDatosAtaque = BloqueBytes.GetBytes(rom, Zona.GetOffset(rom, Variable.DatosAtaque, edicion, compilacion) + posicion * Longitud, Longitud) };
        }
        public static void SetDatosAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion,DatosAtaque datosAtaque)
        {
            BloqueBytes.SetBytes(rom, Zona.GetOffset(rom, Variable.DatosAtaque, edicion, compilacion) + posicion * Longitud, datosAtaque.blDatosAtaque.Bytes);
        }
    }
    public class Ataque : ObjectAutoId
    {
       
        //son 9 bits en total de alli el 511 :) asi en 2 bytes hay ataque y nivel :)
        public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479
        enum LongitudCampos
        {
            Nombre = 13,
           
            PointerEfecto = 4,
            ContestData,
            Descripcion = 4,//es un pointer al texto
            ScriptBatalla,
            Animacion
        }
        enum Variables
        {
            NombreAtaque, Descripción, ScriptBatalla, Animacion
        }
        static Ataque()
        {
            Zona zonaNombresAtaques = new Zona(Variables.NombreAtaque);
          
            Zona zonaDescripcion = new Zona(Variables.Descripción);
            Zona zonaScriptBatalla = new Zona(Variables.ScriptBatalla);
            Zona zonaAnimacion = new Zona(Variables.Animacion);
            //añado las zonas al diccionario
            Zona.DiccionarioOffsetsZonas.Add(zonaAnimacion);
            Zona.DiccionarioOffsetsZonas.Add(zonaScriptBatalla);
            Zona.DiccionarioOffsetsZonas.Add(zonaDescripcion);     
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

            //por investigar!!!
            //efectos el offset tiene que acabar en 0,4,8,C
            //de momento se tiene que investigar...lo que habia antes eran animaciones...

            //descripcion con ellas calculo el total :D
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0xE5440, 0xE5454);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0xE5418, 0xE542C);
            zonaDescripcion.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0xA0494, 0xA04B4);
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

            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x148B0);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x148B0);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x3E854);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x3E854);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x146e4);
            zonaScriptBatalla.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x146e4);
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
        DatosAtaque datosAtaque;
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

        public DatosAtaque DatosAtaque
        {
            get
            {
                return datosAtaque;
            }

          private  set
            {
                datosAtaque = value;
            }
        }

        public override string ToString()
        {
            return Nombre + "\n" + Descripcion;
        }

        public static int GetTotalAtaques(RomData rom)
        {
            return GetTotalAtaques(rom.RomGBA, rom.Edicion, rom.Compilacion);
        }
        public static int GetTotalAtaques(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Hex offsetDescripciones = Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion);
            int total = 1;//el primero lo salto porque no tiene descripcion :)
            while (Offset.IsAPointer(rom, offsetDescripciones))
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
        public static Ataque[] GetAtaques(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            Ataque[] ataques = new Ataque[GetTotalAtaques(rom, edicion, compilacion)];
            for (int i = 0; i < ataques.Length; i++)
                ataques[i] = GetAtaque(rom, edicion, compilacion, i);
            return ataques;
        }
        public static Ataque GetAtaque(RomData rom, Hex posicion)
        {
            return GetAtaque(rom.RomGBA, rom.Edicion, rom.Compilacion, posicion);
        }
        public static Ataque GetAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion)
        {
            BloqueString nombre = BloqueString.GetString(rom, Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre, (int)LongitudCampos.Nombre, true);
            //la descripcion del primer ataque no existe y todas las descripciones se retrasan 1
            BloqueString descripcion = BloqueString.GetString(rom, Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion) + (posicion == 0 ? posicion : posicion - 1) * (int)LongitudCampos.Descripcion));
            DatosAtaque datosAtaque = DatosAtaque.GetDatosAtaque(rom, edicion, compilacion, posicion);
            return new Ataque() { Nombre = nombre, Descripcion = descripcion,DatosAtaque=datosAtaque };
        }
        public static void SetAtaque(RomData rom, Hex posicion, Ataque ataque)
        {
            SetAtaque(rom.RomGBA, rom.Edicion, rom.Compilacion, posicion, ataque);
        }
        public static void SetAtaque(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex posicion, Ataque ataque)
        {
            Hex offsetNombre = Zona.GetOffset(rom, Variables.NombreAtaque, edicion, compilacion) + posicion * (int)LongitudCampos.Nombre;
            Hex offsetDescripcion = Offset.GetOffset(rom, Zona.GetOffset(rom, Variables.Descripción, edicion, compilacion) + posicion * (int)LongitudCampos.Descripcion);
            //nombre
            BloqueBytes.RemoveBytes(rom, offsetNombre, (int)LongitudCampos.Nombre);
            BloqueString.SetString(rom, offsetNombre, ataque.Nombre);
            //descripcion
            BloqueBytes.RemoveBytes(rom, offsetDescripcion, ataque.descripcion.LengthInnerRom);
            BloqueString.SetString(rom, offsetDescripcion, ataque.Descripcion);
            DatosAtaque.SetDatosAtaque(rom, edicion, compilacion, posicion, ataque.DatosAtaque);

        }
        public static void SetAtaques(RomData romData, IList<Ataque> ataques)
        {
            SetAtaques(romData.RomGBA, romData.Edicion, romData.Compilacion, ataques);
        }
        public static void SetAtaques(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, IList<Ataque> ataques)
        {
            for (int i = 0; i < ataques.Count; i++)
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
            Edicion edicion = Edicion.ZafiroEsp;
            if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion)))
            {
                edicion = Edicion.RubiEsp;
                if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion)))
                {
                    edicion = Edicion.RubiUsa;
                    if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion, CompilacionRom.Compilacion.Primera)))
                    {
                        if (!Offset.IsAPointer(Zona.GetOffset(rom, Variables.Animacion, edicion, CompilacionRom.Compilacion.Segunda)))
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
