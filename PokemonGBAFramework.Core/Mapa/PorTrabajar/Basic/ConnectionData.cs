using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class ConnectionData
	{
		private int originalSize;
		private MapHeader mapHeader;
		public int pNumConnections;
		public OffsetRom pData;
		public List<Connection> aConnections;

		public ConnectionData(RomGba rom, MapHeader mHeader)
		{
			mapHeader = mHeader;
			load(rom);
		}

		public void load(RomGba rom)
		{
			int offset = mapHeader.OffsetConnect;
			pNumConnections = new OffsetRom(rom, offset);
			offset += OffsetRom.LENGTH;
			pData = new OffsetRom(rom, offset);
			aConnections = new List<Connection>();

			offset = pData;
			for (int i = 0; i < pNumConnections; i++)
			{
				aConnections.Add(new Connection(rom,offset));
				offset += Connection.LENGTH;
			}

			originalSize = getConnectionDataSize();
		}

		//public void save()
		//{
		//	if (pData < 0x08000000)
		//		pData += 0x08000000;

		//	rom.Seek(BitConverter.shortenPointer(mapHeader.pConnect));
		//	rom.writePointer(pNumConnections);
		//	rom.writePointer(pData);

		//	rom.Seek(BitConverter.shortenPointer(pData));
		//	for (int i = 0; i < pNumConnections; i++)
		//	{
		//		aConnections[i].save();
		//	}
		//}

		public int getConnectionDataSize()
		{
			return aConnections.Count * Connection.LENGTH;
		}

		public void addConnection()
		{

		}

		public void addConnection(RomGba rom,Connection.Type c, byte bank, byte map)
		{
			pNumConnections++;
			aConnections.Add(new Connection(c, bank, map));
			//rom.floodBytes(BitConverter.shortenPointer(pData), rom.freeSpaceByte, originalSize);

			//TODO make this a setting, ie always repoint vs keep pointers
			if (originalSize < getConnectionDataSize())
			{
				pData =new OffsetRom(rom.Data.SearchEmptyBytes(getConnectionDataSize()));
			}


		}
	}

}
