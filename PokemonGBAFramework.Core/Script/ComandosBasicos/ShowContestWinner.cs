/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowContestWinner.
	/// </summary>
	public class ShowContestWinner:Comando
	{
		public const byte ID=0x77;
		public const int SIZE=2;
		
		public ShowContestWinner()
		{
			
		}
		
		public ShowContestWinner(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ShowContestWinner(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ShowContestWinner(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Muestra al vencedor el concurso.(Solo para la región de Hoenn en la de Kanto actua como nop)";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ShowContestWinner";
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
