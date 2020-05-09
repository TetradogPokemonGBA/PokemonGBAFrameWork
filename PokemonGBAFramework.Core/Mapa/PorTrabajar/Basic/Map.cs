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
		public MapData MapData { get; set; }
		public MapTileData MapTileData { get; set; }
		public MapHeader MapHeader { get; set; }
		public ConnectionData MapConnections { get; set; }
		public HeaderSprites MapSprites { get; set; }
		public SpritesNPCManager MapNPCManager { get; set; }
		public SpritesSignManager MapSignManager { get; set; }
		public SpritesExitManager MapExitManager { get; set; }
		public TriggerManager MapTriggerManager { get; set; }
		public OverworldSpritesManager OverworldSpritesManager { get; set; }
		public OverworldSprites[] EventSprites { get; set; }

		public static Map Get(RomGba rom, int bank, int map,BankLoader bankLoader) => Get(rom, bankLoader.maps[bank][map]);

		public static Map Get(RomGba rom, int dataOffset)
		{
			Map mapa = new Map();
			mapa.MapHeader = MapHeader.Get(rom, new OffsetRom(dataOffset));

			mapa.MapConnections = ConnectionData.Get(rom, mapa.MapHeader);
			mapa.MapSprites = HeaderSprites.Get(rom, mapa.MapHeader.OffsetSprites);

			mapa.MapNPCManager = new SpritesNPCManager(rom, mapa.MapSprites.OffsetNPC, mapa.MapSprites.NumNPC);
			mapa.MapSignManager = new SpritesSignManager(rom, mapa.MapSprites.OffsetSigns, mapa.MapSprites.NumSigns);
			mapa.MapTriggerManager = new TriggerManager(rom, mapa.MapSprites.OffsetTraps, mapa.MapSprites.NumTraps);
			mapa.MapExitManager = new SpritesExitManager(rom, mapa.MapSprites.OffsetExits, mapa.MapSprites.NumExits);

			mapa.MapData = MapData.Get(rom, mapa.MapHeader);
			mapa.MapTileData =  MapTileData.Get(rom, mapa.MapData);
			return mapa;
		}
	}

}
