/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of Faceplayer.
    /// </summary>
    public class Faceplayer : Comando
    {
        public const byte ID = 0x5A;
        public const string NOMBRE = "Faceplayer";
        public const string DESCRIPCION = "Mueve el que ha sido llamado hacia el PLAYER";


        public Faceplayer()
        {

        }

        public Faceplayer(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public Faceplayer(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe Faceplayer(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
