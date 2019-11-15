/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Gotostd.
	/// </summary>
	public class Gotostd:Comando
	{
		public const byte ID=0x8;
		public new const int SIZE=2;
        public const string NOMBRE = "Gotostd";
        public const string DESCRIPCION = "Salta a la función compilada";

        public Gotostd(byte funcion)
		{
			Funcion=funcion;
		}
		public Gotostd(RomGba rom,int offset):base(rom,offset)
		{}
		public Gotostd(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Gotostd(byte* ptRom,int offset):base(ptRom,offset)
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

        public byte Funcion { get; set; }
        #region implemented abstract members of Comando

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{Funcion};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Funcion=ptrRom[offsetComando];
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Funcion;
		}


		#endregion
	}
	public class Callstd:Gotostd
	{
		public new const byte ID=0x9;
        public new const string NOMBRE= "CallStd";
        public new const string DESCRIPCION= "Llama a la función compilada";
        public Callstd(byte function) : base(function) { }
        public Callstd(RomGba rom,int offset):base(rom,offset)
		{}
		public Callstd(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Callstd(byte* ptRom,int offset):base(ptRom,offset)
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
	}
	
	
}
