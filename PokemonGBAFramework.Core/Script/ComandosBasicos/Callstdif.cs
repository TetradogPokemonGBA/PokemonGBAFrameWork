/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class Callstdif : Gotostdif
	{
		public new const byte ID = 0xB;
		public new const string NOMBRE="Callstdif";
		public new const string DESCRIPCION="llama a la función si se cumple la condición";
		
		public Callstdif(byte funcionAsm,byte condicion):base(funcionAsm,condicion)
		{}

		public Callstdif(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Callstdif(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Callstdif(byte* ptRom, int offset) : base(ptRom, offset)
		{
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

		public override byte IdComando {
			get {
				return ID;
			}
		}

	
	}
}


