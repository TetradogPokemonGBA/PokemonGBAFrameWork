/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of CompareBankToByte.
	/// </summary>
	public class CompareBankToByte:Comando
	{
		public const byte ID=0x1C;
		public const int SIZE=3;
		byte bank;
		byte valueToCompare;
		public CompareBankToByte(RomGba rom,int offset):base(rom,offset)
		{}
		public CompareBankToByte(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareBankToByte(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "CompareBankToByte";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
				return "Compara la variable guardada en el bank (buffer) con la variable";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public byte Bank {
			get {
				return bank;
			}
			set {
				bank = value;
			}
		}

		public byte ValueToCompare {
			get {
				return valueToCompare;
			}
			set {
				valueToCompare = value;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{		
			bank=ptrRom[offsetComando++];
			valueToCompare=ptrRom[offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=bank;
			ptrRomPosicionado++;
			*ptrRomPosicionado=valueToCompare;
		}
	}
}
