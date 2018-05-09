/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of PokeMart2.
	/// </summary>
	public class PokeMart2:Comando
	{
		public const byte ID=0x87;
		public const int SIZE=5;
		OffsetRom listaObjetos;
		
		public PokeMart2(OffsetRom listaObjetos)
		{
			ListaObjetos=listaObjetos;
			
		}
		
		public PokeMart2(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PokeMart2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PokeMart2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Abre una tienda pokemon con la lista de objetos/precios especificada.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PokeMart2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom ListaObjetos
		{
			get{ return listaObjetos;}
			set{listaObjetos=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{listaObjetos};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			listaObjetos=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,listaObjetos);
		}
	}
}
