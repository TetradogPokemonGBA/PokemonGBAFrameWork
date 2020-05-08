using Gabriel.Cat.S.Drawing;
using PokemonGBAFramework.Core.Mapa.Basic.Render;
using PokemonGBAFramework.Core.Mapa.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Map 
	{
	private MapData mapData;
	private MapTileData mapTileData;
	public MapHeader mapHeader;
	public ConnectionData mapConnections;
	public HeaderSprites mapSprites;

	public SpritesNPCManager mapNPCManager;
	public SpritesSignManager mapSignManager;
	public SpritesExitManager mapExitManager;
	public TriggerManager mapTriggerManager;
	public OverworldSpritesManager overworldSpritesManager;
	public int dataOffset = 0;
	public OverworldSprites[] eventSprites;
	public bool isEdited;

	public Map(RomGba rom, int bank, int map, int localTSize, int engine = 0) : this(rom, (int)BankLoader.maps[bank][map],localTSize,engine){	}

	public Map(RomGba rom, int dataOffset,int localTSize,int engine=0)
	{
		this.dataOffset = dataOffset;
		mapHeader = new MapHeader(rom, dataOffset);
		
		mapConnections = new ConnectionData(rom, mapHeader);
		mapSprites = new HeaderSprites(rom, (int)mapHeader.OffsetSprites);

		mapNPCManager = new SpritesNPCManager(rom, this, (int)mapSprites.PNPC, mapSprites.BNumNPC);
		mapSignManager = new SpritesSignManager(rom, this, (int)mapSprites.PSigns, mapSprites.BNumSigns);
		mapTriggerManager = new TriggerManager(rom, this, (int)mapSprites.PTraps, mapSprites.BNumTraps);
		mapExitManager = new SpritesExitManager(rom, this, (int)mapSprites.PExits, mapSprites.BNumExits);

		mapData = new MapData(rom, mapHeader,localTSize,engine);
		mapTileData = new MapTileData(rom, mapData);
		isEdited = true;
	}

	public MapData getMapData()
	{
		return mapData;
	}

	public MapTileData getMapTileData()
	{
		return mapTileData;
	}


	//public static Bitmap renderMap(RomGba rom,int bank, int map)
	//{
	//	return renderMap(rom,new Map(rom, bank, map), true);
	//}

	//public static Bitmap renderMap(RomGba rom,Map map,BlockRenderer renderer, bool full)
	//{
	//	TilesetCache.switchTileset(rom,map);
	//	renderer.setGlobalTileset(TilesetCache.Get(rom,map.getMapData().GlobalTileSetPtr));
	//	renderer.setLocalTileset(TilesetCache.Get(rom,map.getMapData().LocalTileSetPtr));


	//	Bitmap imgBuffer = new Bitmap(8, 8);
	//	Image tiles;
	//		Collage collage = new Collage();
	//		Bitmap bmpTile;
	//	try
	//	{
	//		imgBuffer = new Bitmap((int)map.getMapData().MapWidth * 16,
	//				(int)map.getMapData().MapHeight * 16);
	//			Graphics gcBuff;//= imgBuffer.();

	//		for (int y = 0; y < map.getMapData().MapHeight; y++)
	//		{
	//			for (int x = 0; x < map.getMapData().MapWidth; x++)
	//			{
	//				gcBuff = imgBuffer.getGraphics();
	//				int TileID = (map.getMapTileData().getTile(x, y).getID());
	//				int srcX = (TileID % TileEditorPanel.editorWidth) * 16;
	//				int srcY = (TileID / TileEditorPanel.editorWidth) * 16;
	//				gcBuff.drawImage(((Bitmap)(tiles)).getSubimage(srcX, srcY, 16, 16), x * 16, y * 16, null);
	//				//new org.zzl.minegaming.GBAUtils.PictureFrame(((BufferedImage)(tiles)).getSubimage(srcX, srcY, 16, 16)).show();
	//			}
	//		}
	//	}
	//	catch (Exception e)
	//	{
	//		Console.WriteLine($"Error rendering map./n{e.Message}");
	//		//imgBuffer.SetColor(Color.Red);
	//		//imgBuffer.getGraphics().fillRect(0, 0, 8, 8);
	//	}

	//	return imgBuffer;
	//}
}

}
