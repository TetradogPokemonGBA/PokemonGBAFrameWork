/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public DoWildBattle(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public DoWildBattle(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe DoWildBattle(byte* ptRom, int offset) : base(ptRom, offset)
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
