/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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

        public DoWeather(ScriptAndASMManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public DoWeather(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe DoWeather(ScriptAndASMManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
