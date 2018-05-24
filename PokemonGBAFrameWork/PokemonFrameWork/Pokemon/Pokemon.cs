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
    public class PokemonCompleto : IComparable,IElementoBinarioComplejo
    {

        public enum OrdenPokemon
        {
            GameFreak,
            Local,
            Nacional
        }

        public static OrdenPokemon Orden = OrdenPokemon.Nacional;
        public static readonly Zona ZonaOrdenLocal;
        public static readonly Zona ZonaOrdenNacional;
        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(PokemonCompleto));



        static PokemonCompleto()
        {
            ZonaOrdenLocal = new Zona("Orden Local");
            ZonaOrdenNacional = new Zona("Orden Nacional");

  
            //orden local
            ZonaOrdenLocal.Add(0x3F9BC, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);
            ZonaOrdenLocal.Add(0x3F7F0, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaOrdenLocal.Add(0x430DC, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaOrdenLocal.Add(EdicionPokemon.RojoFuegoUsa, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(EdicionPokemon.VerdeHojaUsa, 0x431F0, 0x43204);
            ZonaOrdenLocal.Add(0x6D3FC, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

            //orden nacional
            ZonaOrdenNacional.Add(0x3FA08, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);
            ZonaOrdenNacional.Add(0x3F83C, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaOrdenNacional.Add(0x43128, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp);
            ZonaOrdenNacional.Add(EdicionPokemon.RojoFuegoUsa, 0x4323C, 0x43250);
            ZonaOrdenNacional.Add(EdicionPokemon.VerdeHojaUsa, 0x4323C, 0x43250);
            ZonaOrdenNacional.Add(0x6D448, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

         
    
        }
        public PokemonCompleto()
        {
            Stats = new Stats();
            Descripcion = new Descripcion();
            Sprites = new SpritesCompleto();
            //cry=new Cry();
            //growl=new Growl();
            Huella = new Huella();
            AtaquesAprendidos = new AtaquesAprendidos();
        }

        public Word OrdenNacional { get; set; }

        public Word OrdenLocal { get; set; }

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
                        compareTo = OrdenLocal.CompareTo(other.OrdenLocal);
                        break;
                    case OrdenPokemon.Nacional:
                        compareTo = OrdenNacional.CompareTo(other.OrdenNacional);
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

        public static PokemonCompleto GetPokemon(RomGba rom, int ordenGameFreak, int totalEntradasPokedex)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            PokemonCompleto pokemon = new PokemonCompleto();
            pokemon.OrdenGameFreak = new Word((ushort)ordenGameFreak);
            try
            {
                pokemon.OrdenLocal = new Word(rom, Zona.GetOffsetRom(ZonaOrdenLocal, rom).Offset + (pokemon.OrdenGameFreak - 1) * 2);
            }
            catch { }
            try
            {
                pokemon.OrdenNacional = new Word(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset + (pokemon.OrdenGameFreak - 1) * 2);
            }
            catch { }
            pokemon.Sprites = SpritesCompleto.GetSprites(rom, ordenGameFreak);
            pokemon.Stats = Stats.GetStats(rom, ordenGameFreak);
          
            pokemon.Nombre = Nombre.GetNombre(rom, pokemon.OrdenGameFreak);
            if (pokemon.OrdenNacional >= 0 && pokemon.OrdenNacional <= totalEntradasPokedex && false)
            {//el &&false es porque de momento no se como se hace esta parte...
             //pokemon.Cry=Cry.GetCry(rom,pokemon.OrdenNacional);
             //pokemon.Growl=Growl.GetGrowl(rom,pokemon.OrdenNacional);
            }
            pokemon.Huella = Huella.GetHuella(rom, ordenGameFreak);
            pokemon.AtaquesAprendidos = AtaquesAprendidos.GetAtaquesAprendidos(rom, ordenGameFreak);
            if (pokemon.OrdenNacional > 0 && pokemon.OrdenNacional < totalEntradasPokedex)
                pokemon.Descripcion = Descripcion.GetDescripcionPokedex(rom, pokemon.OrdenNacional);
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

        public static void SetPokemon(RomGba rom, PokemonCompleto pokemon, int totalEntradasPokedex, int totalObjetos, LlistaOrdenadaPerGrups<int, AtaquesAprendidos> dicAtaquesPokemon)
        {

  
            pokemon.Stats.SetObjetosEnLosStats(totalObjetos);

            Word.SetData(rom, Zona.GetOffsetRom(ZonaOrdenLocal, rom).Offset + pokemon.OrdenGameFreak * Word.LENGTH, pokemon.OrdenLocal);
            Word.SetData(rom, Zona.GetOffsetRom(ZonaOrdenNacional, rom).Offset + pokemon.OrdenGameFreak * Word.LENGTH, pokemon.OrdenNacional);
            Nombre.SetNombre(rom, pokemon.OrdenGameFreak, pokemon.Nombre);
            Stats.SetStats(rom, pokemon.OrdenGameFreak, pokemon.Stats);
            if (pokemon.Descripcion != null && pokemon.OrdenNacional > 0 && pokemon.OrdenNacional < totalEntradasPokedex)
                Descripcion.SetDescripcionPokedex(rom, pokemon.OrdenNacional, pokemon.Descripcion);

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

        }


        static int EspacioOcupadoAtaquesAprendidos(IList<PokemonCompleto> pokedex)
        {
            int total = 0;
            for (int i = 0; i < pokedex.Count; i++)
                total += pokedex[i].AtaquesAprendidos.ToBytesGBA().Length;
            return total;
        }



    }
}
