/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PokeMart.
	/// </summary>
	public class PokeMart:Comando,ITienda
	{
		public const byte ID = 0x86;
		public new const int SIZE = 5;
		public const string NOMBRE= "PokeMart";
		public const string DESCRIPCION= "Abre la tienda pokemon con la lista de objetos/precios especificada.";

		public PokeMart() { }
		public PokeMart(BloqueTienda listaObjetos)
		{
			ListaObjetos = listaObjetos;
 
		}
   
		public PokeMart(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PokeMart(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PokeMart(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public BloqueTienda ListaObjetos { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ ListaObjetos };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			ListaObjetos =new BloqueTienda(ptrRom, new OffsetRom(ptrRom, offsetComando)); 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			OffsetRom.Set(data,1, new OffsetRom(ListaObjetos.IdUnicoTemp));
			return data;
 
		}
	}
}
