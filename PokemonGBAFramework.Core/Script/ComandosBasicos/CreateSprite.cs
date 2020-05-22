/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

		public CreateSprite() { }
        public CreateSprite(Byte spriteAUsar, Byte personajeVirtual, Word coordenadaX, Word coordenadaY, Byte comportamiento, Byte orientacion)
		{
			SpriteAUsar = spriteAUsar;
			PersonajeVirtual = personajeVirtual;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			Comportamiento = comportamiento;
			Orientacion = orientacion;
 
		}
   
		public CreateSprite(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CreateSprite(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CreateSprite(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

        public override System.Collections.Generic.IList<object> GetParams()
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
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			SpriteAUsar = ptrRom[offsetComando];
			offsetComando++;
			PersonajeVirtual = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			Comportamiento = ptrRom[offsetComando];
			offsetComando++;
			Orientacion = ptrRom[offsetComando]; 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1] = SpriteAUsar;
			data[2]= PersonajeVirtual; 
			Word.SetData(data,3, CoordenadaX);
 
			Word.SetData(data,5, CoordenadaY);
 
			data[7] = Comportamiento;
			data[8]= Orientacion;

			return data;
		}
	}
}
