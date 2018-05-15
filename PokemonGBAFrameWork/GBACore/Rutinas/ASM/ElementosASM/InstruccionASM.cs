using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
    public abstract class InstruccionASM : ElementoASM
    {
        public InstruccionASM(string nombre = null, string descripcion = "") : base(nombre, descripcion)
        {
        }

        public override string GetString(Edicion edicion)
        {
            return "." + Nombre;
        }
    }
}
