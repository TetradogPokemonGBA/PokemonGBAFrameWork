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
		
		public SetVirtualAddress(RomGba rom,int offset):base(rom,offset)
		{
		}
		public SetVirtualAddress(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetVirtualAddress(byte* ptRom,int offset):base(ptRom,offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			valor=new DWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			DWord.SetData(ptrRomPosicionado,Valor);
		}
	}
}
