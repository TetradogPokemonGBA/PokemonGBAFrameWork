/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public CheckCoins(Word variableAUsar)
		{
			VariableAUsar=variableAUsar;
			
		}
		
		public CheckCoins(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckCoins(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckCoins(byte* ptRom,int offset):base(ptRom,offset)
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

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VariableAUsar};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			VariableAUsar=new Word(ptrRom,offsetComando);			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,VariableAUsar);			
		}
	}
}
