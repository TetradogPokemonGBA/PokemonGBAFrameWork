using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class ConnectionData
	{
		public List<Connection> ConnectionsList { get; set; }

		public ConnectionData()
		{
			ConnectionsList = new List<Connection>();
		}

		public int Size=> ConnectionsList.Count * Connection.LENGTH;

		public void Add(Connection.Type type, byte bank, byte map)
		{
			ConnectionsList.Add(new Connection(type, bank, map));
		}

		public static ConnectionData Get(RomGba rom, MapHeader mapHeader)
		{
			return Get(rom, mapHeader.OffsetConnect);
		}
		public static ConnectionData Get(RomGba rom, OffsetRom mapHeaderConnect)
		{
			int offsetData;
			ConnectionData connectionData = new ConnectionData();
			int offset = mapHeaderConnect;
			uint numConnections = new DWord(rom, offset);
			offset += DWord.LENGTH;
			offsetData = new OffsetRom(rom, offset);

			for (uint i = 0; i < numConnections; i++)
			{
				connectionData.ConnectionsList.Add(Connection.Get(rom, offsetData));
				offsetData += Connection.LENGTH;
			}
			return connectionData;
		}

	}

}
