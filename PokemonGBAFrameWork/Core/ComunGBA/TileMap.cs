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
			if(bmp==null)
				throw new ArgumentNullException("bmp");
			if(bmp.Width%Tile.PIXELSPORLINEA!=0||bmp.Width%Tile.PIXELSPORLINEA!=0)
				throw new ArgumentException("La imagen tiene que ser divisible por "+Tile.PIXELSPORLINEA);
			if(bmp.Palette==null||bmp.Palette.Entries.Length>GranPaleta.COUNT)
				throw new ArgumentException("Error con la paleta");
			
			
			
			tileMap=new int[bmp.Width/Tile.PIXELSPORLINEA,bmp.Height/Tile.PIXELSPORLINEA];
			
			if(!estaConvertidaAGba)
				imgData=bmp.GetBytes();
			else imgData=Gabriel.Cat.Extension.Extension.GetBytes(bmp);
			
			imgPalete=bmp.GetPaleta();
			
			tileSet=new TileSet(new GranPaleta(imgPalete));
			

			unsafe{
				
				byte*[] ptrsImg=new byte*[Tile.PIXELSPORLINEA];
				fixed(byte* ptrImgData=imgData)
				{
					bytesLinea=Extension.BYTESPORCOLOR*bmp.Width;
					ptrsImg[0]=ptrImgData;
					for(int i=1;i<ptrsImg.Length;i++)
						ptrsImg[i]=ptrsImg[i-1]+bytesLinea;
					for(int i=0,x=0,xFin=tileMap.GetLength(DimensionMatriz.X),y=0,f=bmp.Width*bmp.Height*Extension.BYTESPORCOLOR;i<f;i+=Tile.SIZEBYTESIMG)
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
						if(x==xFin)
						{
							x=0;
							y++;
						}
						//avanzo los punteros
						for(int j=0;j<ptrsImg.Length;j++)
							ptrsImg[j]+=Tile.SIZEBYTESIMGLINEA;
						
					}
					if(System.Diagnostics.Debugger.IsAttached)
						System.Diagnostics.Debugger.Break();
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
