/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of WaitFanFare.
	/// </summary>
	public class WaitFanFare:Comando
	{
		public const byte ID=0x32;

		public WaitFanFare()
		{}
		public WaitFanFare(RomGba rom,int offset):base(rom,offset)
		{
		}
		public WaitFanFare(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe WaitFanFare(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Espera a que acabe la reproduccion de un fanfare";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "WaitFanFare";
			}
		}
	}
}
