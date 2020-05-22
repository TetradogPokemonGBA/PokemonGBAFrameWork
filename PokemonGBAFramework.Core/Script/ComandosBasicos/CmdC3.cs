/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CmdC3.
	/// </summary>
	public class CmdC3:Comando
	{
		public const byte ID = 0xC3;
		public new const int SIZE = Comando.SIZE+1;
        public const string NOMBRE = "CmdC3";
        public const string DESCRIPCION= "Bajo investigación";

		public CmdC3() { }
        public CmdC3(Byte unknow)
		{
			Unknow = unknow;
 
		}
   
		public CmdC3(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CmdC3(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CmdC3(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

        public override System.Collections.Generic.IList<object> GetParams()
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
