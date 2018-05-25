/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;
//corregido http://www.sphericalice.com/romhacking/documents/script/index.html#c-77
namespace PokemonGBAFrameWork.ComandosScript
{
    /// <summary>
    /// Description of CmdC8.
    /// </summary>
    public class CmdC8 : Comando
    {
        public const byte ID = 0xC8;
        public const int SIZE = 1 + DWord.LENGTH;

        public CmdC8(DWord unknow)
        {
            Unknow = unknow;
        }

        public CmdC8(RomGba rom, int offset) : base(rom, offset)
        {
        }
        public CmdC8(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CmdC8(byte* ptRom, int offset) : base(ptRom, offset)
        { }
        public override string Descripcion
        {
            get
            {
                return "Bajo Investigación";
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
                return "CmdC8";
            }
        }
        public override int Size
        {
            get
            {
                return SIZE;
            }
        }
        /// <summary>
        /// Pointer or Bank
        /// </summary>
        public DWord Unknow { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { Unknow };
        }
        protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
        {
            Unknow = new DWord(ptrRom,offsetComando);
        }
        protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
        {
            base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado++;
            DWord.SetData(ptrRomPosicionado, Unknow);
        }
    }
}
