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
        public const string NOMBRE = "ContestLinkTransfer";
        public const string DESCRIPCION= "Establece una conexión usando el adaptador wireless.";
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
                return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}
		
	}
}
