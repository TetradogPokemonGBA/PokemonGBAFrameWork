/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of FadeScreenDelay.
	/// </summary>
	public class FadeScreenDelay:Comando
	{
		public const byte ID=0x98;
		public new const int SIZE=Comando.SIZE+1+1;
        public const string NOMBRE = "FadeScreenDelay";
        public const string DESCRIPCION = "Desvanece la pantalla entrando o saliendo, después de un rato";
        public FadeScreenDelay(Byte efectoDeDesvanecimiento,Byte retardo)
		{
			EfectoDeDesvanecimiento=efectoDeDesvanecimiento;
			Retardo=retardo;
			
		}
		
		public FadeScreenDelay(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public FadeScreenDelay(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe FadeScreenDelay(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public Byte EfectoDeDesvanecimiento { get; set; }

        public FadeScreen.EfectoFedeScreen Efecto
		{
			get{return (FadeScreen.EfectoFedeScreen)EfectoDeDesvanecimiento;}
			set{EfectoDeDesvanecimiento=(byte)value;}
			
		}
        public Byte Retardo { get; set; }

        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{EfectoDeDesvanecimiento,Retardo};
		}
		protected unsafe override void CargarCamando(ScriptManager scriptManager,byte* ptrRom, int offsetComando)
		{
			EfectoDeDesvanecimiento=*(ptrRom+offsetComando);
			offsetComando++;
			Retardo=*(ptrRom+offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
           data[0]=IdComando;
			*ptrRomPosicionado=EfectoDeDesvanecimiento;
			++ptrRomPosicionado;
			*ptrRomPosicionado=Retardo;
		}
	}
}
