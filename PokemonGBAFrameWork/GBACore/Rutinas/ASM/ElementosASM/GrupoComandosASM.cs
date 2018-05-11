using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFrameWork.ASM
{
    public class GrupoComandosASM:ComandoASM
    {
        public const string COMANDO = "if";
        public GrupoComandosASM(string descripcion = "") : base(descripcion)
        {
            DicComandosPorEdicion = new LlistaOrdenada<string, FuncionASM>();
        }

   
        public LlistaOrdenada<string, FuncionASM> DicComandosPorEdicion { get; private set; }
        public override string Comando { get => COMANDO;}

        public override byte[] GetBytes(Edicion edicion, Llista<InstruccionASM> instrucciones, Llista<FuncionASM> funciones, Llista<VariableASM> variables)
        {
            FuncionASM comandos = DicComandosPorEdicion[edicion.GameCode];
            return comandos.GetBytes(edicion,instrucciones,funciones,variables);
        }
        public override string GetString(Edicion edicion)
        {
            FuncionASM comandos = DicComandosPorEdicion[edicion.GameCode];
            return comandos.GetString(edicion);
        }
        public void SetDescripcion(string descripcion)
        {
            Descripcion = descripcion;
        }
        public override void LeerComando(string lineaActual)
        {
            throw new NotImplementedException();
        }
        public  void LeerComando(string gameCodeInicial,StringReader srComandosASM)
        {
            const string MARCAFIN = "endif";
            string[] lineasInicioIf = { "if", "elif", "else" };
            string linea;
            string gameCode=gameCodeInicial;
            FuncionASM funcionActual=new FuncionASM();
            DicComandosPorEdicion.Add(gameCode, funcionActual);
            do
            {
                linea = srComandosASM.ReadLine().ToLower().Trim();
                if(lineasInicioIf.Contains(linea)||linea==MARCAFIN)
                {
                    //acabo el if actual
                    if(linea!=MARCAFIN)
                    {
                        //empiezo otro
                        if(linea=="else")
                        {
                            //no tiene gamecode
                            gameCode = Edicion.Desconocida.GameCode;

                        }
                        else
                        {
                            //tiene gamecode
                            gameCode = linea.Split("==")[1];
                        }
                        funcionActual = new FuncionASM();
                        DicComandosPorEdicion.Add(gameCode, funcionActual);
                    }
                 
                }
                else if(funcionActual!=null&&!String.IsNullOrEmpty(linea)&&!String.IsNullOrWhiteSpace(linea))
                {
                    funcionActual.Comandos.Add(ComandoASM.GetComando(srComandosASM));
                }
                //sino es un comentario o espacio

            } while (linea != MARCAFIN);
        }

        public override void LeerComando(BinaryReader brComandosASM)
        {
            throw new NotImplementedException();//no se puede leer un grupo de comandos porque en la gba solo hay los comandos para esa edicion.
        }

       
    }
}
