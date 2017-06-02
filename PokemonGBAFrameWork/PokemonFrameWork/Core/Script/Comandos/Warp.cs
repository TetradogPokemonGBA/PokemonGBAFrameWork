/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:08
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of Warp.
	/// </summary>
	public class Warp:Comando
	{
		public const byte ID = 0x39;
		public const int SIZE = 8;

		byte bank;
		byte map;
		byte exit;
		short coordenadaX;
		short coordenadaY;
		public Warp(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Warp(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Warp(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Warp";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa";
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}
		
		public byte Bank {
			get {
				return bank;
			}
			set {
				bank = value;
			}
		}

		public byte Map {
			get {
				return map;
			}
			set {
				map = value;
			}
		}

		public byte Exit {
			get {
				return exit;
			}
			set {
				exit = value;
			}
		}

		public short CoordenadaX {
			get {
				return coordenadaX;
			}
			set {
				coordenadaX = value;
			}
		}

		public short CoordenadaY {
			get {
				return coordenadaY;
			}
			set {
				coordenadaY = value;
			}
		}
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			bank=ptrRom[offsetComando++];
			map=ptrRom[offsetComando++];
			exit=ptrRom[offsetComando++];
			coordenadaX=Word.GetWord(ptrRom,offsetComando);
			offsetComando+=Word.LENGTH;
			coordenadaY=Word.GetWord(ptrRom,offsetComando);
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado=bank;
			ptrRomPosicionado++;
			*ptrRomPosicionado=map;
			ptrRomPosicionado++;
			*ptrRomPosicionado=exit;
			ptrRomPosicionado++;
			Word.SetWord(ptrRomPosicionado,coordenadaX);
			ptrRomPosicionado+=Word.LENGTH;
			Word.SetWord(ptrRomPosicionado,coordenadaY);
		}
	}
	public class WarpWalk:Warp
	{
		public const byte ID = 0x3B;

		public WarpWalk(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public WarpWalk(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe WarpWalk(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "WarpWalk";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa con el sonido de anadar";
			}
		}
	}
	public class WarpTeleport:Warp
	{
		public const byte ID = 0x3D;

		public WarpTeleport(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public WarpTeleport(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe WarpTeleport(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "WarpTeleport";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa con el efecto teletransportación";
			}
		}
	}
	public class Warp3:Warp
	{
		public const byte ID = 0x3E;

		public Warp3(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Warp3(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Warp3(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Warp3";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa";
			}
		}
	}
	public class Warp4:Warp
	{
		public const byte ID = 0x40;

		public Warp4(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Warp4(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Warp4(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Warp4";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa";
			}
		}
	}
	public class Warp5:Warp
	{
		public const byte ID = 0x41;

		public Warp5(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public Warp5(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe Warp5(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "Warp5";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa";
			}
		}
	}
	public class WarpMuted:Warp
	{
		public const byte ID = 0x3A;

		public WarpMuted(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public WarpMuted(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe WarpMuted(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "WarpMuted";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Mueve al player a otro mapa sin hacer el sonar el efecto";
			}
		}
	}
	public class SetWarpplace:Warp
	{
		public const byte ID = 0x3F;

		public SetWarpplace(RomGba rom, int offset) : base(rom, offset)
		{
		}

		public SetWarpplace(byte[] bytesScript, int offset) : base(bytesScript, offset)
		{
		}

		public unsafe SetWarpplace(byte* ptRom, int offset) : base(ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "SetWarpplace";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "sets the place a warp that lead to warp 127 of map 127.127 warps the player";
			}
		}
	}
	
}
