/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
 * 
 * Código bajo licencia GNU
 * creditos a hackmew por la rutina <3
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
		//public static readonly ASM RutinaEsmeralda=ASM.Compilar(Resources.ASMShinyzer);
		public static readonly int VariableShinytzer = (int)(Hex)"8003";//mirar de poder cambiarla...hasta que no lo sepa hacer será readonly
		#region Rutina
		//mas adelante poner el codigo fuente y usar la clase ASM
		public static readonly byte[] RutinaComun = {
			0x20, 0x0E, 0x03, 0x28, 0x03, 0xD1, 0x20, 0x48, 0x00, 0x78, 0x00, 0x28,
			0x00, 0xD1, 0x70, 0x47, 0x3C, 0xB5, 0x43, 0x1E, 0x1C, 0x48, 0x03, 0x70,
			0x44, 0x78, 0x00, 0x2C, 0x26, 0xD1, 0x0C, 0x1C, 0x1A, 0x4A, 0x00, 0xF0,
			0x2A, 0xF8, 0x07, 0x23, 0x18, 0x40, 0x03, 0x1C, 0x17, 0x4A, 0x00, 0xF0,
			0x24, 0xF8, 0x05, 0x04, 0x05, 0x43, 0x5D, 0x40, 0x65, 0x40, 0x70, 0xB4,
			0x29, 0x0C, 0x28, 0x04, 0xDB, 0x43, 0x1B, 0x0C, 0x0D, 0x4C, 0x0E, 0x4D,
			0x06, 0x1C, 0x66, 0x43, 0x76, 0x19, 0x32, 0x0C, 0x8A, 0x42, 0x04, 0xD0,
			0x01, 0x30, 0x01, 0x3B, 0x00, 0x2B, 0xF5, 0xD1, 0x04, 0xE0, 0x09, 0x4A,
			0x16, 0x60, 0x62, 0xBC, 0x3D, 0x60, 0x3C, 0xBD, 0x70, 0xBC, 0xD9, 0xE7,
			0x01, 0x25, 0x9D, 0x40, 0x2C, 0x40, 0x00, 0x2C, 0x00, 0xD0, 0x39, 0x68,
			0xF5, 0xE7, 0x10, 0x47, 0x6D, 0x4E, 0xC6, 0x41, 0x73, 0x60, 0x00, 0x00}
		;
		public static readonly byte[] RutinaEsmeralda = {
			0x80, 0x5D, 0x00, 0x03, 0xDE, 0x75, 0x03, 0x02, 0xCD, 0xF5, 0x06, 0x08
		};


		public static readonly byte[] RutinaRojoYVerde = {
			0x00, 0x50, 0x00, 0x03, 0xBE, 0x70, 0x03, 0x02, 0xC9, 0x4E, 0x04, 0x08
		};

		public static readonly byte[] RutinaRubiYZafiro = {
			0x18, 0x48, 0x00, 0x03, 0xCA, 0xE8, 0x02, 0x02, 0x85, 0x0E, 0x04, 0x08
		};


		#endregion
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
			aux=RutinaRubiYZafiro;//Compilo RUBI y SHAPHIRE//los ultimos 4 bytes es un pointer a un texto
			DicRutina.Add(EdicionPokemon.RubiUsa,aux);
			DicRutina.Add(EdicionPokemon.ZafiroUsa,aux);
			DicRutina.Add(EdicionPokemon.RubiEsp,aux);
			DicRutina.Add(EdicionPokemon.ZafiroEsp,aux);
			aux=RutinaRojoYVerde;//Compilo Verde y Rojo
			DicRutina.Add(EdicionPokemon.VerdeHojaUsa,aux);
			DicRutina.Add(EdicionPokemon.RojoFuegoUsa,aux);
			DicRutina.Add(EdicionPokemon.VerdeHojaEsp,aux);
			DicRutina.Add(EdicionPokemon.RojoFuegoEsp,aux);
			
			aux=RutinaEsmeralda;//RutinaEsmeralda.AsmBinary;
			DicRutina.Add(EdicionPokemon.EsmeraldaUsa,aux);
			DicRutina.Add(EdicionPokemon.EsmeraldaEsp,aux);
			
			

		}
		public static bool EstaActivado(RomGba rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return PosicionRutinaShinyzer(rom)>0;
		}
		public static int Activar(RomData rom)
		{
			return Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			int offsetAct;
			if(posicion<0){
				//más adelante añadir la variable :) asi se puede personalizar :D
				//el pointer que apunta a un texto también pero cuando sepa que es :)
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
				
				posicion=rom.Data.SearchEmptyBytes(RutinaComun.Length+DicRutina[edicion].Length);
				rom.Data.SetArray(posicion,RutinaComun);
				rom.Data.SetArray(posicion+RutinaComun.Length,DicRutina[edicion]);//los ultimos 4 bytes son para un pointer que va aun texto...de momento dejo el que hay por defecto :)
				rom.Data.SetArray(offsetAct,new PokemonGBAFrameWork.OffsetRom(posicion+1).BytesPointer);//pongo el pointer a al rutina+1 porque asi es como se ponen los offsets de las rutinas :)
				
			}
			return posicion;

		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			int offsetAct;
			if (posicion > 0)
			{
				rom.Data.Remove(posicion,RutinaComun.Length+DicRutina[edicion].Length);
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
		public  static int PosicionRutinaShinyzer(RomGba rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(RutinaComun);//mirar si esta en la parte común la variable
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
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Lock());
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Faceplayer());
			scriptBattleShiny.ComandosScript.Add(ScriptLineaPokemonShinyEntrenador(entrenador,pokemon));
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Trainerbattle(0x0,(short)(indexEntrenador+1),0x0,new OffsetRom(0),new OffsetRom(0)));
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Release());
			return scriptBattleShiny;
		}
	}
}
