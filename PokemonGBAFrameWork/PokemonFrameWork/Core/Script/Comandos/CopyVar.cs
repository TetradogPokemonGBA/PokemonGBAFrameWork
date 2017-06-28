/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 6:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of CopyVar.
	/// </summary>
	public class CopyVar:Comando
	{
		public const byte ID=0x19;
		public const int SIZE=0x5;
		
		short variableDestino;
		short variableOrigen;
		
		public CopyVar(short variableDestino,short variableOrigen)
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
				return "Copia el valor de la variable origen en la variable destino";
			}
		}
		public override string Nombre {
			get {
				return "CopyVar";
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

		public short VariableDestino {
			get {
				return variableDestino;
			}
			set {
				variableDestino = value;
			}
		}

		public short VariableOrigen {
			get {
				return variableOrigen;
			}
			set {
				variableOrigen = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VariableDestino,VariableOrigen};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variableDestino=Word.GetWord(ptrRom,offsetComando);
			variableOrigen=Word.GetWord(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			Word.SetWord(ptrRomPosicionado,variableDestino);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,variableOrigen);
		}
	}
	
}
