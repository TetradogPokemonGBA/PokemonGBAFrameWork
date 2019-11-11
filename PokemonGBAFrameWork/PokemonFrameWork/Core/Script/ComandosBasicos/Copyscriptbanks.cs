/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:59
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Copyscriptbanks.
	/// </summary>
	public class Copyscriptbanks:Comando
	{
		public const byte ID=0x14;
		public new const int SIZE=Comando.SIZE+1+1;
        public const string NOMBRE = "Copyscriptbanks";
        public const string DESCRIPCION= "Copia un bank script a otro";

        public Copyscriptbanks(byte bankDestination,byte bankSource)
		{
			BankDestination=bankDestination;
			BankSource=bankSource;
		}
		public Copyscriptbanks(RomGba rom,int offset):base(rom,offset)
		{}
		public Copyscriptbanks(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Copyscriptbanks(byte* ptRom,int offset):base(ptRom,offset)
		{}
		
		#region implemented abstract members of Comando
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
        #endregion

        public byte BankDestination { get; set; }

        public byte BankSource { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{BankDestination,BankSource};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			BankDestination=ptrRom[offsetComando];
			BankSource=ptrRom[offsetComando+1];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=BankDestination;
			ptrRomPosicionado++;
			*ptrRomPosicionado=BankSource;
		}
	}
}
