/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 16:47
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonEntrenador.
	/// </summary>
	public class PokemonEntrenador
	{
		Word especie;//quinto byte
		byte ivs;//primer byte
		Word level;//segundo byte no se porque son dos bytes...tendria que ser 1...
		Word item;//septimo byte
		//a partir del byte 9//puede que los movimientos no esten cambiados por lo tanto no estarian...
		Word move1;
		Word move2;
		Word move3;
		Word move4;
		

		
		public Word Especie
		{
			get
			{
				return especie;
			}

			set
			{
				especie = value;
			}
		}

		public byte Ivs
		{
			get
			{
				return ivs;
			}

			set
			{
				ivs = value;
			}
		}

		public Word Nivel
		{
			get
			{
				return level;
			}

			set
			{
				level = value;
			}
		}

		public Word Item
		{
			get
			{
				return item;
			}

			set
			{
				item = value;
			}
		}

		public Word Move1
		{
			get
			{
				return move1;
			}

			set
			{
				move1 = value;
			}
		}

		public Word Move2
		{
			get
			{
				return move2;
			}

			set
			{
				move2 = value;
			}
		}

		public Word Move3
		{
			get
			{
				return move3;
			}

			set
			{
				move3 = value;
			}
		}

		public Word Move4
		{
			get
			{
				return move4;
			}

			set
			{
				move4 = value;
			}
		}
	}
}
