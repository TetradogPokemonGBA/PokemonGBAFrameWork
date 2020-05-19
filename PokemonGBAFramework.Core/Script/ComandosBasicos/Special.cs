/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Special.
	/// </summary>
	public class Special:Comando
	{
		public const byte ID=0x25;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
        public const string NOMBRE= "Special";
        public const string DESCRIPCION= "Llama al evento especial";

        public Special(Word eventoALlamar)
		{
			EventoALlamar=eventoALlamar;
		}
		public Special(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Special(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Special(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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

        public Word EventoALlamar { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{EventoALlamar};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			EventoALlamar=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			Word.SetData(data, ,EventoALlamar);
		}
	}

}
