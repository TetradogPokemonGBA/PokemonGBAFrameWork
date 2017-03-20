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
		ushort especie;//quinto byte
		byte ivs;//primer byte
		ushort level;//segundo byte no se porque son dos bytes...tendria que ser 1...
		ushort item;//septimo byte
		//a partir del byte 9//puede que los movimientos no esten cambiados por lo tanto no estarian...
		ushort move1;
		ushort move2;
		ushort move3;
		ushort move4;
		

		
		public ushort Especie
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

		public ushort Nivel
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

		public ushort Item
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

		public ushort Move1
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

		public ushort Move2
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

		public ushort Move3
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

		public ushort Move4
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
