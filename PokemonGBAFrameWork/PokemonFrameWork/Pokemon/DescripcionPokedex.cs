﻿/*
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
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork.Pokemon
{
    /// <summary>
    /// Description of DescripcionPokedex.
    /// </summary>
    public class Descripcion : PokemonFrameWorkItem
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

        BloqueString blEspecie;
        BloqueString blDescripcion;//en el set si es Rubi o Zafiro se divide en dos paginas
        Word peso;
        Word altura;
        Word escalaPokemon;
        Word escalaEntrenador;
        //datos que desconozco
        Word numero;
        Word direccionPokemon;
        Word direccionEntrenador;
        Word numero2;

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
        public Word Peso
        {
            get
            {
                return peso;
            }
            set
            {
                peso = value;
            }
        }
        /// <summary>
        /// Se tiene que dividir entre 10 para obtener la medida en metros
        /// </summary>
        public Word Altura
        {
            get
            {
                return altura;
            }
            set
            {
                altura = value;
            }
        }

        public Word EscalaPokemon
        {
            get
            {
                return escalaPokemon;
            }
            set
            {
                escalaPokemon = value;
            }
        }

        public Word EscalaEntrenador
        {
            get
            {
                return escalaEntrenador;
            }
            set
            {
                escalaEntrenador = value;
            }
        }

        public Word Numero
        {
            get
            {
                return numero;
            }
            set
            {
                numero = value;
            }
        }

        public Word DireccionPokemon
        {
            get
            {
                return direccionPokemon;
            }
            set
            {
                direccionPokemon = value;
            }
        }

        public Word DireccionEntrenador
        {
            get
            {
                return direccionEntrenador;
            }
            set
            {
                direccionEntrenador = value;
            }
        }

        public Word Numero2
        {
            get
            {
                return numero2;
            }
            set
            {
                numero2 = value;
            }
        }



        public BloqueString Especie { get => blEspecie; set => blEspecie = value; }
        public BloqueString Texto { get => blDescripcion; set => blDescripcion = value; }

        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static Descripcion[] GetDescripcionPokedex(RomGba rom)
        {
            Descripcion[] descripcions = new Descripcion[GetTotal(rom)];
            for (int i = 0; i < descripcions.Length; i++)
                descripcions[i] = GetDescripcionPokedex(rom, i);
            return descripcions;
        }

        public static Descripcion GetDescripcionPokedex(RomGba rom, int ordenNacionalPokemon)
        {
            int offsetDescripcionPokemon = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + ordenNacionalPokemon * LongitudDescripcion((EdicionPokemon)rom.Edicion);
            int posicionActual = offsetDescripcionPokemon;
            Descripcion descripcionPokemon = new Descripcion();
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

            if (((EdicionPokemon)rom.Edicion).Idioma == Idioma.Ingles)
                descripcionPokemon.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Español;
            else descripcionPokemon.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Ingles;

            descripcionPokemon.IdElemento = (ushort)ordenNacionalPokemon;

            return descripcionPokemon;


        }

        public static void SetDescripcionPokedex(RomGba rom, IList<Descripcion> descripciones)
        {
            //borro los datos antiguos
            int totalAntiguo = GetTotal(rom);
            for (int i = 0; i < totalAntiguo; i++)
                Remove(rom, i);
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaDescripcion, rom), rom.Data.SearchEmptyBytes(descripciones.Count * LongitudDescripcion((EdicionPokemon)rom.Edicion)));
            //pongo los datos
            for (int i = 0; i < descripciones.Count; i++)
                SetDescripcion(rom, i, descripciones[i]);
        }

        public static void SetDescripcion(RomGba rom, int ordenNacionalPokemon, Descripcion descripcion)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            int offsetDescripcionPokemon = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + ordenNacionalPokemon * LongitudDescripcion(edicion);
            int posicionActual = offsetDescripcionPokemon;
            int totalPagina = TotalText(edicion);
            BloqueString.Remove(rom, posicionActual);
            BloqueString.SetString(rom, posicionActual, descripcion.Especie);
            posicionActual += (int)LongitudCampos.NombreEspecie;

            Word.SetData(rom, posicionActual, descripcion.Altura);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.Peso);
            posicionActual += Word.LENGTH;

            //pongo las paginas de la pokedex
            try
            {
                BloqueString.Remove(rom, new OffsetRom(rom, posicionActual).Offset);
            }
            catch { }
            rom.Data.SetArray(posicionActual, new OffsetRom(BloqueString.SetString(rom, descripcion.Texto.Texto.Substring(0, totalPagina))).BytesPointer);
            posicionActual += OffsetRom.LENGTH;
            if (edicion.EsRubiOZafiro)
            {
                try
                {
                    BloqueString.Remove(rom, new OffsetRom(rom, posicionActual).Offset);
                }
                catch { }
                if (descripcion.Texto.Texto.Length > totalPagina)
                    rom.Data.SetArray(posicionActual, new OffsetRom(BloqueString.SetString(rom, descripcion.Texto.Texto.Substring(totalPagina))).BytesPointer);
                posicionActual += OffsetRom.LENGTH;
            }

            Word.SetData(rom, posicionActual, descripcion.Numero);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.EscalaPokemon);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.DireccionPokemon);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.EscalaEntrenador);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.DireccionEntrenador);
            posicionActual += Word.LENGTH;
            Word.SetData(rom, posicionActual, descripcion.Numero2);
        }
        private static int TotalText(EdicionPokemon edicion)
        {
            int total;
            if (!edicion.EsRubiOZafiro)
                total = (int)LongitudCampos.PaginasGeneral;
            else total = (int)LongitudCampos.PaginasRubiZafiro;
            return total;
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

        public static void Remove(RomGba rom, int ordenNacionalPokemon)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;

            int offsetDescripcionPokemon = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + ordenNacionalPokemon * LongitudDescripcion(edicion);
            int posicionActual = offsetDescripcionPokemon;

            posicionActual += (int)LongitudCampos.NombreEspecie;
            posicionActual += Word.LENGTH;
            posicionActual += Word.LENGTH;
            //borro las paginas de la pokedex
            BloqueString.Remove(rom, new OffsetRom(rom, posicionActual).Offset);
            posicionActual += OffsetRom.LENGTH;
            if (edicion.EsRubiOZafiro)
            {
                BloqueString.Remove(rom, new OffsetRom(rom, posicionActual).Offset);
            }
            //borro los datos
            rom.Data.Remove(offsetDescripcionPokemon, LongitudDescripcion(edicion));
        }
    }
}
