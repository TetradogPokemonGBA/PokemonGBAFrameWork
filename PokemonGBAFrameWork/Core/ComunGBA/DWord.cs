/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 16/03/2017
 * Time: 21:31
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of DWord.
	/// </summary>
	public static class DWord
	{
		public const int LENGTH=4;
		
		
		public static void SetDWord(RomGba rom, int offset, int dWord)
		{
			SetDWord(rom.Data.Bytes, offset, dWord);
		}
		
		public  static void SetDWord(byte[] rom, int offset, int dWord)
		{
			if (offset < 0 || offset + LENGTH> rom.Length)
				throw new ArgumentOutOfRangeException();
			unsafe{
				fixed(byte* ptrRom=rom)
					SetDWord(ptrRom,offset,dWord);
			
			}

		}
		public unsafe static void SetDWord(byte* ptrDatos,int offset, int dWord)
		{
			SetDWord(ptrDatos+offset,dWord);
		}
		public unsafe static void SetDWord(byte* ptrDatosPosicionados, int dWord)
		{
			byte[] bytesDWord=Serializar.GetBytes(dWord);
			fixed(byte* ptrBytesDWord=bytesDWord)
				MetodosUnsafe.WriteBytes(ptrDatosPosicionados,ptrBytesDWord,LENGTH);

		}

		public static int GetDWord(RomGba rom, int offset)
		{
			return GetDWord(rom.Data.Bytes, offset);
		}
		public static int GetDWord(byte[] bytes,int offsetDWord)
		{
			if (offsetDWord + LENGTH > bytes.Length)
				throw new ArgumentOutOfRangeException();
			int result;
			unsafe{
				fixed(byte* ptrBytes=bytes)
					result=GetDWord(ptrBytes+offsetDWord);
			}
			return result;
		}
		public unsafe static int GetDWord(byte* ptrDatos,int offset)
		{
			return GetDWord(ptrDatos+offset);
		}
		public unsafe static int GetDWord(byte* ptrBytesPosicionado)
		{
			byte[] bytesDWord=MetodosUnsafe.ReadBytes(ptrBytesPosicionado,LENGTH);
			return Serializar.ToInt(bytesDWord);
		}
	}
}
