/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of LockAll.
    /// </summary>
    public class LockAll : Comando
    {
        public const byte ID = 0x69;
        public const string NOMBRE = "LockAll";
        public const string DESCRIPCION = "Detiene el movimiento de todos los personajes de la pantalla";


        public LockAll()
        {

        }

        public LockAll(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public LockAll(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe LockAll(byte* ptRom, int offset) : base(ptRom, offset)
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
