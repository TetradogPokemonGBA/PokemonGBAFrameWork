/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:09
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Comando.
	/// </summary>
	public abstract class Comando
	{
		internal Comando(RomGba rom,int offsetComando):this(rom.Data.Bytes,offsetComando)
		{}
		internal Comando(byte[] bytesComando,int offset)
		{
			unsafe{
				fixed(byte* ptRom=bytesComando)
					CargarCamando(ptRom,offset);
			}
		}
		internal unsafe Comando(byte* ptrRom,int offsetComando)
		{
			CargarCamando(ptrRom,offsetComando);
		}
		public abstract string Descripcion
		{
			get;
		}
		public abstract byte IdComando
		{
			get;
		}
		public abstract string Nombre
		{
			get;
		}
		public abstract int Size
		{
			get;
		}
		protected abstract unsafe  void CargarCamando(byte* ptrRom,int offsetComando);
		public void SetComando(RomGba rom,int offsetActualComando,params int[] parametrosExtra)
		{
			unsafe{
				fixed(byte* ptRom=rom.Data.Bytes)
					SetComando(ptRom+offsetActualComando,parametrosExtra);
			}
		}
		
		public byte[] GetComandoArray(params int[] parametrosExtra)
		{
			byte[] bytesComando=new byte[Size];
			unsafe{
				fixed(byte* ptComando=bytesComando)
					SetComando(ptComando,parametrosExtra);
				
			}
			return bytesComando;
		}
		protected abstract unsafe void SetComando(byte* ptrRomPosicionado,params int[] parametrosExtra);
		public override string ToString()
		{
			return Nombre;
		}
		
	}
}
