using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.ComandosScript;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PokemonGBAFramework.Core.StringToScript
{
    public static class XSEReader
    {
        public static readonly string[] ComentariosUnaLinea = { "//", "#" };

 
        class BloqueOrg
        {
            public enum TipoOrg
            {
                String, Movement, Script,Tienda//#raw word 0xFFFF acaba en #raw word 0x0
            }
            public TipoOrg Tipo { get; set; }
            public string Id { get; set; }
            public Script Script { get; set; }
            public BloqueString Texto { get; set; }
            public BloqueMovimiento Movimiento { get; set; }
            public BloqueTienda Tienda { get; set; }
        }
        static Comando GetCommand(TwoKeysList<string, int, BloqueOrg> dicScripts,params string[] camposComando)
        {
            Comando comando;
            If1 if1;
            MsgBox msgbox;
            Type commandType;
            switch (camposComando[0])
            {
                case "if":
                    //if 0xCondicion call/goto 0xOffset/etiqueta
                    if1 = new If1();
                    if1.Condicion=byte.Parse(camposComando[1].Contains('x') ?camposComando[1].Split('x')[1]:camposComando[1]);
                    if1.Script = dicScripts[camposComando[3]].Script;
                    comando = if1;

                    break;
                case "msgbox":
                    //msgbox etiqueta/offset 0xTipo
                    msgbox = new MsgBox();
                    msgbox.Texto = dicScripts[camposComando[1]].Texto;
                    msgbox.Tipo=(MsgBox.MsgBoxTipo) byte.Parse(camposComando[1].Contains('x') ? ((int)(Hex)camposComando[1].Split('x')[1]).ToString() : camposComando[1]);
                    comando = msgbox;
                    break;
                default:

                    commandType = Comando.DicTypes[camposComando[0]];
                    comando = (Comando)Activator.CreateInstance(commandType);
                    EndLoadCommand(comando, dicScripts, camposComando);
                    break;

            }
            
            return comando;

        }
         static void EndLoadCommand(Comando comando, TwoKeysList<string, int, BloqueOrg> dicScripts,string[] camposComandoConId)
        {
            //falta testing			
            
            object obj;
            string aux = "";
            int pos = 1;
            List<Propiedad> propiedades = comando.GetPropiedades();
            try
            {
                for (int j = 0; j < propiedades.Count; j++)
                    if (propiedades[j].Info.Uso.HasFlag(UsoPropiedad.Set)) //uso las propiedades con SET 
                    {
                        aux = camposComandoConId[pos].Contains("x") ? ((int)(Hex)camposComandoConId[pos].Split('x')[1]).ToString() : camposComandoConId[pos];
                        switch (propiedades[j].Info.Tipo.Name)
                        {
                            case "byte":
                            case nameof(Byte):
                                obj = byte.Parse(aux);
                                break;
                            case nameof(Script):
                                obj = dicScripts[aux].Script;
                                break;
                            case nameof(BloqueMovimiento):
                                obj = dicScripts[aux].Movimiento;
                                break;
                            case nameof(BloqueTienda):
                                obj = dicScripts[aux].Tienda ;
                                break;
                            case nameof(BloqueString):
                                obj = dicScripts[aux].Texto;
                                break;
                            case nameof(Word):
                                obj = new Word(ushort.Parse(aux));
                                break;
                            case nameof(DWord):
                                obj = new DWord(uint.Parse(aux));
                                break;
                            case nameof(OffsetRom):
                                obj = new OffsetRom(int.Parse(aux));
                                break;
                            default:
                                obj = default;
                                break;
                        }
                        comando.SetProperty(propiedades[j].Info.Nombre, obj);
                        pos++;
                    }
            }
            catch
            {
                throw new Exception($"No se ha encontrado '{aux}'");
            }
        }

        public static string Normalitze(this string comando)
        {
            int inicioComentario;

            comando = comando.Trim();

            while (comando.Contains("  "))
                comando = comando.Replace("  ", " ");

            comando = comando.Trim('\r', '\n', '\t');


            if (comando.Length > 0 && !comando.StartsWith(ComentariosUnaLinea))
            {
                inicioComentario = comando.IndexOf("/*");
                if (inicioComentario >= 0)
                {
                    comando = comando.Remove(inicioComentario, inicioComentario - comando.IndexOf("*/"));
                }
                for (int k = 0; k < ComentariosUnaLinea.Length; k++)
                {
                    inicioComentario = comando.IndexOf(ComentariosUnaLinea[k]);
                    if (inicioComentario >= 0)
                    {
                        comando = comando.Remove(inicioComentario);
                    }
                }
            }
            return comando;
        }

        public static IList<Script> GetFromXSE(this string comandosConEnter)
        {
            return GetFromXSE(comandosConEnter.Split('\n'));
        }
        public static IList<Script> GetFromXSE(this IList<string> comandos)
        {
            string[] camposComando;
            string comandoActual;
            TwoKeysList<string,int,BloqueOrg> scripts = new TwoKeysList<string, int, BloqueOrg>();
            BloqueOrg org=default;
            SortedList<string, BloqueOrg> dicBloques = new SortedList<string, BloqueOrg>();

            //cargo todas las etiquetas de los scripts
            for(int i = 0; i < comandos.Count; i++)
            {
                comandoActual = Normalitze(comandos[i]);
                if (!string.IsNullOrEmpty(comandoActual))
                {
                    if (comandoActual.Contains(' '))
                        camposComando = comandoActual.Split(' ');
                    else camposComando = new string[] { comandoActual };

                    if (camposComando[0] == "#org")
                    {
                        if(!dicBloques.ContainsKey(camposComando[1]))
                        {//script,texto,movimientos,tienda?,
                            org = new BloqueOrg() { Id = camposComando[1] };
                            dicBloques.Add(camposComando[1], org);
                        }
                    }else if (!Equals(org, default))
                    {
                        //determino el tipo
                        if (comandoActual[0] == '=')
                            org.Tipo = BloqueOrg.TipoOrg.String;
                        else if (comandoActual.Contains("#raw word"))
                            org.Tipo = BloqueOrg.TipoOrg.Tienda;
                        else if (comandoActual.Contains("#raw"))
                            org.Tipo = BloqueOrg.TipoOrg.Movement;
                        else org.Tipo = BloqueOrg.TipoOrg.Script;

                        org = default;//para saltar las proximas lineas
                    }

                }

            }
            for (int i = 0; i < comandos.Count; i++)
            {
                comandoActual = Normalitze(comandos[i]);
                if (!string.IsNullOrEmpty(comandoActual))
                {
                    if (comandoActual.Contains(' '))
                        camposComando = comandoActual.Split(' ');
                    else camposComando = new string[] { comandoActual };

                    if (camposComando[0] != "#org")
                    {
                        if (Equals(org, default))
                        {
                            throw new ScriptXSEMalFormadoException(comandos[i]);
                        }

                        switch (org.Tipo)
                        {
                            case BloqueOrg.TipoOrg.Script:
                                if (Comando.DicTypes.ContainsKey(camposComando[0]))
                                {
                                    org.Script.ComandosScript.Add(GetCommand(scripts, camposComando));

                                }
                                else if (camposComando[0] != "return" && camposComando[0] != "end")
                                {
                                    throw new ScriptXSEMalFormadoException(comandos[i]);
                                }
                                break;
                            case BloqueOrg.TipoOrg.Movement://#raw 0xMOVE
                                org.Movimiento.List.Add(byte.Parse(camposComando[1].Contains('x') ? camposComando[1].Split('x')[1] : camposComando[1]));

                                break;
                            case BloqueOrg.TipoOrg.String://= Texto
                                org.Texto.Texto +=comandoActual.StartsWith("=")? comandoActual.Substring(1).Trim():comandoActual;
                                break;
                            case BloqueOrg.TipoOrg.Tienda:
                                org.Tienda.Objetos.Add(new Word( camposComando[2].Contains('x') ? ((Hex)camposComando[2].Split('x')[1]) : int.Parse(camposComando[2]) ) );
                                break;
                        }

                    }
                    else { 
                        org = scripts[camposComando[1]];
                        switch(org.Tipo)
                        {
                            case BloqueOrg.TipoOrg.Script:org.Script = new Script();break;
                            case BloqueOrg.TipoOrg.Movement:org.Movimiento = new BloqueMovimiento();break;
                            case BloqueOrg.TipoOrg.String: org.Texto = new BloqueString();break;
                            case BloqueOrg.TipoOrg.Tienda:org.Tienda = new BloqueTienda();break;
                        }
                    }
                    
                }

            }
            return scripts.Values.Filtra((o)=>o.Tipo== BloqueOrg.TipoOrg.Script).Select((o)=>o.Script).ToArray();
        }
  


    }
}
