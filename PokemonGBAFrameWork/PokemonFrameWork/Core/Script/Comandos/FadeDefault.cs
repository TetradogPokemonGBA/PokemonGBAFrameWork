/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:52
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of FadeDefault.
	/// </summary>
	public class FadeDefault:Comando
	{
		public const byte ID=0x35;

		public FadeDefault()
		{}
		public FadeDefault(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FadeDefault(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FadeDefault(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Suavemente cambia a la canción por defecto del mapa";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FadeDefault";
			}
		}
	}
}
