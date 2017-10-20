/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferAttack.
	/// </summary>
	public class BufferAttack:Comando
	{
		public const byte ID=0x82;
		public const int SIZE=4;
		Byte buffer;
		ushort ataque;
		
		public BufferAttack(Byte buffer,ushort ataque)
		{
			Buffer=buffer;
			Ataque=ataque;
			
		}
		
		public BufferAttack(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferAttack(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferAttack(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Guarda el nombre del ataque en el buffer especificado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "BufferAttack";
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
		public ushort Ataque
		{
			get{ return ataque;}
			set{ataque=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{buffer,ataque};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			ataque=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			Word.SetWord(ptrRomPosicionado,Ataque);
			ptrRomPosicionado+=Word.LENGTH;
			
		}
	}
}
