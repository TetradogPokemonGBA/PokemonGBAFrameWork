using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Creditos
    {
        public static readonly string[] Comunidades = { "Wahackforo", "PokemonCommunity", "Github" };
        public const int WAHACKFORO = 0;
        public const int POKEMONCOMMUNITY = 1;
        public const int GITHUB = 2;

        public Creditos()
        {
            DicCreditos = new LlistaOrdenada<string, LlistaOrdenadaPerGrups<string, string>>();
            for (int i = 0; i < Comunidades.Length; i++)
                DicCreditos.Add(Comunidades[i], new LlistaOrdenadaPerGrups<string, string>());
        }
        public void Add(string comunidad, string usuario, string queHaHecho)
        {
            if (!DicCreditos.ContainsKey(comunidad))
                DicCreditos.Add(comunidad, new LlistaOrdenadaPerGrups<string, string>());
            DicCreditos[comunidad].Add(usuario, queHaHecho);
        }

        /// <summary>
        /// Key1 Comunidad,Key2 usuario,Value queHaHeho
        /// </summary>
        public LlistaOrdenada<string, LlistaOrdenadaPerGrups<string, string>> DicCreditos { get; private set; }
        public override string ToString()
        {
            return ToString("");
        }
        public string ToString(string app)
        {
            StringBuilder str = new StringBuilder();
            //comunidad\n
            //\tusuario:\n
            //\t\tquehahecho\n
            str.Append(app);
            str.Append("\n");
            foreach (KeyValuePair<string, LlistaOrdenadaPerGrups<string, string>> comunidad in DicCreditos)
            {
                if (comunidad.Value.Count() > 0)
                {
                    str.Append("\t");
                    str.Append(comunidad.Key);
                    str.Append("\n");
                    foreach (KeyValuePair<string, string[]> usuario in comunidad.Value)
                    {
                        if (usuario.Value.Length > 0)
                        {
                            str.Append("\t\t");
                            str.Append(usuario.Key);
                            str.Append("\n");
                            for (int i = 0; i < usuario.Value.Length; i++)
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
