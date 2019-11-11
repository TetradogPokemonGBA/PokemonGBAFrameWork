/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:56
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.ComandosScript
{
	public class CompareFarByteToByte : CompareFarByteToBank
	{
		public new const int ID = 0x1F;
        public new const string NOMBRE = "CompareFarByteToByte";
        public new const string DESCRIPCION= "Compara el byte que apunte el offset con el byte pasado como parametro";

        public CompareFarByteToByte(byte bank,int offsetToByte):base(bank,offsetToByte)
		{}
		public CompareFarByteToByte(byte bank,OffsetRom offsetToByte):base(bank,offsetToByte)
		{}
		public CompareFarByteToByte(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public CompareFarByteToByte(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe CompareFarByteToByte(byte* ptRom, int offset) : base(ptRom, offset)
		{
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

		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
	}
}


