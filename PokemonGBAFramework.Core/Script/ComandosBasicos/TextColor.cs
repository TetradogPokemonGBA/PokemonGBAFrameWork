/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//mirar de poner una enumeracion con los colores posibles :)
namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of TextColor.
	/// </summary>
	public class TextColor:Comando
	{
		public const byte ID = 0xC7;
		public new const int SIZE = Comando.SIZE+1;
		public const string NOMBRE = "TextColor";
		public const string DESCRIPCION = "Cambia el color del texto usado.";


		public TextColor() { }
		public TextColor(Byte color)
		{
			Color = color;
 
		}
   
		public TextColor(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public TextColor(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe TextColor(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Byte Color { get; set; }
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.VerdeHoja|Edicion.Pokemon.RojoFuego;
		}
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Color };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Color = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 
			data[0]=IdComando;
			data[1] = Color;

			return data;
		}
	}
}
