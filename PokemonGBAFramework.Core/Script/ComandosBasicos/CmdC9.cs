/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of CmdC9.
    /// </summary>
    public class CmdC9 : Comando
    {
        public const byte ID = 0xC9;
        public const string NOMBRE = "CmdC9";
        public const string DESCRIPCION= "Bajo investigación.";
        public CmdC9()
        {

        }

        public CmdC9(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public CmdC9(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe CmdC9(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
