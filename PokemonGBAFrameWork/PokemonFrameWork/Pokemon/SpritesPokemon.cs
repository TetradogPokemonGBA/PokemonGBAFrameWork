﻿/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 12/03/2017
 * Time: 14:59
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.Pokemon.Sprite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;


namespace PokemonGBAFrameWork.Pokemon
{
    /// <summary>
    /// Description of Sprite.
    /// </summary>
    public class SpritesCompleto : PokemonFrameWorkItem
    {
        public const byte ID = 0x29;
        public const int LONGITUDLADO = 64;
        public const int TAMAÑOIMAGENDESCOMPRIMIDA = 2048;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<SpritesCompleto>();


        public SpritesCompleto()
        {

            SpritesFrontales = new Frontales();
            SpritesTraseros = new Traseros();
            PaletaNormal = new PaletaNormal();
            PaletaShiny = new PaletaShiny();

        }

        public Frontales SpritesFrontales
        {
            get;
            set;
        }
        public Traseros SpritesTraseros
        {
            get;
            set;
        }

        public PaletaNormal PaletaNormal
        {
            get;
            set;
        }

        public PaletaShiny PaletaShiny
        {
            get;
            set;
        }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static SpritesCompleto GetSprites(RomGba rom, int indexOrdenGameFreakPokemon)
        {

            SpritesCompleto spritePokemon = new SpritesCompleto();

            spritePokemon.SpritesFrontales = Frontales.GetFrontales(rom, indexOrdenGameFreakPokemon);
            spritePokemon.SpritesTraseros = Traseros.GetTraseros(rom, indexOrdenGameFreakPokemon);
            spritePokemon.PaletaNormal = PaletaNormal.GetPaletaNormal(rom, indexOrdenGameFreakPokemon);
            spritePokemon.PaletaShiny = PaletaShiny.GetPaletaShiny(rom, indexOrdenGameFreakPokemon);

            spritePokemon.IdElemento = (ushort)indexOrdenGameFreakPokemon;


            spritePokemon.IdFuente = EdicionPokemon.IDMINRESERVADO;

            if (!((EdicionPokemon)rom.Edicion).EsEsmeralda)
                spritePokemon.IdFuente -= (int)AbreviacionCanon.BPE;

            return spritePokemon;
        }
        public static SpritesCompleto[] GetSprites(RomGba rom)
        {
            SpritesCompleto[] sprites = new SpritesCompleto[Huella.GetTotal(rom)];
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = GetSprites(rom, i);
            return sprites;
        }
  

    }
}
