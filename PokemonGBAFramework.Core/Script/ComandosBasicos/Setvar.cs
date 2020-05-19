/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:12
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Setvar.
	/// </summary>
	public class SetVar:Comando
	{
		public const byte ID=0x16;
		public new const int SIZE=0x5;
        public const string NOMBRE= "Setvar";
        public const string DESCRIPCION= "Asigna a la variable el valor especificado";
		public SetVar() { }
        public SetVar(Word variable,Word valor)
		{
			Variable=variable;
			Valor=valor;
		}
		public SetVar(ScriptAndASMManager scriptManager, RomGba rom,int offset):base(scriptManager, rom,offset)
		{}
		public SetVar(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe SetVar(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
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

        public Word Variable { get; set; }

        public Word Valor { get; set; }
        #endregion

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{Variable,Valor};
		}

		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Variable=new Word(ptrRom,offsetComando);
			Valor=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];

			data[0] = IdComando;
			Word.SetData(data,1 ,Variable);		
			Word.SetData(data, 3,Valor);

			return data;
		
		}
	}
	
	public class SubVar:SetVar
	{
		public new const byte ID=0x18;
        public new const string DESCRIPCION= "Resta cualquier valor a la variable";
        public new const string NOMBRE= "SubVar";
		public SubVar() { }
        public SubVar(Word variable,Word valorARestar):base(variable,valorARestar)
		{}
		public SubVar(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager, rom,offset)
		{}
		public SubVar(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe SubVar(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
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
		#endregion
	}
}
