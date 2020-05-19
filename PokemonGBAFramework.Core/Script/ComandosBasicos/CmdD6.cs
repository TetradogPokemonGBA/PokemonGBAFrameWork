/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of CmdD6.
    /// </summary>
    public class CmdD6 : Comando
    {
        public const byte ID = 0xD6;
        public const string NOMBRE = "CmdD6";
        public const string DESCRIPCION= "Bajo investigación.";
        public CmdD6()
        {

        }

        public CmdD6(ScriptManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public CmdD6(ScriptManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe CmdD6(ScriptManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
