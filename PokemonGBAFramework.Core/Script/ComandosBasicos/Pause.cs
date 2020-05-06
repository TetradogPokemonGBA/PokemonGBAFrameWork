/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:01
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Pause.
	/// </summary>
	public class Pause:Comando
	{
		public const byte ID=0x28;
		public new const int SIZE=Comando.SIZE+Word.LENGTH;
        public const string NOMBRE= "Pause";
        public const string DESCRIPCION= "Pausa el script el tiempo estimado";

        public Pause(Word delay)
		{
			Delay=delay;
		}
		public Pause(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Pause(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Pause(byte* ptRom,int offset):base(ptRom,offset)
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

        public Word Delay { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Delay};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			Delay=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=base.Size;
			Word.SetData(ptrRomPosicionado,Delay);
		}
	}
}
