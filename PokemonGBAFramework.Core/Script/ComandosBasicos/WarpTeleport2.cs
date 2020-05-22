/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of WarpTeleport2.
	/// </summary>
	public class WarpTeleport2:Warp
	{
		public new const byte ID = 0xD1;
		public new const string NOMBRE = "WarpTeleport2";
		public new const string DESCRIPCION = "Transporta al jugador al mapa especificado con el efecto de teletransportación";

		public WarpTeleport2() { }
		public WarpTeleport2(Byte bancoAIr, Byte mapaAIr, Byte salidaAIr, Word coordenadaX, Word coordenadaY) : base(bancoAIr, mapaAIr, salidaAIr, coordenadaX, coordenadaY)
		{


		}

		public WarpTeleport2(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public WarpTeleport2(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe WarpTeleport2(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
			: base(scriptManager,ptRom, offset)
		{
		}
		public override string Descripcion {
			get {
				return DESCRIPCION;
			}
		}

		public override byte IdComando {
			get {
				return ID;
			}
		}
		public override string Nombre {
			get {
				return NOMBRE;
			}
		}

	
	}
}
