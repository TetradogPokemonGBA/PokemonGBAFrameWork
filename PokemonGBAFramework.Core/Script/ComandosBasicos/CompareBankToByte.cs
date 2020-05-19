/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareBankToByte.
	/// </summary>
	public class CompareBankToByte:Comando
	{
		public const byte ID=0x1C;
		public new const int SIZE=Comando.SIZE+1+1;
        public const string NOMBRE = "CompareBankToByte";
        public const string DESCRIPCION= "Compara la variable guardada en el bank (buffer) con la variable";

        public CompareBankToByte(byte bank,byte valorAComparar)
		{
			Bank=bank;
			ValueToCompare=valorAComparar;
		}
		public CompareBankToByte(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CompareBankToByte(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareBankToByte(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

        public byte Bank { get; set; }

        public byte ValueToCompare { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Bank,ValueToCompare};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{		
			Bank=ptrRom[offsetComando++];
			ValueToCompare=ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=Bank;
			ptrRomPosicionado++;
			*ptrRomPosicionado=ValueToCompare;
		}
	}
}
