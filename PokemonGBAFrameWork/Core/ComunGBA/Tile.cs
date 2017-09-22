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
		
		
		public const int PIXELSPORLINEA=8;
		public const int TOTALPIXELS=PIXELSPORLINEA*PIXELSPORLINEA;
		public const int SIZEBYTESIMGLINEA=PIXELSPORLINEA*Extension.BYTESPORCOLOR;
		public const int SIZEBYTESIMG=PIXELSPORLINEA*SIZEBYTESIMGLINEA;
		
		byte[] datos;
		GranPaleta paleta;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="ptrsImg">La imagen tiene que ser de Extension.ToGbaBitmap</param>
		/// <param name="paleta"></param>
		/// <param name="widthImg"></param>
		public unsafe Tile(byte*[] ptrsImg,GranPaleta paleta,int widthImg)
		{
			if(ptrsImg.Length!=PIXELSPORLINEA)
				throw new ArgumentOutOfRangeException("ptrsImg",String.Format("Tienen que ser {0} para poder leer la imagen correctamente.",PIXELSPORLINEA));
			
			byte? pos=0;
			int bytesLinea=widthImg*Extension.BYTESPORCOLOR;
			byte*[]ptrsData=new byte*[PIXELSPORLINEA];
			
			this.paleta=paleta;
			datos=new byte[TOTALPIXELS];
			fixed(byte* ptrData=datos)
			{
				ptrsData[0]=ptrData;
				for(int i=1;i<PIXELSPORLINEA;i++)
				{
					ptrsData[i]=ptrsData[i-1]+bytesLinea;
				}
				
				for(int i=0;i<datos.Length&&pos.HasValue;i+=PIXELSPORLINEA)
				{
					
					for(int j=0;j<PIXELSPORLINEA&&pos.HasValue;j++)
					{
						pos=paleta.GetPosicion(*(ptrsImg[j]+Extension.R),*(ptrsImg[j]+Extension.G),*(ptrsImg[j]+Extension.B));
						if(pos.HasValue){
							
							*ptrsData[j]=pos.Value;
							ptrsImg[j]+=Extension.BYTESPORCOLOR;
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
