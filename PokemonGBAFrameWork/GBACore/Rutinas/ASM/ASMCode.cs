using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.ASM;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
   public class ASMCode
    {
        Llista<InstruccionASM> instrucciones;
        Llista<FuncionASM> funciones;
        Llista<VariableASM> variables;

        public string GetString(Edicion edicion)
        {
            StringBuilder str = new StringBuilder();
            //.text,.align 2,etc...
            for (int i = 0; i < instrucciones.Count; i++)
                str.AppendLine(instrucciones[i].GetString(edicion));
            //funciones
            for (int i = 0; i < funciones.Count; i++)
                str.AppendLine(funciones[i].GetString(edicion));
            //variables
            for (int i = 0; i < variables.Count; i++)
                str.AppendLine(variables[i].GetString(edicion));

            return str.ToString();

        }
        public byte[] GetBytes(Edicion edicion)
        {
            List<byte[]> lstBytes = new List<byte[]>();
            byte[] bytes= { };

            for (int i = 0; i < funciones.Count; i++)
                lstBytes.Add(funciones[i].GetBytes(edicion,instrucciones,funciones,variables));
         

            return bytes.AddArray(lstBytes.ToArray());

        }
    }
}
