/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:33
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace PokemonGBAFrameWork
{
	public enum Idioma
	{
		Español = 'S',
		Ingles = 'E'
	}
	public enum AbreviacionCanon
	{
		/// <summary>
		///Abreviación Rubi
		/// </summary>
		AXV,
		/// <summary>
		///Abreviación Zafiro
		/// </summary>
		AXP,
		/// <summary>
		///Abreviación Esmeralda
		/// </summary>
		BPE,
		/// <summary>
		///Abreviación Rojo Fuego
		/// </summary>
		BPR,
		/// <summary>
		///Abreviación Verde Hoja
		/// </summary>
		BPG
			
	}
	
	/// <summary>
	/// Description of EdicionPokemon.
	/// </summary>
	public class EdicionPokemon:Edicion,IComparable
	{
		//Ediciones canon usa
		public static readonly EdicionPokemon RubiUsa = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXV", "POKEMON RUBY"), Idioma.Ingles, AbreviacionCanon.AXV);
		public static readonly EdicionPokemon ZafiroUsa = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "AXP", "POKEMON SAPP"), Idioma.Ingles, AbreviacionCanon.AXP);
		public static readonly EdicionPokemon EsmeraldaUsa = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPE", "POKEMON EMER"), Idioma.Ingles, AbreviacionCanon.BPE);
		public static readonly EdicionPokemon RojoFuegoUsa = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPR", "POKEMON FIRE"), Idioma.Ingles, AbreviacionCanon.BPR);
		public static readonly EdicionPokemon VerdeHojaUsa = new EdicionPokemon(new Edicion((char)Idioma.Ingles, "BPG", "POKEMON LEAF"), Idioma.Ingles, AbreviacionCanon.BPG);
		//Ediciones canon esp
		public static readonly EdicionPokemon RubiEsp = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXV", "POKEMON RUBY"), Idioma.Español, AbreviacionCanon.AXV);
		public static readonly EdicionPokemon ZafiroEsp = new EdicionPokemon(new Edicion((char)Idioma.Español, "AXP", "POKEMON SAPP"), Idioma.Español, AbreviacionCanon.AXP);
		public static readonly EdicionPokemon EsmeraldaEsp = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPE", "POKEMON EMER"), Idioma.Español, AbreviacionCanon.BPE);
		public static readonly EdicionPokemon RojoFuegoEsp = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPR", "POKEMON FIRE"), Idioma.Español, AbreviacionCanon.BPR);
		public static readonly EdicionPokemon VerdeHojaEsp = new EdicionPokemon(new Edicion((char)Idioma.Español, "BPG", "POKEMON LEAF"), Idioma.Español, AbreviacionCanon.BPG);
		//todas las edicionesCanon
		public static readonly EdicionPokemon[] EdicionesCanon = new EdicionPokemon[] {
			RubiUsa,
			ZafiroUsa,
			EsmeraldaUsa,
			RojoFuegoUsa,
			VerdeHojaUsa,
			RubiEsp,
			ZafiroEsp,
			EsmeraldaEsp,
			RojoFuegoEsp,
			VerdeHojaEsp
		};
		Idioma idioma;
		AbreviacionCanon abreviacionCanon;
		
		private EdicionPokemon(Edicion edicion)
			: base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
		{
		}
		private EdicionPokemon(Edicion edicion, Idioma idioma, AbreviacionCanon abreviacionRomCanon)
			: base(edicion.InicialIdioma, edicion.Abreviacion, edicion.NombreCompleto)
		{
			Idioma = idioma;
			AbreviacionRom = abreviacionRomCanon;
		}
		#region Propiedades
		public Idioma Idioma {
			get {
				return idioma;
			}
			private set{ idioma = value; }
		}
		public AbreviacionCanon AbreviacionRom {
			get {
				return abreviacionCanon;
			}
			private set{ abreviacionCanon = value; }
		}
        public bool RegionKanto
        {
            get { return this.AbreviacionRom == AbreviacionCanon.BPG || this.AbreviacionRom == AbreviacionCanon.BPR; }
        }
        public bool RegionHoenn
        {
            get { return !RegionKanto; }
        }
		public bool EstaModificada {
			get{ return (char)idioma != InicialIdioma || Abreviacion != AbreviacionRom.ToString(); }
		}
		#endregion
		#region Overrides
		public override bool Equals(object obj)
		{
			EdicionPokemon other = obj as EdicionPokemon;
			bool equals = other != null;
			if (equals)
				equals = this.idioma == other.idioma && this.abreviacionCanon == other.abreviacionCanon;
			return equals;
		}
		#endregion
		public int CompareTo(object obj)
		{
			EdicionPokemon edicion=obj as EdicionPokemon;
			int compareTo;
			if(edicion!=null)
			{
				compareTo=idioma.CompareTo(edicion.idioma);
				if(compareTo==(int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals)
					compareTo=abreviacionCanon.CompareTo(edicion.abreviacionCanon);
				                          
			}else compareTo=(int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
			return compareTo;
		}
		public static EdicionPokemon GetEdicionPokemon(RomGba rom)
		{
			EdicionPokemon edicionPokemon = new EdicionPokemon(rom.Edicion);
			bool edicionValida;
			AbreviacionCanon abreviacionRom;
			edicionPokemon.Idioma = (Idioma)edicionPokemon.InicialIdioma;
			edicionValida = Enum.TryParse(edicionPokemon.Abreviacion, out abreviacionRom);
			edicionPokemon.AbreviacionRom=abreviacionRom;
			//compruebo que este bien
			if (edicionValida && edicionPokemon.Idioma == Idioma.Español || edicionPokemon.Idioma == Idioma.Ingles){
				edicionValida = ValidaEdicion(rom, edicionPokemon);		
			}
			else
				edicionValida = false;
			
			if (!edicionValida) {
				//si esta mal corrijo los campos Idioma y AbreviacionRom
				
				for (int i = 0; !edicionValida && i < EdicionesCanon.Length; i++) {
					if (ValidaEdicion(rom, EdicionesCanon[i])) { //tengo que saber que edicion y que idioma es
						edicionPokemon.Idioma = EdicionesCanon[i].Idioma;
						edicionPokemon.AbreviacionRom = EdicionesCanon[i].AbreviacionRom;
						edicionValida = true;
					}
					//si no es una edicion canon es que ha sido muy modificada y no se leerla
					if (!edicionValida)
						throw new FormatoRomNoReconocidoException();
					
				}
			}
			return edicionPokemon;
		}

		static bool ValidaEdicion(RomGba rom, EdicionPokemon edicionPokemon)
		{
			bool valida = false;
			//tengo que encontrar si es verdad que sea su edicion...
			//diferenciar idioma,edicion
			try {
				switch (edicionPokemon.AbreviacionRom) {
					case AbreviacionCanon.AXV:
					case AbreviacionCanon.AXP:
						valida = Zona.GetOffsetRom(rom, Ataque.ZonaAnimacion, edicionPokemon, Compilacion.Compilaciones[0]).IsAPointer;
						if (!valida)
							valida = Zona.GetOffsetRom(rom, Ataque.ZonaAnimacion, edicionPokemon, Compilacion.Compilaciones[1]).IsAPointer;
						break;
					case AbreviacionCanon.BPE:
					case AbreviacionCanon.BPR:
					case AbreviacionCanon.BPG:
						valida = Zona.GetOffsetRom(rom, DescripcionPokedex.ZonaDescripcion, edicionPokemon, Compilacion.Compilaciones[0]).IsAPointer;
						if (!valida && edicionPokemon.AbreviacionRom != AbreviacionCanon.BPE)
							valida = Zona.GetOffsetRom(rom, DescripcionPokedex.ZonaDescripcion, edicionPokemon, Compilacion.Compilaciones[1]).IsAPointer;
						break;

				}
				
			} catch (Exception ex){
			}
				return valida;
			
		}
	}
}
