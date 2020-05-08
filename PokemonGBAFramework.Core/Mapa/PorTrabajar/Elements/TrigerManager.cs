using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class TriggerManager
    {
        public List<Trigger> MapTriggers { get; set; }
        private Map loadedMap;
        private int internalOffset = 0;
        private int originalSize;

        public int getSpriteIndexAt(int x, int y)
        {
            const int MARCAFIN = -1;
            int pos = MARCAFIN;

            for (int i = 0; i < MapTriggers.Count && pos == MARCAFIN; i++)
            {
                if (MapTriggers[i].BX == x && MapTriggers[i].BY == y)
                {
                    pos = i;
                }
            }

            return pos;
        }

        public void LoadTriggers(RomGba rom, Map m, int count, int offset)
        {
            internalOffset = offset;
            MapTriggers = new List<Trigger>();
            for (int i = 0; i < count; i++)
            {
                MapTriggers.Add(new Trigger(rom, offset));
                offset += Trigger.LENGTH;
            }
            originalSize = getSize();
            this.loadedMap = m;
        }


        public TriggerManager(RomGba rom, Map m, int offset, int count)
        {
            LoadTriggers(rom, m, count, offset);
        }

        public int getSize()
        {
            return MapTriggers.Count * Trigger.LENGTH;
        }

        public void Add(int x, int y)
        {
            MapTriggers.Add(new Trigger((byte)x, (byte)y));
        }

        public void Remove(int x, int y)
        {
            MapTriggers.RemoveAt(getSpriteIndexAt(x, y));
        }

        //public void save()
        //{
        //	rom.floodBytes(BitConverter.shortenPointer(internalOffset), rom.freeSpaceByte, originalSize);

        //	// TODO make this a setting, ie always repoint vs keep pointers
        //	int i = getSize();
        //	if (originalSize < getSize())
        //	{
        //		internalOffset = rom.findFreespace(DataStore.FreespaceStart, getSize());

        //		if (internalOffset < 0x08000000)
        //			internalOffset += 0x08000000;
        //	}

        //	loadedMap.mapSprites.pTraps = internalOffset & 0x1FFFFFF;
        //	loadedMap.mapSprites.bNumTraps = (byte)mapTriggers.size();

        //	rom.Seek(internalOffset);
        //	for (Trigger t : mapTriggers)
        //		t.save();
        //}
    }

}
