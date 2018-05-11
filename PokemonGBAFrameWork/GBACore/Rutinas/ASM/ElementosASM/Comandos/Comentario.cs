using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
    public class Comentario : ComandoASM
    {
  
        public static string[] InicioComentario = { "@", "'","/","*" };

        public Comentario(string descripcion = "") : base(descripcion)
        {
        }

        public override string Comando => InicioComentario[0];

        public override byte[] GetBytes(Edicion edicion)
        {
            return new byte[0];
        }

        public override void LeerComando(string comandosASM)
        {
           Descripcion = comandosASM.Substring(1);
        }

        public override void LeerComando(BinaryReader brComandosASM)
        {
            
        }
    }
}
