/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:13
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of SetByte.
	/// </summary>
	public class SetByte:Comando
	{
		public const byte ID=0xE;
		public const int SIZE=0x2;
		byte byteAPoner;
		public SetByte(byte byteAPoner)
		{
		   ByteAPoner=byteAPoner;
		}
		public SetByte(RomGba rom,int offset):base(rom,offset)
		{}
		public SetByte(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetByte(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "SetByte";
			}
		}
		public override string Descripcion {
			get {
				return "Inserta el byte en la dirección predefinida";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

		public byte ByteAPoner {
			get {
				return byteAPoner;
			}
			set {
				byteAPoner = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ByteAPoner};
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			byteAPoner=ptrRom[offsetComando];
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			*ptrRomPosicionado=byteAPoner;
			ptrRomPosicionado++;
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		#endregion
	}
	public class SetByte2:SetByte
	{
		public const byte ID=0x10;
		public const int SIZE=0x3;
		byte memoryBankToUse;
		public SetByte2(RomGba rom,int offset):base(rom,offset)
		{}
		public SetByte2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetByte2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "SetByte2";
			}
		}
		public override string Descripcion {
			get {
				return "Inserta el byte en el memory bank";
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
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			memoryBankToUse=ptrRom[offsetComando++];
			base.CargarCamando(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			*ptrRomPosicionado=IdComando;
			ptrRomPosicionado++;
			*ptrRomPosicionado=memoryBankToUse;
			ptrRomPosicionado++;
			*ptrRomPosicionado=ByteAPoner;
			ptrRomPosicionado++;
		}
	}
}
