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
		public const int SIZE = 4;
		Word pokemon;
		Word catchLocation;
 
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
		public override string Descripcion {
			get {
				return "Cambia el lugar donde se ha capturado un pokemon del equipo.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetCatchLocation";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Pokemon {
			get{ return pokemon; }
			set{ pokemon = value; }
		}
		public Word CatchLocation {
			get{ return catchLocation; }
			set{ catchLocation = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pokemon, catchLocation };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			pokemon = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			catchLocation = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Pokemon);
 
			Word.SetData(data, , CatchLocation);
		}
	}
}
