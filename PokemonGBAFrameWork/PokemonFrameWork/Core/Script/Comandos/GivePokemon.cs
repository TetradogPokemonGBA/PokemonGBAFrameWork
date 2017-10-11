/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of GivePokemon.
	/// </summary>
	public class GivePokemon:Comando
	{
		public const byte ID=0x79;
		public const int SIZE=15;
		static readonly int BytesFill=SIZE-5;
		short pokemon;
		Byte nivel;
		short objetoEquipado;
		
		public GivePokemon(short pokemon,Byte nivel,short objetoEquipado)
		{
			Pokemon=pokemon;
			Nivel=nivel;
			ObjetoEquipado=objetoEquipado;
			
		}
		
		public GivePokemon(RomGba rom,int offset):base(rom,offset)
		{
		}
		public GivePokemon(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe GivePokemon(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Regala un pokemon al jugador";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "GivePokemon";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public short Pokemon
		{
			get{ return pokemon;}
			set{pokemon=value;}
		}
		public Byte Nivel
		{
			get{ return nivel;}
			set{nivel=value;}
		}
		public short ObjetoEquipado
		{
			get{ return objetoEquipado;}
			set{objetoEquipado=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{pokemon,nivel,objetoEquipado};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			pokemon=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			nivel=*(ptrRom+offsetComando);
			offsetComando++;
			objetoEquipado=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			const byte FILL=0x0;
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			Word.SetWord(ptrRomPosicionado,Pokemon);
			ptrRomPosicionado+=Word.LENGTH;
			*ptrRomPosicionado=nivel;
			++ptrRomPosicionado;
			Word.SetWord(ptrRomPosicionado,ObjetoEquipado);
			ptrRomPosicionado+=Word.LENGTH;
			for(int i=0;i<BytesFill;i++)
			{
				*ptrRomPosicionado=FILL;
				ptrRomPosicionado++;
			}
			
		}
	}
}
