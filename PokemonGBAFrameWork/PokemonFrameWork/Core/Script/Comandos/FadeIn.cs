/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.Script
{
	public class FadeIn : FadeOut
	{
		public const byte ID = 0x38;

		public FadeIn(byte velocidadDesvanecimiento):base(velocidadDesvanecimiento)
		{}
		public FadeIn(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public FadeIn(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe FadeIn(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Descripcion {
			get {
				return "Se desvanece la canción actual del Sappy";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "FadeIn";
			}
		}
	}
}


