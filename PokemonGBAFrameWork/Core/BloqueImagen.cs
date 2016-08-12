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
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of BloqueImagen.
	/// </summary>
	public class BloqueImagen
	{
		public class Paleta
		{
			public const int TAMAÑOPALETA = 16;
			Hex offsetInicio;
			Color[] paleta;
			public Paleta(Hex offsetInicio, Color[] paleta)
			{
				OffsetInicio = offsetInicio;
				ColoresPaleta = paleta;
			}
			public Hex OffsetInicio {
				get {
					return offsetInicio;
				}
				set {
					if (offsetInicio < 0)
						throw new ArgumentOutOfRangeException();
					offsetInicio = value;
				}
			}

			

			public Color[] ColoresPaleta {
				get {
					return paleta;
				}
				set {
					if (value == null || value.Length != TAMAÑOPALETA)
						throw new ArgumentException();
					paleta = value;
				}
			}
			public static Paleta GetPaleta(RomPokemon rom, Hex offsetInicioPaleta)
			{
				throw new NotImplementedException();
			}
			public static Paleta GetPaleta(Bitmap img)
			{
				throw new NotImplementedException();
			}
			
			public static void SetPaleta(RomPokemon rom, Paleta paleta)
			{
			
				throw new NotImplementedException();
			}
		}
		
		Hex offsetInicio;
		byte[] datosImagenComprimida;
		List<Paleta> paletas;
		public BloqueImagen(Hex offsetInicio, byte[] datosImagenComprimida, params Paleta[] paletas)
		{
			if (datosImagenComprimida == null || paletas == null)
				throw new ArgumentNullException();
			if (paletas.Length == 0)
				throw new ArgumentException("Se necesita al menos una paleta");
			if (!ValidarDatosImagenComprimida(datosImagenComprimida))
				throw new ArgumentException("Los bytes no son de una imagen correcta");
			
			OffsetInicio = offsetInicio;
			this.datosImagenComprimida = datosImagenComprimida;
			this.paletas = new List<Paleta>(paletas);
		}

		public Hex OffsetInicio {
			get {
				return offsetInicio;
			}
			set {
				if (value < 0)
					throw new ArgumentOutOfRangeException();
				offsetInicio = value;
			}
		}
		public Hex OffsetFin {
			get{ return OffsetInicio + DatosImagenComprimida.Length; }
		}
		/// <summary>
		/// Obtener la imagen con la paleta del index /establecer la imagen y añadir(al final de la lista) o reemplazar la paleta que sea
		/// </summary>
		public Bitmap this[int index] {
			get {
				if (index < 0 || paletas.Count < index)
					throw new ArgumentOutOfRangeException("index");
				return GetImage(datosImagenComprimida, paletas[index]);
			}
			set {
				Paleta paleta;
				if (value == null)
					throw new ArgumentNullException();
				//la valido
				datosImagenComprimida = GetDatosComprimidosImagen(value);
				paleta = Paleta.GetPaleta(value);
				if (paletas.Count < index)
					paletas[index] = paleta;
				else
					paletas.Insert(paletas.Count, paleta);
			}
		}
		public Paleta GetPaleta(int index)
		{
			return paletas[index];
		}
		/// <summary>
		/// Pone la paleta al final a no ser que se diga la posición
		/// </summary>
		/// <param name="paleta"></param>
		/// <param name="index">si es negativo se pondrá al final</param>
		/// <returns></returns>
		public void AddPaleta(Paleta paleta, int index = -1)
		{
			if (paleta == null)
				throw new ArgumentNullException();
			if (index > paletas.Count)
				index = -1;
			if (index < 0)
				paletas.Insert(paletas.Count, paleta);
			else
				paletas.Insert(index, paleta);
		}
		public void ReplacePaleta(Paleta paletaNueva, int indexPaletaAReemplazar)
		{
			if (paletas.Count < indexPaletaAReemplazar || indexPaletaAReemplazar < 0)
				throw new ArgumentOutOfRangeException();
			if (paletaNueva == null)
				throw  new ArgumentNullException();
			paletas.RemoveAt(indexPaletaAReemplazar);
			paletas.Insert(indexPaletaAReemplazar, paletaNueva);
		}
		public void RemovePaleta(int indexPaletaAEliminar)
		{
			paletas.RemoveAt(indexPaletaAEliminar);
		}
		
		public byte[] DatosImagenComprimida {
			get{ return datosImagenComprimida; }
		}

		public static bool ValidarDatosImagenComprimida(byte[] datosImagenComprimida)
		{
			throw new NotImplementedException();
		}

		public byte[] GetDatosComprimidosImagen(Bitmap value)
		{
			throw new NotImplementedException();
		}
		public static Bitmap GetImage(byte[] datosImagenComprimida, Paleta color)
		{
			throw new NotImplementedException();
		}
		public static byte[] GetDatosComprimidosImagen(RomPokemon rom, Hex offsetInicioDatos)
		{
			throw new NotImplementedException();
		}
		public static BloqueImagen GetImage(RomPokemon rom, Hex offsetInicioDatos, params Paleta[] paletas)
		{
			if (rom == null || paletas == null)
				throw new ArgumentNullException();
			if (offsetInicioDatos < 0)
				throw new ArgumentOutOfRangeException("offsetInicioDatos");
			return new BloqueImagen(offsetInicioDatos, GetDatosComprimidosImagen(rom, offsetInicioDatos), paletas);
			
		}
		public static void SetImage(RomPokemon rom, BloqueImagen img)
		{
		
			if (rom == null || img == null)
				throw new ArgumentNullException();
			BloqueBytes.SetBytes(rom, img.OffsetInicio, img.DatosImagenComprimida);
			for (int i = 0; i < img.paletas.Count; i++)
				Paleta.SetPaleta(rom, img.paletas[i]);
		}
		public static void SetImage(RomPokemon rom, Hex offsetInicio, Bitmap img)
		{
			SetImage(rom, offsetInicio, img, new Paleta[]{ });
		}
		public static void SetImage(RomPokemon rom, Hex offsetInicio, Bitmap img, params Paleta[] masPaletas)
		{
			SetImage(rom, new BloqueImagen(offsetInicio, GetDatosComprimidosImagen(img), masPaletas.AfegirValor(Paleta.GetPaleta(img)).ToTaula()));
		}
		public static void SetImage(RomPokemon rom, Hex offsetInicio, byte[] datosImg, params Paleta[] paletas)
		{
			SetImage(rom, new BloqueImagen(offsetInicio, datosImg, paletas));
		}
		

	}
}
