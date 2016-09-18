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
            rom.Datos.SetArray(bytes.OffsetInicio, bytes.Bytes);
		}
		public static BloqueBytes GetBytes(RomGBA rom, Hex offsetInicio, Hex longitud)
		{
			
			if (offsetInicio < 0 || longitud < 0 || rom.Datos.Length < offsetInicio + longitud)
				throw new ArgumentOutOfRangeException();
		
			return new BloqueBytes(offsetInicio, rom.Datos.SubArray(offsetInicio,longitud));
			
		}
		public static void RemoveBytes(RomGBA rom, Hex offsetInicio, Hex longitud, byte byteEnBlanco = 0x00)
		{
            if (rom.Datos.Length < offsetInicio + longitud)
                throw new ArgumentOutOfRangeException();
            rom.Datos.Remove(offsetInicio, longitud, byteEnBlanco);
				
			
		}
		/// <summary>
		/// Cambia el tamaño de la rom, hay que tener en cuenta que se pueden perder datos i/o dejar inutilizable la rom al reducirla.
		/// </summary>
		/// <param name="rom">no puede ser null</param>
		/// <param name="tamañoATener">el tamaño tiene que estar entre el minimo y el maximo</param>
		/// <param name="byteEnBlanco">byte a poner en lugar de los que hay</param>
		public static void RomSizeChange(RomGBA rom,int tamañoATener,byte byteEnBlanco=0x00){
			if(rom==null||tamañoATener<TamañoMinimoRom||tamañoATener>TamañoMaximoRom)throw new ArgumentException();
            byte[] romExpandida;
            if (rom.Datos.Length < tamañoATener)
            {
                romExpandida = new byte[tamañoATener - rom.Datos.Length];
                rom.Datos = rom.Datos.AddArray(romExpandida);
            }
            else
            {
                rom.Datos = rom.Datos.SubArray(tamañoATener);
            }
		}
		public static Hex SearchBytes(RomGBA rom,byte[] bytesAEncontrar){
			return SearchBytes(rom,0,bytesAEncontrar);
		}
		public static Hex SearchBytes(RomGBA rom,Hex offsetInicio, byte[] bytesAEncontrar)
		{
            return rom.Datos.BuscarArray(offsetInicio,bytesAEncontrar);
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
            byte[] bytesEmpty = new byte[length];
            Hex posicion = SearchBytes(rom, offsetInicio, bytesEmpty);
            if (ends048C)
            {
                posicionString = posicion;
                //busco la posicion valida si no hay lanzo excepcion por falta de espacio
                while (posicionString[posicionString.Length - 1] != '0' && posicionString[posicionString.Length - 1] != '4' && posicionString[posicionString.Length - 1] != '8' && posicionString[posicionString.Length - 1] != 'C' && !acabado)
                {
                    posicion = SearchBytes(rom, posicion + 1, bytesEmpty);
                    posicionString = posicion;
                    acabado = posicion < 0;
                }

            }
            return posicion;
        }
    }
}
