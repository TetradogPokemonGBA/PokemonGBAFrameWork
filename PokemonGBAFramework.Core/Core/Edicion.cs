using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
    public class Edicion
    {
        public enum Pokemon
        {
            Rubi,Zafiro,RubiOZafiro,Esmeralda,RojoFuego,VerdeHoja,RojoOVerde
        }

        public Pokemon Version { get; set; }
        public bool EsKanto => Version >= Pokemon.RojoFuego;
        public bool EsHoenn => !EsKanto;
        public bool EsEsmeralda => Version == Pokemon.Esmeralda;
        public static Edicion Get(RomGba romGba)
        {
            Edicion edicion = new Edicion();

            if (romGba.Data.Bytes.SearchArray(Nombre.MuestraAlgoritmo) > 0)
            {
                if (romGba.Data.Bytes.SearchArray(Huella.MuestraAlgoritmoHoenn) > 0)
                {
                    if (romGba.Data.Bytes.SearchArray(Stats.MuestraAlgoritmoRubiYZafiro) > 0)
                    {//RubiZafiroCheck
                     //faltan más checks 
                     //al final pongo esto
                        edicion.Version = Pokemon.RubiOZafiro;
                    }

                    else if (romGba.Data.Bytes.SearchArray(Stats.MuestraAlgoritmoEsmeralda) > 0)
                    {
                        //EsmeraldaCheck
                        //faltan más checks 
                        //al final pongo esto
                        edicion.Version = Pokemon.Esmeralda;
                    }
                    else throw new RomNoValidaException();
                }
                else if (romGba.Data.Bytes.SearchArray(Huella.MuestraAlgoritmoKanto) > 0)
                { //RojoVerdeCheck
                    if (romGba.Data.Bytes.SearchArray(Stats.MuestraAlgoritmoKanto) > 0)
                    { //RojoVerdeCheck
                        //faltan más checks 
                        //al final pongo esto
                        edicion.Version = Pokemon.RojoOVerde;
                    }
                    else throw new RomNoValidaException();
                }
                else throw new RomNoValidaException();
            }
            else throw new RomNoValidaException();

            return edicion;
        }
    }
}
