/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Compare.
	/// </summary>
	public class Compare:Comando
	{
		public const int ID=0x21;
		public const int SIZE=0x5;
		
		short variable;
		short valorAComparar;
		
		public Compare(short variable,short valorAComparar)
		{
			Variable=variable;
			ValorAComparar=valorAComparar;
		}
		public Compare(RomGba rom,int offset):base(rom,offset)
		{}
		public Compare(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Compare(byte* ptRom,int offset):base(ptRom,offset)
		{}

		#region implemented abstract members of Comando

		public override string Descripcion {
			get {
				return "Compara el valor de la variable con el valor pasado como parametro";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Compare";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public short Variable {
			get {
				return variable;
			}
			set {
				variable = value;
			}
		}
		public short ValorAComparar {
			get {
				return valorAComparar;
			}
			set {
				valorAComparar = value;
			}
		}
		#endregion
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variable=Word.GetWord(ptrRom,offsetComando);
			valorAComparar=Word.GetWord(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,variable);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,valorAComparar);
		}
	}
}
