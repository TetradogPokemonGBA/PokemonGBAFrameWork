/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of AddPcItem.
	/// </summary>
	public class AddPcItem:Comando
	{
		public const byte ID=0x49;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE="AddPcItem";
		public const string DESCRIPCION="Añade la cantidad del objeto especificado en el pc del player";

        public AddPcItem(Word objeto,Word cantidad)
		{
			Objeto=objeto;
			Cantidad=cantidad;
			
		}
		
		public AddPcItem(RomGba rom,int offset):base(rom,offset)
		{
		}
		public AddPcItem(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe AddPcItem(byte* ptRom,int offset):base(ptRom,offset)
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
        public Word Objeto { get; set; }
        public Word Cantidad { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Objeto,Cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Objeto=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			Cantidad=new Word(ptrRom,offsetComando);
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
            ptrRomPosicionado += base.Size;
            Word.SetData(ptrRomPosicionado,Objeto);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,Cantidad);
			
		}
	}
}
