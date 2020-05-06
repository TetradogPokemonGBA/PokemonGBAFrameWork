/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of CmdD5.
    /// </summary>
    public class CmdD5 : Comando
    {
        public const byte ID = 0xD5;
        public const string NOMBRE = "CmdD5";
        public const string DESCRIPCION= "Bajo investigacion.Podria actuar como nop.";
        public CmdD5()
        {

        }

        public CmdD5(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public CmdD5(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CmdD5(byte* ptRom, int offset) : base(ptRom, offset)
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
