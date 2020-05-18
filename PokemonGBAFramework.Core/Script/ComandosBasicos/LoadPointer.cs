/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 2:20
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Utilitats;
using System;


namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of LoadPointer.
	/// </summary>
	public class LoadPointer:Comando
	{
		public  const byte ID=0xF;
		public new  const int SIZE=Comando.SIZE+1+OffsetRom.LENGTH;
        public  const string NOMBRE = "LoadPointer";
        public  const string DESCRIPCION = "Carga el puntero de un script para poderlo llamar en otros métodos";

		public LoadPointer() { }
        public LoadPointer(byte memoryBankToUse,Script scriptToLoad)
		{
			MemoryBankToUse=memoryBankToUse;
			Script = scriptToLoad;
		}
		public LoadPointer(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public LoadPointer(ScriptManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe LoadPointer(ScriptManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
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
		public Script Script { get; set; }
        public override int Size {
			get {
				return SIZE;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{MemoryBankToUse,Script};
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(ScriptManager scriptManager, byte* ptrRom, int offsetComando)
		{
			MemoryBankToUse=ptrRom[offsetComando++];
			Script = scriptManager.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			data[1] = MemoryBankToUse;
			OffsetRom.Set(data,2, new OffsetRom(Script.IdUnicoTemp));
			return data;
		}


		#endregion

	}
	public class MsgBox : Comando
	{
		public const byte ID = 0xF;
		public new const int SIZE = Comando.SIZE + 1 + OffsetRom.LENGTH;
		public const string NOMBRE = "MsgBox";
		public const string DESCRIPCION = "Carga el puntero de un script para poderlo llamar en otros métodos";
		public enum MsgBoxTipo
		{

		}
		public MsgBox() { }
		public MsgBox(MsgBoxTipo tipo, BloqueString texto)
		{
			Tipo = tipo;
			Texto = texto;
		}
		public MsgBox(ScriptManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
		{ }
		public MsgBox(ScriptManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
		{ }
		public unsafe MsgBox(ScriptManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
		{ }
		public override string Nombre
		{
			get
			{
				return NOMBRE;
			}
		}
		public override string Descripcion
		{
			get
			{
				return DESCRIPCION;
			}
		}
		public override byte IdComando
		{
			get
			{
				return ID;
			}
		}

		public MsgBoxTipo Tipo { get; set; }
		public BloqueString Texto { get; set; }
		public override int Size
		{
			get
			{
				return SIZE;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] { Tipo, Texto };
		}
		#region implemented abstract members of Comando

		protected unsafe override void CargarCamando(ScriptManager scriptManager, byte* ptrRom, int offsetComando)
		{
			Tipo = (MsgBoxTipo)ptrRom[offsetComando++];
			Texto = BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			data[1] = (byte)Tipo;
			OffsetRom.Set(data, 2, new OffsetRom(Script.IdUnicoTemp));
			return data;
		}


		#endregion

	}
}
