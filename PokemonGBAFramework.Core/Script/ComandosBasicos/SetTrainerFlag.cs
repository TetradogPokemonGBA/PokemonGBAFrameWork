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
		public new const int SIZE = Comando.SIZE+Word.LENGTH;
		public const string NOMBRE = "SetTrainerFlag";
		public const string DESCRIPCION = "Activa el falg del entrenador";

		public SetTrainerFlag() { }
		public SetTrainerFlag(Word entrenador)
		{
			Entrenador = entrenador;
 
		}
   
		public SetTrainerFlag(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetTrainerFlag(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetTrainerFlag(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
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
		public Word Entrenador { get; set; }

		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Entrenador };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Entrenador = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			Word.SetData(data,1, Entrenador);
			
			return data;
		}
	}
}
