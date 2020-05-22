/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualLoadPointer.
	/// </summary>
	public class VirtualLoadPointer:Comando,IString
	{
		public const byte ID = 0xBE;
		public new const int SIZE = Comando.SIZE+OffsetRom.LENGTH;
		public const string NOMBRE = "VirtualLoadPointer";
		public const string DESCRIPCION = "Prepara un pointer para un dialogo de texto.";



		public VirtualLoadPointer() { }
		public VirtualLoadPointer(BloqueString text)
		{
			Texto = text;
 
		}
   
		public VirtualLoadPointer(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public VirtualLoadPointer(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe VirtualLoadPointer(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public BloqueString Texto { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Texto };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Texto =BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			OffsetRom.Set(data,1,new OffsetRom(Texto.IdUnicoTemp));

			return data;
		}
	}
}
