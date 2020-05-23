/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of HideBox.
	/// </summary>
	public class HideBox:Comando
	{
		public const byte ID=0x73;
		public new const int SIZE=Comando.SIZE+1+1+1+1;
        public const string NOMBRE = "HideBox";
        public const string DESCRIPCION = "Oculta una caja abierta";

		public HideBox() { }
        public HideBox(Byte posicionX,Byte posicionY,Byte ancho,Byte alto)
		{
			PosicionX=posicionX;
			PosicionY=posicionY;
			Ancho=ancho;
			Alto=alto;
			
		}
		
		public HideBox(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public HideBox(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe HideBox(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Byte PosicionX { get; set; }
        public Byte PosicionY { get; set; }
        public Byte Ancho { get; set; }
        public Byte Alto { get; set; }
        protected override Edicion.Pokemon GetCompatibilidad()
		{
			return Edicion.Pokemon.Zafiro|Edicion.Pokemon.Rubi;
		}
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(PosicionX)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(PosicionY)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Ancho)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Alto))};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			PosicionX=*(ptrRom+offsetComando);
			offsetComando++;
			PosicionY=*(ptrRom+offsetComando);
			offsetComando++;
			Ancho=*(ptrRom+offsetComando);
			offsetComando++;
			Alto=*(ptrRom+offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			data[1]=PosicionX;
			data[2]=PosicionY;
			data[3]=Ancho;
			data[4]=Alto;
			return data;
		}
	}
}
