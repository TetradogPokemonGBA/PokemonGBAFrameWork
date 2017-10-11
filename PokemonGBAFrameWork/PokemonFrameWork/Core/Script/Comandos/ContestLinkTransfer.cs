/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ContestLinkTransfer.
	/// </summary>
	public class ContestLinkTransfer:Comando
	{
		public const byte ID=0x8E;
		public const int SIZE=1;
		
		public ContestLinkTransfer()
		{
			
		}
		
		public ContestLinkTransfer(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ContestLinkTransfer(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ContestLinkTransfer(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Establece una conexión usando el adaptador wireless.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ContestLinkTransfer";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			
		}
	}
}
