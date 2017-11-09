/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of FadeScreen3.
	/// </summary>
	public class FadeScreen3:Comando
	{
		public const byte ID = 0xDC;
		public const int SIZE = 2;
		Byte unknown;
 
		public FadeScreen3(Byte unknown)
		{
			Unknown = unknown;
 
		}
   
		public FadeScreen3(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public FadeScreen3(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe FadeScreen3(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Desvanece la pantalla entrando o saliendo.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FadeScreen3";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Unknown {
			get{ return unknown; }
			set{ unknown = value; }
		}
 		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPE;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ unknown };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			unknown = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = unknown;
		}
	}
}
