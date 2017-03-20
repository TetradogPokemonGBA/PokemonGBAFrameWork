/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:11
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Zona.
	/// </summary>
	public class Zona:IComparable,IClauUnicaPerObjecte
	{

		string nombre;
		LlistaOrdenada<Compilacion,LlistaOrdenada<EdicionPokemon,int>> diccionario;

		public Zona(string nombre)
		{
			this.nombre=nombre;
			diccionario=new LlistaOrdenada<Compilacion, LlistaOrdenada<EdicionPokemon, int>>();
			for(int i=0;i<Compilacion.Compilaciones.Length;i++){	
				Diccionario.Add(Compilacion.Compilaciones[i],new LlistaOrdenada<EdicionPokemon, int>());
			}
		}
		public Zona(Enum enumZona):this(enumZona.ToString())
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
		public LlistaOrdenada<Compilacion,LlistaOrdenada<EdicionPokemon,int>> Diccionario {
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
					diccionario[Compilacion.Compilaciones[i]].Add(ediciones[j],zonasCompilacion[i]);
			}
			for(int i=zonasCompilacion.Length;i<Compilacion.Compilaciones.Length;i++){
				for(int j=0;j<ediciones.Length;j++)
					diccionario[Compilacion.Compilaciones[i]].Add(ediciones[j],zonasCompilacion[zonasCompilacion.Length-1]);
			}
			
		}
		#endregion
		#region IComparable implementation

		public int CompareTo(object obj)
		{
			Zona other=obj as Zona;
			int compareTo=other!=null?nombre.CompareTo(other.nombre):(int)Gabriel.Cat.CompareTo.Inferior;
			return compareTo;
		}

		#endregion

		public static OffsetRom GetOffsetRom(RomGba rom, Zona zona, EdicionPokemon edicionPokemon, Compilacion compilacion)
		{
			if(!zona.Diccionario.ContainsKey(compilacion)||!zona.Diccionario[compilacion].ContainsKey(edicionPokemon))
				throw new   RomFaltaInvestigacionException();
			return  new OffsetRom(rom,zona.Diccionario[compilacion][edicionPokemon]);
		}
	}
}
