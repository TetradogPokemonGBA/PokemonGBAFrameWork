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
		
		public CompareBankToFarByte(byte bank,int offsetToByteToCompare):this(bank,new OffsetRom(offsetToByteToCompare))
		{

		}
		public CompareBankToFarByte(byte bank,OffsetRom offsetToByteToCompare)
		{
			Bank=bank;
			OffsetToByteToCompare=offsetToByteToCompare;
		}
		public CompareBankToFarByte(RomGba rom,int offset):base(rom,offset)
		{}
		public CompareBankToFarByte(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareBankToFarByte(byte* ptRom,int offset):base(ptRom,offset)
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Bank,OffsetToByteToCompare};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Bank=ptrRom[offsetComando++];
			offsetToByteToCompare=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Bank;
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetToByteToCompare);
		}
	}
}
