using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class HeaderSprites
	{

		public int NumNPC=>MapNPCManager.Items.Count;
		public int NumWarps =>MapExitManager.Items.Count;
		public int NumTriggers=>MapTriggerManager.Items.Count;//traps o triggers?
		public int NumSigns =>MapSignManager.Items.Count;

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

			int numNPC;
			int numExits;
			int numTraps;
			int numSigns;
			
			HeaderSprites headerSprites = new HeaderSprites();

			numNPC= rom.Data[offsetData++];
			numExits = rom.Data[offsetData++];
			numTraps = rom.Data[offsetData++];
			numSigns = rom.Data[offsetData++];

			offsetNPC = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetExits = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetTraps = new OffsetRom(rom, offsetData);
			offsetData += OffsetRom.LENGTH;
			offsetSigns = new OffsetRom(rom, offsetData);

			if (!offsetNPC.IsEmpty)
			{
				headerSprites.MapNPCManager = new SpritesNPCManager(rom, offsetNPC.Integer, numNPC);
			}

			if (!offsetExits.IsEmpty)
			{
				headerSprites.MapExitManager = new SpritesExitManager(rom, offsetExits.Integer, numExits);
			}
			if (!offsetTraps.IsEmpty)
			{
				headerSprites.MapTriggerManager = new TriggerManager(rom, offsetTraps.Integer, numTraps);
			}
			if (!offsetSigns.IsEmpty)
			{
				headerSprites.MapSignManager = new SpritesSignManager(rom, offsetSigns.Integer, numSigns);
			}



			return headerSprites;
		}
	}
}
