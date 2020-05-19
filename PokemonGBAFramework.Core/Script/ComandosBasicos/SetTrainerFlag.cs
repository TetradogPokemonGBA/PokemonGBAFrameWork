/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
   
		public SetTrainerFlag(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetTrainerFlag(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetTrainerFlag(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			entrenador = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, , Entrenador);
		}
	}
}
