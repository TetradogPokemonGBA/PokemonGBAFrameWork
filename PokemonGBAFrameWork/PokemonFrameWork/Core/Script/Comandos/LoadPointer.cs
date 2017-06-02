/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of LoadPointer.
	/// </summary>
	public class LoadPointer:Call
	{
		public const byte ID=0xF;
		public const int SIZE=0x6;
		byte memoryBankToUse;
		Script scritp;
		public LoadPointer(RomGba rom,int offset):base(rom,offset)
		{}
		public LoadPointer(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe LoadPointer(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "LoadPointer";
			}
		}
		public override string Descripcion {
			get {
				return "Carga el puntero de un script para poderlo llamar en otros métodos";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

		public byte MemoryBankToUse {
			get {
				return memoryBankToUse;
			}
			set {
				memoryBankToUse = value;
			}
		}

		public Script Scritp {
			get {
				return scritp;
			}
			set {
				scritp = value;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			memoryBankToUse=ptrRom[offsetComando];
			base.CargarCamando(ptrRom,offsetComando+1);
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			OffsetRom offset=new OffsetRom(parametrosExtra[0]);
			*ptrRomPosicionado=IdComando;
			ptrRomPosicionado++;
			*ptrRomPosicionado=memoryBankToUse;
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offset);
		}

		

		#endregion
	}
}
