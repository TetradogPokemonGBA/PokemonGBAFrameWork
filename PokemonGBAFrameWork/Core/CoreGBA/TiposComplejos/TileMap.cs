using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class TileMap
    {
        int[,] tileMap;
        TileSet tileSet;

        public TileMap(int height, int width, TileSet tileSet)
        {
            tileMap = new int[width, height];
            this.tileSet = tileSet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="estaConvertidaAGba">Convertida con Extension.ToGbaBitmap</param>
        public TileMap(Bitmap bmp, bool estaConvertidaAGba = false)
        {

            int bytesLinea;
            Tile tileCargada;
            int posTileEncontrada;
            Color[] imgPalete;
            byte[] imgData;
            int x = 0;
            int xFin;
            int y = 0;
            int yFin;
            int total;
            int pos = 0;
            int bytesBloque;
            int[] ptrsImg;
            byte[] mapImg;
            GranPaleta paleta;
            if (bmp == null)
                throw new ArgumentNullException("bmp");
            if (bmp.Width % Tile.PIXELSPORLINEA != 0 || bmp.Width % Tile.PIXELSPORLINEA != 0)
                throw new ArgumentException("La imagen tiene que ser divisible por " + Tile.PIXELSPORLINEA);
            if (bmp.Palette == null || bmp.Palette.Entries.Length > GranPaleta.COUNT)
                throw new ArgumentException("Error con la paleta");



            tileMap = new int[bmp.Width / Tile.PIXELSPORLINEA, bmp.Height / Tile.PIXELSPORLINEA];
            xFin = tileMap.GetLength(DimensionMatriz.X);
            yFin = tileMap.GetLength(DimensionMatriz.Y);
            total = xFin * yFin;
            if (!estaConvertidaAGba)
                imgData = bmp.GetBytes();
            else imgData = Gabriel.Cat.Extension.Extension.GetBytes(bmp);

            imgPalete = bmp.GetPaleta();

            tileSet = new TileSet(new GranPaleta(imgPalete));
            paleta = tileSet.Paleta;

            mapImg = GranPaleta.GetMap(paleta, imgData);


            ptrsImg = new int[Tile.PIXELSPORLINEA];

            bytesLinea = bmp.Width;
            bytesBloque = bytesLinea * Tile.TOTALLINEAS;
            ptrsImg[0] = 0;

            for (int i = 1; i < ptrsImg.Length; i++)
                ptrsImg[i] = ptrsImg[i - 1] + bytesLinea;

            while (ptrsImg[0] < mapImg.Length)
            {

                tileCargada = new Tile(ptrsImg, mapImg, paleta);
                //parte terriblemente lenta!
                //algo pasa que al final no se ven bien...por mirar...
                posTileEncontrada = tileSet.IndexOf(tileCargada);
                if (posTileEncontrada < 0)
                {

                    tileSet.Add(tileCargada);
                    posTileEncontrada = tileSet.Count - 1;

                }

                tileMap[x, y] = posTileEncontrada;

                //avanzo x para ver si puedo avanzar y
                x++;

                if (x == xFin)
                {
                    x = 0;
                    y++;
                    //si ya ha acabado las tiles de esa fila avanzo a la siguiente
                    for (int j = 0; j < ptrsImg.Length; j++)
                        ptrsImg[j] += bytesBloque;
                }

            }
        }

        public int[,] Map
        {
            get
            {
                return tileMap;
            }
        }

        public TileSet TileSet
        {
            get
            {
                return tileSet;
            }
        }
        public Tile this[int x, int y]
        {
            get { return tileSet[Map[x, y]]; }
            set { Map[x, y] = tileSet[value]; }
        }
        public Bitmap BuildBitmap()
        {
            return tileSet.BuildBitmap(tileMap);
        }
        public Point GetPosicionTileMap(Point posicionImg)
        {
            return GetPosicionTileMap(tileMap, posicionImg);
        }
        public Tile GetTile(Point posicionImg)
        {
            return TileSet[tileMap[posicionImg.X, posicionImg.Y]];
        }
        /// <summary>
        /// Obtiene las coordenadas X,Y del TileMap
        /// </summary>
        /// <param name="tileMap"></param>
        /// <param name="posicionImg">posición en el Bitmap</param>
        /// <returns>posición TileMap</returns>
        public static Point GetPosicionTileMap(int[,] tileMap, Point posicionImg)
        {

            int xMax = tileMap.GetLength(DimensionMatriz.X);
            int yMax = tileMap.GetLength(DimensionMatriz.Y);
            int x = posicionImg.X / Tile.PIXELSPORLINEA;
            int y = posicionImg.Y / Tile.PIXELSPORLINEA;

            if (x < 0)
                x = 0;

            else if (x > xMax)
                x = xMax;

            if (y < 0)
                y = 0;

            else if (y > yMax)
                y = yMax;

            return new Point(x, y);
        }
    }
}
