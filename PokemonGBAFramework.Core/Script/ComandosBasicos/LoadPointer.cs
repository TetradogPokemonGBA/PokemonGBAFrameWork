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
		public LoadPointer(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{}
		public LoadPointer(ScriptAndASMManager scriptManager, byte[] bytesScript,int offset):base(scriptManager, bytesScript,offset)
		{}
		public unsafe LoadPointer(ScriptAndASMManager scriptManager, byte* ptRom,int offset):base(scriptManager, ptRom,offset)
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
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(MemoryBankToUse)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Script))};
		}
		#region implemented abstract members of Comando

		protected unsafe  override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			MemoryBankToUse=ptrRom[offsetComando++];
			Script = scriptManager.GetScript(ptrRom, new OffsetRom(ptrRom, offsetComando));
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
	public class MsgBox : Comando,IString
	{
		public const byte ID = 0xF;
		public new const int SIZE = Comando.SIZE + 1 + OffsetRom.LENGTH;
		public const string NOMBRE = "MsgBox";
		public const string DESCRIPCION = "Carga el puntero de un string para poderlo llamar en otros métodos";
		//source:https://www.pokecommunity.com/showthread.php?t=164276 //creditos: HackMew
		public enum MsgBoxTipo
		{
			/// <summary>
			/// This is the msgbox used for normal people. Using this type means that you don't need to use the lock, faceplayer or release commands.
			/// </summary>
			Gente = 0x2,
			/// <summary>
			/// Used for signs etc. No lock or faceplayer effect. Only shows the sign textbox when used on an actual sign.
			/// </summary>
			Poste = 0x3,
			/// <summary>
			/// A normal msgbox except for the fact that it does not close. Command closeonkeypress must be used to close it. No lock or faceplayer effect.
			/// </summary>
			CierrePorComando = 0x4,
			/// <summary>
			/// Used for Yes/No questions. No lock or faceplayer effect.
			/// </summary>
			Pregunta = 0x5,
			/// <summary>
			/// Normal textbox. Has no lock or faceplayer effect.
			/// </summary>
			Normal = 0x6
		}
		public MsgBox() { }
		public MsgBox(MsgBoxTipo tipo, BloqueString texto)
		{
			Tipo = tipo;
			Texto = texto;
		}
		public MsgBox(ScriptAndASMManager scriptManager, RomGba rom, int offset) : base(scriptManager, rom, offset)
		{ }
		public MsgBox(ScriptAndASMManager scriptManager, byte[] bytesScript, int offset) : base(scriptManager, bytesScript, offset)
		{ }
		public unsafe MsgBox(ScriptAndASMManager scriptManager, byte* ptRom, int offset) : base(scriptManager, ptRom, offset)
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
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[] { new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Tipo)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Texto)) };
		}
		#region implemented abstract members of Comando

		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager, byte* ptrRom, int offsetComando)
		{
			Tipo = (MsgBoxTipo)ptrRom[offsetComando++];
			Texto = BloqueString.Get(ptrRom, new OffsetRom(ptrRom, offsetComando));
		}

		public override byte[] GetBytesTemp()
		{
			byte[] data = new byte[Size];
			data[0] = IdComando;
			data[1] = (byte)Tipo;
			OffsetRom.Set(data, 2, new OffsetRom(Texto.IdUnicoTemp));
			return data;
		}


		#endregion

	}
}
