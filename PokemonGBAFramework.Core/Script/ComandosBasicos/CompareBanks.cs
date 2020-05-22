/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 7:39
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CompareBanks.
	/// </summary>
	public class CompareBanks:Comando
	{
		public const byte ID=0x1B;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE = "CompareBanks";
        public const string DESCRIPCION= "Compara dos banks";

		public CompareBanks() { }
        public CompareBanks(Word bank1,Word bank2)
		{
			Bank1=bank1;
			Bank2=bank2;
		}
		public CompareBanks(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public CompareBanks(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CompareBanks(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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

        public Word Bank1 { get; set; }

        public Word Bank2 { get; set; }
        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Bank1,Bank2};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Bank1=new Word(ptrRom,offsetComando);
			Bank2=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			Word.SetData(data,1,Bank1);
			Word.SetData(data,3,Bank2);
			return data;
		}
	}
}
