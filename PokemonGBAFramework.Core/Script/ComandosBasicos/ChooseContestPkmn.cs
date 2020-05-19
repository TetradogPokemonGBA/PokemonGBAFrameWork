/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ChooseContestPkmn.
	/// </summary>
	public class ChooseContestPkmn:Comando
	{
		public const byte ID=0x8B;
        public const string NOMBRE = "ChooseContestPkmn";
        public const string DESCRIPCION= "Abre un menu para escoger el pokemon concursante.";
        public ChooseContestPkmn()
		{
			
		}
		
		public ChooseContestPkmn(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ChooseContestPkmn(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ChooseContestPkmn(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi|Edicion.Pokemon.Esmeralda;
		}
	}
}
