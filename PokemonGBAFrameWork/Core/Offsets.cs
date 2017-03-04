/*
 * Creado por SharpDevelop.
 * Usuario: pc
 * Fecha: 27/08/2016
 * Hora: 19:01
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
//créditos Gamer2020 por el codigo fuente de PokemonGameEditor-3.7 que ha servido para establecer partes de la lógica
//créditos Naren jr. por la parte de obtener el offset con los bytes (lo de parsear y lo del 09 y 08)
namespace PokemonGBAFrameWork
{

    public enum Longitud
    {
        Offset = 4,
        Word = 2,
        DieciseisMegas = 0xFFFFFF,
        TreintaYDosMegas = DieciseisMegas * 2

    }


    public static class CompilacionRom
    {
        public enum Compilacion
        {
            Primera,
            Segunda
            //si hay mas se ponen cuando sean necesarias
        }
        public static Compilacion GetCompilacion(RomGBA rom)
        {
            return GetCompilacion(rom, Edicion.GetEdicion(rom));
        }
        public static Compilacion GetCompilacion(RomGBA rom, Edicion edicion)
        {
            Compilacion compilacion = GetCompilacion(rom, edicion, false);
            if (compilacion == Compilacion.Segunda)
                GetCompilacion(rom, edicion, true);//si no es correcta orignia una excepcion de formato
            return compilacion;

        }
        static Compilacion GetCompilacion(RomGBA rom, Edicion edicion, bool comprovarQueEsSegunda)
        {
            Compilacion compilacion;
            if (rom == null || edicion == null)
                throw new ArgumentNullException();
            compilacion = !comprovarQueEsSegunda ? Compilacion.Primera : Compilacion.Segunda;
            if (!Descripcion.ValidarZona(rom, edicion, compilacion))
            {
                if (comprovarQueEsSegunda)
                    throw new InvalidRomFormat();
                else//si no es valida es que es la segunda
                    compilacion = Compilacion.Segunda;
            }
            return compilacion;
        }
    }
    public static class Offset
    {
        public static Hex GetOffset(RomGBA rom, Hex offsetInicio)
        {
            return GetOffset(rom.Datos, offsetInicio);
        }
        public static Hex GetOffset(BloqueBytes bl, Hex offsetInicio)
        {
            return GetOffset(bl.Bytes, offsetInicio);
        }
        /// <summary>
        /// Obtiene el offset de la ROM
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offsetInicio"></param>
        /// <returns>si el pointer no tiene bien el formato correcto devuelve -1</returns>
        public static Hex GetOffset(byte[] bytes, Hex offsetInicio)
        {
            //los bytes estan permutados
            int posicion = (int)offsetInicio;
            byte byteComprobador = bytes[posicion + 3];
            Hex offset;
            unsafe
            {
                fixed (byte* ptrBytes = bytes)
                {
                    if (byteComprobador == 8 || byteComprobador == 9)
                        offset = ((Hex)(new byte[] {
                                                     ptrBytes[posicion+2],
                                                     ptrBytes[posicion+1],
                                                     ptrBytes[posicion]
                                 })) + (byteComprobador == 9 ? (int)Longitud.DieciseisMegas : 0);
                    else offset = -1;
                }
            }
            return offset;
        }
        /// <summary>
        /// convierte el offset en un pointer y lo pasa a byteArray
        /// </summary>
        /// <param name="offsetToSave"></param>
        /// <returns></returns>
        public static byte[] ToBytesRom(Hex offsetToSave)
        {
            uint offset = (uint)offsetToSave;
            bool esNueve = offset > (int)Longitud.DieciseisMegas;
            byte[] bytesPointer = new byte[(int)Longitud.Offset];
            int posicion = 0;

            if (esNueve)
                offset -= (int)Longitud.DieciseisMegas;
            unsafe
            {
                byte* ptrBytesPointer;
                fixed (byte* ptBytesPointer = bytesPointer)
                {
                    ptrBytesPointer = ptBytesPointer;
                    ptrBytesPointer += posicion;
                    *ptrBytesPointer = Convert.ToByte((offset & 0xff));
                    ptrBytesPointer++;
                    *ptrBytesPointer = Convert.ToByte(((offset >> 8) & 0xff));
                    ptrBytesPointer++;
                    *ptrBytesPointer = Convert.ToByte(((offset >> 0x10) & 0xff));
                    ptrBytesPointer++;
                    if (esNueve)
                        *ptrBytesPointer = 0x9;
                    else *ptrBytesPointer = 0x8;




                }
            }

            return bytesPointer;
        }
        /// <summary>
        /// Convierte de Pointer a Offset
        /// </summary>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public static Hex GetOffset(Hex pointer)
        {
            string pointerString = pointer.ToString();
            Hex resultado = pointerString.Substring(4, 2) + pointerString.Substring(2, 2) + pointerString.Substring(0, 2);
            if (pointerString.Substring(6, 2) == "09")
                resultado += (int)Longitud.DieciseisMegas;
            return resultado;

        }
        public static void SetOffset(RomGBA rom, Hex offsetInicio, Hex offsetToSave)
        {
            if (rom == null || offsetInicio < 0 || offsetToSave < 0 || offsetInicio + (int)Longitud.Offset > rom.Datos.Length || offsetToSave > (int)Longitud.TreintaYDosMegas) throw new ArgumentException();
            BloqueBytes.SetBytes(rom, offsetInicio, ToBytesRom(offsetToSave));
        }

        public static Hex ToPointer(Hex offset)
        {
            string offsetString = offset.ToString();
            bool esNueve = offset > (int)Longitud.DieciseisMegas;
            Hex resultado;
            if (esNueve)
            {
                offset -= (int)Longitud.DieciseisMegas;
                offsetString = offset.ToString();
            }
            resultado = offsetString.Substring(4, 2) + offsetString.Substring(2, 2) + offsetString.Substring(0, 2);
            if (esNueve)
                resultado = resultado.ToString() + "09";
            else resultado = resultado.ToString() + "08";
            return resultado;
        }
        public static Hex GetPointer(RomGBA rom, Hex offsetPointer)
        { return GetPointer(rom.Datos, offsetPointer); }
        public static Hex GetPointer(byte[] bytes, Hex offsetPointer)
        {
            //los bytes estan permutados
            int posicion = (int)offsetPointer;
            byte byteComprobador = bytes[posicion + 3];
            Hex offset;
            unsafe
            {
                fixed (byte* ptrBytes = bytes)
                {
                    if (byteComprobador == 8 || byteComprobador == 9)
                        offset = ((Hex)(new byte[] {
                                                    ptrBytes[posicion],
                                                    ptrBytes[posicion+1],
                                                    ptrBytes[posicion+2],
                                                    byteComprobador
                                  }));
                    else offset = -1;
                }
            }

            return offset;
        }

        public static bool IsAPointer(Hex pointerToCheck)
        {
            string stringHex = pointerToCheck.ByteString;
            return stringHex.EndsWith("08") || stringHex.EndsWith("09");
        }

        public static bool IsAPointer(RomGBA rom, Hex offsetByteValidador)
        {
            byte byteValidador = rom.Datos[offsetByteValidador+(int)Longitud.Offset-1];
            return (byteValidador == 0x8 || byteValidador == 0x9);
        }
    }
    public static class Word
    {
        public static void SetDWord(RomGBA rom, Hex offset, short dword)
        {
            SetWordOrDWord(rom.Datos, offset, dword,false);
        }
        public static void SetWord(RomGBA rom, Hex offset, short word)
        {
            SetWordOrDWord(rom.Datos, offset, word);
        }
        public static void SetDWord(byte[] rom, Hex offset, short dword)
        {
            SetWordOrDWord(rom, offset, dword,false);
        }
        public static void SetWord(byte[] rom,Hex offset, short word)
        {
            SetWordOrDWord(rom, offset, word);
        }
         static void SetWordOrDWord(byte[] rom, Hex offset, short word,bool isWord=true)
        {
            if (offset < 0 || offset + (int)Longitud.Word > rom.Length || word < short.MinValue || word > short.MaxValue)
                throw new ArgumentOutOfRangeException();
            int zonaWord = (int)offset;
            unsafe
            {
                byte* ptrDatos;
                fixed (byte* ptDatos = rom)
                {
                    ptrDatos = ptDatos;
                    ptrDatos += zonaWord;
                    if(isWord)
                        *ptrDatos = Convert.ToByte((word & 0xff));
                    else
                        *ptrDatos = Convert.ToByte(((word >> 8) & 0xff));
                    ptrDatos++;
                    if(isWord)
                        *ptrDatos = Convert.ToByte(((word >> 8) & 0xff));
                    else
                        *ptrDatos = Convert.ToByte((word & 0xff));
                }
            }

        }

        public static short GetWord(RomGBA rom, Hex offsetWord)
        {
            return GetWord(rom.Datos, offsetWord);
        }
        public static short GetWord(byte[] bytes, Hex offsetWord)
        {
            return GetWordOrDWord(bytes, offsetWord);
        }
        public static short GetDWord(RomGBA rom, Hex offsetDWord)
        {
            return GetDWord(rom.Datos, offsetDWord);
        }
        public static short GetDWord(byte[] bytes,Hex offsetDWord)
        {
            return GetWordOrDWord(bytes, offsetDWord, false);
        }

        static short GetWordOrDWord(byte[] bytes,Hex offsetWordOrDWord,bool esWord=true)
        {
            if (offsetWordOrDWord + (int)Longitud.Word > bytes.Length)
                throw new ArgumentOutOfRangeException();
            short num;
            byte[] bytesWordOrDWord;
            uint offsetWordP1 = (uint)offsetWordOrDWord;
            uint offsetWordP2 = offsetWordP1 + 1;
            unsafe
            {
                fixed (byte* ptrDatos = bytes)
                    if(!esWord)
                      bytesWordOrDWord = new byte[] { ptrDatos[offsetWordP2], ptrDatos[offsetWordP1] };
                    else
                      bytesWordOrDWord = new byte[] { ptrDatos[offsetWordP1], ptrDatos[offsetWordP2] };

                num = Serializar.ToShort(bytesWordOrDWord);
            }
            return num;
        }
    }
    /// <summary>
    /// las zonas son offsets donde se guardan offsets permutados.
    /// </summary>
    public class Zona : IComparable, IComparable<Zona>, IClauUnicaPerObjecte
    {
        static readonly int NumeroCompilaciones = Enum.GetNames(typeof(CompilacionRom.Compilacion)).Length;
        /// <summary>
        /// Sirve para poder usarse entre clases de forma global
        /// </summary>
        public static LlistaOrdenada<Zona> DiccionarioOffsetsZonas = new LlistaOrdenada<Zona>();


        string variable;
        LlistaOrdenada<Edicion, Hex[]> diccionarioZonas;

        public Zona(string variable)
        {
            this.variable = variable;
            diccionarioZonas = new LlistaOrdenada<Edicion, Hex[]>();
        }
        public Zona(Enum enumVariable) : this(enumVariable.ToString()) { }
        public string Variable
        {
            get
            {
                return variable;
            }
            set
            {
                if (value == null)
                    value = "";
                variable = value;
            }
        }
        public Hex[] this[Edicion edicionZona]
        {
            get
            {
                if (!diccionarioZonas.ContainsKey(edicionZona))
                    throw new RomInvestigacionExcepcion();
                return diccionarioZonas[edicionZona];
            }
            set
            {
                if (value == null || value.Length == 0)
                    throw new ArgumentException();
                if (!diccionarioZonas.ContainsKey(edicionZona))
                    throw new RomInvestigacionExcepcion();
                diccionarioZonas[edicionZona] = value;
            }
        }
        public Hex this[Edicion edicionZona, CompilacionRom.Compilacion compilacion]
        {
            get
            {
                if (compilacion < CompilacionRom.Compilacion.Primera || compilacion > CompilacionRom.Compilacion.Segunda)
                    throw new ArgumentOutOfRangeException();
                return this[edicionZona][(int)compilacion];
            }
            set
            {
                if (compilacion < CompilacionRom.Compilacion.Primera || compilacion > CompilacionRom.Compilacion.Segunda)
                    throw new ArgumentOutOfRangeException();
                Hex[] zonasPorCompilacion = this[edicionZona];
                zonasPorCompilacion[(int)compilacion] = value;
                this[edicionZona] = zonasPorCompilacion;
            }
        }
        public void AddOrReplaceZonaOffset(Edicion edicion, params Hex[] zonasOffset)
        {
            AddOrReplaceZonaOffset(edicion, true, zonasOffset);
        }
        /// <summary>
        /// añade o reemplaza las zonas para esa edicion
        /// </summary>
        /// <param name="edicion"></param>
        /// <param name="ponerPrimeraZonaSiFaltan">si es false pone la ultima</param>
        /// <param name="zonasOffset">el orden lo indica la compilacion de destino, en caso de faltar se pondrá la primera hasta tener todas</param>
        public void AddOrReplaceZonaOffset(Edicion edicion, bool ponerPrimeraZonaSiFaltan, params Hex[] zonasOffset)
        {
            Hex[] zonasAPoner;

            if (edicion == null || zonasOffset == null)
                throw new ArgumentNullException();
            if (zonasOffset.Length == 0)
                throw new ArgumentException("Se tiene que pasar como minimo una zonaOffset", "zonasOffset");
            if (zonasOffset.Length > NumeroCompilaciones)
                zonasAPoner = zonasOffset.SubList(0, NumeroCompilaciones).ToTaula();
            else if (zonasOffset.Length < NumeroCompilaciones)
            {
                zonasAPoner = new Hex[NumeroCompilaciones];
                for (int i = 0; i < NumeroCompilaciones; i++)
                    if (ponerPrimeraZonaSiFaltan)
                        zonasAPoner[i] = zonasOffset[0];
                    else
                        zonasAPoner[i] = zonasOffset[zonasOffset.Length - 1];
            }
            else
                zonasAPoner = (Hex[])zonasOffset.Clone();//asi evito que modifiquen la array desde fuera sin control
            diccionarioZonas.AddOrReplace(edicion, zonasAPoner);
        }
        public bool RemoveZonaOffset(Edicion edicion)
        {
            if (edicion == null)
                throw new ArgumentNullException("edicion");
            return diccionarioZonas.Remove(edicion);
        }

        #region IClauUnicaPerObjecte implementation
        public IComparable Clau
        {
            get
            {
                return variable;
            }
        }
        #endregion
        #region IComparable implementation


        public int CompareTo(object obj)
        {
            return CompareTo(obj as Zona);
        }

        #region IComparable implementation
        public int CompareTo(Zona other)
        {
            int compareTo;
            if (other != null)
                compareTo = string.Compare(variable, other.Variable, StringComparison.Ordinal);
            else
                compareTo = -1;
            return compareTo;
        }
        #endregion

        #endregion
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, Enum variableZona)
        {
            return GetOffset(rom, variableZona.ToString());
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, string variableZona)
        {
            return GetOffset(rom, variableZona, Edicion.GetEdicion(rom));
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <param name="compilacion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, Enum variableZona, CompilacionRom.Compilacion compilacion)
        {
            return GetOffset(rom, variableZona.ToString(), compilacion);
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <param name="compilacion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, string variableZona, CompilacionRom.Compilacion compilacion)
        {
            return GetOffset(rom, variableZona, Edicion.GetEdicion(rom), compilacion);
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <param name="edicion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, Enum variableZona, Edicion edicion)
        {
            return GetOffset(rom, variableZona.ToString(), edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <param name="edicion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, string variableZona, Edicion edicion)
        {
            return GetOffset(rom, variableZona, edicion, CompilacionRom.GetCompilacion(rom, edicion));
        }
        public static Hex GetVariable(RomGBA rom, Enum variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            return GetVariable(rom, variableZona.ToString(), edicion, compilacion);
        }
        public static Hex GetVariable(RomGBA rom, string variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            return DiccionarioOffsetsZonas[variableZona][edicion, compilacion];
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="variableZona"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, Enum variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            return GetOffset(rom, variableZona.ToString(), edicion, compilacion);
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="variableZona">Usa el diccionario para obtener la zona </param>
        /// <returns>Si no es valido devuelve -1</returns>
        public static Hex GetOffset(RomGBA rom, string variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            if (string.IsNullOrEmpty(variableZona))
                throw new ArgumentNullException();
            return GetOffset(rom, DiccionarioOffsetsZonas[variableZona], edicion, compilacion);
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="zona"></param>
        /// <returns>Si no es valido devuelve -1</returns>
		public static Hex GetOffset(RomGBA rom, Zona zona)
        {
            return GetOffset(rom, zona, Edicion.GetEdicion(rom));
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="zona"></param>
        /// <param name="edicion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
		public static Hex GetOffset(RomGBA rom, Zona zona, Edicion edicion)
        {
            return GetOffset(rom, zona[edicion, CompilacionRom.GetCompilacion(rom, edicion)]);
        }
        /// <summary>
        /// Obtiene el Offset leido de la ROM escrito como POINTER
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="zona"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <returns>Si no es valido devuelve -1</returns>
		public static Hex GetOffset(RomGBA rom, Zona zona, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            if (rom == null || zona == null || edicion == null)
                throw new ArgumentNullException();
            else if (!zona.diccionarioZonas.ContainsKey(edicion) || zona.diccionarioZonas[edicion].Length < (int)compilacion)
                throw new RomInvestigacionExcepcion();

            return Offset.GetOffset(rom, zona[edicion, compilacion]);
        }
        public static void SetOffset(RomGBA rom, Enum variableZona, Hex offsetToSave)
        {
            SetOffset(rom, variableZona.ToString(), offsetToSave);
        }

        public static void SetOffset(RomGBA rom, string variableZona, Hex offsetToSave)
        {
            SetOffset(rom, variableZona, Edicion.GetEdicion(rom), offsetToSave);
        }
        public static void SetOffset(RomGBA rom, Enum variableZona, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
        {
            SetOffset(rom, variableZona.ToString(), compilacion, offsetToSave);
        }

        public static void SetOffset(RomGBA rom, string variableZona, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
        {
            SetOffset(rom, variableZona, Edicion.GetEdicion(rom), compilacion, offsetToSave);
        }
        public static void SetOffset(RomGBA rom, Enum variableZona, Edicion edicion, Hex offsetToSave)
        {
            SetOffset(rom, variableZona.ToString(), edicion, offsetToSave);
        }

        public static void SetOffset(RomGBA rom, string variableZona, Edicion edicion, Hex offsetToSave)
        {
            SetOffset(rom, variableZona, edicion, CompilacionRom.GetCompilacion(rom, edicion), offsetToSave);
        }
        public static void SetOffset(RomGBA rom, Enum variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
        {
            SetOffset(rom, variableZona.ToString(), edicion, compilacion, offsetToSave);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="edicion"></param>
        /// <param name="compilacion"></param>
        /// <param name="variableZona">Usa el diccionario para obtener la zona </param>
        /// <param name="offsetToSave"></param>
        public static void SetOffset(RomGBA rom, string variableZona, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
        {
            if (string.IsNullOrEmpty(variableZona))
                throw new ArgumentNullException();
            SetOffset(rom, DiccionarioOffsetsZonas[variableZona], edicion, compilacion, offsetToSave);
        }
        /// <summary>
        /// Actualiza el pointer de todas las zonas donde estaba el anterior
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="zona"></param>
        /// <param name="offsetToSave"></param>
        public static void SetOffset(RomGBA rom, Zona zona, Hex offsetToSave)
        {
            if (zona == null)
                throw new ArgumentNullException();
            SetOffset(rom, zona, Edicion.GetEdicion(rom), offsetToSave);
        }
        public static void SetOffset(RomGBA rom, Zona zona, Edicion edicion, Hex offsetToSave)
        {
            if (zona == null)
                throw new ArgumentNullException();
            SetOffset(rom, zona, edicion, CompilacionRom.GetCompilacion(rom, edicion), offsetToSave);
        }
        public static void SetOffset(RomGBA rom, Zona zona, Edicion edicion, CompilacionRom.Compilacion compilacion, Hex offsetToSave)
        {
            if (zona == null)
                throw new ArgumentNullException();
            SetOffset(rom, zona[edicion, compilacion], offsetToSave);
        }
        /// <summary>
        /// Actualiza el pointer de todas las zonas donde estaba el anterior
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="offsetZona"></param>
        /// <param name="offsetToSave"></param>
        public static void SetOffset(RomGBA rom, Hex offsetZona, Hex offsetToSave)
        {
            if (rom == null)
                throw new ArgumentNullException();
            if (offsetZona < 0 || offsetToSave < 0 || offsetToSave > (int)Longitud.TreintaYDosMegas || offsetToSave + (int)Longitud.Offset > rom.Datos.Length)
                throw new ArgumentOutOfRangeException();

            Hex offsetZonaAActualizar = 0;//buscar el minimo real porque no hay pointers en el byte 4 (ya que 0+(int)Offsets.Longitud.Offset)
            byte[] bytesOffsetOld;
            byte[] bytesPointer = Offset.ToBytesRom(offsetToSave);


            bytesOffsetOld = BloqueBytes.GetBytes(rom, offsetZona, (int)Longitud.Offset).Bytes;//los bytes del offset a cambiar por el nuevo
                                                                                               //busco todos los offsets que tengan los bytes viejos y los sustituyo (desde el principio)
            do
            {
                offsetZonaAActualizar = BloqueBytes.SearchBytes(rom, offsetZonaAActualizar + (int)Longitud.Offset, bytesOffsetOld);
                if (offsetZonaAActualizar > -1)
                    BloqueBytes.SetBytes(rom, offsetZonaAActualizar, bytesPointer);
            } while (offsetZonaAActualizar > -1);
        }
        //hacer metodo para guardar y cargar zonas desde un xml

    }
}
