/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 6:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CopyVar.
	/// </summary>
	public class CopyVar:Comando
	{
		public const byte ID=0x19;
		public new const int SIZE=0x5;
        public const string NOMBRE= "CopyVar";
        public const string DESCRIPCION= "Copia el valor de la variable origen en la variable destino";

        public CopyVar(Word variableDestino,Word variableOrigen)
		{
			VariableDestino=variableDestino;
			VariableOrigen=variableOrigen;
		}
		
		public CopyVar(RomGba rom,int offset):base(rom,offset)
		{}
		public CopyVar(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CopyVar(byte* ptRom,int offset):base(ptRom,offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			VariableDestino=new Word(ptrRom,offsetComando);
			VariableOrigen=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,VariableDestino);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,VariableOrigen);
		}
	}
	
}
