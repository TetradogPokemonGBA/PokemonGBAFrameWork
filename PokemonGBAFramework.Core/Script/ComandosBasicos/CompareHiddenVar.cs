/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFramework.Core.ComandosScript
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
		public CompareHiddenVar() { }
        public CompareHiddenVar(Byte variable, DWord valorAComparar)
		{
			Variable = variable;
			ValorAComparar = valorAComparar;
 
		}
   
		public CompareHiddenVar(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CompareHiddenVar(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CompareHiddenVar(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;
		public override string Nombre => NOMBRE;
		public override int Size => SIZE;
		public Byte Variable { get; set; }
        public DWord ValorAComparar { get; set; }

        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Variable)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(ValorAComparar)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Variable = ptrRom[offsetComando];
			offsetComando++;
			ValorAComparar = new DWord(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]= Variable;
            DWord.SetData(data,2, ValorAComparar);
			return data;
		}
	}
}
