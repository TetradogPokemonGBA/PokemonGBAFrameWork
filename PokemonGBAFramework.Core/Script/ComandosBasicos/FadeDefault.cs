/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 02/06/2017
 * Hora: 13:52
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core.ComandosScript
{
	/// <summary>
	/// Description of FadeDefault.
	/// </summary>
	public class FadeDefault:Comando
	{
		public const byte ID=0x35;
        public const string NOMBRE = "FadeDefault";
        public const string DESCRIPCION = "Suavemente cambia a la canción por defecto del mapa";

        public FadeDefault()
		{}
		public FadeDefault(ScriptAndASMManager scriptManager, RomGba rom,int offset):base(scriptManager,rom,offset)
		{
		}
		public FadeDefault(ScriptAndASMManager scriptManager,byte[] bytesScript,int offset):base(scriptManager,bytesScript,offset)
		{}
		public unsafe FadeDefault(ScriptAndASMManager scriptManager,byte* ptRom,int offset):base(scriptManager,ptRom,offset)
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
