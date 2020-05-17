using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework.Core.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PokemonGBAFramework.Core.StringToScript
{
    public static class XSEReader
    {
        public static readonly string[] ComentariosUnaLinea = { "//", "#" };


        public static Comando GetCommand(params string[] camposComando)
        {
            Comando comando;
            Type commandType =Comando.DicTypes[camposComando[0]];

            comando = (Comando)Activator.CreateInstance(commandType);
            Load(comando,camposComando);
            return comando;

        }
        public static void Load(Comando comando,string[] camposComandoConId)
        {
            //falta testing			
            string aux;
            object obj;
            int pos = 1;
            List<Propiedad> propiedades = comando.GetPropiedades();
            for (int j = 0; j < propiedades.Count; j++)
                if (propiedades[j].Info.Uso.HasFlag(UsoPropiedad.Set)) //uso las propiedades con SET 
                {
                    aux = camposComandoConId[pos].Contains("x") ? ((int)(Hex)camposComandoConId[pos].Split('x')[1]).ToString() : !camposComandoConId[pos].Contains("@") ? camposComandoConId[pos]:camposComandoConId[pos].Substring(1);
                    switch (propiedades[j].Info.Tipo.Name)
                    {
                        case "byte":
                        case nameof(Byte):
                            obj = byte.Parse(aux);
                            break;
                        case nameof(Script):
                            if (camposComandoConId[pos].Contains("x"))
                                obj = int.Parse(aux);//offset script
                            else obj = aux;//es una etiquetaDinamica
                            break;
                        case nameof(Word):
                            obj = new Word(ushort.Parse(aux));
                            break;
                        case nameof(DWord):
                            obj = new DWord(uint.Parse(aux));
                            break;
                        default:
                            obj = default;
                            break;
                    }
                    comando.SetProperty(propiedades[j].Info.Nombre, obj);
                    pos++;
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
        public static IList<Script> GetFromXSE(this string[] comandos)
        {
            SortedList<int,Script> scripts = new SortedList<int,Script>();
            string comandoActual;
            for(int i = 0; i < comandos.Length; i++)
            {
                comandoActual = Normalitze(comandos[i]);
                if (!string.IsNullOrEmpty(comandoActual))
                {


                }

            }
            return scripts.Values;
        }
        public string GetDeclaracionXSE(string etiqueta, bool addDynamicTag = true)
        {
            return GetDeclaracionXSE(etiqueta, IsEndFinished, addDynamicTag);
        }
        /// <summary>
        /// Obtiene el script en formato XSE
        /// </summary>
        /// <param name="rom"></param>
        /// <param name="etiqueta"></param>
        /// <param name="idEnd"></param>
        /// <returns></returns>
        public static string GetDeclaracion(Script script,string etiqueta, bool? isEnd, bool addDynamicTag = true)
        {

            StringBuilder strSCript = new StringBuilder();
            if (addDynamicTag)
            {
                strSCript.Append("#dynamic ");
                strSCript.AppendLine(Script.OffsetInicioDynamic.ByteString);
            }
            if (!string.IsNullOrEmpty(etiqueta))
            {
                strSCript.Append("#org @");
                strSCript.AppendLine(etiqueta);
            }
            for (int i = 0; i < script.ComandosScript.Count; i++)
            {
                strSCript.AppendLine(script.ComandosScript[i].LineaEjecucionXSE());
            }

            if (isEnd.GetValueOrDefault() || Script.EsUnaFuncionAcabadaEnEndOReturn(script.ComandosScript[script.ComandosScript.Count - 1]).GetValueOrDefault())
            {
                strSCript.Append("end");
            }
            else if (isEnd.HasValue)//si tiene y no ha entrado antes es que es false osea es un return :)
                strSCript.Append("return");

            return strSCript.ToString();

        }
        public string GetAllDeclaracionXSE(RomGba rom, string etiqueta = "Start", bool? isEnd = null, bool addDynamicTag = false)
        {

            return GetAllDeclaracionXSE(rom.Data.Bytes, etiqueta, isEnd, addDynamicTag);

        }
        public string GetAllDeclaracionXSE(byte[] rom, string etiqueta = "Start", bool addDynamicTag = false)
        {
            return GetAllDeclaracionXSE(rom, etiqueta, IsEndFinished, addDynamicTag);
        }
        public string GetAllDeclaracionXSE(byte[] rom, string etiqueta, bool? isEnd, bool addDynamicTag = false)
        {
            IOffsetScript aux;
            LoadPointer loadPointerStr;
            OffsetRom offset;
            string strScript = GetDeclaracionXSE(etiqueta, isEnd, addDynamicTag);
            StringBuilder str = new StringBuilder();

            if (string.IsNullOrEmpty(etiqueta) && OffsetData != default)
                str.AppendLine($"#org {((Hex)(int)OffsetData).ByteString}");

            str.AppendLine(strScript);
            for (int i = 0; i < ComandosScript.Count; i++)
            {

                aux = ComandosScript[i] as IOffsetScript;

                if (aux != default)
                {
                    loadPointerStr = aux as LoadPointer;

                    str.AppendLine();
                    str.AppendLine("----------");
                    offset = aux.Offset;
                    if (loadPointerStr != default)
                    {
                        str.AppendLine($"#org {((Hex)(int)offset).ByteString}");
                        str.Append("= ");
                        str.AppendLine(BloqueString.Get(rom, offset).Texto.Replace("\n", "\\n"));
                    }
                    else
                    {
                        str.AppendLine(new Script(rom, offset).GetAllDeclaracionXSE(rom, null, false));
                    }
                }

            }
            return str.ToString();

        }
        public static IList<Script> FromXSE(FileInfo archivoXSE)
        {
            if (!archivoXSE.Exists)
                throw new System.IO.FileNotFoundException("No se ha podido encontrar el archivo...");
            return FromXSE(System.IO.File.ReadAllLines(archivoXSE.FullName));
        }
        public static IList<Script> FromXSE(IList<string> scriptXSE)
        {//por probar
            if (scriptXSE == null)
                throw new ArgumentNullException("scriptXSE");

            string[] comandoActualCampos;
            string[] defineCampos;
            string lineaLower;
            string auxLinea;
            IScript offsetScript;
            string strOffset;

            Script scriptActual = default;
            SortedList<string, Script> dicScriptsCargados = new SortedList<string, Script>();




            for (int i = scriptXSE.Count - 1; i >= 0; i--)
            {
                //tener en cuenta los define
                lineaLower = scriptXSE[i].ToLower();
                if (lineaLower.Contains("define"))
                {
                    defineCampos = lineaLower.Split(' ');
                    for (int j = i; j >= 0; j--)
                    {
                        if (scriptXSE[j].ToLower().Contains(defineCampos[1]))
                            scriptXSE[j] = scriptXSE[j].ToLower().Replace(defineCampos[1], defineCampos[2]);
                    }
                    scriptXSE.RemoveAt(i);
                }
                else if (lineaLower.Contains("dynamic")) scriptXSE.RemoveAt(i);  //quitar el dinamic
            }


            for (int i = 0; i < scriptXSE.Count; i++)
            {
                try
                {

                    auxLinea =scriptXSE[i].Normalitze();
                    if (!string.IsNullOrEmpty(auxLinea))
                    {
                        if (auxLinea.Contains(" "))
                            comandoActualCampos = auxLinea.Split(' ');
                        else comandoActualCampos = new string[] { auxLinea };

                        if (comandoActualCampos[0] == "#org")
                        {
                            //anidar scripts anidados
                            if (!dicScriptsCargados.ContainsKey(comandoActualCampos[1]))
                            {
                                scriptActual = new Script();
                                dicScriptsCargados.Add(comandoActualCampos[1], scriptActual);
                            }
                            else
                            {
                                scriptActual = dicScriptsCargados[comandoActualCampos[1]];
                            }
                        }

                        else if (Comando.DicTypes.ContainsKey(comandoActualCampos[0]))
                        {
                            scriptActual.ComandosScript.Add(GetCommand(comandoActualCampos));
                        }
                        else if (comandoActualCampos[0] != "return" && comandoActualCampos[0] != "end")
                        {
                            //si no esta hago una excepcion
                            throw new Exception("falta return/end en el script");
                        }
                    }


                }
                catch
                {
                    throw new ScriptXSEMalFormadoException(scriptXSE[i]);
                }
            }
            //ahora vinculo los scripts anidados
            //foreach(var script in dicScriptsCargados)
            //{
            //    for(int i = 0; i < script.Value.ComandosScript.Count; i++)
            //    {
            //        offsetScript = script.Value.ComandosScript[i] as IOffsetScript;
            //        if (offsetScript != null)
            //        {
            //            strOffset = ((Hex)(int)offsetScript.Offset).ByteString.ToLower();
            //            if(dicScriptsCargados.ContainsKey(strOffset))
            //                 offsetScript.Script = dicScriptsCargados[strOffset];
            //        }
            //    }
            //}

            return dicScriptsCargados.Values;
        }



    }
}
