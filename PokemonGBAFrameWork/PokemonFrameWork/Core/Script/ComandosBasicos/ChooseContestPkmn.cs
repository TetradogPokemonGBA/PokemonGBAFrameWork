/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ChooseContestPkmn.
	/// </summary>
	public class ChooseContestPkmn:Comando
	{
		public const byte ID=0x8B;
		public const int SIZE=1;
		
		public ChooseContestPkmn()
		{
			
		}
		
		public ChooseContestPkmn(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ChooseContestPkmn(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ChooseContestPkmn(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Abre un menu para escoger el pokemon concursante.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ChooseContestPkmn";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.AXP|AbreviacionCanon.AXV|AbreviacionCanon.BPE;
		}
	}
}
