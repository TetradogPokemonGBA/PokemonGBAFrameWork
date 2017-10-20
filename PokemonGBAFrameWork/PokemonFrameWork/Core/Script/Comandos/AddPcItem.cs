/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of AddPcItem.
	/// </summary>
	public class AddPcItem:Comando
	{
		public const byte ID=0x49;
		public const int SIZE=5;
		ushort objeto;
		ushort cantidad;
		
		public AddPcItem(ushort objeto,ushort cantidad)
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
				return "Añade la cantidad del objeto especificado en el pc del player";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "AddPcItem";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public ushort Objeto
		{
			get{ return objeto;}
			set{objeto=value;}
		}
		public ushort Cantidad
		{
			get{ return cantidad;}
			set{cantidad=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{objeto,cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			objeto=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			cantidad=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,Objeto);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,Cantidad);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
}
