using PokemonGBAFramework.Core.Mapa.Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
    public class MapHeader
    {
        public enum TipoMapa : byte
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


        public OffsetRom Offset { get; set; }

        public MapData MapData { get; set; }
        public ConnectionData MapConnections { get; set; }
        public HeaderSprites MapSprites { get; set; }
        public MapScript MapScript { get; set; }

        public Word Song { get; set; }
        public Word Map { get; set; }
        public NombreMapa Nombre { get; set; }
        public Destello Flash { get; set; }
        public Tiempo Weather { get; set; }
        public TipoMapa Tipo { get; set; }
        public Lucha Fight { get; set; }

        /// <summary>
        /// Always 0x1
        /// </summary>
        public byte UnknownFiller { get; set; }
        public DisplayNameStyle DisplayName { get; set; }
        public sbyte FloorLevel { get; set; }
        public Word Filler { get; set; }

        public override string ToString()
        {
            return Nombre.ToString();
        }
        public static MapHeader Get(RomGba rom,TilesetCache tilesetCache, OffsetRom offsetMapHeader,OffsetRom offsetNombreMapa=default)
        {


            OffsetRom offsetMap;
            OffsetRom offsetSprites;
            OffsetRom offsetScript;
            OffsetRom offsetConnect;

            byte labelID;
            
            MapHeader mapHeader = new MapHeader();
            int offset = offsetMapHeader;
            mapHeader.Offset = offsetMapHeader;

            offsetMap = new OffsetRom(rom, offset);//layoutData
            offset += OffsetRom.LENGTH;
            offsetSprites = new OffsetRom(rom, offset);//eventData
            offset += OffsetRom.LENGTH;
            offsetScript = new OffsetRom(rom, offset);//MapScripts
            offset += OffsetRom.LENGTH;
            offsetConnect = new OffsetRom(rom, offset);//ConnectionData
            offset += OffsetRom.LENGTH;

            mapHeader.Song = new Word(rom,offset);//MapSong
            offset += Word.LENGTH;
            mapHeader.Map = new Word(rom,offset);//MapIndex
            offset += Word.LENGTH;

            labelID = rom.Data[offset++];//MapNameIndex

            mapHeader.Flash = (Destello)rom.Data[offset++];//CaveBehavior
            mapHeader.Weather = (Tiempo)rom.Data[offset++];//Weather
            mapHeader.Tipo = (TipoMapa)rom.Data[offset++];//MapType
            if (rom.Edicion.EsKanto)
            {
                mapHeader.UnknownFiller = rom.Data[offset++];//allways 0x1
                mapHeader.DisplayName = (DisplayNameStyle)rom.Data[offset++];
                mapHeader.FloorLevel =(sbyte) rom.Data[offset++];
            }
            else
            {
                mapHeader.Filler =new Word(rom,offset);
                offset += Word.LENGTH;
                mapHeader.DisplayName = (DisplayNameStyle)rom.Data[offset++];
         
            }
            mapHeader.Fight = (Lucha)rom.Data[offset++];
           

            mapHeader.Nombre = NombreMapa.Get(rom,labelID, offsetNombreMapa );
            if (!offsetMap.IsEmpty)
            {
                mapHeader.MapData = MapData.Get(rom, offsetMap,tilesetCache);
            }
            if (!offsetSprites.IsEmpty)
            {
                mapHeader.MapSprites = HeaderSprites.Get(rom, offsetSprites);
            }
            if (!offsetScript.IsEmpty)
            {
                mapHeader.MapScript = MapScript.Get(rom, offsetScript);
            }

            if (!offsetConnect.IsEmpty)
            {
                mapHeader.MapConnections = ConnectionData.Get(rom, offsetConnect);
            }


            return mapHeader;
        }


        public static bool Check(RomGba rom,int offsetMapHeader)
        {
            bool isOK = MapData.Check(rom, offsetMapHeader);
            return isOK;
        }


    }
}
