/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 31/05/2017
 * Hora: 19:38
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork.Script
{
	/// <summary>
	/// Description of IDeclaracion.
	/// </summary>
	public interface IDeclaracion
	{
		byte[] GetDeclaracion(RomGba rom,params object[] parametrosExtra);
	}
}
