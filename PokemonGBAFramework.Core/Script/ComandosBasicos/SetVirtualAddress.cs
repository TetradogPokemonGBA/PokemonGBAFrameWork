/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetVirtualAddress.
	/// </summary>
	public class SetVirtualAddress:Comando
	{
		public const byte ID=0xB8;
		public new const int SIZE=Comando.SIZE+DWord.LENGTH;
		public const string NOMBRE = "SetVirtualAddress";
		public const string DESCRIPCION = "jumps to the specified value- value at 0x020375C4 in RAM, continuing execution from there";

		public SetVirtualAddress() { }
		public SetVirtualAddress(DWord valor)
		{
			Valor=valor;
			
		}
		
		public SetVirtualAddress(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public SetVirtualAddress(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetVirtualAddress(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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
		public DWord Valor { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Valor};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Valor=new DWord(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			DWord.SetData(data,1,Valor);
			
			return data;
		}
	}
}
