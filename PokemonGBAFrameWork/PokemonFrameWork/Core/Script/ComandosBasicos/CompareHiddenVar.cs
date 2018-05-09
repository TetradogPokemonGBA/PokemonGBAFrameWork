/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CompareHiddenVar.
	/// </summary>
	public class CompareHiddenVar:Comando
	{
		public const byte ID = 0xCC;
		public const int SIZE = 4;
		Byte variable;
		Word valorAComparar;
 
		public CompareHiddenVar(Byte variable, Word valorAComparar)
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
				return "Compara el valor de las variables ocultas.";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "CompareHiddenVar";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte Variable {
			get{ return variable; }
			set{ variable = value; }
		}
		public Word ValorAComparar {
			get{ return valorAComparar; }
			set{ valorAComparar = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ variable, valorAComparar };
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variable = *(ptrRom + offsetComando);
			offsetComando++;
			valorAComparar = new Word(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = variable;
			++ptrRomPosicionado; 
			Word.SetData(ptrRomPosicionado, ValorAComparar);
		}
	}
}
