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
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TileSet.
	/// </summary>
	public class TileSet
	{
		public static int TilesPorLinea=5;
		GranPaleta paleta;
		Llista<Tile> tiles;
		public TileSet()
		{
			tiles=new Llista<Tile>();
			paleta=new GranPaleta();
		}

		public Llista<Tile> Tiles {
			get {
				return tiles;
			}
		}

		public GranPaleta Paleta {
			get {
				return paleta;
			}
		}
		public Bitmap BuildBitmap()
		{
			const int DEFAULTLIENA=5;
			int width;
			int height;
			int[,] tileMap;
			if(TilesPorLinea<=0)
				TilesPorLinea=DEFAULTLIENA;
			
			width=TilesPorLinea>Tiles.Count?Tiles.Count:TilesPorLinea;
			height=Tiles.Count/TilesPorLinea+(width==TilesPorLinea&&Tiles.Count%TilesPorLinea!=0?1:0);
			
			tileMap=new int[width,height];
			//los pongo por orden
			for(int x=0,xMax=width,yMax=height,i=0;x<xMax;x++)
				for(int y=0;y<yMax;y++,i++)
					tileMap[x,y]=i;
			
			return BuildBitmap(tileMap);
		}
		public Bitmap BuildBitmap(int[,] tileMap)
		{
			Color colorActual;
			int lenghtX=tileMap.GetLength(DimensionMatriz.X);
			int lenghtY=tileMap.GetLength(DimensionMatriz.Y);
			Bitmap bmp=new Bitmap(lenghtX*Tile.PIXELSPORLINEA,lenghtY*Tile.PIXELSPORLINEA);
			int bytesLinea;
			int bytesBloque;
			int pos;
			if(Tiles.Count>0){
				unsafe{
					
					byte*[] ptrsImg=new byte*[Tile.PIXELSPORLINEA];
					byte*[] ptrsTile=new byte*[Tile.PIXELSPORLINEA];
					bmp.TrataBytes((MetodoTratarBytePointer)((ptrBytes)=>{
					                                         	
					                                         	//pongo los tiles
					                                         	bytesLinea=Extension.BYTESPORCOLOR*bmp.Width;
					                                         	bytesBloque=bytesLinea*Tile.PIXELSPORLINEA;
					                                         	ptrsImg[0]=ptrBytes;
					                                         	for(int i=1;i<ptrsImg.Length;i++)
					                                         		ptrsImg[i]=ptrsImg[i-1]+bytesLinea;
					                                         	for(int y=0;y<lenghtY;y++){
					                                         		for(int x=0;x<lenghtX;x++)
					                                         		{
					                                         			pos=tileMap[x,y];
					                                         			
					                                         			if(pos<0||pos>GranPaleta.LENGHT)
					                                         				pos=0;
					                                         			
					                                         			fixed(byte* ptrTile=tiles[pos].Datos)
					                                         			{
					                                         				ptrsTile[0]=ptrTile;
					                                         				for(int i=1;i<ptrsTile.Length;i++)
					                                         					ptrsTile[i]=ptrsTile[i-1]+Tile.PIXELSPORLINEA;
					                                         				//pongo cada linea de pixeles
					                                         				for(int j=0;j<Tile.PIXELSPORLINEA;j++)
					                                         				{
					                                         					//pongo toda la linea
					                                         					for(int k=0;k<Tile.PIXELSPORLINEA;k++)
					                                         					{
					                                         						//cojo el color que toca
					                                         						colorActual=Paleta[*ptrsTile[j]];
					                                         						//pongo el pixel
					                                         						*ptrsImg[j]=colorActual.A;
					                                         						ptrsImg[j]++;
					                                         						*ptrsImg[j]=colorActual.R;
					                                         						ptrsImg[j]++;
					                                         						*ptrsImg[j]=colorActual.G;
					                                         						ptrsImg[j]++;
					                                         						*ptrsImg[j]=colorActual.B;
					                                         						ptrsImg[j]++;
					                                         						//avanzo el pointer del tile
					                                         						ptrsTile[j]++;
					                                         					}
					                                         					
					                                         				}
					                                         			}
					                                         			
					                                         		}
					                                         		//avanzo los pointers
					                                         		for(int i=0;i<ptrsTile.Length;i++)
					                                         			ptrsTile[i]=ptrsTile[i]+bytesBloque;
					                                         		
					                                         	}
					                                         	
					                                         }));
				}
			}
			return bmp;
		}
	}
	
}
