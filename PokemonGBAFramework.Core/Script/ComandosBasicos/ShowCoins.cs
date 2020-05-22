/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of ShowCoins.
	/// </summary>
	public class ShowCoins:Comando
	{
		public const byte ID = 0xC0;
		public new const int SIZE = Comando.SIZE+1+1;
		public const string NOMBRE = "ShowCoins";
		public const string DESCRIPCION = "Muestra el numero de fichas en las coordenadas especificadas.";
		public ShowCoins() { }

		public ShowCoins(Byte coordenadaX, Byte coordenadaY)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			
		}
		
		public ShowCoins(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public ShowCoins(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe ShowCoins(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			return new byte[] { IdComando, CoordenadaX, CoordenadaY };
		}
	}
}
