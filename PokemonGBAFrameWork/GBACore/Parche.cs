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
                this.Inicio = inicio;
                this.DatosOn = datos;
                this.ReemplazabaEspaciosEnBlanco = reemplazabaEspaciosEnBlanco;
                if (!reemplazabaEspaciosEnBlanco)
                {
                    DatosOff = origen.Data.SubArray(inicio, datos.Length);
                }
                else DatosOff = new byte[0];
            }

            public long IdEdicionOrigen { get; set; }
            public int Inicio { get; set; }
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
        Llista<Parte> partesParche;
        /// <summary>
        /// idEdicion, OffsetParche,OffsetEdicion
        /// </summary>
        LlistaOrdenada<long, Llista<TwoKeys<int, int>>> dicOffsetsAbsolutos;
        static Parche()
        {
            Serializador = ElementoBinario.GetSerializador<Parche>();
            MetodosBusquedaOffsetsCompatibles = new Llista<SearchOffsetCompatibleMethod>();
            //pongo los metodos
        }
        public Parche() { this.dicOffsetsAbsolutos = new LlistaOrdenada<long, Llista<TwoKeys<int, int>>>(); partesParche = new Llista<Parte>(); }

        public Parche(RomGba romVirgen, RomGba romConParche, bool forzarCompatibilidad = false) : this()
        {
            partesParche.AddRange(Parte.GetPartes(romVirgen, romConParche, forzarCompatibilidad));
        }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public bool TryAddCompatibility(RomGba romVirgen, RomGba romAAñadirCompatibilidad)
        {
            bool compatible = true;
            TwoKeysList<int, int, RomGba> dic = new TwoKeysList<int, int, RomGba>(); //RomGba es para poner un null...
            int offsetCompatible;
            List<int> offsetsABuscar;
            if (!dicOffsetsAbsolutos.ContainsKey(romAAñadirCompatibilidad.Edicion.Id))
            {
                try
                {
                    for (int i = 0; i < partesParche.Count; i++)
                    {
                        offsetsABuscar = partesParche[i].GetOffsets();
                        for (int j = 0; j < offsetsABuscar.Count; j++)
                        {
                            if (!dic.ContainsKey1(offsetsABuscar[j]))
                            {
                                offsetCompatible = SearchOffsetCompatible(romVirgen, romAAñadirCompatibilidad, offsetsABuscar[j]);
                                if (offsetCompatible >= 0)
                                {
                                    dic.Add(offsetsABuscar[j], offsetCompatible);
                                }
                            }

                        }
                    }

                    dicOffsetsAbsolutos.Add(romAAñadirCompatibilidad.Edicion.Id, new Llista<TwoKeys<int, int>>(dic.GetKeys()));
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
}
