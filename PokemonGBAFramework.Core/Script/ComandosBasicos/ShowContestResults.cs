/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowContestResults.
	/// </summary>
	public class ShowContestResults:Comando
	{
		public const byte ID=0x8D;
		public const int SIZE=1;
		
		public ShowContestResults()
		{
			
		}
		
		public ShowContestResults(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ShowContestResults(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ShowContestResults(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Shows pokémon contest results.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowContestResults";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi|Edicion.Pokemon.Esmeralda;
		}
		
	}
}
