/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Flag.
	/// </summary>
	public class SetFlag:Comando
	{
		public const byte ID=0x29;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
        public const string NOMBRE = "SetFlag";
        public const string DESCRIPCION = "Activa el flag";
        public SetFlag(Word flag)
		{
			Flag=flag;
		}
		public SetFlag(RomGba rom,int offset):base(rom,offset)
		{
		}
		public SetFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe SetFlag(byte* ptRom,int offset):base(ptRom,offset)
		{}
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

		public override string Nombre {
			get {
                return NOMBRE;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}


        public Word Flag { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Flag};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Flag=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,Flag);
		}
	}
	public class ClearFlag:SetFlag
	{
		public new const byte ID=0x2A;
        public new const string NOMBRE = "ClearFlag";
        public new const string DESCRIPCION = "Desactiva el flag";

        public ClearFlag(RomGba rom,int offset):base(rom,offset)
		{
		}
		public ClearFlag(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe ClearFlag(byte* ptRom,int offset):base(ptRom,offset)
		{}
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
		public override string Nombre {
			get {
                return NOMBRE;
			}
		}
	}
	
}
