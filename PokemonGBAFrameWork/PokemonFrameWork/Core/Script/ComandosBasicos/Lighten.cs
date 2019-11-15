/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Lighten.
	/// </summary>
	public class Lighten:Comando
	{
		public const byte ID = 0x9A;
		public new const int SIZE = Comando.SIZE+1;
        public const string NOMBRE = "Lighten";
        public const string DESCRIPCION = "Llama a la animación destello para alumbrar el área";
        public Lighten(Byte tamañoDestello)
		{
			TamañoDestello = tamañoDestello;
 
		}
   
		public Lighten(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Lighten(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Lighten(byte* ptRom, int offset)
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
        public Byte TamañoDestello { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ TamañoDestello };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			TamañoDestello = ptrRom[offsetComando]; 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = TamañoDestello;
		}
	}
}
