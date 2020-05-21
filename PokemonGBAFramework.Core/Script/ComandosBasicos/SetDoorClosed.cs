/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetDoorClosed.
	/// </summary>
	public class SetDoorClosed:Comando
	{
		public const byte ID = 0xAD;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE = "SetDoorClosed";
		public const string DESCRIPCION= "Prepara la puerta para ser cerrada";
		public SetDoorClosed() { }
		public SetDoorClosed(Word coordenadaX, Word coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public SetDoorClosed(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetDoorClosed(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetDoorClosed(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Word CoordenadaX { get; set; }
		public Word CoordenadaY { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];

			data[0]=IdComando;
			Word.SetData(data,1, CoordenadaX);
			Word.SetData(data,3, CoordenadaY);

			return data;
		}
	}
}
