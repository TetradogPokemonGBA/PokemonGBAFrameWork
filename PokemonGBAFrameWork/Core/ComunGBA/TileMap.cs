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
		public TileMap(Bitmap bmp)
		{
			const int BYTESPORCOLOR=4;
			int bytesLinea;
			Tile tileCargada;
			int posTileEncontrada;
			
			if(bmp.Palette==null||bmp.Palette.Entries.Length>GranPaleta.LENGHT)
				throw new ArgumentException("Error con la paleta");
			if(bmp.Width%Tile.SIZE!=0||bmp.Width%Tile.SIZE!=0)
				throw new ArgumentException("La imagen tiene que ser divisible por "+Tile.SIZE);
			
			tileSet=new TileSet();
			
			for(int i=0;i<bmp.Palette.Entries.Length;i++)
			{
				tileSet.Paleta[i]=bmp.Palette.Entries[i];
			}
			unsafe{
				
				byte*[] ptrsImg=new byte*[Tile.SIZE];
				fixed(byte* ptrImgData=bmp.GetBytes())
				{
					bytesLinea=BYTESPORCOLOR*bmp.Width;
					ptrsImg[0]=ptrImgData;
					for(int i=1;i<ptrsImg.Length;i++)
						ptrsImg[i]=ptrsImg[i-1]+bytesLinea;
					for(int i=0,x=0,xFin=tileMap.GetLength(DimensionMatriz.X),y=0,f=bmp.Width*bmp.Height*BYTESPORCOLOR;i<f;i+=Tile.SIZEBYTESIMG)
					{
						
						tileCargada=new Tile(ptrsImg,tileSet.Paleta,bmp.Width);
						posTileEncontrada=-1;
						for(int j=0;j<tileSet.Tiles.Count&&posTileEncontrada<0;j++)
						{
							if(tileSet.Tiles[j].Equals(tileCargada))
								posTileEncontrada=j;
						}
						if(posTileEncontrada<0){
							tileSet.Tiles.Add(tileCargada);
							posTileEncontrada=tileSet.Tiles.Count-1;
						}
						tileMap[x,y]=posTileEncontrada;
						x++;
						if(x>xFin)
						{
							x=0;
							y++;
						}
						//avanzo los punteros
						for(int j=0;j<ptrsImg.Length;j++)
							ptrsImg[i]+=Tile.SIZEBYTESIMGLINEA;
					}
				}
				
				
			}
			//pongo los colores a GBA :)
			tileSet.Paleta.ConvertirGBAColor();
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
		public Bitmap BuildBitmap()
		{
			return tileSet.BuildBitmap(tileMap);
		}
		public  Point GetPosicionTileMap(Point posicionImg)
		{
			return GetPosicionTileMap(tileMap,posicionImg);
		}
		public static Point GetPosicionTileMap(int[,] tileMap,Point posicionImg)
		{
			
			int xMax=tileMap.GetLength(DimensionMatriz.X);
			int yMax=tileMap.GetLength(DimensionMatriz.Y);
			int x=posicionImg.X/Tile.SIZE;
			int y=posicionImg.Y/Tile.SIZE;
			
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
