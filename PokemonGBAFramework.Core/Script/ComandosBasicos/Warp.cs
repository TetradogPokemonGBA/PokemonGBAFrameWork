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
		public new const int SIZE = Comando.SIZE+1+1+1+Word.LENGTH+Word.LENGTH;
		public const string NOMBRE = "Warp";
		public const string DESCRIPCION = "Mueve al player a otro mapa";

		public Warp() { }
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
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override int Size {
			get {
				return SIZE;
			}
		}

		public byte Bank { get; set; }

		public byte Map { get; set; }

		public byte Exit { get; set; }

		public Word CoordenadaX { get; set; }

		public Word CoordenadaY { get; set; }
		public override System.Collections.Generic.IList<Gabriel.Cat.S.Utilitats.Propiedad> GetParams()
		{
			return new Gabriel.Cat.S.Utilitats.Propiedad[]{ new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Bank)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Map)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(Exit)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(CoordenadaX)), new Gabriel.Cat.S.Utilitats.Propiedad(this, nameof(CoordenadaY)) };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			Bank = ptrRom[offsetComando++];
			Map = ptrRom[offsetComando++];
			Exit = ptrRom[offsetComando++];
			CoordenadaX = new Word(ptrRom, offsetComando);
			offsetComando += Word.LENGTH;
			CoordenadaY = new Word(ptrRom, offsetComando);
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			
			data[0]=IdComando;
			data[1] = Bank;
			data[2] = Map;
			data[3]= Exit;
			Word.SetData(data,4, CoordenadaX);
			Word.SetData(data,6, CoordenadaY);

			return data;
		}
	}
	public class WarpWalk:Warp
	{
		public new const byte ID = 0x3B;
		public new const string NOMBRE = "WarpWalk";
		public new const string DESCRIPCION = "Mueve al player a otro mapa con el sonido de anadar";

		public WarpWalk() { }
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
				return NOMBRE;
				
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class WarpTeleport:Warp
	{
		public new const byte ID = 0x3D;
		public new const string NOMBRE = "WarpTeleport";
		public new const string DESCRIPCION = "Mueve al player a otro mapa con el efecto teletransportación";
		public WarpTeleport() { }
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
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class Warp3:Warp
	{
		public new const byte ID = 0x3E;
		public new const string NOMBRE = "Warp3";
		public new const string DESCRIPCION = "Mueve al player a otro mapa";
		public Warp3() { }
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
				return NOMBRE;
					
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class Warp4:Warp
	{
		public new const byte ID = 0x40;
		public new const string NOMBRE = "Warp4";
		public new const string DESCRIPCION = "Mueve al player a otro mapa";
		public Warp4() { }
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
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class Warp5:Warp
	{
		public new const byte ID = 0x41;
		public new const string NOMBRE = "Warp5";
		public new const string DESCRIPCION = "Mueve al player a otro mapa";
		public Warp5() { }
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
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class WarpMuted:Warp
	{
		public new const byte ID = 0x3A;
		public new const string NOMBRE = "WarpMuted";
		public new const string DESCRIPCION = "Mueve al player a otro mapa sin hacer el sonar el efecto";
		public WarpMuted() { }
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
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	public class SetWarpPlace:Warp
	{
		public new const byte ID = 0x3F;
		public new const string NOMBRE = "SetWarpplace";
		public new const string DESCRIPCION = "sets the place a warp that lead to warp 127 of map 127.127 warps the player";
		public SetWarpPlace() { }
		public SetWarpPlace(byte bank, byte map, byte exit, Word coordenadaX, Word coordenadaY)
			: base(bank, map, exit, coordenadaX, coordenadaY)
		{

		}
		public SetWarpPlace(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public SetWarpPlace(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe SetWarpPlace(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return NOMBRE;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}
	}
	
}
