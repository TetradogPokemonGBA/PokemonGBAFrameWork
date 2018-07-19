using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Mapa
{
    public class DatosBloque
    {
        Llista<Bloque> bloques;
        public DatosBloque()
        {
            bloques = new Llista<Bloque>();
        }
        public DatosBloque(byte[] datos):this()
        {
            unsafe
            {
                ushort* ptrDatos;
                fixed(byte* ptDatos=datos)
                {
                    ptrDatos =(ushort*) ptDatos;
                    for(int i=0,iF=datos.Length/2;i<iF;i++)
                    {
                        Add(*ptrDatos);
                        ptrDatos++;
                    }
                }
            }
        }
        public void Add(ushort bloque)
        {
            Add((Bloque)bloque);
        }
        public void Add(Bloque bloque)
        {
            bloques.Add(bloque);
        }
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[bloques.Count * 2];
            unsafe
            {
                ushort* ptrBloques;
                fixed (byte* ptBloques = bytes)
                {
                    ptrBloques = (ushort*)ptBloques;
                    for (int i = 0; i < bloques.Count; i++)
                    {
                        *ptrBloques = bloques[i];
                        ptrBloques++;
                    }
                }
            }
            return bytes;
        }
        public DatosBloque Clone()
        {
            DatosBloque clon = new DatosBloque();
            for(int i=0;i<bloques.Count;i++)
            {
                clon.Add((ushort)bloques[i]);
            }
            return clon;
        }
        public void ReemplazarBloques(DatosBloque datosBloque)
        {
            bloques.Clear();
            for (int i = 0; i <datosBloque.bloques.Count; i++)
            {
                bloques.Add((Bloque)(ushort)datosBloque.bloques[i]);
            }
        }
        public override bool Equals(object obj)
        {
            DatosBloque other = obj as DatosBloque;
            bool equals=other!=null;
            if(equals)
            {
                equals = bloques.Count == other.bloques.Count;
                if(equals)
                {
                    for (int i = 0; i < bloques.Count && equals; i++)
                        equals = (ushort)bloques[i] == other.bloques[i];
                }
            }
            return equals;
        }
    }
}
