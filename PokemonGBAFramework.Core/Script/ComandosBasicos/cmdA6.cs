/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

		public CmdA6() { }
        public CmdA6(Byte unknow)
		{
			Unknow = unknow;
 
		}
   
		public CmdA6(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CmdA6(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CmdA6(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Byte Unknow { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Unknow };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Unknow = ptrRom[offsetComando];	
        }
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1] = Unknow;
			return data;
		}
	}
}
