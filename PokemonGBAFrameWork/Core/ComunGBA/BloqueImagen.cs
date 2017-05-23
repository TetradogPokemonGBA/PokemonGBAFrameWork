/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 9:36
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		public const int LENGTHHEADER = 4;
		public const int LENGTHHEADERCOMPLETO = OffsetRom.LENGTH + LENGTHHEADER;
		static readonly byte[] DefaultHeader = { 0x0, 0x8 };
		static readonly byte[] DefaultHeader2 = { 0x0, 0x10 };
		int offset;
		short id;
		short formato;
		BloqueBytes datosDescomprimidos;
		Llista<Paleta> paletas;

		public BloqueImagen()
		{
			paletas = new Llista<Paleta>();
			datosDescomprimidos = new BloqueBytes(0);
			offset = -1;
			id = -1;
			formato = -1;

		}
		public BloqueImagen(int longitudLado) : this(new Bitmap(longitudLado, longitudLado))
		{

		}
		public BloqueImagen(Bitmap img) : this()
		{
			datosDescomprimidos = GetDatosDescomprimidos(img);
			Paletas.Add(Paleta.GetPaleta(img));
		}
		public BloqueImagen(BloqueBytes datosDescomprimidosImg, params Paleta[] paletas) : this()
		{
			if (datosDescomprimidosImg == null || paletas == null)
				throw new ArgumentNullException();

			datosDescomprimidos = datosDescomprimidosImg;
			Paletas.AddRange(paletas);
		}
		public int Offset
		{
			get
			{
				return offset;
			}
			set
			{

				offset = value;
			}
		}
		public short Id
		{
			get
			{
				return id;
			}
			set
			{

				id = value;
			}
		}

		public short Formato
		{
			get
			{
				return formato;
			}
			set
			{
				formato = value;
			}
		}
		private byte[] Header
		{
			get { return Serializar.GetBytes(formato).AddArray(Serializar.GetBytes(id)); }
			set
			{
				if (value == null)
					throw new ArgumentNullException();
				if (value.Length < LENGTHHEADER)
					value = value.AddArray(new byte[LENGTHHEADER - value.Length]);

				formato = Serializar.ToShort(value.SubArray(2));
				id = Serializar.ToShort(value.SubArray(2, 2));

			}
		}
		public byte[] HeaderCompleto
		{
			get { return GetHeaderCompleto(Offset); }

		}
		public BloqueBytes DatosDescomprimidos
		{
			get
			{
				return datosDescomprimidos;
			}
		}

		public Llista<Paleta> Paletas
		{
			get
			{
				return paletas;
			}
		}
		public Bitmap this[int indexPaleta]
		{
			get { return this[paletas[indexPaleta]]; }
		}
		public Bitmap this[Paleta paleta]
		{
			get { return BuildBitmap(datosDescomprimidos.Bytes, paleta); }
		}
		public byte[] GetHeaderCompleto(int pointerData)
		{
			return new OffsetRom(pointerData).BytesPointer.AddArray(Header);
		}
		public Bitmap GetImg(int indexPaleta = 0, bool showBackGround = false)
		{
			return BuildBitmap(DatosDescomprimidos.Bytes, Paletas[indexPaleta], showBackGround);
		}
		public byte[] DatosComprimidos()
		{
			return Lz77.Comprimir(datosDescomprimidos.Bytes, 0);
		}
		public static void SetBloqueImagen(RomGba rom, int offsetHeader, BloqueImagen bloqueImg, bool borrarDatosAnterioresImg = true, bool setPaletasSacadasDeLaRom = false)
		{


			if (borrarDatosAnterioresImg && bloqueImg.Offset > 0)
				try
			{
				rom.Data.Remove(new OffsetRom(rom, offsetHeader).Offset, Lz77.Longitud(rom.Data.Bytes, bloqueImg.Offset));
			}
			catch { }
			bloqueImg.offset = offsetHeader;
			if (setPaletasSacadasDeLaRom)
				for (int i = 0; i < bloqueImg.Paletas.Count; i++)
					if (bloqueImg.Paletas[i].Offset > 0)
			{
				Paleta.SetPaleta(rom, bloqueImg.Paletas[i]);
			}

			rom.Data.SetArray(offsetHeader + OffsetRom.LENGTH, bloqueImg.Header);
			SetBloqueImagenSinHeader(rom, offsetHeader, bloqueImg);
		}

		public static void SetBloqueImagenSinHeader(RomGba rom, int offsetImagen, BloqueImagen bloqueImg)
		{
			rom.Data.SetArray(offsetImagen, new OffsetRom(rom, rom.Data.SetArray(bloqueImg.DatosComprimidos())).BytesPointer);
		}

		public static BloqueImagen GetBloqueImagenSinHeader(RomGba rom, int offsetPointerData)
		{
			BloqueImagen bloqueCargado = new BloqueImagen();
			bloqueCargado.Offset = new OffsetRom(rom, offsetPointerData).Offset;
			bloqueCargado.DatosDescomprimidos.Bytes = Lz77.Descomprimir(rom.Data.Bytes, bloqueCargado.Offset);
			return bloqueCargado;
		}
		public static BloqueImagen GetBloqueImagen(RomGba rom, int offsetHeader)
		{
			byte[] header = rom.Data.SubArray(offsetHeader + OffsetRom.LENGTH, LENGTHHEADER);
			BloqueImagen bloqueCargado = GetBloqueImagenSinHeader(rom, offsetHeader);
			bloqueCargado.Header = header;
			return bloqueCargado;
		}


		#region Interpretando datos
		public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida,Paleta paleta,bool showBackground=false)
		{
			int longitudLado = Convert.ToInt32(Math.Sqrt(datosImagenDescomprimida.Length / 32)) * 8;//sacado de NSE creditos a Link12552
			return BuildBitmap(datosImagenDescomprimida,paleta,longitudLado,longitudLado,showBackground);
		}
		public static Bitmap BuildBitmap(byte[] datosImagenDescomprimida, Paleta paleta,int width,int height, bool showBackground = false)
		{
			if (datosImagenDescomprimida == null || paleta == null)
				throw new ArgumentNullException();
			const int BYTESPERPIXEL = 4;
			const int NUM = 8;//poner algo mas descriptivo

			
			int bytesPorLado = BYTESPERPIXEL * width;
			Bitmap bmpTiles = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			System.Drawing.Color[] colores = paleta.Colores;
			System.Drawing.Color color;
			byte temp;
			int posByteImgArray = 0, pos;

			if (!showBackground)
				colores[0] = Color.Transparent;
			unsafe
			{

				bmpTiles.TrataBytes((MetodoTratarBytePointer)((bytesBmp) =>
				                                              {

				                                              	for (int y1 = 0; y1 < height; y1 += NUM)
				                                              		for (int x1 = 0; x1 < width; x1 += NUM)
				                                              			for (int y2 = 0; y2 < NUM; y2++)
				                                              				for (int x2 = 0; x2 < NUM; x2 += 2, posByteImgArray++)
				                                              	{
				                                              		temp = datosImagenDescomprimida[posByteImgArray];
				                                              		//pongo los pixels de dos en dos porque se leen diferente de la paleta
				                                              		//pixel izquierdo

				                                              		pos = (x1 + x2) * BYTESPERPIXEL + (y1 + y2) * bytesPorLado;
				                                              		color = colores[temp & 0xF];

				                                              		bytesBmp[pos] = color.B;
				                                              		bytesBmp[pos + 1] = color.G;
				                                              		bytesBmp[pos + 2] = color.R;
				                                              		bytesBmp[pos + 3] = color.A;

				                                              		//pixel derecho
				                                              		pos += BYTESPERPIXEL;

				                                              		color = colores[(temp & 0xF0) >> 4];
				                                              		bytesBmp[pos] = color.B;
				                                              		bytesBmp[pos + 1] = color.G;
				                                              		bytesBmp[pos + 2] = color.R;
				                                              		bytesBmp[pos + 3] = color.A;



				                                              	}

				                                              }));

			}


			return bmpTiles;
		}
		#region Desarrollando la conversion de Bitmap a GBAData descomprimida
		internal static BloqueBytes GetDatosDescomprimidos(Bitmap img)
		{
			//por testear
			if (img == null)
				throw new ArgumentNullException("img");
			Paleta paleta = Paleta.Default;
			byte[] toReturn = new byte[(img.Height * img.Width) >> 1];
			int index = 0;
			System.Drawing.Color temp;
			byte outValue = 0, index2;
			bool buscandoPaleta;
			for (int i = 0; i < img.Height; i++)
			{
				for (int j = 0; j < img.Width / 2; j++)
				{

					outValue = 0;
					index2 = 0;
					for (int k = 0; k < 2; k++)
					{
						temp = img.GetPixel((j * 2) + k, i);

						buscandoPaleta = true;
						for (int l = 0; l < paleta.Colores.Length && buscandoPaleta; l++)
							if (temp.ToArgb().Equals(paleta.Colores[l].ToArgb()))
						{
							outValue = (byte)(index2 << (k * 4));
							buscandoPaleta = false;
						}
						index2++;
					}
					toReturn[index] = (byte)(toReturn[index] | outValue);
				}
				index++;
			}
			return new BloqueBytes(toReturn);
		}
		internal static byte[] GetDataFromBitmap(Bitmap bitmap)
		{
			byte[] sprite;
			byte aux;
			byte[] rgbValues=bitmap.GetBytes();
			sprite = new byte[bitmap.Width * bitmap.Height / 2];
			

			# endregion



			//Wierd format with PNG's from Unlz
			// -for some strange reason unlz exports 16 color sprites in 256 color format
			// -this doubles the size of the internal data and makes it hard to decode properly
			if (rgbValues.Length > bitmap.Width * bitmap.Height / 2)
			{
				for (int y = 0; y < bitmap.Height; y++)
				{
					for (int x = 0; x + 1 < bitmap.Width; x++)
					{
						aux = (byte)(rgbValues[y * (bitmap.Width) + x] + (rgbValues[y * (bitmap.Width) + x + 1] << 4));
						if (aux != 0)
						{
							SetByte(sprite, bitmap.Width, bitmap.Height, x, y, aux);
						}
					}

				}
			}
			else
			{
				for (int y = 0; y < bitmap.Height; y++)
				{
					for (int x = 0; x < bitmap.Width / 2; x++)
					{
						aux = FlipByte(rgbValues[y * (bitmap.Width / 2) + x]);
						if (aux != 0)
						{
							SetByte(sprite, bitmap.Width, bitmap.Height, x * 2, y, aux);
						}
					}

				}
			}

			return sprite;
		}
		
		
		static int Position2Index(Size Size, Point Position)
		{
			int r = (Position.Y - (Position.Y % 8)) / 2 * Size.Width;
			r += (Position.X - (Position.X % 8)) * 4;
			r += (Position.Y % 8) * 4;
			r += (Position.X % 8) / 2;

			return r;
		}

		static void SetByte( byte[] Data, int Width, int Height, int x, int y, byte val)
		{
			int pos = Position2Index(new Size(Width, Height), new Point(x, y));

			Data[pos] = val;
		}

		static byte FlipByte(byte Byte)
		{
			return (byte)((Byte >> 4) + ((Byte << 4) & 0xF0));
		}
		#endregion
		/// <summary>
		/// Convert Bitmap To 4BPP Byte Array
		/// </summary>
		/// <param name="img"></param>
		/// <returns></returns>
		static BloqueBytes GetDatosComprimidos(Bitmap img)
		{

			return new BloqueBytes(Lz77.Comprimir(GetDatosDescomprimidos(img).Bytes));
		}
		
		#region Conversiones
		public static implicit operator Bitmap(BloqueImagen bloqueImg)
		{
			return bloqueImg[0];
		}
		public static Bitmap operator +(BloqueImagen bloqueImagen, Paleta paleta)
		{
			return bloqueImagen[paleta];
		}
		#endregion

		public static bool IsHeaderOk(RomGba gbaRom, int offsetToCheck)
		{
			//PointerHeaderID
			return new OffsetRom(gbaRom, offsetToCheck).IsAPointer && (gbaRom.Data.Bytes.ArrayEqual(DefaultHeader, offsetToCheck + OffsetRom.LENGTH) || gbaRom.Data.Bytes.ArrayEqual(DefaultHeader2, offsetToCheck + OffsetRom.LENGTH));
		}

		public static void Remove(RomGba rom, int offsetSpriteActual)
		{
			int offsetDatos = new OffsetRom(rom, offsetSpriteActual).Offset;
			//borro los datos
			rom.Data.Remove(offsetDatos, Lz77.Longitud(rom.Data.Bytes, offsetDatos));
			//borro el header
			rom.Data.Remove(offsetSpriteActual, LENGTHHEADERCOMPLETO);
		}
	}
}
