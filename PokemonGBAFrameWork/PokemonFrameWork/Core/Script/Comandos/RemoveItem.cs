/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of RemoveItem.
	/// </summary>
	public class RemoveItem:Comando
	{
		public const byte ID=0x45;
		public const int SIZE=5;
		short objetoAQuitar;

		short cantidad;

		
		public RemoveItem(short objetoAQuitar,short cantidad)
		{
			ObjetoAQuitar=objetoAQuitar;

			Cantidad=cantidad;

			
		}
		
		public RemoveItem(RomGba rom,int offset):base(rom,offset)
		{
		}
		public RemoveItem(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe RemoveItem(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Quita la cantidad del objeto especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "RemoveItem";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		

		public short ObjetoAQuitar
		{
			get{ return objetoAQuitar;}
			set{objetoAQuitar=value;}
		}

		public short Cantidad
		{
			get{ return cantidad;}
			set{cantidad=value;}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{objetoAQuitar,cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			objetoAQuitar=Word.GetWord(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			cantidad=Word.GetWord(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,ObjetoAQuitar);

			ptrRomPosicionado+=Word.LENGTH;

			Word.SetWord(ptrRomPosicionado,Cantidad);

			ptrRomPosicionado+=Word.LENGTH;

			
		}
	}
}
