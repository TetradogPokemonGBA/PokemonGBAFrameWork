/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//en el XSE esta mal en verdad no tiene parametros source:http://www.sphericalice.com/romhacking/documents/script/index.html#c-B1
namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CmdB1.
	/// </summary>
	public class CmdB1:Comando
	{
		public const byte ID = 0xB1;

        public const string NOMBRE = "CmdB1";
        public const string DESCRPICION= "Bajo investigación,podria hacer igual que nop";
        public CmdB1()
		{

 
		}
   
		public CmdB1(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CmdB1(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CmdB1(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
                return DESCRPICION;
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
		
	}
}
