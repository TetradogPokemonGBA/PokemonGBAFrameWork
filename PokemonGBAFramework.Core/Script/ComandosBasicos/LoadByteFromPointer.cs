/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:43
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class LoadByteFromPointer : WriteByteToOffset
	{
		public new const byte ID = 0x12;
        public new const string NOMBRE= "Loadbytefrompointer";
        public new const string DESCRIPCION= "Carga el byte de la posición para poder ser usada en otros comandos";

        public LoadByteFromPointer(int offsetToLoadByte,byte valor):base(offsetToLoadByte,valor)
		{}
		public LoadByteFromPointer(OffsetRom offsetToLoadByte,byte valor):base(offsetToLoadByte,valor)
		{}
		public LoadByteFromPointer(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public LoadByteFromPointer(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe LoadByteFromPointer(byte* ptRom, int offset) : base(ptRom, offset)
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


