/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckDecoration.
	/// </summary>
	public class CheckDecoration:Comando
	{
		public const byte ID=0x4E;
		public const int SIZE=3;
		Word decoracion;
		
		public CheckDecoration(Word decoracion)
		{
			Decoracion=decoracion;
			
		}
		
		public CheckDecoration(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckDecoration(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Comprueba si un objeto decorativo esta en el pc del player";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "CheckDecoration";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Decoracion
		{
			get{ return decoracion;}
			set{decoracion=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{decoracion};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			decoracion=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,Decoracion);			
		}
	}
}
