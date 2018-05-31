using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Utilitats;
namespace PokemonGBAFrameWork
{
    public class Paquete : IElementoBinarioComplejo
    {
        public static string PathRomsBase;
        public static string PathPaquetes;
        public static readonly ElementoBinario Serializador;
        static TwoKeysList<string, long, RomGba> RomsCargadas;
        static TwoKeysList<string, long, Paquete> PaquetesCargados;
        public long Id { get; set; }
        public Llista<ElementoSerializado> ElementosPaquetePendientes { get; private set; }
        public PokemonFrameWorkItem[] ItemsCargados { get; private set; }
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
            ElementosPaquetePendientes = new Llista<ElementoSerializado>();
            Id = DateTime.Now.Ticks;
            PaquetesCargados.Add(""+Id, Id, this);
        }
        public void FullLoad()
        {
            IElementoBinarioComplejo aux;
            //miro cada elemento a ver si necesita cargar la base.
            if (ItemsCargados == null)
                ItemsCargados = new PokemonFrameWorkItem[ElementosPaquetePendientes.Count];
            for (int i = 0; i < ItemsCargados.Length; i++)
            {
                if (ItemsCargados[i] == null)
                {
                    if (ElementosPaquetePendientes[i].Id <= EdicionPokemon.IDMINRESERVADO)
                        aux = GetElementoBase(EdicionPokemon.GetEdicionCompatible(ElementosPaquetePendientes[i].Id), ElementosPaquetePendientes[i].IdTipo, ElementosPaquetePendientes[i].IdElemento);
                    else
                    {
                        //viene de un paquete
                        aux = PaquetesCargados[ElementosPaquetePendientes[i].Id].GetFullElement(ElementosPaquetePendientes[i].Id, ElementosPaquetePendientes[i].IdElemento);
                    }
                    ItemsCargados[i] = (PokemonFrameWorkItem)aux.Serialitzer.GetObject(ElementosPaquetePendientes[i].GetBytesCompletos(aux.Serialitzer.GetBytes(aux)));
                }
            }

        }

        public PokemonFrameWorkItem GetFullElement(long idFuente, ushort idElemento)
        {//falta testing
            IElementoBinarioComplejo aux = null;
            if (ItemsCargados[idElemento] == null)
            {
                if (idFuente <= EdicionPokemon.IDMINRESERVADO)
                    aux = GetElementoBase(EdicionPokemon.GetEdicionCompatible(idFuente), ElementosPaquetePendientes[idElemento].IdTipo, ElementosPaquetePendientes[idElemento].IdElemento);
                else if (ElementosPaquetePendientes[idElemento].Id != idFuente)
                {
                    //viene de un paquete
                    if (!PaquetesCargados.ContainsKey2(idFuente))
                        throw new PaqueteNoCargadoException();
                    aux = PaquetesCargados[idFuente].GetFullElement(ElementosPaquetePendientes[idElemento].Id, ElementosPaquetePendientes[idElemento].IdElemento);
                }
                else if (ElementosPaquetePendientes[idElemento].SinBase)
                {
                    aux = GetElementoBase(EdicionPokemon.GetEdicionCompatible(ElementosPaquetePendientes[idElemento].Id), ElementosPaquetePendientes[idElemento].IdTipo, ElementosPaquetePendientes[idElemento].IdElemento);
                }
                else//es 100% original del usuario :3 de allí que  tenga base
                {
                    //tiene la base
                    aux = (IElementoBinarioComplejo) GetSerializador(ElementosPaquetePendientes[idElemento].IdTipo).GetObject(ElementosPaquetePendientes[idElemento].BytesSinBase);
                }
                ItemsCargados[idElemento] = (PokemonFrameWorkItem)aux;
            }
            return ItemsCargados[idElemento];
        }



