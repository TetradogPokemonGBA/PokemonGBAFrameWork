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

		public static ConnectionData Get(RomGba rom, int offsetMapHeaderConnect)
		{
			int offsetData;
			ConnectionData connectionData = new ConnectionData();
			int offset = offsetMapHeaderConnect;
			int numConnections = new OffsetRom(rom, offset).Integer;
			offset += OffsetRom.LENGTH;
			offsetData = new OffsetRom(rom, offset);

			for (int i = 0; i < numConnections; i++)
			{
				connectionData.ConnectionsList.Add(Connection.Get(rom, offsetData));
				offsetData += Connection.LENGTH;
			}
			return connectionData;
		}

	}

}
