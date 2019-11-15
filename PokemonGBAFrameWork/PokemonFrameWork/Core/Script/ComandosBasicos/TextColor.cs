/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//mirar de poner una enumeracion con los colores posibles :)
namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of TextColor.
	/// </summary>
	public class TextColor:Comando
	{
		public const byte ID = 0xC7;
		public const int SIZE = 2;
		Byte color;
 
		public TextColor(Byte color)
		{
			Color = color;
 
		}
   
		public TextColor(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public TextColor(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe TextColor(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Cambia el color del texto usado.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "TextColor";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Color {
			get{ return color; }
			set{ color = value; }
		}
		protected override AbreviacionCanon GetCompatibilidad()
		{
			return AbreviacionCanon.BPG|AbreviacionCanon.BPR;
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ color };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			color = ptrRom[offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = color;
		}
	}
}
