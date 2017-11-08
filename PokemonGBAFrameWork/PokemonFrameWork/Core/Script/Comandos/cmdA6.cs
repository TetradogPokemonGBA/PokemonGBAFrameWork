/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of cmdA6.
	/// </summary>
	public class cmdA6:Comando
	{
		public const byte ID = 0xA6;
		public const int SIZE = 2;
		Byte unknow;
 
		public cmdA6(Byte unknow)
		{
			Unknow = unknow;
 
		}
   
		public cmdA6(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public cmdA6(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe cmdA6(byte* ptRom, int offset)
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
				return "cmdA6";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Unknow {
			get{ return unknow; }
			set{ unknow = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ unknow };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			unknow = *(ptrRom + offsetComando);		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = unknow; 
		}
	}
}
