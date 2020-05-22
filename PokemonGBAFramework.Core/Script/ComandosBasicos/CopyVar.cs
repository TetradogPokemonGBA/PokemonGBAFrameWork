/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 6:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CopyVar.
	/// </summary>
	public class CopyVar:Comando
	{
		public const byte ID=0x19;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE= "CopyVar";
        public const string DESCRIPCION= "Copia el valor de la variable origen en la variable destino";

		public CopyVar() { }
        public CopyVar(Word variableDestino,Word variableOrigen)
		{
			VariableDestino=variableDestino;
			VariableOrigen=variableOrigen;
		}
		
		public CopyVar(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CopyVar(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CopyVar(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion => DESCRIPCION;
		public override string Nombre => NOMBRE;
		public override byte IdComando => ID;
		public override int Size => SIZE;

		public Word VariableDestino { get; set; }

        public Word VariableOrigen { get; set; }
        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VariableDestino,VariableOrigen};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			VariableDestino=new Word(ptrRom,offsetComando);
			VariableOrigen=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1,VariableDestino);
 
			Word.SetData(data,3,VariableOrigen);
			return data;
		}
	}
	
}
