/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of ClearTrainerFlag.
	/// </summary>
	public class ClearTrainerFlag:Comando
	{
		public const byte ID=0x61;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "ClearTrainerFlag";
        public const string DESCRIPCION= "Desactiva el flag del entrenador";
        public ClearTrainerFlag(Word entrenador)
		{
			Entrenador=entrenador;

			
		}
		
		public ClearTrainerFlag(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ClearTrainerFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ClearTrainerFlag(byte* ptRom,int offset):base(ptRom,offset)
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


        public Word Entrenador { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Entrenador};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Entrenador=new Word(ptrRom,offsetComando);	
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,Entrenador);
		
		}
	}
}
