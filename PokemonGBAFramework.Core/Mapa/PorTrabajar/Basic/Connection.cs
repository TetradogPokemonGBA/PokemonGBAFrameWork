using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
namespace PokemonGBAFramework.Core.Mapa
{
	public class Connection
	{
		public const int LENGTH=12;

		public enum Type
		{
			NULL,
			DOWN,
			UP,
			LEFT,
			RIGHT,
			DIVE,
			EMERGE
		}



		public Connection() { }
		public Connection(Type tipo, byte bank, byte map)
		{
			ConnectionType = tipo;
			Offset = default;
			Bank = bank;
			Map = map;
			Filler = 0;
		}
		public Type ConnectionType { get; set; }
		public OffsetRom Offset { get; set; }//de momento
		public byte Bank { get; set; }
		public byte Map { get; set; }
		public Word Filler { get; set; }

		public override string ToString()
		{
			return $"{ConnectionType} {Bank}:{Map}";
		}

		public byte[] GetBytes()
		{
			DWord dType = (uint)ConnectionType;
			return dType.Data.AddArray(!Equals(Offset, default) ? new OffsetRom(Offset).BytesPointer : new byte[OffsetRom.LENGTH], new byte[] { Bank, Map }, !Equals(Filler, default) ? Filler.Data : new byte[Word.LENGTH]);
		}


		public static Connection Get(RomGba rom,int offset)
		{
			Connection connection = new Connection();

			connection.ConnectionType =(Type)new OffsetRom(rom,offset).Integer;
			offset += OffsetRom.LENGTH;
			connection.Offset = new OffsetRom(rom,offset);
			offset += OffsetRom.LENGTH;
			connection.Bank = rom.Data[offset++];
			connection.Map = rom.Data[offset++];
			connection.Filler = new Word(rom,offset);

			return connection;


		}

	}

}
