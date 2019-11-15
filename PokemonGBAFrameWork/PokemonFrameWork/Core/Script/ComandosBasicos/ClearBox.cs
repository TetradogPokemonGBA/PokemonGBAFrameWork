/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ClearBox.
	/// </summary>
	public class ClearBox:Comando
	{
		public const byte ID = 0x74;
		public new const int SIZE = Comando.SIZE+1+1+1+1;
        public const string NOMBRE = "ClearBox";
        public const string DESCRIPCION= "Vacia una parte de una caja personalizada";

        public ClearBox(Byte posicionX, Byte posicionY, Byte ancho, Byte alto)
		{
			PosicionX = posicionX;
			PosicionY = posicionY;
			Ancho = ancho;
			Alto = alto;
 
		}
   
		public ClearBox(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public ClearBox(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe ClearBox(byte* ptRom, int offset)
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
        public Byte PosicionX { get; set; }
        public Byte PosicionY { get; set; }
        public Byte Ancho { get; set; }
        public Byte Alto { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PosicionX, PosicionY, Ancho, Alto };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			PosicionX = ptrRom[offsetComando];
			offsetComando++;
			PosicionY = ptrRom[offsetComando];
			offsetComando++;
			Ancho = ptrRom[offsetComando];
			offsetComando++;
			Alto = ptrRom[offsetComando];

			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = PosicionX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = PosicionY;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = Ancho;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = Alto;

		}
	}
}
