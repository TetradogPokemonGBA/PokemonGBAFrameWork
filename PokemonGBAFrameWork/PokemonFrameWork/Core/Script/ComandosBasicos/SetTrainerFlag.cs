/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetTrainerFlag.
	/// </summary>
	public class SetTrainerFlag:Comando
	{
		public const byte ID = 0x62;
		public const int SIZE = 3;
		Word entrenador;
 
		public SetTrainerFlag(Word entrenador)
		{
			Entrenador = entrenador;
 
		}
   
		public SetTrainerFlag(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SetTrainerFlag(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SetTrainerFlag(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Activa el falg del entrenador";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetTrainerFlag";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Entrenador {
			get{ return entrenador; }
			set{ entrenador = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ entrenador };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			entrenador = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado, Entrenador);
		}
	}
}
