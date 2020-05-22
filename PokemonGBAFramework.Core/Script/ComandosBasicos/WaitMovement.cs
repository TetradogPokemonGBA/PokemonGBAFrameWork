/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitMovement.
	/// </summary>
	public class WaitMovement : Comando
	{
		public const byte JUGADOR = 0xFF;
		public const byte ID = 0x51;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE = "WaitMovement";
		public const string DESCRIPCION = "Espera a que el ApplyMovement acabe";

		public WaitMovement():this(JUGADOR)
		{

		}
		public WaitMovement(Word persona) => Persona = persona;
		public WaitMovement(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitMovement(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitMovement(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Word Persona { get; set; }
		protected override unsafe void CargarCamando(ScriptAndASMManager scriptAndASMManager,byte* ptrRom, int offsetComando)
		{
			Persona = new Word(ptrRom, offsetComando);
		}
		protected override IList<object> GetParams()
		{
			return new object[] { Persona };
		}
		public override byte[] GetBytesTemp() 
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			Word.SetData(data,1, Persona);

			return data;
		}

	}
}
