/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg.
	/// </summary>
	public class PrepareMsg:Comando
	{
		public const byte ID=0x67;
		public const int SIZE=5;
		OffsetRom texto;
		
		public PrepareMsg(OffsetRom texto)
		{
			Texto=texto;
			
		}
		
		public PrepareMsg(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PrepareMsg(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PrepareMsg(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Prepara el texto para mostrarlo enseguida";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PrepareMsg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom Texto
		{
			get{ return texto;}
			set{texto=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{texto};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			texto=new OffsetRom(ptrRom,offsetComando);
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado,texto);
			
		}
	}
}
