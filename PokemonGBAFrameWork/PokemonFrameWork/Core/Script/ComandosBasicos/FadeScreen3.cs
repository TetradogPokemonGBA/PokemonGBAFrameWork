/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of FadeScreen3.
	/// </summary>
	public class FadeScreen3:Comando
	{
		public const byte ID = 0xDC;
		public new const int SIZE = Comando.SIZE+1;
        public const string NOMBRE = "FadeScreen3";
        public const string DESCRIPCION = "Desvanece la pantalla entrando o saliendo.";

        public FadeScreen3(Byte unknown)
		{
			Unknown = unknown;
 
		}
   
		public FadeScreen3(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public FadeScreen3(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe FadeScreen3(byte* ptRom, int offset)
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
        public Byte Unknown { get; set; }
        protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Unknown };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Unknown = ptrRom[offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Unknown;
		}
	}
}
