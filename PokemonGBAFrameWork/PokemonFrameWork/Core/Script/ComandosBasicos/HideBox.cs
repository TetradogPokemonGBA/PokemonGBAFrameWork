/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of HideBox.
	/// </summary>
	public class HideBox:Comando
	{
		public const byte ID=0x73;
		public const int SIZE=5;
		Byte posicionX;
		Byte posicionY;
		Byte ancho;
		Byte alto;
		
		public HideBox(Byte posicionX,Byte posicionY,Byte ancho,Byte alto)
		{
			PosicionX=posicionX;
			PosicionY=posicionY;
			Ancho=ancho;
			Alto=alto;
			
		}
		
		public HideBox(RomGba rom,int offset):base(rom,offset)
		{
		}
		public HideBox(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe HideBox(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Oculta una caja abierta";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "HideBox";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte PosicionX
		{
			get{ return posicionX;}
			set{posicionX=value;}
		}
		public Byte PosicionY
		{
			get{ return posicionY;}
			set{posicionY=value;}
		}
		public Byte Ancho
		{
			get{ return ancho;}
			set{ancho=value;}
		}
		public Byte Alto
		{
			get{ return alto;}
			set{alto=value;}
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.AXP|AbreviacionCanon.AXV;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{posicionX,posicionY,ancho,alto};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			posicionX=*(ptrRom+offsetComando);
			offsetComando++;
			posicionY=*(ptrRom+offsetComando);
			offsetComando++;
			ancho=*(ptrRom+offsetComando);
			offsetComando++;
			alto=*(ptrRom+offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=posicionX;
			++ptrRomPosicionado;
			*ptrRomPosicionado=posicionY;
			++ptrRomPosicionado;
			*ptrRomPosicionado=ancho;
			++ptrRomPosicionado;
			*ptrRomPosicionado=alto;
		}
	}
}
