/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of cmdD9.
    /// </summary>
    public class CmdD9 : Comando
    {
        public const byte ID = 0xD9;
        public const string NOMBRE = "CmdD9";
        public const string DESCRIPCION= "Bajo investigación";
        public CmdD9()
        {

        }

        public CmdD9(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public CmdD9(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CmdD9(byte* ptRom, int offset) : base(ptRom, offset)
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
