using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class Traseros:PokemonFrameWorkItem
    {
        public const byte ID = 0x27;
        public static readonly Zona ZonaImgTrasera;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Traseros>();
        Llista<BloqueImagen> sprites;
        static Traseros()
        {
            ZonaImgTrasera = new Zona("Imagen Trasera Pokemon");
            ZonaImgTrasera.Add(0xD3D8, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);
            ZonaImgTrasera.Add(0x12C, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10);

        }
        public Traseros()
        {
            Sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites { get => sprites; private set => sprites = value; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public static Traseros GetTraseros(RomGba rom,int posicion)
        {
            byte[] auxImg;
            Traseros traseros = new Traseros();
            int offsetImgTraseraPokemon = Zona.GetOffsetRom(ZonaImgTrasera, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgTrasera = BloqueImagen.GetBloqueImagen(rom, offsetImgTraseraPokemon);
            auxImg = bloqueImgTrasera.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos +=SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                traseros.Sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }
            traseros.IdFuente = EdicionPokemon.IDMINRESERVADO;

            if (!((EdicionPokemon)rom.Edicion).EsEsmeralda)
                traseros.IdFuente -= (int)AbreviacionCanon.BPE;
            traseros.IdElemento = (ushort)posicion;

            return traseros;

        }
        public static Traseros[] GetTraseros(RomGba rom)
        {
            Traseros[] traseros = new Traseros[Huella.GetTotal(rom)];
            for (int i = 0; i < traseros.Length; i++)
                traseros[i] = GetTraseros(rom, i);
            return traseros;
        }
    }
}
