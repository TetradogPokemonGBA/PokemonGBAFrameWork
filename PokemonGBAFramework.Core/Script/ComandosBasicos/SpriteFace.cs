/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteFace.
	/// </summary>
	public class SpriteFace:Comando
	{
		public const byte ID = 0x5B;
		public new const int SIZE = Comando.SIZE + Word.LENGTH + 1;
		public const string NOMBRE = "SpriteFace";
		public const string DESCRIPCION = "Cambia donde mira el sprite";


		public SpriteFace() { }
		public SpriteFace(Word personaje, byte mirandoA)
		{
			Personaje = personaje;
			MirandoA = mirandoA;
 
		}
   
		public SpriteFace(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteFace(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteFace(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Word Personaje { get; set; }
		public byte MirandoA { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Personaje, MirandoA };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Personaje = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			MirandoA = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, Personaje);
			data[3] = MirandoA;

			return data;
		}
	}
}
