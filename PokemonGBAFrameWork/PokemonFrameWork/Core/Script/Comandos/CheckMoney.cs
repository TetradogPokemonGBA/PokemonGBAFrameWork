/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckMoney.
	/// </summary>
	public class CheckMoney:Comando
	{
		public const byte ID=0x92;
		public const int SIZE=6;
        DWord dineroAComprobar;
		Byte comprobarEjecucionComando;
		
		public CheckMoney(DWord dineroAComprobar,Byte comprobarEjecucionComando)
		{
			DineroAComprobar=dineroAComprobar;
			ComprobarEjecucionComando=comprobarEjecucionComando;
			
		}
		
		public CheckMoney(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckMoney(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckMoney(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Comprueba si el jugador tiene el dinero especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "CheckMoney";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public DWord DineroAComprobar
		{
			get{ return dineroAComprobar;}
			set{dineroAComprobar=value;}
		}
		public Byte ComprobarEjecucionComando
		{
			get{ return comprobarEjecucionComando;}
			set{comprobarEjecucionComando=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{dineroAComprobar,comprobarEjecucionComando};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			dineroAComprobar=new DWord(ptrRom+offsetComando);
			offsetComando+=DWord.LENGTH;
			comprobarEjecucionComando=*(ptrRom+offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			DWord.SetDWord(ptrRomPosicionado,DineroAComprobar);
				ptrRomPosicionado+=DWord.LENGTH;
			*ptrRomPosicionado=comprobarEjecucionComando;
			++ptrRomPosicionado;
			
		}
	}
}
