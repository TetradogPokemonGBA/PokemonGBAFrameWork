/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:28
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WarpHole.
	/// </summary>
	public class WarpHole:Comando
	{
		public const byte ID = 0x3C;

		public const int SIZE = 3;
		
		byte bank;
		byte map;
		
		public WarpHole(byte bank, byte map)
		{
			Bank = bank;
			Map = map;
		}

		public WarpHole(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}

		public WarpHole(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}

		public unsafe WarpHole(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}

		public override string Nombre {
			get {
				return "WarpHole";
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}

		public override string Descripcion {
			get {
				return "Cambia al jugador a otro mapa con el efecto agujero";
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
		protected override System.Collections.Generic.IList<object> GetParams()
		{
			return new Object[]{ Bank, Map };
		}
		protected unsafe override void CargarCamando(ScriptAndASMManager scriptManager,byte* ptrRom, int offsetComando)
		{
			bank = ptrRom[offsetComando++];
			map = ptrRom[offsetComando];
		}
		public override byte[] GetBytesTemp()
		{
			byte[] data=new byte[Size];
			 data[0]=IdComando;
			*ptrRomPosicionado = bank;
			 data[0]=IdComando;
			*ptrRomPosicionado = map;
		}

	}
}
