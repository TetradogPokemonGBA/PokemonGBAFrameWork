/*
 * User: Pikachu240
 * Código bajo licencia GNU
 */
using System;
using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Zona.
	/// </summary>
	public class Var:IComparable
	{

		string nombre;
		LlistaOrdenada<Compilacion, LlistaOrdenada<Edicion,int>> diccionario;

		public Var(string nombre)
		{
			this.nombre=nombre;
			diccionario=new LlistaOrdenada<Compilacion, LlistaOrdenada<Edicion, int>>();
			for(int i=0;i<CompilacionPokemon.Compilaciones.Length;i++){	
				diccionario.Add(CompilacionPokemon.Compilaciones[i],new LlistaOrdenada<Edicion, int>());
			}
		}
		public Var(Enum enumZona):this(enumZona.ToString())
		{}
		
		public string Nombre {
			get {
              	return nombre;
			}
		}
		#region IClauUnicaPerObjecte implementation
		public IComparable Clau {
			get {
				return Nombre;
			}
		}
		

		#endregion
		public LlistaOrdenada<Compilacion, LlistaOrdenada<Edicion,int>> Diccionario {
			get {
				return diccionario;
			}
		}

		#region Metodos
		public void Add(EdicionPokemon edicion,params int[] zonasCompilacion)
		{ Add(new EdicionPokemon[]{edicion},zonasCompilacion);}
		public void Add( int zonaCompilacion,params EdicionPokemon[] ediciones)
		{ Add(ediciones,new int[]{zonaCompilacion});}
		public void Add(EdicionPokemon[] ediciones,params int[] zonasCompilacion)
		{
			for(int i=0;i<zonasCompilacion.Length;i++){
				for(int j=0;j<ediciones.Length;j++)
					diccionario[CompilacionPokemon.Compilaciones[i]].Add(ediciones[j],zonasCompilacion[i]);
			}
			for(int i=zonasCompilacion.Length;i<CompilacionPokemon.Compilaciones.Length;i++){
				for(int j=0;j<ediciones.Length;j++)
					diccionario[CompilacionPokemon.Compilaciones[i]].Add(ediciones[j],zonasCompilacion[zonasCompilacion.Length-1]);
			}
			
		}
		#endregion
		#region IComparable implementation

		public int CompareTo(object obj)
		{
			Var other=obj as Var;
			int compareTo=other!=null?nombre.CompareTo(other.nombre):(int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
			return compareTo;
		}

		#endregion

		protected static int GetValue(Var variable, Edicion edicion)
		{
            return GetValue(variable, edicion, edicion.Compilacion);
		}
        protected static int GetValue(Var variable, Edicion edicion,Compilacion compilacion)
        {
            if (!variable.Diccionario.ContainsKey(compilacion) || !variable.Diccionario[compilacion].ContainsKey(edicion))
                throw new RomFaltaInvestigacionException();
            return variable.Diccionario[compilacion][edicion];
        }
    }
}
