/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 20:48
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Growl.
	/// </summary>
	public class Growl:BloqueSonido
	{
		public  const string HEADER="303C0000";
		public static readonly byte[] BytesHeader=(Hex)HEADER;
		public Growl():base(HEADER)
		{
		}
		private Growl(BloqueSonido bloque):base(bloque)
		{}
		public static Growl GetGrowl(RomGba rom,int ordenNacional)
		{
			return new Growl(BloqueSonido.GetBloqueSonido(rom,BytesHeader,ordenNacional));
			
		}
	}
}
