/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of BufferItems2.
	/// </summary>
	public class BufferItems2:Comando
	{
		public const byte ID=0xE2;
		public const int SIZE=6;
		Byte buffer;
		Word objetoAGuardar;
		Word cantidad;
		
		public BufferItems2(Byte buffer,Word objetoAGuardar,Word cantidad)
		{
			Buffer=buffer;
			ObjetoAGuardar=objetoAGuardar;
			Cantidad=cantidad;
			
		}
		
		public BufferItems2(RomGba rom,int offset):base(rom,offset)
		{
		}
		public BufferItems2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe BufferItems2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Guarda el nombre en plural del objeto en el buffer especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "BufferItems2";
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
		public Word ObjetoAGuardar
		{
			get{ return objetoAGuardar;}
			set{objetoAGuardar=value;}
		}
		public Word Cantidad
		{
			get{ return cantidad;}
			set{cantidad=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{buffer,objetoAGuardar,cantidad};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			buffer=*(ptrRom+offsetComando);
			offsetComando++;
			objetoAGuardar=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			cantidad=new Word(ptrRom,offsetComando);
		
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=buffer;
			++ptrRomPosicionado;
			Word.SetWord(ptrRomPosicionado,ObjetoAGuardar);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,Cantidad);
	
		}
	}
}
