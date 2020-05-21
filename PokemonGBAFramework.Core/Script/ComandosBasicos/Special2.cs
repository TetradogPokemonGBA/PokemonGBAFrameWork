/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:53
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Special2.
	/// </summary>
	public class Special2:Comando
	{
		public const byte ID=0x26;
		public new const int SIZE=Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE= "Special2";
        public const string DESCRIPCION= "Como Special pero guardando el valor devuelto";

		public Special2() { }
        public Special2(Word eventoALlamar,Word variable)
		{
			EventoALlamar=eventoALlamar;
			Variable=variable;
		}
		public Special2(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public Special2(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe Special2(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        /// <summary>
        /// Es la variable donde se guardará el resultado del evento
        /// </summary>
        public Word Variable { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Variable,EventoALlamar};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Variable=new Word(ptrRom,offsetComando);
			EventoALlamar=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			Word.SetData(data,1,Variable);
			Word.SetData(data,3,EventoALlamar);

			return data;
		}
	}
}
