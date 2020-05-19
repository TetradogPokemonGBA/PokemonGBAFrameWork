/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of DoAnimation.
	/// </summary>
	public class DoAnimation:Comando
	{
		public const byte ID = 0x9C;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "DoAnimation";
        public const string DESCRIPCION = "Ejecuta la animación de movimiento especificada.";
        public DoAnimation(Word animacion)
		{
			Animacion = animacion;
 
		}
   
		public DoAnimation(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public DoAnimation(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe DoAnimation(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
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
        public Word Animacion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Animacion };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Animacion = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data, , Animacion);
		}
	}
}
