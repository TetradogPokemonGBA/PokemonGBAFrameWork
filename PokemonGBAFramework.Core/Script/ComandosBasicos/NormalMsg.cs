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
		public const string DESCRIPCION= "Quita el efecto de SignMsg.";
		public const string NOMBRE= "NormalMsg";
		public NormalMsg()
		{
   
		}
   
		public NormalMsg(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public NormalMsg(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe NormalMsg(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;

		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.VerdeHoja | Edicion.Pokemon.RojoFuego;
		}
	}
}
