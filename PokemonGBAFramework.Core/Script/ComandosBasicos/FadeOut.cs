/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of FadeOut.
	/// </summary>
	public class FadeOut:Comando
	{
		public const byte ID=0x37;
		public new const int SIZE=Comando.SIZE+1;
        public const string NOMBRE = "FadeOut";
        public const string DESCRIPCION = "Se desvanece la canción actual del Sappy";
        public FadeOut(byte velocidadDesvanecimiento)
		{
			VelocidadDesvanecimiento=velocidadDesvanecimiento;
		}
		public FadeOut(ScriptAndASMManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public FadeOut(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe FadeOut(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
        public byte VelocidadDesvanecimiento { get; set; }
        protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VelocidadDesvanecimiento};
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			VelocidadDesvanecimiento=ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			data[0]=IdComando;
			*ptrRomPosicionado=VelocidadDesvanecimiento;
		}
		
	}
	
}
