/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of GiveEgg.
	/// </summary>
	public class GiveEgg:Comando
	{
		public const byte ID=0x7A;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "GiveEgg";
        public const string DESCRIPCION = "Entrega un huevo al entrenador.";
        public GiveEgg(Word pokemon)
		{
			this.Pokemon=pokemon;
		}
		
		public GiveEgg(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public GiveEgg(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe GiveEgg(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}

        public Word Pokemon { get; set; }

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
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Pokemon};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, ,Pokemon);
			
		}
	}
}
