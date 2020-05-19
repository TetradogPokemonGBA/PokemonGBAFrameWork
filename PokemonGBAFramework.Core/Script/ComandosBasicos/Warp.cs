/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:08
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
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
		Word coordenadaX;
		Word coordenadaY;
		
		public Warp(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
		{
			Bank = bank;
			Map = map;
			Exit = exit;
			CoordenadaX = coordenadaX;
			CoordenadaY = coordenadaY;
		}
		public Warp(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public Warp(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Warp(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public Word CoordenadaX {
			get {
				return coordenadaX;
			}
			set {
				coordenadaX = value;
			}
		}

		public Word CoordenadaY {
			get {
				return coordenadaY;
			}
			set {
				coordenadaY = value;
			}
		}
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Bank, Map, Exit, CoordenadaX, CoordenadaY };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			bank = ptrRom[offsetComando++];
			map = ptrRom[offsetComando++];
			exit = ptrRom[offsetComando++];
			coordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			coordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			ptrRomPosicionado++;
			*ptrRomPosicionado = bank;
			ptrRomPosicionado++;
			*ptrRomPosicionado = map;
			ptrRomPosicionado++;
			*ptrRomPosicionado = exit;
			ptrRomPosicionado++;
			Word.SetData(data, , coordenadaX);
 
			Word.SetData(data, , coordenadaY);
		}
	}
	public class WarpWalk:Warp
	{
		public const byte ID = 0x3B;

		public WarpWalk(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public WarpWalk(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public WarpWalk(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe WarpWalk(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public WarpTeleport(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public WarpTeleport(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public WarpTeleport(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe WarpTeleport(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public Warp3(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public Warp3(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public Warp3(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Warp3(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public Warp4(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public Warp4(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public Warp4(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Warp4(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
		public Warp5(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public Warp5(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public Warp5(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe Warp5(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public WarpMuted(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public WarpMuted(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public WarpMuted(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe WarpMuted(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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

		public SetWarpplace(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public SetWarpplace(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public SetWarpplace(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe SetWarpplace(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
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
