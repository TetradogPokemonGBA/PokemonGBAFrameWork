using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Structs
{
	public class MapID
	{
		public MapID(int bank, int map)
		{
			this.Bank = bank;
			this.Map = map;
		}

		public int Bank { get; set; }
		public int Map { get; set; }

		public override bool Equals(Object b)
		{
			MapID other = b as MapID;
			return other != null&& other.Bank == this.Bank&& other.Map == this.Map;
			
		}
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

}
