using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class WildData : ICloneable, IClonable<WildData>
    {
        public enum Type
        {
            Grass, Water, Tree, Fishing
        }
        public static readonly int NumWildAreas = Enum.GetNames(typeof(Type)).Length;
        public WildPokemonData[] WildArea { get; set; } = new WildPokemonData[NumWildAreas];
        public WildDataHeader WildDataHeader { get; set; }

        public WildData() { }


        public WildData(int bank, int map, OffsetRom offsetGrass, OffsetRom offsetWater, OffsetRom offsetTree, OffsetRom offsetFishing)
        {
            WildDataHeader = new WildDataHeader(bank, map, offsetGrass, offsetWater, offsetTree, offsetFishing);
        }


        public void Remove(RomGba rom, Type tipoWildData)
        {
            int size;
            int pkmnData;
            int position;
            OffsetRom offset;
            WildPokemonData wildPokemonData;

            if (!ReferenceEquals(WildArea[(int)tipoWildData], default))
            {
                switch (tipoWildData)
                {
                    case Type.Water:
                        position = 1;
                        offset = WildDataHeader.OffsetWater;
                        WildDataHeader.OffsetWater = default;
                        break;
                    case Type.Tree:
                        position = 2;
                        offset = WildDataHeader.OffsetTrees;
                        WildDataHeader.OffsetTrees = default;

                        break;
                    case Type.Fishing:
                        position = 3;
                        offset = WildDataHeader.OffsetFishing;
                        WildDataHeader.OffsetFishing = default;
                        break;
                    default:
                        position = 0;
                        offset = WildDataHeader.OffsetGrass;
                        WildDataHeader.OffsetGrass = default;
                        break;
                }

                wildPokemonData = WildArea[position];
                size = wildPokemonData.Size;
                pkmnData = (int)wildPokemonData.OffsetPokemonData;
                rom.Data.Remove((int)offset, WildPokemonData.LENGTH);
                WildArea[position] = default;

                rom.Data.Remove(pkmnData, size);
            }
        }

        public void Add(Type tipoWildArea, byte ratio=0x15)
        {
            WildPokemonData wildAreaData = new WildPokemonData(tipoWildArea, ratio);
            WildArea[(int)tipoWildArea] = wildAreaData;

        }



        public object Clone() => Clon();
        public WildData Clon()
        {
            WildData other = new WildData();
            other.WildDataHeader = WildDataHeader.Clon();
            for (int i = 0; i < WildArea.Length; i++)
                other.WildArea[i] = WildArea[i].Clon();
            return other;
        }

        public static WildData Get(RomGba rom, WildDataHeader wildDataHeader)
        {
            WildData wildData = new WildData();
            wildData.WildDataHeader = wildDataHeader.Clon();


            if (!Equals(wildDataHeader.OffsetGrass, default))
                wildData.WildArea[(int)Type.Grass] = WildPokemonData.Get(rom, WildData.Type.Grass, wildDataHeader.OffsetGrass);

            if (!Equals(wildDataHeader.OffsetWater, default))
                wildData.WildArea[(int)Type.Water] = WildPokemonData.Get(rom, WildData.Type.Water, wildDataHeader.OffsetWater);

            if (!Equals(wildDataHeader.OffsetTrees, default))
                wildData.WildArea[(int)Type.Tree] = WildPokemonData.Get(rom, WildData.Type.Tree, wildDataHeader.OffsetTrees);

            if (!Equals(wildDataHeader.OffsetFishing, default))
                wildData.WildArea[(int)Type.Fishing] = WildPokemonData.Get(rom, WildData.Type.Fishing, wildDataHeader.OffsetFishing);
          
            return wildData;
        }

    }

}
