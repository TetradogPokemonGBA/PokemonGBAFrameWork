/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 21:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of BloqueImagen.
	/// </summary>
	public class BloqueImagen
	{
		public const int TAMAÑOPALETA=16;
		Hex offsetInicio;
		byte[] datosImagenComprimida;
		List<Color[]> paletas;
		public BloqueImagen(Hex offsetInicio,byte[] datosImagenComprimida,params Color[][] paletas)
		{
			if(datosImagenComprimida==null||paletas==null)
				throw new ArgumentNullException();
			if(paletas.Length==0)
				throw new ArgumentException("Se necesita al menos una paleta");
			for(int i=0,f=paletas.Length;i<f;i++)
				if(paletas[i].Length!=TAMAÑOPALETA)
					throw new ArgumentOutOfRangeException(String.Format("Se ha encontrado que la paleta {0} tiene un tamaño diferente a {1}",i,TAMAÑOPALETA));
			if(!ValidarDatosImagenComprimida(datosImagenComprimida))
				throw new ArgumentException("Los bytes no son de una imagen correcta");
			
			OffsetInicio=offsetInicio;
			this.datosImagenComprimida=datosImagenComprimida;
			this.paletas=new List<Color[]>(paletas);
		}

		public Hex OffsetInicio {
			get {
				return offsetInicio;
			}
			set {
				if(value<0)throw new ArgumentOutOfRangeException();
				offsetInicio = value;
			}
		}
		public Hex OffsetFin{
			get{return OffsetInicio+DatosImagenComprimida.Length;}
		}
		public Bitmap this[int index] {
			get {
				if(index<0||paletas.Count<index)
					throw new ArgumentOutOfRangeException("Fuera de rango en la tabla de paletas");
				return GenerarImagen(datosImagenComprimida,paletas[index]);
			}
			set {
				Color[] paleta;
				if(value==null)throw new ArgumentNullException();
				//la valido
				datosImagenComprimida =ObtenerBytesComprimidosImagen(value);
				paleta=ObtenerPaletaImagen(value);
				if(paletas.Count<index)
					paletas[index]=paleta;
				else paletas.Insert(paletas.Count,paleta);
			}
		}
		public Color[] GetPaleta(int index)
		{
			return paletas[index];
		}
		
		public byte[] DatosImagenComprimida{
			get{return datosImagenComprimida;}
		}

		public static bool ValidarDatosImagenComprimida(byte[] datosImagenComprimida)
		{
			throw new NotImplementedException();
		}

		public static Color[] ObtenerPaletaImagen(Bitmap value)
		{
			throw new NotImplementedException();
		}
		public byte[] ObtenerBytesComprimidosImagen(Bitmap value)
		{
			throw new NotImplementedException();
		}
		public static Bitmap GenerarImagen(byte[] datosImagenComprimida, Color[] color)
		{
			throw new NotImplementedException();
		}
	}
}
