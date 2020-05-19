/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualCall.
	/// </summary>
	public class VirtualCall:Comando,IEndScript
	{
		public const byte ID = 0xBA;
		public const int SIZE = 5;
		OffsetRom funcionPersonalizada;
 
		public VirtualCall(OffsetRom funcionPersonalizada)
		{
			FuncionPersonalizada = funcionPersonalizada;
 
		}
   
		public VirtualCall(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualCall(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualCall(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Llama a la función.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualCall";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom FuncionPersonalizada {
			get{ return funcionPersonalizada; }
			set{ funcionPersonalizada = value; }
		}

        public bool IsEnd => false;
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ funcionPersonalizada };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			funcionPersonalizada = new OffsetRom(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, funcionPersonalizada);
		}
	}
}
