/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of BufferBoxName.
    /// </summary>
    public class BufferBoxName : Comando
    {
        public const byte ID = 0xC6;
        public new const int SIZE = Comando.SIZE+1+Word.LENGTH;
        public const string NOMBRE = "BufferBoxName";
        public const string DESCRIPCION = "Guarda el nombre de la caja especificada en el buffer especificado";

        public BufferBoxName() { }
        public BufferBoxName(Byte buffer, Word cajaPcAGuardar)
        {
            Buffer = buffer;
            CajaPcAGuardar = cajaPcAGuardar;

        }

        public BufferBoxName(ScriptManager scriptManager,RomGba rom, int offset)
             : base(scriptManager,rom, offset)
        {
        }
        public BufferBoxName(ScriptManager scriptManager,byte[] bytesScript, int offset)
            : base(scriptManager,bytesScript, offset)
        {
        }
        public unsafe BufferBoxName(ScriptManager scriptManager,byte* ptRom, int offset)
            : base(scriptManager,ptRom, offset)
        {
        }
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
        public Word CajaPcAGuardar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { Buffer, CajaPcAGuardar };
        }
        protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
        {
            Buffer = ptrRom[offsetComando];
            offsetComando++;
            CajaPcAGuardar = new Word(ptrRom, offsetComando);

        }
        public override byte[] GetBytesTemp()
        {
            byte[] data = new byte[Size];
            data[0] = IdComando;
            data[1] = Buffer;
            Word.SetData(data, 2, CajaPcAGuardar);
            return data;

        }
    }
}
