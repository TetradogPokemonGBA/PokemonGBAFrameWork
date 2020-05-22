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
	public class VirtualCall:Comando,IEndScript,IScript
	{
		public const byte ID = 0xBA;
		public new const int SIZE = Comando.SIZE+OffsetRom.LENGTH;
		public const string NOMBRE = "VirtualCall";
		public const string DESCRIPCION = "Llama a la función.";

		public VirtualCall() { }
		public VirtualCall(Script funcionPersonalizada)
		{
			Script = funcionPersonalizada;
 
		}
   
		public VirtualCall(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualCall(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualCall(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Script Script { get; set; }

		public virtual bool IsEnd => false;
        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Script };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Script =scriptManager.GetScript(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			OffsetRom.Set(data,1,new OffsetRom(Script.IdUnicoTemp));
			
			return data;
		}
	}
}
