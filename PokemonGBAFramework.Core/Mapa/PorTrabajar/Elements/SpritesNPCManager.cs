using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class SpritesNPCManager
    {
        public List<SpriteNPC> mapNPCs;
        private int internalOffset;
        private int originalSize;
        private Map loadedMap;

        public int[] GetSpriteIndices()
        {
            int i = 0;
            int[] indices = new int[mapNPCs.Count];
            for (i = 0; i < mapNPCs.Count; i++)
            {
                indices[i] = mapNPCs[i].BSpriteSet;
            }
            return indices;
        }

        public int getSpriteIndexAt(int x, int y)
        {
            const int MARCAFIN = -1;
            int pos = MARCAFIN;
            int i;
            for (i = 0; i < mapNPCs.Count && pos == MARCAFIN; i++)
            {
                if (mapNPCs[i].BX == x && mapNPCs[i].BY == y)
                {
                    pos = i;
                }
            }

            return pos;

        }

        public SpritesNPCManager(RomGba rom,Map m, int offset, int count)
        {
            internalOffset = offset;
            loadedMap = m;
            mapNPCs = new List<SpriteNPC>();
            int i = 0;
            for (i = 0; i < count; i++)
            {
                mapNPCs.Add(new SpriteNPC(rom,offset));
                offset += SpriteNPC.LENGTH;
            }
            originalSize = getSize();
        }

        public int getSize()
        {
            return mapNPCs.Count* SpriteNPC.LENGTH;
        }

        public void add(int x, int y)
        {
            mapNPCs.Add(new SpriteNPC((byte)x, (byte)y));
        }

        public void remove(int x, int y)
        {
            mapNPCs.RemoveAt(getSpriteIndexAt(x, y));
        }

        //public void save()
        //{
        //    rom.floodBytes(BitConverter.shortenPointer(internalOffset), rom.freeSpaceByte, originalSize);

        //    // TODO make this a setting, ie always repoint vs keep pointers
        //    int i = getSize();
        //    if (originalSize < getSize())
        //    {
        //        internalOffset = rom.findFreespace(DataStore.FreespaceStart, getSize());

        //        if (internalOffset < 0x08000000)
        //            internalOffset += 0x08000000;
        //    }

        //    loadedMap.mapSprites.pNPC = internalOffset;
        //    loadedMap.mapSprites.bNumNPC = (byte)mapNPCs.size();

        //    rom.Seek(internalOffset);
        //    for (SpriteNPC n : mapNPCs)
        //        n.save();
        //}
    }

}
