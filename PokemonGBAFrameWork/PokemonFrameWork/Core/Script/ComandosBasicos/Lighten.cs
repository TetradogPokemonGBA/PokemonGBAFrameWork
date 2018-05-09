/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Lighten.
	/// </summary>
	public class Lighten:Comando
	{
		public const byte ID = 0x9A;
		public const int SIZE = 2;
		Byte tamañoDestello;
 
		public Lighten(Byte tamañoDestello)
		{
			TamañoDestello = tamañoDestello;
 
		}
   
		public Lighten(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Lighten(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Lighten(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Llama a la animación destello para alumbrar el área";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Lighten";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte TamañoDestello {
			get{ return tamañoDestello; }
			set{ tamañoDestello = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ tamañoDestello };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			tamañoDestello = *(ptrRom + offsetComando); 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = tamañoDestello;
		}
	}
}
