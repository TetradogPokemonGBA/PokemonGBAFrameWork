/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckAttack.
	/// </summary>
	public class CheckAttack:Comando
	{
		public const byte ID=0x7C;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;		
		public const string NOMBRE="CheckAttack";
		public const string DESCRIPCION="Comprueba que haya un pokemon en el equipo con un ataque en particular";

        public CheckAttack(Word ataqueAComprobar)
		{
			AtaqueAComprobar=ataqueAComprobar;
			
		}
		
		public CheckAttack(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckAttack(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckAttack(byte* ptRom,int offset):base(ptRom,offset)
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
        public Word AtaqueAComprobar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{AtaqueAComprobar};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			AtaqueAComprobar=new Word(ptrRom,offsetComando);			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,AtaqueAComprobar);			
		}
	}
}
