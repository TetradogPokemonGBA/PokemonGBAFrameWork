using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
	public class WildPokemonData : ICloneable,IClonable<WildPokemonData>
	{
		public const int LENGTH = 8;

		public static readonly int[] NumPokemon = new int[] { 12, 5, 5, 10 };
		public WildPokemonData() { }


		public WildPokemonData(WildData.Type tipoArea, byte ratio,Word pokemon=default)
		{
			if(Equals(pokemon,default))
			  pokemon = new Word(1);

			Type = tipoArea;
			Ratio = ratio;
			OffsetPokemonData =default;
			DNEnabled = 0;
			AreaWildPokemon = new WildPokemon[(DNEnabled > 0 ? 4 : 1),NumPokemon[(int)Type]];

			for (int i = 0; i < NumPokemon[(int)Type]; i++)
			{
				AreaWildPokemon[0,i] = new WildPokemon(pokemon);
			}
		}

		public WildPokemonData(WildData.Type tipoArea, bool enableDN)
		{

			Type = tipoArea;
			Ratio = 0x21;
			DNEnabled = (byte)(enableDN ? 0x1 : 0x0);
			OffsetPokemonData = default;
			AreaWildPokemon = new WildPokemon[(DNEnabled > 0 ? 4 : 1),NumPokemon[(int)Type]];
			for (int j = 0; j < (DNEnabled > 0 ? 4 : 1); j++)
			{
				for (int i = 0; i < NumPokemon[(int)Type]; i++)
				{
					AreaWildPokemon[j,i] = new WildPokemon();
				}
			}
		}

		public WildPokemonData(WildPokemonData wildAreaData)
		{
			WildPokemon[,] pokeTransfer;


				if (ADNPokemon != default)
				{
					this.ADNPokemon = (OffsetRom[])wildAreaData.ADNPokemon.Clone();
					pokeTransfer = new WildPokemon[wildAreaData.AreaWildPokemon.Length, NumPokemon[(int)wildAreaData.Type]];

					for (int j = 0; j < wildAreaData.AreaWildPokemon.Length; j++)
					{
						for (int i = 0; i < NumPokemon[(int)wildAreaData.Type]; i++)
						{
							pokeTransfer[j, i] = new WildPokemon(wildAreaData.AreaWildPokemon[j, i].Especie, wildAreaData.AreaWildPokemon[j, i].NivelMinimo, wildAreaData.AreaWildPokemon[j, i].NivelMaximo);
						}
					}
					this.AreaWildPokemon = (WildPokemon[,])pokeTransfer.Clone();

					this.DNEnabled = wildAreaData.DNEnabled;
					this.Ratio = wildAreaData.Ratio;
					this.OffsetData = wildAreaData.OffsetData;
					this.OffsetPokemonData = wildAreaData.OffsetPokemonData;
					this.Type = wildAreaData.Type;
				}
			
		
		}
		public WildData.Type Type { get; set; }
		public OffsetRom OffsetData { get; set; }
		public byte Ratio { get; set; }
		public byte DNEnabled { get; set; }
		public OffsetRom OffsetPokemonData { get; set; }
		public WildPokemon[,] AreaWildPokemon { get; set; }
		public OffsetRom[] ADNPokemon { get; set; }

		public void convertToDN()
		{
			DNEnabled = 1;
			WildPokemon[,] pokeTransfer = new WildPokemon[4,NumPokemon[(int)Type]];

			for (int j = 0; j < 4; j++)
			{
				for (int i = 0,f= NumPokemon[(int)Type]; i < f; i++)
				{
					pokeTransfer[j,i] = new WildPokemon(AreaWildPokemon[0,i].Especie,AreaWildPokemon[0,i].NivelMinimo, AreaWildPokemon[0,i].NivelMaximo);
				}
			}

			AreaWildPokemon =(WildPokemon[,]) pokeTransfer.Clone();
		}



		public int Size=> NumPokemon[(int)Type] * WildPokemon.LENGTH;



		public Object Clone() => Clon();
		public WildPokemonData Clon()=> new WildPokemonData(this);

		public static WildPokemonData Get(RomGba rom, WildData.Type tipoArea, OffsetRom offsetWildPokemonData)
		{
			WildPokemonData wildPokemonData = new WildPokemonData();
			int offset = offsetWildPokemonData;
			wildPokemonData.Type = tipoArea;
			if (offset <= 0x1FFFFFF && offset >= 0x100)
			{


				try
				{
					wildPokemonData.Ratio = rom.Data[offset++];
					wildPokemonData.DNEnabled = rom.Data[offset++];
					offset += 0x2;
					wildPokemonData.OffsetPokemonData = new OffsetRom(rom, offset);
					offset += OffsetRom.LENGTH;
					wildPokemonData.AreaWildPokemon = new WildPokemon[(wildPokemonData.DNEnabled > 0 ? 4 : 1), NumPokemon[(int)wildPokemonData.Type]];
					wildPokemonData.ADNPokemon = new OffsetRom[4];

					for (int j = 0; j < 4; j++)
					{
						if (wildPokemonData.DNEnabled == 0x1)
							wildPokemonData.ADNPokemon[j] = new OffsetRom(rom, (int)(wildPokemonData.OffsetPokemonData) + (j * OffsetRom.LENGTH));
						else
							wildPokemonData.ADNPokemon[j] = default;
					}


					for (int j = 0, jf = (wildPokemonData.DNEnabled > 0 ? 4 : 1); j < jf; j++)
					{
						if (wildPokemonData.DNEnabled == 0)
							offset = (wildPokemonData.OffsetPokemonData);
						else
							offset = ((int)wildPokemonData.ADNPokemon[j] & 0x1FFFFFF);

						for (int i = 0; i < NumPokemon[(int)wildPokemonData.Type]; i++)
						{
							wildPokemonData.AreaWildPokemon[j, i] = WildPokemon.Get(rom, offset);
							offset += WildPokemon.LENGTH;
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					wildPokemonData = default;
				}

			}
			return wildPokemonData;
		}
	}

}
