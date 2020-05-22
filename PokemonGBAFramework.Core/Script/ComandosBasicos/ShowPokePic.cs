/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowPokePic.
	/// </summary>
	public class ShowPokePic:Comando
	{
		public const byte ID = 0x75;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+1;
		public const string NOMBRE = "ShowPokePic";
		public const string DESCRIPCION = "Muestra un pokemon en una caja";
		public ShowPokePic() { }
		public ShowPokePic(Word pokemon, Byte coordenadaX, Byte coordenadaY)
		{
			Pokemon = pokemon;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public ShowPokePic(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowPokePic(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowPokePic(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Word Pokemon { get; set; }
		public Byte CoordenadaX { get; set; }
		public Byte CoordenadaY { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			Word.SetData(data,1, Pokemon);
			data[3] = CoordenadaX;
			data[4]= CoordenadaY;

			return data;
		}
	}
}
