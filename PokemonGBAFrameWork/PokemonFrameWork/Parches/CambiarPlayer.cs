/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/10/2017
 * Hora: 22:36
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of CambiarPlayer.
	/// </summary>
	public static class CambiarPlayer
	{
		static readonly byte[] Part1InsertOffsetRutina = { 0x00, 0x48, 0x00, 0x47 };
		static readonly byte[] RutinaOff = {0x98, 0x46, 0x24, 0x04, 0x24, 0x0C, 0x2D, 0x04};
		public static readonly Variable VarOffsetPonerRutina;
		public static readonly Variable VarOffsetRutina1;
		public static readonly Variable VarOffsetRutina2;
		public static readonly Creditos Creditos;
		public static readonly ASM RutinaKanto;
		const int POSOFFSET1 = 12;
		//empezando por el final
		const int POSOFFSET2 = 4;
		static readonly Word VariableRutina=(Word)0x80F4;
		//empezando por el principio
		const int POSVAR=2;
		static CambiarPlayer()
		{
			Creditos = new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.POKEMONCOMMUNITY], "FBI", "Rutina y post : https://www.pokecommunity.com/showpost.php?p=8620314&postcount=465");
			RutinaKanto = ASM.Compilar(Resources.ASMCambiarPJKanto);
			
			VarOffsetPonerRutina = new Variable("Donde se pone el offset de la rutina compilada");
			
			VarOffsetPonerRutina.Add(EdicionPokemon.RojoFuegoUsa, 0x5CA4C, 0x5CA60);
			VarOffsetPonerRutina.Add(EdicionPokemon.VerdeHojaUsa, 0x5CA4C, 0x5CA60);
			VarOffsetPonerRutina.Add(EdicionPokemon.RojoFuegoEsp, 0x5CB20);
			VarOffsetPonerRutina.Add(EdicionPokemon.VerdeHojaEsp, 0x5CB20);
			
			VarOffsetRutina1 = new Variable("Offset1");
			
			VarOffsetRutina1.Add(EdicionPokemon.RojoFuegoUsa, 0x6E6D1, 0x6E6E5);
			VarOffsetRutina1.Add(EdicionPokemon.VerdeHojaUsa, 0x6E6D1, 0x6E6E5);
			VarOffsetRutina1.Add(EdicionPokemon.RojoFuegoEsp, 0x6E709);
			VarOffsetRutina1.Add(EdicionPokemon.VerdeHojaEsp, 0x6E709);
			
			VarOffsetRutina2 = new Variable("Offset2");
			
			VarOffsetRutina2.Add(EdicionPokemon.RojoFuegoUsa, 0x5CA55, 0x5CA69);
			VarOffsetRutina2.Add(EdicionPokemon.VerdeHojaUsa, 0x6CA55, 0x5CA69);
			VarOffsetRutina2.Add(EdicionPokemon.RojoFuegoEsp, 0x5CB29);
			VarOffsetRutina2.Add(EdicionPokemon.VerdeHojaEsp, 0x5CB29);
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int inicio=Variable.GetVariable(VarOffsetPonerRutina,edicion,compilacion);
			return rom.Data.SearchArray(inicio,inicio+Part1InsertOffsetRutina.Length+1,Part1InsertOffsetRutina)>0;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
            
			int inicio;
			byte[] rutina;
			if(!EstaActivado(rom,edicion,compilacion)){
				rutina=(byte[])RutinaKanto.AsmBinary.Clone();
				Word.SetWord(rutina,2, VariableRutina);
				inicio=Variable.GetVariable(VarOffsetPonerRutina,edicion,compilacion);
				rutina.SetArray(rutina.Length-POSOFFSET1,new OffsetRom(Variable.GetVariable(VarOffsetRutina1,edicion,compilacion)).BytesPointer);
				rutina.SetArray(rutina.Length-POSOFFSET2,new OffsetRom(Variable.GetVariable(VarOffsetRutina2,edicion,compilacion)).BytesPointer);
				rom.Data.SetArray(inicio,Part1InsertOffsetRutina);
				inicio+=Part1InsertOffsetRutina.Length;
				rom.Data.SetArray(inicio,new OffsetRom(rom.Data.SetArray(rutina)+1).BytesPointer);
			}
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int inicio;

			if(EstaActivado(rom,edicion,compilacion)){
				
				inicio=Variable.GetVariable(VarOffsetPonerRutina,edicion,compilacion);
				//Borro la rutina
				rom.Data.Remove(new OffsetRom(rom,inicio+Part1InsertOffsetRutina.Length).Offset-1,RutinaKanto.AsmBinary.Length);
				//pongo los datos como estaban
				rom.Data.SetArray(inicio,RutinaOff);
			}
		}
		public static Script GetSimpleScript(RomData romData,MiniSprite mini)
		{
			return GetSimpleScript(romData.Minis,mini,romData.Edicion,romData.Compilacion);
		}
		public static Script GetSimpleScript(IList<MiniSprite> minis,MiniSprite mini,EdicionPokemon edicion=null,Compilacion compilacion=null)
		{
			int index=minis.IndexOf(mini);
			return GetSimpleScript(index,edicion,compilacion);
			
		}
		public static Script GetSimpleScript(int index,EdicionPokemon edicion=null,Compilacion compilacion=null)
		{
			Script scriptCambiarSprite=new Script();
			scriptCambiarSprite.ComandosScript.Add(new ComandosScript.SetFlag((ushort)0x406));
			scriptCambiarSprite.ComandosScript.Add(new ComandosScript.SetVar(VariableRutina, index));
			if(edicion!=null&&compilacion!=null)
				scriptCambiarSprite.ComandosScript.Add(RefreshMiniPlayer.Comando(edicion,compilacion));//falta probarlo :)
			
			return scriptCambiarSprite;
		}
	}
}
