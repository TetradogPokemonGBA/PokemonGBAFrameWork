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
		public new const int SIZE = Comando.SIZE+1+1;
		public const string NOMBRE = "SpriteFace2";
		public const string DESCRIPCION = "Cambia la orientacion del sprite virtual";

		public SpriteFace2() { }
		public SpriteFace2(Byte personajeVirtual, Byte orientacion)
		{
			PersonajeVirtual = personajeVirtual;
			Orientacion = orientacion;
 
		}
   
		public SpriteFace2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SpriteFace2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SpriteFace2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Byte PersonajeVirtual { get; set; }
		public Byte Orientacion { get; set; }

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(PersonajeVirtual)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Orientacion)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PersonajeVirtual = ptrRom[offsetComando];
			offsetComando++;
			Orientacion = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			data[1] = PersonajeVirtual;
			data[2] = Orientacion;

			return data;
		}
	}
}
