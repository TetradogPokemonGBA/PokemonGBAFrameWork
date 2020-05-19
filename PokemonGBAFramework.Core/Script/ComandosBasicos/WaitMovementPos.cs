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
		public new const int SIZE = 5;
		public const string NOMBRE= "WaitMovementPos";
		public const string DESCRIPCION= "Espera a que acabe el ApplyMovement del personaje especificado y luego pone las coordenadas X/Y";
		Word personajeAEsperar;

		public WaitMovementPos(Word personajeAEsperar, Byte coordenadaX, Byte coordenadaY)
		{
			PersonajeAEsperar = personajeAEsperar;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
 
		}
   
		public WaitMovementPos(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitMovementPos(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitMovementPos(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion=> DESCRIPCION;

		public override byte IdComando=> ID;
		public override string Nombre => NOMBRE;
		public override int Size=> SIZE;
		public Word PersonajeAEsperar {
			get=>personajeAEsperar;
			set{
				if (value == default)
					value = new Word();
				personajeAEsperar = value; }
		}
		public Byte CoordenadaX { get; set; }
		public Byte CoordenadaY { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personajeAEsperar, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personajeAEsperar = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , PersonajeAEsperar);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = CoordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = CoordenadaY;
		}
	}
}
