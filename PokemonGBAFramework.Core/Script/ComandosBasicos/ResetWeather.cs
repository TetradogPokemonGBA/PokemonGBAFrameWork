/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of ResetWeather.
    /// </summary>
    public class ResetWeather : Comando
    {
        public const byte ID = 0xA3;
        public const string DESCRIPCION = "Prepara la desaparición del tiempo al tiempo por defecto.";
        public const string NOMBRE = "ResetWeather";
        public ResetWeather()
        {

        }

        public ResetWeather(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
        {
        }
        public ResetWeather(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe ResetWeather(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
        { }
        public override string Descripcion => DESCRIPCION;

        public override byte IdComando => ID;
        public override string Nombre => NOMBRE;



    }
}
