/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of GiveMoney.
	/// </summary>
	public class GiveMoney:Comando
	{
		public const byte ID = 0x90;
		public new const int SIZE = Comando.SIZE+DWord.LENGTH+1;
        public const string NOMBRE = "GiveMoney";
        public const string DESCRIPCION = "Da al jugador algo de dinero.";
        public GiveMoney(DWord dineroADar, Byte comprobarEjecucionComando)
		{
			DineroADar = dineroADar;
			ComprobarEjecucionComando = comprobarEjecucionComando;
			
		}
		
		public GiveMoney(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public GiveMoney(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe GiveMoney(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
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
        public DWord DineroADar { get; set; }
        public Byte ComprobarEjecucionComando { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ DineroADar, ComprobarEjecucionComando };
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			DineroADar = new DWord(ptrRom , offsetComando);
			offsetComando += DWord.LENGTH;
			ComprobarEjecucionComando = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			DWord.SetData(data, , DineroADar);
			ptrRomPosicionado += DWord.LENGTH;
			*ptrRomPosicionado = ComprobarEjecucionComando;
		}
	}
}
