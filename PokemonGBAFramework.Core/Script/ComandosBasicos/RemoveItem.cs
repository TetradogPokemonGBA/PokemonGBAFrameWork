/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of RemoveItem.
	/// </summary>
	public class RemoveItem:Comando
	{
		public const byte ID=0x45;
		public const int SIZE=5;
		Word objetoAQuitar;

		Word cantidad;

		
		public RemoveItem(Word objetoAQuitar,Word cantidad)
		{
			ObjetoAQuitar=objetoAQuitar;

			Cantidad=cantidad;

			
		}
		
		public RemoveItem(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public RemoveItem(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe RemoveItem(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
		

		public Word ObjetoAQuitar
		{
			get{ return objetoAQuitar;}
			set{objetoAQuitar=value;}
		}

		public Word Cantidad
		{
			get{ return cantidad;}
			set{cantidad=value;}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{objetoAQuitar,cantidad};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			objetoAQuitar=new Word(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			cantidad=new Word(ptrRom,offsetComando);
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, ,ObjetoAQuitar);

 

			Word.SetData(data, ,Cantidad);
			
		}
	}
}
