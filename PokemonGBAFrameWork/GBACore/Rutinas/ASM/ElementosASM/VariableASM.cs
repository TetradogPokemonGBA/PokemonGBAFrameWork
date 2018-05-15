using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
    public class VariableASM : ElementoASM
    {
        string tipo;
        string valor;
        public VariableASM(string nombre = null, string descripcion = "") : base(nombre, descripcion)
        {
        }

        public string Tipo { get => tipo; set => tipo = value; }
        public string Valor { get => valor; set => valor = value; }

        public override string GetString(Edicion edicion)
        {
            return Nombre + ":" + "." + Tipo + " " + Valor;
        }
    }
}
