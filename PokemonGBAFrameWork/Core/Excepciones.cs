/*
 * Creado por SharpDevelop.
 * Usuario: pc
 * Fecha: 28/08/2016
 * Hora: 2:36
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of RomInvestigacionExcepcion.
	/// </summary>
	public class RomInvestigacionExcepcion:Exception
	{
		public RomInvestigacionExcepcion() : base("Rom incomatible por falta de investigación!") { }
	}
	public class InvalidRomFormat:Exception
	{
		public InvalidRomFormat():base("La rom no tiene el formato correcto"){}
	}
}
