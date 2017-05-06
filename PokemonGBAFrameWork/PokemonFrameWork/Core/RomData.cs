/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of RomData.
	/// </summary>
	public class RomData:ObjectAutoId
	{
		
		Compilacion compilacion;
		EdicionPokemon edicion;
		RomGba rom;
		
		//datos rom
		Llista<Ataque> ataques;
		Llista<Entrenador> entrenadores;
		Llista<ClaseEntrenador> clasesEntrenadores;
		Llista<Pokemon> pokedex;
		Llista<Tipo> tipos;
		Llista<Habilidad> habilidades;
		Llista<PokeballBatalla> pokeballsBatalla;
		Llista<Objeto> objetos;
		//extras
		Mugshots mugshots;
		public RomData(string path):this(new RomGba(path))
		{}
		public RomData(FileInfo path):this(new RomGba(path))
		{}
		public RomData(RomGba rom)
		{
			Rom=rom;
			ataques=new Llista<Ataque>(Ataque.GetAtaques(this));
			entrenadores=new Llista<Entrenador>(Entrenador.GetEntrenadores(this));
			clasesEntrenadores=new Llista<ClaseEntrenador>(ClaseEntrenador.GetClasesEntrenador(this));
			pokedex=new Llista<Pokemon>(Pokemon.GetPokedex(this));
			tipos=new Llista<Tipo>(Tipo.GetTipos(this));
			habilidades=new Llista<Habilidad>(Habilidad.GetHabilidades(this));
			pokeballsBatalla=new Llista<PokeballBatalla>(PokeballBatalla.GetPokeballsBatalla(this));
			objetos=new Llista<Objeto>(Objeto.GetObjetos(this));
			if(Mugshots.EstaActivado(this))
				mugshots=Mugshots.GetMugshots(this);
			else mugshots=new Mugshots();
		}
		
		public Compilacion Compilacion {
			get {
				return compilacion;
			}
		}

		public EdicionPokemon Edicion {
			get {
				return edicion;
			}
		}

		public RomGba Rom {
			get {
				return rom;
			}
			set{
				rom=value;
				edicion=EdicionPokemon.GetEdicionPokemon(rom);
				compilacion=Compilacion.GetCompilacion(this);
				
			}
		}

		public Llista<Ataque> Ataques {
			get {
				return ataques;
			}
		}

		public Llista<Entrenador> Entrenadores {
			get {
				return entrenadores;
			}
		}

		public Llista<ClaseEntrenador> ClasesEntrenadores {
			get {
				return clasesEntrenadores;
			}
		}
		public Llista<Pokemon> Pokedex {
			get {
				return pokedex;
			}
		}

		public Llista<Tipo> Tipos {
			get {
				return tipos;
			}
		}

		public Llista<Habilidad> Habilidades {
			get {
				return habilidades;
			}
		}

		public Llista<PokeballBatalla> PokeballsBatalla {
			get {
				return pokeballsBatalla;
			}
		}

		public Llista<Objeto> Objetos {
			get {
				return objetos;
			}
		}

		public Mugshots Mugshots {
			get {
				return mugshots;
			}
		}

		public void Save()
		{
			Pokemon.SetPokedex(this);
			Ataque.SetAtaques(this);
			Entrenador.SetEntrenadores(this);
			ClaseEntrenador.SetClasesEntrenador(this);
			
			Tipo.SetTipos(this);
			Habilidad.SetHabilidades(this);
			PokeballBatalla.SetPokeballsBatalla(this);
			Objeto.SetObjetos(this);
			Mugshots.SetMugshots(this);
			rom.SaveEdicion();
			rom.Save();
		}
	}
}
