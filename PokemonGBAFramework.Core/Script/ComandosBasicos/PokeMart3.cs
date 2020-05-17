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
	public class PokeMart3:Comando
	{
		public const byte ID=0x88;
		public const int SIZE=5;
		OffsetRom listaObjetos;
		
		public PokeMart3(OffsetRom listaObjetos)
		{
			ListaObjetos=listaObjetos;
			
		}
		
		public PokeMart3(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PokeMart3(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PokeMart3(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Abre una tienda pokemon con la lista objetos/precio especificada.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PokeMart3";
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
			OffsetRom.Set(ptrRomPosicionado,listaObjetos);
			
		}
	}
}
