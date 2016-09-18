/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 12/08/2016
 * Time: 19:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Gabriel.Cat;
using Gabriel.Cat.Extension;
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of TratarBytes.
	/// </summary>
	public class BloqueBytes
	{
		//el minimo y el maximo se tienen que estudiar :)
		public static readonly int TamañoMinimoRom = 1;
		public static readonly int TamañoMaximoRom = 67108864;

		Hex offset;
		byte[] bytes;
		public BloqueBytes(Hex offset, byte[] bytesAPoner)
		{
			Bytes = bytesAPoner;
			OffsetInicio = offset;
		}

		public Hex OffsetInicio {
			get {
				
				return offset;
			}
			set {
				if (value < 0)
					throw new ArgumentOutOfRangeException();
				offset = value;
				
			}
		}
		public Hex OffsetFin {
			get{ return OffsetInicio + bytes.Length; }
		}
		public byte[] Bytes {
			get {
				return bytes;
			}
			set {
				bytes = value;
				if (bytes == null)
					bytes = new byte[0];
			}
		}
		public void SaveFile(string pathFileWithNameAndExtension)
		{
			if(String.IsNullOrEmpty(pathFileWithNameAndExtension))
				throw new ArgumentException();
			FileStream str=null;
			StreamWriter strW=null;
			try{
				str=new FileStream(pathFileWithNameAndExtension,FileMode.Create);
				
				strW=new StreamWriter(str,System.Text.Encoding.UTF8);
				strW.Write((int)OffsetInicio);
				strW.Write(Bytes);
			}catch{throw;}finally{
				if(strW!=null)
					strW.Close();
				else try{ File.Delete(pathFileWithNameAndExtension);/*si falla lo borr*/}catch{}//por si peta :)
				if(str!=null)
					str.Close();
			}
		}
        /// <summary>
        /// Guarda donde quepan los datos y devuelve el offset donde se han puesto
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Hex SetBytes(RomGBA rom, byte[] bytes,bool ends048C=false)
        {
            Hex posicion = SearchEmptyBytes(rom, bytes.Length,ends048C);
            if (posicion == -1) throw new ArgumentOutOfRangeException("No se ha encontrado lugar para los datos");
            SetBytes(rom, posicion, bytes);
            return posicion;
        }

     
        public static void SetBytes(RomGBA rom, Hex offsetInicio, byte[] bytes)
		{
			SetBytes(rom, new BloqueBytes(offsetInicio, bytes));
		}
		public static void SetBytes(RomGBA rom, BloqueBytes bytes)
		{
			if (bytes.OffsetFin >= rom.Datos.Length)
				throw new ArgumentOutOfRangeException();
			unsafe {
				fixed(byte* ptrBytesDatos = rom.Datos)
                {
                    fixed (byte* ptrBytesSet = bytes.Bytes)
                    {
                        byte* ptBytesDatos = ptrBytesDatos;
                        byte* ptBytesSet = ptrBytesDatos;
                        ptBytesDatos += bytes.OffsetInicio;
                        for (int i = bytes.OffsetInicio, f = bytes.OffsetFin, pos = 0; i < f; i++, pos++)
                        {
                            *ptBytesDatos = *ptBytesSet;
                            ptBytesSet++;
                            ptBytesDatos++;
                        }
                    }
                }
				
			}
		}
		public static BloqueBytes GetBytes(RomGBA rom, Hex offsetInicio, Hex longitud)
		{
			
			if (offsetInicio < 0 || longitud < 0 || rom.Datos.Length < offsetInicio + longitud)
				throw new ArgumentOutOfRangeException();
			byte[] bytes = new byte[longitud];
			unsafe {
				fixed(byte* ptrBytesRom = rom.Datos)
                {
                    fixed (byte* ptrOut = bytes)
                    {
                        byte* ptBytesRom = ptrBytesRom;
                        byte* ptOut = ptrOut;
                        ptBytesRom += offsetInicio;
                        for (int i = offsetInicio, f = i + longitud, pos = 0; i < f; i++, pos++)
                        {
                            *ptOut = *ptBytesRom;
                            ptBytesRom++;
                            ptOut++;
                        }
                    }
                }
			}
			return new BloqueBytes(offsetInicio, bytes);
			
		}
		public static void RemoveBytes(RomGBA rom, Hex offsetInicio, Hex longitud, byte byteEnBlanco = 0x00)
		{
            if (rom.Datos.Length < offsetInicio + longitud)
                throw new ArgumentOutOfRangeException();
			unsafe {
				fixed(byte* ptrbytesRom = rom.Datos)
                {
                    byte* ptbytesRom = ptrbytesRom;
                    ptbytesRom += offsetInicio;
                    for (int i = 0, f = longitud, pos = offsetInicio; i < f; i++, pos++)
                    {
                        *ptbytesRom = byteEnBlanco;
                        ptbytesRom++;
                    }
                }
				
			}
		}
		/// <summary>
		/// Cambia el tamaño de la rom, hay que tener en cuenta que se pueden perder datos i/o dejar inutilizable la rom al reducirla.
		/// </summary>
		/// <param name="rom">no puede ser null</param>
		/// <param name="tamañoATener">el tamaño tiene que estar entre el minimo y el maximo</param>
		/// <param name="byteEnBlanco">byte a poner en lugar de los que hay</param>
		public static void RomSizeChange(RomGBA rom,int tamañoATener,byte byteEnBlanco=0x00){
			if(rom==null||tamañoATener<TamañoMinimoRom||tamañoATener>TamañoMaximoRom)throw new ArgumentException();
			byte[] romExpandida=new byte[tamañoATener];
			
			unsafe{
				fixed(byte* ptrBytesRomExpandida=romExpandida){
					fixed(byte* ptrBytesRom=rom.Datos){
                        byte* ptBytesRomExpandida = ptrBytesRomExpandida;
                        byte* ptBytesRom = ptrBytesRom;
                        for (int i = 0; i < rom.Datos.Length && i < romExpandida.Length; i++)
                        {
                            *ptBytesRomExpandida = *ptBytesRom;
                            ptBytesRom++;
                            ptBytesRomExpandida++;
                        }
						if(byteEnBlanco!=byte.MinValue)//asi evito el ponerle el valor por defecto que ya tiene :)
							for(int i=rom.Datos.Length;i<romExpandida.Length;i++)
                            {
                                *ptBytesRomExpandida = byteEnBlanco;
                                ptBytesRomExpandida++;
                            }

                    }
					
				}
				
			}
			rom.Datos=romExpandida;
		}
		public static Hex SearchBytes(RomGBA rom,byte[] bytesAEncontrar){
			return SearchBytes(rom,0,bytesAEncontrar);
		}
		public static Hex SearchBytes(RomGBA rom,Hex offsetInicio, byte[] bytesAEncontrar)
		{
            if (bytesAEncontrar.Length == 1) System.Diagnostics.Debugger.Break();
			if (bytesAEncontrar == null)
				throw new ArgumentNullException("bytesAEncontrar");
			if (offsetInicio + bytesAEncontrar.Length > rom.Datos.Length)
				throw new ArgumentOutOfRangeException();
			const int DIRECCIONNOENCONTRADO = -1;
			int direccionBytes = DIRECCIONNOENCONTRADO;
			int posibleDireccion = DIRECCIONNOENCONTRADO;
			int posicionBytesAEncontrar = 0;
			//busco la primera aparicion de esos bytes y
			unsafe
			{
				fixed(byte* prtBytesDatos=rom.Datos)
					fixed (byte* ptrBytesAEcontrar = bytesAEncontrar)
				{
                    byte* ptBytesDatos = prtBytesDatos;
                    byte* ptBytesAEcontrar = ptrBytesAEcontrar;
                    for (int i = offsetInicio; i < rom.Datos.Length && direccionBytes == DIRECCIONNOENCONTRADO&&i+(bytesAEncontrar.Length-posicionBytesAEncontrar)<rom.Datos.Length/*si los bytes que quedan por ver se pueden llegar a ver continuo sino paro*/; i++)
					{
						if (*ptBytesDatos == *ptBytesAEcontrar)
						{
							if (posibleDireccion == DIRECCIONNOENCONTRADO)//si es la primera vez que entra
								posibleDireccion = i;//le pongo el inicio
							else if (posicionBytesAEncontrar >= bytesAEncontrar.Length)//si es la ultima vez
								direccionBytes = posibleDireccion;//le pongo el resultado para poder salir del bucle
						}
						else { posibleDireccion = DIRECCIONNOENCONTRADO;posicionBytesAEncontrar = 0; }
                        if (i < rom.Datos.Length)
                        {
                            ptBytesAEcontrar++;
                            ptBytesDatos++;
                        }
					}
				}
			}

			return direccionBytes;
		}
		public static BloqueBytes LoadFile(FileInfo file)
		{
			const int bytesInt=4;
			Stream str=null;
			byte[] bytesBloque;
			try{
				str=file.OpenRead();
				
				bytesBloque=new byte[str.Length-bytesInt];
				str.Position=bytesInt;
				str.Read(bytesBloque,0,bytesBloque.Length);}
			catch{
				throw new FormatException("el archivo no es valido");
			}
			return new BloqueBytes((Hex)str.Read(4),bytesBloque);
		}
		public static BloqueBytes LoadFile(string filePath){
			
			return LoadFile(new FileInfo(filePath));
		}
        public static Hex SearchEmptyBytes(RomGBA rom,Hex length)
        { return SearchEmptyBytes(rom, 0, length); }
        public static Hex SearchEmptyBytes(RomGBA rom,Hex inicio, Hex length)
        {
            return SearchBytes(rom,inicio, new byte[length]);//como por defecto es 0x0 ya me va bien aunque tambien se usa el 0xFF...
        }

        public static Hex SearchEmptyBytes(RomGBA rom, int length, bool ends048C)
        {
            return SearchEmptyBytes(rom, 0, length, ends048C);
        }
        public static Hex SearchEmptyBytes(RomGBA rom, Hex offsetInicio, Hex length, bool ends048C)
        {
            string posicionString;
            bool acabado = false;
            Hex posicion = SearchEmptyBytes(rom, offsetInicio, length);
            if (ends048C)
            {
                posicionString = posicion;
                //busco la posicion valida si no hay lanzo excepcion por falta de espacio
                while (posicionString[posicionString.Length - 1] != '0' && posicionString[posicionString.Length - 1] != '4' && posicionString[posicionString.Length - 1] != '8' && posicionString[posicionString.Length - 1] != 'C' && !acabado)
                {
                    posicion = SearchEmptyBytes(rom, posicion + 1, length);
                    posicionString = posicion;
                    acabado = posicion < 0;
                }

            }
            return posicion;
        }
    }
}
