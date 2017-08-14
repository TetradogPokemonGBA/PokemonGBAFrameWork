/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	public static class Shinyzer
	{
		public static readonly ASM RutinaEsmeralda=ASM.Compilar(Resources.ASMShinyzer);
		public static int VariableShinytzer = (int)(Hex)"8003";//mirar de poder cambiarla...

		const byte BYTE1ON=0x88;
		const byte BYTE2ON=0x88;
		const byte BYTE3ON=0x4;
		static readonly byte[] Array1ON=new byte[]{0x1,0x48};
		static readonly byte[] Array2ON=new byte[]{0xF0,0xFF,0xFA,0x01,0xE0};//el segundo byte depende de la edición
		
		const byte BYTE1OFF=0x78;
		const byte BYTE2OFF=0x78;
		const byte BYTE3OFF=0x2;
		static readonly byte[] Array1OFF=new byte[]{0xA0,0x78};
		static readonly byte[] Array2OFF=new byte[]{0x04,0x09,0x18,0xE0,0x78,0x00,0x06,0x09,0x18};
		
		static readonly Variable VariableEdicionArray2;
		
		static readonly Variable VariablePosicionInicio;
		static readonly LlistaOrdenada<EdicionPokemon,byte[]> DicRutina;

		const int SUMAENTREBYTES=2;
		const int SUMAENTREARRAYS=3;
		
		//falta la rutina de cada uno
		
		static Shinyzer()
		{
			byte[] aux=null;
			VariableEdicionArray2=new Variable("Variable edición array 2");
			VariableEdicionArray2.Add(0x2D,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			VariableEdicionArray2.Add(0x14,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);
			VariableEdicionArray2.Add(0x0C,EdicionPokemon.RubiUsa,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroUsa,EdicionPokemon.ZafiroEsp);
			
			VariablePosicionInicio=new Variable("Posicion inicio Variables");
			VariablePosicionInicio.Add(0x3D4DD,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			VariablePosicionInicio.Add(0x3D6A9,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			VariablePosicionInicio.Add(0x406C5,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoUsa);
			VariablePosicionInicio.Add(0x405B1,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.RojoFuegoEsp);
			VariablePosicionInicio.Add(0x6AF91,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			
			DicRutina=new LlistaOrdenada<EdicionPokemon, byte[]>();
			
			
			//se tiene que mirar el codigo fuente...para la variable poderla cambiar
			//falta añadir el codigo fuente de todos...de momento pondré los bytes compilados...mas adelante poner el codigo fuente :)
			//aux=COmpilo RUBI y SHAPHIRE//los ultimos 4 bytes es un pointer a un texto
			DicRutina.Add(EdicionPokemon.RubiUsa,aux);
			DicRutina.Add(EdicionPokemon.ZafiroUsa,aux);
			DicRutina.Add(EdicionPokemon.RubiEsp,aux);
			DicRutina.Add(EdicionPokemon.ZafiroEsp,aux);
			//aux=Compilo Verde y Rojo
			DicRutina.Add(EdicionPokemon.VerdeHojaUsa,aux);
			DicRutina.Add(EdicionPokemon.RojoFuegoUsa,aux);
			DicRutina.Add(EdicionPokemon.VerdeHojaEsp,aux);
			DicRutina.Add(EdicionPokemon.RojoFuegoEsp,aux);
			
			aux=RutinaEsmeralda.AsmBinary;
			DicRutina.Add(EdicionPokemon.EsmeraldaUsa,aux);
			DicRutina.Add(EdicionPokemon.EsmeraldaEsp,aux);
			
			

		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			if (rom == null||edicion==null||compilacion==null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(Variable.GetVariable(VariableEdicionArray2,edicion,compilacion),Array2OFF)>0;
		}
		public static int Activar(RomData rom)
		{
			return Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom,edicion);
			int offsetAct;
			if(posicion<0){
				offsetAct=Variable.GetVariable(VariablePosicionInicio,edicion,compilacion);
				rom.Data[offsetAct]=BYTE1ON;
				offsetAct+=SUMAENTREBYTES;
				rom.Data[offsetAct]=BYTE2ON;
				offsetAct+=SUMAENTREBYTES;
				rom.Data[offsetAct]=BYTE3ON;
				offsetAct+=SUMAENTREARRAYS;
				rom.Data.SetArray(offsetAct,Array1ON);
				offsetAct+=SUMAENTREARRAYS;
				rom.Data.SetArray(offsetAct,Array2ON);
				offsetAct+=Array2ON.Length;
				posicion=rom.Data.SetArray(DicRutina[edicion]);
				rom.Data.SetArray(offsetAct,new PokemonGBAFrameWork.OffsetRom(posicion).BytesPointer);//pongo el pointer a al rutina
				
			}
			return posicion;

		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom,edicion);
			int offsetAct;
			if (posicion > 0)
			{
				rom.Data.Remove( posicion, DicRutina[edicion].Length);
				offsetAct=Variable.GetVariable(VariablePosicionInicio,edicion,compilacion);
				rom.Data[offsetAct]=BYTE1OFF;
				offsetAct+=SUMAENTREBYTES;
				rom.Data[offsetAct]=BYTE2OFF;
				offsetAct+=SUMAENTREBYTES;
				rom.Data[offsetAct]=BYTE3OFF;
				offsetAct+=SUMAENTREARRAYS;
				rom.Data.SetArray(offsetAct,Array1OFF);
				offsetAct+=SUMAENTREARRAYS;
				rom.Data.SetArray(offsetAct,Array2OFF);
				
			}
		}
		/// <summary>
		/// Permite saber donde esta la rutina insertada
		/// </summary>
		/// <param name="rom"></param>
		/// <returns>devuelve -1 si no lo ha encontrado</returns>
		public  static int PosicionRutinaShinyzer(RomGba rom,EdicionPokemon edicion)
		{
			if (rom == null||edicion==null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(DicRutina[edicion]);
		}
		public static ComandosScript.SetVar ScriptLineaPokemonShiny(short numPokemon)
		{
			if (numPokemon<0)
				throw new ArgumentOutOfRangeException();
			return new ComandosScript.SetVar(VariableShinytzer,numPokemon);
		}
		/// <summary>
		/// Crear fácilmente el script shinytzer para que el entrenador al ser llamado tenga esos pokemon
		/// </summary>
		/// <param name="entrenador"></param>
		/// <param name="pokemon">primer,segundo,tercero,cuarto...</param>
		/// <returns></returns>
		public static ComandosScript.SetVar ScriptLineaPokemonShinyEntrenador(Entrenador entrenador,params bool[] pokemon)
		{
			if (entrenador == null)
				throw new ArgumentNullException();

			const int MAXPOKEMONENTRENADOR = 6,BITSBYTE=8;

			Hex numPokemonShiny;
			bool[] pokemonFinal;

			if (pokemon.Length < MAXPOKEMONENTRENADOR)
			{
				pokemonFinal = new bool[MAXPOKEMONENTRENADOR];
				for (int i =0; i< pokemon.Length;  i++)
					pokemonFinal[i] = pokemon[i];
				
			}
			else if (pokemon.Length > MAXPOKEMONENTRENADOR)
				pokemonFinal = pokemon.SubArray(0, MAXPOKEMONENTRENADOR);
			else pokemonFinal = pokemon;

			pokemon = pokemonFinal;
			pokemonFinal = new bool[BITSBYTE];
			for (int i = 2,j=0; j<pokemon.Length; i++,j++)
				pokemonFinal[i] = pokemon[j];

			numPokemonShiny = pokemonFinal.ToByte();
			
			return new ComandosScript.SetVar(VariableShinytzer,(int)(Hex)(numPokemonShiny.ToString().PadLeft(2, '0') + entrenador.EquipoPokemon.NumeroPokemon.ToString().PadLeft(2,'0')));
		}
		public static string SimpleScriptBattleShinyTrainerXSE(int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			return SimpleScriptBattleShinyTrainer(indexEntrenador,entrenador,pokemon).GetDeclaracionXSE(true,"ScriptTrainer"+entrenador.Nombre+"Shiny");
		}
		public static Script SimpleScriptBattleShinyTrainer(int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			StringBuilder strScript=new StringBuilder();
			Script scriptBattleShiny=new Script();
			//falta pasar los parametros a C#  y luego hacerlo :)
			strScript.Append("#dynamic 0x800000 \r\n#org @ScriptTrainer");
			strScript.Append(entrenador.Nombre);
			strScript.Append("Shiny\r\nlock\r\nfaceplayer\r\n");
			strScript.Append(ScriptLineaPokemonShinyEntrenador(entrenador,pokemon));
			strScript.Append("\r\ntrainerbattle 0x0 ");
			strScript.Append(((Hex)(indexEntrenador+1)).ByteString);
			strScript.Append(" 0x0 0x0 0x0\r\nrelease\r\nend");
			return scriptBattleShiny;
		}
	}
}
