/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of ExecuteRam.
    /// </summary>
    public class ExecuteRam : Comando
    {
        public const byte ID = 0xCF;
        public const string NOMBRE = "ExecuteRam";
        public const string DESCRIPCION = "Calculates the current location of the RAM script area and passes the execution to that offset.";
        public ExecuteRam()
        {

        }

        public ExecuteRam(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public ExecuteRam(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe ExecuteRam(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
        { }
        public override string Descripcion
        {
            get
            {
                return DESCRIPCION;
            }
        }

        public override byte IdComando
        {
            get
            {
                return ID;
            }
        }
        public override string Nombre
        {
            get
            {
                return NOMBRE;
            }
        }


    }
}
