/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:56
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of CompareFarByteToBank.
	/// </summary>
	public class CompareFarByteToBank:Comando
	{
		public const byte ID=0x1E;
		public const int SIZE=6;
		
		byte bank;
		OffsetRom offsetToByte;
		
		public CompareFarByteToBank(byte bank,int offsetToByte):this(bank,new OffsetRom(offsetToByte))
		{}
		public CompareFarByteToBank(byte bank,OffsetRom offsetToByte)
		{
			BankToCompare=bank;
			OffsetToByte=offsetToByte;
		}
		public CompareFarByteToBank(RomGba rom,int offset):base(rom,offset)
		{}
		public CompareFarByteToBank(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareFarByteToBank(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "CompareFarByteToBank";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
				return "Compara el byte que apunte el offset con la variable guardada en el bank (buffer)";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public byte BankToCompare {
			get {
				return bank;
			}
			set {
				bank = value;
			}
		}

		public OffsetRom OffsetToByte {
			get {
				return offsetToByte;
			}
			set {
				offsetToByte = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{BankToCompare,OffsetToByte.Offset};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			offsetToByte=new OffsetRom(ptrRom,offsetComando);
			bank=ptrRom[offsetComando+OffsetRom.LENGTH];
			
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetToByte);
			ptrRomPosicionado[OffsetRom.LENGTH]=bank;
		}
	}
	
}
