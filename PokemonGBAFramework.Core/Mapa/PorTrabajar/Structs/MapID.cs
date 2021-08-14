using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Structs
{
	public class MapID
	{
		public MapID(int bank, int map)
		{
			Bank = bank;
			Map = map;
		}

		public int Bank { get; set; }
		public int Map { get; set; }

		public override bool Equals(object b)
		{
			MapID other = b as MapID;
			return !ReferenceEquals(other, default) && other.Bank == Bank && other.Map == Map;
			
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

}
