using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Utilitats;
namespace PokemonGBAFrameWork
{
    public class Paquete:IElementoBinarioComplejo
    {
        public static string PathRomsBase;
        public static string PathPaquetes;
        public static readonly ElementoBinario Serializador;
        static TwoKeysList<string, long, RomGba> RomsCargadas;
        static TwoKeysList<string, long, Paquete> PaquetesCargados;
        public long Id { get; set; }
        public Llista<IElementoBinarioComplejo> ElementosPaquete { get; private set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;
        static Paquete()
        {
            Serializador = ElementoBinario.GetSerializador<Paquete>();
            PathPaquetes = System.IO.Path.Combine(Environment.CurrentDirectory, "Paquetes");
            RomsCargadas = new TwoKeysList<string, long, RomGba>();
            PaquetesCargados = new TwoKeysList<string, long, Paquete>();
            PathRomsBase = Path.Combine(Environment.CurrentDirectory, "RomsBase");

            try
            {
                if (!Directory.Exists(PathRomsBase))
                    Directory.CreateDirectory(PathRomsBase);
                else
                {
                    CargarRomsBase();
                }
                if (!System.IO.Directory.Exists(PathPaquetes))
                    System.IO.Directory.CreateDirectory(PathPaquetes);
                else
                {
                    CargarPaquetes();
                }

            }
            catch { }

        }



        public Paquete()
        {
            ElementosPaquete = new Llista<IElementoBinarioComplejo>();
            Id = DateTime.Now.Ticks;
        }
        public void FullLoad()
        {
            //miro cada elemento a ver si necesita cargar la base.

        }
        public static IElementoBinarioComplejo GetElementoBase(RomGba rom,byte idTipo,ushort idElemento)
        {
            IElementoBinarioComplejo elemento=null;
            int posicion = (int)idElemento;
            switch(idTipo)
            {
                case ClaseEntrenadorCompleto.ID:elemento = ClaseEntrenadorCompleto.GetClaseEntrenador(rom, posicion);break;
                case ClaseEntrenador.Nombre.ID: elemento = ClaseEntrenador.Nombre.GetNombre(rom, posicion); break;
                case ClaseEntrenador.RateMoney.ID: elemento = ClaseEntrenador.RateMoney.GetRateMoney(rom, posicion); break;
                case ClaseEntrenador.Sprite.ID: elemento = ClaseEntrenador.Sprite.GetSprite(rom, posicion); break;
                case ClaseEntrenador.Sprite.Data.ID: elemento = ClaseEntrenador.Sprite.Data.GetData(rom, posicion); break;
                case ClaseEntrenador.Sprite.Paleta.ID: elemento = ClaseEntrenador.Sprite.Paleta.GetPaleta(rom, posicion); break;
                case Entrenador.ID: elemento = Entrenador.GetEntrenador(rom, posicion); break;
                case EquipoPokemonEntrenador.ID: elemento = EquipoPokemonEntrenador.GetEquipo(rom, posicion); break;

                case Objeto.Datos.ID: elemento = Objeto.Datos.GetDatos(rom, posicion); break;
                case Objeto.Sprite.ID: elemento = Objeto.Sprite.GetSprite(rom, posicion); break;
                case ObjetoCompleto.ID: elemento = ObjetoCompleto.GetObjeto(rom, posicion); break;

                case PokeballBatalla.ID: elemento = PokeballBatalla.GetPokeballBatalla(rom, posicion); break;

                case PokemonErrante.Ruta.ID: elemento = PokemonErrante.Ruta.GetRuta(rom, posicion); break;

                case Mini.Sprite.ID:elemento = Mini.Sprite.GetMiniSprite(rom, posicion);break;
                case Mini.Paletas.ID: elemento = Mini.Paletas.GetPaletaMinis(rom, posicion); break;

                case AtaqueCompleto.ID:elemento = AtaqueCompleto.GetAtaque(rom, posicion);break;
                case Ataque.Concursos.ID: elemento = Ataque.Concursos.GetConcursos(rom, posicion); break;
                case Ataque.Datos.ID: elemento = Ataque.Datos.GetDatos(rom, posicion); break;
                case Ataque.Descripcion.ID: elemento = Ataque.Descripcion.GetDescripcion(rom, posicion); break;
                case Ataque.Nombre.ID: elemento = Ataque.Nombre.GetNombre(rom, posicion); break;

                case PokemonCompleto.ID: elemento = PokemonCompleto.GetPokemon(rom, posicion); break;
                case Pokemon.AtaquesAprendidos.ID: elemento = Pokemon.AtaquesAprendidos.GetAtaquesAprendidos(rom, posicion); break;
                case Pokemon.Descripcion.ID:elemento = Pokemon.Descripcion.GetDescripcionPokedex(rom, posicion);break;
                case Pokemon.Huella.ID: elemento = Pokemon.Huella.GetHuella(rom, posicion); break;
                case Pokemon.Nombre.ID: elemento = Pokemon.Nombre.GetNombre(rom, posicion); break;
                case Pokemon.OrdenLocal.ID: elemento = Pokemon.OrdenLocal.GetOrdenLocal(rom, posicion); break;
                case Pokemon.OrdenNacional.ID: elemento = Pokemon.OrdenNacional.GetOrdenNacional(rom, posicion); break;
                case Pokemon.SpritesCompleto.ID: elemento = Pokemon.SpritesCompleto.GetSprites(rom, posicion); break;
                case Pokemon.Sprite.Frontales.ID: elemento = Pokemon.Sprite.Frontales.GetFrontales(rom, posicion); break;
                case Pokemon.Sprite.PaletaNormal.ID: elemento = Pokemon.Sprite.PaletaNormal.GetPaletaNormal(rom, posicion); break;
                case Pokemon.Sprite.PaletaShiny.ID: elemento = Pokemon.Sprite.PaletaShiny.GetPaletaShiny(rom, posicion); break;
                case Pokemon.Sprite.Traseros.ID: elemento = Pokemon.Sprite.Traseros.GetTraseros(rom, posicion); break;
                case Pokemon.Stats.ID: elemento = Pokemon.Stats.GetStats(rom, posicion); break;
                case Pokemon.TipoCompleto.ID: elemento = Pokemon.TipoCompleto.GetTipo(rom, posicion); break;
                case Pokemon.Tipo.Nombre.ID: elemento = Pokemon.Tipo.Nombre.GetNombre(rom, posicion); break;


            }
            return elemento;
        }
        public static void CargarPaquetes()
        {
            string[] paquetes = Directory.GetFiles(PathPaquetes, "*.*", SearchOption.AllDirectories);
            Paquete paqueteActual;
            for (int i = 0; i < paquetes.Length; i++)
            {
                try
                {
                    paqueteActual = (Paquete)Serializador.GetObject(File.ReadAllBytes(paquetes[i]));
                    if (!PaquetesCargados.ContainsKey2(paqueteActual.Id))
                    {
                        PaquetesCargados.Add(paquetes[i], paqueteActual.Id, null);
                    }
                }
                catch { }
            }
        }
        public static void CargarRomsBase()
        {
            string[] romsBase;
            RomGba romActual;
            EdicionPokemon edicionPokemon;
            romsBase = Directory.GetFiles(PathRomsBase, "*.gba", SearchOption.AllDirectories);
            for (int i = 0; i < romsBase.Length; i++)
            {
                try
                {
                    romActual = new RomGba(romsBase[i]);
                    edicionPokemon = EdicionPokemon.GetEdicionPokemon(romActual);
                    if (!RomsCargadas.ContainsKey2(edicionPokemon.Id))
                    {
                        RomsCargadas.Add(romActual.Nombre, edicionPokemon.Id, romActual);
                        romActual.UnLoad();
                    }

                }
                catch { }

            }
        }
    }
}
