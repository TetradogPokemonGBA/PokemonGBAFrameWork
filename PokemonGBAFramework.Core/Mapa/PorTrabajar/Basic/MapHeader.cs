using Gabriel.Cat.S.Drawing;
using PokemonGBAFramework.Core.Mapa.Elements;
using PokemonGBAFramework.Core.Mapa.PorTrabajar.Basic;
using PokemonMapEditor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static PokemonGBAFramework.Core.Mapa.Basic.Bank;
using static PokemonGBAFramework.Core.Mapa.Basic.Tileset;
using static PokemonGBAFramework.Core.PokemonErrante;

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
        public static MapHeader Get(RomGba rom,TilesetCache tilesetCache, OffsetRom offsetMapHeader,OffsetRom offsetNombreMapa=default,OffsetRom offsetTilesets=default)
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
                mapHeader.MapData = MapData.Get(rom, offsetMap,tilesetCache,offsetTilesets);
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
            mapHeader._tiles = mapHeader.Render(rom);
            mapHeader._bmps = mapHeader.GetImgs(rom);

            return mapHeader;
        }
        IEnumerable<CollageTile> _tiles;
        CollageTile tiles;
        Bitmap tilesetImg;
        public Bitmap TilesetImg
        {
            get
            {
                if (Equals(tilesetImg, null))
                {
                    tilesetImg = Tiles.CrearCollage();
                }
                return tilesetImg;
            }
        }
        public CollageTile Tiles
        {
            get
            {
                if (Equals(tiles, default))
                {
                    tiles = _tiles.First();
                }
                return tiles;
            }
        }
        IEnumerable<Bitmap> _bmps;
        Bitmap map;
        Bitmap border;
        public Bitmap MapImg
        {
            get
            {
                Bitmap[] bmps;
                if (Equals(map, null))
                {
                    bmps = _bmps.ToArray();
                    map = bmps[0];
                    border = bmps[1];   
                }
                return map;
            }
        }
        public Bitmap BorderImg
        {
            get
            {
                if(Equals(border, null))
                {
                    map = MapImg;
                }
                return border;
            }
        }

        ushort[,] TileData;
        ushort[,] BorderData;
        public IEnumerable<Bitmap> GetImgs(RomGba rom)
        {
            Bitmap mapImage=MapData.GetMapEmpty();
            Bitmap mapBorderImage = MapData.GetBorderEmpty();



            TileData = Maps.RenderMap(rom, mapImage, TilesetImg, this);
            //Maps.RenderAttributes(AttributeMap, AttributeBlocks, CurrentMap);



            Graphics BorderGraphics = Graphics.FromImage(mapBorderImage);

            BorderData = Maps.RenderBorder(rom, BorderGraphics, TilesetImg, this);


            yield return mapImage;
            yield return mapBorderImage;

       
        }

        #region Render Block



        IEnumerable<CollageTile> Render(RomGba game)
        {
            //tengo un problema con las paletas
            ushort TileData;

            int TileIndex;
            int FlipX;
            int FlipY;
            int PalIndex;
            int offset;
            byte[] RawGfx;
            byte[] TilesetGfx;

            Tileset major = MapData.GlobalTileset;
            Tileset minor = MapData.LocalTileset;
            CollageTile collage = new CollageTile();
            int[] TilesetImageOffsets = new int[2] { major.TilesetHeader.OffsetImagen, minor.TilesetHeader.OffsetImagen };
            int[] TilesetBlockDataOffset = new int[2] { major.TilesetHeader.OffsetBlockData, minor.TilesetHeader.OffsetBlockData };

            int x = 0;
            int y = 0;



            int[] BlockLimit = new int[2]; // Block limits for the 2 tilesets
            int[] TilesetSize = new int[2]; // Tileset filesize of the 2 tilesets
            List<byte> Gfx = new List<byte>();
            int MajorPalettes; // Number of palettes for the major tileset
            Color[] paleta = minor.Paleta.Colores.ToList().ToArray();
            PaletteHook(paleta);







            if (game.Edicion.EsKanto)
            {
                MajorPalettes = 112;
                BlockLimit[0] = 640;
                BlockLimit[1] = 512;
                TilesetSize[0] = 0x5000;
                TilesetSize[1] = 0X5000;
            }
            else
            {
                MajorPalettes = 96;
                BlockLimit[0] = 512;
                BlockLimit[1] = 512;
                TilesetSize[0] = 0x4000;
                TilesetSize[1] = 0X5000;
            }
            for (int i = 0; i < MajorPalettes; i++)
            {
                paleta[i] = major.Paleta.Colores[i];
            }

           

            // Reading Tileset



            for (int i = 0; i < TilesetImageOffsets.Length; i++)
            {

                RawGfx = game.Data.SubArray(TilesetImageOffsets[i], TilesetSize[i]);

                if (RawGfx[0] == 0x10) // Check for LZ77 compression
                    Gfx.AddRange(Decode(LZ77.Descomprimir(RawGfx)));
                else
                    Gfx.AddRange(Decode(RawGfx));

                if (i == 0 && Gfx.Count < 0x8000)
                {
                    for (int ii = 0; ii < 640; ii++)
                        Gfx.Add(0x0);
                }
            }

            TilesetGfx = Gfx.ToArray();



            byte[] PositionX = new byte[4] { 0, 8, 0, 8 };
            byte[] PositionY = new byte[4] { 0, 0, 8, 8 };




            
            for (int tilesetDataIndex = 0; tilesetDataIndex < TilesetBlockDataOffset.Length; tilesetDataIndex++) // 2 Tilesets
            {
                offset = TilesetBlockDataOffset[tilesetDataIndex];
                for (int i = 0; i < BlockLimit[tilesetDataIndex]; i++) // Amount of tiles for each tileset (block number)
                {
                    //construcción del bloque
                    //hay 8 bloques por linea

                   
                    for (int layer = 0; layer < 2; layer++) // 2 Layers (Bottom & Top)
                    {
                  
                        for (int tile = 0; tile < 4; tile++) // 4 Tiles for each layer
                        {

                            TileData = new Word(game, offset);
                            offset += Word.LENGTH;
                            TileIndex = TileData & 0x3FF;
                            FlipX = (TileData & 0x400) >> 10;
                            FlipY = (TileData & 0x800) >> 11;
                            PalIndex = (TileData & 0xF000) >> 12;

                            collage.Add(DrawTile8(TilesetGfx, TileIndex, FlipX == 1, FlipY == 1, PalIndex, paleta),new Point(x * 16 + PositionX[tile], y * 16 + PositionY[tile]),1-layer);
                       

                        }
                    }

                    x++;
                    if (x == 8)
                    {
                        x = 0;
                        y++;
                    }



                }
            }
            collage.Sort();
            yield return collage;

        }
        static byte[] Decode(byte[] rawData)
        {
            List<byte> DecodedData = new List<byte>();

            for (int i = 0; i < rawData.Length; i++)
            {
                DecodedData.Add((byte)(rawData[i] % 0x10));
                DecodedData.Add((byte)(rawData[i] / 0x10));
            }

            return DecodedData.ToArray();
        }

        static Bitmap DrawTile8(byte[] gfxData, int tileindex, bool flipX, bool flipY, int palIndex, Color[] palette)
        {
            int x = 0;
            int y = 0;
            int mx = 0;
            int my = 0;
            Bitmap bmp = new Bitmap(8, 8);
            FastPixel fpDrawer = new FastPixel(bmp);
            if (flipX)
                x += 7;
            if (flipY)
                y += 7;

            int TileSeeker = tileindex * 64;
            if (!(TileSeeker + 64 > gfxData.Length))
            {
                fpDrawer.Lock();
                for (int i = 0; i < 64; i++)
                {
                    if (gfxData[TileSeeker + i] > 0)
                        fpDrawer.SetPixel(x + (flipX ? -mx : mx), y + (flipY ? -my : my), palette[gfxData[TileSeeker + i] + (palIndex * 16)]);

                    mx++;
                    if (mx == 8)
                    {
                        mx = 0;
                        my++;
                    }
                }
                fpDrawer.Unlock();
            }
            return bmp;
        }

        private  void PaletteHook(Color[] palette)
        {
            int red;
            int blue;
            int green;
            ushort color;
            DayLight actual;
            bool mapTypeOk;

            switch (Tipo)
            {
                case TipoMapa.Pueblo:
                case TipoMapa.Cueva:
                case TipoMapa.Edificio:
                case TipoMapa.BaseSecreta:
                    mapTypeOk = false;
                    break;
                default:
                    mapTypeOk = true;
                    break;
            }

            if (Actual != DayLight.Normal && mapTypeOk)
            {

                actual = Actual;

                if (Actual == DayLight.Auto)
                {
                    if (DateTime.Now.Hour >= 4 && DateTime.Now.Hour < 6) // Morning
                        actual = DayLight.Morning;
                    else if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 17) // Day
                        actual = DayLight.Day;
                    else if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 18) // Afternoon
                    {
                        actual = DayLight.Afternoon;
                    }
                    else if (DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 22) // Evening
                    {
                        actual = DayLight.Evening;
                    }
                    else if (DateTime.Now.Hour >= 22 || DateTime.Now.Hour < 4) // Night
                    {
                        actual = DayLight.Night;
                    }
                }
                if (actual != DayLight.Day)
                {
                    for (int i = 0; i < palette.Length; i++)
                    {
                        color = BasePaleta.ToUshort(palette[i]);
                        red = 0x1E & color;
                        blue = (0x1E << 0x5) & color;
                        green = (0x1E << 0xA) & color;

                        switch (actual)
                        {
                            case DayLight.Morning:
                                green >>= 0x1;
                                break;
                            case DayLight.Afternoon:
                                blue >>= 0x1;
                                green >>= 0x1;
                                break;
                            case DayLight.Evening:
                                red >>= 0x1;
                                blue >>= 0x1;
                                break;
                            case DayLight.Night:
                                red >>= 0x1;
                                blue >>= 0x1;
                                green >>= 0x1;
                                break;

                        }

                        palette[i] = BasePaleta.ToColor((ushort)(red | blue | green));
                    }
                }
            }

        }


        #endregion

        public static bool Check(RomGba rom,int offsetMapHeader)
        {
            bool isOK = MapData.Check(rom, offsetMapHeader);
            return isOK;
        }


    }













    public class CollageTile : Collage
    {
        const int PartTileLength = 8;
        const int TileLength = PartTileLength * 2;
        public Collage this[int x,int y]
        {
            get
            {
          

                ImageFragment fragment;
                Collage tile = new Collage();
                        
                for (int i = 0; i <2; i++)
                {
                    fragment = GetFragment(x * TileLength, y * TileLength, i);
                    if (fragment!=null)
                    {
                        tile.Add(fragment.ImageBase, 0, 0,-i);
                    }
                    fragment = GetFragment(x * TileLength + PartTileLength, y * TileLength, i);
                    if (fragment!=null)
                    {
                        tile.Add(fragment.ImageBase, PartTileLength, 0,-i);
                    }
                    fragment = GetFragment(x * TileLength, y * TileLength + PartTileLength, i);
                    if (fragment != null)
                    {
                        tile.Add(fragment.ImageBase,0, PartTileLength,-i);
                    }
                    fragment = GetFragment(x * TileLength + PartTileLength, y * TileLength + PartTileLength, i);
                    if (fragment != null)
                    {
                        tile.Add(fragment.ImageBase, PartTileLength, PartTileLength,-i);
                    }
                }

                return tile;
            }
        }
    }
}
