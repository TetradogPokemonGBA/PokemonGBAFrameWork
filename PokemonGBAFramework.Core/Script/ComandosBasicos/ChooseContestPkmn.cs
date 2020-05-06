/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ChooseContestPkmn.
	/// </summary>
	public class ChooseContestPkmn:Comando
	{
		public const byte ID=0x8B;
        public const string NOMBRE = "ChooseContestPkmn";
        public const string DESCRIPCION= "Abre un menu para escoger el pokemon concursante.";
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
			return AbreviacionCanon.AXP|AbreviacionCanon.AXV|AbreviacionCanon.BPE;
		}
	}
}
