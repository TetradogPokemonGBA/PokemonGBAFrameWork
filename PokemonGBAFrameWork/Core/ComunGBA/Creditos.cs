/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 13/09/2017
 * Hora: 10:35
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Creditos.
	/// </summary>
	public class Creditos
	{
		public static readonly string[] Comunidades={"Wahackforo","PokemonCommunity","Github"};
		public const int WAHACKFORO=0;
		public const int POKEMONCOMMUNITY=1;
		public const int GITHUB=2;
		
		LlistaOrdenada<string,LlistaOrdenadaPerGrups<string,string>> dicCreditos;//key1 Comunidad,key2 usuario,value que ha hecho
		public Creditos()
		{
			dicCreditos=new LlistaOrdenada<string,LlistaOrdenadaPerGrups<string,string>>();
			for(int i=0;i<Comunidades.Length;i++)
				dicCreditos.Add(Comunidades[i],new LlistaOrdenadaPerGrups<string, string>());
		}
		public void Add(string comunidad,string usuario,string queHaHecho)
		{
			if(!dicCreditos.ContainsKey(comunidad))
				dicCreditos.Add(comunidad,new LlistaOrdenadaPerGrups<string, string>());
			dicCreditos[comunidad].Add(usuario,queHaHecho);
		}
		
		/// <summary>
		/// Key1 Comunidad,Key2 usuario,Value queHaHeho
		/// </summary>
		public LlistaOrdenada<string, LlistaOrdenadaPerGrups<string, string>> DicCreditos {
			get {
				return dicCreditos;
			}
		}
	}
}
