/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowContestWinner.
	/// </summary>
	public class ShowContestWinner:Comando
	{
		public const byte ID=0x77;
		public const int SIZE=2;
		
		public ShowContestWinner()
		{
			
		}
		
		public ShowContestWinner(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ShowContestWinner(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ShowContestWinner(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Muestra al vencedor el concurso.(Solo para la región de Hoenn en la de Kanto actua como nop)";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowContestWinner";
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
