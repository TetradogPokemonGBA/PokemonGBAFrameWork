/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ContestLinkTransfer.
	/// </summary>
	public class ContestLinkTransfer:Comando
	{
		public const byte ID=0x8E;
        public const string NOMBRE = "ContestLinkTransfer";
        public const string DESCRIPCION= "Establece una conexión usando el adaptador wireless.";
        public ContestLinkTransfer()
		{
			
		}
		
		public ContestLinkTransfer(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ContestLinkTransfer(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ContestLinkTransfer(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		
	}
}
