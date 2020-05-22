/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetCatchLocation.
	/// </summary>
	public class SetCatchLocation:Comando
	{
		public const byte ID = 0xD2;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE= "SetCatchLocation";
		public const string DESCRIPCION = "Cambia el lugar donde se ha capturado un pokemon del equipo.";
		public SetCatchLocation() { }

		public SetCatchLocation(Word pokemon, Word catchLocation)
		{
			Pokemon = pokemon;
			CatchLocation = catchLocation;
 
		}
   
		public SetCatchLocation(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetCatchLocation(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetCatchLocation(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Word Pokemon { get; set; }
		public Word CatchLocation { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Pokemon, CatchLocation };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CatchLocation = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];

			data[0]=IdComando;
			Word.SetData(data,1, Pokemon);
			Word.SetData(data,3, CatchLocation);

			return data;
		}
	}
}
