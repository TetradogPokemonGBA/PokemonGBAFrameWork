/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 8:03
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CompareFarBytes.
	/// </summary>
	public class CompareFarBytes:Comando
	{
		public const int ID=0x20;
		public const int SIZE=9;
		
		OffsetRom offsetA;
		OffsetRom offsetB;
		
		public CompareFarBytes(int offsetA,int offsetB):this(new OffsetRom(offsetA),new OffsetRom(offsetB))
		{}
		public CompareFarBytes(OffsetRom offsetA,OffsetRom offsetB)
		{
			OffsetA=offsetA;
			OffsetB=offsetB;
		}
		public CompareFarBytes(RomGba rom,int offset):base(rom,offset)
		{}
		public CompareFarBytes(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CompareFarBytes(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "CompareFarBytes";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
				return "Compara los bytes aljados en los offsets";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

		public OffsetRom OffsetA {
			get {
				return offsetA;
			}
			set {
				offsetA = value;
			}
		}

		public OffsetRom OffsetB {
			get {
				return offsetB;
			}
			set {
				offsetB = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{OffsetA.Offset,OffsetB.Offset};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			offsetA=new OffsetRom(ptrRom,offsetComando);
			offsetB=new OffsetRom(ptrRom,offsetComando+OffsetRom.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetA);
			OffsetRom.SetOffset(ptrRomPosicionado+OffsetRom.LENGTH,offsetB);
		}
	}
}
