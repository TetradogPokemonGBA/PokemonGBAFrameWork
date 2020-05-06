/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:43
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat.S.Extension;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WriteByteToOffset.
	/// </summary>
	public class WriteByteToOffset:SetByte
	{
		public new const byte ID = 0x11;
		public new const int SIZE = 0x6;
        public new const string NOMBRE= "Writebytetooffset";
        public new const string DESCRIPCION= "Inserta el byte en el offset especificado";

        public WriteByteToOffset(int offset, byte valor)
			: this(new OffsetRom(offset), valor)
		{
		}
		public WriteByteToOffset(OffsetRom offset, byte valor)
			: base(valor)
		{
			OffsetToWrite = offset;
		}
		
		public WriteByteToOffset(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public WriteByteToOffset(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe WriteByteToOffset(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
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

        public OffsetRom OffsetToWrite { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return base.GetParams().AfegirValor(OffsetToWrite);
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
            offsetComando += base.Size;
			OffsetToWrite = new OffsetRom(ptrRom, offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			OffsetRom.SetOffset(ptrRomPosicionado, OffsetToWrite);
		}
	}
	
	public class SetFarByte:WriteByteToOffset
	{
		public new const byte ID = 0x13;
        public new const string NOMBRE= "Setfarbyte";
        public new const string DESCRIPCION= "Inserta el byte en la dirección especificada";
        public SetFarByte(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public SetFarByte(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe SetFarByte(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
                return NOMBRE;
			}
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
	}
}
