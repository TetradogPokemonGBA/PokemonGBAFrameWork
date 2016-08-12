/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 19:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TratarBytes.
	/// </summary>
	public class BloqueBytes
	{
		Hex offset;
		byte[] bytes;
		public BloqueBytes(Hex offset, byte[] bytesAPoner)
		{
			Bytes = bytesAPoner;
			OffsetInicio = offset;
		}

		public Hex OffsetInicio {
			get {
				
				return offset;
			}
			set {
				if(value<0)throw new ArgumentOutOfRangeException();
				offset = value;
				
			}
		}
		public Hex OffsetFin {
			get{ return OffsetInicio + bytes.Length; }
		}
		public byte[] Bytes {
			get {
				return bytes;
			}
			set {
				bytes = value;
				if (bytes == null)
					bytes = new byte[0];
			}
		}
		public static void SetBytes(RomPokemon rom,Hex offsetInicio,byte[] bytes){
			SetBytes(rom,new BloqueBytes(offsetInicio,bytes));
		}
		public static void SetBytes(RomPokemon rom, BloqueBytes bytes)
		{
			if (bytes.OffsetFin >= rom.Datos.Length)
				throw new ArgumentOutOfRangeException();
			unsafe {			
				fixed(byte* bytesRom=rom.Datos) 
					for (int i = bytes.OffsetInicio, f = bytes.OffsetFin, pos = 0; i < f; i++,pos++)
						bytesRom[i] = bytes.bytes[pos];
				
			}
		}
		public static BloqueBytes GetBytes(RomPokemon rom, Hex offsetInicio, Hex longitud)
		{
		
			if (offsetInicio < 0 || longitud < 0 || rom.Datos.Length < offsetInicio + longitud)
				throw new ArgumentOutOfRangeException();
			byte[] bytes = new byte[longitud];
			unsafe {
				fixed(byte* bytesRom=rom.Datos)
					for (int i = offsetInicio, f = i + longitud, pos = 0; i < f; i++,pos++)
						bytes[pos] = bytesRom[i];
			}
			return new BloqueBytes(offsetInicio,bytes);
		
		}
	}
}
