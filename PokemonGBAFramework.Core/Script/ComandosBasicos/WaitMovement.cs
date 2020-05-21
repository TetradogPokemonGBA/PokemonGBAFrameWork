/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WaitMovement.
	/// </summary>
	public class WaitMovement : Comando
	{
		public const byte JUGADOR = 0xFF;
		public const byte ID = 0x51;
		public new const int SIZE = 3;

		private Word persona;

		public WaitMovement():this(JUGADOR)
		{

		}
		public WaitMovement(Word persona) => Persona = persona;
		public WaitMovement(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WaitMovement(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WaitMovement(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion
		{
			get
			{
				return "Espera a que el ApplyMovement acabe";
			}
		}

		public override byte IdComando
		{
			get
			{
				return ID;
			}
		}
		public override string Nombre
		{
			get
			{
				return "WaitMovement";
			}
		}
		public override int Size
		{
			get
			{
				return SIZE;
			}
		}
		public Word Persona
		{
			get => persona; 
			set {

				if (value == default)
					value = new Word();

				persona = value;
			}
		}
		protected override unsafe void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Persona = new Word(ptrRom, offsetComando);
		}
		protected override IList<object> GetParams()
		{
			return new object[] { Persona };
		}
		protected override unsafe void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			Word.SetData(data, , Persona);
		}

	}
}
