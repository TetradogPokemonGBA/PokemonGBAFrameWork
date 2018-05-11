using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
    public class FuncionASM : ElementoASM
    {

        public FuncionASM(string nombre=null) : base(nombre)
        {
            Comandos = new Llista<ComandoASM>();
        }

        public Llista<ComandoASM> Comandos { get; private set; }
        public byte[] GetBytes(Edicion edicion)
        {
            if (edicion == null)
                edicion = Edicion.Desconocida;

            List<byte[]> bytesComandos = new List<byte[]>();
            byte[] bytes;
            int lenght = 0;
            int offset = 0;

            for (int i = 0; i < Comandos.Count; i++)
                bytesComandos.Add(Comandos[i].GetBytes(edicion));
            for (int i = 0; i < bytesComandos.Count; i++)
                lenght += bytesComandos[i].Length;
            bytes = new byte[lenght];
            for (int i = 0; i < bytesComandos.Count; i++)
            {
                bytes.SetArray(offset, bytesComandos[i]);
                offset += bytesComandos[i].Length;
            }
            return bytes;
        }
        public string GetString(Edicion edicion)
        {
            StringBuilder str = new StringBuilder();

            str.Append(Nombre);
            str.AppendLine(":");

            for (int i = 0; i < Comandos.Count; i++)
                str.AppendLine(Comandos[i].GetString(edicion));
            return str.ToString();
        }
    }
}
