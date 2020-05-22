/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
		public PayMoney() { }
        public PayMoney(DWord dineroACoger,Byte comprobarEjecucionComando)
		{
			DineroACoger=dineroACoger;
			ComprobarEjecucionComando=comprobarEjecucionComando;
			
		}
		
		public PayMoney(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PayMoney(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PayMoney(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{DineroACoger,ComprobarEjecucionComando};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			DineroACoger=new DWord(ptrRom,offsetComando);
			offsetComando+=DWord.LENGTH;
			ComprobarEjecucionComando=*(ptrRom+offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			DWord.SetData(data,1,DineroACoger);
			data[5]=ComprobarEjecucionComando;
			return data;
			
		}
	}
}
