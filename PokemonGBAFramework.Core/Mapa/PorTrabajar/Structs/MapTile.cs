using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Structs
{
	public class MapTile : IClonable<MapTile>, ICloneable
	{
        public const int LENGTH=2;

        public int ID { get; set; }
		public int Meta { get; set; }


		public MapTile(int id=0, int meta=0)
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
