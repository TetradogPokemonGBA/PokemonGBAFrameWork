/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Creditos FBI por el codigo ASM https://www.pokecommunity.com/showpost.php?p=8509928&postcount=20
 * Fecha: 29/05/2017
 * Hora: 21:11
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of ImpedirCapturaViaScript.
	/// </summary>
	public static class ImpedirCapturaViaScript
	{
		public static int Variable=0x8000;
		public const string PARAREEMPLAZARENLATURINA="VARIABLE";
		public static readonly Variable VariablePosicionLinea;
		public static readonly byte[] RutinaOn={0x01,0x48,0x00,0x47,0x00,0x00 ,0xFF ,0xFF,0xFF,0x08};
		public static readonly byte[] RutinaOff = {	0x0C, 0x48, 0x01, 0x68, 0x80, 0x20, 0x00, 0x02, 0x08, 0x40};

		static ImpedirCapturaViaScript()
		{
			VariablePosicionLinea=new Variable("ImpedirCapturaViaScript,Variable donde dice donde se tiene que poner la linea para que el codigo funcione");
			VariablePosicionLinea.Add(EdicionPokemon.RojoFuegoUsa,0x2D452,0x2D466);
			VariablePosicionLinea.Add(EdicionPokemon.VerdeHojaUsa,0x2D452,0x2D466);
			VariablePosicionLinea.Add(EdicionPokemon.VerdeHojaEsp,0x2D3D6);
			VariablePosicionLinea.Add(EdicionPokemon.RojoFuegoEsp,0x2D3D6);
		}
		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			//pongo los demas parametros para el futuro que sea universal
			return rom.Data.SearchArray(RutinaOff)<0;
		}
		public static void Activar(RomData rom)
		{
			Activar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Activar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			byte[] bytesLinea;
			int offsetLinea=PokemonGBAFrameWork.Variable.GetVariable(VariablePosicionLinea,edicion,compilacion);
			string codigo=Resources.ASMPokemonInCapturable.Replace(PARAREEMPLAZARENLATURINA,(Hex)Variable);
			byte[] bytesRutinaCompilada=ASM.Compilar(codigo).AsmBinary;
			int offsetRutina=rom.Data.SearchArray(bytesRutinaCompilada);
			if(offsetRutina<0)
				offsetRutina=rom.Data.SetArray(bytesRutinaCompilada);
			
			bytesLinea=RutinaOn.Clone() as byte[];
			bytesLinea.SetArray(6,new OffsetRom(offsetRutina).BytesPointer);
			rom.Data.SetArray(offsetLinea,bytesLinea);
			
			
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static void Desactivar(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int IDENTIFICARLARUTINAENROM=9;
			int offsetLinea=PokemonGBAFrameWork.Variable.GetVariable(VariablePosicionLinea,edicion,compilacion);
			byte[] bytesRutinaCompilada=ASM.Compilar(Resources.ASMPokemonInCapturable.Replace(PARAREEMPLAZARENLATURINA,(Hex)Variable)).AsmBinary;
			int offsetRutina=rom.Data.SearchArray(bytesRutinaCompilada.SubArray(IDENTIFICARLARUTINAENROM));
			if(offsetRutina>0)
				rom.Data.Remove(offsetRutina,bytesRutinaCompilada.Length);
			rom.Data.SetArray(offsetLinea,RutinaOff);
		}
		
	}
}
