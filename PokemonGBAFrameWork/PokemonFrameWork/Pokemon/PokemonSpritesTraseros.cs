using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Poke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class Traseros
    {
        public const byte ID = 0x27;
        public static readonly Zona ZonaImgTrasera;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Traseros>();

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
        public Llista<BloqueImagen> Sprites { get; private set; }




        public static PokemonGBAFramework.Pokemon.Sprites.Traseros GetTraseros(RomGba rom, int posicion)
        {
            byte[] auxImg;
            Traseros traseros = new Traseros();
            int offsetImgFrontalPokemon = Zona.GetOffsetRom(ZonaImgTrasera, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;
            BloqueImagen bloqueImgFrontal = BloqueImagen.GetBloqueImagen(rom, offsetImgFrontalPokemon);
            auxImg = bloqueImgFrontal.DatosDescomprimidos.Bytes;
            for (int i = 0, f = auxImg.Length / SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA, pos = 0; i < f; i++, pos += SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                traseros.Sprites.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos, SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA))));
            }

            return new PokemonGBAFramework.Pokemon.Sprites.Traseros() { Imagenes = traseros.Sprites.Select((img) => img.GetImg()).ToList() };
        }
        public static PokemonGBAFramework.Paquete GetTraseros(RomGba rom)
        {
            return rom.GetPaquete("Traseros Pokemon", (r, i) => GetTraseros(r, i), Huella.GetTotal(rom));
        }
    }
}
