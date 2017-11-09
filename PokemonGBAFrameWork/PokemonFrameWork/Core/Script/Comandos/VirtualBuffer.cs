/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of VirtualBuffer.
	/// </summary>
	public class VirtualBuffer:Comando
	{
		public const byte ID=0xBF;
		public const int SIZE=6;
		Byte buffer;
		OffsetRom texto;
		
		public VirtualBuffer(Byte buffer,OffsetRom texto)
		{
			Buffer=buffer;
			String=texto;
			
		}
		
		public VirtualBuffer(RomGba rom,int offset):base(rom,offset)
		{
		}
		public VirtualBuffer(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe VirtualBuffer(byte* ptRom,int offset):base(ptRom,offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			texto=new OffsetRom(ptrRom,offsetComando)
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			OffsetRom.SetOffset(ptrRomPosicionado,texto);
		}
	}
}
