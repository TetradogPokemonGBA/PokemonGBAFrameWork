/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

        public BufferAttack(Byte buffer, Word ataque)
        {
            Buffer = buffer;
            Ataque = ataque;

        }

        public BufferAttack(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public BufferAttack(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe BufferAttack(byte* ptRom, int offset) : base(ptRom, offset)
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
        public Byte Buffer { get; set; }
        public Word Ataque { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { Buffer, Ataque };
        }
        protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
        {
            Buffer = ptrRom[offsetComando];
            offsetComando++;
            Ataque = new Word(ptrRom, offsetComando);

        }
        protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
        {
            base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado += base.Size;
            *ptrRomPosicionado = Buffer;
            ++ptrRomPosicionado;
            Word.SetData(ptrRomPosicionado, Ataque);

        }
    }
}
