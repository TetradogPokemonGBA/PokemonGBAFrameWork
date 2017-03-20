/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 18:13
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Huella.
	/// </summary>
	public class Huella
	{
		public const int LENGHT=32;
		static readonly Color[] PaletaHuella={Color.Transparent,Color.Black};
		public static readonly Zona ZonaHuella;
		
		BloqueBytes blImgHuellaGBA;
		
		static Huella()
		{
			ZonaHuella=new Zona("Img Huella");
			
			ZonaHuella.Add(EdicionPokemon.RojoFuegoUsa, 0x105E14, 0x105E8C);
			ZonaHuella.Add(EdicionPokemon.VerdeHojaUsa, 0x105DEC, 0x105E64);
			ZonaHuella.Add(EdicionPokemon.EsmeraldaUsa, 0xC0DBC);
			ZonaHuella.Add(EdicionPokemon.RubiUsa, 0x917C8, 0x917E8, 0x917E8);
			ZonaHuella.Add(EdicionPokemon.ZafiroUsa, 0x917C8, 0x917E8, 0x917E8);

			ZonaHuella.Add(EdicionPokemon.VerdeHojaEsp, 0x105F8C);
			ZonaHuella.Add(EdicionPokemon.RojoFuegoEsp, 0x105FB4);
			ZonaHuella.Add(EdicionPokemon.EsmeraldaEsp, 0xC0B80);
			ZonaHuella.Add(EdicionPokemon.RubiEsp, 0x919F8);
			ZonaHuella.Add(EdicionPokemon.ZafiroEsp, 0x919F8);
			
		}
		
		public Huella()
		{
			blImgHuellaGBA=new BloqueBytes(LENGHT);
		}

		private Huella(BloqueBytes bytesHuellaGBA)
		{
			blImgHuellaGBA=bytesHuellaGBA;
		}
		public BloqueBytes BytesHuellaGBA {
			get {
				return blImgHuellaGBA;
			}
		}
		
		public Bitmap GetImagen()
		{
			return ReadImage(blImgHuellaGBA.Bytes);

		}
		public void SetImagen(Bitmap imgHuella)
		{
			if(imgHuella==null)
				imgHuella=new Bitmap(16,16);
			else if(imgHuella.Height!=16||imgHuella.Width!=16)
				throw new ArgumentException("La imagen tiene que ser 16x16");
			
			blImgHuellaGBA.Bytes=WriteImage(imgHuella);			
		}
		public static Huella GetHuella(RomData rom,int posicion)
		{
			return GetHuella(rom.Rom,rom.Edicion,rom.Compilacion,posicion);
		}
		public static Huella GetHuella(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion)
		{
			//le el offset del pointer que toca
			int offsetBytesHuella = GetOffsetHuella(rom, edicion, compilacion, posicion);
			//lee los bytes de la imagen del offset leido
			return new Huella(BloqueBytes.GetBytes(rom.Data, offsetBytesHuella, LENGHT));
		}
		/// <summary>
		/// Obtiene el offset de la lista de pointers.
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="edicion"></param>
		/// <param name="compilacion"></param>
		/// <param name="posicion"></param>
		/// <returns></returns>
		public static int GetOffsetHuella(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion)
		{
			return new OffsetRom(rom, Zona.GetOffsetRom(rom, ZonaHuella, edicion, compilacion).Offset + posicion * OffsetRom.LENGTH).Offset;
		}
		public static bool PoscionIsOK(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion)
		{
			//asi puedo calcular el numero de pokemons que hay sin hacer faena en vano.
			const int MARCAFIN = -1;
			return GetOffsetHuella(rom, edicion, compilacion, posicion) != MARCAFIN;
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
			const int RGBA = 4,RGB=RGBA-1;
			const byte ON=0xFF;//color on :)
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

		public static void SetHuella(RomData rom,Huella huella,int posicion)
		{
			SetHuella(rom.Rom,rom.Edicion,rom.Compilacion,huella,posicion);
		}
		/// <summary>
		/// Escribe los datos de la huella en la rom
		/// </summary>
		/// <param name="rom"></param>
		/// <param name="edicion"></param>
		/// <param name="compilacion"></param>
		/// <param name="huella">se usara el offset para escribir los bytes que contiene en la rom y actualizar el pointer con la direccion que tenga el bloque</param>
		/// <param name="posicion"></param>
		public static void SetHuella(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, Huella huella, int posicion)
		{
			if (huella == null ||  huella.BytesHuellaGBA.Bytes.Length != LENGHT|| rom == null || edicion == null || posicion < 0)
				throw new ArgumentException();

			OffsetRom.SetOffset(rom,new OffsetRom(Zona.GetOffsetRom(rom, ZonaHuella, edicion, compilacion).Offset + posicion * OffsetRom.LENGTH), rom.Data.SetArray(huella.BytesHuellaGBA.Bytes));
		}
		static byte[] WriteImage(Bitmap bmp)
		{
			return ConvertToGBA(ComprimirBytesImg(bmp.GetBytes()));
		}
		private static byte[] ComprimirBytesImg(byte[] bytesImgDescomprimidosRGBA)
		{//al parecer no comprime igual...y no son los mismos bytes que saco de la rom...
			//puede ser que los ponga al rebes?? porque la imagen se veia cortada por la mitad y girada...eso quiere decir que se corta verticalmente....
			const byte BITSBYTE = 8;
			const byte WHITE = 0xFF,TRANSPARENT=0x0;//gamefreak solo guarda la transparencia lo demas es 0x00 que sin transparencia se ve negro...
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
									*ptrBitsColor = *ptrBytesImgDescompridos!=TRANSPARENT;


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
											ptrBytesImgDescompridos ++;//avanzo a A
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
		
	}
}
