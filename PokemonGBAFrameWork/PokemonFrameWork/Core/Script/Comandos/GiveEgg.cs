/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GiveEgg.
	/// </summary>
	public class GiveEgg:Comando
	{
		public const byte ID=0x7A;
		public const int SIZE=3;

		short pokemon;
		public GiveEgg(short pokemon)
		{
			this.pokemon=pokemon;
		}
		
		public GiveEgg(RomGba rom,int offset):base(rom,offset)
		{
		}
		public GiveEgg(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe GiveEgg(byte* ptRom,int offset):base(ptRom,offset)
		{}

		public short Pokemon {
			get {
				return pokemon;
			}
			set {
				pokemon = value;
			}
		}

		public override string Descripcion {
			get {
				return "Entrega un huevo al entrenador.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "GiveEgg";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Pokemon};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pokemon=Word.GetWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,Pokemon);
			
		}
	}
}
