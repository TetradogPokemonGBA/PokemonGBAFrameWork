using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;

//por revisar
namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of Huella.
	/// </summary>
	public class Huella
	{
		public const int LENGHT = 32;
		static readonly Color[] PaletaHuella = { Color.Transparent, Color.Black };

		public static readonly byte[] MuestraAlgoritmoKanto = { 0x00, 0x94, 0x14, 0x25, 0x01, 0x95, 0x02 };
		public static readonly int InicioRelativoKanto =-MuestraAlgoritmoKanto.Length -32;
		public static readonly byte[] MuestraAlgoritmoHoenn = { 0x10, 0xB5, 0x00, 0x04, 0x09, 0x04, 0x09 };
		public static readonly int InicioRelativoHoenn =-MuestraAlgoritmoHoenn.Length -Zona.LENGTH;
		public Huella()
		{
			BytesHuellaGBA = new BloqueBytes(LENGHT);
		}

		private Huella(BloqueBytes bytesHuellaGBA)
		{
			BytesHuellaGBA = bytesHuellaGBA;
		}
        public BloqueBytes BytesHuellaGBA { get; set; }


        public Bitmap GetImagen()
		{
			return ReadImage(BytesHuellaGBA.Bytes);

		}
		public void SetImagen(Bitmap imgHuella)
		{
			if (imgHuella == null)
				imgHuella = new Bitmap(16, 16);
			else if (imgHuella.Height != 16 || imgHuella.Width != 16)
				throw new ArgumentException("La imagen tiene que ser 16x16");

			BytesHuellaGBA.Bytes = WriteImage(imgHuella);
		}


		public static Huella Get(RomGba rom, int posicion, OffsetRom inicioOffsetHuella = default)
		{
			//le el offset del pointer que toca
			int offsetBytesHuella = GetOffset(rom, posicion,inicioOffsetHuella);
			//lee los bytes de la imagen del offset leido
			return new Huella(BloqueBytes.GetBytes(rom.Data, offsetBytesHuella, LENGHT));
		}
		/// <summary>
		/// Obtiene el offset de la lista de pointers.
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="posicion"></param>
		/// <returns></returns>
		public static int GetOffset(RomGba rom, int posicion,OffsetRom inicioOffsetHuella=default)
		{
			const int NOENCONTRADO = -1;
			if (Equals(inicioOffsetHuella, default))
				inicioOffsetHuella = GetOffset(rom);
			OffsetRom offset = new OffsetRom(rom, inicioOffsetHuella + posicion * OffsetRom.LENGTH);
			return offset.IsAPointer? offset.Offset:NOENCONTRADO;
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static Zona GetZona(RomGba rom)
		{
			return Zona.Search(rom, rom.Edicion.EsKanto ? MuestraAlgoritmoKanto : MuestraAlgoritmoHoenn, rom.Edicion.EsKanto ? InicioRelativoKanto : InicioRelativoHoenn);
		}

		public static bool PoscionIsOK(RomGba rom, int posicion, OffsetRom inicioOffsetHuella = default)
		{
			//asi puedo calcular el numero de pokemons que hay sin hacer faena en vano.
			const int MARCAFIN = -1;
			return GetOffset(rom, posicion,inicioOffsetHuella) != MARCAFIN;
		}
		static Bitmap ReadImage(byte[] bytesHuellaGBA)
		{
			Bitmap bmpHuella = new Bitmap(16, 16);
			byte[] bytesHuellaDescomprimida = DescomprimirBytesImgRGBA(ConvertToImgBytes(bytesHuellaGBA));
			bmpHuella.SetBytes(bytesHuellaDescomprimida);
			//para comprobar que todo va bien :D
			/*   byte[] bytesHuellaHecha =ConvertToGBA(ComprimirBytesImg(bytesHuellaDescomprimida));
            for (int i = 0; i < bytesHuellaHecha.Length; i++)
                if (bytesHuella[i] != bytesHuellaHecha[i])
                    System.Diagnostics.Debugger.Break();*/
			return bmpHuella;
		}
		static byte[] ConvertToImgBytes(byte[] bytesGBA)
		{
			const int XMEDIO = 8;
			const int XFIN = 16;
			byte[] bytesImgComprimidos = new byte[bytesGBA.Length];

			//los pone por orden natural
			unsafe
			{
				byte* ptrBytesGBAIzquierda, ptrBytesGBADerecha, ptrBytesImg;
				fixed (byte* ptBytesGBA = bytesGBA, ptBytesImg = bytesImgComprimidos)
				{
					ptrBytesGBAIzquierda = ptBytesGBA;
					ptrBytesGBADerecha = ptBytesGBA;
					ptrBytesImg = ptBytesImg;
					//los pone por orden natural
					for (int i = 0; i < 2; i++)//primero arriba y luego abajo :D
					{
						ptrBytesGBADerecha += XMEDIO;
						for (int x = 0; x < XFIN; x += 2)
						{
							*ptrBytesImg = *ptrBytesGBAIzquierda;
							ptrBytesImg++;
							ptrBytesGBAIzquierda++;
							*ptrBytesImg = *ptrBytesGBADerecha;
							ptrBytesImg++;
							ptrBytesGBADerecha++;
						}
						ptrBytesGBAIzquierda += XMEDIO;//asi empieza la segunda mitad ya que estaba por un cuarto
					}

				}
			}
			return bytesImgComprimidos;
		}

		private static byte[] DescomprimirBytesImgRGBA(byte[] bytesImgComprimidos)
		{
			const int RGBA = 4, RGB = RGBA - 1;
			const byte ON = 0xFF;//color on :)
			byte[] bytesImgDescomprimidosRGBA = new byte[bytesImgComprimidos.Length * RGBA * 8];//RGBA* 8bits
			bool[] bitsColor;
			unsafe
			{
				byte* ptrBytesImgComprimidos, ptrBytesImgDescompridos;
				bool* ptrBitsColor;
				fixed (byte* ptBytesImgComprimidos = bytesImgComprimidos, ptBytesImgDescomprimidos = bytesImgDescomprimidosRGBA)
				{
					ptrBytesImgComprimidos = ptBytesImgComprimidos;
					ptrBytesImgDescompridos = ptBytesImgDescomprimidos;
					for (int i = 0; i < bytesImgComprimidos.Length; i++)
					{
						bitsColor = (*ptrBytesImgComprimidos).ToBits();
						fixed (bool* ptBitsColor = bitsColor)
						{
							ptrBitsColor = ptBitsColor;
							for (int j = 0; j < bitsColor.Length; j++)
							{
								ptrBytesImgDescompridos += RGB;
								if (*ptrBitsColor)
								{
									*ptrBytesImgDescompridos = ON;//para no ser transparente y quedar negro :D
								}
								ptrBytesImgDescompridos++;
								ptrBitsColor++;
							}
						}
						ptrBytesImgComprimidos++;
					}
				}
			}
			return bytesImgDescomprimidosRGBA;
		}

		static byte[] WriteImage(Bitmap bmp)
		{
			return ConvertToGBA(ComprimirBytesImg(bmp.GetBytes()));
		}
		static byte[] ComprimirBytesImg(byte[] bytesImgDescomprimidosRGBA)
		{//al parecer no comprime igual...y no son los mismos bytes que saco de la rom...
		 //puede ser que los ponga al rebes?? porque la imagen se veia cortada por la mitad y girada...eso quiere decir que se corta verticalmente....
			const byte BITSBYTE = 8;
			const byte WHITE = 0xFF, TRANSPARENT = 0x0;//gamefreak solo guarda la transparencia lo demas es 0x00 que sin transparencia se ve negro...
			bool[] bitsColor;
			byte[] bytesImgComprimidos = new byte[LENGHT];


			unsafe
			{
				byte* ptrBytesImgComprimidos, ptrBytesImgDescompridos;
				bool* ptrBitsColor;
				fixed (byte* ptBytesImgComprimidos = bytesImgComprimidos, ptBytesImgDescomprimidos = bytesImgDescomprimidosRGBA)
				{
					ptrBytesImgComprimidos = ptBytesImgComprimidos;
					ptrBytesImgDescompridos = ptBytesImgDescomprimidos;
					for (int i = 0; i < bytesImgComprimidos.Length; i++)
					{
						//formo el color
						bitsColor = new bool[BITSBYTE];
						fixed (bool* ptBitsColor = bitsColor)
						{
							ptrBitsColor = ptBitsColor;
							for (byte j = 0; j < BITSBYTE; j++)
							{
								//R is != OFF then is ON
								if (*ptrBytesImgDescompridos != WHITE)
								{
									ptrBytesImgDescompridos += 3;//avanzo a A
									*ptrBitsColor = *ptrBytesImgDescompridos != TRANSPARENT;


								}
								else
								{
									ptrBytesImgDescompridos++;
									//G is != OFF then is ON
									if (*ptrBytesImgDescompridos != WHITE)
									{
										ptrBytesImgDescompridos += 2;//avanzo a A
										*ptrBitsColor = *ptrBytesImgDescompridos != TRANSPARENT;

									}
									else
									{
										ptrBytesImgDescompridos++;
										//B is != OFF then is ON
										if (*ptrBytesImgDescompridos != WHITE)
										{
											ptrBytesImgDescompridos++;//avanzo a A
											*ptrBitsColor = *ptrBytesImgDescompridos != TRANSPARENT;

										}
										else
										{
											//es blanco o transparente asi que lo salto
											ptrBytesImgDescompridos++;//avanzo a A

										}

									}

								}
								ptrBytesImgDescompridos++;//siguiente color
								ptrBitsColor++;
							}
						}
						//lo asigno
						*ptrBytesImgComprimidos = bitsColor.ToByte();
						ptrBytesImgComprimidos++;
					}

				}
			}
			return bytesImgComprimidos;
		}
		static byte[] ConvertToGBA(byte[] bytesImgComprimidos)
		{
			const int XMEDIO = 8;
			const int XFIN = 16;
			byte[] bytesGBA = new byte[bytesImgComprimidos.Length];
			//los pone por orden en el cuadrado que le toca :D
			unsafe
			{
				byte* ptrBytesGBAIzquierda, ptrBytesGBADerecha, ptrBytesImg;
				fixed (byte* ptBytesGBA = bytesGBA, ptBytesImg = bytesImgComprimidos)
				{
					ptrBytesGBAIzquierda = ptBytesGBA;
					ptrBytesGBADerecha = ptBytesGBA;
					ptrBytesImg = ptBytesImg;
					//los pone por orden natural
					for (int i = 0; i < 2; i++)//primero arriba y luego abajo :D
					{
						ptrBytesGBADerecha += XMEDIO;
						for (int x = 0; x < XFIN; x += 2)
						{

							*ptrBytesGBAIzquierda = *ptrBytesImg;
							ptrBytesImg++;
							ptrBytesGBAIzquierda++;
							*ptrBytesGBADerecha = *ptrBytesImg;
							ptrBytesImg++;
							ptrBytesGBADerecha++;

						}
						ptrBytesGBAIzquierda += XMEDIO;
					}

				}
			}
			return bytesGBA;
		}

		/// <summary>
		/// Sirve para saber como queda guardada la imagen :) en la rom
		/// </summary>
		/// <param name="bmp"></param>
		/// <returns></returns>
		public static Bitmap ConvertToGbaBmp(Bitmap bmp)
		{
			return ReadImage(WriteImage(bmp));
		}
		public static int GetTotal(RomGba rom, OffsetRom offsetHuella=default)
		{
			int offset;
			int total = 0;

			if(Equals(offsetHuella,default))
				 offsetHuella = GetOffset(rom);
			offset = offsetHuella;
			while (new OffsetRom(rom, offset).IsAPointer)
			{
				offset += OffsetRom.LENGTH;
				total++;
			}
			return total - 1;
		}

		public static Huella[] Get(RomGba rom,OffsetRom offsetHuellas=default) => GetAll<Huella>(rom, Huella.Get,offsetHuellas);
		public static Huella[] GetOrdenLocal(RomGba rom, OffsetRom offsetHuellas = default) => OrdenLocal.GetOrdenados<Huella>(rom,(r,o)=>Huella.Get(r),Equals(offsetHuellas,default)? GetOffset(rom):offsetHuellas);
		public static Huella[] GetOrdenNacional(RomGba rom, OffsetRom offsetHuellas = default) => OrdenNacional.GetOrdenados<Huella>(rom, (r, o) => Huella.Get(r), Equals(offsetHuellas, default) ? GetOffset(rom) : offsetHuellas);
		public static T[] GetAll<T>(RomGba rom,GetMethod<T> metodo,OffsetRom inicioMetodo=default)
		{
			T[] total = new T[GetTotal(rom)];
			try
			{
				for (int i = 0; i < total.Length; i++)
					total[i] = metodo(rom, i, inicioMetodo);
			}
			catch(Exception ex)
			{
				if(System.Diagnostics.Debugger.IsAttached)
					System.Diagnostics.Debugger.Break();

				throw ex;
			}
			return total;
		}
	}
}