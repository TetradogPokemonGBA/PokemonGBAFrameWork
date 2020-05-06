/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of CmdB2.
    /// </summary>
    public class CmdB2 : Comando
    {
        public const byte ID = 0xB2;
        public const string NOMBRE = "CmdB2";
        public const string DESCRIPCION= "Bajo investigación.";
        public CmdB2()
        {

        }

        public CmdB2(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public CmdB2(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CmdB2(byte* ptRom, int offset) : base(ptRom, offset)
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
        public override int Size
        {
            get
            {
                return SIZE;
            }
        }

    }
}
