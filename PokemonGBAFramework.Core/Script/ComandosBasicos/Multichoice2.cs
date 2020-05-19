/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Multichoice2.
	/// </summary>
	public class Multichoice2:Comando
	{
		public const byte ID = 0x70;
		public new const int SIZE = Comando.SIZE+Multichoice.SIZE+1;
        public const string NOMBRE = "Multichoice2";
        public const string DESCRIPCION = "Pone una lista de opciones para que el jugador haga, con opción por defecto";
        public Multichoice2(Byte coordenadaX, Byte coordenadaY, Byte idLista, Byte opcionPorDefecto, Byte botonBCancela)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			IdLista = idLista;
			OpcionPorDefecto = opcionPorDefecto;
			BotonBCancela = botonBCancela;
 
		}
   
		public Multichoice2(ScriptManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Multichoice2(ScriptManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Multichoice2(ScriptManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
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
        public Byte CoordenadaX { get; set; }
        public Byte CoordenadaY { get; set; }
        public Byte IdLista { get; set; }
        public Byte OpcionPorDefecto { get; set; }
        public Byte BotonBCancela { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				CoordenadaX,
				CoordenadaY,
				IdLista,
				OpcionPorDefecto,
				BotonBCancela
			};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
			offsetComando++;
			IdLista = ptrRom[offsetComando];
			offsetComando++;
			OpcionPorDefecto = ptrRom[offsetComando];
			offsetComando++;
			BotonBCancela = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado = CoordenadaX;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = CoordenadaY;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = IdLista;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = OpcionPorDefecto;
			++ptrRomPosicionado; 
			*ptrRomPosicionado = BotonBCancela;
		}
	}
}
