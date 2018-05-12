/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 11:06
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Compilacion.
	/// </summary>
	public class CompilacionPokemon:Compilacion
	{
		public static readonly CompilacionPokemon[] Compilaciones=new CompilacionPokemon[]{new CompilacionPokemon(1,0),new CompilacionPokemon(1,1),new CompilacionPokemon(1,2)};

        private CompilacionPokemon(int version, int subVersion) : base(version, subVersion)
        {
        }
      

		public static CompilacionPokemon GetCompilacion(RomGba rom,EdicionPokemon edicion)
		{
            //ahora tengo la edicion correctamente
            CompilacionPokemon compilacionRom = null;
            switch (edicion.AbreviacionRom)
            {
                case AbreviacionCanon.AXV:
                case AbreviacionCanon.AXP:
                    if (edicion.Idioma == Idioma.Español || Zona.GetOffsetRom(DescripcionPokedex.ZonaDescripcion, rom, edicion, CompilacionPokemon.Compilaciones[0]).IsAPointer)
                        compilacionRom = Compilaciones[0];
                    else
                    {
                        if (Zona.GetOffsetRom(Ataque.ZonaAnimacion, rom, edicion, CompilacionPokemon.Compilaciones[1]).IsAPointer)
                            compilacionRom = Compilaciones[1];
                        //me falta saber como diferenciar Ruby&Zafiro 1.1 y Ruby&Zafiro 1.2 USA

                    }
                    break;
                case AbreviacionCanon.BPE:
                    compilacionRom = Compilaciones[0];
                    break;
                case AbreviacionCanon.BPR:
                case AbreviacionCanon.BPG:
                    if (edicion.Idioma == Idioma.Español || Zona.GetOffsetRom(DescripcionPokedex.ZonaDescripcion, rom, edicion, CompilacionPokemon.Compilaciones[0]).IsAPointer)
                        compilacionRom = Compilaciones[0];
                    else
                    {
                        compilacionRom = Compilaciones[1];
                    }
                    break;
            }
            return compilacionRom;
        }
	}
}
