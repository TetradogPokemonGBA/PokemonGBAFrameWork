/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetAnimation.
	/// </summary>
	public class SetAnimation:Comando
	{
		public const byte ID = 0x9D;
		public const int SIZE = 4;
		Byte animacion;
		Word variableAUsar;
 
		public SetAnimation(Byte animacion, Word variableAUsar)
		{
			Animacion = animacion;
			VariableAUsar = variableAUsar;
 
		}
   
		public SetAnimation(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SetAnimation(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SetAnimation(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Asigna la animación de movimiento.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetAnimation";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Animacion {
			get{ return animacion; }
			set{ animacion = value; }
		}
		public Word VariableAUsar {
			get{ return variableAUsar; }
			set{ variableAUsar = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ animacion, variableAUsar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			animacion = *(ptrRom + offsetComando);
			offsetComando++;
			variableAUsar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = animacion;
			++ptrRomPosicionado; 
			Word.SetWord(ptrRomPosicionado, VariableAUsar);
		}
	}
}
