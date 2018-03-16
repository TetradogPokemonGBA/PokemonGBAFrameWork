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
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of RomData.
	/// </summary>
	public class RomData
	{
		//si se añaden más hacer propiedad y poner en el metodo UnLoad la forma de liberar la memoria
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
		Llista<MiniSprite> minis;
		PaletasMinis paletasMinis;
		LlistaOrdenadaPerGrups<int,AtaquesAprendidos> dicAtaquesPokemon;
		PokemonErrante.Ruta[] rutas;
		//extras
		Mugshots mugshots;
		public RomData(string path)
			: this(new RomGba(path))
		{
		}
		public RomData(FileInfo path)
			: this(new RomGba(path))
		{
		}
		public RomData(RomGba rom)
		{
			Rom = rom;
			rom.UnLoaded+=UnLoad;
			
			//falta ponerlo en su sitio cuando esté acabado
			/*if(Mugshots.EstaActivado(this))
				mugshots=Mugshots.GetMugshots(this);
			else mugshots=new Mugshots();*/
			
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
			set {
				rom = value;
				edicion = EdicionPokemon.GetEdicionPokemon(rom);
				compilacion = Compilacion.GetCompilacion(this);
				
			}
		}

		public Llista<Ataque> Ataques {
			get {
				if (ataques == null)
					ataques = new Llista<Ataque>(Ataque.GetAtaques(this));
				return ataques;
			}
		}

		public Llista<Entrenador> Entrenadores {
			get {
				if (entrenadores == null)
					entrenadores = new Llista<Entrenador>(Entrenador.GetEntrenadores(this));
				return entrenadores;
			}
		}

		public Llista<ClaseEntrenador> ClasesEntrenadores {
			get {
				if (clasesEntrenadores == null)
					clasesEntrenadores = new Llista<ClaseEntrenador>(ClaseEntrenador.GetClasesEntrenador(this));
				return clasesEntrenadores;
			}
		}
		public Llista<Pokemon> Pokedex {
			get {
				if (pokedex == null)
					pokedex = new Llista<Pokemon>(Pokemon.GetPokedex(this));
				return pokedex;
			}
		}

		public Llista<Tipo> Tipos {
			get {
				if (tipos == null)
					tipos = new Llista<Tipo>(Tipo.GetTipos(this));
				return tipos;
			}
		}

		public Llista<Habilidad> Habilidades {
			get {
				if (habilidades == null)
					habilidades = new Llista<Habilidad>(Habilidad.GetHabilidades(this));
				return habilidades;
			}
		}

		public Llista<PokeballBatalla> PokeballsBatalla {
			get {
				if (pokeballsBatalla == null)
					pokeballsBatalla = new Llista<PokeballBatalla>(PokeballBatalla.GetPokeballsBatalla(this));
				return pokeballsBatalla;
			}
		}

		public Llista<Objeto> Objetos {
			get {
				if (objetos == null)
					objetos = new Llista<Objeto>(Objeto.GetObjetos(this));
				return objetos;
			}
		}

		public Llista<MiniSprite> Minis {
			get {
				if(minis==null)
					minis = new Llista<MiniSprite>(MiniSprite.GetMiniSprites(this, paletasMinis));
				return minis;
			}
		}

		public PaletasMinis PaletasMinis {
			get {
				if (paletasMinis == null)
					paletasMinis = PaletasMinis.GetPaletasMinis(this);
				return paletasMinis;
			}
		}
		/// <summary>
		/// Son las rutas donde el pokemon errante salta (si es null es porque falta desarrollar la compatibilidad)
		/// </summary>
		public PokemonErrante.Ruta[] Rutas {
			get {
				if (rutas==null&&PokemonErrante.EsCompatible(this))
					rutas = PokemonErrante.Ruta.GetRutas(this);
				return rutas;
			}
		}
		internal Mugshots Mugshots {
			get {
				
				return mugshots;
			}
		}

		public LlistaOrdenadaPerGrups<int, AtaquesAprendidos> DicAtaquesPokemon {
			get {
				if (dicAtaquesPokemon == null)
					dicAtaquesPokemon = AtaquesAprendidos.GetAtaquesAprendidosDic(this);
				return dicAtaquesPokemon;
			}
		}

		void UnLoad(object sender, EventArgs e)
		{
			UnLoad();
		}

		public void UnLoad()
		{
			rom.UnLoaded-=UnLoad;
			rom.UnLoad();
			rom.UnLoaded+=UnLoad;
			
			compilacion=null;
			edicion=null;

			
			//datos rom
			ataques=null;
			entrenadores=null;
			clasesEntrenadores=null;
			pokedex=null;
			tipos=null;
			habilidades=null;
			pokeballsBatalla=null;
			objetos=null;
			minis=null;
			paletasMinis=null;
			dicAtaquesPokemon=null;
			rutas=null;
			//extras
			mugshots=null;
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
			//Mugshots.SetMugshots(this);
			rom.SaveEdicion();
			rom.Save();
		}
	}
}
