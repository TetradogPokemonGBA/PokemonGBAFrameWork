/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:01
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Pause.
	/// </summary>
	public class Pause:Comando
	{
		public const byte ID=0x28;
		public const int SIZE=1+Word.LENGTH;
		
		short delay;
		
		public Pause(short delay)
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
				return "Pausa el script el tiempo estimado";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Pause";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		
		public short Delay {
			get {
				return delay;
			}
			set {
				delay = value;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			delay=Word.GetWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,delay);
		}
	}
}
