/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:15
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of CompareVars.
	/// </summary>
	public class CompareVars:Comando
	{
		
		public const int ID=0x22;
		public const int SIZE=5;
		
		short variableA;
		short variableB;
		
		public CompareVars(short variableA,short variableB)
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

		public short VariableA {
			get {
				return variableA;
			}
			set {
				variableA = value;
			}
		}

		public short VariableB {
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
			variableA=Word.GetWord(ptrRom,offsetComando);
			variableB=Word.GetWord(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,variableA);
			Word.SetWord(ptrRomPosicionado+Word.LENGTH,variableB);
		}
	}
}
