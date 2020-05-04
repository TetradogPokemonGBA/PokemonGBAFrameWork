using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Pokemon
    {
        public enum OrdenPokemon
        {
            GameFreak,
            Local,
            Nacional
        }

        public static OrdenPokemon Orden = OrdenPokemon.Nacional;

        public Pokemon()
        {
            OrdenLocal = new OrdenLocal();
            OrdenNacional = new OrdenNacional();
            Stats = new Stats();
            Descripcion = new DescripcionPokedex();
            Sprites = new Sprites();
            Huella = new Huella();
            AtaquesAprendidos = new AtaquesAprendidos();
        }


        public OrdenNacional OrdenNacional { get; set; }
        public OrdenLocal OrdenLocal { get; set; }

        public Word OrdenGameFreak { get; set; }

        public Nombre Nombre { get; set; }

        public DescripcionPokedex Descripcion { get; set; }

        public Sprites Sprites { get; set; }

        public Stats Stats { get; set; }



        public Huella Huella { get; set; }

        public AtaquesAprendidos AtaquesAprendidos { get; set; }

        #region IComparable implementation


        public int CompareTo(object obj)
        {
            Pokemon other = obj as Pokemon;
            int compareTo;
            if (other != null)
            {
                switch (Orden)
                {
                    case OrdenPokemon.GameFreak:
                        compareTo = OrdenGameFreak.CompareTo(other.OrdenGameFreak);
                        break;
                    case OrdenPokemon.Local:
                        compareTo = OrdenLocal.Orden.CompareTo(other.OrdenLocal);
                        break;
                    case OrdenPokemon.Nacional:
                        compareTo = OrdenNacional.Orden.CompareTo(other.OrdenNacional);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }


            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }

        #endregion
        public override string ToString()
        {
            return Nombre + "  #" + OrdenNacional;
        }

        public static Pokemon Get(RomGba rom, int ordenGameFreak, int totalEntradasPokedex = -1,OffsetRom offsetDescripcionPokedex=default,OffsetRom offsetOrdenLocal=default,OffsetRom offsetOrdenNacional=default,OffsetRom offsetNombre=default,OffsetRom offsetHuella=default,OffsetRom offsetStats=default,OffsetRom offsetAtaquesAprendidos=default,OffsetRom[] offsetsSprites=default)
        {
            if (totalEntradasPokedex < 0)
                totalEntradasPokedex = DescripcionPokedex.GetTotal(rom,offsetDescripcionPokedex);

            Pokemon pokemon = new Pokemon();

            pokemon.OrdenGameFreak = new Word((ushort)ordenGameFreak);
            pokemon.OrdenLocal = OrdenLocal.Get(rom, ordenGameFreak,offsetOrdenLocal);
            pokemon.OrdenNacional = OrdenNacional.Get(rom, ordenGameFreak,offsetOrdenNacional);
            pokemon.Sprites = Sprites.Get(rom, ordenGameFreak,offsetsSprites);
            pokemon.Stats = Stats.Get(rom, ordenGameFreak,offsetStats);

            pokemon.Nombre = Nombre.Get(rom, pokemon.OrdenGameFreak,offsetNombre);
            pokemon.Huella = Huella.Get(rom, ordenGameFreak,offsetHuella);
            pokemon.AtaquesAprendidos = AtaquesAprendidos.Get(rom, ordenGameFreak,offsetAtaquesAprendidos);
            if (pokemon.OrdenNacional.Orden != default && pokemon.OrdenNacional.Orden < totalEntradasPokedex)
                pokemon.Descripcion = DescripcionPokedex.Get(rom, pokemon.OrdenNacional.Orden,offsetDescripcionPokedex);


            return pokemon;

        }

        public static Pokemon[] Get(RomGba rom, OffsetRom offsetDescripcionPokedex = default, OffsetRom offsetOrdenLocal = default, OffsetRom offsetOrdenNacional = default, OffsetRom offsetNombre = default, OffsetRom offsetHuella = default, OffsetRom offsetStats = default, OffsetRom offsetAtaquesAprendidos = default, OffsetRom[] offsetsSprites = default, int totalPokemon = -1, int totalDescripciones = -1)
        {
             const int MISSIGNO = 0;

            Pokemon[] pokedex = new Pokemon[totalPokemon<0?Huella.GetTotal(rom, offsetHuella):totalPokemon];
            int totalEntradasPokedex =totalDescripciones<0? DescripcionPokedex.GetTotal(rom, offsetDescripcionPokedex):totalDescripciones;

             offsetDescripcionPokedex =Equals(offsetDescripcionPokedex, default)? DescripcionPokedex.GetOffset(rom):offsetDescripcionPokedex;
             offsetOrdenLocal = Equals(offsetOrdenLocal, default) ? OrdenLocal.GetOffset(rom):offsetOrdenLocal;
             offsetOrdenNacional = Equals(offsetOrdenNacional, default) ? OrdenNacional.GetOffset(rom):offsetOrdenNacional;
             offsetNombre = Equals(offsetNombre, default) ? Nombre.GetOffset(rom):offsetNombre;
             offsetHuella = Equals(offsetHuella, default) ? Huella.GetOffset(rom):offsetHuella;
             offsetAtaquesAprendidos = Equals(offsetAtaquesAprendidos, default) ? AtaquesAprendidos.GetOffset(rom):offsetAtaquesAprendidos;
             offsetStats = Equals(offsetStats, default) ? Stats.GetOffset(rom):offsetStats;
             offsetsSprites = Equals(offsetsSprites, default) ? Sprites.GetOffsets(rom): offsetsSprites;


            for (int i = 0; i < pokedex.Length; i++)
                pokedex[i] = Get(rom, i, totalEntradasPokedex,offsetDescripcionPokedex,offsetOrdenLocal,offsetOrdenNacional,offsetNombre,offsetHuella,offsetStats,offsetAtaquesAprendidos, offsetsSprites);
           
            if(pokedex[MISSIGNO].OrdenGameFreak==pokedex[MISSIGNO].OrdenLocal.Orden) //es missigno que tiene en la nacional el mismo orden que Mew por eso lo pongo
                 pokedex[MISSIGNO].OrdenNacional.Orden = pokedex[MISSIGNO].OrdenGameFreak;

            return pokedex;

        }
        public static Pokemon[] GetOrdenLocal(RomGba rom, OffsetRom offsetDescripcionPokedex = default, OffsetRom offsetOrdenLocal = default, OffsetRom offsetOrdenNacional = default, OffsetRom offsetNombre = default, OffsetRom offsetHuella = default, OffsetRom offsetStats = default, OffsetRom offsetAtaquesAprendidos = default, OffsetRom[] offsetsSprites = default)
        {
            Pokemon[] pokemon = Get(rom,offsetDescripcionPokedex,offsetOrdenLocal,offsetOrdenNacional,offsetNombre,offsetHuella,offsetStats,offsetAtaquesAprendidos,offsetsSprites);
            Pokemon[] ordenados = new Pokemon[pokemon.Length];
            for (int i = 0; i < pokemon.Length; i++)
                ordenados[pokemon[i].OrdenLocal.Orden] = pokemon[i];
            return ordenados;
        }
        public static Pokemon[] GetOrdenNacional(RomGba rom, OffsetRom offsetDescripcionPokedex = default, OffsetRom offsetOrdenLocal = default, OffsetRom offsetOrdenNacional = default, OffsetRom offsetNombre = default, OffsetRom offsetHuella = default, OffsetRom offsetStats = default, OffsetRom offsetAtaquesAprendidos = default, OffsetRom[] offsetsSprites = default)
        {
            Pokemon[] pokemon = Get(rom, offsetDescripcionPokedex, offsetOrdenLocal, offsetOrdenNacional, offsetNombre, offsetHuella, offsetStats, offsetAtaquesAprendidos, offsetsSprites);
            Pokemon[] ordenados = new Pokemon[pokemon.Length];
            for (int i = 0; i < pokemon.Length; i++)
                ordenados[pokemon[i].OrdenNacional.Orden] = pokemon[i];
            return ordenados;
        }
    }
}
