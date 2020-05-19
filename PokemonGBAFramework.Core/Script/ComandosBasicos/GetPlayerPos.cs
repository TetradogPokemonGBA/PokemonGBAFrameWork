/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:32
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of GetPlayerPos.
	/// </summary>
	public class GetPlayerPos:Comando
	{
		public const byte ID = 0x42;

		public new const int SIZE = Comando.SIZE+Word.LENGTH+Word.LENGTH;
        public const string NOMBRE = "GetPlayerPos";
        public const string DESCRIPCION = "Obtiene las coordenadas X,Y del jugador";
        public GetPlayerPos(Word coordenadaX,Word coordenadaY)
		{
			CoordenadaX=coordenadaX;
			CoordenadaY=coordenadaY;
			
		}

		public GetPlayerPos(ScriptManager scriptManager,RomGba rom, int offset)  : base(scriptManager,rom, offset)
		{
		}

		public GetPlayerPos(ScriptManager scriptManager,byte[] bytesScript, int offset) : base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe GetPlayerPos(ScriptManager scriptManager,byte* ptRom, int offset) : base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre {
			get {
                return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
                return DESCRIPCION;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}

        public Word CoordenadaX { get; set; }

        public Word CoordenadaY { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{CoordenadaX,CoordenadaY};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			CoordenadaX=new Word(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			CoordenadaY=new Word(ptrRom,offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado+=base.Size;
			Word.SetData(data, ,CoordenadaX);
 
			Word.SetData(data, ,CoordenadaY);
		}
	}
}
