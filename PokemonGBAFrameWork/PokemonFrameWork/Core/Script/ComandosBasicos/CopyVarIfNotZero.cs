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
		public new const byte ID = 0x1A;
        public new const string NOMBRE= "CopyVarIfNotZero";
        public new const string DESCRIPCION= "Copia el valor de la variable origen en la variable destino si es mas grande que 0";

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
                return DESCRIPCION;
			}
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
	}
}


