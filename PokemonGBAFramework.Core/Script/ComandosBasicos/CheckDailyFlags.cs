/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:15
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckDailyFlags.
	/// </summary>
	public class CheckDailyFlags:Comando
	{
		public const byte ID=0x2D;
		public const string NOMBRE="CheckDailyFlags";
		public const string DESCRIPCION="chekcs the daily flags to see if any of them have been set already,but only if they were set previously.Then it clears those flags";
		
		public CheckDailyFlags()
		{}

		public CheckDailyFlags(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckDailyFlags(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckDailyFlags(byte* ptRom,int offset):base(ptRom,offset)
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
		protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi|Edicion.Pokemon.Esmeralda;
		}
	}
}
