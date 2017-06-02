/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:35
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Sound.
	/// </summary>
	public class Sound:Comando
	{
		public const byte ID=0x2F;
		public const int SIZE=0x3;

		short sonido;
		public Sound(RomGba rom,int offset):base(rom,offset)
		{
		}
		public Sound(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe Sound(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Reproduce el sonido";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Sound";
			}
		}

		public short Sonido {
			get {
				return sonido;
			}
			set {
				sonido = value;
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			sonido=Word.GetWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,sonido);
		}
	}
	public class FanFare:Sound
	{
		public const byte ID=0x31;

		public FanFare(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FanFare(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FanFare(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Reproduce una cancion Sappy como un fanfare";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "Fanfare";
			}
		}
	}
	public class PlaySong2:Sound
	{
		public const byte ID=0x34;

		public PlaySong2(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PlaySong2(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PlaySong2(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Cambia a otra cancion Sappy";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PlaySong2";
			}
		}
	}
	public class FadeSong:Sound
	{
		public const byte ID=0x36;

		public FadeSong(RomGba rom,int offset):base(rom,offset)
		{
		}
		public FadeSong(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe FadeSong(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Suavemente cambia a otra cancion Sappy";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "FadeSong";
			}
		}
	}
	public class PlaySong:Sound
	{
		public const int ID=0x33;
		public const int SIZE=4;
		
		byte desconocido;
		public PlaySong(RomGba rom,int offset):base(rom,offset)
		{
		}
		public PlaySong(byte[] bytesScript,int offset):base(bytesScript,offset)
		{}
		public unsafe PlaySong(byte* ptRom,int offset):base(ptRom,offset)
		{}
		public override string Descripcion {
			get {
				return "Cambia a otra cancion Sappy";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return "PlaySong";
			}
		}
		public override int Size {
			get {
				return SIZE;
			}
		}
		public byte Desconocido {
			get {
				return desconocido;
			}
			set {
				desconocido = value;
			}
		}
		protected unsafe  override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			base.CargarCamando(ptrRom, offsetComando);
			desconocido=ptrRom[offsetComando+Word.LENGTH];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado+=SIZE-1;
			*ptrRomPosicionado=desconocido;
		}
	}
}
