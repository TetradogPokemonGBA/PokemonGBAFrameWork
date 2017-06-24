/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 3:06
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of CopyByte.
	/// </summary>
	public class CopyByte:Comando
	{
		public const byte ID=0x15;
		public const int SIZE=0x9;
		
		OffsetRom offsetDestination;
		OffsetRom offsetSource;
		
		public CopyByte(int offsetDestination, int offsetSource):this(new OffsetRom(offsetDestination),new OffsetRom(offsetSource))
		{}
		public CopyByte(OffsetRom offsetDestination,OffsetRom offsetSource)
		{
			OffsetDestination=offsetDestination;
			OffsetSource=offsetSource;
		}
		public CopyByte(RomGba rom,int offset):base(rom,offset)
		{}
		public CopyByte(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CopyByte(byte* ptRom,int offset):base(ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
		public override string Descripcion {
			get {
				return "Copia el byte del offset origen al offset destino";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Copybyte";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		#endregion

		public OffsetRom OffsetDestination {
			get {
				return offsetDestination;
			}
			set {
				offsetDestination = value;
			}
		}

		public OffsetRom OffsetSource {
			get {
				return offsetSource;
			}
			set {
				offsetSource = value;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			byte[] bytesPtr=new byte[OffsetRom.LENGTH];
			byte* ptComando=ptrRom+offsetComando;
			
			for(int i=0;i<bytesPtr.Length;i++)
			{
				bytesPtr[i]=*ptComando;
				ptComando++;
			}
			offsetDestination=new OffsetRom(bytesPtr);
			
			for(int i=0;i<bytesPtr.Length;i++)
			{
				bytesPtr[i]=*ptComando;
				ptComando++;
			}
			offsetSource=new OffsetRom(bytesPtr);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetDestination);
			ptrRomPosicionado+=OffsetRom.LENGTH;
			OffsetRom.SetOffset(ptrRomPosicionado,offsetSource);
		}
	}
}
