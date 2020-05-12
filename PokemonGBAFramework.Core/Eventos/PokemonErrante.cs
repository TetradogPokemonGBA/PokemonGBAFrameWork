using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class PokemonErrante
    {
        public static readonly Creditos Creditos;

        public class Mapa
        {
            public const int SALTOSESKANTO = 7;
            public const int SALTOSHOENN = 6;
            public const int MAXSALTOS = byte.MaxValue + 1;
            public static readonly byte[] MuestraAlgoritmoTablaKanto = { 0x02, 0x33, 0x02, 0x34, 0x02, 0x53, 0x02, 0x54, 0x02 };
            public static readonly byte[] MuestraAlgoritmoTablaRubiYZafiro = { 0x60, 0x7E, 0x02, 0x02, 0x0A, 0x00, 0x00, 0x00 };
            public static readonly byte[] MuestraAlgoritmoTablaEsmeralda = { 0xF0, 0x01, 0x00, 0x00, 0xE1, 0x11, 0x00, 0x00 };
            public const int MINSALTOS=3;
            public const byte MARCAFIN = 0xFF;
            public const int ErrorSaltoRepetido = -101;
            public const int ErrorNumeroDeSaltosInferior = -202;
            public const int ErrorRutaSaltoRepetida = -303;
            public const int ErrorRutaIgualAlSaltoDeLaLinea = -404;
            public const int ErrorSaltoLineaNoEncontrado = -505;
            public const int ErrorRutaRepetidaEnLinea = -606;
            public const int ErrorNoHayNingunaRutaQueApunteAUnSalto = -707;
            public const int TodoCorrecto = 123;
            public class Salto
            {
                public byte[] Rutas { get; set; }
                public bool Check
                {
                    get
                    {
                        int index = Rutas.IndexByte(MARCAFIN);
                        return index > MINSALTOS - 1||index==-1;
                    }
                }
                public override string ToString()
                {
                    return String.Join(" ",Rutas.Select((r)=>((Hex)r).ToString()));
                }
            }
            public List<Salto> Saltos { get; set; } = new List<Salto>();
            public bool CheckCount => Saltos != default && Saltos.Count < MAXSALTOS;
            public int Check
            {
                get
                {
                    
                    int toCheck = TodoCorrecto;
                    bool correcto = CheckCount;
                    SortedList<int, int> dic = new SortedList<int, int>();
                    SortedList<int, int> dicRow = new SortedList<int, int>();
                    //SortedList<int, int> dicRows = new SortedList<int, int>();
                    for (int i = 0; i < Saltos.Count && correcto; i++)
                    {
                        correcto = Saltos[i].Check;//Numero de saltos correcto
                        if (correcto)
                        {
                            correcto = !dic.ContainsKey(Saltos[i].Rutas[0]);//no se repiten
                            if (correcto)
                                dic.Add(Saltos[i].Rutas[0], Saltos[i].Rutas[0]);
                            else toCheck = ErrorSaltoRepetido;
                        }
                        else toCheck = ErrorNumeroDeSaltosInferior;
                    }
                    for (int i = 0; i < Saltos.Count && correcto; i++)
                    {
                        dicRow.Clear();
                        for (int j = 1; j < Saltos[i].Rutas.Length && correcto && Saltos[i].Rutas[j] != MARCAFIN; j++)
                        {
                            correcto = dic.ContainsKey(Saltos[i].Rutas[j]);
                            if (correcto)
                            {
                                correcto = Saltos[i].Rutas[0] != Saltos[i].Rutas[j];//todos los saltos tienen una linea de salto y no son a la misma ruta
                                if (correcto)
                                {
                                    correcto = !dicRow.ContainsKey(Saltos[i].Rutas[j]);//no se repiten dentro del mismo salto
                                    if (correcto)
                                    {
                                        dicRow.Add(Saltos[i].Rutas[j], Saltos[i].Rutas[j]);
                                        //if(!dicRows.ContainsKey(Saltos[i].Rutas[j]))
                                        //     dicRows.Add(Saltos[i].Rutas[j], Saltos[i].Rutas[j]);
                                    }
                                    else toCheck = ErrorRutaRepetidaEnLinea;
                                }
                                else toCheck = ErrorRutaIgualAlSaltoDeLaLinea;
                            }
                            else toCheck = ErrorSaltoLineaNoEncontrado;


                        }
                    }
                    //al parecer no es un problema porque en las ediciones de Kanto no lo tienen en cuenta...

                    //for (int i = 0; i < Saltos.Count && correcto; i++)
                    //{
                       
                    //        correcto = dicRows.ContainsKey(Saltos[i].Rutas[0]);//alguna linea lo salta
                    //        if (!correcto)
                    //             toCheck = ErrorNoHayNingunaRutaQueApunteAUnSalto;
                        
                    //}

                    return toCheck;
                }
            }
            public int Length => Saltos[0].Rutas.Length;
            public byte[] GetBytes()
            {
                byte[] data = new byte[Saltos.Count * Length];
                for (int i = 0; i < Saltos.Count; i++)
                    data.SetArray(i * Length, Saltos[i].Rutas);
                return data;
            }
            public static void Set(RomGba rom, Mapa mapa)
            {
                rom.Data.Replace(Get(rom).GetBytes(), mapa.GetBytes());
            }

            public static Mapa Get(RomGba rom,OffsetRom offsetMapaPokemonErrante = default)
            {
                const byte FIN = 0xFF;

                if (Equals(offsetMapaPokemonErrante, default))
                    offsetMapaPokemonErrante = GetOffset(rom);

                Salto salto;
                bool acabado;
                int totalRutasSalto = GetTotalRutasParaSaltar(rom);
                int offsetMapa = offsetMapaPokemonErrante;
                Mapa mapa = new Mapa();
               
                do
                {
                    salto = new Salto() { Rutas = rom.Data.SubArray(offsetMapa, totalRutasSalto) };
                    acabado = salto.Rutas[0]==FIN;
                    if(!acabado)
                       mapa.Saltos.Add(salto);
                    offsetMapa += totalRutasSalto;
                } while (!acabado);
                return mapa;
            }

            public static int GetBank(RomGba rom)
            {
                return rom.Edicion.EsHoenn ? 0 : 3;
            }

            public static int GetTotalRutasParaSaltar(RomGba rom)
            {
                return rom.Edicion.EsKanto ? SALTOSESKANTO : SALTOSHOENN;
            }

            public static OffsetRom GetOffset(RomGba rom)
            {
                byte[] algoritmo;

                if (rom.Edicion.EsEsmeralda)
                {
                    algoritmo = MuestraAlgoritmoTablaEsmeralda;
                }else if (rom.Edicion.EsKanto)
                {
                    algoritmo = MuestraAlgoritmoTablaKanto;
                }
                else
                {
                    algoritmo = MuestraAlgoritmoTablaRubiYZafiro;
                }

                return new OffsetRom(rom.Data.SearchArray(algoritmo)+algoritmo.Length);
            }


        }
        public class Pokemon
        {
            public enum Stat
            {
                Dormido = -1, Envenenado = 0, Quemado = 1, Congelado = 2, Paralizado = 3, EnvenenamientoGrave = 4
            }
            public enum Disponibilidad
            {//- Variable de la disponibilidad (0x100 = disponible, 0x0 = no disponible) = Esmeralda 0x5F29; FR 0x5071
                Activo = 0x100, Inactivo = 0
            }
            public enum Dormido : int
            {
                NoDormido = 0,//000
                UnTurno,//001
                DosTurnos,//010
                TresTurnos,//011
                CuatroTurnos,//100
                CincoTurnos,//101
                SeisTurnos,//110
                SieteTurnos//111

            }
            public const int MAXTURNOSDORM = 7;
            public Word Vida { get; set; }
            public Word Nivel { get; set; }
            public byte Stats { get; set; }
            #region Stats por separado
            public bool EnvenenadoGrave
            {
                get
                {
                    return GetStatNoDormido(Stat.EnvenenamientoGrave);
                }
                set
                {
                    SetStatNoDormido(Stat.EnvenenamientoGrave, value);
                }
            }
            public bool Envenenado
            {
                get
                {
                    return GetStatNoDormido(Stat.Envenenado);
                }
                set
                {
                    SetStatNoDormido(Stat.Envenenado, value);
                }
            }
            public bool Paralizado
            {
                get
                {
                    return GetStatNoDormido(Stat.Paralizado);
                }
                set { SetStatNoDormido(Stat.Paralizado, value); }
            }
            public bool Congelado
            {
                get
                {
                    return GetStatNoDormido(Stat.Congelado);
                }
                set
                {

                    SetStatNoDormido(Stat.Congelado, value);

                }
            }
            public bool Quemado
            {
                get
                {
                    return GetStatNoDormido(Stat.Quemado);
                }
                set
                {
                    SetStatNoDormido(Stat.Quemado, value);
                }
            }


            public Dormido TurnosDormido
            {
                //no funciona...por arreglar...
                get
                {
                    bool[] fix = { false, false, false, false, false };
                    byte bTurnos = fix.AfegirValors(Stats.ToBits().SubArray(0, 3)).ToArray().ToByte();
                    return (Dormido)bTurnos;

                }
                set
                {

                    IList<bool> bitsStat;
                    bool[] bitsAPoner;

                    //pongo los turnos
                    bitsAPoner = ((byte)value).ToBits();
                    bitsStat = Stats.ToBits();
                    for (int i = 0, f = 3; i < f; i++)
                        bitsStat[i] = bitsAPoner[5 + i];
                    Stats = bitsStat.ToArray().ToByte();
                }
            }


            public bool GetStatNoDormido(Stat i)
            {
                return Stats.ToBits()[3 + (int)i];
            }

            public void SetStatNoDormido(Stat i, bool value)
            {
                bool[] bitsStat = Stats.ToBits();
                bitsStat[3 + (int)i] = value;
                Stats = bitsStat.ToByte();
            }
            #endregion
            public Core.Pokemon Errante { get; set; }
        }
        static PokemonErrante()
        {
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO], "Ratzhier", "Investigación");
        }
        public static Script GetScript(RomGba rom, Pokemon pokemonErrante) => GetScript(rom.Edicion, pokemonErrante);
        public static Script GetScript(Edicion edicion, Pokemon pokemonErrante)
        {
            Hex nivelYEstado;
            string estado, nivel;
            ushort auxNivelYEstado;
            Script scriptPokemonErrante = new Script();
            scriptPokemonErrante.ComandosScript.Add(new ComandosScript.Special(GetVariableSpecialPokemonErrante(edicion)));
            scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(GetVariablePokemonErranteVar(edicion), pokemonErrante.Errante.OrdenNacional.Orden));
            scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(GetVariableVitalidadVar(edicion), pokemonErrante.Vida));
            estado = ((Hex)pokemonErrante.Stats).ToString().PadLeft(2, '0');
            nivel = ((Hex)((byte)pokemonErrante.Nivel)).ToString();
            nivelYEstado = (Hex)(estado + nivel);
            auxNivelYEstado = (ushort)(uint)nivelYEstado;
            scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(GetVariableNivelYEstadoVar(edicion), new Word(auxNivelYEstado)));
            
            return scriptPokemonErrante;


        }
        #region Variables
        #region SobreCarga
        public static Word GetVariableNivelYEstadoVar(RomGba rom)
        {
            return GetVariableNivelYEstadoVar(rom.Edicion);
        }

        public  static Word GetVariableVitalidadVar(RomGba rom)
        {
            return GetVariableVitalidadVar(rom.Edicion);
        }

        public static Word GetVariablePokemonErranteVar(RomGba rom)
        {
            return GetVariablePokemonErranteVar(rom.Edicion);
        }

        public static Word GetVariableSpecialPokemonErrante(RomGba rom)
        {
            return GetVariableSpecialPokemonErrante(rom.Edicion);
        }
        #endregion
        public static Word GetVariableNivelYEstadoVar(Edicion edicion)
        {
            Word var;
            if (edicion.EsEsmeralda)
            {
                var = 0x4F26;//ESP,USA
            }
            else if (edicion.EsKanto)
            {
                var = 0x506E;//ESP
            }
            else
            {
                var = 0x4B56;//ESP
            }
            return var;
        }

        public static Word GetVariableVitalidadVar(Edicion edicion)
        {
            Word var;
            if (edicion.EsEsmeralda)
            {
                var = 0x4F25;//ESP,USA
            }
            else if (edicion.EsKanto)
            {
                var = 0x506D;//ESP
            }
            else
            {
                var = 0x4B55;//ESP
            }
            return var;
        }

        public static Word GetVariablePokemonErranteVar(Edicion edicion)
        {
            Word var;
            if (edicion.EsEsmeralda)
            {
                var = 0x4F24;//ESP,USA
            }
            else if (edicion.EsKanto)
            {
                var = 0x506C;//ESP
            }
            else
            {
                var = 0x4B54;//ESP
            }
            return var;
        }

        public static Word GetVariableSpecialPokemonErrante(Edicion edicion)
        {
            Word var;
            if (edicion.EsEsmeralda)
            {
                var = 0x12B;//ESP,USA
            }
            else if (edicion.EsKanto)
            {
                var = 0x129;//ESP
            }
            else
            {
                var = 0x12B;//ESP
            }
            return var;
        }

        public static Word GetVariableVariableDisponibleVar(Edicion edicion)
        {
            Word var;
            if (edicion.EsEsmeralda)
            {
                var = 0x5F29;//ESP,USA
            }
            else if (edicion.EsKanto)
            {
                var = 0x5071;//ESP
            }
            else
            {
                var = 0x4B59;//ESP
            }
            return var;
        }
        #endregion
    }
}
