using Gabriel.Cat;
using Gabriel.Cat.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
    public static class PokemonErrante
    {
        public class Ruta
        {
            enum Variables
            {
                PokemonErranteBancoMapaRutaValido,
                PokemonErranteColumnasFilaRuta,
                PokemonErranteOffsetTablaFilasRuta,
                PokemonErranteOffSetRutina1,
                PokemonErranteOffSetRutina2,
                PokemonErranteOffSetRutina3,
            }
            public const int MAXLENGTH = 7;
            public const byte MAXIMODERUTAS = 255;
            static Ruta()
            {
                Zona zonaBancoMapaRutaValido = new Zona(Variables.PokemonErranteBancoMapaRutaValido),
                     zonaColumnasFilaRuta = new Zona(Variables.PokemonErranteColumnasFilaRuta),
                     zonaOffsetTablaFilasRuta = new Zona(Variables.PokemonErranteOffsetTablaFilasRuta),
                     zonaOffSetRutina1 = new Zona(Variables.PokemonErranteOffSetRutina1),
                     zonaOffSetRutina2 = new Zona(Variables.PokemonErranteOffSetRutina2),
                     zonaOffSetRutina3 = new Zona(Variables.PokemonErranteOffSetRutina3);

                zonaBancoMapaRutaValido.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0);
                zonaBancoMapaRutaValido.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 3);

                zonaColumnasFilaRuta.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 6);
                zonaColumnasFilaRuta.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 7);

                zonaOffsetTablaFilasRuta.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0xD5A140);
                zonaOffsetTablaFilasRuta.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x64C685);

                zonaOffSetRutina1.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x161928);
                zonaOffSetRutina1.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x141d6e);

                zonaOffSetRutina2.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1619c6);
                zonaOffSetRutina2.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x141df6);

                zonaOffSetRutina3.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x161a82);
                zonaOffSetRutina3.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x141eae);

                Zona.DiccionarioOffsetsZonas.AddRange(new Zona[] { zonaBancoMapaRutaValido, zonaColumnasFilaRuta, zonaOffsetTablaFilasRuta, zonaOffSetRutina1, zonaOffSetRutina2, zonaOffSetRutina3 });

            }
            public byte[] Rutas { get; private set; }
            public Ruta()
            {
                Rutas = new byte[MAXLENGTH];
            }

            public static void SetRutas(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, IEnumerable<Ruta> rutasDondeAparece)
            {
                Ruta[] rutas = rutasDondeAparece.ToArray();
                int columnas = (int)Zona.GetVariable(rom, Variables.PokemonErranteColumnasFilaRuta, edicion, compilacion);
                byte[] bytesRutas = new byte[rutas.Length * columnas];
                if (rutas.Length==0||rutas.Length > MAXIMODERUTAS) throw new ArgumentOutOfRangeException(); //como maximo 255 rutas
                //borro la tabla anterior
                BloqueBytes.RemoveBytes(rom, Zona.GetVariable(rom, Variables.PokemonErranteOffsetTablaFilasRuta, edicion, compilacion), columnas * rom.Datos[Zona.GetVariable(rom, Variables.PokemonErranteOffSetRutina1, edicion, compilacion)]);
                //pongo cuantas filas hay donde toca
                rom.Datos[Zona.GetVariable(rom, Variables.PokemonErranteOffSetRutina1, edicion, compilacion)] = (byte)rutas.Length;
                rom.Datos[Zona.GetVariable(rom, Variables.PokemonErranteOffSetRutina2, edicion, compilacion)] = (byte)rutas.Length;
                rom.Datos[Zona.GetVariable(rom, Variables.PokemonErranteOffSetRutina3, edicion, compilacion)] = (byte)(rutas.Length-1);//numero de filas-1 en el offset3
                //guardo la nueva tabla //el offset de la tabla tiene que acabar en '0', '4', '8', 'C' 
                unsafe
                {
                    fixed(byte* ptrBytesRutas = bytesRutas)
                    {
                        byte* ptBytesRutas = ptrBytesRutas,ptBytesRuta;
                        for (int i = 0; i < rutas.Length; i++)
                        {
                            fixed(byte* ptrBytesRuta=rutas[i].Rutas)
                            {
                                ptBytesRuta = ptrBytesRuta;   
                                for (int j = 0; j < columnas; j++)
                                {
                                    *ptBytesRutas = *ptBytesRuta;
                                    ptBytesRutas++;
                                    ptBytesRuta++;
                                }
                            }
                        }
                    }
                }
               
                Offset.SetOffset(rom, Zona.GetOffset(rom, Variables.PokemonErranteOffsetTablaFilasRuta, edicion, compilacion), BloqueBytes.SetBytes(rom, bytesRutas,true));
            }
            public static Ruta[] GetRutas(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
            {
                int columnas = (int)Zona.GetVariable(rom, Variables.PokemonErranteColumnasFilaRuta, edicion, compilacion);
                Ruta[] rutas = new Ruta[rom.Datos[Zona.GetVariable(rom, Variables.PokemonErranteOffSetRutina1, edicion, compilacion)]];
                BloqueBytes bloqueDatos = BloqueBytes.GetBytes(rom, Zona.GetVariable(rom, Variables.PokemonErranteOffsetTablaFilasRuta, edicion, compilacion), columnas * rutas.Length);
                for (int i = 0; i < rutas.Length; i++)
                {
                    rutas[i] = new Ruta();
                    for (int j = 0; j < columnas; j++)
                        rutas[i].Rutas[j] = bloqueDatos.Bytes[i * columnas + j];
                }
                return rutas;

            }
        }
        public class Pokemon
        {
            enum Variables
            {

                //script
                PokemonErranteSpecial,
                PokemonErranteVar,
                PokemonErranteVitalidadVar,
                PokemonErranteNivelYEstadoVar,
                PokemonErranteDisponibleVar,
            }
            //por investigar para todas las versiones!!!
            static Pokemon()
            {
                Zona zonaSpecialPokemonErrante = new Zona(Variables.PokemonErranteSpecial),
                zonaPokemonErranteVar = new Zona(Variables.PokemonErranteVar),
                zonaVitalidadVar = new Zona(Variables.PokemonErranteVitalidadVar),
                zonaNivelYEstadoVar = new Zona(Variables.PokemonErranteNivelYEstadoVar),
                zonaDisponibleVar = new Zona(Variables.PokemonErranteDisponibleVar);

                //pongo los datos encontrados


                zonaSpecialPokemonErrante.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x12B);
                zonaSpecialPokemonErrante.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x129);

                zonaPokemonErranteVar.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x4F24);
                zonaPokemonErranteVar.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x506C);

                zonaVitalidadVar.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x4F25);
                zonaVitalidadVar.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x506D);

                zonaNivelYEstadoVar.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x4F26);
                zonaNivelYEstadoVar.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x506E);

                zonaDisponibleVar.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x5F29);
                zonaDisponibleVar.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x5071);
                Zona.DiccionarioOffsetsZonas.AddRange(new Zona[] {zonaDisponibleVar,zonaNivelYEstadoVar,zonaPokemonErranteVar,zonaSpecialPokemonErrante,zonaVitalidadVar });
            }
            PokemonGBAFrameWork.Pokemon pokemon;
            int vida;
            int nivel;
            byte stats;

            public Pokemon(PokemonGBAFrameWork.Pokemon pokemon, int vida, int nivel, byte stats)
            {
                PokemonErrante = pokemon;
                Vida = vida;
                Nivel = nivel;
                Stats = stats;
            }
            public PokemonGBAFrameWork.Pokemon PokemonErrante
            {
                get
                {
                    return pokemon;
                }

                set
                {
                    if (value == null) throw new ArgumentNullException();
                    pokemon = value;
                }
            }

            public int Vida
            {
                get
                {
                    return vida;
                }

                set
                {
                    if (value < 0 || value > ushort.MaxValue) throw new ArgumentOutOfRangeException();//mirar de especificar el maximo
                    vida = value;
                }
            }

            public int Nivel
            {
                get
                {
                    return nivel;
                }

                set
                {
                    if (value < 0 || value > 100) throw new ArgumentOutOfRangeException();
                    nivel = value;
                }
            }

            public byte Stats
            {
                get
                {
                    return stats;
                }

                set
                {
                    stats = value;
                }
            }

            //metodos para sacar el script en texto y en bytes...mas adelante sacarlo con las clases de los scripts :D
            //script
            public static void SetPokemonScript(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion,Hex offset, Pokemon pokemonErrante)
            {
                BloqueBytes.SetBytes(rom, offset, BytesScript(rom, edicion, compilacion, pokemonErrante));
            }
            public static byte[] BytesScript(RomGBA rom,Edicion edicion,CompilacionRom.Compilacion compilacion ,Pokemon pokemonErrante)
            {
                //mas adelante pasarlo a la zona de scripts
                byte[] bytes;
                string[] campos;
                campos = BytesScriptString(rom,edicion,compilacion,pokemonErrante).Replace("\r\n", ",").Replace("  ", ",").Replace(" ",",").Split(',');
                bytes = new byte[campos.Length];
                for (int i = 0; i < bytes.Length; i++)
                    bytes[i] = Convert.ToByte((int)(Hex)campos[i]);
                return bytes;
            }
            public static string BytesScriptString(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Pokemon pokemonErrante)
            {

                //mas adelante pasarlo a la zona de scripts
                string script, variableQueToca, stringQueToca;
                stringQueToca = (Zona.GetVariable(rom, Variables.PokemonErranteSpecial, edicion, compilacion)).Number;
                stringQueToca = stringQueToca.PadLeft(4, '0');
                script = "25 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2);
                stringQueToca = (Zona.GetVariable(rom, Variables.PokemonErranteVar, edicion, compilacion)).Number;
                variableQueToca = ((Hex)pokemonErrante.PokemonErrante.OrdenPokedexNacional).Number.PadLeft(4, '0');
                script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + variableQueToca.Substring(2, 2) + " " + variableQueToca.Substring(0, 2);
                stringQueToca = (Zona.GetVariable(rom, Variables.PokemonErranteVitalidadVar, edicion, compilacion)).Number;
                variableQueToca = ((Hex)pokemonErrante.Vida).Number.PadLeft(4, '0');
                script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + variableQueToca.Substring(2, 2) + " " + variableQueToca.Substring(0, 2);
                stringQueToca = (Zona.GetVariable(rom, Variables.PokemonErranteNivelYEstadoVar, edicion, compilacion)).Number;
                script += "\r\n16 " + stringQueToca.Substring(2, 2) + " " + stringQueToca.Substring(0, 2) + " " + ((Hex)pokemonErrante.Nivel).Number.PadLeft(2, '0') + " " + ((Hex)pokemonErrante.Stats).Number.PadLeft(2, '0');
                script += "\r\n02";
                return script;
            }
            public static string Script(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion, Pokemon pokemonErrante)
            {

                const string CABEZERASCRIPT = "#dynamic 0x800000\r\n#org @ScriptPokemonErrante";
                string script = CABEZERASCRIPT;
                script += "\r\nspecial " + (Zona.GetVariable(rom, Variables.PokemonErranteSpecial, edicion, compilacion)).ByteString;
                script += "\r\nsetvar  " + (Zona.GetVariable(rom, Variables.PokemonErranteVar, edicion, compilacion)).ByteString + " " + ((Hex)pokemonErrante.PokemonErrante.OrdenPokedexNacional).ByteString;
                script += "\r\nsetvar  " + (Zona.GetVariable(rom, Variables.PokemonErranteVitalidadVar, edicion, compilacion)).ByteString + " " + ((Hex)pokemonErrante.Vida).ByteString;
                script += "\r\nsetvar  " + (Zona.GetVariable(rom, Variables.PokemonErranteNivelYEstadoVar, edicion, compilacion)).ByteString + " " + ((Hex)pokemonErrante.Stats).ByteString + ((Hex)pokemonErrante.Nivel).Number;
                script += "\r\nend";
                return script;
            }

        }
        //mirar de abstraer un poco mas y ponerlo en otro sitio mas comun porque creo que se usa en mas sitios
        public static bool ValidaDireccion(RomGBA rom, Hex offset, Hex length)
        {
            //si no acaba en 048C no es valida y si en esa direccion no hay esos bytes como minimo tampoco
            string posicionString = offset;
            bool esValida = posicionString[posicionString.Length - 1] != '0' && posicionString[posicionString.Length - 1] != '4' && posicionString[posicionString.Length - 1] != '8' && posicionString[posicionString.Length - 1] != 'C';
            if (esValida)
                esValida = BloqueBytes.SearchEmptyBytes(rom,offset,length,true)==offset;
            return esValida;
        }
    }
}
