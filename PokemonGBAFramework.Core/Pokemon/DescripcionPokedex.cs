using System;
using System.Collections.Generic;

namespace PokemonGBAFramework.Core
{
    /// <summary>
    /// Description of DescripcionPokedex.
    /// </summary>
    public class DescripcionPokedex 
    {
        public enum LongitudCampos
        {
            TotalGeneral = 36,
            TotalEsmeralda = 32,
            NombreEspecie = 12,
            PaginasRubiZafiro = 2,
            PaginasGeneral = 1,
        }
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x00, 0x2D, 0x0A, 0xD0, 0x29, 0x1C };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x00, 0x2A, 0x09, 0xD0, 0xA3, 0xE0 };
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoEsmeralda.Length - 48;

        public static readonly byte[] MuestraAlgoritmoKanto = { 0x07, 0x98, 0x03, 0x22, 0x2D, 0xF0 };
        public static readonly int InicioRelativoKanto = -MuestraAlgoritmoKanto.Length - 32;
        public DescripcionPokedex()
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

        public override string ToString()
        {
            return Texto.ToString();
        }
        public static DescripcionPokedex[] GetOrdenNacional(RomGba rom)
        {
            DescripcionPokedex[] nombres = new DescripcionPokedex[GetTotal(rom)];
            OffsetRom offset = GetOffset(rom);
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = Get(rom, i, offset);
            return nombres;
        }


        public static DescripcionPokedex Get(RomGba rom, int ordenNacionalPokemon,OffsetRom offsetInicioDescripcionPokedex=default)
        {
            if (Equals(offsetInicioDescripcionPokedex, default))
                offsetInicioDescripcionPokedex = GetOffset(rom);

            int offsetDescripcionPokemon = offsetInicioDescripcionPokedex + ordenNacionalPokemon * LongitudDescripcion(rom.Edicion);
            int posicionActual = offsetDescripcionPokemon;
            DescripcionPokedex descripcionPokemon = new DescripcionPokedex();
            descripcionPokemon.Especie = BloqueString.Get(rom, posicionActual, (int)LongitudCampos.NombreEspecie);
            posicionActual += (int)LongitudCampos.NombreEspecie;
            descripcionPokemon.Altura = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.Peso = new Word(rom, posicionActual);
            posicionActual += Word.LENGTH;
            descripcionPokemon.Texto = BloqueString.Get(rom, new OffsetRom(rom, posicionActual).Offset);
            posicionActual += OffsetRom.LENGTH;
            if (!rom.Edicion.EsEsmeralda)
            {//Esmeralda no tiene ese puntero y Rojo y Verde Apuntan a una pagina vacia asi que no hay problema
                descripcionPokemon.Texto.Texto += "\n" + BloqueString.Get(rom, new OffsetRom(rom, posicionActual).Offset).Texto;
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

            return descripcionPokemon;


        }

        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }
        public static int GetZona(RomGba rom)
        {
            byte[] algoritmo;
            int inicio;
            if (rom.Edicion.EsEsmeralda)
            {
                algoritmo = MuestraAlgoritmoEsmeralda;
                inicio = InicioRelativoEsmeralda;
            }
            else if (rom.Edicion.EsHoenn)
            {
                algoritmo = MuestraAlgoritmoRubiYZafiro;
                inicio = InicioRelativoRubiYZafiro;
            }
            else
            {
                algoritmo = MuestraAlgoritmoKanto;
                inicio = InicioRelativoKanto;
            }
            return Zona.Search(rom, algoritmo, inicio);
        }

        public static int LongitudDescripcion(Edicion edicion)
        {
            int total;
            if (!edicion.EsEsmeralda)
                total = (int)LongitudCampos.TotalGeneral;
            else total = (int)LongitudCampos.TotalEsmeralda;
            return total;
        }

        public static int GetTotal(RomGba rom, OffsetRom offsetDescripcionPokedex=default)
        {
            int total = 0;
            int offsetInicio =Equals(offsetDescripcionPokedex,default)? GetOffset(rom):offsetDescripcionPokedex;
            while (ValidarIndicePokemon(rom, offsetInicio, total))
                total += 3;
            while (!ValidarIndicePokemon(rom, offsetInicio, total))
                total--;

            return total;
        }
        private static bool ValidarOffset(RomGba rom, int offsetInicioDescripcion)
        {
            Edicion edicion = (Edicion)rom.Edicion;
            int offsetValidador;
            bool valido = offsetInicioDescripcion > -1;//si el offset no es valido devuelve -1
            if (valido)
            {
                offsetValidador = offsetInicioDescripcion + (int)LongitudCampos.NombreEspecie + 4/*poner lo que es...*/ ;

                valido = new OffsetRom(rom, offsetValidador).IsAPointer;
                if (valido && edicion.EsHoenn&&!edicion.EsEsmeralda)
                {
                    offsetValidador += OffsetRom.LENGTH;
                    valido = new OffsetRom(rom, offsetValidador).IsAPointer;
                }
            }
            return valido;

        }
        private static bool ValidarIndicePokemon(RomGba rom, int offsetInicio, int ordenGameFreak)
        {
            return ValidarOffset(rom, offsetInicio + ordenGameFreak * LongitudDescripcion(rom.Edicion));
        }

    }
}
