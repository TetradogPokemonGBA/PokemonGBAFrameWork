/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public Faceplayer(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public Faceplayer(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe Faceplayer(byte* ptRom, int offset) : base(ptRom, offset)
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
