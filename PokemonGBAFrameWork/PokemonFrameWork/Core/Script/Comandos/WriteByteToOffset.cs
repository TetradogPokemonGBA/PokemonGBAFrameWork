/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:43
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of WriteByteToOffset.
	/// </summary>
	public class WriteByteToOffset:SetByte
	{
		public const byte ID=0x11;
		public const int SIZE=0x6;
		OffsetRom offsetToWrite;
		public WriteByteToOffset(RomGba rom,int offset):base(rom,offset)
		{}
		public WriteByteToOffset(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe WriteByteToOffset(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Inserta el byte en el offset especificado";
			}
		}
		public override string Nombre {
			get {
				return "Writebytetooffset";
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando++);
			offsetToWrite=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetToWrite);
		}
	}
	
	public class SetFarByte:WriteByteToOffset
	{
		public const byte ID=0x13;
		public SetFarByte(RomGba rom,int offset):base(rom,offset)
		{}
		public SetFarByte(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetFarByte(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Setfarbyte";
			}
		}
		public override string Descripcion {
			get {
				return "Inserta el byte en la dirección especificada";
			}
		}
	}
}
