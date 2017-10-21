/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 21/10/2017
 * Hora: 1:36
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Refresh.
	/// </summary>
	public static class Refresh
	{
		static readonly Variable callToRefresh;
		const uint ASUMAR=0x80000000;
		static Refresh()
		{
			callToRefresh=new Variable("Offset rutina");
			callToRefresh.Add(EdicionPokemon.RojoFuegoUsa,0x5BE61,0x5BE75);
			callToRefresh.Add(EdicionPokemon.VerdeHojaUsa,0x5BE61,0x5BE75);
			callToRefresh.Add(EdicionPokemon.RojoFuegoEsp,0x5BF35);
			callToRefresh.Add(EdicionPokemon.VerdeHojaEsp,0x5BF35);
			
			//investigando
			callToRefresh.Add(EdicionPokemon.ZafiroUsa,0x590D1,0x590F1);
			callToRefresh.Add(EdicionPokemon.RubiUsa,0x590CD,0x590ED);
			callToRefresh.Add(EdicionPokemon.EsmeraldaUsa,0x8B441);
			
			callToRefresh.Add(EdicionPokemon.ZafiroEsp,0x5950D);
			callToRefresh.Add(EdicionPokemon.RubiEsp,0x59509);
			callToRefresh.Add(EdicionPokemon.EsmeraldaEsp,0x8B455);
		}
		public static ComandosScript.CallAsm Comando(EdicionPokemon edicion,Compilacion compilacion)
		{
			return new ComandosScript.CallAsm((ASUMAR+(uint)Variable.GetVariable(callToRefresh,edicion,compilacion)));
			
		}
		
		
	}
}
