/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualMsgBox.
	/// </summary>
	public class VirtualMsgBox:Comando
	{
		public const byte ID = 0xBD;
		public const int SIZE = 5;
		OffsetRom text;
 
		public VirtualMsgBox(OffsetRom text)
		{
			Text = text;
 
		}
   
		public VirtualMsgBox(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public VirtualMsgBox(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe VirtualMsgBox(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara un puntero para ser usado en una caja de texto.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualMsgBox";
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
			OffsetRom.Set(ptrRomPosicionado, text);
		}
	}
}
