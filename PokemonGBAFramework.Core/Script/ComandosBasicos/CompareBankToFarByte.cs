/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:50
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareBankToFarByte.
	/// </summary>
	public class CompareBankToFarByte:Comando
	{
		public const byte ID=0x1D;
		public new const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
        public const string NOMBRE = "CompareBankToFarByte";
        public const string DESCRIPCION= "Compara la variable guardada en el bank (buffer) con el byte que apunte el offset";
        OffsetRom offsetToByteToCompare;
		
		public CompareBankToFarByte() { }
		public CompareBankToFarByte(byte bank,int offsetToByteToCompare):this(bank,new OffsetRom(offsetToByteToCompare))
		{

		}
		public CompareBankToFarByte(byte bank,OffsetRom offsetToByteToCompare)
		{
			Bank=bank;
			OffsetToByteToCompare=offsetToByteToCompare;
		}
		public CompareBankToFarByte(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CompareBankToFarByte(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareBankToFarByte(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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

        public byte Bank { get; set; }

        public OffsetRom OffsetToByteToCompare {
			get {
				return offsetToByteToCompare;
			}
			set {
                if (value == null)
                    value = new OffsetRom();
				offsetToByteToCompare = value;
			}
		}
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Bank,OffsetToByteToCompare};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Bank=ptrRom[offsetComando++];
			offsetToByteToCompare=new OffsetRom(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]=Bank;
			OffsetRom.Set(data,2,offsetToByteToCompare);
			return data;
		}
	}
}
