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
        public class Mapa
        {
            public const int SALTOSESKANTO = 7;
            public const int SALTOSHOENN = 6;

            public static readonly byte[] MuestraAlgoritmoTablaKanto = { 0x02, 0x33, 0x02, 0x34, 0x02, 0x53, 0x02, 0x54, 0x02 };
            public static readonly byte[] MuestraAlgoritmoTablaRubiYZafiro = { 0x60, 0x7E, 0x02, 0x02, 0x0A, 0x00, 0x00, 0x00 };
            public static readonly byte[] MuestraAlgoritmoTablaEsmeralda = { 0xF0, 0x01, 0x00, 0x00, 0xE1, 0x11, 0x00, 0x00 };

            public class Salto
            {
                public byte[] Rutas { get; set; }

                public override string ToString()
                {
                    return String.Join(" ",Rutas.Select((r)=>((Hex)r).ToString()));
                }
            }
            public List<Salto> Saltos { get; set; } = new List<Salto>();

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
            scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(GetVariableNivelYEstadoVar(edicion), new Word(auxNivelYEstado)));//por mirar
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
            throw new NotImplementedException();
        }

        public static Word GetVariableVitalidadVar(Edicion edicion)
        {
            throw new NotImplementedException();
        }

        public static Word GetVariablePokemonErranteVar(Edicion edicion)
        {
            throw new NotImplementedException();
        }

        public static Word GetVariableSpecialPokemonErrante(Edicion edicion)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
