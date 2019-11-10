/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:25
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CallAsm.
	/// </summary>
	public class CallAsm:Call
	{
		public new const byte ID=0x23;
		public new const string NOMBRE="CallAsm";
		public new const string DESCRIPCION="Continua con la ejecución de otro script que tiene que tener return";
		public CallAsm(int offset):this(new OffsetRom(offset))
		{}
		public CallAsm(OffsetRom offsetAsm):base(offsetAsm)
		{}
		public CallAsm(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CallAsm(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CallAsm(byte* ptRom,int offset):base(ptRom,offset)
		{}
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

		public override string Nombre {
			get {
				return NOMBRE;
			}
		}

	

	}
}
