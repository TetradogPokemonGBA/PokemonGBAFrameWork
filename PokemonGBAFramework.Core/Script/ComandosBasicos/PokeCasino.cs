/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of PokeCasino.
    /// </summary>
    public class PokeCasino : Comando
    {
        public const byte ID = 0x89;
        public new const int SIZE = 3;
        public const string NOMBRE = "PokeCasino";
        public const string DESCRIPCION = "Abre el Casino.";
        public PokeCasino()
        {

        }

        public PokeCasino(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
        {
        }
        public PokeCasino(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe PokeCasino(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
        { }
        public override string Descripcion => DESCRIPCION;

        public override byte IdComando => ID;
        public override string Nombre => NOMBRE;
        public override int Size => SIZE;


    }
}
