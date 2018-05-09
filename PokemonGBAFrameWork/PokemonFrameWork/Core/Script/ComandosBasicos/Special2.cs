/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 12:53
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Special2.
	/// </summary>
	public class Special2:Comando
	{
		public const byte ID=0x26;
		public const int SIZE=1+1+Word.LENGTH;
		
		Word variable;
		Word eventoALlamar;
		
		public Special2(Word eventoALlamar,Word variable)
		{
			EventoALlamar=eventoALlamar;
			Variable=variable;
		}
		public Special2(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Special2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Special2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Como Special pero guardando el valor devuelto";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Special";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public Word EventoALlamar {
			get {
				return eventoALlamar;
			}
			set {
				eventoALlamar = value;
			}
		}
		/// <summary>
		/// Es la variable donde se guardará el resultado del evento
		/// </summary>
		public Word Variable {
			get {
				return variable;
			}
			set {
				variable = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Variable,EventoALlamar};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			variable=new Word(ptrRom,offsetComando);
			eventoALlamar=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetData(ptrRomPosicionado,variable);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetData(ptrRomPosicionado,eventoALlamar);
		}
	}
}
