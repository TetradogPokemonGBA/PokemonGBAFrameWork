/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:56
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareFarByteToBank.
	/// </summary>
	public class CompareFarByteToBank:Comando
	{
		public const byte ID=0x1E;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
        public const string NOMBRE = "CompareFarByteToBank";
        public const string DESCRIPCION= "Compara el byte que apunte el offset con la variable guardada en el bank (buffer)";
        OffsetRom offsetToByte;
		
		public CompareFarByteToBank() { }
		public CompareFarByteToBank(byte bank,int offsetToByte):this(bank,new OffsetRom(offsetToByte))
		{}
		public CompareFarByteToBank(byte bank,OffsetRom offsetToByte)
		{
			BankToCompare=bank;
			OffsetToByte=offsetToByte;
		}
		public CompareFarByteToBank(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CompareFarByteToBank(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareFarByteToBank(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

        public byte BankToCompare { get; set; }

        public OffsetRom OffsetToByte {
			get {
				return offsetToByte;
			}
			set {
                if (value == null)
                    value = new OffsetRom();
				offsetToByte = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{BankToCompare,OffsetToByte};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			offsetToByte=new OffsetRom(ptrRom,offsetComando);
			BankToCompare=ptrRom[offsetComando+OffsetRom.LENGTH];
			
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			OffsetRom.Set(data,1,offsetToByte);
			data[5]=BankToCompare;
			return data;
		}
	}
	
}
