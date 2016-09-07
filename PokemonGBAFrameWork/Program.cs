/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 18:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	class Program
	{
		public static void Main(string[] args)
		{
			//para hacer testing
			string path=args.Length!=0?args[0]:"Pokémon  Verde Hoja.gba";
			new RomGBA(new System.IO.FileInfo(path)).BackUp();
		}
	}
}