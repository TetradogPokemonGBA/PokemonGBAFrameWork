/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of VirtualBuffer.
	/// </summary>
	public class VirtualBuffer:Comando
	{
		public const byte ID=0xBF;
		public new const int SIZE=6;
		Byte buffer;
		OffsetRom texto;
		
		public VirtualBuffer(Byte buffer,OffsetRom texto)
		{
			Buffer=buffer;
			String=texto;
			
		}
		
		public VirtualBuffer(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public VirtualBuffer(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe VirtualBuffer(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Almacena el texto en el buffer especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "VirtualBuffer";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Buffer
		{
			get{ return buffer;}
			set{buffer=value;}
		}
		public OffsetRom String
		{
			get{ return texto;}
			set{texto=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{buffer,texto};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			texto=new OffsetRom(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			OffsetRom.Set(ptrRomPosicionado,texto);
		}
	}
}
