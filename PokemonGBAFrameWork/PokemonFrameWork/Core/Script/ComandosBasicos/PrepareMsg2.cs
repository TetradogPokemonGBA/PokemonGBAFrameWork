/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg2.
	/// </summary>
	public class PrepareMsg2:Comando
	{
		public const byte ID = 0x9B;
		public const int SIZE = 5;
		OffsetRom pointerTexto;
 
		public PrepareMsg2(OffsetRom pointerTexto)
		{
			PointerTexto = pointerTexto;
 
		}
   
		public PrepareMsg2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public PrepareMsg2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe PrepareMsg2(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Bajo investigación...";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PrepareMsg2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom PointerTexto {
			get{ return pointerTexto; }
			set{ pointerTexto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pointerTexto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pointerTexto = new OffsetRom(ptrRom, offsetComando);
 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado, pointerTexto);
		}
	}
}
