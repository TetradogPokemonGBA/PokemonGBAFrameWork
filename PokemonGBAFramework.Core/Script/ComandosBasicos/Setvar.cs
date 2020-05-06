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

        public SetVar(Word variable,Word valor)
		{
			Variable=variable;
			Valor=valor;
		}
		public SetVar(RomGba rom,int offset):base(rom,offset)
		{}
		public SetVar(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetVar(byte* ptRom,int offset):base(ptRom,offset)
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

		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Variable=new Word(ptrRom,offsetComando);
			Valor=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,Variable);
			ptrRomPosicionado+=Word.LENGTH;
			
			Word.SetData(ptrRomPosicionado,Valor);
		
		}
	}
	
	public class SubVar:SetVar
	{
		public new const byte ID=0x18;
        public new const string DESCRIPCION= "Resta cualquier valor a la variable";
        public new const string NOMBRE= "SubVar";

        public SubVar(Word variable,Word valorARestar):base(variable,valorARestar)
		{}
		public SubVar(RomGba rom,int offset):base(rom,offset)
		{}
		public SubVar(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SubVar(byte* ptRom,int offset):base(ptRom,offset)
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
