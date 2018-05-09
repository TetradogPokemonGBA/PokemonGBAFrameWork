/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Cmd2C.
	/// </summary>
	public class Cmd2C:Comando
	{
		public const byte ID=0x2C;
		public const int SIZE=1+Word.LENGTH*2;
		Word desconocido1;
		Word desconocido2;
		
		public Cmd2C(Word desconocido1,Word desconocido2)	
		{
			Desconocido1=desconocido1;
			Desconocido2=desconocido2;
		}
			
		public Cmd2C(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Cmd2C(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Cmd2C(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Uso desconocido";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Cmd2C";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Word Desconocido1 {
			get {
				return desconocido1;
			}
			set {
				desconocido1 = value;
			}
		}

		public Word Desconocido2 {
			get {
				return desconocido2;
			}
			set {
				desconocido2 = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{Desconocido1,Desconocido2};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			desconocido1=new Word(ptrRom,offsetComando);
			desconocido2=new Word(ptrRom,offsetComando+Word.LENGTH);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,desconocido1);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,desconocido2);
		}
	}
}
