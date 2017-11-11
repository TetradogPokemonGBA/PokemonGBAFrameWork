/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 11/11/2017
 * Hora: 14:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of ILastResult.
	/// </summary>
	public interface ILastResult
	{
		/// <summary>
		/// Es el valor o valores que puede tener la variable LastResult después de leer el comando
		/// </summary>
		IList<object> LastResult
		{
			get;
		}
	}
}
