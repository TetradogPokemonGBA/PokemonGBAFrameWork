using Gabriel.Cat;
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
        public RomData(IEnumerable<Habilidad> habilidades, IEnumerable<Tipo> tipos, IEnumerable<Objeto> objetos, IEnumerable<Pokemon> pokedex,Edicion edicion,CompilacionRom.Compilacion compilacion):this()
        {
            this.habilidades.AfegirMolts(habilidades);
            this.tipos.AfegirMolts(tipos);
            this.objetos.AfegirMolts(objetos);
            this.pokedex.AfegirMolts(pokedex);
            this.edicion = edicion;
            this.compilacion = compilacion;
        }

        public RomData()
        {
            habilidades = new Llista<Habilidad>();
            tipos = new Llista<Tipo>();
            objetos = new Llista<Objeto>();
            pokedex = new Llista<Pokemon>();
            edicion = new Edicion("","",'o');
        }

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

        public static RomData GetRomData(RomGBA rom)
        {
            Edicion edicion = Edicion.GetEdicion(rom);
            CompilacionRom.Compilacion compilacion = CompilacionRom.GetCompilacion(rom, edicion);
            return new RomData(Habilidad.GetHabilidades(rom, edicion, compilacion), Tipo.GetTipos(rom, edicion, compilacion), Objeto.GetObjetos(rom, edicion, compilacion), Pokemon.GetPokemons(rom, edicion, compilacion), edicion, compilacion);
        }
        public static void SetRomData(RomGBA rom, RomData romData)
        {
            Habilidad.SetHabilidades(rom, romData.Habilidades);
            Tipo.SetTipos(rom, romData.Tipos);
            Objeto.SetObjetos(rom, romData.Objetos);
            Pokemon.SetPokedex(rom, romData.Pokedex);
            Edicion.SetEdicion(rom,romData.Edicion);

        }
    }
}
