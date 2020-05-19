/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
		public SetFlag(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public SetFlag(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe SetFlag(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Flag=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, ,Flag);
		}
	}
	public class ClearFlag:SetFlag
	{
		public new const byte ID=0x2A;
        public new const string NOMBRE = "ClearFlag";
        public new const string DESCRIPCION = "Desactiva el flag";

        public ClearFlag(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public ClearFlag(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe ClearFlag(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
