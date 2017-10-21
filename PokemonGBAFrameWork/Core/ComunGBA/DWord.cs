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
	public  class DWord:IComparable,IComparable<DWord>
	{
		public const int LENGTH=4;
		
		byte[] dWord;
		public DWord(int dWord)
		{
			this.dWord=Serializar.GetBytes(dWord);
		}
		public DWord(uint dWord)
		{
			this.dWord=Serializar.GetBytes(dWord);
		}
		public DWord(RomData rom,int offsetDWord):this(rom.Rom,offsetDWord)
		{}
		public DWord(RomGba rom,int offsetDWord):this(rom.Data,offsetDWord)
		{}
		public DWord(BloqueBytes rom,int offsetDWord):this(rom.Bytes,offsetDWord)
		{}
		public DWord(byte[] rom,int offsetDWord)
		{
			unsafe{
				fixed(byte* ptrRom=rom)
				{
					dWord=new DWord(ptrRom+offsetDWord).dWord;
				}
				
			}
		}
		public unsafe DWord(byte* ptrRom,int offsetDWord):this(ptrRom+offsetDWord)
		{}
		public unsafe DWord(byte* ptrRomPosicionado)
		{
			dWord=MetodosUnsafe.ReadBytes(ptrRomPosicionado,LENGTH);
		}
		public byte[] Data
		{
			get{return dWord;}
		}
		
		#region IComparable implementation
		public int CompareTo(object obj)
		{
			return CompareTo(obj as DWord);
		}
		#endregion
		#region IComparable implementation
		public int CompareTo(DWord other)
		{
			int compareTo;
			if(other!=null)
			{
				compareTo=(int)dWord.CompareTo(other.dWord);
			}else compareTo=(int)Gabriel.Cat.CompareTo.Inferior;
			
			return compareTo;
		}
		#endregion
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			DWord other = obj as DWord;
			bool isEquals;
			if (other == null)
				isEquals= false;
			else isEquals= this.dWord.ArrayEqual(other.dWord);
			return isEquals;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				if (dWord != null)
					hashCode += 1000000007 * dWord.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(DWord lhs, DWord rhs) {
			bool iguales;
			if (ReferenceEquals(lhs, rhs))
				iguales= true;
			else if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				iguales= false;
			else iguales= lhs.Equals(rhs);
			
			return iguales;
			
		}

		public static bool operator !=(DWord lhs, DWord rhs) {
			return !(lhs == rhs);
		}

		#endregion
		public static implicit operator byte[](DWord dWord)
		{
			return dWord.dWord;
		}
		public static implicit operator uint(DWord dWord)
		{
			return Serializar.ToUInt(dWord.dWord);
		}
		public static implicit operator DWord(uint dWord)
		{
			return new DWord(dWord);
		}
		public static explicit operator int(DWord word)
		{
			return Convert.ToInt32((uint)word);
		}
		public static explicit operator DWord(int word)
		{
			return Convert.ToUInt32((int)word);
		}
		
	}
}
