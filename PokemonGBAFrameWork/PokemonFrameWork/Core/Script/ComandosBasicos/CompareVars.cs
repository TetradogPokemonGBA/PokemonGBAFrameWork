/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:15
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CompareVars.
	/// </summary>
	public class CompareVars:Comando
	{
		
		public const int ID=0x22;
		public const int SIZE=5;
		
		Word variableA;
		Word variableB;
		
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
				return "Compara el valor de las variables";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "CompareVars";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public Word VariableA {
			get {
				return variableA;
			}
			set {
				variableA = value;
			}
		}

		public Word VariableB {
			get {
				return variableB;
			}
			set {
				variableB = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{variableA,variableB};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variableA=new Word(ptrRom,offsetComando);
			variableB=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,variableA);
			Word.SetData(ptrRomPosicionado+Word.LENGTH,variableB);
		}
	}
}
