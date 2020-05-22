/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:44
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Cmd24.
	/// </summary>
	public class Cmd24:Comando
	{
		public const byte ID=0x24;
		public new const int SIZE=Comando.SIZE+OffsetRom.LENGTH;
        public const string NOMBRE = "Cmd24";
        public const string DESCRIPCION = "Se desconoce el uso que tiene";
        OffsetRom offsetDesconocido;
        

		public Cmd24() { }
        public Cmd24(int offset):this(new OffsetRom(offset))
		{}
		public Cmd24(OffsetRom offsetDesconocido)
		{
			OffsetDesconocido=offsetDesconocido;
		}
		public Cmd24(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Cmd24(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Cmd24(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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

		public OffsetRom OffsetDesconocido {
			get {
				return offsetDesconocido;
			}
			set {
				if(value==null)
					value=new OffsetRom();
				offsetDesconocido = value;
			}
		}
		public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{offsetDesconocido};
		}

		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
		  offsetDesconocido=new OffsetRom(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			OffsetRom.Set(data,1,offsetDesconocido);//como no se que es de momento lo dejo así
			return data;
		}
	}
}
