/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 16:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of DescripcionPokedex.
	/// </summary>
	public class DescripcionPokedex
	{
		//en construccion
		BloqueString nombreEspecie;//tiene un tamaño maximo en todas las versiones de 0xC
		BloqueString descripcion;
		//falta proporcion con entrenador y peso
		public DescripcionPokedex()
		{
		}
		//calcular OffsetInicio con el offset de la descripcion
	}
}
