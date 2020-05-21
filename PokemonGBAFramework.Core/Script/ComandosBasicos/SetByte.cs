/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:13
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of SetByte.
	/// </summary>
	public class SetByte:Comando
	{
		public const byte ID=0xE;
		public new const int SIZE=0x2;
        public const string NOMBRE= "SetByte";
        public const string DESCRIPCION= "Inserta el byte en la dirección predefinida";
		public SetByte() { }
        public SetByte(byte byteAPoner)
		{
		   ByteAPoner=byteAPoner;
		}
		public SetByte(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public SetByte(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetByte(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Nombre {
			get {
                return NOMBRE;
			}
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

        public override int Size
        {
            get
            {
                return SIZE;
            }
        }


        public byte ByteAPoner { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ByteAPoner};
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(ScriptAndASMManager scriptAndASMManager, byte* ptrRom, int offsetComando)
		{
			ByteAPoner=ptrRom[offsetComando];
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0] = IdComando;
			data[1]=ByteAPoner;
			return data;
		}

		#endregion
	}
	public class SetByte2:SetByte
	{
		public new const byte ID=0x10;
		public new const int SIZE=0x3;
        public new const string NOMBRE= "SetByte2";
        public new const string DESCRIPCION= "Inserta el byte en el memory bank";

		public SetByte2() { }

        public SetByte2(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public SetByte2(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetByte2(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Nombre {
			get {
                return NOMBRE;
			}
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

        public byte MemoryBankToUse { get; set; }
        public override int Size {
			get {
				return SIZE;
			}
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			MemoryBankToUse=ptrRom[offsetComando++];
			ByteAPoner= ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			return new byte[] { IdComando, MemoryBankToUse, ByteAPoner };
		}
	}
}
