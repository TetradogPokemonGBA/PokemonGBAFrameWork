/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckMoney.
	/// </summary>
	public class CheckMoney:Comando
	{
		public const byte ID=0x92;
		public new const int SIZE=Comando.SIZE+DWord.LENGTH+1;
		public const string NOMBRE="CheckMoney";
		public const string DESCRIPCION="Comprueba si el jugador tiene el dinero especificado.";

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
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
        public DWord DineroAComprobar { get; set; }
        public Byte ComprobarEjecucionComando { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{DineroAComprobar,ComprobarEjecucionComando};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			DineroAComprobar=new DWord(ptrRom,offsetComando);
			offsetComando+=DWord.LENGTH;
			ComprobarEjecucionComando=*(ptrRom+offsetComando);
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			DWord.SetData(ptrRomPosicionado,DineroAComprobar);
		    ptrRomPosicionado+=DWord.LENGTH;
			*ptrRomPosicionado=ComprobarEjecucionComando;
			
		}
	}
}
