/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of RemoveCoins.
	/// </summary>
	public class RemoveCoins:Comando
	{
		public const byte ID = 0xB5;
		public new const int SIZE = 3;
		public const string NOMBRE= "RemoveCoins";
		public const string DESCRIPCION= "Coge el numero especificado de fichas del jugador.";

		public RemoveCoins() { }
		public RemoveCoins(Word numeroDeFichasACoger)
		{
			NumeroDeFichasACoger = numeroDeFichasACoger;
 
		}
   
		public RemoveCoins(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public RemoveCoins(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe RemoveCoins(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
		public Word NumeroDeFichasACoger { get; set; }

		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(NumeroDeFichasACoger)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			NumeroDeFichasACoger = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			Word.SetData(data,1, NumeroDeFichasACoger);
			return data;
		}
	}
}
