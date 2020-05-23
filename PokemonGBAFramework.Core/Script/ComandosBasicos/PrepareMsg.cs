/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of PrepareMsg.
    /// </summary>
    public class PrepareMsg : Comando
    {
        public const byte ID = 0x67;
        public new const int SIZE = 5;
        public const string DESCRIPCION = "Prepara el texto para mostrarlo enseguida";
        public const string NOMBRE = "PrepareMsg";

        public PrepareMsg() { }

        public PrepareMsg(BloqueString texto)
        {
            Texto = texto;

        }

        public PrepareMsg(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
        {
        }
        public PrepareMsg(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe PrepareMsg(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
        { }
        public override string Descripcion => DESCRIPCION;

        public override byte IdComando => ID;
        public override string Nombre => NOMBRE;
        public override int Size => SIZE;
        public BloqueString Texto { get; set; }


        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
        {
            return new Gabriel.Cat.S.Utilitats.Propiedad[] { new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Texto)) };
        }
        protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
        {
            Texto = BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));

        }
        public override byte[] GetBytesTemp()
        {
            byte[] data = new byte[Size];
            data[0] = IdComando;
            OffsetRom.Set(data, 1, new OffsetRom(Texto.IdUnicoTemp));
            return data;

        }
    }
}
