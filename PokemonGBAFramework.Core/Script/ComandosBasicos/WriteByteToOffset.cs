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
		public new const int SIZE = SetByte.SIZE+OffsetRom.LENGTH;
        public new const string NOMBRE= "WriteByteToOffset";
        public new const string DESCRIPCION= "Inserta el byte en el offset especificado";

		public WriteByteToOffset() { }
        public WriteByteToOffset(int offset, byte valor)
			: this(new OffsetRom(offset), valor)
		{
		}
		public WriteByteToOffset(OffsetRom offset, byte valor)
			: base(valor)
		{
			OffsetToWrite = offset;
		}
		
		public WriteByteToOffset(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WriteByteToOffset(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WriteByteToOffset(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return base.GetParams().AfegirValor(new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(OffsetToWrite)));
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(scriptManager,ptrRom, offsetComando);
            offsetComando += base.ParamsSize;
			OffsetToWrite = new OffsetRom(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];

			data[0]=IdComando;
			data[1] = base.ByteAPoner;
			OffsetRom.Set(data,2, OffsetToWrite);

			return data;
		}
	}
	
	public class SetFarByte:WriteByteToOffset
	{
		public new const byte ID = 0x13;
        public new const string NOMBRE= "Setfarbyte";
        public new const string DESCRIPCION= "Inserta el byte en la dirección especificada";

		public SetFarByte() { }
        public SetFarByte(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public SetFarByte(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe SetFarByte(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
