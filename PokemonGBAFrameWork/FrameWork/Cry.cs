using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    public class Cry:ObjectAutoId
    {
        public const string IdCry = "203C0000";
        public const string IdGrowl = "303C0000";
        public static readonly byte[] BytesIdCry = (Hex)IdCry;
        public static readonly byte[] BytesIdGrowl = (Hex)IdGrowl;
        const int CANALESMAX = 1;

        static Cry()
        {
            BloqueSonido.DiccionarioHeaderSignificadoCanalesMax.Add(IdCry, "Cry",CANALESMAX);
            BloqueSonido.DiccionarioHeaderSignificadoCanalesMax.Add(IdGrowl, "Growl",CANALESMAX); 
        }

        public static BloqueSonido GetCry(RomGBA rom,int posicion=0)
        {
            return BloqueSonido.GetBloqueSonido(rom, BytesIdCry, posicion);
        }

        public static BloqueSonido GetGrowl(RomGBA rom, int posicion = 0)
        {
            return BloqueSonido.GetBloqueSonido(rom, BytesIdGrowl, posicion);
        }

    }
}
