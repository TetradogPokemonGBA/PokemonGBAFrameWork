/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:59
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Copyscriptbanks.
	/// </summary>
	public class CopyScriptBanks:Comando
	{
		public const byte ID=0x14;
		public new const int SIZE=Comando.SIZE+1+1;
        public const string NOMBRE = "Copyscriptbanks";
        public const string DESCRIPCION= "Copia un bank script a otro";

		public CopyScriptBanks() { }

        public CopyScriptBanks(byte bankDestination,byte bankSource)
		{
			BankDestination=bankDestination;
			BankSource=bankSource;
		}
		public CopyScriptBanks(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public CopyScriptBanks(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CopyScriptBanks(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(BankDestination)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(BankSource))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			BankDestination=ptrRom[offsetComando];
			BankSource=ptrRom[offsetComando+1];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]=BankDestination;
			data[2]=BankSource;
			return data;
		}
	}
}
