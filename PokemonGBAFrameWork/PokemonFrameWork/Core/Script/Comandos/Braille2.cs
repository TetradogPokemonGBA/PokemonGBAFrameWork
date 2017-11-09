/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Braille2.
	/// </summary>
	public class Braille2:Comando
	{
		public const byte ID=0xD3;
		public const int SIZE=5;
		OffsetRom brailleData;
		
		public Braille2(OffsetRom brailleData)
		{
			BrailleData=brailleData;
			
		}
		
		public Braille2(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Braille2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Braille2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Establece la variable 0x8004 en un valor basado en el ancho de la cadena en braille en el texto.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Braille2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom BrailleData
		{
			get{ return brailleData;}
			set{brailleData=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{brailleData};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			brailleData=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,brailleData);
		}
	}
}
