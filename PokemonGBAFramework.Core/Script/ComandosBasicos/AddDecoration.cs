/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of AddDecoration.
	/// </summary>
	public class AddDecoration:Comando
	{
		public const byte ID=0x4B;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
		public const string NOMBRE="AddDecoration";
		public const string DESCRIPCION="Añade un objeto decorativo en el pc del player";

		public AddDecoration() { }
        public AddDecoration(Word decoracion)
		{
			Decoracion=decoracion;
			
		}
		
		public AddDecoration(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public AddDecoration(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe AddDecoration(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
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

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Decoracion};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			Decoracion=new Word(ptrRom,offsetComando);
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0]=IdComando;
			Word.SetData(data, 1, Decoracion);
			return data;
		}
	}
}
