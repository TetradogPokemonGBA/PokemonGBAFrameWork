/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of NormalMsg.
	/// </summary>
	public class NormalMsg:Comando
	{
		public const byte ID = 0xCB;

  
		public NormalMsg()
		{
   
		}
   
		public NormalMsg(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public NormalMsg(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe NormalMsg(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Quita el efecto de SignMsg.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "NormalMsg";
			}
		}

		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPG | AbreviacionCanon.BPR;
		}
	}
}
