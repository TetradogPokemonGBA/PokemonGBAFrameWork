using System;
using System.Collections.Generic;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class Bank
	{
		public const int MAX = byte.MaxValue + 1;
		List<OffsetRom> Offsets { get; set; } = new List<OffsetRom>();
		public static Bank Get(RomGba rom, int index, OffsetRom offsetTablaBankPointers = default)
		{
			int offsetMapsBank;
			OffsetRom aux;
			Bank bank = new Bank();

			if (Equals(offsetTablaBankPointers, default))
				offsetTablaBankPointers = GetOffset(rom);

			offsetMapsBank = new OffsetRom(rom, offsetTablaBankPointers + index * OffsetRom.LENGTH);
			do
			{
				aux = new OffsetRom(rom, offsetMapsBank);
				offsetMapsBank += OffsetRom.LENGTH;
				if (aux.IsAPointer)
					bank.Offsets.Add(aux);

			} while (aux.IsAPointer && bank.Offsets.Count < MAX);


			return bank;
		}
		public static Bank[] Get(RomGba rom, OffsetRom offsetTablaBankPointers = default)
		{//int offsetTablaBankPointers,int numBanks,int[] mapBankSize
			Bank[] banks = new Bank[4];
			if (Equals(offsetTablaBankPointers, default))
				offsetTablaBankPointers = GetOffset(rom);
			for (int i = 0; i < banks.Length; i++)
				banks[i] = Get(rom, i, offsetTablaBankPointers);
			return banks;
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static Zona GetZona(RomGba rom)
		{
			throw new NotImplementedException();
		}
	}

}
