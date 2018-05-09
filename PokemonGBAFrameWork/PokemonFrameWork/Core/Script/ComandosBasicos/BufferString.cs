/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferString.
	/// </summary>
	public class BufferString:Comando
	{
		public const byte ID=0x85;
		public const int SIZE=6;
		public const string NOMBRE="BufferString";
		public const string DESCRIPCION="Guarda una string en el Buffer especificado";
		Byte buffer;
		OffsetRom texto;
		
		public BufferString(Byte buffer,OffsetRom texto)
		{
			Buffer=buffer;
			String=texto;
			
		}
		
		public BufferString(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferString(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferString(byte* ptRom,int offset):base(ptRom,offset)
		{}
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
			texto=new OffsetRom(ptrRom,new OffsetRom(ptrRom,offsetComando).Offset);		
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
