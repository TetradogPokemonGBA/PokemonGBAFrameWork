/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of FadeOut.
	/// </summary>
	public class FadeOut:Comando
	{
		public const byte ID=0x37;
		public const int SIZE=2;
		
		byte velocidadDesvanecimiento;
		public FadeOut(byte velocidadDesvanecimiento)
		{
			VelocidadDesvanecimiento=velocidadDesvanecimiento;
		}
		public FadeOut(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FadeOut(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FadeOut(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Se desvanece la canción actual del Sappy";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FadeOut";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public byte VelocidadDesvanecimiento {
			get {
				return velocidadDesvanecimiento;
			}
			set {
				velocidadDesvanecimiento = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{VelocidadDesvanecimiento};
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			velocidadDesvanecimiento=ptrRom[offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=velocidadDesvanecimiento;
		}
		
	}
	
}
