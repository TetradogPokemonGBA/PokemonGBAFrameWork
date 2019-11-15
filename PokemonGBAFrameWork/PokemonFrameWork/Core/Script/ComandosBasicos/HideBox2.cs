/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of HideBox2.
	/// </summary>
	public class HideBox2:Comando
	{
		public const byte ID = 0xDA;
        public const string NOMBRE = "HideBox2";
        public const string DESCRIPCION = "Oculta una caja mostrada.";

        public HideBox2()
		{
   
		}
   
		public HideBox2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public HideBox2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe HideBox2(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
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
