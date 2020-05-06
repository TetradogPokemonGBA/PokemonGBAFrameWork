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
//implementar en todos los comandos, esta interficie sirve para dar información al usuario asi que los objetos pueden tener rangos de valores y valores exactos a poder ser todos los posibles por el comando y si se puede tener en cuenta el contexto para descartar valores...por ejemplo compare hara que lastresult tenga unos valores pero si antes del compare se especifica el valor habran menos...se tiene que estudiar para que esté lo más acotado posible ;)
namespace PokemonGBAFramework.Core
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
