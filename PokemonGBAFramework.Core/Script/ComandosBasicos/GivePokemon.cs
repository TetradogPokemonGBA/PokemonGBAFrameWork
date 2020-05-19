/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of GivePokemon.
	/// </summary>
	public class GivePokemon:Comando
	{
		public const byte ID=0x79;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+1+Word.LENGTH+BYTESFILL;
	    const int BYTESFILL=9;
        const byte FILL = 0x0;
        public const string NOMBRE = "GivePokemon";
        public const string DESCRIPCION = "Regala un pokemon al jugador";
        public GivePokemon(Word pokemon,Byte nivel,Word objetoEquipado)
		{
			Pokemon=pokemon;
			Nivel=nivel;
			ObjetoEquipado=objetoEquipado;
			
		}
		
		public GivePokemon(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public GivePokemon(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe GivePokemon(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Word Pokemon { get; set; }
        public Byte Nivel { get; set; }
        public Word ObjetoEquipado { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Pokemon,Nivel,ObjetoEquipado};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Pokemon=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			Nivel=*(ptrRom+offsetComando);
			offsetComando++;
			ObjetoEquipado=new Word(ptrRom,offsetComando);
  
        }
		public override byte[] GetBytesTemp()
		{
			
			byte[] data=new byte[Size];
           data[0]=IdComando;
			Word.SetData(data, ,Pokemon);
 
			*ptrRomPosicionado=Nivel;
			++ptrRomPosicionado;
			Word.SetData(data, ,ObjetoEquipado);
 

			for(int i=0;i<BYTESFILL;i++)
			{
				*ptrRomPosicionado=FILL;
				ptrRomPosicionado++;
			}
			
		}
	}
}
