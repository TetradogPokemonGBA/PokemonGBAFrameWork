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
	public class SetPokemonPP:Comando
	{
		public const byte ID=0x7B;
		public new const int SIZE=Comando.SIZE+1+1+Word.LENGTH;
		public const string NOMBRE = "SetPokemonPP";
		public const string DESCRIPCION = "Pone a un pokemon del equipo los PPs al ataque especificado";

		public SetPokemonPP() { }
		public SetPokemonPP(Byte pokemon,Byte slotAtaque,Word pPAPoner)
		{
			Pokemon=pokemon;
			SlotAtaque=slotAtaque;
			PPAPoner=pPAPoner;
			
		}
		
		public SetPokemonPP(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public SetPokemonPP(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetPokemonPP(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
		public Byte Pokemon { get; set; }
		public Byte SlotAtaque { get; set; }
		public Word PPAPoner { get; set; }

		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Pokemon,SlotAtaque,PPAPoner};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon=*(ptrRom+offsetComando);
			offsetComando++;
			SlotAtaque=*(ptrRom+offsetComando);
			offsetComando++;
			PPAPoner=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			data[1]=Pokemon;
			data[2]=SlotAtaque;
			Word.SetData(data,3,PPAPoner);

			return data;
		}
	}
}
