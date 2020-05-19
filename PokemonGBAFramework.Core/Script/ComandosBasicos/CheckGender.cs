/*
 * Usuario: Pikachu240
 * Licencia GNU GPL V3
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckGender.
	/// </summary>
	public class CheckGender:Comando
	{
		public const byte ID = 0xA0;
		public const string NOMBRE="CheckGender";
		public const string DESCRIPCION="Comprueba si el jugador es chico o chica y lo guarda en LASTRESULT.";
  
		public CheckGender()
		{
   
		}
   
		public CheckGender(ScriptAndASMManager scriptManager,RomGba rom, int offset)
			 : base(scriptManager,rom, offset)
		{
		}
		public CheckGender(ScriptAndASMManager scriptManager,byte[] bytesScript, int offset)
			: base(scriptManager,bytesScript, offset)
		{
		}
		public unsafe CheckGender(ScriptAndASMManager scriptManager,byte* ptRom, int offset)
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
