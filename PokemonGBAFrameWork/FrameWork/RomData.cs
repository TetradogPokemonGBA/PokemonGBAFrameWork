using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Tiene todas las cosas de la rom por grupos.//se tiene que acabar...
    /// </summary>
  public  class RomData
    {
        Llista<Habilidad> habilidades;
        Llista<Tipo> tipos;
        Llista<Objeto> objetos;
        Llista<Pokemon> pokedex;
        Edicion edicion;
        CompilacionRom.Compilacion compilacion;
        RomGBA rom;
        Entrenadores entrenadoresClasss;
        public RomData(RomGBA rom,IEnumerable<Habilidad> habilidades, IEnumerable<Tipo> tipos, IEnumerable<Objeto> objetos, IEnumerable<Pokemon> pokedex,IEnumerable<Entrenador> entrenadores, Entrenadores spritesEntrenadores, Edicion edicion,CompilacionRom.Compilacion compilacion):this()
        {
            RomGBA = rom;
            this.habilidades.AddRange(habilidades);
            this.tipos.AddRange(tipos);
            this.objetos.AddRange(objetos);
            this.pokedex.AddRange(pokedex);
            this.Entrenadores.AddRange(entrenadores);
            this.edicion = edicion;
            this.compilacion = compilacion;
            this.entrenadoresClasss = spritesEntrenadores;

        }
        public RomData(string pathGba):this(new RomGBA(pathGba))
        { }
        public RomData()
        {
            habilidades = new Llista<Habilidad>();
            tipos = new Llista<Tipo>();
            objetos = new Llista<Objeto>();
            pokedex = new Llista<Pokemon>();
            edicion = new Edicion("","",'o');
            Entrenadores = new Llista<Entrenador>();
        }

        public RomData(RomGBA rom):this()
        {
            RomGBA = rom;
            Edicion = Edicion.GetEdicion(rom);
            Compilacion = CompilacionRom.GetCompilacion(rom, edicion);
            this.habilidades.AddRange(Habilidad.GetHabilidades(rom, edicion, compilacion));
            this.tipos.AddRange(Tipo.GetTipos(rom, edicion, compilacion));
            this.objetos.AddRange(Objeto.GetObjetos(rom, edicion, compilacion));
            this.pokedex.AddRange(Pokemon.GetPokemons(rom, edicion, compilacion));
            this.Entrenadores.AddRange(Entrenador.GetEntrenadores(this));
            EntrenadoresClases = PokemonGBAFrameWork.Entrenadores.GetEntrenadoresClases(this);

        }

        public Llista<Entrenador> Entrenadores
        { get; private set; }
        public Llista<Habilidad> Habilidades
        {
            get
            {
                return habilidades;
            }

           private set
            {
                habilidades = value;
            }
        }

        public Llista<Tipo> Tipos
        {
            get
            {
                return tipos;
            }

            private set
            {
                tipos = value;
            }
        }

        public Llista<Objeto> Objetos
        {
            get
            {
                return objetos;
            }

            private set
            {
                objetos = value;
            }
        }

        public Llista<Pokemon> Pokedex
        {
            get
            {
                return pokedex;
            }

            private set
            {
                pokedex = value;
            }
        }

        public Edicion Edicion
        {
            get
            {
                return edicion;
            }

          private  set
            {
                edicion = value;
            }
        }

        public CompilacionRom.Compilacion Compilacion
        {
            get
            {
                return compilacion;
            }

            set
            {
                compilacion = value;
            }
        }

        public RomGBA RomGBA { get;  set; }

        public Entrenadores EntrenadoresClases
        {
            get
            {
                return entrenadoresClasss;
            }

            set
            {
                if (value == null) throw new ArgumentNullException();
                entrenadoresClasss = value;
            }
        }

    

        public void SetRomData()
        {
            SetRomData(this);
        }
        public void GetRomData()
        {
            RomData romLoaded = new RomData(RomGBA);
            this.Compilacion = romLoaded.Compilacion;
            this.Edicion = romLoaded.Edicion;
            habilidades.Clear();
            habilidades.AddRange(romLoaded.habilidades);
            pokedex.Clear();
            pokedex.AddRange(romLoaded.pokedex);
            objetos.Clear();
            objetos.AddRange(romLoaded.objetos);
            tipos.Clear();
            tipos.AddRange(romLoaded.tipos);
        }
        public static void SetRomData(RomData romData)
        {
            Habilidad.SetHabilidades(romData.RomGBA, romData.Habilidades);
            Tipo.SetTipos(romData.RomGBA, romData.Tipos);
            Objeto.SetObjetos(romData.RomGBA, romData.Objetos);
            Pokemon.SetPokedex(romData.RomGBA, romData.Pokedex);
            Edicion.SetEdicion(romData.RomGBA, romData.Edicion);
            PokemonGBAFrameWork.Entrenadores.SetSpritesEntrenadores(romData);
            Entrenador.SetEntrenadores(romData);
        }
    }
}
