/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckCoins.
	/// </summary>
	public class CheckCoins:Comando
	{
		public const byte ID=0xB3;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
		public const string NOMBRE="CheckCoins";
		public const string DESCRIPCION="Guarda el numero de monedas en la variable.";

		public CheckCoins() { }
        public CheckCoins(Word variableAUsar)
		{
			VariableAUsar=variableAUsar;
			
		}
		
		public CheckCoins(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public CheckCoins(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CheckCoins(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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
        public Word VariableAUsar { get; set; }

        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(VariableAUsar))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			VariableAUsar=new Word(ptrRom,offsetComando);			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1,VariableAUsar);
			return data;
		}
	}
}
