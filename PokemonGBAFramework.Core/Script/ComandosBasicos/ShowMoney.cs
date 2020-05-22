/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowMoney.
	/// </summary>
	public class ShowMoney:Comando
	{
		public const byte ID = 0x93;
		public new const int SIZE = Comando.SIZE+1+1+1;
		public const string NOMBRE = "ShowMoney";
		public const string DESCRIPCION = "Muestra en las coordenadas especificadas el dinero que tiene el jugador";
		public ShowMoney() { }
		public ShowMoney(Byte coordenadaX, Byte coordenadaY, Byte comprobarEjecucionComando)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			ComprobarEjecucionComando = comprobarEjecucionComando;
			
		}
		
		public ShowMoney(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowMoney(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowMoney(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Byte CoordenadaX { get; set; }
		public Byte CoordenadaY { get; set; }
		public Byte ComprobarEjecucionComando { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ CoordenadaX, CoordenadaY, ComprobarEjecucionComando };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
			offsetComando++;
			ComprobarEjecucionComando = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			return new byte[] { IdComando, CoordenadaX, CoordenadaY, ComprobarEjecucionComando };
		}
	}
}
