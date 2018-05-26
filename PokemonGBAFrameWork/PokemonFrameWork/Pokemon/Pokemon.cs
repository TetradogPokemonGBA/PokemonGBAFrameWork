/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 18:01
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.Pokemon;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of Pokemon.
    /// </summary>
    public class PokemonCompleto : IComparable, IElementoBinarioComplejo
    {

        public enum OrdenPokemon
        {
            GameFreak,
            Local,
            Nacional
        }
        public const byte ID = 0x20;
        public static OrdenPokemon Orden = OrdenPokemon.Nacional;
   
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<PokemonCompleto>();

        public PokemonCompleto()
        {
            OrdenLocal = new OrdenLocal();
            OrdenNacional = new OrdenNacional();
            Stats = new Stats();
            Descripcion = new Descripcion();
            Sprites = new SpritesCompleto();
            //cry=new Cry();
            //growl=new Growl();
            Huella = new Huella();
            AtaquesAprendidos = new AtaquesAprendidos();
        }


        public OrdenNacional OrdenNacional { get; set; }
        public OrdenLocal OrdenLocal { get; set; }

        public Word OrdenGameFreak { get; set; }

        public Nombre Nombre { get; set; }

        public Descripcion Descripcion { get; set; }

        public SpritesCompleto Sprites { get; set; }

        public Stats Stats { get; set; }


        //public Cry Cry {
        //	get {
        //		return cry;
        //	}
        //	set {
        //		cry = value;
        //	}
        //}

        //public Growl Growl {
        //	get {
        //		return growl;
        //	}
        //	set {
        //		growl = value;
        //	}
        //}

        public Huella Huella { get; set; }

        public AtaquesAprendidos AtaquesAprendidos { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        #region IComparable implementation


        public int CompareTo(object obj)
        {
            PokemonCompleto other = obj as PokemonCompleto;
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

        public static PokemonCompleto GetPokemon(RomGba rom, int ordenGameFreak, int totalEntradasPokedex=-1)
        {
            if (totalEntradasPokedex < 0)
                totalEntradasPokedex = Descripcion.GetTotal(rom);

            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            PokemonCompleto pokemon = new PokemonCompleto();
            pokemon.OrdenGameFreak = new Word((ushort)ordenGameFreak);
            pokemon.OrdenLocal = OrdenLocal.GetOrdenLocal(rom, ordenGameFreak);
            pokemon.OrdenNacional = OrdenNacional.GetOrdenNacional(rom, ordenGameFreak);
            pokemon.Sprites = SpritesCompleto.GetSprites(rom, ordenGameFreak);
            pokemon.Stats = Stats.GetStats(rom, ordenGameFreak);

            pokemon.Nombre = Nombre.GetNombre(rom, pokemon.OrdenGameFreak);
            if (pokemon.OrdenNacional.Orden != null && pokemon.OrdenNacional.Orden <= totalEntradasPokedex && false)
            {//el &&false es porque de momento no se como se hace esta parte...
             //pokemon.Cry=Cry.GetCry(rom,pokemon.OrdenNacional);
             //pokemon.Growl=Growl.GetGrowl(rom,pokemon.OrdenNacional);
            }
            pokemon.Huella = Huella.GetHuella(rom, ordenGameFreak);
            pokemon.AtaquesAprendidos = AtaquesAprendidos.GetAtaquesAprendidos(rom, ordenGameFreak);
            if (pokemon.OrdenNacional.Orden != null && pokemon.OrdenNacional.Orden < totalEntradasPokedex)
                pokemon.Descripcion = Descripcion.GetDescripcionPokedex(rom, pokemon.OrdenNacional.Orden);
            return pokemon;

        }

        public static PokemonCompleto[] GetPokedex(RomGba rom)
        {
            PokemonCompleto[] pokedex = new PokemonCompleto[Huella.GetTotal(rom)];
            int totalEntradasPokedex = Descripcion.GetTotal(rom);
            for (int i = 0; i < pokedex.Length; i++)
                pokedex[i] = PokemonCompleto.GetPokemon(rom, i, totalEntradasPokedex);
            return pokedex;

        }

        public static void SetPokemon(RomGba rom, PokemonCompleto pokemon, int totalEntradasPokedex=-1, int totalObjetos=-1, LlistaOrdenadaPerGrups<int, AtaquesAprendidos> dicAtaquesPokemon=null)
        {
            if (totalEntradasPokedex < 0)
                totalEntradasPokedex = Descripcion.GetTotal(rom);
            if (totalObjetos < 0)
                totalObjetos = Objeto.Datos.GetTotal(rom);
            if (dicAtaquesPokemon == null)
                dicAtaquesPokemon = AtaquesAprendidos.GetAtaquesAprendidosDic(rom);

            OrdenLocal.SetOrdenLocal(rom, pokemon.OrdenGameFreak, pokemon.OrdenLocal);
            Nombre.SetNombre(rom, pokemon.OrdenGameFreak, pokemon.Nombre);
            Stats.SetStats(rom, pokemon.OrdenGameFreak, pokemon.Stats,totalObjetos);

            OrdenNacional.SetOrdenNacional(rom, pokemon.OrdenGameFreak, pokemon.OrdenNacional);
            if (pokemon.Descripcion != null && pokemon.OrdenNacional.Orden != null && pokemon.OrdenNacional.Orden < totalEntradasPokedex)
                Descripcion.SetDescripcion(rom, pokemon.OrdenNacional.Orden, pokemon.Descripcion);

            if (pokemon.AtaquesAprendidos != null)
                AtaquesAprendidos.SetAtaquesAprendidos(rom, pokemon.OrdenGameFreak, pokemon.AtaquesAprendidos, dicAtaquesPokemon);

            if (pokemon.Huella != null)
                Huella.SetHuella(rom, pokemon.OrdenGameFreak, pokemon.Huella);

            if (pokemon.Sprites != null)
                SpritesCompleto.SetSprites(rom, pokemon.OrdenGameFreak, pokemon.Sprites);

            //falta hacer los setCry y setGrowl
        }

        public static void SetPokedex(RomGba rom, IList<PokemonCompleto> pokedex)
        {
            List<OrdenLocal> ordenesLocal = new List<OrdenLocal>();
            List<OrdenNacional> ordenesNacionales = new List<OrdenNacional>();
            List<Huella> huellas = new List<Huella>();
            List<SpritesCompleto> sprites = new List<SpritesCompleto>();
            List<AtaquesAprendidos> ataquesAprendidos = new List<AtaquesAprendidos>();
            List<Descripcion> descripcions = new List<Descripcion>();
            List<Nombre> nombres = new List<Nombre>();
            List<Stats> stats = new List<Stats>();

            for(int i=0;i<pokedex.Count;i++)
            {
                ordenesLocal.Add(pokedex[i].OrdenLocal);
                ordenesNacionales.Add(pokedex[i].OrdenNacional);
                huellas.Add(pokedex[i].Huella);
                sprites.Add(pokedex[i].Sprites);
                ataquesAprendidos.Add(pokedex[i].AtaquesAprendidos);
                descripcions.Add(pokedex[i].Descripcion);
                nombres.Add(pokedex[i].Nombre);
                stats.Add(pokedex[i].Stats);
            }

            OrdenLocal.SetOrdenLocal(rom, ordenesLocal);
            OrdenNacional.SetOrdenNacional(rom, ordenesNacionales);
            Huella.SetHuella(rom, huellas);
            SpritesCompleto.SetSprites(rom, sprites);
            AtaquesAprendidos.SetAtaquesAprendidos(rom, ataquesAprendidos);
            Descripcion.SetDescripcionPokedex(rom, descripcions);
            Nombre.SetNombre(rom, nombres);
            Stats.SetStats(rom, stats);
            
        }


    }
}
