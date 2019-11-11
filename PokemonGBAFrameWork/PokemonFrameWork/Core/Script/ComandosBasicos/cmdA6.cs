/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of cmdA6.
	/// </summary>
	public class CmdA6:Comando
	{
		public const byte ID = 0xA6;
		public new const int SIZE = Comando.SIZE+1;
        public const string NOMBRE = "CmdA6";
        public const string DESCRIPCION= "Bajo investigación...";

        public CmdA6(Byte unknow)
		{
			Unknow = unknow;
 
		}
   
		public CmdA6(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CmdA6(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CmdA6(byte* ptRom, int offset)
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
        public Byte Unknow { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Unknow };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Unknow = *(ptrRom + offsetComando);	
        }
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Unknow; 
		}
	}
}
