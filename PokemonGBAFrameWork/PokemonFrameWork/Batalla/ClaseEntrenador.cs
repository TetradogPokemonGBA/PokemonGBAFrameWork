using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.ClaseEntrenador;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class CategoriaEntrenador
    {
        RateMoney rateMoney;
        Sprite sprite;
        Nombre nombre;

        public CategoriaEntrenador()
        {
            sprite = new Sprite();
            nombre = new Nombre();
            rateMoney = new RateMoney();
        }

        public RateMoney RateMoney
        {
            get
            {
                return rateMoney;
            }
            set
            {
                rateMoney = value;
            }
        }

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
        }

        public Nombre Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }
        public override string ToString()
        {
            return nombre.ToString();
        }

        public static CategoriaEntrenador GetClaseEntrenador(RomGba rom, int index)
        {
            //los nombres no los carga bien...
           
            //podria ser que no se posicionase bien...
            CategoriaEntrenador claseCargada = new CategoriaEntrenador();

            claseCargada.Sprite = Sprite.GetSprite(rom, index);
            claseCargada.Nombre = Nombre.GetNombre(rom, index);
            claseCargada.RateMoney = RateMoney.GetRateMoney(rom, index);

            return claseCargada;
        }

        public static CategoriaEntrenador[] GetClasesEntrenador(RomGba rom)
        {
            CategoriaEntrenador[] clases = new CategoriaEntrenador[Sprite.GatTotal(rom)];
            for (int i = 0; i < clases.Length; i++)
                clases[i] = GetClaseEntrenador(rom, i);
            return clases;
        }


        public static void SetClaseEntrenador(RomGba rom, CategoriaEntrenador claseEntrenador, int index)
        {
            if (rom == null || claseEntrenador == null)
                throw new ArgumentNullException();

            Nombre.SetNombre(rom, index, claseEntrenador.Nombre);
            Sprite.SetSprite(rom, index, claseEntrenador.Sprite);
            RateMoney.SetRateMoney(rom, index, claseEntrenador.RateMoney);

        }

        public static void SetClasesEntrenador(RomGba rom, IList<CategoriaEntrenador> clasesEntrenador)
        {
            IList<Sprite> sprites = new List<Sprite>();
            IList<Nombre> nombres = new List<Nombre>();
            IList<RateMoney> rates = new List<RateMoney>();


            for (int i = 0; i < clasesEntrenador.Count; i++)
            {
                sprites.Add(clasesEntrenador[i].Sprite);
                nombres.Add(clasesEntrenador[i].Nombre);
                rates.Add(clasesEntrenador[i].RateMoney);
            }
            Sprite.SetSprite(rom, sprites);
            Nombre.SetNombre(rom, nombres);
            RateMoney.SetRateMoney(rom, rates);
        }
    }
}
