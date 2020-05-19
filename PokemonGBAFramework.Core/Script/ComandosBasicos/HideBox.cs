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
        public HideBox(Byte posicionX,Byte posicionY,Byte ancho,Byte alto)
		{
			PosicionX=posicionX;
			PosicionY=posicionY;
			Ancho=ancho;
			Alto=alto;
			
		}
		
		public HideBox(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public HideBox(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe HideBox(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{PosicionX,PosicionY,Ancho,Alto};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
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
			ptrRomPosicionado+=base.Size;
			*ptrRomPosicionado=PosicionX;
			++ptrRomPosicionado;
			*ptrRomPosicionado=PosicionY;
			++ptrRomPosicionado;
			*ptrRomPosicionado=Ancho;
			++ptrRomPosicionado;
			*ptrRomPosicionado=Alto;
		}
	}
}
