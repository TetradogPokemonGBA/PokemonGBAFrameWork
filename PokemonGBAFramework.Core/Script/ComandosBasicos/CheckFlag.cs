/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class CheckFlag : SetFlag
	{
		public new const byte ID = 0x2B;
		public new const string NOMBRE="CheckFlag";
		public new const string DESCRIPCION="Comprueba el estado del flag y lo guarda en 'lastresult'";

		public CheckFlag() { }
		public CheckFlag(Word flag):base(flag)
		{}
		public CheckFlag(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public CheckFlag(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe CheckFlag(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

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


