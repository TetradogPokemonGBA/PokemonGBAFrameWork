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
using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//sacado de aqui http://wahackforo.com/t-25561/fr-en-proceso-sistema-mt-bw
namespace PokemonGBAFrameWork
{
	public static class SistemaMTBW
	{
		//se tienen que editar 2 bytes de dos sitios diferentes cambiar A9 por 90  y al lado tiene que haber 20; si cancelas el aprendizaje pierdes la MT si no no...tiene que acabarse la investigacion hecha por el autor...
		public static readonly Variable VariableOffsetMTBW1;
		public static readonly Variable VariableOffsetMTBW2;
		public static readonly Variable VariableOffsetMTBW3;
		public static readonly Variable VariableOffsetMTBWB;
		public static readonly Variable VariableASMOffset1;
		public static readonly Variable VariableASMOffset2;
		public static readonly Variable VariableOffsetASM;
		
		static readonly byte[] NewSystem;//de momento son estos bytes...si cambian lo haré generico...
		static readonly byte[] NewSystemASM;
		static readonly byte[] OldSystem;
		static readonly byte[] OldSystemASM;//quitar el asm
		const byte NEWSYSTEMPARTB=0xE0;//falta parte ASM para que se vea bien...
		const byte OLDSYSTEMPARTB=0xD1;
		static SistemaMTBW()
		{
			VariableOffsetMTBW1 = new Variable("OffsetMTBW1");
			VariableOffsetMTBW2 = new Variable("OffsetMTBW2");
			VariableOffsetMTBW3 = new Variable("OffsetMTBW3");
			VariableOffsetMTBWB=new Variable("OffsetMTBWB");
			
			VariableASMOffset1=new Variable("OffsetASM1");
			VariableASMOffset2=new Variable("OffsetASM2");
			VariableOffsetASM=new Variable("OffsetASM");

			NewSystem = new byte[] { 0x90, 0x20 };
			OldSystem = new byte[] { 0xA9, 0x20 };
			OldSystemASM=new byte[]{ 0x38, 0x1C, 0x08, 0x21, 0x22, 0x1C, 0x01, 0xF0 };
			NewSystemASM=new byte[]{ 0x00, 0x48, 0x00, 0x47 };
			VariableOffsetMTBW1.Add(EdicionPokemon.VerdeHojaUsa, 0x124E78, 0x124EF0);
			VariableOffsetMTBW1.Add(EdicionPokemon.RojoFuegoUsa, 0x124EA0, 0x124F18);
			VariableOffsetMTBW1.Add(EdicionPokemon.VerdeHojaEsp, 0x124FF4);
			VariableOffsetMTBW1.Add(EdicionPokemon.RojoFuegoEsp, 0x12501C);


			VariableOffsetMTBW2.Add(EdicionPokemon.VerdeHojaUsa,0x124F44 ,0x124FBC);
			VariableOffsetMTBW2.Add(EdicionPokemon.RojoFuegoUsa, 0x124F6C, 0x124FE4);
			VariableOffsetMTBW2.Add(EdicionPokemon.VerdeHojaEsp, 0x1250C0);
			VariableOffsetMTBW2.Add(EdicionPokemon.RojoFuegoEsp, 0x1250E8);
			
			VariableOffsetMTBW3.Add(EdicionPokemon.VerdeHojaUsa,0x125C4C ,0x125CC4);
			VariableOffsetMTBW3.Add(EdicionPokemon.RojoFuegoUsa, 0x125C74, 0x124CEC);
			VariableOffsetMTBW3.Add(EdicionPokemon.VerdeHojaEsp, 0x125DC8);
			VariableOffsetMTBW3.Add(EdicionPokemon.RojoFuegoEsp, 0x125DF0);
			//por comprobar
			VariableOffsetMTBWB.Add(EdicionPokemon.VerdeHojaUsa,0x131E7D ,0x131EF5);
			VariableOffsetMTBWB.Add(EdicionPokemon.RojoFuegoUsa, 0x131EA5, 0x131F1D);
			VariableOffsetMTBWB.Add(EdicionPokemon.VerdeHojaEsp, 0x131FF9);
			VariableOffsetMTBWB.Add(EdicionPokemon.RojoFuegoEsp, 0x132021);
			
			VariableASMOffset1.Add(EdicionPokemon.VerdeHojaUsa,0x133588 ,0x133600);
			VariableASMOffset1.Add(EdicionPokemon.RojoFuegoUsa, 0x1335B0, 0x133628);
			VariableASMOffset1.Add(EdicionPokemon.VerdeHojaEsp, 0x133750);
			VariableASMOffset1.Add(EdicionPokemon.RojoFuegoEsp, 0x133778);
			
			VariableASMOffset2.Add(EdicionPokemon.VerdeHojaUsa,0x131EB6 ,0x131F2E);
			VariableASMOffset2.Add(EdicionPokemon.RojoFuegoUsa, 0x131EDE, 0x131F56);
			VariableASMOffset2.Add(EdicionPokemon.VerdeHojaEsp, 0x132032);
			VariableASMOffset2.Add(EdicionPokemon.RojoFuegoEsp, 0x13205A);
			
			VariableOffsetASM.Add(EdicionPokemon.VerdeHojaUsa,0x131ECC ,0x131F44);
			VariableOffsetASM.Add(EdicionPokemon.RojoFuegoUsa, 0x131EF4, 0x131F6C);
			VariableOffsetASM.Add(EdicionPokemon.VerdeHojaEsp, 0x132048);
			VariableOffsetASM.Add(EdicionPokemon.RojoFuegoEsp, 0x132070);
			
		}

