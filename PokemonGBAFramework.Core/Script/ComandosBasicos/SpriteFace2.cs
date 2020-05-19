/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SpriteFace2.
	/// </summary>
	public class SpriteFace2:Comando
	{
		public const byte ID = 0xAB;
		public const int SIZE = 3;
		Byte personajeVirtual;
		Byte orientacion;
 
		public SpriteFace2(Byte personajeVirtual, Byte orientacion)
		{
			PersonajeVirtual = personajeVirtual;
			Orientacion = orientacion;
 
		}
   
		public SpriteFace2(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteFace2(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteFace2(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia la orientacion del sprite virtual";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SpriteFace2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte PersonajeVirtual {
			get{ return personajeVirtual; }
			set{ personajeVirtual = value; }
		}
		public Byte Orientacion {
			get{ return orientacion; }
			set{ orientacion = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ personajeVirtual, orientacion };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			personajeVirtual = ptrRom[offsetComando];
			offsetComando++;
			orientacion = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado = personajeVirtual;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = orientacion;
		}
	}
}
