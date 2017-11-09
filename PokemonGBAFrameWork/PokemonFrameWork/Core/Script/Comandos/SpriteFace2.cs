/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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
   
		public SpriteFace2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SpriteFace2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SpriteFace2(byte* ptRom, int offset)
			: base(ptRom, offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			personajeVirtual = *(ptrRom + offsetComando);
			offsetComando++;
			orientacion = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = personajeVirtual;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = orientacion;
		}
	}
}
