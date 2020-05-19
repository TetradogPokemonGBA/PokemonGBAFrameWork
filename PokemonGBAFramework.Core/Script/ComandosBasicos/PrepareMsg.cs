/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of PrepareMsg.
	/// </summary>
	public class PrepareMsg:Comando
	{
		public const byte ID=0x67;
		public const int SIZE=5;
		OffsetRom texto;
		
		public PrepareMsg(OffsetRom texto)
		{
			Texto=texto;
			
		}
		
		public PrepareMsg(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public PrepareMsg(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe PrepareMsg(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Prepara el texto para mostrarlo enseguida";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PrepareMsg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom Texto
		{
			get{ return texto;}
			set{texto=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{texto};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			texto=new OffsetRom(ptrRom,offsetComando);
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			OffsetRom.Set(ptrRomPosicionado,texto);
			
		}
	}
}
