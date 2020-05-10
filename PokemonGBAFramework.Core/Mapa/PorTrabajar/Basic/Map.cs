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
		//en el archivo pone Maps pero ahora no se muy bien...
		public static readonly byte[] MuestraAlgoritmoRubiYZafiro= {0x09, 0x48, 0x41, 0x68, 0x00, 0x68, 0x58, 0x60};
		public static readonly int IndexRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 16;

		public static readonly byte[] MuestraAlgoritmoKanto = { 0x59, 0x60, 0x09, 0x48, 0x41, 0x68 };
		public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length - 16;

		public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x59, 0x60, 0x09, 0x48, 0x41, 0x68, 0x00, 0x68 };
		public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;

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

		public static Map Get(RomGba rom, int bank, int map,BankLoader bankLoader, OffsetRom offsetMapHeader = default) => Get(rom, (int)bankLoader.maps[bank][map],offsetMapHeader);

		public static Map Get(RomGba rom,int posMapa, OffsetRom offsetMapHeader=default)
		{
			return Get(rom, MapHeader.Get(rom,posMapa,offsetMapHeader));
		}
		public static Map Get(RomGba rom, MapHeader mapHeader)
		{
			Map mapa = new Map();
			mapa.MapHeader = mapHeader;

			mapa.MapConnections = ConnectionData.Get(rom, mapa.MapHeader);
			mapa.MapSprites = HeaderSprites.Get(rom, mapa.MapHeader.OffsetSprites);

			if (!Equals(mapa.MapSprites.OffsetNPC, default))
				mapa.MapNPCManager = new SpritesNPCManager(rom, mapa.MapSprites.OffsetNPC, mapa.MapSprites.NumNPC);
			if (!Equals(mapa.MapSprites.OffsetSigns, default))
				mapa.MapSignManager = new SpritesSignManager(rom, mapa.MapSprites.OffsetSigns, mapa.MapSprites.NumSigns);
			if(!Equals(mapa.MapSprites.OffsetTraps,default))
				mapa.MapTriggerManager = new TriggerManager(rom, mapa.MapSprites.OffsetTraps, mapa.MapSprites.NumTraps);
			if (!Equals(mapa.MapSprites.OffsetExits, default))
				mapa.MapExitManager = new SpritesExitManager(rom, mapa.MapSprites.OffsetExits, mapa.MapSprites.NumExits);

			mapa.MapData = MapData.Get(rom, mapa.MapHeader);
			mapa.MapTileData =  MapTileData.Get(rom, mapa.MapData);
			return mapa;
		}
		public static Map[] Get(RomGba rom,OffsetRom offsetTablaMapaHeader = default)
		{
			MapHeader[] headers = MapHeader.Get(rom, offsetTablaMapaHeader);
			Map[] mapas = new Map[headers.Length];
			for (int i = 0; i < mapas.Length; i++)
				mapas[i] = Get(rom, headers[i]);
			return mapas;
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static Zona GetZona(RomGba rom)
		{
			byte[] algoritmo;
			int index;
			if (rom.Edicion.EsEsmeralda)
			{
				algoritmo = MuestraAlgoritmoEsmeralda;
				index = IndexRelativoEsmeralda;

			}else if (rom.Edicion.EsKanto)
			{
				algoritmo = MuestraAlgoritmoKanto;
				index = IndexRelativoKanto;
			}
			else
			{
				algoritmo = MuestraAlgoritmoRubiYZafiro;
				index = IndexRelativoRubiYZafiro;
			}

			return Zona.Search(rom, algoritmo, index);
		}
	}

}
