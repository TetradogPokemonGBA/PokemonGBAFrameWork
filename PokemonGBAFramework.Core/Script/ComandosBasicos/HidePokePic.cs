/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of HidePokePic.
    /// </summary>
    public class HidePokePic : Comando
    {
        public const byte ID = 0x76;
        public const string NOMBRE = "HidePokePic";
        public const string DESCRIPCION = "Oculta una imagen de un pokemon previamente mostrada";
        public HidePokePic()
        {

        }

        public HidePokePic(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public HidePokePic(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe HidePokePic(byte* ptRom, int offset) : base(ptRom, offset)
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
