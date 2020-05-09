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
		public OffsetRom Offset { get; set; }
		public byte Bank { get; set; }
		public byte Map { get; set; }
		public Word Filler { get; set; }
		public static Connection Get(RomGba rom,int offset)
		{
			Connection connection = new Connection();

			connection.ConnectionType =(Type)(uint)new DWord(rom,offset);//no lo lee bien...aunque algunos parece que si...
			offset += DWord.LENGTH;
			connection.Offset = new OffsetRom(rom,offset);
			offset += OffsetRom.LENGTH;
			connection.Bank = rom.Data[offset++];
			connection.Map = rom.Data[offset++];
			connection.Filler = new Word(rom, offset);

			if (!connection.Offset.IsEmpty)
				connection.Offset.Fix();
			else connection.Offset = default;

			return connection;


		}

		public byte[] GetBytes()
		{
			DWord dType = (uint)ConnectionType;
			return dType.Data.AddArray(!Equals(Offset, default) ? Offset.BytesPointer:new byte[OffsetRom.LENGTH], new byte[] { Bank, Map },!Equals(Filler,default) ?Filler.Data:new byte[Word.LENGTH]);
		}
	}

}
