using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Structs
{
	public class MapTile : IClonable<MapTile>, ICloneable
	{
		public int ID { get; set; }
		public int Meta { get; set; }

		public void SetID(int i)
		{
			ID = i;

		}
		public MapTile(int id, int meta)
		{
			ID = id;
			Meta = meta;
		}


		public Object Clone()
		{
			return Clon();
		}
		public MapTile Clon()
		{
			return new MapTile(ID, Meta);
		}
	}

}
