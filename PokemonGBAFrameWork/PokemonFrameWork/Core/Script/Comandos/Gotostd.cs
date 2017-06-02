/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 01/06/2017
 * Hora: 1:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Gotostd.
	/// </summary>
	public class Gotostd:Comando
	{
		public const byte ID=0x8;
		public const int SIZE=2;
		
		byte funcion;
		public Gotostd(RomGba rom,int offset):base(rom,offset)
		{}
		public Gotostd(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Gotostd(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Salta a la función compilada";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Nombre {
			get {
				return "Gotostd";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public byte Funcion {
			get {
				return funcion;
			}
			set {
				funcion = value;
			}
		}
		#region implemented abstract members of Comando

		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			funcion=ptrRom[offsetComando];
		}

		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado,parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=funcion;
		}


		#endregion
	}
	public class Callstd:Gotostd
	{
		public const byte ID=0x9;
		public Callstd(RomGba rom,int offset):base(rom,offset)
		{}
		public Callstd(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Callstd(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "CallStd";
			}
		}
		public override string Descripcion {
			get {
				return "Llama a la función compilada";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}
	}
	
	
}
