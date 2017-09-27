/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 13/09/2017
 * Hora: 10:35
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
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
		public override string ToString()
		{
			return ToString("");
		}
		public string ToString(string app)
		{
			StringBuilder str=new StringBuilder();
			//comunidad\n
			//\tusuario:\n
			//\t\tquehahecho\n
			str.Append(app);
			str.Append("\n");
			foreach(KeyValuePair<string,LlistaOrdenadaPerGrups<string,string>> comunidad in dicCreditos)
			{
				if(comunidad.Value.Count()>0){
					str.Append("\t");
					str.Append(comunidad.Key);
					str.Append("\n");
					foreach(KeyValuePair<string,string[]> usuario in comunidad.Value)
					{
						if(usuario.Value.Length>0){
							str.Append("\t\t");
							str.Append(usuario.Key);
							str.Append("\n");
							for(int i=0;i<usuario.Value.Length;i++)
							{
								str.Append("\t\t\t");
								str.Append(usuario.Value[i]);
								str.Append("\n");
							}
						}
					}
				}
			}
			return str.ToString();
		}
	}
}
