/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of Braille2.
    /// </summary>
    public class Braille2 : Braille
    {
        public new const byte ID = 0xD3;
        public new const string NOMBRE = "Braille2";
        public new const string DESCRIPCION = "Establece la variable 0x8004 en un valor basado en el ancho de la cadena en braille en el texto.";

        public Braille2() { }
        public Braille2(BloqueBraille brailleData):base(brailleData)
        {
         

        }

        public Braille2(ScriptAndASMManager scriptManager,RomGba rom, int offset) : base(scriptManager,rom, offset)
        {
        }
        public Braille2(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe Braille2(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
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
