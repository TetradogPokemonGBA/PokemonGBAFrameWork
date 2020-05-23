/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetAnimation.
	/// </summary>
	public class SetAnimation:Comando
	{
		public const byte ID = 0x9D;
		public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
		public const string NOMBRE= "SetAnimation";
		public const string DESCRIPCION= "Asigna la animación de movimiento.";

		public SetAnimation() { }
		public SetAnimation(Byte animacion, Word variableAUsar)
		{
			Animacion = animacion;
			VariableAUsar = variableAUsar;
 
		}
   
		public SetAnimation(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetAnimation(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetAnimation(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Byte Animacion { get; set; }
		public Word VariableAUsar { get; set; }

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Animacion)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(VariableAUsar)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Animacion = ptrRom[offsetComando];
			offsetComando++;
			VariableAUsar = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];

			data[0] = IdComando;
			data[1] = Animacion;
			Word.SetData(data, 2, VariableAUsar);

			return data;
		}
	}
}
