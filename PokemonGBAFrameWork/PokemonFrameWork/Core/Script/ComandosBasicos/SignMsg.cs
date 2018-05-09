/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SignMsg.
	/// </summary>
	public class SignMsg:Comando
	{
		public const byte ID = 0xCA;
		public const int SIZE = 1;
  
		public SignMsg()
		{
   
		}
   
		public SignMsg(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SignMsg(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SignMsg(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia la presentación de la caja de dialogo para que parezca un post";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SignMsg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPG | AbreviacionCanon.BPR;
		}
	}
}
