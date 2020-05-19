/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PokenavCall.
	/// </summary>
	public class PokenavCall:Comando
	{
		public const byte ID = 0xDF;
		public const int SIZE = 5;
		OffsetRom text;
 
		public PokenavCall(OffsetRom text)
		{
			Text = text;
 
		}
   
		public PokenavCall(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PokenavCall(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PokenavCall(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Muestra una llamada del Pokenav.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PokenavCall";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom Text {
			get{ return text; }
			set{ text = value; }
		}
 
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Esmeralda;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ text };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			text =new OffsetRom(ptrRom, offsetComando);
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, text);
 
		}
	}
}
