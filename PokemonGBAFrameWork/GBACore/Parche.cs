using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Binaris;
namespace PokemonGBAFrameWork
{

    public class Parche : IElementoBinarioComplejo
    {
        /// <summary>
        /// Delegado para añadir metodos de busqueda de offsets
        /// </summary>
        /// <param name="romVirgen"></param>
        /// <param name="romAAñadirCompatibilidad"></param>
        /// <param name="offsetVirgen"></param>
        /// <returns>devuelve el offset compatible o -1 si no lo encuentra</returns>
        public delegate int SearchOffsetCompatibleMethod(RomGba romVirgen, RomGba romAAñadirCompatibilidad, int offsetVirgen);

        public class Parte : IElementoBinarioComplejo
        {
            /// <summary>
            /// Si no encuentro diferencias en X bytes acabo la parte sino continuo.
            /// </summary>
            public static int MinEntrePartes = 10;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Parte>();

            public Parte() { }
            public Parte(RomGba origen, Edicion edicionParche, int inicio, byte[] datos)
            {//la edicion del parche puede ser diferente si es forzada de alli que esté por separado
                const byte ESPACIO0 = 0x0, ESPACIOFF = 0xFF;

                bool reemplazabaEspaciosEnBlanco = true;

                for (int i = 0, j = inicio; i < datos.Length && reemplazabaEspaciosEnBlanco; i++, j++)
                    reemplazabaEspaciosEnBlanco = origen.Data.Bytes[j] == ESPACIO0 || origen.Data.Bytes[j] == ESPACIOFF;

                this.IdEdicionOrigen = edicionParche.Id;
                this.OffsetInicio = inicio;
                this.DatosOn = datos;
                this.ReemplazabaEspaciosEnBlanco = reemplazabaEspaciosEnBlanco;
                if (!reemplazabaEspaciosEnBlanco)
                {
                    DatosOff = origen.Data.SubArray(inicio, datos.Length);
                }
                else DatosOff = new byte[0];
            }

            public long IdEdicionOrigen { get; set; }
            public int OffsetInicio { get; set; }
            public byte[] DatosOn { get; set; }
            public byte[] DatosOff { get; set; }
            public bool ReemplazabaEspaciosEnBlanco { get; set; }
            public bool EsRelativa
            {
                get { return ReemplazabaEspaciosEnBlanco; }
            }
            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
            public List<int> GetOffsets()
            {
                OffsetRom offset;
                int i = 0;
                int f = DatosOn.Length - OffsetRom.LENGTH - 1;//-1 para poder ver si hay un último offset
                List<int> offsets = new List<int>();
                DatosOn.UnsafeMethod((ptrDatosOn) =>
                {
                    while (i < f)
                    {
                        offset = new OffsetRom(DatosOn, i);
                        if (!offset.IsAPointer)
                            i++;
                        else
                        {
                            offsets.Add(offset.Offset);
                            i += OffsetRom.LENGTH;
                        }
                    }
                });
                return offsets;
            }
            public static List<Parte> GetPartes(RomGba romVirgen, RomGba romConParche, bool forzarCompatibilidad = false)
            {
                if (romVirgen.Edicion.Id != romConParche.Edicion.Id && !forzarCompatibilidad)
                    throw new RomNoCompatibleException(romVirgen.Edicion.GameCode, romConParche.Edicion.GameCode);
                List<Parte> partes = new List<Parte>();
                //saco las partes diferentes
                int inicioParte, contadorSeparacion;
                unsafe
                {
                    romVirgen.Data.Bytes.UnsafeMethod((ptrVirgen) =>
                    {
                        romConParche.Data.Bytes.UnsafeMethod((ptrConParche) =>
                        {
                            inicioParte = -1;
                            contadorSeparacion = 0;
                            for (int i = 0; i < romVirgen.Data.Length && i < romConParche.Data.Length; i++)
                            {

                                if (*ptrVirgen.PtrArray != *ptrConParche.PtrArray)
                                {
                                    if (inicioParte < 0)
                                        inicioParte = i;
                                    contadorSeparacion = 0;
                                }
                                else
                                {
                                    if (contadorSeparacion < MinEntrePartes)
                                        contadorSeparacion++;
                                    else
                                    {
                                        partes.Add(new Parte(romVirgen, romConParche.Edicion, inicioParte, romConParche.Data.SubArray(inicioParte, i - contadorSeparacion)));
                                        contadorSeparacion = 0;
                                        inicioParte = -1;
                                    }
                                }
                            }
                            if (inicioParte >= 0)
                            {
                                //se a acabado sin encontrar las suficientes diferencias para acabar la parte
                                partes.Add(new Parte(romVirgen, romConParche.Edicion, inicioParte, romConParche.Data.SubArray(inicioParte, romConParche.Data.Length - contadorSeparacion)));

                            }


                        });
                    });
                }
                return partes;
            }
        }

