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
		public static readonly Creditos Creditos;
		//public static readonly ASM RutinaEsmeralda=ASM.Compilar(Resources.ASMShinyzer);
		public static readonly int VariableShinytzer = (int)(Hex)"8003";//mirar de poder cambiarla...hasta que no lo sepa hacer será readonly
		#region Rutina
		//mas adelante poner el codigo fuente y usar la clase ASM
		//de momento pondré los bytes directamente :)

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
		static readonly LlistaOrdenada<EdicionPokemon,LlistaOrdenada<Compilacion,byte[]>> DicRutina;

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
			
			DicRutina=new LlistaOrdenada<EdicionPokemon,LlistaOrdenada<Compilacion, byte[]>>();
			
			
			//se tiene que mirar el codigo fuente...para la variable poderla cambiar
			//falta añadir el codigo fuente de todos...de momento pondré los bytes compilados...mas adelante poner el codigo fuente :)
			//aux=RutinaRubiYZafiro;//Compilo RUBI y SHAPHIRE//los ultimos 4 bytes es un pointer a un texto
			DicRutina.Add(EdicionPokemon.RubiUsa,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.RubiUsa].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRubiYZafiroUsa10);
			DicRutina[EdicionPokemon.RubiUsa].Add(Compilacion.Compilaciones[1],Resources.ShinyzerRubiZafiroUsa11Y12);
			DicRutina[EdicionPokemon.RubiUsa].Add(Compilacion.Compilaciones[2],Resources.ShinyzerRubiZafiroUsa11Y12);
			DicRutina.Add(EdicionPokemon.ZafiroUsa,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.ZafiroUsa].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRubiYZafiroUsa10);
			DicRutina[EdicionPokemon.ZafiroUsa].Add(Compilacion.Compilaciones[1],Resources.ShinyzerRubiZafiroUsa11Y12);
			DicRutina[EdicionPokemon.ZafiroUsa].Add(Compilacion.Compilaciones[2],Resources.ShinyzerRubiZafiroUsa11Y12);
			DicRutina.Add(EdicionPokemon.RubiEsp,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.RubiEsp].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRubiYZafiroEsp);
			DicRutina.Add(EdicionPokemon.ZafiroEsp,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.ZafiroEsp].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRubiYZafiroEsp);
			//aux=RutinaRojoYVerde;//Compilo Verde y Rojo
			DicRutina.Add(EdicionPokemon.VerdeHojaUsa,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.VerdeHojaUsa].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRojoYVerdeUsa10);
			DicRutina[EdicionPokemon.VerdeHojaUsa].Add(Compilacion.Compilaciones[1],Resources.ShinyzerRojoYVerdeUsa11);
			DicRutina.Add(EdicionPokemon.RojoFuegoUsa,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.RojoFuegoUsa].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRojoYVerdeUsa10);
			DicRutina[EdicionPokemon.RojoFuegoUsa].Add(Compilacion.Compilaciones[1],Resources.ShinyzerRojoYVerdeUsa11);
			DicRutina.Add(EdicionPokemon.VerdeHojaEsp,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.VerdeHojaEsp].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRojoYVerdeEsp);
			DicRutina.Add(EdicionPokemon.RojoFuegoEsp,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.RojoFuegoEsp].Add(Compilacion.Compilaciones[0],Resources.ShinyzerRojoYVerdeEsp);
			//aux=RutinaEsmeralda;//RutinaEsmeralda.AsmBinary;
			DicRutina.Add(EdicionPokemon.EsmeraldaUsa,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.EsmeraldaUsa].Add(Compilacion.Compilaciones[0],Resources.ShinyzerEsmeraldaUsa);
			DicRutina.Add(EdicionPokemon.EsmeraldaEsp,new LlistaOrdenada<Compilacion, byte[]>());
			DicRutina[EdicionPokemon.EsmeraldaEsp].Add(Compilacion.Compilaciones[0],Resources.ShinyzerEsmeraldaEsp);
			
			//Creditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.WAHACKFORO],"HackMew","Ha hecho la rutina");
			

		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return PosicionRutinaShinyzer(rom,edicion,compilacion)>0;
		}
		public static int Activar(RomData rom)
		{
			return Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom,edicion,compilacion);
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
				
				posicion=rom.Data.SearchEmptyBytes(DicRutina[edicion][compilacion].Length);
				rom.Data.SetArray(posicion,DicRutina[edicion][compilacion]);//los ultimos 4 bytes son para un pointer que va aun texto...de momento dejo el que hay por defecto :)
				rom.Data.SetArray(offsetAct,new PokemonGBAFrameWork.OffsetRom(posicion+1).BytesPointer);//pongo el pointer a al rutina+1 porque asi es como se ponen los offsets de las rutinas :)
				
			}
			return posicion;

		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int posicion = PosicionRutinaShinyzer(rom,edicion,compilacion);
			int offsetAct;
			if (posicion > 0)
			{
				rom.Data.Remove(posicion,DicRutina[edicion][compilacion].Length);
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
		public  static int PosicionRutinaShinyzer(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(DicRutina[edicion][compilacion]);//mirar si esta en la parte común la variable
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
				pokemonFinal=new bool[MAXPOKEMONENTRENADOR-pokemon.Length].AfegirValors(pokemon).ToArray();
				
			}
			else if (pokemon.Length > MAXPOKEMONENTRENADOR)
				pokemonFinal = pokemon.SubArray(0, MAXPOKEMONENTRENADOR);
			else pokemonFinal = pokemon;

			pokemon = pokemonFinal;
			pokemonFinal = new bool[BITSBYTE];
			for (int i =BITSBYTE-pokemon.Length,j=0; j<pokemon.Length; i++,j++)
				pokemonFinal[i] = pokemon[j];

			numPokemonShiny = pokemonFinal.Reverse().ToArray().ToByte();
			
			return new ComandosScript.SetVar(VariableShinytzer,(int)(Hex)(numPokemonShiny.ToString().PadLeft(2, '0') + entrenador.EquipoPokemon.NumeroPokemon.ToString().PadLeft(2,'0')));
		}
		public static string SimpleScriptBattleShinyTrainerXSE(RomGba rom,int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			return SimpleScriptBattleShinyTrainer(rom,indexEntrenador,entrenador,pokemon).GetDeclaracionXSE(true,"ScriptTrainer"+entrenador.Nombre+"Shiny");
		}
		public static Script SimpleScriptBattleShinyTrainer(RomGba rom,int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			const bool POKEMONSHINY=true;
			short sIndex=(short)(indexEntrenador+1);
			StringBuilder strScript=new StringBuilder();
			Script scriptBattleShiny=new Script();
			
			indexEntrenador++;//le sumo uno porque no empieza por 0 en la gba sino por 1
			
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Lock());
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Faceplayer());
			if (Gabriel.Cat.Extension.Extension.Contains<bool>(pokemon, POKEMONSHINY)){
				scriptBattleShiny.ComandosScript.Add(ScriptLineaPokemonShinyEntrenador(entrenador, pokemon));
			}
			//scriptBattleShiny.ComandosScript.Add(new ComandosScript.ClearTrainerFlag(sIndex));
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Trainerbattle(0x0,sIndex,0x0,new OffsetRom(0),new OffsetRom(0)));
			scriptBattleShiny.ComandosScript.Add(new ComandosScript.Release());
			return scriptBattleShiny;
		}
	}
}
