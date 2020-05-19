/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:38
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of CheckSound.
	/// </summary>
	public class CheckSound:Comando
	{
		public const byte ID=0x30;
		public const string NOMBRE="CheckSound";
		public const string DESCRIPCION="Comprueba si esta reproduciendose el sonido,fanfare o canción";

		public CheckSound()
		{}
		public CheckSound(ScriptManager scriptManager,RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public CheckSound(ScriptManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe CheckSound(ScriptManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
		{}
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