        public static readonly ElementoBinario Serializador;
        public static readonly Llista<SearchOffsetCompatibleMethod> MetodosBusquedaOffsetsCompatibles;
        static readonly SearchOffsetCompatibleMethod[] MetodosDefault;
        static Parche()
        {
            Serializador = ElementoBinario.GetSerializador<Parche>();
            MetodosBusquedaOffsetsCompatibles = new Llista<SearchOffsetCompatibleMethod>();
            
            MetodosDefault =new SearchOffsetCompatibleMethod[]{ /*pongo los metodos*/ };
        }
        public Parche()
        {
            this.DicOffsetsAbsolutos = new LlistaOrdenada<long, LlistaOrdenada<int, int>>();
            PartesParche = new Llista<Parte>();
            EdicionesCompatiblesConfirmadas = new LlistaOrdenada<long, bool>();
        }

        public Parche(RomGba romVirgen, RomGba romConParche, bool forzarCompatibilidad = false) : this()
        {
            PartesParche.AddRange(Parte.GetPartes(romVirgen, romConParche, forzarCompatibilidad));
        }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
        public string Autor { get; set; }
        public string Descripcion { get; set; }
        public string UrlPost { get; set; }
        public Llista<Parte> PartesParche { get; set; }
        /// <summary>
        /// idEdicion, OffsetParche,OffsetEdicion
        /// </summary>
        public LlistaOrdenada<long, LlistaOrdenada<int, int>> DicOffsetsAbsolutos { get; set; }
        public LlistaOrdenada<long, bool> EdicionesCompatiblesConfirmadas { get; set; }
        public long IdEdicionOrigen { get { return PartesParche[0].IdEdicionOrigen; } }
        public List<Parte> CheckDeadLock(bool pararAlPrimeroProblema=false)
        {
            List<Parte> partesConProblemas = new List<Parte>();
            SortedList<int, List<int>> dic = new SortedList<int, List<int>>();
            List<int> offsets;
            //miro las partes que se llaman entre ellas
            for(int i=0;i<PartesParche.Count;i++)
                if(PartesParche[i].EsRelativa)
                {
                    dic.Add(PartesParche[i].OffsetInicio, PartesParche[i].GetOffsets());

                }
            for (int i = 0; i < PartesParche.Count&&(!pararAlPrimeroProblema||partesConProblemas.Count==0); i++)
                if (PartesParche[i].EsRelativa)
                {
                    offsets = dic[PartesParche[i].OffsetInicio];
                    for (int j = 0,k=partesConProblemas.Count; j < offsets.Count&&k==partesConProblemas.Count; j++)
                        if (dic.ContainsKey(offsets[j]) && dic[offsets[j]].Contains(PartesParche[i].OffsetInicio))
                            partesConProblemas.Add(PartesParche[i]);

                }
            return partesConProblemas;
        }
        public void Restore(RomGba romADesparchear)
        {
            throw new NotImplementedException();
            //encontrar partes relativas y quitarlas
            //poner las partes absolutas off (se tienen que poner igualmente los offsets compatibles)
        }
        public void Apply(RomGba romAParchear)
        {
            const int INTENTOSMAX = 100;
            if (IdEdicionOrigen != romAParchear.Edicion.Id && !DicOffsetsAbsolutos.ContainsKey(romAParchear.Edicion.Id))
                throw new RomNoCompatibleException(romAParchear.Edicion.GameCode);
            if (CheckDeadLock(true).Count > 0)
                throw new Exception("Dead lock detected!");
            //pongo las partes relativas donde sea (si no estan)
            SortedList<int, int> dicOffsetsRelativosPuestos = new SortedList<int, int>();//offsetOriginal,offsetAPoner
            byte[] bytesParte;
            List<int> offsetsParte;
            List<Parte> partesRelativasConOffsetsRelativos = new List<Parte>();
            int intentos;
            int pos;
            for (int i = 0; i < PartesParche.Count; i++)
            {
                if (PartesParche[i].EsRelativa)
                {
                    offsetsParte = PartesParche[i].GetOffsets();
                    //miro que no sean relativos
                    if (!HayRelativos(offsetsParte))
                    {
                        //Pongo las partes relativas que no tienen offsets a partes relativas
                        bytesParte = PreparaParte(romAParchear.Edicion.Id, PartesParche[i], dicOffsetsRelativosPuestos);
                        dicOffsetsRelativosPuestos.Add(PartesParche[i].OffsetInicio, romAParchear.Data.SetArrayIfNotExist(bytesParte));
                    }
                    else
                    {
                        partesRelativasConOffsetsRelativos.Add(PartesParche[i]);
                    }
                }
            }

            intentos = 0;
            pos = 0;
            //necesito ordenar los relativos dependientes de otros relativos 
            while(partesRelativasConOffsetsRelativos.Count>0&&intentos<INTENTOSMAX)
            {
                try
                {
                    //Pongo las partes relativas que no tienen offsets a partes relativas
                    bytesParte = PreparaParte(romAParchear.Edicion.Id, partesRelativasConOffsetsRelativos[pos], dicOffsetsRelativosPuestos);
                    dicOffsetsRelativosPuestos.Add(partesRelativasConOffsetsRelativos[pos].OffsetInicio, romAParchear.Data.SetArrayIfNotExist(bytesParte));
                    dicOffsetsRelativosPuestos.RemoveAt(pos);
                    intentos = 0;
                }
                catch { intentos++; }
                pos = pos++ % partesRelativasConOffsetsRelativos.Count;
            }
            if (partesRelativasConOffsetsRelativos.Count > 0)//lo malo es que ya se habia aplicado una parte...mirar de solucionarlo...de momento que prueben con un clone y si no hay problemas que sustituyan :)
                throw new PartesProblematicasException(partesRelativasConOffsetsRelativos);


            //pongo las partes absolutas
            for (int i = 0; i < PartesParche.Count; i++)
                if (!PartesParche[i].EsRelativa)
                {
                    //pongo las partes absolutas
                    bytesParte = PreparaParte(romAParchear.Edicion.Id, PartesParche[i], dicOffsetsRelativosPuestos);
                    romAParchear.Data.SetArray(PartesParche[i].OffsetInicio, bytesParte);
                }
        }

