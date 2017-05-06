/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 9:38
 * 
 * Código bajo licencia GNU
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
	/// Description of Paleta.
	/// </summary>
	public class Paleta
	{
		public static System.Drawing.Color BackgroundColorDefault=System.Drawing.Color.Transparent;
		public const int LENGTHHEADER=4;
		public const int LENGTHHEADERCOMPLETO=OffsetRom.LENGTH+LENGTHHEADER;
		public const int LENGTH=16;
		public static readonly Paleta Default=new Paleta();
		static readonly byte[] DefaultHeader={0x0,0x0};
		int offset;
		short id;
		short formato;
		System.Drawing.Color[] colores;
		
		public Paleta()
		{
			colores=new System.Drawing.Color[LENGTH];
			offset=-1;
		}
		public Paleta(params System.Drawing.Color[] coloresPaleta):this()
		{
			if(coloresPaleta==null)
				throw new ArgumentNullException();
			for(int i=0;i<LENGTH&&i<coloresPaleta.Length;i++)
				this.colores[i]=coloresPaleta[i];
			
		}

		public int Offset {
			get {
				return offset;
			}
			set { offset = value; }
		}
		public short Id {
			get {
				return id;
			}
			set { id=value; }
		}

		public short Formato {
			get {
				return formato;
			}
			set
			{
				formato = value;
			}
		}
		private byte[] Header{
			get{ return Serializar.GetBytes(formato).AddArray(Serializar.GetBytes(id));}
			set{
				if(value==null)
					throw new ArgumentNullException();
				if(value.Length<LENGTHHEADER)
					value=value.AddArray(new byte[LENGTHHEADER-value.Length]);
				
				id=Serializar.ToShort(value.SubArray(2));
				formato=Serializar.ToShort(value.SubArray(2,2));
			}
		}
		public byte[] HeaderCompleto{
			get{return GetHeaderCompleto(Offset);}
			
		}
		public System.Drawing.Color[] Colores{
			get {
				return colores;
			}
		}
		public System.Drawing.Color this[int index]{
			get{return colores[index];}
			set{colores[index]=value;}
		}
		
		public byte[] GetHeaderCompleto(int pointerData)
		{
			return new OffsetRom(pointerData).BytesPointer.AddArray(Header);
		}
		public static Paleta GetPaletaSinHeader(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
		{
			//sacado de Nameless
			if (rom == null || offsetPointerPaleta < 0)
				throw new ArgumentException();
			byte[] bytesPaletaDescomprimidos = Lz77.Descomprimir(rom.Data.Bytes,new OffsetRom(rom, offsetPointerPaleta).Offset);
			Paleta paleta=GetPaleta(bytesPaletaDescomprimidos);		
			if (!showBackgroundColor)
			{
				paleta[0] = BackgroundColorDefault;
			}
			paleta.offset=offsetPointerPaleta;
			return paleta;
		}
		public static Paleta GetPaleta(byte[] datosPaletaDescomprimida){
		
		Paleta paleta=new Paleta();

			ushort tempValue;
			byte  r, g, b;
			System.Drawing.Color colorPaleta;

			for (int i = 0; i < LENGTH; i++)
			{
				tempValue = Serializar.ToUShort(datosPaletaDescomprimida.SubArray(i*2,2));
				r = (byte)((tempValue & 0x1f) << 3);
				g = (byte)(((tempValue >> 5) & 0x1f) << 3);
				b = (byte)(((tempValue >> 10) & 0x1f) << 3);
				colorPaleta = System.Drawing.Color.FromArgb(0xFF, r, g, b);
				paleta[i] = colorPaleta;

			}
			return paleta;
		}
		public static Paleta GetPaleta(RomGba rom, int offsetPointerPaleta, bool showBackgroundColor = true)
		{
			byte[] header=rom.Data.SubArray(offsetPointerPaleta+OffsetRom.LENGTH,LENGTHHEADER);
			Paleta paletaCargada=GetPaletaSinHeader(rom,offsetPointerPaleta,showBackgroundColor);
			paletaCargada.Header=header;
			return paletaCargada;
		}
		public static Paleta GetPaleta(Bitmap img)
		{
			
			SortedList<int,int> colores=new SortedList<int,int>();
			Paleta paleta=new Paleta();
			int argbColorActual;
			unsafe{
				Gabriel.Cat.V2.Color* ptrColoresImg;
				fixed(byte* ptDatosImg=img.GetBytes())
				{
					ptrColoresImg=(Gabriel.Cat.V2.Color*)ptDatosImg;
					for(int i=0,f=img.Height*img.Width,j=1;i<f&&colores.Count<LENGTH;i++)
					{
						argbColorActual=(*ptrColoresImg).ToArgb();
						if(!colores.ContainsKey(argbColorActual)&&(*ptrColoresImg).Alfa==byte.MaxValue)
						{
							colores.Add(argbColorActual,argbColorActual);
							paleta[j++]=*ptrColoresImg; 
						}
					}
				}
			}
			
			
			return paleta;
		}

		
		public static void SetPaleta(RomGba rom, Paleta paleta)
		{
			SetPaleta(rom,paleta.Offset,paleta);
		}
		public static void SetPaleta(RomGba rom,int offsetHeaderPaleta, Paleta paleta,bool borrarDatosPaletaAntigua=true)
		{
			SetPaletaSinHeader(rom,offsetHeaderPaleta,paleta,borrarDatosPaletaAntigua);
			rom.Data.SetArray(offsetHeaderPaleta+OffsetRom.LENGTH,paleta.Header);
			paleta.offset=offsetHeaderPaleta;
			


		}

		public static void SetPaletaSinHeader(RomGba rom, int offset, Paleta paleta,bool borrarDatosPaletaAntigua=true)
		{
			//sacado de Nameless
			const int BYTESCOLORGBA = 2;
			int offsetData;
			System.Drawing.Color[] coloresPaleta = paleta.Colores;
			byte[] bytesPaleta = new byte[LENGTH * BYTESCOLORGBA];
			byte[] bytesAux = new byte[BYTESCOLORGBA];
			

			for (int i = 0; i < LENGTH; i++)
			{
				bytesAux[0] = (byte)((byte)(coloresPaleta[i].R / 8) + ((byte)((coloresPaleta[i].G / 8) & 0x7) << 5));
				bytesAux[1] = (byte)((((byte)(coloresPaleta[i].B / 8)) << 2) + ((byte)(coloresPaleta[i].G / 8) >> 3));
				bytesPaleta.SetArray(i * 2, bytesAux);
			}
			
			//la comprimo
			bytesPaleta = Lz77.Comprimir(bytesPaleta);

			if (borrarDatosPaletaAntigua)
				try{
				rom.Data.Remove(new OffsetRom(rom,offset).Offset, bytesPaleta.Length);
			}catch{}

			offsetData =rom.Data.SetArray(bytesPaleta);
			rom.Data.SetArray(offset,new OffsetRom(offsetData).BytesPointer);
		}

		public static Color ToGBAColor(Color color)
		{
			byte parteA,parteB;
			ushort colorGBA;
			parteA=(byte)((byte)(color.R / 8) + ((byte)((color.G / 8) & 0x7) << 5));
			parteB=(byte)((((byte)(color.B / 8)) << 2) + ((byte)(color.G / 8) >> 3));
			colorGBA=Serializar.ToUShort(new byte[]{parteA,parteB});
			return System.Drawing.Color.FromArgb(0xFF,(byte)((colorGBA & 0x1f) << 3), (byte)(((colorGBA >> 5) & 0x1f) << 3), (byte)(((colorGBA >> 10) & 0x1f) << 3));
		}
		public static Bitmap ToGBAColor(Bitmap bmp)
		{
			int total=bmp.Height*bmp.Width;
			bmp=bmp.Clone() as Bitmap;
			unsafe{
				bmp.TrataBytes((MetodoTratarBytePointer)((ptrBytesBmp)=>{
				                                         	
				                                         	int* ptrColoresBmp=(int*)ptrBytesBmp;
				                                         	
				                                         	for(int i=0;i<total;i++)
				                                         	{
				                                         		*ptrColoresBmp=ToGBAColor(Color.FromArgb((int)*ptrColoresBmp)).ToArgb();
				                                         		ptrColoresBmp++;
				                                         	}
				                                         	
				                                         }));
			}
			return bmp;
		}


		public static bool IsHeaderOk(RomGba gbaRom,int offsetToCheck)
		{
			return new OffsetRom(gbaRom,offsetToCheck).IsAPointer&&gbaRom.Data.Bytes.ArrayEqual(DefaultHeader,offsetToCheck+OffsetRom.LENGTH+2);
		}

		public static void Remove(RomGba rom, int offsetPaletaActual)
		{
			int offsetDatos;
			try{
			offsetDatos=new OffsetRom(rom,offsetPaletaActual).Offset;
			//borro los datos
			rom.Data.Remove(offsetDatos,Lz77.Longitud(rom.Data.Bytes,offsetDatos));
			//borro el header
			rom.Data.Remove(offsetPaletaActual,LENGTHHEADERCOMPLETO);
			}catch{}
		}
	}
	
}
