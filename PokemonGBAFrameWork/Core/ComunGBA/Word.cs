/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 14:59
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
	/// Description of Word.
	/// </summary>
	public static class Word
	{
		public const int LENGTH=2;
		
		
		public static void SetWord(RomGba rom, int offset, short word)
		{
			SetWord(rom.Data.Bytes, offset, word);
		}
		
		public  static void SetWord(byte[] rom, int offset, short word)
		{
			if (offset < 0 || offset + LENGTH> rom.Length)
				throw new ArgumentOutOfRangeException();
			int zonaWord = (int)offset;
			unsafe
			{
				byte* ptrDatos;
				fixed (byte* ptDatos = rom)
				{
					ptrDatos = ptDatos;
					ptrDatos += zonaWord;
					
					*ptrDatos = Convert.ToByte((word & 0xff));
					
					ptrDatos++;
					
					*ptrDatos = Convert.ToByte(((word >> 8) & 0xff));
					
				}
			}

		}

		public static short GetWord(RomGba rom, int offsetWord)
		{
			return GetWord(rom.Data.Bytes, offsetWord);
		}
		public static short GetWord(byte[] bytes, int offsetWord=0)
		{
			return GetWordOrDWord(bytes, offsetWord);
		}
		
		static short GetWordOrDWord(byte[] bytes,int offsetWord)
		{
			if (offsetWord + LENGTH > bytes.Length)
				throw new ArgumentOutOfRangeException();
			byte[] bytesWord=bytes.SubArray(offsetWord,LENGTH);
			return Serializar.ToShort(bytesWord);
		}
	}
}
