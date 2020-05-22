using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class Bank
    {

        public static byte[] MuestraAlgoritmo = { 0x03, 0x4A, 0x80, 0x0B, 0x80, 0x18, 0x00, 0x68 };
        public static int IndexRelativo = 16 - MuestraAlgoritmo.Length;


        //mas adelante comprobar que cada offset sea de un mapa así puedo prescindir de esto
        static int[] banksSizeRubi = { 53, 4, 4, 5, 6, 6, 7, 6, 6, 12, 7, 16, 9, 23, 12, 12, 13, 1, 1, 1, 2, 0, 0, 0, 85, 43, 11, 1, 0, 12, 0, 0, 2, 0 };
        static int[] banksSizeKanto = { 5, 123, 60, 66, 4, 6, 8, 10, 6, 8, 20, 10, 8, 2, 10, 4, 2, 2, 2, 1, 1, 2, 2, 3, 2, 3, 2, 1, 1, 1, 1, 7, 5, 5, 8, 8, 5, 5, 1, 1, 1, 2, 1 };
        static int[] banksSizeEsmeralda = { 57, 5, 5, 6, 7, 8, 9, 7, 7, 14, 8, 17, 10, 23, 13, 15, 15, 2, 2, 2, 3, 1, 1, 1, 108, 61, 89, 2, 1, 13, 1, 1, 2, 1 };




        public MapHeader[] Maps { get; set; }
        public static Bank Get(RomGba rom,int bankIndex,TilesetCache tilesetCache=default,OffsetRom offsetTabla=default,OffsetRom offsetTablaNombreMapa=default)
        {
            if (Equals(offsetTabla, default))
                offsetTabla = GetOffset(rom);

            if (Equals(offsetTablaNombreMapa, default))
                offsetTablaNombreMapa = NombreMapa.GetOffset(rom);

            if (Equals(tilesetCache, default))
                tilesetCache = new TilesetCache();

            int offset = new OffsetRom(rom, offsetTabla + bankIndex * OffsetRom.LENGTH);
            Bank bank = new Bank();

            int[] banksSize;

            if (rom.Edicion.EsEsmeralda)
            {
                banksSize = banksSizeEsmeralda;
            }
            else if (rom.Edicion.EsKanto)
            {
                banksSize = banksSizeKanto;
            }
            else
            {
                banksSize = banksSizeRubi;
            }

            bank.Maps =new MapHeader[banksSize[bankIndex]];

            for (int j = 0; j < bank.Maps.Length; j++)
            {
                bank.Maps[j] = MapHeader.Get(rom, tilesetCache, new OffsetRom(rom, offset), offsetTablaNombreMapa);

                offset += OffsetRom.LENGTH;
            }


            return bank;
        }
        public static Bank[] Get(RomGba rom,TilesetCache tilesetCache=default,OffsetRom offsetTablaBank=default, OffsetRom offsetTablaNombreMapa = default)
        {
            if(Equals(offsetTablaBank,default))
               offsetTablaBank = GetOffset(rom);

            if (Equals(offsetTablaNombreMapa, default))
                offsetTablaNombreMapa = NombreMapa.GetOffset(rom);
            if (Equals(tilesetCache, default))
                tilesetCache = new TilesetCache();


            Bank[] banks = new Bank[GetTotal(rom,offsetTablaBank)];

            for (int i = 0; i < banks.Length; i++)
                banks[i] = Get(rom, i,tilesetCache, offsetTablaBank,offsetTablaNombreMapa);


            return banks;

        }

       
        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmo, IndexRelativo);
        }


        public static int GetTotal(RomGba rom, OffsetRom offsetTablaMapHeader = default)
        {
            int offset;
            int total = 0;
            if (Equals(offsetTablaMapHeader, default))
                offsetTablaMapHeader = GetOffset(rom);
            offset = offsetTablaMapHeader;

            while (OffsetRom.Check(rom,offset))
            {
                total++;
                offset += OffsetRom.LENGTH;
            }
            return total;
        }

        public class MapTreeNode
        {
            public int Map;
            public int MiniMap;
            public string name;
            public MapTreeNode(string name, int mapNum, int miniMapNum)
            {
                this.name = name;
                Map = mapNum;
                MiniMap = miniMapNum;
            }
            public override string ToString()
            {
                return name + " (" + Map + "." + MiniMap + ")";
            }
        }
    }

}
