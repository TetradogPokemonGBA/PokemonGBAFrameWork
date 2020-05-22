/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of UpdateMoney.
	/// </summary>
	public class UpdateMoney:UpdateCoins
	{
		public new const byte ID = 0x95;
		public new const int SIZE = UpdateCoins.SIZE+1;
		public new const string NOMBRE = "UpdateMoney";
		public new const string DESCRIPCION = "Actualiza el dinero mostrado.";

		public UpdateMoney() { }
		public UpdateMoney(Byte coordenadaX, Byte coordenadaY, Byte comprobarEjecucionComando):base(coordenadaX,coordenadaY)
		{

			ComprobarEjecucionComando = comprobarEjecucionComando;
 
		}
   
		public UpdateMoney(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public UpdateMoney(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe UpdateMoney(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

		
		public Byte ComprobarEjecucionComando { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ CoordenadaX, CoordenadaY, ComprobarEjecucionComando };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(scriptManager, ptrRom, offsetComando);
			ComprobarEjecucionComando = ptrRom[offsetComando+base.ParamsSize];
		}
		public override byte[] GetBytesTemp()
		{
			return base.GetBytesTemp().AddArray(new byte[] { ComprobarEjecucionComando });
		}
	}
}
