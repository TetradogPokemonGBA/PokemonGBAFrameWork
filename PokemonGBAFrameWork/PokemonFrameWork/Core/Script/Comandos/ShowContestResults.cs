/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ShowContestResults.
	/// </summary>
	public class ShowContestResults:Comando
	{
		public const byte ID=0x8D;
		public const int SIZE=1;
		
		public ShowContestResults()
		{
			
		}
		
		public ShowContestResults(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ShowContestResults(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ShowContestResults(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Shows pokémon contest results.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowContestResults";
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
