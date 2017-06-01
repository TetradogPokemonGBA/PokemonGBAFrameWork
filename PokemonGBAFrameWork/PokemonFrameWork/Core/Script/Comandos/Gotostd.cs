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
			*ptrRomPosicionado=funcion;
			ptrRomPosicionado++;
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
	public class Gotostdif:Gotostd
	{
		
		public const byte ID=0xA;
		public const int SIZE=3;
		
		byte condicion;
		public Gotostdif(RomGba rom,int offset):base(rom,offset)
		{}
		public Gotostdif(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Gotostdif(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "Gotostdif";
			}
		}
		public override string Descripcion {
			get {
				return base.Descripcion+" si se cumple la condición";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

		public byte Condicion {
			get {
				return condicion;
			}
			set {
				condicion = value;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
			Condicion=ptrRom[++offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			*ptrRomPosicionado=Condicion;
			ptrRomPosicionado++;
		}
		
	}
	public class Callstdif:Gotostdif
	{
		public const byte ID=0xB;
		public const int SIZE=3;
		public Callstdif(RomGba rom,int offset):base(rom,offset)
		{}
		public Callstdif(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Callstdif(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Nombre {
			get {
				return "Callstdif";
			}
		}
		public override string Descripcion {
			get {
				return "llama a la función si se cumple la condición";
			}
		}
		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
	}
}
