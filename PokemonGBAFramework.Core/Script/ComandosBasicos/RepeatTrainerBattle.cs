/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of RepeatTrainerBattle.
    /// </summary>
    public class RepeatTrainerBattle : Comando
    {
        public const byte ID = 0x5D;
        public const string DESCRIPCION = "Repite la última batalla empezada contra un entrenador";
        public const string NOMBRE = "RepeatTrainerBattle";
        public RepeatTrainerBattle()
        {

        }

        public RepeatTrainerBattle(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
        {
        }
        public RepeatTrainerBattle(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe RepeatTrainerBattle(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
        { }
        public override string Descripcion => DESCRIPCION;

        public override byte IdComando => ID;
        public override string Nombre => NOMBRE;


    }
}
