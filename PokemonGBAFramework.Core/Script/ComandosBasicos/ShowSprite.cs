/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowSprite.
	/// </summary>
	public class ShowSprite:Comando
	{
		public const byte ID = 0x55;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE = "ShowSprite";
		public const string DESCRIPCION = "Muestra un sprite previamente ocultado";

		public ShowSprite() { }
		public ShowSprite(Word personajeAMostrar)
		{
			PersonajeAMostrar = personajeAMostrar;
 
		}
   
		public ShowSprite(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowSprite(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowSprite(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Word PersonajeAMostrar { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAMostrar };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAMostrar = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, PersonajeAMostrar);

			return data;
		}
	}
}
