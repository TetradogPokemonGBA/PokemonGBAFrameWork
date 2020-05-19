/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetWeather.
	/// </summary>
	public class SetWeather:Comando
	{
		public const byte ID = 0xA4;
		public const int SIZE = 3;
		Word tiempoNuevo;
 
		public SetWeather(Word tiempoNuevo)
		{
			TiempoNuevo = tiempoNuevo;
 
		}
   
		public SetWeather(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetWeather(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetWeather(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Prepara la transición al tiempo especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetWeather";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word TiempoNuevo {
			get{ return tiempoNuevo; }
			set{ tiempoNuevo = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ tiempoNuevo };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			tiempoNuevo = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , TiempoNuevo);
		}
	}
}
