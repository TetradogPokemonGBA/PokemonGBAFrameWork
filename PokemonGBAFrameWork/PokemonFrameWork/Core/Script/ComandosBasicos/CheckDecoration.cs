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
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
		public const string NOMBRE="CheckDecoration";
		public const string DESCRIPCION="Comprueba si un objeto decorativo esta en el pc del player";

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
        public Word Decoracion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Decoracion};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Decoracion=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,Decoracion);			
		}
	}
}
