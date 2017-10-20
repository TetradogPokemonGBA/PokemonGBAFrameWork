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
		
		
		public static void SetWord(RomGba rom, int offset, ushort word)
		{
			SetWord(rom.Data.Bytes, offset, word);
		}
		public unsafe static void SetWord(byte* ptrRom,int offset,ushort word)
		{
			byte* ptComando=ptrRom+offset;
			SetWord(ptComando,word);
		}
		public unsafe static void SetWord(byte* ptrDatosPosicionados,ushort word)
		{

			*ptrDatosPosicionados = Convert.ToByte((word & 0xff));
			
			ptrDatosPosicionados++;
			
			*ptrDatosPosicionados = Convert.ToByte(((word >> 8) & 0xff));
		}
		public  static void SetWord(byte[] rom, int offset, ushort word)
		{
			if (offset < 0 || offset + LENGTH> rom.Length)
				throw new ArgumentOutOfRangeException();
			int zonaWord = (int)offset;
			unsafe
			{
				fixed (byte* ptDatos = rom)
				{
					SetWord(ptDatos,offset,word);
					
				}
			}

		}

		public static ushort GetWord(RomGba rom, int offsetWord)
		{
			return GetWord(rom.Data.Bytes, offsetWord);
		}
		public static ushort GetWord(byte[] bytes, int offsetWord=0)
		{
			return GetWordOrDWord(bytes, offsetWord);
		}
		public unsafe static ushort GetWord(byte* ptrRom,int offset)
		{
			byte* ptComando=ptrRom+offset;
			return GetWord(ptComando);
		}
		public unsafe static ushort GetWord(byte* ptrPosicionado)
		{
			byte[] bytesWord=new byte[Word.LENGTH];
			for(int i=0;i<bytesWord.Length;i++)
			{
				bytesWord[i]=*ptrPosicionado;
				ptrPosicionado++;
			}
			return Word.GetWord(bytesWord);
		}
		static ushort GetWordOrDWord(byte[] bytes,int offsetWord)
		{
			if (offsetWord + LENGTH > bytes.Length)
				throw new ArgumentOutOfRangeException();
			byte[] bytesWord=bytes.SubArray(offsetWord,LENGTH);
			return Serializar.ToUShort(bytesWord);
		}
	}
}
