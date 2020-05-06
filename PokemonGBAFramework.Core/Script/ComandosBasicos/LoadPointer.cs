/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;


namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of LoadPointer.
	/// </summary>
	public class LoadPointer:Call
	{
		public new const byte ID=0xF;
		public new const int SIZE=Call.SIZE+1;
        public new const string NOMBRE = "LoadPointer";
        public new const string DESCRIPCION = "Carga el puntero de un script para poderlo llamar en otros métodos";

        public LoadPointer(byte memoryBankToUse,OffsetRom scriptToLoad):base(scriptToLoad)
		{
			MemoryBankToUse=memoryBankToUse;
		}
		public LoadPointer(RomGba rom,int offset):base(rom,offset)
		{}
		public LoadPointer(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe LoadPointer(byte* ptRom,int offset):base(ptRom,offset)
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{MemoryBankToUse,Offset};
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			MemoryBankToUse=ptrRom[offsetComando];
			base.CargarCamando(ptrRom,offsetComando+1);
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			OffsetRom offset=Offset;
			*ptrRomPosicionado=IdComando;
			ptrRomPosicionado++;
			*ptrRomPosicionado=MemoryBankToUse;
			ptrRomPosicionado++;
			OffsetRom.SetOffset(ptrRomPosicionado,offset);
		}

		

		#endregion
	}
}
