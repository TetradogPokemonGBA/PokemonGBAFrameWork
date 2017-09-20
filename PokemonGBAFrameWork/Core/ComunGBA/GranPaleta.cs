/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 20/09/2017
 * Hora: 16:25
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of GranPaleta.
	/// </summary>
	public class GranPaleta
	{
		public const int LENGHT=256;
		
		Color[] paleta;
		public GranPaleta()
		{
			paleta=new Color[LENGHT];
		}
		public Color this[int index]
		{
			get{return paleta[index];}
			set{paleta[index]=value;}
		}
		public int Lenght
		{
			get{
				return LENGHT;
			}
		}
		public void ConvertirGBAColor()
		{
			for(int i=0;i<paleta.Length;i++)
				paleta[i]=Paleta.ToGBAColor(paleta[i]);
		}
		public byte? GetPosicion(Color color)
		{
			return GetPosicion(color.R,color.G,color.B);
		}
		public byte? GetPosicion(byte r,byte g,byte b)
		{
			byte? posicion=null;
			for(byte i=0;i<LENGHT&&posicion<0;i++)
			{
				if(paleta[i].R==r&&paleta[i].G==g&&paleta[i].B==b)
					posicion=i;
			}
			return posicion;
		}
	}
}
