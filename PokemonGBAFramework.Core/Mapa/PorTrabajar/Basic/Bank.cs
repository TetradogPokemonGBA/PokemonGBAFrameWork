using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class Bank
    {
        public class Map
        {
            public uint Offset { get; set; }
            public int? Index { get; set; }
            public bool IsReserved { get; set; }
        }

        public static byte[] MuestraAlgoritmo = { 0x03, 0x4A, 0x80, 0x0B, 0x80, 0x18, 0x00, 0x68 };
        public static int IndexRelativo = 16 - MuestraAlgoritmo.Length;







        public MapHeader[] Maps { get; set; }


        public static List<List<Map>> GetBankSize(RomGba rom,OffsetRom offsetTabla=default)
        {
            //source:https://github.com/fancycoconut/PokemonMapEditor

            if (Equals(offsetTabla, default))
                offsetTabla = GetOffset(rom);

            const uint andOffset=0xFF000000;
            const uint greaterOffset= 0x8000000;
            const uint offsetsDistinct1 =0xFFFFFFFF;
            const uint offsetsDistinct2 = 0xF7F7F7F7;
            const uint offsetReserved = 0x77777777;
            const int kantoFixMapIndex=0x58;


           List<List<Map>> lstBanksSize = new List<List<Map>>();
           List<Map> lstSizeAct;
     
            uint NextMapBank;
            bool acabado;
            int offset;
            uint CurrentMapBank;
            int  mapIndex;
            uint mapOffset;
            Map mapAct;
            bool isLastMap=false;

       
            do
            {

                offset = offsetTabla + lstBanksSize.Count * OffsetRom.LENGTH;
                CurrentMapBank = (uint)new OffsetRom(rom, offset).Offset;
                offset += OffsetRom.LENGTH;
                try
                {
                    NextMapBank = (uint)new OffsetRom(rom, offset).Offset;
                }
                catch
                {
                    NextMapBank = CurrentMapBank;
                    isLastMap = true;
                }
                acabado = ((CurrentMapBank & andOffset) >= greaterOffset && CurrentMapBank != offsetsDistinct1 && CurrentMapBank != offsetsDistinct2);

                if (!acabado)
                {
                    lstSizeAct = new List<Map>();
                    do
                    {
                        offset = (int)CurrentMapBank;
                        mapOffset = (uint)new OffsetRom(rom, offset).Offset;
                        acabado = ((CurrentMapBank & andOffset) >= greaterOffset && CurrentMapBank != offsetsDistinct1 && CurrentMapBank != offsetsDistinct2);
                        if (!acabado)
                        {
                            mapAct = new Map
                            {
                                Offset = mapOffset,
                                Index = null,
                                IsReserved = mapOffset == offsetReserved

                            };
                            if (!mapAct.IsReserved)
                            {
                                mapIndex = rom.Data[(int)mapOffset + 20];
                                if (rom.Edicion.EsKanto)
                                {
                                    mapIndex -= kantoFixMapIndex;
                                }
                                mapAct.Index = mapIndex;
                            }
                            lstSizeAct.Add(mapAct);
                            CurrentMapBank += OffsetRom.LENGTH;
                        }
                    } while (CurrentMapBank < NextMapBank);



                    lstBanksSize.Add(lstSizeAct);

                }

            } while (!isLastMap);
      
          


            return lstBanksSize;

        }
        public static Bank Get(RomGba rom,int bankIndex,List<List<Map>> lstBaksSize=default,TilesetCache tilesetCache=default,OffsetRom offsetTabla=default,OffsetRom offsetTablaNombreMapa=default)
        {
            if (Equals(offsetTabla, default))
                offsetTabla = GetOffset(rom);

            if (Equals(offsetTablaNombreMapa, default))
                offsetTablaNombreMapa = NombreMapa.GetOffset(rom);

            if (Equals(tilesetCache, default))
                tilesetCache = new TilesetCache();

            if (Equals(lstBaksSize, default))
                lstBaksSize = GetBankSize(rom,offsetTabla);

            int offset = new OffsetRom(rom, offsetTabla + bankIndex * OffsetRom.LENGTH);
            Bank bank = new Bank();

            List<Map> banksSize = lstBaksSize[bankIndex];



            bank.Maps =new MapHeader[banksSize.Count];

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
            List<List<Map>> lstBaksSize = GetBankSize(rom,offsetTablaBank);
            for (int i = 0; i < banks.Length; i++)
                banks[i] = Get(rom, i,lstBaksSize,tilesetCache, offsetTablaBank,offsetTablaNombreMapa);


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
            public int Map { get; set; }
            public int MiniMap { get; set; }
            public string Name { get; set; }
            public MapTreeNode(string name, int mapNum, int miniMapNum)
            {
                Name = name;
                Map = mapNum;
                MiniMap = miniMapNum;
            }
            public override string ToString()
            {
                return $"{Name}  ({Map}.{MiniMap})";
            }
        }
    }

}
