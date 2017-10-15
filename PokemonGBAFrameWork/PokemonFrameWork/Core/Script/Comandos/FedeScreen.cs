/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of FedeScreen.
	/// </summary>
	public class FedeScreen:Comando
	{
		public enum EfectoFedeScreen:byte{
			Entrar=0x0,
			Salir=0x1
				
		}
		public const byte ID=0x97;
		public const int SIZE=2;
		Byte efectoDeDesvanecimiento;
		
		public FedeScreen(Byte efectoDeDesvanecimiento)
		{
			EfectoDeDesvanecimiento=efectoDeDesvanecimiento;
			
		}
		
		public FedeScreen(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FedeScreen(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FedeScreen(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Desavanece la pantalla entrando o saliendo";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FedeScreen";
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
		public EfectoFedeScreen Efecto
		{
			get{return (EfectoFedeScreen)EfectoDeDesvanecimiento;}
			set{efectoDeDesvanecimiento=(byte)value;}
			
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{efectoDeDesvanecimiento};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			efectoDeDesvanecimiento=*(ptrRom+offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=efectoDeDesvanecimiento;
			++ptrRomPosicionado;
			
		}
	}
}
