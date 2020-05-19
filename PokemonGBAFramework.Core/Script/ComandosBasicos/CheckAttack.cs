/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckAttack.
	/// </summary>
	public class CheckAttack:Comando
	{
		public const byte ID=0x7C;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;		
		public const string NOMBRE="CheckAttack";
		public const string DESCRIPCION="Comprueba que haya un pokemon en el equipo con un ataque en particular";

		public CheckAttack() { }
        public CheckAttack(Word ataqueAComprobar)
		{
			AtaqueAComprobar=ataqueAComprobar;
			
		}
		
		public CheckAttack(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public CheckAttack(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CheckAttack(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Word AtaqueAComprobar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{AtaqueAComprobar};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			AtaqueAComprobar=new Word(ptrRom,offsetComando);			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1,AtaqueAComprobar);
			return data;
		}
	}
}
