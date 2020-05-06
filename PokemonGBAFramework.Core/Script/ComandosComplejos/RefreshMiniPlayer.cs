/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 21/10/2017
 * Hora: 1:36
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of Refresh.
	/// </summary>
	public static class RefreshMiniPlayer
	{
		static readonly Variable callToRefresh;
		const int ASUMAR=0x08000000;
		static RefreshMiniPlayer()
		{
			callToRefresh=new Variable("Offset rutina");
			callToRefresh.Add(EdicionPokemon.RojoFuegoUsa10,0x5BE61,0x5BE75);
			callToRefresh.Add(EdicionPokemon.VerdeHojaUsa10,0x5BE61,0x5BE75);
			callToRefresh.Add(EdicionPokemon.RojoFuegoEsp10,0x5BF35);
			callToRefresh.Add(EdicionPokemon.VerdeHojaEsp10,0x5BF35);
			
			//investigando
			callToRefresh.Add(EdicionPokemon.ZafiroUsa10,0x590D1,0x590F1);
			callToRefresh.Add(EdicionPokemon.RubiUsa10,0x590CD,0x590ED);
			callToRefresh.Add(EdicionPokemon.EsmeraldaUsa10,0x8B441);
			
			callToRefresh.Add(EdicionPokemon.ZafiroEsp10,0x5950D);
			callToRefresh.Add(EdicionPokemon.RubiEsp10,0x59509);
			callToRefresh.Add(EdicionPokemon.EsmeraldaEsp10,0x8B455);
		}
		public static ComandosScript.CallAsm Comando(EdicionPokemon edicion)
		{
			return new ComandosScript.CallAsm((ASUMAR+Variable.GetVariable(callToRefresh,edicion)));
			
		}
		
		
	}
}
