/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:12
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class AddVar : SetVar
	{
		public new const byte ID = 0x17;
		public new const string NOMBRE="Addvar";
		public new const string DESCRIPCION="Añade cualquier valor a la variable";
		public AddVar() { }
		public AddVar(Word variable,Word valorAAñadir):base(variable,valorAAñadir)
		{}
			
		public AddVar(ScriptAndASMManager scriptManager,RomGba rom, int offset) : base(scriptManager,rom, offset)
		{
		}

		public AddVar(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe AddVar(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

		#region implemented abstract members of Comando
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
	#endregion
	}
}


