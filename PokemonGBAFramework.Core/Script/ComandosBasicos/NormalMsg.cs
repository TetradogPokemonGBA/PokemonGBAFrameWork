/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of NormalMsg.
	/// </summary>
	public class NormalMsg:Comando
	{
		public const byte ID = 0xCB;

  
		public NormalMsg()
		{
   
		}
   
		public NormalMsg(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public NormalMsg(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe NormalMsg(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Quita el efecto de SignMsg.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "NormalMsg";
			}
		}

		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.VerdeHoja | Edicion.Pokemon.RojoFuego;
		}
	}
}
