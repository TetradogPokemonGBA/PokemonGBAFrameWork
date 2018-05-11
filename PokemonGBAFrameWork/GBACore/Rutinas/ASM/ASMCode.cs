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
        Llista<FuncionASM> funciones;

        public string GetString(Edicion edicion)
        {
            StringBuilder str = new StringBuilder();
            //.text,.align 2,etc...
            //funciones
            for (int i = 0; i < funciones.Count; i++)
                str.AppendLine(funciones[i].GetString(edicion));
            //variables

            return str.ToString();

        }
        public byte[] GetBytes(Edicion edicion)
        {
            List<byte[]> lstBytes = new List<byte[]>();
            byte[] bytes= { };
            //.text,.align 2,etc...
            //funciones
            for (int i = 0; i < funciones.Count; i++)
                lstBytes.Add(funciones[i].GetBytes(edicion));
            //variables

            return bytes.AddArray(lstBytes.ToArray());

        }
    }
}
