/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Multichoice3.
	/// </summary>
	public class Multichoice3:Comando
	{
		public const byte ID = 0x71;
		public new const int SIZE = Multichoice2.SIZE;
        public const string NOMBRE = "Multichoice3";
        public const string DESCRIPCION = "Pone una lista de opciones para que el jugador haga.el número de opciones por fila se puede establecer";

		public Multichoice3() { }
        public Multichoice3(Byte coordenadaX, Byte coordenadaY, Byte idLista, Byte numeroDeOpcionesPorFila, Byte botonBCancela)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			IdLista = idLista;
			NumeroDeOpcionesPorFila = numeroDeOpcionesPorFila;
			BotonBCancela = botonBCancela;
 
		}
   
		public Multichoice3(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Multichoice3(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Multichoice3(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Byte NumeroDeOpcionesPorFila { get; set; }
        public Byte BotonBCancela { get; set; }

        public override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[] {
				CoordenadaX,
				CoordenadaY,
				IdLista,
				NumeroDeOpcionesPorFila,
				BotonBCancela
			};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
			offsetComando++;
			IdLista = ptrRom[offsetComando];
			offsetComando++;
			NumeroDeOpcionesPorFila = ptrRom[offsetComando];
			offsetComando++;
			BotonBCancela = ptrRom[offsetComando]; 
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]= CoordenadaX;
			data[2] = CoordenadaY;
			data[3]= IdLista;
			data[4]= NumeroDeOpcionesPorFila;
			data[5] = BotonBCancela;
			return data;
		}
	}
}
