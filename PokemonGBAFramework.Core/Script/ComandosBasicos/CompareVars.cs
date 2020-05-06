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

        public CompareVars(Word variableA,Word variableB)
		{
			VariableA=variableA;
			VariableB=variableB;
		}
		
		public CompareVars(RomGba rom,int offset):base(rom,offset)
		{}
		public CompareVars(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareVars(byte* ptRom,int offset):base(ptRom,offset)
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

        public Word VariableA { get; set; }

        public Word VariableB { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VariableA,VariableB};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			VariableA=new Word(ptrRom,offsetComando);
			VariableB=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,VariableA);
			Word.SetData(ptrRomPosicionado+Word.LENGTH,VariableB);
		}
	}
}
