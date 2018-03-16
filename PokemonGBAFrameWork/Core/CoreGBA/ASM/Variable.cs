/*
 * Creado por SharpDevelop.
 * Usuario: tetradog
 * Fecha: 12/12/2017
 * Licencia GNU v3
 */
using System;

using Gabriel.Cat.S.Utilitats;

namespace PokemonGBAFrameWork.Rutinas
{
	/// <summary>
	/// Description of Variable.
	/// </summary>
	public class Variable
	{
		string nombre;
		LlistaOrdenada<Compilacion,LlistaOrdenada<Edicion,string>> diccionario;
		public Variable(string nombre = "nombreVariable")
		{
			diccionario = new LlistaOrdenada<Compilacion, LlistaOrdenada<Edicion, string>>();
			Nombre = nombre;
		}

		public string Nombre {
			get {
				return nombre;
			}
			set {
				nombre = value;
			}
		}
		public LlistaOrdenada<Compilacion, LlistaOrdenada<Edicion, string>> Diccionario {
			get {
				return diccionario;
			}
		}

		public void Add(Compilacion compilacion, Edicion edicion, string valor)
		{
			if (!diccionario.ContainsKey(compilacion))
				diccionario.Add(compilacion, new LlistaOrdenada<Edicion, string>());
			diccionario[compilacion].AddOrReplace(edicion, valor);
		}
		/// <summary>
		/// Elimina del diccionario la entrada si existe
		/// </summary>
		/// <param name="compilacion"></param>
		/// <param name="edicion"></param>
		/// <returns>true si existia</returns>
		public bool Remove(Compilacion compilacion, Edicion edicion)
		{
			bool removed;
			if (diccionario.ContainsKey(compilacion)) {
				removed=diccionario[compilacion].Remove(edicion);
			}else removed=false;
			
			return removed;
		}
		//depende de como se quira :D
		public string this[Compilacion compilacion, Edicion edicion] {
			get {
				return diccionario[compilacion][edicion];
			}
		}
		public string this[Edicion edicion, Compilacion compilacion] {
			get {
				return diccionario[compilacion][edicion];
			}
		}
	}
}
