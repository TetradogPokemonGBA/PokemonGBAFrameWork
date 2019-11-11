/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
    /// <summary>
    /// Description of CloseOnKeyPress.
    /// </summary>
    public class CloseOnKeyPress : Comando
    {
        public const byte ID = 0x68;
        public const string NOMBRE = "CloseOnKeyPress";
        public const string DESCRIPCION= "Mantiene abierto un mensaje y lo cierra al pulsar una tecla";
        public CloseOnKeyPress()
        {

        }

        public CloseOnKeyPress(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public CloseOnKeyPress(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CloseOnKeyPress(byte* ptRom, int offset) : base(ptRom, offset)
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
