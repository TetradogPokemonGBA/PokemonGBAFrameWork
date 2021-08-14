using PokemonGBAFramework.Core.Mapa.Basic.Render;
using System;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class Block
	{
		public int LADO = 2;
		public Tile[,] ThirdLayer { get; set; }
		public Tile[,] Foreground { get; set; }
		public Tile[,] Background { get; set; }
		public int BlockID { get; set; }
		public long BackgroundMetaData { get; set; }

		public Block(int blockID, long bgBytes)
		{
			BlockID = blockID;
			BackgroundMetaData = bgBytes;


			ThirdLayer = new Tile[LADO, LADO];
			Foreground = new Tile[LADO, LADO];
			Background = new Tile[LADO, LADO];
			for (int i = 0; i < LADO; i++)
			{
				for (int j = 0; j < LADO; j++)
				{
					Foreground[i, j] = new Tile(0, 0, false, false);
					Background[i, j] = new Tile(0, 0, false, false);
				}
			}
		}

		public void SetTile(int x, int y, Tile t)
		{
			try
			{
				if (x < LADO)
					Background[x, y] = t.Clon();
				else
					Foreground[x - LADO, y] = t.Clon();
			}
			catch (Exception e) { Console.WriteLine(e.Message); }
		}

		public Tile Get(int x, int y)
		{
			Tile tile;
			try
			{
				if (x < LADO)
					tile= Background[x, y].Clon();
				else
					tile= Foreground[x - LADO, y].Clon();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				tile= new Tile(0, 0, false, false);
			}
			return tile;
		}
		public static Block Get(RomGba rom, BlockRenderer render, int blockID)
		{
			return new Block(blockID, render.getBehaviorByte(rom, blockID));
		}
		
	}
}