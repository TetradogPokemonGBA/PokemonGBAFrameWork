/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:56
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Poke;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork.Pokemon
{
    /// <summary>
    /// Description of DescripcionPokedex.
    /// </summary>
    public class Descripcion 
    {
        public enum LongitudCampos
        {
            TotalGeneral = 36,
            TotalEsmeralda = 32,
            NombreEspecie = 12,
            PaginasRubiZafiro = 2,
            PaginasGeneral = 1,
        }
        public const byte ID = 0x1B;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Descripcion>();

        public static readonly Zona ZonaDescripcion;

        static Descripcion()
        {
            ZonaDescripcion = new Zona("DescripcionPokedex");

            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaEsp10, 0xBFA48);
            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaUsa10, 0xBFA20);

            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoEsp10, 0x88FEC);
            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoUsa10, 0x88E34, 0x88E48);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaEsp10, 0x88FC0);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaUsa10, 0x88E08, 0x88E1C);

            ZonaDescripcion.Add(EdicionPokemon.RubiEsp10, 0x8F998);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroEsp10, 0x8F998);
            ZonaDescripcion.Add(EdicionPokemon.RubiUsa10, 0x8F508, 0x8F528);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroUsa10, 0x8F508, 0x8F528);

        }
        public Descripcion()
        {
            Texto = new BloqueString();
            Especie = new BloqueString((int)LongitudCampos.NombreEspecie);
        }
        /// <summary>
        /// Se tiene que dividir entre 10 para obtener la medida en Kg
        /// </summary>
        public Word Peso { get; set; }
        /// <summary>
        /// Se tiene que dividir entre 10 para obtener la medida en metros
        /// </summary>
        public Word Altura { get; set; }

        public Word EscalaPokemon { get; set; }

        public Word EscalaEntrenador { get; set; }

        public Word Numero { get; set; }

        public Word DireccionPokemon { get; set; }

        public Word DireccionEntrenador { get; set; }

        public Word Numero2 { get; set; }



        public BloqueString Especie { get; set; }
        public BloqueString Texto { get; set; }


        public static PokemonGBAFramework.Paquete GetDescripcionPokedex(RomGba rom)
        {
            return Poke.Extension.GetPaquete(rom,"Descripciones Pokedex",(r,i)=>GetDescripcionPokedex(r,i),GetTotal(rom));
        }

        public static PokemonGBAFramework.Pokemon.DescripcionPokedex GetDescripcionPokedex(RomGba rom, int ordenNacionalPokemon)
        {
            int offsetDescripcionPokemon = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + ordenNacionalPokemon * LongitudDescripcion((EdicionPokemon)rom.Edicion);
            int posicionActual = offsetDescripcionPokemon;
            Descripcion descripcionPokemon = new Descripcion();
            PokemonGBAFramework.Pokemon.DescripcionPokedex descripcion = new PokemonGBAFramework.Pokemon.DescripcionPokedex();
            descripcionPokemon.Especie = BloqueString.GetString(rom, posicionActual, (int)LongitudCampos.NombreEspecie);
            posicionActual += (int)LongitudCampos.NombreEspecie;
            descripcionPokemon.Altura = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.Peso = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.Texto = BloqueString.GetString(rom, new OffsetRom(rom, posicionActual).Offset);
            posicionActual += OffsetRom.LENGTH;
            if (!((EdicionPokemon)rom.Edicion).EsEsmeralda)
            {//Esmeralda no tiene ese puntero y Rojo y Verde Apuntan a una pagina vacia asi que no hay problema
                descripcionPokemon.Texto.Texto += "\n" + BloqueString.GetString(rom, new OffsetRom(rom, posicionActual).Offset).Texto;
                posicionActual += OffsetRom.LENGTH;
            }
            descripcionPokemon.Numero = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.EscalaPokemon = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.DireccionPokemon = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.EscalaEntrenador = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.DireccionEntrenador = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.Numero2 = new Word(rom, posicionActual);

            descripcionPokemon.SetValues(descripcion);

            return descripcion;


        }
       public static int LongitudDescripcion(EdicionPokemon edicion)
        {
            int total;
            if (!edicion.EsEsmeralda)
                total = (int)LongitudCampos.TotalGeneral;
            else total = (int)LongitudCampos.TotalEsmeralda;
            return total;
        }

        public static int GetTotal(RomGba rom)
        {
            int total = 0;
            int offsetInicio = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset;
            while (ValidarIndicePokemon(rom, offsetInicio, total))
                total += 3;
            while (!ValidarIndicePokemon(rom, offsetInicio, total))
                total--;

            return total;
        }
        private static bool ValidarOffset(RomGba rom, int offsetInicioDescripcion)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetValidador;
            bool valido = offsetInicioDescripcion > -1;//si el offset no es valido devuelve -1
            if (valido)
            {
                offsetValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ ;

                valido = new OffsetRom(rom, offsetValidador).IsAPointer;
                if (valido && edicion.EsRubiOZafiro)
                {
                    offsetValidador += OffsetRom.LENGTH;
                    valido = new OffsetRom(rom, offsetValidador).IsAPointer;
                }
            }
            return valido;

        }
        private static bool ValidarIndicePokemon(RomGba rom, int offsetInicio, int ordenGameFreak)
        {
            return ValidarOffset(rom, offsetInicio + ordenGameFreak * LongitudDescripcion((EdicionPokemon)rom.Edicion));
        }

     }
}
