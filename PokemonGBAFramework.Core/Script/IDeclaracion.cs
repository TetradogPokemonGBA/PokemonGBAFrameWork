/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:38
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of IDeclaracion.
	/// </summary>
	public interface IDeclaracion
	{
		byte[] GetDeclaracion(byte[] data,params object[] parametrosExtra);
	}
    public static class ExtensionIDeclaracion
    {
        public static byte[] GetDeclaracion(this IDeclaracion objDeclaracion,RomGba rom,params object[] parametrosExtra)
        {
            return objDeclaracion.GetDeclaracion(rom.Data.Bytes, parametrosExtra);
        }
    }
}
