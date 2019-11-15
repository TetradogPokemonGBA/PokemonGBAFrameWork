/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CompareHiddenVar.
	/// </summary>
	public class CompareHiddenVar:Comando
	{
		public const byte ID = 0xCC;
		public new const int SIZE = Comando.SIZE+1+DWord.LENGTH;
        public const string NOMBRE = "CompareHiddenVar";
        public const string DESCRIPCION= "Compara el valor de las variables ocultas.";

        public CompareHiddenVar(Byte variable, DWord valorAComparar)
		{
			Variable = variable;
			ValorAComparar = valorAComparar;
 
		}
   
		public CompareHiddenVar(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public CompareHiddenVar(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe CompareHiddenVar(byte* ptRom, int offset)
			: base(ptRom, offset)
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
        public Byte Variable { get; set; }
        public DWord ValorAComparar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Variable, ValorAComparar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Variable = ptrRom[offsetComando];
			offsetComando++;
			ValorAComparar = new DWord(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = Variable;
			++ptrRomPosicionado;
            DWord.SetData(ptrRomPosicionado, ValorAComparar);
		}
	}
}
