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
using System;
using System.Collections.Generic;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonErrante.
	/// </summary>
	public static class PokemonErrante
	{
		public class Ruta
		{
			public const int MAXLENGTH = 7;
			public const byte MAXIMODERUTAS = byte.MaxValue-1;
			
			public static readonly Variable VariableBancoMapaRutaValido;
			public static readonly Variable VariableColumnasFilaRuta;
			public static readonly Variable VariableOffsetTablaFilasRuta;
			public static readonly Variable	VariableOffSetRutina1;
			public static readonly Variable	VariableOffSetRutina2;
			public static readonly Variable	VariableOffSetRutina3;
			
			static Ruta()
			{
				VariableBancoMapaRutaValido = new Variable("Pokemon Errante Banco Mapa Ruta Valido");
				VariableColumnasFilaRuta = new Variable("Pokemon Errante Columnas Fila Ruta");
				VariableOffsetTablaFilasRuta = new Variable("Pokemon Errante Offset Tabla Filas Ruta");
				VariableOffSetRutina1 = new Variable("Pokemon Errante OffSet Rutina 1");
				VariableOffSetRutina2 = new Variable("Pokemon Errante OffSet Rutina 2");
				VariableOffSetRutina3 = new Variable("Pokemon Errante OffSet Rutina 3");

				VariableBancoMapaRutaValido.Add( 0,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableBancoMapaRutaValido.Add( 3,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);

				VariableColumnasFilaRuta.Add(6,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableColumnasFilaRuta.Add(7,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);

				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.EsmeraldaEsp, 0xD5A140);
				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.RojoFuegoEsp, 0x64C685);

				//investigando zonaOffsetTablaFilasRuta
				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.EsmeraldaUsa, 0xD5A144);
				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.VerdeHojaEsp, 0x64BD7D);
				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.VerdeHojaUsa, 0x655665,0x6556D5);
				VariableOffsetTablaFilasRuta.Add(EdicionPokemon.RojoFuegoUsa, 0x655D89, 0x655DE9);

				VariableOffSetRutina1.Add(EdicionPokemon.EsmeraldaEsp, 0x161928);
				VariableOffSetRutina1.Add(EdicionPokemon.RojoFuegoEsp, 0x141D6E);

				//investigacion a ciegas
				VariableOffSetRutina1.Add(EdicionPokemon.EsmeraldaUsa, 0x161C84);
				VariableOffSetRutina1.Add(EdicionPokemon.VerdeHojaEsp, 0x141D46);
				VariableOffSetRutina1.Add(EdicionPokemon.VerdeHojaUsa, 0x141B6A,0x141BE2);
				VariableOffSetRutina1.Add(EdicionPokemon.RojoFuegoUsa, 0x141B92, 0x141C0A);

				VariableOffSetRutina2.Add(EdicionPokemon.EsmeraldaEsp, 0x1619c6);
				VariableOffSetRutina2.Add(EdicionPokemon.RojoFuegoEsp, 0x141df6);

				//investigacion a ciegas
				VariableOffSetRutina2.Add(EdicionPokemon.EsmeraldaUsa, 0x161D22);
				VariableOffSetRutina2.Add(EdicionPokemon.VerdeHojaEsp, 0x141D88);
				VariableOffSetRutina2.Add(EdicionPokemon.VerdeHojaUsa, 0x141BAC, 0x141C24);
				VariableOffSetRutina2.Add(EdicionPokemon.RojoFuegoUsa, 0x141BD4, 0x141C4C);

				VariableOffSetRutina3.Add(EdicionPokemon.EsmeraldaEsp, 0x161A82);
				VariableOffSetRutina3.Add(EdicionPokemon.RojoFuegoEsp, 0x141EAE);

				//investigacion a ciegas
				VariableOffSetRutina3.Add(EdicionPokemon.EsmeraldaUsa, 0x161DDE);
				VariableOffSetRutina3.Add(EdicionPokemon.VerdeHojaEsp, 0x141E86);
				VariableOffSetRutina3.Add(EdicionPokemon.VerdeHojaUsa,0x141CAA, 0x141D22);
				VariableOffSetRutina3.Add(EdicionPokemon.RojoFuegoUsa, 0x141CD2, 0x141D4A);


			}
			
			public byte[] Rutas { get; private set; }
			public Ruta()
			{
				Rutas = new byte[MAXLENGTH];
			}
			
			public static Ruta[] GetRutas(RomData rom)
			{
				return GetRutas(rom.Rom,rom.Edicion,rom.Compilacion);
			}
			public static Ruta[] GetRutas(RomGba rom, EdicionPokemon edicion,Compilacion compilacion)
			{
				int columnas = Variable.GetVariable( VariableColumnasFilaRuta, edicion, compilacion);
				Ruta[] rutas = new Ruta[rom.Data[Variable.GetVariable(VariableOffSetRutina1, edicion, compilacion)]];
				BloqueBytes bloqueDatos = BloqueBytes.GetBytes(rom.Data, Variable.GetVariable( VariableOffsetTablaFilasRuta, edicion, compilacion), columnas * rutas.Length);
				for (int i = 0; i < rutas.Length; i++)
				{
					rutas[i] = new Ruta();
					for (int j = 0; j < columnas; j++)
						rutas[i].Rutas[j] = bloqueDatos.Bytes[i * columnas + j];
				}
				return rutas;

			}
			public static void SetRutas(RomData rom,IList<Ruta> rutasDondeAparece)
			{
				SetRutas(rom.Rom,rom.Edicion,rom.Compilacion,rutasDondeAparece);
			}
			public static void SetRutas(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, IList<Ruta> rutasDondeAparece)
			{
				
				int columnas = Variable.GetVariable(VariableColumnasFilaRuta, edicion, compilacion);
				byte[] bytesRutas = new byte[rutasDondeAparece.Count * columnas];
				if (rutasDondeAparece.Count==0||rutasDondeAparece.Count> MAXIMODERUTAS)
					throw new ArgumentOutOfRangeException(); //como maximo 255 rutas
				//borro la tabla anterior
				rom.Data.Remove( Variable.GetVariable( VariableOffsetTablaFilasRuta, edicion, compilacion), columnas * rom.Data[Variable.GetVariable( VariableOffSetRutina1, edicion, compilacion)]);
				//pongo cuantas filas hay donde toca
				rom.Data[Variable.GetVariable( VariableOffSetRutina1, edicion, compilacion)] = (byte)rutasDondeAparece.Count;
				rom.Data[Variable.GetVariable( VariableOffSetRutina2, edicion, compilacion)] = (byte)rutasDondeAparece.Count;
				rom.Data[Variable.GetVariable( VariableOffSetRutina3, edicion, compilacion)] = (byte)(rutasDondeAparece.Count-1);//numero de filas-1 en el offset3
				//guardo la nueva tabla //el offset de la tabla tiene que acabar en '0', '4', '8', 'C'
				unsafe
				{
					fixed(byte* ptrBytesRutas = bytesRutas)
					{
						byte* ptBytesRutas = ptrBytesRutas,ptBytesRuta;
						for (int i = 0; i < rutasDondeAparece.Count; i++)
						{
							fixed(byte* ptrBytesRuta=rutasDondeAparece[i].Rutas)
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
				
				OffsetRom.SetOffset(rom,new OffsetRom(Variable.GetVariable( VariableOffsetTablaFilasRuta, edicion, compilacion)), rom.Data.SetArray(bytesRutas));
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
				Activo=0x100,Inactivo=0
			}
			public static readonly Variable VariableSpecialPokemonErrante;
			public static readonly Variable VariablePokemonErranteVar;
			public static readonly Variable VariableVitalidadVar;
			public static readonly Variable VariableNivelYEstadoVar;
			public static readonly Variable VariableDisponibleVar;
			
			public const int MAXTURNOSDORMIDO=7;
			
			PokemonGBAFrameWork.Pokemon pokemon;
			int vida;
			int nivel;
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


				VariableSpecialPokemonErrante.Add(0x12B,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableSpecialPokemonErrante.Add(0x129,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);


				VariablePokemonErranteVar.Add(0x4F24,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariablePokemonErranteVar.Add(0x506C,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
				VariablePokemonErranteVar.Add(EdicionPokemon.VerdeHojaUsa, 0x5100, 0x5114);
				VariablePokemonErranteVar.Add(EdicionPokemon.RojoFuegoUsa, 0x5100, 0x5114);

				VariableVitalidadVar.Add(0x4F25,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableVitalidadVar.Add(0x506D,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
				VariableVitalidadVar.Add(EdicionPokemon.VerdeHojaUsa, 0x5101, 0x5115);
				VariableVitalidadVar.Add(EdicionPokemon.RojoFuegoUsa, 0x5101, 0x5115);//logica

				VariableNivelYEstadoVar.Add(0x4F26,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableNivelYEstadoVar.Add(0x506E,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);

				VariableNivelYEstadoVar.Add(EdicionPokemon.VerdeHojaUsa, 0x5102, 0x5116);//logica
				VariableNivelYEstadoVar.Add(EdicionPokemon.RojoFuegoUsa, 0x5102, 0x5116);//logica

				VariableDisponibleVar.Add(0x5F29,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
				VariableDisponibleVar.Add( 0x5071,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);

				VariableDisponibleVar.Add(EdicionPokemon.VerdeHojaUsa, 0x5105, 0x5119);//logica
				VariableDisponibleVar.Add(EdicionPokemon.RojoFuegoUsa, 0x5105, 0x5119);//logica


			}
			public Pokemon(PokemonGBAFrameWork.Pokemon pokemon, int vida=1, int nivel=1, byte stats=0)
			{
				PokemonErrante = pokemon;
				Vida = vida;
				Nivel = nivel;
				Stats = stats;
			}
			public PokemonGBAFrameWork.Pokemon PokemonErrante
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

			public int Vida
			{
				get
				{
					return vida;
				}

				set
				{
					if (value < 0 || value > short.MaxValue) throw new ArgumentOutOfRangeException();//mirar de especificar el maximo
					vida = value;
				}
			}

			public int Nivel
			{
				get
				{
					return nivel;
				}

				set
				{
					if (value < 0) throw new ArgumentOutOfRangeException();
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
				get{
					return GetStatNoDormido(Stat.EnvenenamientoGrave);
				}
				set{
					SetStatNoDormido(Stat.EnvenenamientoGrave,value);
				}
			}
			public bool Envenenado
			{
				get{
					return GetStatNoDormido(Stat.Envenenado);
				}
				set{
					SetStatNoDormido(Stat.Envenenado,value);
				}
			}
			public bool Paralizado
			{
				get{
					return GetStatNoDormido(Stat.Paralizado);
				}
				set{SetStatNoDormido(Stat.Paralizado,value);}
			}
			public bool Congelado
			{
				get{
					return GetStatNoDormido(Stat.Congelado);
				}
				set{
					
					SetStatNoDormido(Stat.Congelado,value);
					
				}
			}

			

			public int Dormido
			{
				get{
					bool[] fix={false,false,false,false,false};
					return (stats.ToBits().SubArray(0,3)).AfegirValors(fix).ToArray().ToByte();
				}
				set{
					
					IList<bool> bitsStat;
					bool[] bitsAPoner;
					if(value>MAXTURNOSDORMIDO)
						value=MAXTURNOSDORMIDO;
					else if(value<0)
						value=0;
					//pongo los turnos
					bitsAPoner=((byte)value).ToBits();
					bitsStat=stats.ToBits();
					for(int i=0,f=3;i<f;i++)
						bitsStat[i]=bitsAPoner[i];
					stats=bitsStat.ToTaula().ToByte();
				}
			}

			public bool GetStatNoDormido(Stat i)
			{
				return stats.ToBits()[3+(int)i];
			}

			public void SetStatNoDormido(Stat i, bool value)
			{
				bool[] bitsStat=stats.ToBits();
				bitsStat[3+(int)i]=value;
				stats=bitsStat.ToByte();
			}
			#endregion
			//metodos para sacar el script en texto y en bytes...mas adelante sacarlo con las clases de los scripts :D
			//script
			public static void SetPokemonScript(RomGba rom, EdicionPokemon edicion, Compilacion compilacion,int offset, Pokemon pokemonErrante)
			{
				rom.Data.SetArray(offset, BytesScript( edicion, compilacion, pokemonErrante));
			}
			public static byte[] BytesScript(EdicionPokemon edicion,Compilacion compilacion ,Pokemon pokemonErrante)
			{
				//mas adelante pasarlo a la zona de scripts
				byte[] bytes;
				string[] campos;
				campos = BytesScriptString(edicion,compilacion,pokemonErrante).Replace("\r\n", ",").Replace("  ", ",").Replace(" ",",").Split(',');
				bytes = new byte[campos.Length];
				for (int i = 0; i < bytes.Length; i++)
					bytes[i] = Convert.ToByte((int)(Hex)campos[i]);
				return bytes;
			}
			public static string BytesScriptString(EdicionPokemon edicion, Compilacion compilacion, Pokemon pokemonErrante)
			{

				//mas adelante pasarlo a la zona de scripts
				string script, variableQueToca, stringQueToca;
				stringQueToca = ((Hex)Variable.GetVariable(VariableSpecialPokemonErrante, edicion, compilacion)).Number;
				stringQueToca = stringQueToca.PadLeft(4, '0');
				script = "25 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2);
				stringQueToca = ((Hex)Variable.GetVariable(VariablePokemonErranteVar, edicion, compilacion)).Number;
				variableQueToca = ((Hex)pokemonErrante.PokemonErrante.OrdenNacional).Number.PadLeft(4, '0');
				script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + variableQueToca.Substring(2, 2) + " " + variableQueToca.Substring(0, 2);
				stringQueToca = ((Hex)Variable.GetVariable(VariableVitalidadVar, edicion, compilacion)).Number;
				variableQueToca = ((Hex)pokemonErrante.Vida).Number.PadLeft(4, '0');
				script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + variableQueToca.Substring(2, 2) + " " + variableQueToca.Substring(0, 2);
				stringQueToca = ((Hex)Variable.GetVariable(VariableNivelYEstadoVar, edicion, compilacion)).Number;
				script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + ((Hex)pokemonErrante.Nivel).Number.PadLeft(2, '0') + " " + ((Hex)pokemonErrante.Stats).Number.PadLeft(2, '0');
				script += "\r\n02";
				return script;
			}
			public static string Script(EdicionPokemon edicion, Compilacion compilacion,Pokemon pokemonErrante)
			{

				const string CABEZERASCRIPT = "#dynamic 0x800000\r\n#org @ScriptPokemonErrante";
				string script = CABEZERASCRIPT;
				script += "\r\nspecial " + ((Hex)Variable.GetVariable(VariableSpecialPokemonErrante, edicion, compilacion)).ByteString;
				script += "\r\nsetvar  " + ((Hex)Variable.GetVariable(VariablePokemonErranteVar, edicion, compilacion)).ByteString+ " " + ((Hex)pokemonErrante.PokemonErrante.OrdenNacional).ByteString;
				script += "\r\nsetvar  " + ((Hex)Variable.GetVariable(VariableVitalidadVar, edicion, compilacion)).ByteString + " " + ((Hex)pokemonErrante.Vida).ByteString;
				script += "\r\nsetvar  " + ((Hex)Variable.GetVariable(VariableNivelYEstadoVar, edicion, compilacion)).ByteString+ " " + ((Hex)pokemonErrante.Stats).ByteString + ((Hex)pokemonErrante.Nivel).Number;
				script += "\r\nend";
				return script;
			}

		}
	}
}
