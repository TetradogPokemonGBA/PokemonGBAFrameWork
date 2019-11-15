/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of PayMoney.
	/// </summary>
	public class PayMoney:Comando
	{
		public const byte ID=0x91;
		public new const int SIZE=Comando.SIZE+DWord.LENGTH+1;
        public const string NOMBRE = "PayMoney";
        public const string DESCRIPCION = "Coge algo de dinero del jugador";
        public PayMoney(DWord dineroACoger,Byte comprobarEjecucionComando)
		{
			DineroACoger=dineroACoger;
			ComprobarEjecucionComando=comprobarEjecucionComando;
			
		}
		
		public PayMoney(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PayMoney(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PayMoney(byte* ptRom,int offset):base(ptRom,offset)
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
        public DWord DineroACoger { get; set; }
        public Byte ComprobarEjecucionComando { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{DineroACoger,ComprobarEjecucionComando};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			DineroACoger=new DWord(ptrRom,offsetComando);
			offsetComando+=DWord.LENGTH;
			ComprobarEjecucionComando=*(ptrRom+offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			DWord.SetData(ptrRomPosicionado,DineroACoger);
			ptrRomPosicionado+=DWord.LENGTH;
			*ptrRomPosicionado=ComprobarEjecucionComando;
			
		}
	}
}
