using Gabriel.Cat.S.Extension;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public abstract class BaseManager<T> where T: SpriteBase,new()
	{
		public List<T> Items { get; set; }

		public BaseManager(RomGba rom, int offset, int count)
		{
			Items = new List<T>();
			for (int i = 0; i < count; i++)
			{
				Items.Add(IGet(rom, offset));
				offset += LengthSingelItem;
			}
		}
		
	    protected abstract int LengthSingelItem { get; }
		public int Size => Items.Count * LengthSingelItem;

		public T AddNew(int x, int y)
		{
			T newItem = IGetNew(x, y);
			Items.Add(newItem);
			return newItem;
		}
		protected abstract T IGetNew(int x, int y);
		protected abstract T IGet(RomGba rom, int offset);
		public void RemoveAt(int x, int y)
		{
			Items.RemoveAt(IndexOf(x, y));
		}
		public int IndexOf(int x, int y)
		{
			const int NOENCONTRADO = -1;
			int pos = NOENCONTRADO;

			for (int i = 0; i < Items.Count && pos == NOENCONTRADO; i++)
			{
				if (Items[i].X == x && Items[i].Y == y)
				{
					pos = i;
				}
				i++;
			}

			return pos;

		}
		public byte[] GetBytes() =>new byte[0].AddArray(Items.Select(item => item.GetBytes()).ToArray());

	}

}
