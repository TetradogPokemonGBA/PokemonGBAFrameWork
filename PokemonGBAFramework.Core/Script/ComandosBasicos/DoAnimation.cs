/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of DoAnimation.
	/// </summary>
	public class DoAnimation:Comando
	{
		public const byte ID = 0x9C;
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "DoAnimation";
        public const string DESCRIPCION = "Ejecuta la animación de movimiento especificada.";
        public DoAnimation(Word animacion)
		{
			Animacion = animacion;
 
		}
   
		public DoAnimation(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public DoAnimation(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe DoAnimation(byte* ptRom, int offset)
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
        public Word Animacion { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Animacion };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Animacion = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado, Animacion);
		}
	}
}
