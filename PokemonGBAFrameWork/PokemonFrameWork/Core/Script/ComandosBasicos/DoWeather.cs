/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
    /// <summary>
    /// Description of DoWeather.
    /// </summary>
    public class DoWeather : Comando
    {
        public const byte ID = 0xA5;
        public const string NOMBRE = "DoWeather";
        public const string DESCRIPCION = "Ejecuta el cambio del tiempo hecho con Set/ Reset Weather";


        public DoWeather()
        {

        }

        public DoWeather(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public DoWeather(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe DoWeather(byte* ptRom, int offset) : base(ptRom, offset)
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
