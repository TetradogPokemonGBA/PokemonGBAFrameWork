/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetPkmnPP.
	/// </summary>
	public class SetPkmnPP:Comando
	{
		public const byte ID=0x7B;
		public const int SIZE=5;
		Byte pokemon;
		Byte slotAtaque;
		Word pPAPoner;
		
		public SetPkmnPP(Byte pokemon,Byte slotAtaque,Word pPAPoner)
		{
			Pokemon=pokemon;
			SlotAtaque=slotAtaque;
			PPAPoner=pPAPoner;
			
		}
		
		public SetPkmnPP(RomGba rom,int offset):base(rom,offset)
		{
		}
		public SetPkmnPP(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetPkmnPP(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Pone a un pokemon del equipo los PPs al ataque especificado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "SetPokemonPP";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Pokemon
		{
			get{ return pokemon;}
			set{pokemon=value;}
		}
		public Byte SlotAtaque
		{
			get{ return slotAtaque;}
			set{slotAtaque=value;}
		}
		public Word PPAPoner
		{
			get{ return pPAPoner;}
			set{pPAPoner=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{pokemon,slotAtaque,pPAPoner};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pokemon=*(ptrRom+offsetComando);
			offsetComando++;
			slotAtaque=*(ptrRom+offsetComando);
			offsetComando++;
			pPAPoner=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=pokemon;
			++ptrRomPosicionado;
			*ptrRomPosicionado=slotAtaque;
			++ptrRomPosicionado;
			Word.SetData(ptrRomPosicionado,PPAPoner);
		}
	}
}
