/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:02
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class Jumpram : Comando
	{
		public const byte ID = 0xC;
        public const string NOMBRE= "Jumpram";
        public const string DESCRIPCION= "Salta a la dirección por defecto de la memoria ram y ejecuta el script guardado allí";

        public Jumpram()
		{}
		public Jumpram(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public Jumpram(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Jumpram(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
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

		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}

		#region implemented abstract members of Comando

	#endregion
	}
}


