/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:36
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CountPokemon.
	/// </summary>
	public class CountPokemon:Comando
	{
		public const byte ID = 0x43;
        public const string NOMBRE= "CountPokemon";
        public const string DESCRIPCION= "Cuenta el numero de pokemon del equipo y guarda el resultado en 'lastresult'";

        public CountPokemon()
		{}
		public CountPokemon(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public CountPokemon(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe CountPokemon(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre => NOMBRE;

		public override byte IdComando => ID;

		public override string Descripcion => DESCRIPCION;

	}
}
