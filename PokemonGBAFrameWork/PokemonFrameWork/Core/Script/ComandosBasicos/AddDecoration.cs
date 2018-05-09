/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of AddDecoration.
	/// </summary>
	public class AddDecoration:Comando
	{
		public const byte ID=0x4B;
		public const int SIZE=3;
		public const string NOMBRE="AddDecoration";
		public const string DESCRIPCION="Añade un objeto decorativo en el pc del player";
		Word decoracion;
		
		public AddDecoration(Word decoracion)
		{
			Decoracion=decoracion;
			
		}
		
		public AddDecoration(RomGba rom,int offset):base(rom,offset)
		{
		}
		public AddDecoration(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe AddDecoration(byte* ptRom,int offset):base(ptRom,offset)
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
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,Decoracion);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
}
