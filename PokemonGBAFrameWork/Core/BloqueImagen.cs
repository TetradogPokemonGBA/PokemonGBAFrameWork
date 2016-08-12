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
		/// <summary>
		/// Pone la paleta al final a no ser que se diga la posición
		/// </summary>
		/// <param name="paleta"></param>
		/// <param name="index">si es negativo se pondrá al final</param>
		/// <returns></returns>
		public void AddPaleta(Color[] paleta,int index=-1)
		{
			if(paleta==null||paleta.Length!=TAMAÑOPALETA)throw new ArgumentException("Paleta incorrecta");
			if(index>paletas.Count)index=-1;
			if(index<0)
				paletas.Insert(paletas.Count,paleta);
			else paletas.Insert(index,paleta);
		}
		public void ReplacePaleta(Color[] paletaNueva,int indexPaletaAReemplazar)
		{
			if(paletas.Count<indexPaletaAReemplazar||indexPaletaAReemplazar<0)throw new ArgumentOutOfRangeException();
			if(paletaNueva==null||paletaNueva.Length!=TAMAÑOPALETA)throw  new ArgumentException("Paleta incorrecta");
			paletas.RemoveAt(indexPaletaAReemplazar);
			paletas.Insert(indexPaletaAReemplazar,paletaNueva);
		}
		public void RemovePaleta(int indexPaletaAEliminar){
			paletas.RemoveAt(indexPaletaAEliminar);
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
		public static BloqueImagen GetImage(RomPokemon rom,Hex offsetInicioDatos,params Hex[] offsetInicioPaletas)
		{
			if(rom==null||offsetInicioPaletas==null)throw new ArgumentNullException();
			if(offsetInicioDatos<0)throw new ArgumentOutOfRangeException(" offset datos imagen fuera de rango ");
			if(offsetInicioPaletas.Length==0)throw new ArgumentException("se necesita direcciones para obtener las paletas");
			for(int i=0;i<offsetInicioPaletas.Length;i++)
				if(offsetInicioPaletas[i]<0)
					throw new ArgumentOutOfRangeException("offset Paleta fuera de indice");
			List<Color[]> paletas=new List<Color[]>();
			for(int i=0;i<offsetInicioPaletas.Length;i++)
				paletas.Add(GetPaleta(rom,offsetInicioPaletas[i]));
			return new BloqueImagen(offsetInicioDatos,GetDatosComprimidosImagen(rom,offsetInicioDatos),paletas.ToArray());
			
		}
		public static byte[] GetDatosComprimidosImagen(RomPokemon rom,Hex offsetInicioDatos)
		{
			throw new NotImplementedException();
		}
	    public static Color[] GetPaleta(RomPokemon rom,Hex offsetInicioPaleta)
		{
			throw new NotImplementedException();
		}
	}
}
