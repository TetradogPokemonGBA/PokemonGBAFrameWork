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

        public BufferBoxName(Byte buffer, Word cajaPcAGuardar)
        {
            Buffer = buffer;
            CajaPcAGuardar = cajaPcAGuardar;

        }

        public BufferBoxName(RomGba rom, int offset)
            : base(rom, offset)
        {
        }
        public BufferBoxName(byte[] bytesScript, int offset)
            : base(bytesScript, offset)
        {
        }
        public unsafe BufferBoxName(byte* ptRom, int offset)
            : base(ptRom, offset)
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
        public Byte Buffer { get; set; }
        public Word CajaPcAGuardar { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { Buffer, CajaPcAGuardar };
        }
        protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
        {
            Buffer = ptrRom[offsetComando];
            offsetComando++;
            CajaPcAGuardar = new Word(ptrRom, offsetComando);

        }
        protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
        {
            base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado = Buffer;
            ++ptrRomPosicionado;
            Word.SetData(ptrRomPosicionado, CajaPcAGuardar);

        }
    }
}
