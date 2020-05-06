/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Cmd2C.
	/// </summary>
	public class Cmd2C:Comando
	{
		public const byte ID=0x2C;
        public const string NOMBRE = "Cmd2C";
        public const string DESCRIPCION= "Uso desconocido, podria hacer igual que nop";
        public Cmd2C()	
		{

		}
			
		public Cmd2C(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd2C(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd2C(byte* ptRom,int offset):base(ptRom,offset)
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
