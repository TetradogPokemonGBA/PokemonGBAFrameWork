/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg2.
	/// </summary>
	public class PrepareMsg2:Comando
	{
		public const byte ID = 0x9B;
		public const int SIZE = 5;
		OffsetRom pointerTexto;
 
		public PrepareMsg2(OffsetRom pointerTexto)
		{
			PointerTexto = pointerTexto;
 
		}
   
		public PrepareMsg2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public PrepareMsg2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe PrepareMsg2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Bajo investigación...";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PrepareMsg2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom PointerTexto {
			get{ return pointerTexto; }
			set{ pointerTexto = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ pointerTexto };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			pointerTexto = new OffsetRom(ptrRom, offsetComando);
 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado, pointerTexto);
		}
	}
}
