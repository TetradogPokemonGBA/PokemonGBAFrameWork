/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public Lighten(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Lighten(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Lighten(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			TamañoDestello = ptrRom[offsetComando]; 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			*ptrRomPosicionado = TamañoDestello;
		}
	}
}
