using System.Collections.Generic;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public abstract class BaseManager<T> where T: SpriteBase,new()
	{
		public List<T> Items;

		public BaseManager(RomGba rom, int offset, int count)
		{
			Items = new List<T>();
			for (int i = 0; i < count; i++)
			{
				Items.Add(IGet(rom, offset));
				offset += SpriteSign.LENGTH;
			}
		}
		
	    protected abstract int LengthSingelItem { get; }
		public int Size => Items.Count * LengthSingelItem;

		public void Add(int x, int y)
		{
			Items.Add(IGetNew(x,y));
		}
		protected abstract T IGetNew(int x, int y);
		protected abstract T IGet(ScriptAndASMManager scriptManager,RomGba rom, int offset);
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

	}

}
