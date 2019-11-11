/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
    /// <summary>
    /// Description of EndTrainerBattle.
    /// </summary>
    public class EndTrainerBattle : Comando
    {
        public const byte ID = 0x5E;
        public const string NOMBRE = "EndTrainerBattle";
        public const string DESCRIPCION = "Vuelve desde la batalla contra el entrenador sin empezar el mensaje";


        public EndTrainerBattle()
        {

        }

        public EndTrainerBattle(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public EndTrainerBattle(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe EndTrainerBattle(byte* ptRom, int offset) : base(ptRom, offset)
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
