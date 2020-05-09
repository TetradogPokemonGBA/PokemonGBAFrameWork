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
        public WildPokemonData() { }


        public WildPokemonData(WildData.Type tipoArea, byte ratio, Word pokemon = default,bool dnEnlabled=false)
        {
            if (Equals(pokemon, default))
                pokemon = new Word(1);

            Type = tipoArea;
            Ratio = ratio;
            OffsetPokemonData = default;
            DNEnabled = dnEnlabled;
            AreaWildPokemon = new WildPokemon[(DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME), NumPokemon[(int)Type]];

            for (int j = 0; j < (DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME); j++)
            {
                for (int i = 0; i < NumPokemon[(int)Type]; i++)
                {
                    AreaWildPokemon[j, i] = new WildPokemon(pokemon);
                }
            }
        }

        public WildPokemonData(WildData.Type tipoArea, bool enableDN)
        {

            Type = tipoArea;
            Ratio = 0x21;
            DNEnabled = enableDN;
            OffsetPokemonData = default;
            AreaWildPokemon = new WildPokemon[(DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME), NumPokemon[(int)Type]];
            for (int j = 0; j < (DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME); j++)
            {
                for (int i = 0; i < NumPokemon[(int)Type]; i++)
                {
                    AreaWildPokemon[j, i] = new WildPokemon();
                }
            }
        }

        public WildPokemonData(WildPokemonData wildAreaData)
        {
            if (ADNPokemon != default)
            {
                this.ADNPokemon = (OffsetRom[])wildAreaData.ADNPokemon.Clone();
                this.AreaWildPokemon = new WildPokemon[wildAreaData.AreaWildPokemon.Length, NumPokemon[(int)wildAreaData.Type]];

                for (int j = 0; j < wildAreaData.AreaWildPokemon.Length; j++)
                {
                    for (int i = 0; i < NumPokemon[(int)wildAreaData.Type]; i++)
                    {
                        this.AreaWildPokemon[j, i] = new WildPokemon(wildAreaData.AreaWildPokemon[j, i].Especie, wildAreaData.AreaWildPokemon[j, i].NivelMinimo, wildAreaData.AreaWildPokemon[j, i].NivelMaximo);
                    }
                }


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
        public bool DNEnabled { get; set; }
        public OffsetRom OffsetPokemonData { get; set; }
        public WildPokemon[,] AreaWildPokemon { get; set; }
        public OffsetRom[] ADNPokemon { get; set; }

        public void convertToDN()
        {
            WildPokemon[,] pokeTransfer = new WildPokemon[Tileset.MAXTIME, NumPokemon[(int)Type]];
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



        public int Size => NumPokemon[(int)Type] * WildPokemon.LENGTH;
        public object Clone() => Clon();
        public WildPokemonData Clon() => new WildPokemonData(this);


        public static WildPokemonData Get(RomGba rom, WildData.Type tipoArea, OffsetRom offsetWildPokemonAreaData)
        {
            const byte DNENABLED = 0x1;

            int offset;
            WildPokemonData wildPokemonData = new WildPokemonData();
            
            wildPokemonData.Type = tipoArea;
            offset = offsetWildPokemonAreaData;

            if (offset <= 0x1FFFFFF && offset >= 0x100)
            {
                try
                {
                    wildPokemonData.Ratio = rom.Data[offset++];
                    wildPokemonData.DNEnabled = rom.Data[offset++] == DNENABLED;
                    offset += 0x2;
                    wildPokemonData.OffsetPokemonData = new OffsetRom(rom, offset);
                    offset += OffsetRom.LENGTH;
                    wildPokemonData.AreaWildPokemon = new WildPokemon[(wildPokemonData.DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME), NumPokemon[(int)wildPokemonData.Type]];
                    wildPokemonData.ADNPokemon = new OffsetRom[Tileset.MAXTIME];

                    for (int j = 0; j < Tileset.MAXTIME; j++)
                    {
                        if (wildPokemonData.DNEnabled)
                            wildPokemonData.ADNPokemon[j] = new OffsetRom(rom, wildPokemonData.OffsetPokemonData + (j * OffsetRom.LENGTH));
                        else
                            wildPokemonData.ADNPokemon[j] = default;
                    }


                    for (int j = 0, jf = wildPokemonData.DNEnabled ? Tileset.MAXTIME : Tileset.MINTIME; j < jf; j++)
                    {
                        if (!wildPokemonData.DNEnabled)
                            offset = wildPokemonData.OffsetPokemonData;
                        else
                            offset = wildPokemonData.ADNPokemon[j] & 0x1FFFFFF;

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
