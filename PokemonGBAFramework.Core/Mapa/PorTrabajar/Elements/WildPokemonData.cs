using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Mapa.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Elements
{
    public class WildPokemonData : ICloneable, IClonable<WildPokemonData>
    {
        public const int LENGTH = 8;
        //no se donde va pero en el archivo pone WildPokemon
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x04, 0x0C, 0x20, 0x1C, 0x12, 0xE0 };
        public static readonly int IndexRelativoEsmeralda = 0;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0xD0, 0x20, 0x1C, 0x13, 0xE0, 0x00, 0x00 };
        public static readonly int IndexRelativoKanto = 0;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x05, 0xD1, 0x18, 0x1C, 0x0E, 0xE0 };
        public static readonly int IndexRelativoRubiYZafiro = 0;

        public static readonly int[] NumPokemon = new int[] { 12, 5, 5, 10 };

        private WildPokemonData() { }
        public WildPokemonData(WildData.Type tipoArea, byte ratio=0x21, Word pokemon = default,bool dnEnlabled=false)
        {
            if (Equals(pokemon, default))
                pokemon = new Word(1);

            Type = tipoArea;
            Ratio = ratio;
            OffsetPokemonData = default;
            DNEnabled = dnEnlabled;
            AreaWildPokemon = new WildPokemon[(DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME), NumPokemon[(int)Type]];

            for (int j = 0,jF= DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME, iF = NumPokemon[(int)Type]; j < jF; j++)
            {
                for (int i = 0; i < iF; i++)
                {
                    AreaWildPokemon[j, i] = new WildPokemon(pokemon);
                }
            }
        }

        public WildData.Type Type { get; set; }
        public OffsetRom OffsetData { get; set; }
        public byte Ratio { get; set; }
        public bool DNEnabled { get; set; }
        public OffsetRom OffsetPokemonData { get; set; }
        public WildPokemon[,] AreaWildPokemon { get; set; }
        public OffsetRom[] ADNPokemon { get; set; }

        public void EnableDN()
        {
            WildPokemon[,] pokeTransfer;
            if (!DNEnabled)
            {
                pokeTransfer = new WildPokemon[Tileset.MAXTIME, NumPokemon[(int)Type]];
                DNEnabled = true;
                for (int j = 0; j < Tileset.MAXTIME; j++)
                {
                    for (int i = 0, f = NumPokemon[(int)Type]; i < f; i++)
                    {
                        pokeTransfer[j, i] = new WildPokemon(AreaWildPokemon[0, i].Especie, AreaWildPokemon[0, i].NivelMinimo, AreaWildPokemon[0, i].NivelMaximo);
                    }
                }

                AreaWildPokemon = pokeTransfer;
            }
        }



        public int Size => NumPokemon[(int)Type] * WildPokemon.LENGTH;
        public object Clone() => Clon();
        public WildPokemonData Clon() {
            WildPokemonData wildPokemonData = new WildPokemonData();
            wildPokemonData.ADNPokemon = (OffsetRom[])ADNPokemon.Clone();
            wildPokemonData.AreaWildPokemon = new WildPokemon[AreaWildPokemon.Length, NumPokemon[(int)Type]];

            for (int j = 0; j < AreaWildPokemon.Length; j++)
            {
                for (int i = 0; i < NumPokemon[(int)Type]; i++)
                {
                    wildPokemonData.AreaWildPokemon[j, i] = new WildPokemon(AreaWildPokemon[j, i].Especie, AreaWildPokemon[j, i].NivelMinimo, AreaWildPokemon[j, i].NivelMaximo);
                }
            }


            wildPokemonData.DNEnabled = DNEnabled;
            wildPokemonData.Ratio = Ratio;
            wildPokemonData.OffsetData = OffsetData;
            wildPokemonData.OffsetPokemonData = OffsetPokemonData;
            wildPokemonData.Type = Type;

            return wildPokemonData;
        }


        public static WildPokemonData Get(RomGba rom, WildData.Type tipoArea, OffsetRom offsetWildPokemonAreaData)
        {
            const byte DNENABLED = 0x1;

            int offset;
            WildPokemonData wildPokemonData=default;

            offset = offsetWildPokemonAreaData;

            if (offset <= 0x1FFFFFF && offset >= 0x100)
            {
                try
                {
                    wildPokemonData = new WildPokemonData();
                    wildPokemonData.Type = tipoArea;
                    wildPokemonData.Ratio = rom.Data[offset++];
                    wildPokemonData.DNEnabled = rom.Data[offset++] == DNENABLED;
                    offset += 0x2;
                    wildPokemonData.OffsetPokemonData = new OffsetRom(rom, offset);
                    offset += OffsetRom.LENGTH;
                    wildPokemonData.AreaWildPokemon = new WildPokemon[(wildPokemonData.DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME), NumPokemon[(int)wildPokemonData.Type]];
                    wildPokemonData.ADNPokemon = new OffsetRom[Tileset.MAXTIME];

                    if (wildPokemonData.DNEnabled)
                    {
                        for (int j = 0; j < Tileset.MAXTIME; j++)
                        {
                            wildPokemonData.ADNPokemon[j] = new OffsetRom(rom, wildPokemonData.OffsetPokemonData + (j * OffsetRom.LENGTH));
                        }
                    }


                    for (int j = 0, jf = wildPokemonData.DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME,iF= NumPokemon[(int)wildPokemonData.Type]; j < jf; j++)
                    {
                        if (!wildPokemonData.DNEnabled)
                            offset = wildPokemonData.OffsetPokemonData;
                        else
                            offset = wildPokemonData.ADNPokemon[j] & 0x1FFFFFF;

                        for (int i = 0; i < iF; i++)
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
      

 
        
        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int index;
            if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                index = IndexRelativoEsmeralda;

            }
            else if (rom.Edicion.EsKanto)
            {
                algoritmo = MuestraAlgoritmoKanto;
                index = IndexRelativoKanto;
            }
            else
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                index = IndexRelativoRubiYZafiro;
            }

            return Zona.Search(rom, algoritmo, index);
        }
    }

}
