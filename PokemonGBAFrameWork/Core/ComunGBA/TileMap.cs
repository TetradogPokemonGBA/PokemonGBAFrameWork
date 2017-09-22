/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/09/2017
 * Hora: 16:22
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TileMap.
	/// </summary>
	public class TileMap
	{
		int[,] tileMap;
		TileSet tileSet;

		public TileMap(int height,int width,TileSet tileSet)
		{
			tileMap=new int[width,height];
			this.tileSet=tileSet;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bmp"></param>
		/// <param name="estaConvertidaAGba">Convertida con Extension.ToGbaBitmap</param>
		public TileMap(Bitmap bmp,bool estaConvertidaAGba=false)
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
            if (bmp==null)
				throw new ArgumentNullException("bmp");
			if(bmp.Width%Tile.PIXELSPORLINEA!=0||bmp.Width%Tile.PIXELSPORLINEA!=0)
				throw new ArgumentException("La imagen tiene que ser divisible por "+Tile.PIXELSPORLINEA);
			if(bmp.Palette==null||bmp.Palette.Entries.Length>GranPaleta.COUNT)
				throw new ArgumentException("Error con la paleta");
			
			
			
			tileMap=new int[bmp.Width/Tile.PIXELSPORLINEA,bmp.Height/Tile.PIXELSPORLINEA];
            xFin = tileMap.GetLength(DimensionMatriz.X);
            yFin = tileMap.GetLength(DimensionMatriz.Y);
            total = xFin * yFin;
            if (!estaConvertidaAGba)
				imgData=bmp.GetBytes();
			else imgData=Gabriel.Cat.Extension.Extension.GetBytes(bmp);
			
			imgPalete=bmp.GetPaleta();
			
			tileSet=new TileSet(new GranPaleta(imgPalete));
			

			unsafe{
				
				byte*[] ptrsImg=new byte*[Tile.PIXELSPORLINEA];
				fixed(byte* ptrImgData=imgData)
				{
					bytesLinea=Extension.BYTESPORCOLOR*bmp.Width;
                    bytesBloque = bytesLinea * Tile.TOTALLINEAS;
					ptrsImg[0]=ptrImgData;

					for(int i=1;i<ptrsImg.Length;i++)
						ptrsImg[i]=ptrsImg[i-1]+bytesLinea;

					while(pos++<total)
					{
						
						tileCargada=new Tile(ptrsImg,tileSet.Paleta,bmp.Width);
						posTileEncontrada=-1;
						for(int j=0;j<tileSet.Tiles.Count&&posTileEncontrada<0;j++)
						{

							if(tileSet.Tiles[j].Equals(tileCargada))
								posTileEncontrada=j;
						}
						if(posTileEncontrada<0){
                            if (tileCargada == null)
                                System.Diagnostics.Debugger.Break();
							tileSet.Tiles.Add(tileCargada);
							posTileEncontrada=tileSet.Tiles.Count-1;
						}
						tileMap[x,y]=posTileEncontrada;
                        //avanzo los punteros
                        for (int j = 0; j < ptrsImg.Length; j++)
                            ptrsImg[j] += Tile.SIZEBYTESIMGLINEA;
                        //avanzo x para ver si puedo avanzar y
                        x++;

                        if (x == xFin&&y<yFin)
                        {
                            x = 0;
                            y++;
                            //si ya ha acabado las tiles de esa fila avanzo a la siguiente
                            for (int j = 0; j < ptrsImg.Length; j++)
                                ptrsImg[j] += bytesBloque;
                        }
						
					}
                   
				}
				
				
			}
		}

		public int[,] Map {
			get {
				return tileMap;
			}
		}

		public TileSet TileSet {
			get {
				return tileSet;
			}
		}
		public Tile this[int x,int y]
		{
			get{return tileSet.Tiles[Map[x,y]];}
			set{Map[x,y]=tileSet.Tiles.IndexOf(value);}
		}
		public Bitmap BuildBitmap()
		{
			return tileSet.BuildBitmap(tileMap);
		}
		public  Point GetPosicionTileMap(Point posicionImg)
		{
			return GetPosicionTileMap(tileMap,posicionImg);
		}
		public Tile GetTile(Point posicionImg)
		{
			return TileSet.Tiles[tileMap[posicionImg.X,posicionImg.Y]];
		}
		/// <summary>
		/// Obtiene las coordenadas X,Y del TileMap
		/// </summary>
		/// <param name="tileMap"></param>
		/// <param name="posicionImg">posición en el Bitmap</param>
		/// <returns>posición TileMap</returns>
		public static Point GetPosicionTileMap(int[,] tileMap,Point posicionImg)
		{
			
			int xMax=tileMap.GetLength(DimensionMatriz.X);
			int yMax=tileMap.GetLength(DimensionMatriz.Y);
			int x=posicionImg.X/Tile.PIXELSPORLINEA;
			int y=posicionImg.Y/Tile.PIXELSPORLINEA;
			
			if(x<0)
				x=0;
			
			else if(x>xMax)
				x=xMax;
			
			if(y<0)
				y=0;
			
			else if(y>yMax)
				y=yMax;
			
			return new Point(x,y);
		}
	}
}
