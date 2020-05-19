/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of cmd8a.
	/// </summary>
	public class Cmd8A:Comando
	{
		public const byte ID=0x8A;
        public const string NOMBRE = "Cmd8A";
        public const string DESCRIPCION= "[aparentemente no hace nada]";
        public Cmd8A()
		{

			
		}
		
		public Cmd8A(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Cmd8A(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Cmd8A(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
	
	}
}
