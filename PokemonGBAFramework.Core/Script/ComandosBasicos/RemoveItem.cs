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
		public new const int SIZE=5;
		public const string DESCRIPCION = "Quita la cantidad del objeto especificado";
		public const string NOMBRE = "RemoveItem";

		public RemoveItem() { }

		public RemoveItem(Word objetoAQuitar,Word cantidad)
		{
			ObjetoAQuitar=objetoAQuitar;

			Cantidad=cantidad;

			
		}
		
		public RemoveItem(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public RemoveItem(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe RemoveItem(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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


		public Word ObjetoAQuitar { get; set; }

		public Word Cantidad { get; set; }
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ObjetoAQuitar,Cantidad};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			ObjetoAQuitar=new Word(ptrRom,offsetComando);

			offsetComando+=Word.LENGTH;

			Cantidad=new Word(ptrRom,offsetComando);
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1,ObjetoAQuitar);
			Word.SetData(data,3,Cantidad);
			return data;
			
		}
	}
}
