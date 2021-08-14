using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class ConnectionData
	{
		

		public ConnectionData()
		{
			Connections = new List<Connection>();
		}
		public List<Connection> Connections { get; set; }
		public int Size=> Connections.Count * Connection.LENGTH;

		public void Add(Connection.Type type, byte bank, byte map)
		{
			Connections.Add(new Connection(type, bank, map));
		}

		public static ConnectionData Get(RomGba rom, int offsetMapHeaderConnect)
		{
			int offsetData;
			ConnectionData connectionData = new ConnectionData();
			int offset = offsetMapHeaderConnect;
			uint numConnections = new DWord(rom, offset);
			offset += DWord.LENGTH;
			offsetData = new OffsetRom(rom, offset);

			for (uint i = 0; i < numConnections; i++)
			{
				connectionData.Connections.Add(Connection.Get(rom, offsetData));
				offsetData += Connection.LENGTH;
			}
			return connectionData;
		}

	}

}
