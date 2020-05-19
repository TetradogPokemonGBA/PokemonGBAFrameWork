/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of HideBox2.
	/// </summary>
	public class HideBox2:Comando
	{
		public const byte ID = 0xDA;
        public const string NOMBRE = "HideBox2";
        public const string DESCRIPCION = "Oculta una caja mostrada.";

        public HideBox2()
		{
   
		}
   
		public HideBox2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public HideBox2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe HideBox2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
	
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
	}
}
