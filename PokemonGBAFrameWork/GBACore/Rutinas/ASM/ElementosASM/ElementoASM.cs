using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
   public abstract class ElementoASM
    {
        static GenIdInt GenId = new GenIdInt();
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public ElementoASM(string nombre=null,string descripcion="")
        {
            Nombre = nombre==null?"Funcion"+GenId.Siguiente():nombre;
            Descripcion = descripcion;
        }
    }
}
