/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:25
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CallAsm.
	/// </summary>
	public class CallAsm:Comando
	{
		public const byte ID=0x23;
		public const int SIZE=1+OffsetRom.LENGTH;
		
		OffsetRom offsetAsm;//en el futuro poner el codigo asm :) ...por mirar...
		public CallAsm(int offset):this(new OffsetRom(offset))
		{}
		public CallAsm(OffsetRom offsetAsm)
		{this.offsetAsm=offsetAsm;}
		public CallAsm(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CallAsm(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CallAsm(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Continua con la ejecución de otro script que tiene que tener return";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "CallAsm";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		public OffsetRom OffsetAsm
		{
			get{
				return offsetAsm;
			}
			set{
				if(value==null)
					value=new OffsetRom();
				offsetAsm=value;
				
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new object[]{OffsetAsm};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			offsetAsm=new OffsetRom(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetAsm);
		}
	

	}
}
