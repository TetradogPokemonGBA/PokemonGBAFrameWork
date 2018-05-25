/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:44
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Cmd24.
	/// </summary>
	public class Cmd24:Comando
	{
		public const byte ID=0x24;
		public const int SIZE=1+OffsetRom.LENGTH;
        public const string NOMBRE = "Cmd24";
        OffsetRom offsetDesconocido;
		
		public Cmd24(int offset):this(new OffsetRom(offset))
		{}
		public Cmd24(OffsetRom offsetDesconocido)
		{
			OffsetDesconocido=offsetDesconocido;
		}
		public Cmd24(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd24(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd24(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Se desconoce el uso que tiene";
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{offsetDesconocido};
		}

		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
		  offsetDesconocido=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetDesconocido);
		}
	}
}
