using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class Hackrom : Paquete
    {
        public static readonly new ElementoBinario Serializador = ElementoBinario.GetSerializador<Hackrom>();
        public static string PathHackroms;
        protected override ElementoBinario ISerializador => Serializador;


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
