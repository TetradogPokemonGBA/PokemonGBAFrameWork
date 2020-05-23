/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of Multichoice.
	/// </summary>
	public class Multichoice:Comando
	{
		public const byte ID = 0x6F;
		public new const int SIZE = Comando.SIZE+1+1+1+1;
        public const string NOMBRE = "Multichoice";
        public const string DESCRIPCION = "Pone una lista de opciones que el Jugador haga";

		public Multichoice() { }
        public Multichoice(Byte coordenadaX, Byte coordenadaY, Byte idLista, Byte botonBCancela)
		{
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
			IdLista = idLista;
			BotonBCancela = botonBCancela;
 
		}
   
		public Multichoice(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public Multichoice(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe Multichoice(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
        public Byte BotonBCancela { get; set; }

        public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(CoordenadaX)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(CoordenadaY)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(IdLista)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(BotonBCancela)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX = ptrRom[offsetComando];
			offsetComando++;
			CoordenadaY = ptrRom[offsetComando];
			offsetComando++;
			IdLista = ptrRom[offsetComando];
			offsetComando++;
			BotonBCancela = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]= CoordenadaX;
			data[2]= CoordenadaY;
			data[3]= IdLista;
			data[4] = BotonBCancela;
			return data;
		}
	}
}
