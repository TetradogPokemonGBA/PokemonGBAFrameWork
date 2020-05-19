/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of BufferAttack.
    /// </summary>
    public class BufferAttack : Comando
    {
        public const byte ID = 0x82;
        public new const int SIZE =Comando.SIZE+1+Word.LENGTH;
        public const string NOMBRE = "BufferAttack";
        public const string DESCRIPCION = "Guarda el nombre del ataque en el buffer especificado.";

        public BufferAttack() { }
        public BufferAttack(Byte buffer, Word ataque)
        {
            Buffer = buffer;
            Ataque = ataque;

        }

        public BufferAttack(ScriptAndASMManager scriptManager, RomGba rom, int offset)  : base(scriptManager,rom, offset)
        {
        }
        public BufferAttack(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
        { }
        public unsafe BufferAttack(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
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
        public override int Size
        {
            get
            {
                return SIZE;
            }
        }
        public byte Buffer { get; set; }
        public Word Ataque { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { Buffer, Ataque };
        }
        protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
        {
            Buffer = ptrRom[offsetComando];
            offsetComando++;
            Ataque = new Word(ptrRom, offsetComando);

        }
        public override byte[] GetBytesTemp()
        {
            byte[] data = new byte[Size];
            data[0] = IdComando;
            data[1] = Buffer;
            Word.SetData(data, 2, Ataque);
            return data;

        }
    }
}
