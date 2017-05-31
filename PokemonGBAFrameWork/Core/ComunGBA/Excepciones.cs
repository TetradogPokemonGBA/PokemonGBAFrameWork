/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 12:03
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of RomFaltaInvestigacionException.
	/// </summary>
	public class RomFaltaInvestigacionException:Exception
	{
		public RomFaltaInvestigacionException():base("Falta investigación")
		{
		}
	}
	public class FormatoRomNoReconocidoException:Exception
	{
		public FormatoRomNoReconocidoException():base("Formato no canonico")
		{
		}
	}
	public class RomSinEspacioException:Exception
	{
		public RomSinEspacioException():base("Rom sin espacio")
		{}
	}
}
