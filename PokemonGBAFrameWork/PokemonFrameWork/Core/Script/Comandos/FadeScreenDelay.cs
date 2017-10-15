/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of FadeScreenDelay.
	/// </summary>
	public class FadeScreenDelay:Comando
	{
		public const byte ID=0x98;
		public const int SIZE=3;
		Byte efectoDeDesvanecimiento;
		Byte retardo;
		
		public FadeScreenDelay(Byte efectoDeDesvanecimiento,Byte retardo)
		{
			EfectoDeDesvanecimiento=efectoDeDesvanecimiento;
			Retardo=retardo;
			
		}
		
		public FadeScreenDelay(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FadeScreenDelay(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FadeScreenDelay(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Desvanece la pantalla entrando o saliendo, después de un rato";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FadeScreenDelay";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte EfectoDeDesvanecimiento
		{
			get{ return efectoDeDesvanecimiento;}
			set{efectoDeDesvanecimiento=value;}
		}
		public FedeScreen.EfectoFedeScreen Efecto
		{
			get{return (FedeScreen.EfectoFedeScreen)EfectoDeDesvanecimiento;}
			set{efectoDeDesvanecimiento=(byte)value;}
			
		}
		public Byte Retardo
		{
			get{ return retardo;}
			set{retardo=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{efectoDeDesvanecimiento,retardo};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			efectoDeDesvanecimiento=*(ptrRom+offsetComando);
			offsetComando++;
			retardo=*(ptrRom+offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=efectoDeDesvanecimiento;
			++ptrRomPosicionado;
			*ptrRomPosicionado=retardo;
			++ptrRomPosicionado;
			
		}
	}
}
