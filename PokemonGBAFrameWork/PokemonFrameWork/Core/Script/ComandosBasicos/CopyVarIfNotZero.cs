/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 6:57
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFrameWork.ComandosScript
{
	public class CopyVarIfNotZero : CopyVar
	{
		public const byte ID = 0x1A;
		
		public CopyVarIfNotZero(Word variableDestino,Word variableOrigen):base(variableDestino,variableOrigen)
		{}

		public CopyVarIfNotZero(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public CopyVarIfNotZero(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe CopyVarIfNotZero(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Descripcion {
			get {
				return "Copia el valor de la variable origen en la variable destino si es mas grande que 0";
			}
		}

		public override string Nombre {
			get {
				return "CopyVarIfNotZero";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
	}
}