        private byte[] PreparaParte(long idDestino, Parte parte, SortedList<int, int> dicOffsetsRelativosPuestos)
        {//en teoria hay todas las partes relativas necesarias y absolutas compatibles...si falta alguna lanzo excepción
            byte[] datosPreparados;
            List<int> offsetsParte = parte.GetOffsets();
            if (offsetsParte.Count > 0)
            {
                datosPreparados = (byte[])parte.DatosOn.Clone();
                //pongo los offsets compatibles
                for (int i = 0; i < offsetsParte.Count; i++)
                {
                    if (DicOffsetsAbsolutos[idDestino].ContainsKey(offsetsParte[i]))
                    {
                        //parte absoluta
                        OffsetRom.SetOffset(datosPreparados, new OffsetRom(offsetsParte[i]), new OffsetRom(DicOffsetsAbsolutos[idDestino].GetValue(offsetsParte[i])));
                    }
                    else if (dicOffsetsRelativosPuestos.ContainsKey(offsetsParte[i]))
                    {
                        //parte relativa
                        OffsetRom.SetOffset(datosPreparados, new OffsetRom(offsetsParte[i]), new OffsetRom(dicOffsetsRelativosPuestos[offsetsParte[i]]));
                    }
                    else
                    {
                        throw new RomNoCompatibleException(EdicionPokemon.GetEdicionCompatible(idDestino).GameCode);
                    }
                }
            }
            else
            {
                datosPreparados = parte.DatosOn;//al no haber partes no hay que tocar la array así que no pasa nada :)
            }
            return datosPreparados;
        }

