/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferNumber.
	/// </summary>
	public class BufferNumber:Comando
	{
		public const byte ID=0x83;
		public const int SIZE=4;
		public const string NOMBRE="BufferNumber";
		public const string DESCRIPCION="Variable version on buffernumber.";
		Byte buffer;
		Word variableToStore;
		
		public BufferNumber(Byte buffer,Word variableToStore)
		{
			Buffer=buffer;
			VariableToStore=variableToStore;
			
		}
		
		public BufferNumber(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferNumber(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferNumber(byte* ptRom,int offset):base(ptRom,offset)
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
		public Word VariableToStore
		{
			get{ return variableToStore;}
			set{variableToStore=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{buffer,variableToStore};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			variableToStore=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			Word.SetData(ptrRomPosicionado,VariableToStore);
		}
	}
}
