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
	public class PokeMart:Comando
	{
		public const byte ID = 0x86;
		public const int SIZE = 5;
		OffsetRom listaObjetos;
 
		public PokeMart(OffsetRom listaObjetos)
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
		public override string Descripcion {
			get {
				return "Abre la tienda pokemon con la lista de objetos/precios especificada.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PokeMart";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom ListaObjetos {
			get{ return listaObjetos; }
			set{ listaObjetos = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ listaObjetos };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			listaObjetos = new OffsetRom(ptrRom, offsetComando); 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, listaObjetos);
 
		}
	}
}
