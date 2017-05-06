/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 7:21
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of BloqueBytes.
	/// </summary>
	public class BloqueBytes:ObjectAutoId
	{
		int offset;
		byte[] datos;
		#region Constructores
		public BloqueBytes(int lengthData)
		{
			Bytes = new byte[lengthData];
			OffsetInicio = -1;
		}
		public BloqueBytes(byte[] datos)
		{
			Bytes = datos;
			OffsetInicio = -1;
		}
		private BloqueBytes(int offsetInicio, byte[] datos)
		{
			Bytes = datos;
			OffsetInicio = offsetInicio;
		}
		
		#endregion
		#region Propiedades
		/// <summary>
		/// Devuelve el offset donde se inician los datos en la rom, puede devolver -1 indicando de que no se han sacado de la rom
		/// </summary>
		public int OffsetInicio {
			get {
				return offset;
			}
			private set {
				offset = value;
			}
		}
		public byte[] Bytes {
			get {
				return datos;
			}
			set {
				if (value == null)
					value = new byte[0];
				datos = value;
			}
		}
		public byte this[int index] {
			get{ return datos[index]; }
			set{ datos[index] = value; }
		}
		public int Length {
			get{ return datos.Length; }
		}
		/// <summary>
		/// devuelve el offset donde se acaban los datos en la rom ,puede devolver -1 indicando de que no se han sacado de la rom
		/// </summary>
		public int OffsetFin {
			get {
				int fin;
				if (OffsetInicio > 0)
					fin = OffsetInicio + Bytes.Length;
				else
					fin = -1;
				return fin;
			}
		}
		#endregion
		
		#region Metodos
		public byte[] SubArray(int inicio, int longitud)
		{
			return Bytes.SubArray(inicio, longitud);
		}
		public void SetArray(int inicio, byte[] datos)
		{
			Bytes.SetArray(inicio, datos);
		}
		public int SetArray( byte[] datos)
		{
			int offsetEmpty=SearchEmptyBytes(datos.Length);
			SetArray(offsetEmpty, datos);
			return offsetEmpty;
		}
		public int SearchEmptyBytes(int length,int inicio=0x800000)
		{
			int offsetEmpty=SearchEmptyBytes(length,0x0,inicio);
			if (offsetEmpty < 0)
				offsetEmpty = SearchEmptyBytes(length,0xFF,inicio);
			return offsetEmpty;
		}
		public int SearchEmptyBytes(int length,byte byteEmpty,int inicio=0x800000)
		{
			//tiene que acabar en 0,4,8,C
			const int MINIMO=100;//asi si hay un bloque que tiene que ser 0x0 o 0xFF por algo pues lo respeta :D mirar de ajustarlo
			int offsetEncontrado=inicio;
			int lengthFinal=length;
			if(length<MINIMO)
				lengthFinal=MINIMO;
			do
				offsetEncontrado=datos.SearchBlock(offsetEncontrado+1,lengthFinal,byteEmpty); 
			while(offsetEncontrado%4!=0&&offsetEncontrado>-1);

			return offsetEncontrado;
		}

		public int SearchArray(byte[] datos)
		{
			return SearchArray(0, datos);
		}
		public int SearchArray(int inicio, byte[] datos)
		{
			return Bytes.SearchArray(inicio, datos);
		}
		public void Remove(int inicio, int longitud, byte byteEmpty = 0xFF)
		{
			Bytes.Remove(inicio, longitud, byteEmpty);
		}
		public BloqueBytes Clone()
		{
			return new BloqueBytes(offset, (byte[])datos.Clone());
			
		}
		#endregion
		#region overrides
		public override string ToString()
		{
			return string.Format("[BloqueBytes Offset={0}]", offset);
		}
		public override bool Equals(object obj)
		{
			BloqueBytes other = obj as BloqueBytes;
			bool equals = other != null && Bytes.Length == other.Bytes.Length;
			
			if (equals) {
				equals=Bytes.ArrayEqual(other.Bytes);
			}
			return equals;
		}
		#endregion
		
		public static void SetBytes(BloqueBytes datos, int inicioDatos, BloqueBytes datosAPoner)
		{
			SetBytes(datos, inicioDatos, datosAPoner.Bytes);
		}
		public static void SetBytes(BloqueBytes datos, int inicioDatos, byte[] datosAPoner)
		{
			datos.Bytes.SetArray(inicioDatos, datosAPoner);
		}
		public static int SetBytes(BloqueBytes datos, BloqueBytes datosAPoner)
		{
			int inicioDatos =	datos.SearchEmptyBytes(datosAPoner.Length);
			SetBytes(datos, inicioDatos, datosAPoner);
			return inicioDatos;
		}
		public static BloqueBytes GetBytes(BloqueBytes bloque,int inicio,byte[] marcaFin)
		{
			return GetBytes(bloque,inicio,bloque.SearchArray(inicio,marcaFin)-inicio);
		}
		
		public static BloqueBytes GetBytes(BloqueBytes bloque, int inicio, int longitud)
		{
			return new BloqueBytes(inicio,bloque.SubArray(inicio, longitud));
		}
	}
}
