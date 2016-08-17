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
			public Paleta(Color[] paleta)
				: this(0, paleta)
			{
			}
			public Paleta(Hex offsetInicio, Color[] paleta)
			{
				if (paleta == null)
					throw new ArgumentNullException("paleta");
				OffsetInicio = offsetInicio;
				ColoresPaleta = paleta;
			}
			public Paleta(Bitmap img)
				: this(0, img)
			{
			}
			public Paleta(Hex offsetInicio, Bitmap img)
			{
				if (img == null)
					throw new ArgumentNullException();
				SortedList<int,Color> coloresImg = new SortedList<int, Color>();
				Color color;
				OffsetInicio = offsetInicio;
				//creo la paleta con la img
				unsafe {
					img.TrataBytes(((Gabriel.Cat.Extension.MetodoTratarBytePointer)((bytesImg) => {
					                                                                	
					                                                                	for (int i = 0, f = img.LengthBytes(); i < f; i += 3) {
					                                                                		color = Color.FromArgb(bytesImg[i], bytesImg[i + 1], bytesImg[i + 2]);
					                                                                		if (!coloresImg.ContainsKey(color.ToArgb()))
					                                                                			coloresImg.Add(color.ToArgb(), color);
					                                                                	}
					                                                                	
					                                                                	
					                                                                	
					                                                                })));
					
				}
				if (coloresImg.Count > TAMAÑOPALETA)
					throw new ArgumentException("la imagen supera el maximo de calores para crear la paleta");
				ColoresPaleta = TrataPaleta(coloresImg.ValuesToArray());
			}

			Color[] TrataPaleta(Color[] paletaATratar)
			{
				Color[] paletaValida = new Color[TAMAÑOPALETA];
				for (int i = 0; i < paletaATratar.Length && i < paletaValida.Length; i++)
					paletaValida[i] = paletaATratar[i];
				return paletaValida;
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
						throw new ArgumentException("error  con la paleta, puede ser null o el tamaño no es el correcto");
					paleta = value;
				}
			}
			/// <summary>
			/// Crea una nueva imagen con la paleta que hay actualmente.
			/// </summary>
			/// <param name="img"></param>
			/// <returns></returns>
			public  Bitmap SetPaleta(Bitmap img)
			{
				//le pongo la paleta
				return BloqueImagen.GetImage(BloqueImagen.GetDatosComprimidosImagen(img),this);

			}
			public static Paleta GetPaletaEmpty()
			{
				return new Paleta(new Color[TAMAÑOPALETA]);
			}
			public static Paleta GetPaleta(Bitmap img)
			{
				return new Paleta(img);
			}

			public static Paleta GetPaleta(RomPokemon rom, Hex offsetInicioPaleta)
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
		public BloqueImagen(Hex offsetInicio, Bitmap img, params Paleta[] paletas)
			: this(offsetInicio, BloqueImagen.GetDatosComprimidosImagen(img), paletas.AfegirValor(new Paleta(img).ToTaula()))
		{
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
				if (value == null)
					throw new ArgumentNullException();
				if (index >= paletas.Count)
					throw new ArgumentOutOfRangeException("index");
				//la valido
				datosImagenComprimida = GetDatosComprimidosImagen(value);
				paletas[index] = new Paleta(value);//saco la paleta :)

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
			set {
				if (!ValidarDatosImagenComprimida(value))
					throw new ArgumentException("Los datos no son validos!!");
				datosImagenComprimida = value;
				
			}
		}

		public static bool ValidarDatosImagenComprimida(byte[] datosImagenComprimida)
		{
			if (datosImagenComprimida == null)
				throw new ArgumentNullException("datosImagenComprimida");
			bool valida;
			throw new NotImplementedException();
		}

		public static byte[] GetDatosComprimidosImagen(Bitmap img)
		{
			if (datosImagenComprimida == null)
				throw new ArgumentNullException("img");
			throw new NotImplementedException();
		}
		public static Bitmap GetImage(byte[] datosImagenComprimida, Paleta paleta)
		{
			if (datosImagenComprimida == null || paleta == null)
				throw new ArgumentNullException();
			throw new NotImplementedException();
		}
		public static byte[] GetDatosComprimidosImagen(RomPokemon rom, Hex offsetInicioDatos)
		{
			if (rom == null || offsetInicioDatos < 0)
				throw new ArgumentException();
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
			if (img == null || masPaletas == null)
				throw new ArgumentNullException();
			SetImage(rom, new BloqueImagen(offsetInicio, GetDatosComprimidosImagen(img), masPaletas.AfegirValor(Paleta.GetPaleta(img)).ToTaula()));
		}
		public static void SetImage(RomPokemon rom, Hex offsetInicio, byte[] datosImg, params Paleta[] paletas)
		{
			SetImage(rom, new BloqueImagen(offsetInicio, datosImg, paletas));
		}
		

	}
}
