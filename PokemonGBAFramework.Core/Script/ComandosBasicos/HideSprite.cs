/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of HideSprite.
	/// </summary>
	public class HideSprite:Comando
	{
		public const byte ID = 0x53;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "HideSprite";
        public const string DESCRIPCION = "Oculta un Sprite";
        public HideSprite(Word personajeAOcultar)
		{
			PersonajeAOcultar = personajeAOcultar;
 
		}
   
		public HideSprite(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public HideSprite(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe HideSprite(ScriptManager scriptManager,byte* ptRom, int offset)
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
        public Word PersonajeAOcultar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAOcultar };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAOcultar = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, , PersonajeAOcultar);
		}
	}
}
