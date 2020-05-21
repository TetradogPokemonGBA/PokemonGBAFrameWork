/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of ReleaseAll.
    /// </summary>
    public class ReleaseAll : Comando
    {
        public const byte ID = 0x6B;

        public const string NOMBRE = "ReleaseAll";
        public const string DESCRIPCION = "Deveulve a todos los personajes de la pantalla el movimiento y cierra cualquier mensaje abierto también";
        public ReleaseAll()
        {

        }

        public ReleaseAll(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
        {
        }
        public ReleaseAll(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe ReleaseAll(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
        { }
        public override string Descripcion => DESCRIPCION;

        public override byte IdComando => ID;
        public override string Nombre => NOMBRE;



    }
}
