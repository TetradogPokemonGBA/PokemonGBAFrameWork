/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitMovementPos.
	/// </summary>
	public class WaitMovementPos:Comando
	{
		public const byte ID = 0x52;
		public new const int SIZE = Comando.SIZE+Word.LENGTH+1+1;
		public const string NOMBRE= "WaitMovementPos";
		public const string DESCRIPCION= "Espera a que acabe el ApplyMovement del personaje especificado y luego pone las coordenadas X/Y";

		public WaitMovementPos() { }
		public WaitMovementPos(Word personajeAEsperar, Byte coordenadaX, Byte coordenadaY)
		{
			PersonajeAEsperar = personajeAEsperar;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public WaitMovementPos(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitMovementPos(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitMovementPos(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion=> DESCRIPCION;

		public override byte IdComando=> ID;
		public override string Nombre => NOMBRE;
		public override int Size=> SIZE;
		public Word PersonajeAEsperar { get; set; }
		public Byte CoordenadaX { get; set; }
		public Byte CoordenadaY { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ PersonajeAEsperar, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeAEsperar = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, PersonajeAEsperar);
			data[3] = CoordenadaX;
			data[4] = CoordenadaY;

			return data;
		}
	}
}
