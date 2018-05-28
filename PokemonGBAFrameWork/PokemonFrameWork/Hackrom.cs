using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Hackrom : Paquete
    {

        public static string PathHackroms;



        static Hackrom()
        {

            try
            {

                if (!Directory.Exists(PathHackroms))
                    Directory.CreateDirectory(PathHackroms);

            }
            catch { }
        }


    }
}
