/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of ApplyMovementPos.
    /// </summary>
    public class ApplyMovementPos : ApplyMovement
    {
        public new const byte ID = 0x50;
        public new const int SIZE = ApplyMovement.SIZE+1+1;
        public new const string NOMBRE = "ApplyMovementPos";
        public new const string DESCRIPCION = "Mueve el personaje y luego establece las coordenadas X/Y";
        public ApplyMovementPos() { }
        public ApplyMovementPos(Word personajeAUsar, BloqueMovimiento datosMovimiento, Byte coordenadaX, Byte coordenadaY):base(personajeAUsar,datosMovimiento)
        {

            CoordenadaX = coordenadaX;
            CoordenadaY = coordenadaY;

        }

        public ApplyMovementPos(ScriptManager scriptManager,RomGba rom, int offset) : base(scriptManager,rom, offset)
        {
        }
        public ApplyMovementPos(ScriptManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
        { }
        public unsafe ApplyMovementPos(ScriptManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
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

        public Byte CoordenadaX { get; set; }
        public Byte CoordenadaY { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return base.GetParams().AfegirValors( new object[]{ CoordenadaX, CoordenadaY });
        }
        protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
        {
            base.CargarCamando(scriptManager,ptrRom, offsetComando);
            offsetComando += base.ParamsSize;
            CoordenadaX = ptrRom[offsetComando];
            offsetComando++;
            CoordenadaY = ptrRom[offsetComando];

        }
        public override byte[] GetBytesTemp()
        {
            return base.GetBytesTemp().AddArray(new byte[] { CoordenadaX, CoordenadaY });

        }
    }
}
