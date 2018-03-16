using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFrameWork
{
   public class GranPaleta
    {
        public const int COUNT = 256;
        SortedList<int, byte> dic;
        Color[] paleta;
        public GranPaleta()
        {
            dic = new SortedList<int, byte>();
            paleta = new Color[COUNT];
        }
        public GranPaleta(Color[] paleta) : this()
        {
            if (paleta != null)
                for (int i = 0; i < paleta.Length && i < this.paleta.Length; i++)
                    this.paleta[i] = paleta[i];
            ConvertirGBAColor();
        }



        public Color this[int index]
        {
            get { return paleta[index]; }
            set
            {
                paleta[index] = value;
                UpdateDic();
            }
        }
        public int Lenght
        {
            get
            {
                return COUNT;
            }
        }
        public void ConvertirGBAColor()
        {
            for (int i = 0; i < paleta.Length; i++)
                paleta[i] = Paleta.ToGBAColor(paleta[i]);
            UpdateDic();
        }

        void UpdateDic()
        {
            int aux;
            dic.Clear();
            for (int i = 0; i < paleta.Length; i++)
            {
                aux = 0xFF << 24 | paleta[i].R << 16 | paleta[i].G << 8 | paleta[i].B;
                if (!dic.ContainsKey(aux))
                    dic.Add(aux, (byte)i);
            }
        }

        public byte? GetPosicion(Color color)
        {
            return GetPosicion(color.R, color.G, color.B);
        }
        public byte? GetPosicion(byte r, byte g, byte b)
        {

            byte? posicion = null;
            int argb = 0xFF << 24 | r << 16 | g << 8 | b;//por mirar

            if (dic.ContainsKey(argb))
                posicion = dic[argb];

            return posicion;
        }
        public static byte[] GetMap(GranPaleta paleta, byte[] bytesARGB)
        {
            if (paleta == null || bytesARGB == null || bytesARGB.Length % 4 != 0)
                throw new ArgumentException();
            byte[] map = new byte[bytesARGB.Length / 4];
            bool todoBien = true;
            unsafe
            {
                byte* ptrMap;
                int* ptrBytesARGB;
                fixed (byte* ptBytesARGB = bytesARGB)
                {
                    fixed (byte* ptMap = map)
                    {
                        ptrBytesARGB = (int*)ptBytesARGB;
                        ptrMap = ptMap;
                        for (int i = 0; i < map.Length && todoBien; i++)
                        {
                            todoBien = paleta.dic.ContainsKey(*ptrBytesARGB);
                            if (todoBien)
                            {
                                *ptrMap = paleta.dic[*ptrBytesARGB];
                                ptrMap++;
                                ptrBytesARGB++;
                            }

                        }
                    }
                }
            }
            if (!todoBien)
                throw new ArgumentException("Color no encontrado en la paleta!");
            return map;
        }
    }
}
