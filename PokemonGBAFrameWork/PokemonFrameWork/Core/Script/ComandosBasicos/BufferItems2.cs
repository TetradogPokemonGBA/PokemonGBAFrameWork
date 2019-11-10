/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferItems2.
	/// </summary>
	public class BufferItems2:BufferItems
	{
		public new const byte ID = 0xE2;
		public new const string NOMBRE="BufferItems2";
		public new const string DESCRIPCION="Guarda el nombre en plural del objeto en el buffer especificado";

        public BufferItems2(Byte buffer, Word objetoAGuardar, Word cantidad):base(buffer,objetoAGuardar,cantidad)
		{
			
		}
		
		public BufferItems2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public BufferItems2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe BufferItems2(byte* ptRom, int offset)
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
		public override int Size {
			get {
				return SIZE;
			}
		}

        protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}

	
	}
}
