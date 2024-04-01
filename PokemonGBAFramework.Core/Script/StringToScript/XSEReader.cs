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
    public class BloqueOrg
    {
        private TipoOrg tipo;

        public enum TipoOrg
        {
            String, Braille, Movement, Script, Tienda//#raw word 0xFFFF acaba en #raw word 0x0
        }
        public TipoOrg Tipo {
            get => tipo; 
            set { 
                tipo = value;
                switch (tipo)
                {
                    case BloqueOrg.TipoOrg.Script: Script = new Script(); break;
                    case BloqueOrg.TipoOrg.Movement: Movimiento = new BloqueMovimiento(); break;
                    case BloqueOrg.TipoOrg.String: Texto = new BloqueString(); break;
                    case BloqueOrg.TipoOrg.Tienda: Tienda = new BloqueTienda(); break;
                }

            } }
        public string Id { get; set; }
        public Script Script { get; set; }
        public BloqueString Texto { get; set; }
        public BloqueMovimiento Movimiento { get; set; }
        public BloqueTienda Tienda { get; set; }
    }
    public static class XSEReader
    {
        public static readonly string[] ComentariosUnaLinea = { "//", "#" };

 
  
        static Comando GetCommand(SortedList<string, BloqueOrg> dicScripts,params string[] camposComando)
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
                    msgbox.Tipo=(MsgBox.MsgBoxTipo) byte.Parse(camposComando[2].Contains("0x") ? ((int)(Hex)camposComando[2].Split('x')[1]).ToString() : camposComando[2]);
                    comando = msgbox;
                    break;
                default:

                    commandType = Comando.DicTypes[camposComando[0]].GetType();
                    comando = (Comando)Activator.CreateInstance(commandType);
                    EndLoadCommand(comando, dicScripts, camposComando);
                    break;

            }
            
            return comando;

        }
         static void EndLoadCommand(Comando comando, SortedList<string, BloqueOrg> dicScripts,string[] camposComandoConId)
        {
            //falta testing			
            
            object obj;
            string aux = "";
            int pos = 1;
            IList<Propiedad> propiedades = comando.GetParams();
            try
            {
                for (int j = 0; j < propiedades.Count; j++)
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
                            obj = dicScripts[aux].Tienda;
                            break;
                        case nameof(BloqueString):
                            obj = dicScripts[aux].Texto;
                            break;
                        case nameof(BloqueBraille):
                            obj = new BloqueBraille() { Texto = dicScripts[aux].Texto };
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
                    propiedades[j].Value = obj;
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
            return comando.ToLower();
        }

        public static IList<Script> GetFromXSE(this string comandosConEnter)
        {
            return GetFromXSE(comandosConEnter.Split('\n'));
        }
        public static IList<Script> GetFromXSE(this IList<string> comandos)
        {
            string[] camposComando;
            string comandoActual;
            SortedList<string, BloqueOrg> scripts = new SortedList<string, BloqueOrg>();
            BloqueOrg org=default;

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
                        if(!scripts.ContainsKey(camposComando[1]))
                        {//script,texto,movimientos,tienda?,
                            org = new BloqueOrg() { Id = camposComando[1] };
                            scripts.Add(camposComando[1], org);
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
                                    org.Script.Comandos.Add(GetCommand(scripts, camposComando));

                                }
                                else if (camposComando[0] != "return" && camposComando[0] != "end")
                                {
                                    throw new ScriptXSEMalFormadoException(comandos[i]);
                                }
                                break;
                            case BloqueOrg.TipoOrg.Movement://#raw 0xMOVE
                                org.Movimiento.List.Add(byte.Parse(camposComando[1].Contains('x') ? ((int)(Hex)camposComando[1].Split('x')[1]).ToString() : camposComando[1]));

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
                       
                    }
                    
                }

            }
            return scripts.Values.Where((o)=>o.Tipo== BloqueOrg.TipoOrg.Script).Select((o)=>o.Script).ToArray();
        }
  


    }

    public static class XSEWriter
    {
        public static string ToXSEOrdenadoPorBloques(this Script script)
        {
            return String.Join("\n\r", script.ToXSE(true).Ordena().Select((b) => b.Value));
        }
        public static List<KeyValuePair<BloqueOrg.TipoOrg, string>> ToXSE(this Script script,bool todosLosBloquesAnidados=false,bool addFinishEndOrReturn=true)
        {
            List<KeyValuePair<BloqueOrg.TipoOrg, string>> bloques = new List<KeyValuePair<BloqueOrg.TipoOrg, string>>();
            StringBuilder str = new StringBuilder();
            str.AppendLine($"#org @Script{(Hex)script.IdUnicoTemp}");
            for (int i = 0; i < script.Comandos.Count; i++)
                str.AppendLine(script.Comandos[i].ToXSE());

            if(addFinishEndOrReturn)
            {
                if (script.IsEnd)
                    str.AppendLine("End");
                else str.AppendLine("Return");
            }
            bloques.Add(new KeyValuePair<BloqueOrg.TipoOrg, string>(BloqueOrg.TipoOrg.Script, str.ToString()));
            if (todosLosBloquesAnidados)
            {
                foreach (Script scriptAnidado in script.GetScripts())
                {
                    bloques.AddRange(scriptAnidado.ToXSE(todosLosBloquesAnidados, addFinishEndOrReturn));
                }
                foreach (BloqueString textos in script.GetStrings())
                {
                    bloques.Add(new KeyValuePair<BloqueOrg.TipoOrg, string>(BloqueOrg.TipoOrg.String, textos.ToXSE()));
                }
                foreach (BloqueTienda tiendas in script.GetTiendas())
                {
                    bloques.Add(new KeyValuePair<BloqueOrg.TipoOrg, string>(BloqueOrg.TipoOrg.Tienda, tiendas.ToXSE()));
                }
                foreach (BloqueMovimiento movimientos in script.GetMovimientos())
                {
                    bloques.Add(new KeyValuePair<BloqueOrg.TipoOrg, string>(BloqueOrg.TipoOrg.Movement, movimientos.ToXSE()));
                }
                foreach (BloqueBraille braille in script.GetBrailles())
                {
                    bloques.Add(new KeyValuePair<BloqueOrg.TipoOrg, string>(BloqueOrg.TipoOrg.Braille, braille.ToXSE()));
                }
                
            }
            return bloques;

        }
        public static List<KeyValuePair<BloqueOrg.TipoOrg, string>> Ordena(this IList<KeyValuePair<BloqueOrg.TipoOrg,string>> listaBloques)
        {
            List<KeyValuePair<BloqueOrg.TipoOrg, string>> lst = new List<KeyValuePair<BloqueOrg.TipoOrg, string>>();
            foreach(BloqueOrg.TipoOrg org in Enum.GetValues(typeof(BloqueOrg.TipoOrg)))
            {
                for (int i = 0; i < listaBloques.Count; i++)
                    if (listaBloques[i].Key.Equals(org))
                        lst.Add(listaBloques[i]);
            }
            lst.Invertir();
            return lst;
        }
        public static string ToXSE(this BloqueString texto)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"#org @Texto{(Hex)texto.IdUnicoTemp}");
            str.Append($"= {texto.Texto}");
            return str.ToString();
        }
        public static string ToXSE(this BloqueBraille braille)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine($"#org @Braille{(Hex)braille.IdUnicoTemp}");
            str.Append($"= {braille.Texto}");
            return str.ToString();
        }
        public static string ToXSE(this BloqueTienda tienda)
        {
            StringBuilder str = new StringBuilder();

            tienda.PonerFin();
            str.AppendLine($"#org @Shop{(Hex)tienda.IdUnicoTemp}");
            for(int i=0;i<tienda.Objetos.Count;i++)
            str.AppendLine($"#raw word 0x{(Hex)tienda.Objetos[i]}");

            return str.ToString();
        }
        public static string ToXSE(this BloqueMovimiento move)
        {
            StringBuilder str = new StringBuilder();

            move.PonMarcaFin();
            str.AppendLine($"#org @Move{(Hex)move.IdUnicoTemp}");
            for (int i = 0; i < move.List.Count; i++)
                str.AppendLine($"#raw 0x{(Hex)move.List[i]}");

            return str.ToString();
        }
        public static string ToXSE(this Comando comando)
        {
            MsgBox msgBox;
            If1 if1;
            StringBuilder str = new StringBuilder();

            switch (comando.IdComando)
            {
                case If1.ID:
                case If2.ID:
                    if1 = comando as If1;
                    str.Append($"if {((Hex)if1.Condicion).ByteString} ");
                    if (if1.Script.IsEnd)
                    {
                        str.Append("goto");
                    }
                    else str.Append("call");
                    str.Append($" @Script{(Hex)if1.Script.IdUnicoTemp}");

                break;
                case MsgBox.ID:
                    msgBox = comando as MsgBox;
                    str.Append($"{msgBox.Nombre} @Texto{((Hex)msgBox.Texto.IdUnicoTemp) } {((Hex) (byte)msgBox.Tipo).ByteString}");

                    break;
                default:
                    str.Append(comando.Nombre);
                    foreach(Propiedad obj in comando.GetParams())
                    {
                        str.Append(" ");
                        switch (obj.Info.Tipo.Name)
                        {
                            case nameof(OffsetRom):
                                str.Append("0x"+obj.Value.ToString());
                                break;
                            case nameof(DWord):
                                str.Append(((Hex)(uint)obj.Value).ByteString);
                                break;
                            case nameof(Word):
                                str.Append(((Hex)(uint)ushort.Parse(obj.Value.ToString())).ByteString);
                                break;
                            case "byte":
                            case nameof(Byte):
                                str.Append(((Hex)int.Parse(obj.Value.ToString())).ByteString);
                                break;
                            case nameof(Script):
                                str.Append("@Script");
                                str.Append((string)(Hex)((Script)obj.Value).IdUnicoTemp);
                                break;
                            case nameof(BloqueMovimiento):
                                str.Append("@Move");
                                str.Append((string)(Hex)((BloqueMovimiento)obj.Value).IdUnicoTemp);
                                break;
                            case nameof(BloqueTienda):
                                str.Append("@Shop");
                                str.Append((string)(Hex)((BloqueTienda)obj.Value).IdUnicoTemp);
                                break;
                            case nameof(BloqueString):
                                str.Append("@Texto");
                                str.Append((string)(Hex)((BloqueString)obj.Value).IdUnicoTemp);
                                break;
                          
                        }
                    }
                    break;
            }
            return str.ToString();

        
        }

    }
}
