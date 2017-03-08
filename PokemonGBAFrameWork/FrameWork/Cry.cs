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

        static Cry()
        {
            BloqueSonido.DiccionarioHeaderSignificado.Add(IdCry, "Cry");
            BloqueSonido.DiccionarioHeaderSignificado.Add(IdGrowl, "Growl"); 
        }
    

    }
}
