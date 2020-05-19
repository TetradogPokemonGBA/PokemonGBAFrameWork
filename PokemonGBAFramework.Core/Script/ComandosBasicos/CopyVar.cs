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

        public CopyVar(Word variableDestino,Word variableOrigen)
		{
			VariableDestino=variableDestino;
			VariableOrigen=variableOrigen;
		}
		
		public CopyVar(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CopyVar(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CopyVar(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override string Nombre {
			get {
                return NOMBRE;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

        public Word VariableDestino { get; set; }

        public Word VariableOrigen { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VariableDestino,VariableOrigen};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			VariableDestino=new Word(ptrRom,offsetComando);
			VariableOrigen=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, ,VariableDestino);
 
			Word.SetData(data, ,VariableOrigen);
		}
	}
	
}
