/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
{
	/// <summary>
	/// Description of Multichoice2.
	/// </summary>
	public class Multichoice2:Comando
	{
		public const byte ID = 0x70;
		public const int SIZE = 6;
		Byte coordenadaX;
		Byte coordenadaY;
		Byte idLista;
		Byte opcionPorDefecto;
		Byte botonBCancela;
 
		public Multichoice2(Byte coordenadaX, Byte coordenadaY, Byte idLista, Byte opcionPorDefecto, Byte botonBCancela)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			IdLista = idLista;
			OpcionPorDefecto = opcionPorDefecto;
			BotonBCancela = botonBCancela;
 
		}
   
		public Multichoice2(RomGba rom, int offset)
			: base(rom, offset)
		{
		}
		public Multichoice2(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}
		public unsafe Multichoice2(byte* ptRom, int offset)
			: base(ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return "Pone una lista de opciones para que el jugador haga, con opción por defecto";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Multichoice2";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public Byte CoordenadaX {
			get{ return coordenadaX; }
			set{ coordenadaX = value; }
		}
		public Byte CoordenadaY {
			get{ return coordenadaY; }
			set{ coordenadaY = value; }
		}
		public Byte IdLista {
			get{ return idLista; }
			set{ idLista = value; }
		}
		public Byte OpcionPorDefecto {
			get{ return opcionPorDefecto; }
			set{ opcionPorDefecto = value; }
		}
		public Byte BotonBCancela {
			get{ return botonBCancela; }
			set{ botonBCancela = value; }
		}
 
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				coordenadaX,
				coordenadaY,
				idLista,
				opcionPorDefecto,
				botonBCancela
			};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			coordenadaX = *(ptrRom + offsetComando);
			offsetComando++;
			coordenadaY = *(ptrRom + offsetComando);
			offsetComando++;
			idLista = *(ptrRom + offsetComando);
			offsetComando++;
			opcionPorDefecto = *(ptrRom + offsetComando);
			offsetComando++;
			botonBCancela = *(ptrRom + offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = coordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = coordenadaY;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = idLista;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = opcionPorDefecto;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = botonBCancela;
		}
	}
}
