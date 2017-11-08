/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Braille.
	/// </summary>
	public class Braille:Comando
	{
		public const byte ID=0x78;
		public const int SIZE=5;
		OffsetRom brailleData;
		
		public Braille(OffsetRom brailleData)
		{
			BrailleData=brailleData;
			
		}
		
		public Braille(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Braille(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Braille(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Muestra una caja con texto en braille( no soporta '\\l','\\p','\\n')";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Braille";
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
			brailleData=new OffsetRom(ptrRom,offsetComando);//por mirar
			offsetComando+=OffsetRom.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,BrailleData);
			
		}
	}
}
