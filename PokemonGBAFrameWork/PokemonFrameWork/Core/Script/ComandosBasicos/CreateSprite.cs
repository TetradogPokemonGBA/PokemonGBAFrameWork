/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CreateSprite.
	/// </summary>
	public class CreateSprite:Comando
	{
		public const byte ID = 0xAA;
		public new const int SIZE = Comando.SIZE+1+1+Word.LENGTH+Word.LENGTH+1+1;
        public const string NOMBRE = "CreateSprite";
        public const string DESCRIPCION = "Crea un sprite virtual en el mapa actual.";

        public CreateSprite(Byte spriteAUsar, Byte personajeVirtual, Word coordenadaX, Word coordenadaY, Byte comportamiento, Byte orientacion)
		{
			SpriteAUsar = spriteAUsar;
			PersonajeVirtual = personajeVirtual;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			Comportamiento = comportamiento;
			Orientacion = orientacion;
 
		}
   
		public CreateSprite(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CreateSprite(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CreateSprite(byte* ptRom, int offset)
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
		public override int Size {
			get {
				return SIZE;
			}
		}
        public Byte SpriteAUsar { get; set; }
        public Byte PersonajeVirtual { get; set; }
        public Word CoordenadaX { get; set; }
        public Word CoordenadaY { get; set; }
        public Byte Comportamiento { get; set; }
        public Byte Orientacion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				SpriteAUsar,
				PersonajeVirtual,
				CoordenadaX,
				CoordenadaY,
				Comportamiento,
				Orientacion
			};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			SpriteAUsar = *(ptrRom + offsetComando);
			offsetComando++;
			PersonajeVirtual = *(ptrRom + offsetComando);
			offsetComando++;
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Comportamiento = *(ptrRom + offsetComando);
			offsetComando++;
			Orientacion = *(ptrRom + offsetComando); 
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = SpriteAUsar;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = PersonajeVirtual;
			++ptrRomPosicionado; 
			Word.SetData(ptrRomPosicionado, CoordenadaX);
			ptrRomPosicionado += Word.LENGTH;
			Word.SetData(ptrRomPosicionado, CoordenadaY);
			ptrRomPosicionado += Word.LENGTH;
			*ptrRomPosicionado = Comportamiento;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = Orientacion;
		}
	}
}
