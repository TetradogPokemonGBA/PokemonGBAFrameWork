using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildPokemonData : ICloneable
	{
		public const int LENGTH = 8;

		public static readonly int[] NumPokemon = new int[] { 12, 5, 5, 10 };

		public WildPokemonData(RomGba rom, WildData.Type t,int offset)
		{
			Type = t;
			if (offset <= 0x1FFFFFF && offset >= 0x100)
			{


				try
				{
					BRatio = rom.Data[offset++];
					BDNEnabled = rom.Data[offset++];
					offset += 0x2;
					PPokemonData = new OffsetRom(rom,offset);
					offset += OffsetRom.LENGTH;
					AWildPokemon = new WildPokemon[(BDNEnabled > 0 ? 4 : 1),NumPokemon[(int)Type]];
					ADNPokemon = new OffsetRom[4];

					for (int j = 0; j < 4; j++)
					{
						if (BDNEnabled == 0x1)
							ADNPokemon[j] = new OffsetRom(rom,(int)(PPokemonData) + (j * 4));
						else
							ADNPokemon[j] = default;
					}


					for (int j = 0, jf = (BDNEnabled > 0 ? 4 : 1); j < jf; j++)
					{
						if (BDNEnabled == 0)
							offset=((int)PPokemonData);
						else
							offset=((int)ADNPokemon[j] & 0x1FFFFFF);

						for (int i = 0; i < NumPokemon[(int)Type]; i++)
						{
							AWildPokemon[j,i] = new WildPokemon(rom,offset);
							offset += WildPokemon.LENGTH;
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}
		}

		public WildPokemonData(WildData.Type t, byte ratio)
		{
			Type = t;
			BRatio = ratio;
			PPokemonData =default;
			BDNEnabled = 0;
			AWildPokemon = new WildPokemon[(BDNEnabled > 0 ? 4 : 1),NumPokemon[(int)Type]];

			for (int i = 0; i < NumPokemon[(int)Type]; i++)
			{
				AWildPokemon[0,i] = new WildPokemon(1, 1, 0);
			}
		}

		public WildPokemonData(WildData.Type t, bool enableDN)
		{

			Type = t;
			BRatio = 0x21;
			BDNEnabled = (byte)(enableDN ? 0x1 : 0x0);
			PPokemonData = default;
			AWildPokemon = new WildPokemon[(BDNEnabled > 0 ? 4 : 1),NumPokemon[(int)Type]];
			for (int j = 0; j < (BDNEnabled > 0 ? 4 : 1); j++)
			{
				for (int i = 0; i < NumPokemon[(int)Type]; i++)
				{
					AWildPokemon[j,i] = new WildPokemon(1, 1, 0);
				}
			}
		}

		public WildPokemonData(WildPokemonData d)
		{
		
			try
			{
				this.ADNPokemon =(OffsetRom[]) d.ADNPokemon.Clone();
				WildPokemon[,] pokeTransfer = new WildPokemon[d.AWildPokemon.Length,NumPokemon[(int)d.Type]];

				for (int j = 0; j < d.AWildPokemon.Length; j++)
				{
					for (int i = 0; i < NumPokemon[(int)d.Type]; i++)
					{
						pokeTransfer[j,i] = new WildPokemon(d.AWildPokemon[j, i].Especie, d.AWildPokemon[j,i].MinLV, d.AWildPokemon[j,i].MaxLV);
					}
				}
				this.AWildPokemon =(WildPokemon[,]) pokeTransfer.Clone();

				this.BDNEnabled = d.BDNEnabled;
				this.BRatio = d.BRatio;
				this.PData = d.PData;
				this.PPokemonData = d.PPokemonData;
				this.Type = d.Type;
			}
			catch  {}
		
		}
		public WildData.Type Type { get; set; }
		public OffsetRom PData { get; set; }
		public byte BRatio { get; set; }
		public byte BDNEnabled { get; set; }
		public OffsetRom PPokemonData { get; set; }
		public WildPokemon[,] AWildPokemon { get; set; }
		public OffsetRom[] ADNPokemon { get; set; }

		public void convertToDN()
		{
			BDNEnabled = 1;
			WildPokemon[,] pokeTransfer = new WildPokemon[4,NumPokemon[(int)Type]];

			for (int j = 0; j < 4; j++)
			{
				for (int i = 0,f= NumPokemon[(int)Type]; i < f; i++)
				{
					pokeTransfer[j,i] = new WildPokemon(AWildPokemon[0,i].Especie,AWildPokemon[0,i].MinLV, AWildPokemon[0,i].MaxLV);
				}
			}

			AWildPokemon =(WildPokemon[,]) pokeTransfer.Clone();
		}



		public int getWildDataSize()
		{
			return NumPokemon[(int)Type] * WildPokemon.LENGTH;
		}



		//public void save()
		//{
		//	rom.writeByte(bRatio);
		//	rom.writeByte(bDNEnabled);
		//	rom.internalOffset += 0x2;

		//	if (pPokemonData == -1)
		//	{
		//		pPokemonData = rom.findFreespace((bDNEnabled == 1 ? 4 * 4 : getWildDataSize()), (int)DataStore.FreespaceStart);
		//		rom.floodBytes((int)pPokemonData, (byte)0, (bDNEnabled == 1 ? 4 * 4 : getWildDataSize())); //Prevent them from taking the same freespace
		//	}
		//	for (int i = 0; i < 4; i++)
		//		if (aDNPokemon[i] == -1 && bDNEnabled == 0x1)
		//		{
		//			aDNPokemon[i] = rom.findFreespace(getWildDataSize(), (int)DataStore.FreespaceStart);
		//			rom.floodBytes((int)aDNPokemon[i], (byte)0, getWildDataSize()); //Prevent them from taking the same freespace
		//		}

		//	rom.writePointer((int)pPokemonData);

		//	if (bDNEnabled == 1)
		//	{
		//		rom.Seek((int)pPokemonData);
		//		rom.writePointer((int)aDNPokemon[0]);
		//		rom.writePointer((int)aDNPokemon[1]);
		//		rom.writePointer((int)aDNPokemon[2]);
		//		rom.writePointer((int)aDNPokemon[3]);
		//	}

		//	for (int j = 0; j < (bDNEnabled > 0 ? 4 : 1); j++)
		//	{
		//		if (bDNEnabled == 0)
		//			rom.Seek((int)pPokemonData);
		//		else
		//			rom.Seek((int)aDNPokemon[j]);

		//		for (int i = 0; i < NumPokemon[type.ordinal()]; i++)
		//		{
		//			try
		//			{
		//				aWildPokemon[j][i].save();
		//			}
		//			catch (Exception e)
		//			{
		//				e.printStackTrace();
		//			}
		//		}
		//	}
		//}

		public Object Clone()
		{
			return new WildPokemonData(this);
		}
	}

}
