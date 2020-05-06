/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:32
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Resetea los valores de las variables 0x8000,0x8001,0x8002
	/// </summary>
	public class ResetVars:Comando
	{
		public const byte ID=0x2E;

		public ResetVars(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ResetVars(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ResetVars(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Resetea los valores de las variables 0x8000,0x8001,0x8002";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "ResetVars";
			}
		}
	}
}
