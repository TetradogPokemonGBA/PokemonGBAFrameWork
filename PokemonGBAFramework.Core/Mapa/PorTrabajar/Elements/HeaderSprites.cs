﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class HeaderSprites
	{

		public int NumNPC { get; set; }
		public int NumWarps { get; set; }
		public int NumTriggers { get; set; }//traps o triggers?
		public int NumSigns { get; set; }

		public SpritesNPCManager MapNPCManager { get; set; }
		public SpritesExitManager MapExitManager { get; set; }
		public TriggerManager MapTriggerManager { get; set; }
		public SpritesSignManager MapSignManager { get; set; }
		

		public static HeaderSprites Get(RomGba rom, int offsetData)
		{
			OffsetRom offsetNPC;
			OffsetRom offsetExits;
			OffsetRom offsetTraps;
			OffsetRom offsetSigns;

			HeaderSprites headerSprites = new HeaderSprites();

			headerSprites.NumNPC = rom.Data[offsetData++];
			headerSprites.NumWarps = rom.Data[offsetData++];
			headerSprites.NumTriggers = rom.Data[offsetData++];
			headerSprites.NumSigns = rom.Data[offsetData++];

			offsetNPC = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetExits = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetTraps = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetSigns = new OffsetRom(rom, offsetData);

			if (!offsetNPC.IsEmpty)
			{
				headerSprites.MapNPCManager = new SpritesNPCManager(rom, offsetNPC.Integer, headerSprites.NumNPC);
			}

			if (!offsetExits.IsEmpty)
			{
				headerSprites.MapExitManager = new SpritesExitManager(rom, offsetExits.Integer, headerSprites.NumWarps);
			}
			if (!offsetTraps.IsEmpty)
			{
				headerSprites.MapTriggerManager = new TriggerManager(rom, offsetTraps.Integer, headerSprites.NumTriggers);
			}
			if (!offsetSigns.IsEmpty)
			{
				headerSprites.MapSignManager = new SpritesSignManager(rom, offsetSigns.Integer, headerSprites.NumSigns);
			}



			return headerSprites;
		}
	}
}
