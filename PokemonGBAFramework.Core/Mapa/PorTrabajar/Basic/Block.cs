using PokemonGBAFramework.Core.Mapa.Basic.Render;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Block
	{
		public Tile[,] tilesThirdLayer;
		public Tile[,] tilesForeground;
		public Tile[,] tilesBackground;
		public int blockID;
		public long backgroundMetaData;

    	public Block(int blockID, long bgBytes)
		{
			this.blockID = blockID;
			this.backgroundMetaData = bgBytes;


			tilesThirdLayer = new Tile[2, 2];
			tilesForeground = new Tile[2, 2];
			tilesBackground = new Tile[2, 2];
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					tilesForeground[i, j] = new Tile(0, 0, false, false);
					tilesBackground[i, j] = new Tile(0, 0, false, false);
				}
			}
		}

		public void setTile(int x, int y, Tile t)
		{
			try
			{
				if (x < 2)
					tilesBackground[x, y] = t.getNewInstance();
				else
					tilesForeground[x - 2, y] = t.getNewInstance();
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}

		public Tile Get(int x, int y)
		{
			Tile tile;
			try
			{
				if (x < 2)
					tile= tilesBackground[x, y].getNewInstance();
				else
					tile= tilesForeground[x - 2, y].getNewInstance();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				tile= new Tile(0, 0, false, false);
			}
			return tile;
		}

		public void setMetaData(int bytes)
		{
			backgroundMetaData = bytes;
		}
		public static Block Get(RomGba rom, BlockRenderer render, int blockID)
		{
			return new Block(blockID, render.getBehaviorByte(rom, blockID));
		}
		
	}
}