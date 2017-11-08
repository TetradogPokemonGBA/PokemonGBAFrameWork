/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg3.
	/// </summary>
	public class PrepareMsg3:Comando
	{
		public const byte ID = 0xDB;
		public const int SIZE = 5;
		OffsetRom texto;
 
		public PrepareMsg3(OffsetRom texto)
		{
			Texto = texto;
 
		}
   
		public PrepareMsg3(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public PrepareMsg3(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe PrepareMsg3(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "bajo investigación";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PrepareMsg3";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom Texto {
			get{ return texto; }
			set{ texto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ texto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			texto =new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado, texto);
		}
	}
}
