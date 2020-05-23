/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:15
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareVars.
	/// </summary>
	public class CompareVars:Comando
	{
		
		public const int ID=0x22;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE = "CompareVars";
        public const string DESCRIPCION= "Compara el valor de las variables";

		public CompareVars() { }
        public CompareVars(Word variableA,Word variableB)
		{
			VariableA=variableA;
			VariableB=variableB;
		}
		
		public CompareVars(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CompareVars(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareVars(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}


		public override string Descripcion => DESCRIPCION;

		public override byte IdComando => ID;

		public override string Nombre => NOMBRE;

		public override int Size => SIZE;

		public Word VariableA { get; set; }

        public Word VariableB { get; set; }
        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(VariableA)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(VariableB))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			VariableA=new Word(ptrRom,offsetComando);
			VariableB=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1,VariableA);
			Word.SetData(data,3,VariableB);
			return data;
		}
	}
}
