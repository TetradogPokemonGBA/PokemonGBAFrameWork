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
	public class Tile:IComparable<Tile>,IComparable
	{
		
		
		public const int PIXELSPORLINEA=8;
		public const int TOTALLINEAS = PIXELSPORLINEA;
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
		public  Tile(int[] ptrsImg,byte[] imgMap,GranPaleta paleta)
		{
			if(ptrsImg.Length!=PIXELSPORLINEA)
				throw new ArgumentOutOfRangeException("ptrsImg",String.Format("Tienen que ser {0} para poder leer la imagen correctamente.",PIXELSPORLINEA));
			
			int[] ptrsData=new int[PIXELSPORLINEA];
			
			this.paleta=paleta;
			datos=new byte[TOTALPIXELS];
			
			ptrsData[0]=0;
			unsafe{
				int* posicionesImg;
				int* posicionesTile;
				byte* ptrDatos;
				byte* ptrImgMap;
				fixed(int* posTile=ptrsData)
				{
					posicionesTile=posTile;
					posicionesTile++;
					for(int i=1;i<PIXELSPORLINEA;i++)
					{
						*posicionesTile=*(posicionesTile-1)+Tile.PIXELSPORLINEA;
						posicionesTile++;
					}
					posicionesTile=posTile;
					fixed(int* posImg=ptrsImg)
					{
						fixed(byte* ptDatos=datos)
						{
							fixed(byte* ptImgMap=imgMap)
							{
								
								ptrImgMap=ptImgMap;
								ptrDatos=ptDatos;
								posicionesImg=posImg;
								
								for(int i=0;i<datos.Length;i+=PIXELSPORLINEA)
								{
									//pongo la linea
									for(int j=0;j<PIXELSPORLINEA;j++)
									{
										ptrDatos[*posicionesTile]=ptrImgMap[*posicionesImg];
										(*posicionesTile)=(*posicionesTile)+1;
										(*posicionesImg)=(*posicionesImg)+1;
										
									}
									//avanzo a la siguiente linea
									posicionesTile++;
									posicionesImg++;
								}
							}
						}
					}
				}
			}

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
			tileSetAux.Add(this);
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

		#region IComparable implementation
		public int CompareTo(object obj)
		{
			return ICompareTo(obj as Tile);
		}
		#endregion
		#region IComparable implementation
		public int CompareTo(Tile other)
		{
			return ICompareTo(other);
		}
		public int ICompareTo(Tile other)
		{
			const int IGUALES=(int)Gabriel.Cat.CompareTo.Iguales;
			int compareTo=other!=null?IGUALES:(int)Gabriel.Cat.CompareTo.Inferior;
			if (compareTo == IGUALES)
			{
				compareTo = (int)datos.CompareTo(other.datos);
			}
			return compareTo;
		}
		#endregion
	}
}
