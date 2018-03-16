/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 20:36
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Utilitats;
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Cry.
	/// </summary>
	public class Cry:BloqueSonido
	{
		public  const string HEADER="203C0000";
		public static readonly byte[] BytesHeader=(Hex)HEADER;
		public Cry():base(HEADER)
		{
		}
		private Cry(BloqueSonido bloque):base(bloque)
		{}
		public static Cry GetCry(RomGba rom,int ordenNacional)
		{
			return new Cry(BloqueSonido.GetBloqueSonido(rom,BytesHeader,ordenNacional));
			
		}
	}
}