		public static bool EstaActivado(RomData rom)
		{
			return EstaActivado(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static bool EstaActivado(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return rom.Data[GetOffset(edicion,compilacion, VariableOffsetMTBW1)] == NewSystem[0] && rom.Data[GetOffset( edicion, compilacion, VariableOffsetMTBW2)] == NewSystem[0]&& rom.Data[GetOffset( edicion, compilacion, VariableOffsetMTBW3)] == NewSystem[0]&& rom.Data[GetOffset( edicion, compilacion, VariableOffsetMTBWB)] == NEWSYSTEMPARTB;
		}


		public static void Activar(RomData rom)
		{
			Activar(rom.Rom, rom.Edicion, rom.Compilacion);
		}

		public static void Activar(RomGba rom, EdicionPokemon edicion,Compilacion compilacion)
		{
			int offsetASM;
			int offsetNewSystemASM;
			if(!EstaActivado(rom,edicion,compilacion))
			{
				rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW1), NewSystem);
				rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW2), NewSystem);
				rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW3), NewSystem);
				rom.Data[GetOffset(edicion,compilacion,VariableOffsetMTBWB)]=NEWSYSTEMPARTB;
				offsetASM=rom.Data.SetArray(GetASMOn(edicion,compilacion).AsmBinary);
				offsetNewSystemASM=GetOffset(edicion,compilacion,VariableOffsetASM);
				rom.Data.SetArray(offsetNewSystemASM,NewSystemASM);
				rom.Data.SetArray(offsetNewSystemASM+NewSystemASM.Length,new OffsetRom(offsetASM+1).BytesPointer);
			}
		}
		public static void Desactivar(RomData rom)
		{
			Desactivar(rom.Rom, rom.Edicion, rom.Compilacion);
		}
		public static void Desactivar(RomGba rom, EdicionPokemon edicion, Compilacion compilacion)
		{
			int offsetASM;
			int offsetPointerASM;
			if(EstaActivado(rom,edicion,compilacion))
			{
				rom.Data.SetArray(GetOffset( edicion, compilacion,VariableOffsetMTBW1), OldSystem);
				rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW2), OldSystem);
				rom.Data.SetArray(GetOffset( edicion, compilacion, VariableOffsetMTBW3), OldSystem);
				rom.Data[GetOffset(edicion,compilacion,VariableOffsetMTBWB)]=OLDSYSTEMPARTB;
				offsetPointerASM=GetOffset(edicion,compilacion,VariableOffsetASM);
				offsetASM=new OffsetRom(rom,offsetPointerASM+4).Offset-1;
				//quito el codigo ASM
				rom.Data.Remove(offsetASM,GetASMOn(edicion,compilacion).AsmBinary.Length);
				//pongo los bytes antiguos :)
				rom.Data.SetArray(offsetPointerASM,OldSystemASM);
			}
		}

		public static ASM GetASMOn(EdicionPokemon edicion,Compilacion compilacion)
		{
			StringBuilder strASM=new StringBuilder(Resources.ASMMTBW);
			strASM.Replace("OFFSET1",(Hex)GetOffset(edicion,compilacion,VariableASMOffset1));
			strASM.Replace("OFFSET2",(Hex)GetOffset(edicion,compilacion,VariableASMOffset2));
			return ASM.Compilar(strASM.ToString());
		}
		private static int GetOffset(EdicionPokemon edicion, Compilacion compilacion, Variable offsetMTBW)
		{
			return Variable.GetVariable(offsetMTBW,  edicion, compilacion);
		}
		
	}
}
