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
        byte[] data;
        bool isCompressed;
        int widthRow;
        public BloqueImagenGrande(byte[] bytesImg,int widthRow,bool isCompressed=false)
        {
            data = bytesImg;
            this.widthRow = widthRow;
            this.isCompressed = isCompressed;
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

        private void EndLoad()
        {
            int restoFila;
            bool incompleteRow;
            if (Equals(dataImg, default))
            {
                if (isCompressed)
                    data = LZ77.Descomprimir(data);

                restoFila = data.Length % widthRow;
                incompleteRow = restoFila != 0;
                this.dataImg = new Row[(data.Length / widthRow) + (incompleteRow ? 1 : 0)];
                for (int i = 0, f = incompleteRow ? this.dataImg.Length - 1 : this.dataImg.Length; i < f; i++)
                    this.dataImg[i] = new Row(data.SubArray(widthRow * i, widthRow));
                if (incompleteRow)
                    this.dataImg[dataImg.Length - 1] = new Row(data.SubArray(widthRow * (this.dataImg.Length - 1), restoFila).AddArray(new byte[widthRow - restoFila]));
                data = default;
            }
        }

        public Bitmap Get(GranPaleta granPaleta)
        {
            Color[] row;
            Bitmap bmp;

            EndLoad();

            bmp = new Bitmap(dataImg[0].Fila.Length, dataImg.Length);

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
            return Get(GetX(index, width), GetY(index, width), height, width);
        }
        public BloqueImagenGrande Get(int x,int y, int height, int width)
        {
            BloqueImagenGrande bloqueImagenGrande = new BloqueImagenGrande();
            int startRow = x * width;
            int indexRow = y * height;
            bloqueImagenGrande.dataImg = new Row[height];

            EndLoad();

            for (int i = 0; i < height; i++)
                bloqueImagenGrande.dataImg[i] = dataImg[indexRow + i].GetRow(startRow, width);
            if(!Equals(Paletas,default))
              bloqueImagenGrande.Paletas.AddRange(Paletas);
            return bloqueImagenGrande;
        }
        public int GetY(int index, int width)
        {
            int oneRow;
            EndLoad();
            oneRow= dataImg[0].Fila.Length / width;
            return index / oneRow;
           
        }

        public int GetX(int index, int width)
        {
            int oneRow;
            EndLoad();
            oneRow = dataImg[0].Fila.Length / width;
            return index % oneRow;
        }

        public int GetTotal(int height,int width)
        {
            EndLoad();
            return (dataImg.Length / height) * (dataImg[0].Fila.Length / width);
        }

        #region operadores y castings
        public static Bitmap operator +(BloqueImagenGrande imagenGrande, GranPaleta paleta) => imagenGrande.Get(paleta);
        public static Bitmap operator +(BloqueImagenGrande imagenGrande, int indexPaleta) => imagenGrande[indexPaleta];
        public static explicit operator Bitmap(BloqueImagenGrande bloque)=>bloque+0;
        #endregion
    }
}
