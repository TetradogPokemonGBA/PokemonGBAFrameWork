/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 17:11
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of PokemonErrante.
    /// </summary>
    public static class PokemonErrante
    {
        public class Ruta : IElementoBinarioComplejo
        {
            public const byte ID = 0x10;
            public const int MAXLENGTH = 7;
            public const byte MAXIMODERUTAS = byte.MaxValue - 1;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Ruta>();

            public static readonly Variable VariableBancoMapaRutaValido;
            public static readonly Variable VariableColumnasFilaRuta;
            public static readonly Variable VariableOffsetTablaFilasRuta;
            public static readonly Variable VariableOffSetRutina1;
            public static readonly Variable VariableOffSetRutina2;
            public static readonly Variable VariableOffSetRutina3;

            static Ruta()
            {
                VariableBancoMapaRutaValido = new Variable("Pokemon Errante Banco Mapa Ruta Valido");
                VariableColumnasFilaRuta = new Variable("Pokemon Errante Columnas Fila Ruta");
                VariableOffsetTablaFilasRuta = new Variable("Pokemon Errante Offset Tabla Filas Ruta");
                VariableOffSetRutina1 = new Variable("Pokemon Errante OffSet Rutina 1");
                VariableOffSetRutina2 = new Variable("Pokemon Errante OffSet Rutina 2");
                VariableOffSetRutina3 = new Variable("Pokemon Errante OffSet Rutina 3");

                VariableBancoMapaRutaValido.Add(0, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableBancoMapaRutaValido.Add(3, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);

                VariableColumnasFilaRuta.Add(6, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableColumnasFilaRuta.Add(7, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);

                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.EsmeraldaEsp10, 0xD5A140);
                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.RojoFuegoEsp10, 0x64C685);

                //investigando zonaOffsetTablaFilasRuta
                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.EsmeraldaUsa10, 0xD5A144);
                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.VerdeHojaEsp10, 0x64BD7D);
                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.VerdeHojaUsa10, 0x655665, 0x6556D5);
                VariableOffsetTablaFilasRuta.Add(EdicionPokemon.RojoFuegoUsa10, 0x655D89, 0x655DE9);

                VariableOffSetRutina1.Add(EdicionPokemon.EsmeraldaEsp10, 0x161928);
                VariableOffSetRutina1.Add(EdicionPokemon.RojoFuegoEsp10, 0x141D6E);

                //investigacion a ciegas
                VariableOffSetRutina1.Add(EdicionPokemon.EsmeraldaUsa10, 0x161C84);
                VariableOffSetRutina1.Add(EdicionPokemon.VerdeHojaEsp10, 0x141D46);
                VariableOffSetRutina1.Add(EdicionPokemon.VerdeHojaUsa10, 0x141B6A, 0x141BE2);
                VariableOffSetRutina1.Add(EdicionPokemon.RojoFuegoUsa10, 0x141B92, 0x141C0A);

                VariableOffSetRutina2.Add(EdicionPokemon.EsmeraldaEsp10, 0x1619c6);
                VariableOffSetRutina2.Add(EdicionPokemon.RojoFuegoEsp10, 0x141df6);

                //investigacion a ciegas
                VariableOffSetRutina2.Add(EdicionPokemon.EsmeraldaUsa10, 0x161D22);
                VariableOffSetRutina2.Add(EdicionPokemon.VerdeHojaEsp10, 0x141D88);
                VariableOffSetRutina2.Add(EdicionPokemon.VerdeHojaUsa10, 0x141BAC, 0x141C24);
                VariableOffSetRutina2.Add(EdicionPokemon.RojoFuegoUsa10, 0x141BD4, 0x141C4C);

                VariableOffSetRutina3.Add(EdicionPokemon.EsmeraldaEsp10, 0x161A82);
                VariableOffSetRutina3.Add(EdicionPokemon.RojoFuegoEsp10, 0x141EAE);

                //investigacion a ciegas
                VariableOffSetRutina3.Add(EdicionPokemon.EsmeraldaUsa10, 0x161DDE);
                VariableOffSetRutina3.Add(EdicionPokemon.VerdeHojaEsp10, 0x141E86);
                VariableOffSetRutina3.Add(EdicionPokemon.VerdeHojaUsa10, 0x141CAA, 0x141D22);
                VariableOffSetRutina3.Add(EdicionPokemon.RojoFuegoUsa10, 0x141CD2, 0x141D4A);


            }

            public byte[] Rutas { get; set; }

            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

            public Ruta()
            {
                Rutas = new byte[MAXLENGTH];
            }

            public static Ruta[] GetRutas(RomGba rom)
            {
                Ruta[] rutas = new Ruta[rom.Data[Variable.GetVariable(VariableOffSetRutina1, rom.Edicion)]];
                for (int i = 0; i < rutas.Length; i++)
                {
                    rutas[i] = GetRuta(rom, i);
                }
                return rutas;

            }

            public static Ruta GetRuta(RomGba rom, int posicion)
            {
                int columnas = Variable.GetVariable(VariableColumnasFilaRuta, rom.Edicion);
                Ruta ruta = new Ruta();
                BloqueBytes bloqueDatos = BloqueBytes.GetBytes(rom.Data, Variable.GetVariable(VariableOffsetTablaFilasRuta, rom.Edicion), columnas * rom.Data[Variable.GetVariable(VariableOffSetRutina1, rom.Edicion)]);

                for (int j = 0; j < columnas; j++)
                    ruta.Rutas[j] = bloqueDatos.Bytes[posicion * columnas + j];

                return ruta;

            }
            public static void SetRutas(RomGba rom, IList<Ruta> rutasDondeAparece)
            {

                int columnas = Variable.GetVariable(VariableColumnasFilaRuta, rom.Edicion);
                byte[] bytesRutas = new byte[rutasDondeAparece.Count * columnas];
                if (rutasDondeAparece.Count == 0 || rutasDondeAparece.Count > MAXIMODERUTAS)
                    throw new ArgumentOutOfRangeException(); //como maximo 255 rutas
                                                             //borro la tabla anterior
                rom.Data.Remove(Variable.GetVariable(VariableOffsetTablaFilasRuta, rom.Edicion), columnas * rom.Data[Variable.GetVariable(VariableOffSetRutina1, rom.Edicion)]);
                //pongo cuantas filas hay donde toca
                rom.Data[Variable.GetVariable(VariableOffSetRutina1, rom.Edicion)] = (byte)rutasDondeAparece.Count;
                rom.Data[Variable.GetVariable(VariableOffSetRutina2, rom.Edicion)] = (byte)rutasDondeAparece.Count;
                rom.Data[Variable.GetVariable(VariableOffSetRutina3, rom.Edicion)] = (byte)(rutasDondeAparece.Count - 1);//numero de filas-1 en el offset3
                                                                                                                         //guardo la nueva tabla //el offset de la tabla tiene que acabar en '0', '4', '8', 'C'
                unsafe
                {
                    fixed (byte* ptrBytesRutas = bytesRutas)
                    {
                        byte* ptBytesRutas = ptrBytesRutas, ptBytesRuta;
                        for (int i = 0; i < rutasDondeAparece.Count; i++)
                        {
                            fixed (byte* ptrBytesRuta = rutasDondeAparece[i].Rutas)
                            {
                                ptBytesRuta = ptrBytesRuta;
                                for (int j = 0; j < columnas; j++)
                                {
                                    *ptBytesRutas = *ptBytesRuta;
                                    ptBytesRutas++;
                                    ptBytesRuta++;
                                }
                            }
                        }
                    }
                }

                OffsetRom.SetOffset(rom, new OffsetRom(Variable.GetVariable(VariableOffsetTablaFilasRuta, rom.Edicion)), rom.Data.SearchEmptySpaceAndSetArray(bytesRutas));
            }

        }
        public class Pokemon : IElementoBinarioComplejo
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
            public const byte ID = 0x11;
            public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Pokemon>();

            public static readonly Variable VariableSpecialPokemonErrante;
            public static readonly Variable VariablePokemonErranteVar;
            public static readonly Variable VariableVitalidadVar;
            public static readonly Variable VariableNivelYEstadoVar;
            public static readonly Variable VariableDisponibleVar;

            public const int MAXTURNOSDORMIDO = 7;

            PokemonGBAFrameWork.PokemonCompleto pokemon;
            Word vida;
            Word nivel;
            byte stats;

            //falta Rubi y Zafiro esp y usa
            static Pokemon()
            {
                VariableSpecialPokemonErrante = new Variable("Pokemon Errante Special");
                VariablePokemonErranteVar = new Variable("Pokemon Errante Var");
                VariableVitalidadVar = new Variable("Pokemon Errante Vitalidad Var");
                VariableNivelYEstadoVar = new Variable("Pokemon Errante Nivel Y Estado Var");
                VariableDisponibleVar = new Variable("Pokemon Errante Disponible Var");

                //pongo los datos encontrados


                VariableSpecialPokemonErrante.Add(0x12B, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableSpecialPokemonErrante.Add(0x129, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
                //falta saber como activar el evento en rubi y zafiro se activa al pasar la liga y ver la tv...
                //-4 en la var del pokemon para los datos encripatos :D
                VariablePokemonErranteVar.Add(0x4F24, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariablePokemonErranteVar.Add(0x506C, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
                VariablePokemonErranteVar.Add(EdicionPokemon.VerdeHojaUsa10, 0x5100, 0x5114);
                VariablePokemonErranteVar.Add(EdicionPokemon.RojoFuegoUsa10, 0x5100, 0x5114);
                //investigacion mia :D
                VariablePokemonErranteVar.Add(0x4B54, EdicionPokemon.RubiEsp10);

                VariableVitalidadVar.Add(0x4F25, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableVitalidadVar.Add(0x506D, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
                VariableVitalidadVar.Add(EdicionPokemon.VerdeHojaUsa10, 0x5101, 0x5115);
                VariableVitalidadVar.Add(EdicionPokemon.RojoFuegoUsa10, 0x5101, 0x5115);//logica
                                                                                        //logica
                VariableVitalidadVar.Add(0x4B55, EdicionPokemon.RubiEsp10);

                VariableNivelYEstadoVar.Add(0x4F26, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableNivelYEstadoVar.Add(0x506E, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);

                VariableNivelYEstadoVar.Add(EdicionPokemon.VerdeHojaUsa10, 0x5102, 0x5116);//logica
                VariableNivelYEstadoVar.Add(EdicionPokemon.RojoFuegoUsa10, 0x5102, 0x5116);//logica
                                                                                           //logica
                VariableNivelYEstadoVar.Add(0x4B56, EdicionPokemon.RubiEsp10);

                VariableDisponibleVar.Add(0x5F29, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
                VariableDisponibleVar.Add(0x5071, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);

                VariableDisponibleVar.Add(EdicionPokemon.VerdeHojaUsa10, 0x5105, 0x5119);//logica
                VariableDisponibleVar.Add(EdicionPokemon.RojoFuegoUsa10, 0x5105, 0x5119);//logica
                                                                                         //logica
                VariableDisponibleVar.Add(0x4B59, EdicionPokemon.RubiEsp10);
            }
            public Pokemon() { }
            public Pokemon(PokemonGBAFrameWork.PokemonCompleto pokemon, Word vida = null, Word nivel = null, byte stats = 0)
            {
                PokemonErrante = pokemon;
                Vida = vida;
                Nivel = nivel;
                Stats = stats;
            }
            public PokemonGBAFrameWork.PokemonCompleto PokemonErrante
            {
                get
                {
                    return pokemon;
                }

                set
                {
                    if (value == null) throw new ArgumentNullException();
                    pokemon = value;
                }
            }

            public Word Vida
            {
                get
                {
                    return vida;
                }

                set
                {
                    if (value == null)
                        value = new Word(1);
                    vida = value;
                }
            }

            public Word Nivel
            {
                get
                {
                    return nivel;
                }

                set
                {
                    if (value == null)
                        value = new Word(1);
                    else if (value > byte.MaxValue)
                        throw new ArgumentOutOfRangeException();
                    nivel = value;
                }
            }

            public byte Stats
            {
                get
                {
                    return stats;
                }

                set
                {
                    stats = value;

                }
            }

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



            public Dormido TurnosDormido
            {
                //no funciona...por arreglar...
                get
                {
                    bool[] fix = { false, false, false, false, false };
                    byte bTurnos = fix.AfegirValors(stats.ToBits().SubArray(0, 3)).ToArray().ToByte();
                    return (Dormido)bTurnos;

                }
                set
                {

                    IList<bool> bitsStat;
                    bool[] bitsAPoner;

                    //pongo los turnos
                    bitsAPoner = ((byte)value).ToBits();
                    bitsStat = stats.ToBits();
                    for (int i = 0, f = 3; i < f; i++)
                        bitsStat[i] = bitsAPoner[5 + i];
                    stats = bitsStat.ToArray().ToByte();
                }
            }

            ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

            public bool GetStatNoDormido(Stat i)
            {
                return stats.ToBits()[3 + (int)i];
            }

            public void SetStatNoDormido(Stat i, bool value)
            {
                bool[] bitsStat = stats.ToBits();
                bitsStat[3 + (int)i] = value;
                stats = bitsStat.ToByte();
            }
            #endregion
            //metodos para sacar el script en texto y en bytes...mas adelante sacarlo con las clases de los scripts :D
            //script
            public static void SetPokemonScript(RomGba rom, int offset, Pokemon pokemonErrante)
            {
                rom.Data.SetArray(offset, GetScript(rom.Edicion, pokemonErrante).GetDeclaracion(rom));
            }

            public static Script GetScript(Edicion edicion, Pokemon pokemonErrante)
            {
                Hex nivelYEstado;
                string estado, nivel;
                ushort auxNivelYEstado;
                Script scriptPokemonErrante = new Script();
                scriptPokemonErrante.ComandosScript.Add(new ComandosScript.Special(new Word((ushort)Variable.GetVariable(VariableSpecialPokemonErrante, edicion))));
                scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(new Word((ushort)Variable.GetVariable(VariablePokemonErranteVar, edicion)), pokemonErrante.PokemonErrante.OrdenNacional.Orden));
                scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(new Word((ushort)Variable.GetVariable(VariableVitalidadVar, edicion)), pokemonErrante.Vida));
                estado = ((Hex)pokemonErrante.Stats).ToString().PadLeft(2, '0');
                nivel = ((Hex)((byte)pokemonErrante.Nivel)).ToString();
                nivelYEstado = (Hex)(estado + nivel);
                auxNivelYEstado = (ushort)(uint)nivelYEstado;
                scriptPokemonErrante.ComandosScript.Add(new ComandosScript.SetVar(new Word((ushort)Variable.GetVariable(VariableNivelYEstadoVar, edicion)), new Word(auxNivelYEstado)));//por mirar
                return scriptPokemonErrante;


            }

        }

        public static readonly Creditos Creditos;
        static PokemonErrante()
        {
            Creditos = new Creditos();
            Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO], "Ratzhier", "Investigación");
        }
        public static bool EsCompatible(EdicionPokemon edicion, Compilacion compilacion)
        {
            return Ruta.VariableBancoMapaRutaValido.Diccionario.ContainsKey(compilacion) && Ruta.VariableBancoMapaRutaValido.Diccionario[compilacion].ContainsKey(edicion);
        }


    }
}
