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
	public class Word:IComparable,IComparable<Word>
	{
		public const int LENGTH=2;
		
		byte[] word;
		public Word(short word)
		{
			this.word=Serializar.GetBytes(word);
		}
		public Word(ushort word)
		{
			this.word=Serializar.GetBytes(word);
		}
		public Word(RomData rom,int offsetWord):this(rom.Rom,offsetWord)
		{}
		public Word(RomGba rom,int offsetWord):this(rom.Data,offsetWord)
		{}
		public Word(BloqueBytes rom,int offsetWord):this(rom.Bytes,offsetWord)
		{}
		public Word(byte[] rom,int offsetWord)
		{
			unsafe{
				fixed(byte* ptrRom=rom)
				{
					word=new Word(ptrRom+offsetWord).word;
				}
				
			}
		}
		public unsafe Word(byte* ptrRom,int offsetWord):this(ptrRom+offsetWord)
		{}
		public unsafe Word(byte* ptrRomPosicionado)
		{
			word=MetodosUnsafe.ReadBytes(ptrRomPosicionado,LENGTH);
		}
		public byte[] Data
		{
			get{return word;}
		}
		
		#region IComparable implementation
		public int CompareTo(object obj)
		{
			return CompareTo(obj as Word);
		}
		#endregion
		#region IComparable implementation
		public int CompareTo(Word other)
		{
			int compareTo;
			if(other!=null)
			{
				compareTo=(int)word.CompareTo(other.word);
			}else compareTo=(int)Gabriel.Cat.CompareTo.Inferior;
			
			return compareTo;
		}
		#endregion
		public override bool Equals(object obj)
		{
			Word other = obj as Word;
			bool isEquals;
			if (other == null)
				isEquals= false;
			else isEquals= this.word.ArrayEqual(other.word);
			return isEquals;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				if (word != null)
					hashCode += 1000000007 * word.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(Word lhs, Word rhs) {
			bool iguales;
			if (ReferenceEquals(lhs, rhs))
				iguales= true;
			else if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				iguales= false;
			else iguales= lhs.Equals(rhs);
			
			return iguales;
			
		}

		public static bool operator !=(Word lhs, Word rhs) {
			return !(lhs == rhs);
		}
		public static implicit operator ushort(Word word)
		{
			return Serializar.ToUShort(word.word);
		}
		public static implicit operator byte[](Word word)
		{
			return word.word;
		}
		public static implicit operator Word(ushort word)
		{
			return new Word(word);
		}
		public static explicit operator short(Word word)
		{
			return Convert.ToInt16((ushort)word);
		}
		public static explicit operator Word(short word)
		{
			return Convert.ToUInt16((short)word);
		}
	}
}
