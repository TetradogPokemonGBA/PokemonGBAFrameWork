/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of cmd8a.
	/// </summary>
	public class Cmd8A:Comando
	{
		public const byte ID=0x8A;
		public const int SIZE=4;
		Byte unKnow1;
		Byte unKnow2;
		Byte unKnow3;
		
		public Cmd8A(Byte unKnow1,Byte unKnow2,Byte unKnow3)
		{
			UnKnow1=unKnow1;
			UnKnow2=unKnow2;
			UnKnow3=unKnow3;
			
		}
		
		public Cmd8A(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd8A(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd8A(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "[aparentemente no hace nada]";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Cmd8A";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte UnKnow1
		{
			get{ return unKnow1;}
			set{unKnow1=value;}
		}
		public Byte UnKnow2
		{
			get{ return unKnow2;}
			set{unKnow2=value;}
		}
		public Byte UnKnow3
		{
			get{ return unKnow3;}
			set{unKnow3=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{unKnow1,unKnow2,unKnow3};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			unKnow1=*(ptrRom+offsetComando);
			offsetComando++;
			unKnow2=*(ptrRom+offsetComando);
			offsetComando++;
			unKnow3=*(ptrRom+offsetComando);
			offsetComando++;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=unKnow1;
			++ptrRomPosicionado;
			*ptrRomPosicionado=unKnow2;
			++ptrRomPosicionado;
			*ptrRomPosicionado=unKnow3;
			++ptrRomPosicionado;
			
		}
	}
}
