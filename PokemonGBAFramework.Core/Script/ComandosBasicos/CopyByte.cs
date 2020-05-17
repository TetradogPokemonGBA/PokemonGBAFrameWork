/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:06
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
    /// <summary>
    /// Description of CopyByte.
    /// </summary>
    public class CopyByte : Comando
    {
        public const byte ID = 0x15;
        public new const int SIZE = Comando.SIZE + OffsetRom.LENGTH + OffsetRom.LENGTH;
        public const string NOMBRE = "CopyByte";
        public const string DESCRIPCION = "Copia el byte del offset origen al offset destino";
        private OffsetRom offsetDestination;
        private OffsetRom offsetSource;

        public CopyByte(int offsetDestination, int offsetSource) : this(new OffsetRom(offsetDestination), new OffsetRom(offsetSource))
        { }
        public CopyByte(OffsetRom offsetDestination, OffsetRom offsetSource)
        {
            OffsetDestination = offsetDestination;
            OffsetSource = offsetSource;
        }
        public CopyByte(RomGba rom, int offset) : base(rom, offset)
        { }
        public CopyByte(byte[] bytesScript, int offset) : base(bytesScript, offset)
        { }
        public unsafe CopyByte(byte* ptRom, int offset) : base(ptRom, offset)
        { }

        #region implemented abstract members of Comando
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
        #endregion

        public OffsetRom OffsetDestination
        {
            get => offsetDestination;
            set
            {
                if (value == null)
                    value = new OffsetRom(); 
                offsetDestination = value;
            }
        }

        public OffsetRom OffsetSource { 
            get => offsetSource; 
            
            set {
                if (value == null)
                    value = new OffsetRom();
                offsetSource = value;
            } 
        }
        protected override System.Collections.Generic.IList<object> GetParams()
        {
            return new Object[] { OffsetDestination.Offset, OffsetSource.Offset };
        }
        protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
        {
            OffsetDestination = new OffsetRom(ptrRom,offsetComando);
            OffsetSource = new OffsetRom(ptrRom,offsetComando+OffsetRom.LENGTH);
        }
        protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
        {
            base.SetComando(ptrRomPosicionado, parametrosExtra);
            ptrRomPosicionado+=base.Size;
            OffsetRom.Set(ptrRomPosicionado, OffsetDestination);
            ptrRomPosicionado += OffsetRom.LENGTH;
            OffsetRom.Set(ptrRomPosicionado, OffsetSource);
        }
    }
}
