using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapHeader
    {
        public enum TipoMapa:byte
        {
            NULL1,
            Pueblo,
            Ciudad,
            Ruta,
            Cueva,
            Buceando,
            NULL2,
            NULL3,
            Edificio,
            BaseSecreta
        }
        public enum Destello : byte
        {
            SinNecesidad,
            Disponible,
            NoDisponible
        }
        public enum Tiempo : byte
        {
            Edificio,
            SoleadoConNubes,
            Normal,
            Lloviendo,
            CuatroCoposDeNieve,
            LluviaConTruenos,
            Niebla,
            Nevando,
            TormentaDeArena,
            NieblaDesdeEsquinaSuperiorDerecha,
            NieblaDensaYBrillante,
            Nublado,
            BajoTierraConUsandoDestello,
            DiluvioConTruenos,
            NieblaSubmarina,
            NULL

        }
        public enum Lucha : byte
        {
            Aleatorio,
            Gimnasio,
            TeamRocket,
            NULL,
            Top4Primero,
            Top4Segundo,
            Top4Tercero,
            Top4Ultimo,
            BigRedPokeball

        }
        public enum DisplayNameStyle : byte
        {
            NoMostrar,
            Mostrar,
            NULL1,
            NULL2,
            NULL3,
            NULL4,
            Pueblo,
            NULL5,
            NULL6,
            NULL7,
            NULL8,
            NULL9,
            NULL10,
            Ciudad,
            NULL11,
            NULL12

        }

        public static byte[] MuestraAlgoritmo = {0x03, 0x4A, 0x80, 0x0B, 0x80, 0x18, 0x00, 0x68 };
        public static int IndexRelativo = 16-MuestraAlgoritmo.Length;
       
        public OffsetRom OffsetMap { get; set; }
        public OffsetRom OffsetSprites { get; set; }
        public OffsetRom OffsetScript { get; set; }
        public OffsetRom OffsetConnect { get; set; }
        public Word Song { get; set; }
        public Word Map { get; set; }
        public byte LabelID { get; set; }
        public Destello Flash { get; set; }
        public Tiempo Weather { get; set; }
        public TipoMapa Tipo { get; set; }
        public Lucha Fight { get; set; }//por logica falta probarlo
        public byte SinUso2 { get; set; }
        public DisplayNameStyle DisplayName { get; set; }
        public byte SinUso3 { get; set; }

        public static MapHeader Get(RomGba rom, int index, OffsetRom offsetTablaMapHeader=default)
        {
            if (Equals(offsetTablaMapHeader, default))
                offsetTablaMapHeader = GetOffset(rom);
            int offset = new OffsetRom(rom, offsetTablaMapHeader + index * OffsetRom.LENGTH);
            MapHeader mapHeader = new MapHeader();
            
            offset &=0x1FFFFFF;

            mapHeader.OffsetMap = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetSprites = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetScript = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.OffsetConnect = new OffsetRom(rom, offset);
            offset += OffsetRom.LENGTH;
            mapHeader.Song = new Word(rom, offset);
            offset += Word.LENGTH;
            mapHeader.Map = new Word(rom, offset);
            offset += Word.LENGTH;

            mapHeader.LabelID = rom.Data[offset++];
            mapHeader.Flash = (Destello)rom.Data[offset++];
            mapHeader.Weather = (Tiempo)rom.Data[offset++];
            mapHeader.Tipo = (TipoMapa)rom.Data[offset++];
            mapHeader.Fight = (Lucha)rom.Data[offset++];
            mapHeader.SinUso2 = rom.Data[offset++];
            mapHeader.DisplayName = (DisplayNameStyle)rom.Data[offset++];
            mapHeader.SinUso3 = rom.Data[offset++];

            if (mapHeader.OffsetMap.IsEmpty)
                mapHeader.OffsetMap = default;
            else mapHeader.OffsetMap.Fix();

            if (mapHeader.OffsetSprites.IsEmpty)
                mapHeader.OffsetSprites = default;
            else mapHeader.OffsetSprites.Fix();

            if (mapHeader.OffsetScript.IsEmpty)
                mapHeader.OffsetScript = default;
            else mapHeader.OffsetScript.Fix();

            if (mapHeader.OffsetScript.IsEmpty)
                mapHeader.OffsetScript = default;
            else mapHeader.OffsetScript.Fix();

            if (mapHeader.OffsetConnect.IsEmpty)
                mapHeader.OffsetConnect = default;
            else mapHeader.OffsetConnect.Fix();

            return mapHeader;
        }
        public static MapHeader[] Get(RomGba rom,OffsetRom offsetTablaMapHeader = default)
        {
            if (Equals(offsetTablaMapHeader, default))
                offsetTablaMapHeader = GetOffset(rom);
            MapHeader[] mapHeaders = new MapHeader[GetTotal(rom, offsetTablaMapHeader)];
            for (int i = 0; i < mapHeaders.Length; i++)
                mapHeaders[i] = Get(rom, i, offsetTablaMapHeader);
            return mapHeaders;
        }
        public static int GetTotal(RomGba rom, OffsetRom offsetTablaMapHeader = default)
        {
            int offset;
            int total = 0;
            if (Equals(offsetTablaMapHeader, default))
                offsetTablaMapHeader = GetOffset(rom);
            offset = offsetTablaMapHeader;

            while (OffsetRom.Check(rom, offset))
            {
                total++;
                offset += OffsetRom.LENGTH;
            }
            return total;
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }

        public static int GetZona(RomGba rom)
        {
            return Zona.Search(rom, MuestraAlgoritmo, IndexRelativo);
        }
    }
}
