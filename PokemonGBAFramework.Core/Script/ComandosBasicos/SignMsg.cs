/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SignMsg.
	/// </summary>
	public class SignMsg:Comando
	{
		public const byte ID = 0xCA;
		public const int SIZE = 1;
  
		public SignMsg()
		{
   
		}
   
		public SignMsg(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SignMsg(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SignMsg(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia la presentación de la caja de dialogo para que parezca un post";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SignMsg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.VerdeHoja | Edicion.Pokemon.RojoFuego;
		}
	}
}