        public static PokemonFrameWorkItem GetElementoBase(EdicionPokemon edicion, byte idTipo, ushort idElemento)
        {
            if (!RomsCargadas.ContainsKey2(edicion.Id))
                throw new RomBaseNoCargadaException();

            PokemonFrameWorkItem elemento = null;
            int posicion = (int)idElemento;
            RomGba rom = RomsCargadas[edicion.Id];
            switch (idTipo)
            {
                case ClaseEntrenadorCompleto.ID: elemento = ClaseEntrenadorCompleto.GetClaseEntrenador(rom, posicion); break;
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

                case Mini.Sprite.ID: elemento = Mini.Sprite.GetMiniSprite(rom, posicion); break;
                case Mini.Paletas.ID: elemento = Mini.Paletas.GetPaletasMinis(rom); break;

                case AtaqueCompleto.ID: elemento = AtaqueCompleto.GetAtaque(rom, posicion); break;
                case Ataque.Concursos.ID: elemento = Ataque.Concursos.GetConcursos(rom, posicion); break;
                case Ataque.Datos.ID: elemento = Ataque.Datos.GetDatos(rom, posicion); break;
                case Ataque.Descripcion.ID: elemento = Ataque.Descripcion.GetDescripcion(rom, posicion); break;
                case Ataque.Nombre.ID: elemento = Ataque.Nombre.GetNombre(rom, posicion); break;

                case PokemonCompleto.ID: elemento = PokemonCompleto.GetPokemon(rom, posicion); break;
                case Pokemon.AtaquesAprendidos.ID: elemento = Pokemon.AtaquesAprendidos.GetAtaquesAprendidos(rom, posicion); break;
                case Pokemon.Descripcion.ID: elemento = Pokemon.Descripcion.GetDescripcionPokedex(rom, posicion); break;
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

                    //si se añaden más hay que ponerlos!
            }
            return elemento;
        }
        public static ElementoBinario GetSerializador(byte idTipo)
        {
            ElementoBinario serializador=null;
            switch (idTipo)
            {
                case ClaseEntrenadorCompleto.ID: serializador = ClaseEntrenadorCompleto.Serializador; break;
                case ClaseEntrenador.Nombre.ID: serializador = ClaseEntrenador.Nombre.Serializador; break;
                case ClaseEntrenador.RateMoney.ID: serializador = ClaseEntrenador.RateMoney.Serializador; break;
                case ClaseEntrenador.Sprite.ID: serializador = ClaseEntrenador.Sprite.Serializador; break;
                case ClaseEntrenador.Sprite.Data.ID: serializador = ClaseEntrenador.Sprite.Data.Serializador; break;
                case ClaseEntrenador.Sprite.Paleta.ID: serializador = ClaseEntrenador.Sprite.Paleta.Serializador; break;
                case Entrenador.ID: serializador = Entrenador.Serializador; break;
                case EquipoPokemonEntrenador.ID: serializador = EquipoPokemonEntrenador.Serializador; break;

                case Objeto.Datos.ID: serializador = Objeto.Datos.Serializador; break;
                case Objeto.Sprite.ID: serializador = Objeto.Sprite.Serializador; break;
                case ObjetoCompleto.ID: serializador = ObjetoCompleto.Serializador; break;

                case PokeballBatalla.ID: serializador = PokeballBatalla.Serializador; break;

                case PokemonErrante.Ruta.ID: serializador = PokemonErrante.Ruta.Serializador; break;

                case Mini.Sprite.ID: serializador = Mini.Sprite.Serializador; break;
                case Mini.Paletas.ID: serializador = Mini.Paletas.Serializador; break;

                case AtaqueCompleto.ID: serializador = AtaqueCompleto.Serializador; break;
                case Ataque.Concursos.ID: serializador = Ataque.Concursos.Serializador; break;
                case Ataque.Datos.ID: serializador = Ataque.Datos.Serializador; break;
                case Ataque.Descripcion.ID: serializador = Ataque.Descripcion.Serializador; break;
                case Ataque.Nombre.ID: serializador = Ataque.Nombre.Serializador; break;

                case PokemonCompleto.ID: serializador = PokemonCompleto.Serializador; break;
                case Pokemon.AtaquesAprendidos.ID: serializador = Pokemon.AtaquesAprendidos.Serializador; break;
                case Pokemon.Descripcion.ID: serializador = Pokemon.Descripcion.Serializador; break;
                case Pokemon.Huella.ID: serializador = Pokemon.Huella.Serializador; break;
                case Pokemon.Nombre.ID: serializador = Pokemon.Nombre.Serializador; break;
                case Pokemon.OrdenLocal.ID: serializador = Pokemon.OrdenLocal.Serializador; break;
                case Pokemon.OrdenNacional.ID: serializador = Pokemon.OrdenNacional.Serializador; break;
                case Pokemon.SpritesCompleto.ID: serializador = Pokemon.SpritesCompleto.Serializador; break;
                case Pokemon.Sprite.Frontales.ID: serializador = Pokemon.Sprite.Frontales.Serializador; break;
                case Pokemon.Sprite.PaletaNormal.ID: serializador = Pokemon.Sprite.PaletaNormal.Serializador; break;
                case Pokemon.Sprite.PaletaShiny.ID: serializador = Pokemon.Sprite.PaletaShiny.Serializador; break;
                case Pokemon.Sprite.Traseros.ID: serializador = Pokemon.Sprite.Traseros.Serializador; break;
                case Pokemon.Stats.ID: serializador = Pokemon.Stats.Serializador; break;
                case Pokemon.TipoCompleto.ID: serializador = Pokemon.TipoCompleto.Serializador; break;
                case Pokemon.Tipo.Nombre.ID: serializador = Pokemon.Tipo.Nombre.Serializador; break;

                    //si se añaden más hay que ponerlos!
            }
            return serializador;
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
    public class RomBaseNoCargadaException : Exception { }
    public class PaqueteNoCargadoException : Exception { }
}
