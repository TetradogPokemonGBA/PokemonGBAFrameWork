/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of AddItem.
	/// </summary>
	public class AddItem:Comando
	{
		public const byte ID=0x44;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE="AddItem";
		public const string DESCRIPCION="Añade la cantidad del objeto especificado";

        public AddItem(Word objetoAAñadir,Word cantidad)
		{
			ObjetoAAñadir=objetoAAñadir;

			Cantidad=cantidad;

			
		}
		
		public AddItem(RomGba rom,int offset):base(rom,offset)
		{
		}
		public AddItem(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe AddItem(byte* ptRom,int offset):base(ptRom,offset)
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


        public Word ObjetoAAñadir { get; set; }

        public Word Cantidad { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ObjetoAAñadir,Cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			ObjetoAAñadir=new Word(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			Cantidad=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
            ptrRomPosicionado += base.Size;
            Word.SetData(ptrRomPosicionado,ObjetoAAñadir);
			

			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,Cantidad);


			
		}
	}
}
