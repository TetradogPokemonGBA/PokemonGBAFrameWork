/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 15:28
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.ComandosScript
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

		public WarpHole(RomGba rom, int offset)
			: base(rom, offset)
		{
		}

		public WarpHole(byte[] bytesScript, int offset)
			: base(bytesScript, offset)
		{
		}

		public unsafe WarpHole(byte* ptRom, int offset)
			: base(ptRom, offset)
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
		protected unsafe override void CargarCamando(byte* ptrRom, int offsetComando)
		{
			bank = ptrRom[offsetComando++];
			map = ptrRom[offsetComando];
		}
		protected unsafe override void SetComando(byte* ptrRomPosicionado, params int[] parametrosExtra)
		{
			base.SetComando(ptrRomPosicionado, parametrosExtra);
			ptrRomPosicionado++;
			*ptrRomPosicionado = bank;
			ptrRomPosicionado++;
			*ptrRomPosicionado = map;
		}

	}
}
