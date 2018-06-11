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

                for (int i = 0,j=inicio; i < datos.Length && reemplazabaEspaciosEnBlanco; i++,j++)
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
            public static List<Parte> GetPartes(RomGba romVirgen,RomGba romConParche,bool forzarCompatibilidad=false)
            {
                if (romVirgen.Edicion.Id != romConParche.Edicion.Id && !forzarCompatibilidad)
                    throw new RomNoCompatibleException(romVirgen.Edicion.GameCode,romConParche.Edicion.GameCode);
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
                            for (int i=0;i<romVirgen.Data.Length&&i<romConParche.Data.Length;i++)
                            {

                                if(*ptrVirgen.PtrArray!=*ptrConParche.PtrArray)
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
                            if(inicioParte>=0)
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

        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Parche>();

        Llista<Parte> partesParche;
        /// <summary>
        /// idEdicion, OffsetParche,OffsetEdicion
        /// </summary>
        LlistaOrdenada<long, Llista<TwoKeys<int, int>>> dicOffsetsAbsolutos;
        public Parche() { this.dicOffsetsAbsolutos = new LlistaOrdenada<long, Llista<TwoKeys<int, int>>>(); partesParche = new Llista<Parte>(); }

        public Parche(RomGba romVirgen,RomGba romConParche,bool forzarCompatibilidad=false):this()
        {
            partesParche.AddRange(Parte.GetPartes(romVirgen, romConParche, forzarCompatibilidad));
        }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        public bool TryAddCompatibility(RomGba romVirgen,RomGba romAAñadirCompatibilidad)
        {
            bool compatible = true;
            Llista<TwoKeys<int, int>> edicionAAñadir = new Llista<TwoKeys<int, int>>();
            //miro si es compatible buscando los offsets de las partes absolutas
            if(compatible)
            {
                dicOffsetsAbsolutos.Add(romAAñadirCompatibilidad.Edicion.Id, edicionAAñadir);
            }
            return compatible;
        }
    }
}
