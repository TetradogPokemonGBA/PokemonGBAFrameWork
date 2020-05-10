using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class BloqueImagenGrande
    {
        class Row
        {
            public Row(byte[] fila)
            {
                Fila = fila;
            }

            public byte[] Fila { get; set; }
            public Color[] GetColors(GranPaleta paleta)
            {
                Color[] row = new Color[Fila.Length];
                for (int i = 0; i < row.Length; i++)
                    row[i] = paleta[Fila[i]];
                return row;
            }

            public Row GetRow(int offset, int length) => new Row(Fila.SubArray(offset, length));
        }

        Row[] dataImg;


        public BloqueImagenGrande(byte[] uncompressedData,int widthRow)
        {
            int restoFila = uncompressedData.Length % widthRow;
            bool incompleteRow = restoFila!= 0;
            dataImg = new Row[(uncompressedData.Length / widthRow)+(incompleteRow?1:0)];
            for (int i = 0, f = incompleteRow ? dataImg.Length - 1 : dataImg.Length; i < f; i++)
                dataImg[i] = new Row(uncompressedData.SubArray(widthRow * i, widthRow));
            if(incompleteRow)
                dataImg[dataImg.Length] = new Row(uncompressedData.SubArray(widthRow * (dataImg.Length-1), restoFila).AddArray(new byte[widthRow-restoFila]));
        }

        private BloqueImagenGrande() { }
      

        public List<GranPaleta> Paletas { get; set; } = new List<GranPaleta>();
        public Bitmap this[int index]
        {
            get
            {
                return Get(Paletas[index]);
            }
        }

        public Bitmap Get(GranPaleta granPaleta)
        {
            Bitmap bmp = new Bitmap(dataImg[0].Fila.Length, dataImg.Length);
            Color[] row;
            unsafe
            {
                Gabriel.Cat.S.Utilitats.V2.Color* ptrColores;

                bmp.TrataBytes((ptrData) =>
                {
                    ptrColores = (Gabriel.Cat.S.Utilitats.V2.Color*)ptrData;

                    for (int i = 0; i < dataImg.Length; i++)
                    {
                        row = dataImg[i].GetColors(granPaleta);
                        for (int j = 0; j < row.Length; j++)
                        {
                            *ptrColores = row[j];
                        }

                    }


                });
            }
            return bmp;
        }

        public BloqueImagenGrande Get(int index, int lado)
        {
            return Get(index, lado, lado);
        }
        public BloqueImagenGrande Get(int index,int height,int width)
        {
            return Get(GetX(index, width), GetY(index, height, width), height, width);
        }
        public BloqueImagenGrande Get(int x,int y, int height, int width)
        {
            BloqueImagenGrande bloqueImagenGrande = new BloqueImagenGrande();
            int startRow = x * width;
            int indexRow = y * height;
            bloqueImagenGrande.dataImg = new Row[height];
            for (int i = 0; i < height; i++)
                bloqueImagenGrande.dataImg[i] = dataImg[indexRow + i].GetRow(startRow, width);
            return bloqueImagenGrande;
        }
        private int GetY(int index, int height, int width)
        {
            int oneRow = dataImg[0].Fila.Length / width;
            int rows = dataImg.Length / height;
            throw new NotImplementedException();
        }

        private int GetX(int index, int width)
        {
            throw new NotImplementedException();
        }

        public int GetTotal(int height,int width)
        {
            return (dataImg.Length / height) * (dataImg[0].Fila.Length / width);
        }

    }
}
