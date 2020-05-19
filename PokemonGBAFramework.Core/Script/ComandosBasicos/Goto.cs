/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 21:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
namespace PokemonGBAFramework.Core.ComandosScript
{
	public class Goto : Call
	{
		public new const byte ID = 0x5;
        public new const string NOMBRE = "Goto";
        public new const string DESCRIPCION = "Continua con otro script";

		public Goto():this(new OffsetRom()) { }
        public Goto(OffsetRom script):base(script)
		{}
		public Goto(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public Goto(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Goto(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre => NOMBRE;

		public override byte IdComando => ID;

		public override string Descripcion => DESCRIPCION;

		public override bool IsEnd => true;
	}
}


