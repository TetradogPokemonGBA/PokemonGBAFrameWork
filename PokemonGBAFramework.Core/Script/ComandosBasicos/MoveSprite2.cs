/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of MoveSprite2.
	/// </summary>
	public class MoveSprite2:MoveSprite
	{
		public new const byte ID = 0x63;
        public new const string NOMBRE = "MoveSprite2";
        public new const string DESCRIPCION = MoveSprite.DESCRIPCION+" (de forma permanente)";


        public MoveSprite2(Word personaje, Word coordenadaX, Word coordenadaY):base(personaje,coordenadaX,coordenadaY)
		{
	
 
		}
   
		public MoveSprite2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public MoveSprite2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe MoveSprite2(byte* ptRom, int offset)
			: base(ptRom, offset)
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

	
	}
}
