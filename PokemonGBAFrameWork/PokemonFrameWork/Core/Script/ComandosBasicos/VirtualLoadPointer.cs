/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of VirtualLoadPointer.
	/// </summary>
	public class VirtualLoadPointer:Comando
	{
		public const byte ID = 0xBE;
		public const int SIZE = 5;
		OffsetRom text;
 
		public VirtualLoadPointer(OffsetRom text)
		{
			Text = text;
 
		}
   
		public VirtualLoadPointer(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public VirtualLoadPointer(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe VirtualLoadPointer(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara un pointer para un dialogo de texto.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualLoadPointer";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom Text {
			get{ return text; }
			set{ text = value; }
		}

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ text };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			text = new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado, text);
		}
	}
}
