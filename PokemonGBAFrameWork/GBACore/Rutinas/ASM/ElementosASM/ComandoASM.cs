using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Gabriel.Cat.S.Extension;

namespace PokemonGBAFrameWork.ASM
{
    public abstract class ComandoASM
    {
        public abstract string Comando { get; }
        public string Descripcion { get; protected set; }
        public ComandoASM(string descripcion="")
        {
            Descripcion = descripcion;
        }


        public abstract void LeerComando(string lineaActual);
        public abstract void LeerComando(BinaryReader brComandosASM);//cuando lo haga ya veré cuantos necesito poner como lineaActual

        public abstract byte[] GetBytes(Edicion edicion);
        public abstract string GetString(Edicion edicion);
        public static ComandoASM GetComando(StringReader srComandosASM)
        {
            ComandoASM comando;
            GrupoComandosASM grupo;
            string linea;
            string strComando;
            if(Comentario.InicioComentario.Contains(((char)srComandosASM.Peek())+""))
            {
                comando = new Comentario(srComandosASM.ReadLine());
            }
            else
            {
                //es un comando
                linea = srComandosASM.ReadLine();
                strComando = linea.Split(' ')[0];
                switch (strComando)
                {
                    case GrupoComandosASM.COMANDO:
                        grupo = new GrupoComandosASM();
                        grupo.LeerComando(linea.Split("==")[1], srComandosASM);
                        comando = grupo;
                        break;
                        //falta añadir el resto de comandos
                    default:
                        comando = null;
                        break;
                }
            }
            return comando;
        }
       
    }
}
