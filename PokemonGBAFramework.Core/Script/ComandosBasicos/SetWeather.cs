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
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE = "SetWeather";
		public const string DESCRIPCION = "Prepara la transición al tiempo especificado.";

		public SetWeather() { }
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
		public Word TiempoNuevo { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ TiempoNuevo };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			TiempoNuevo = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			Word.SetData(data,1, TiempoNuevo);

			return data;
		}
	}
}
