/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of DoWildBattle.
    /// </summary>
    public class DoWildBattle : Comando
    {
        public const byte ID = 0xB7;
        public const string NOMBRE = "DoWildBattle";
        public const string DESCRIPCION = "Ejecuta la batalla preparada con el SetWildBattle.";


        public DoWildBattle()
        {

        }

        public DoWildBattle(ScriptManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public DoWildBattle(ScriptManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe DoWildBattle(ScriptManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
