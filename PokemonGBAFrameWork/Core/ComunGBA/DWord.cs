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
			int zonaWord = (int)offset;
			byte[] bytesDWord=Serializar.GetBytes(dWord);
			rom.SetArray(offset,bytesDWord);

		}

		public static int GetDWord(RomGba rom, int offset)
		{
			return GetDWord(rom.Data.Bytes, offset);
		}
		public static int GetDWord(byte[] bytes,int offsetDWord)
		{
			if (offsetDWord + LENGTH > bytes.Length)
				throw new ArgumentOutOfRangeException();
			byte[] bytesDWord=bytes.SubArray(offsetDWord,LENGTH);
			return Serializar.ToInt(bytesDWord);
		}
	}
}