        private bool HayRelativos(List<int> offsetsParte)
        {
            bool hayRelativos = false;
            for (int j = 0; j < offsetsParte.Count && !hayRelativos; j++)
                for (int i = 0; i < PartesParche.Count && !hayRelativos; i++)
                    if (PartesParche[i].EsRelativa)
                        hayRelativos = offsetsParte[j] == PartesParche[i].OffsetInicio;
            return hayRelativos;
        }

        public bool TryAddCompatibility(RomGba romVirgen, RomGba romAAñadirCompatibilidad)
        {
            bool compatible = romVirgen.Edicion.Id == romAAñadirCompatibilidad.Edicion.Id || DicOffsetsAbsolutos.ContainsKey(romAAñadirCompatibilidad.Edicion.Id);
            LlistaOrdenada<int, int> dic = new LlistaOrdenada<int, int>();
            int offsetCompatible;
            List<int> offsetsABuscar;

            if (!compatible)
            {
                try
                {
                    for (int i = 0; i < PartesParche.Count; i++)
                    {
                        offsetsABuscar = PartesParche[i].GetOffsets();
                        for (int j = 0; j < offsetsABuscar.Count; j++)
                        {
                            if (!dic.ContainsKey(offsetsABuscar[j]))
                            {
                                offsetCompatible = SearchOffsetCompatible(romVirgen, romAAñadirCompatibilidad, offsetsABuscar[j]);
                                if (offsetCompatible >= 0)
                                {
                                    dic.Add(offsetsABuscar[j], offsetCompatible);
                                }
                            }

                        }
                    }
                    DicOffsetsAbsolutos.Add(romAAñadirCompatibilidad.Edicion.Id, dic);
                }
                catch
                {
                    compatible = false;
                }
            }
            return compatible;
        }
        /// <summary>
        ///Busca un offset compatible, si no lo encuentra lanza una expeción
        /// </summary>
        /// <param name="romVirgen"></param>
        /// <param name="romAAñadirCompatibilidad"></param>
        /// <param name="offsetVirgen"></param>
        /// <returns>-1 si el offset apunta a un espacio en blanco</returns>
        public static int SearchOffsetCompatible(RomGba romVirgen, RomGba romAAñadirCompatibilidad, int offsetVirgen)
        {

            int offsetCompatible = int.MinValue;

            //miro si es un offset relativo
            if (romVirgen[offsetVirgen] == byte.MaxValue || romVirgen[offsetVirgen] == byte.MinValue)
                offsetCompatible = -1;
            else
            {
                for (int i = 0; i < MetodosDefault.Length && offsetCompatible == int.MinValue; i++)
                {
                    offsetCompatible = MetodosDefault[i](romVirgen, romAAñadirCompatibilidad, offsetVirgen);
                    if (offsetCompatible < 0)
                        offsetCompatible = int.MinValue;
                }
                for (int i = 0; i < MetodosBusquedaOffsetsCompatibles.Count && offsetCompatible == int.MinValue; i++)
                {
                    offsetCompatible = MetodosBusquedaOffsetsCompatibles[i](romVirgen, romAAñadirCompatibilidad, offsetVirgen);
                    if (offsetCompatible < 0)
                        offsetCompatible = int.MinValue;
                }
                if (offsetCompatible == int.MinValue)
                    throw new RomNoCompatibleException(romVirgen.Edicion.GameCode, romAAñadirCompatibilidad.Edicion.GameCode);
            }
            return offsetCompatible;
        }
    }
    public class PartesProblematicasException : Exception
    {
        public PartesProblematicasException(IList<Parche.Parte> partes)
        {
            this.Partes = partes;
        }

        public IList<Parche.Parte> Partes { get; private set; }
    }
}
