/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/09/2017
 * Hora: 16:23
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Tile.
	/// </summary>
	public class Tile
	{
		const int BYTESPORCOLOR=4;
		public const int SIZE=8;
		public const int SIZEBYTESIMGLINEA=SIZE*BYTESPORCOLOR;
		public const int SIZEBYTESIMG=SIZE*SIZEBYTESIMGLINEA;
		
		byte[] datos;
		GranPaleta paleta;
		public unsafe Tile(byte*[] ptrsImg,GranPaleta paleta,int widthImg)
		{
			if(ptrsImg.Length!=SIZE)
				throw new ArgumentOutOfRangeException();
			
			//A=0
			const int R = 1;
			const int G = R+1;
			const int B = G+1;
			
			byte? pos=0;
			int bytesLinea=widthImg*BYTESPORCOLOR;
			byte*[]ptrsData=new byte*[SIZE];
			
			this.paleta=paleta;
			datos=new byte[SIZE*SIZE];
			fixed(byte* ptrData=datos)
			{
				ptrsData[0]=ptrData;
				for(int i=1;i<SIZE;i++)
				{
					ptrsData[i]=ptrsData[i-1]+bytesLinea;
				}
				
				for(int i=0;i<datos.Length&&pos.HasValue;i+=SIZE)
				{
					
					for(int j=0;j<SIZE&&pos.HasValue;j++)
					{
						pos=paleta.GetPosicion(*(ptrsImg[j]+R),*(ptrsImg[j]+G),*(ptrsImg[j]+B));
						if(pos.HasValue){
							
							*ptrsData[j]=pos.Value;
							ptrsImg[j]+=BYTESPORCOLOR;
							ptrsData[j]++;
						}
					}
				}
			}
			if(!pos.HasValue)
				throw new ArgumentException("Color no encontrado en la paleta...");
		}

		public byte[] Datos {
			get {
				return datos;
			}
		}

		public GranPaleta Paleta {
			get {
				return paleta;
			}
			set {
				paleta = value;
			}
		}
		public Bitmap BuildBitmap()
		{
			TileSet tileSetAux=new TileSet();
			tileSetAux.Tiles.Add(this);
			return tileSetAux.BuildBitmap(new int[1,1]);
		}
		public override bool Equals(object obj)
		{
			Tile otherTile=obj as Tile;
			bool equals=otherTile!=null;
			if(equals)
				equals=datos.ArrayEqual(otherTile.datos);
			return equals;
			
		}

	}
}
