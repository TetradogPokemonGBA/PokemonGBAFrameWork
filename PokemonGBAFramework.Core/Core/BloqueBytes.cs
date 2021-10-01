﻿using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.Text;
namespace PokemonGBAFramework.Core
{
    public class BloqueBytes : IClonable<BloqueBytes>, ICloneable
    {
        byte[] datos;
        #region Constructores
        public BloqueBytes() { }
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
        public int OffsetInicio { get; set; }
        public byte[] Bytes
        {
            get
            {
                return datos;
            }
            set
            {
                if (value == null)
                    value = new byte[0];
                datos = value;
            }
        }
        public byte this[int index]
        {
            get { return datos[index]; }
            set { datos[index] = value; }
        }
        public int Length
        {
            get { return datos.Length; }
        }
        /// <summary>
        /// devuelve el offset donde se acaban los datos en la rom ,puede devolver -1 indicando de que no se han sacado de la rom
        /// </summary>
        public int OffsetFin
        {
            get
            {
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
        public int SearchEmptySpaceAndSetArray(byte[] datos, int inicio = 0x800000)
        {
            int offsetEmpty = SearchEmptyBytes(datos.Length, inicio);
            SetArray(offsetEmpty, datos);
            return offsetEmpty;
        }
        public int SetArrayIfNotExist(byte[] data)
        {
            int offset = SearchArray(data);
            if (offset < 0)
                offset = SearchEmptySpaceAndSetArray(data);
            return offset;
        }
        public int SearchEmptyBytes(int length, int inicio = 0x800000)
        {
            int offsetEmpty = SearchEmptyBytes(length, 0xFF, inicio);
            if (offsetEmpty < 0)
            {
                offsetEmpty = SearchEmptyBytes(length, 0x0, inicio);

                if (offsetEmpty < 0)
                {
                    if (Bytes.Length == RomGba.MAXLENGTH - length)
                        throw new OutOfMemoryException("No se ha encontrado espacio libre...");
                    else
                    {
                        Bytes = Bytes.AddArray(new byte[Bytes.Length + length * 10 < RomGba.MAXLENGTH ? length * 10 : length]);
                        offsetEmpty = SearchEmptyBytes(length, inicio);
                    }
                }
            }
            return offsetEmpty;
        }
        public int SearchEmptyBytes(int length, byte byteEmpty, int inicio = 0x800000)
        {
            //tiene que acabar en 0,4,8,C
           /* const int MINIMO = 150;*///asi si hay un bloque que tiene que ser 0x0 o 0xFF por algo pues lo respeta :D mirar de ajustarlo
            int offsetEncontrado = inicio;
            int lengthFinal = length;
            bool continuarBuscando=true;
            //if (length < MINIMO)
            //    lengthFinal = MINIMO;
            do
            {
                offsetEncontrado = datos.SearchBlock(offsetEncontrado, lengthFinal, byteEmpty);
                if (offsetEncontrado >= 0)
                {
                    continuarBuscando = offsetEncontrado % 4 != 0;
                    if (continuarBuscando)
                        offsetEncontrado = offsetEncontrado.NextOffsetValido();
                }
            }
            while (continuarBuscando && offsetEncontrado > -1);

            return offsetEncontrado;
        }

        public int SearchArray(byte[] datos)
        {
            return SearchArray(0, datos);
        }
        public int SearchArray(int inicio, byte[] datos)
        {
            return SearchArray(inicio, -1, datos);
        }
        public int SearchArray(int inicio, int fin, byte[] datos)
        {
            return Bytes.SearchArray(inicio, fin, datos);
        }

        public void Replace(byte[] oldData, byte[] newData)
        {//por mirar...
            int offsetOffset;
            OffsetRom offsetNew;
            OffsetRom offsetOld;
            int offset = SearchArray(oldData);
            offsetOld = new OffsetRom(offset);

            Remove(offset, oldData.Length);
            offsetNew =new OffsetRom(SearchEmptySpaceAndSetArray(newData, offset));
            //actualizo los punteros que hicieran referencia por el nuevo
            do
            {
                offsetOffset = SearchArray(offsetOld.BytesPointer);
                if (offsetOffset > 0)
                    SetArray(offsetOffset, offsetNew.BytesPointer);
            } while (offsetOffset > 0);
            

            
        }

        public void Remove(int inicio, int longitud, byte byteEmpty = 0xFF)
        {
            Bytes.Remove(inicio, longitud, byteEmpty);
        }

        #region IClonable implementation
        public object Clone()
        {
            return Clon();
        }
        public BloqueBytes Clon()
        {
            return new BloqueBytes(OffsetInicio, (byte[])datos.Clone());
        }


        #endregion

        #endregion
        #region overrides
        public override string ToString()
        {
            return string.Format("[BloqueBytes Offset={0}]", OffsetInicio);
        }
        public override bool Equals(object obj)
        {
            BloqueBytes other = obj as BloqueBytes;
            bool equals = other != null && Bytes.Length == other.Bytes.Length;

            if (equals)
            {
                equals = Bytes.ArrayEqual(other.Bytes);
            }
            return equals;
        }
        #endregion

        public static BloqueBytes GetBytes(BloqueBytes bloque, int inicio, byte[] marcaFin)
        {
            return GetBytes(bloque, inicio, bloque.SearchArray(inicio, marcaFin) - inicio);
        }

        public static BloqueBytes GetBytes(BloqueBytes bloque, int inicio, int longitud)
        {
            return new BloqueBytes(inicio, bloque.SubArray(inicio, longitud));
        }


    }
}