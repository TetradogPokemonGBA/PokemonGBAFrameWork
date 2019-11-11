/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public ExecuteRam(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public ExecuteRam(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe ExecuteRam(byte* ptRom, int offset) : base(ptRom, offset)
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
