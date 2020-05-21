/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PokeMart3.
	/// </summary>
	public class PokeMart3:PokeMart
	{
		public new const byte ID=0x88;
		public new const string DESCRIPCION= "Abre una tienda pokemon con la lista objetos/precio especificada.";
		public new const string NOMBRE= "PokeMart3";
		public PokeMart3() { }
		public PokeMart3(BloqueTienda listaObjetos):base(listaObjetos)
		{
			
		}
		
		public PokeMart3(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PokeMart3(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PokeMart3(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;


	}
}
