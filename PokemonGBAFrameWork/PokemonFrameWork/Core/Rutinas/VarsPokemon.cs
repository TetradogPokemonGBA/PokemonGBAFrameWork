/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 05/11/2017
 * Hora: 1:37
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork.Rutinas.C
{
	/// <summary>
	/// Description of VarsPokemon.
	/// </summary>
	public static class VarsPokemon
	{
		public static Creditos Creditos;
		public static LlistaOrdenada<string,Variable> Diccionario;
		
		static VarsPokemon()
		{
			Creditos=new Creditos();//los creditos falta ponerlo bien...de momento pongo de donde lo he sacado 
			Creditos.Add(Creditos.Comunidades[Creditos.GITHUB],"kaisermg5","tener repositorio https://github.com/kaisermg5/simple-pokemon-data-hack");
			Diccionario=new LlistaOrdenada<string, Variable>();
			//añado edicion por edicion las variables
			
			AddFromResource(Compilacion.Compilaciones[0],EdicionPokemon.EsmeraldaUsa,Resources.VarsBPEUSA);
			AddFromResource(Compilacion.Compilaciones[0],EdicionPokemon.RojoFuegoUsa,Resources.VarsBPR10USA);

		}
		static void AddFromResource(Compilacion compilacion,EdicionPokemon edicion,string recurso)
		{
			const int CAMPONOMBRE=0,CAMPOVALOR=2;
			string[] camposVariable;
			string[] variables=recurso.Split('\n');
			for(int i=0;i<variables.Length;i++)
			{
				//nombre = valor
				camposVariable=variables[i].Split(' ');
				Add(compilacion,edicion,camposVariable[CAMPONOMBRE],camposVariable[CAMPOVALOR]);
			}
		}
		public static void Add(Compilacion compilacion,Edicion edicion,string variable,string valor)
		{
			if(!Diccionario.ContainsKey(variable))
				Diccionario.Add(variable,new Variable(variable));
			Diccionario[variable].Add(compilacion,edicion,valor);
		}

	}
}
