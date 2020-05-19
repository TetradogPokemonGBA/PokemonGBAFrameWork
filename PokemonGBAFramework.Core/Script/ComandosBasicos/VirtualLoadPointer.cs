/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public VirtualLoadPointer(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualLoadPointer(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualLoadPointer(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			text = new OffsetRom(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, text);
		}
	}
}
