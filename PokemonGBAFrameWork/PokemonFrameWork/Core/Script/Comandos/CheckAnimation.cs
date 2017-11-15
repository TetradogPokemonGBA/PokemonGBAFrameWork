/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of CheckAnimation.
	/// </summary>
	public class CheckAnimation:Comando
	{
		public const byte ID=0x9E;
		public const int SIZE=3;
		
		public const string NOMBRE="CheckAnimation";
		public const string DESCRIPCION="comprueba si una animación se está reproduciendo actualmente o no. De esta manera, se detendrá hasta que la animación se haya completado.";
		Word animacion;
		
		public CheckAnimation(Word animacion)
		{
			Animacion=animacion;
			
		}
		
		public CheckAnimation(RomGba rom,int offset):base(rom,offset)
		{
		}
		public CheckAnimation(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe CheckAnimation(byte* ptRom,int offset):base(ptRom,offset)
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
		public Word Animacion
		{
			get{ return animacion;}
			set{animacion=value;}
		}
		
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{animacion};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			animacion=new Word(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,Animacion);
		}
	}
}
