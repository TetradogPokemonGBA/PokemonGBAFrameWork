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
		public const int SIZE=5;
		DWord valor;
		
		public SetVirtualAddress(DWord valor)
		{
			Valor=valor;
			
		}
		
		public SetVirtualAddress(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public SetVirtualAddress(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetVirtualAddress(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "jumps to the specified value- value at 0x020375C4 in RAM, continuing execution from there";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetVirtualAddress";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public DWord Valor
		{
			get{ return valor;}
			set{valor=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{valor};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			valor=new DWord(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			DWord.SetData(data, ,Valor);
		}
	}
}
