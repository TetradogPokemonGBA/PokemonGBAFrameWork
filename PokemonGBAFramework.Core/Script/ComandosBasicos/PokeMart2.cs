/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PokeMart2.
	/// </summary>
	public class PokeMart2:PokeMart
	{
		public new const byte ID=0x87;
		public new const string NOMBRE= "PokeMart2";
		public new const string DESCRIPCION= "Abre una tienda pokemon con la lista de objetos/precios especificada.";
		public PokeMart2() { }
		public PokeMart2(BloqueTienda listaObjetos):base(listaObjetos)
		{
			
		}
		
		public PokeMart2(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PokeMart2(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PokeMart2(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;
		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;


	}
}
