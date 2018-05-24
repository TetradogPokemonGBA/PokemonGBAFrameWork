using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon.Sprite
{
    public class Traseros:IElementoBinarioComplejo
    {
        public static readonly Zona ZonaImgTrasera;
        public static readonly ElementoBinario Serializador = ElementoBinarioNullable.GetElementoBinario(typeof(Traseros));
        Llista<BloqueImagen> sprites;
        static Traseros()
        {
            ZonaImgTrasera = new Zona("Imagen Trasera Pokemon");
            ZonaImgTrasera.Add(0xD3D8, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);
            ZonaImgTrasera.Add(0x12C, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp, EdicionPokemon.EsmeraldaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.VerdeHojaUsa);

        }
        public Traseros()
        {
            Sprites = new Llista<BloqueImagen>();
        }
        public Llista<BloqueImagen> Sprites { get => sprites; private set => sprites = value; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

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
            return traseros;

        }
        public static Traseros[] GetTraseros(RomGba rom)
        {
            Traseros[] traseros = new Traseros[Huella.GetTotal(rom)];
            for (int i = 0; i < traseros.Length; i++)
                traseros[i] = GetTraseros(rom, i);
            return traseros;
        }
        public static void SetTraseros(RomGba rom,int posicion,Traseros traseros)
        {
            BloqueImagen bloqueCompleto;
            byte[] auxImg;
            int offsetImgTraseraPokemon = Zona.GetOffsetRom(ZonaImgTrasera, rom).Offset + BloqueImagen.LENGTHHEADERCOMPLETO * posicion;

            auxImg = new byte[traseros.Sprites.Count *SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA];
            for (int i = 0, pos = 0; i < traseros.Sprites.Count; i++, pos += SpritesCompleto.TAMAÑOIMAGENDESCOMPRIMIDA)
            {
                auxImg.SetArray(pos, traseros.Sprites[i].DatosDescomprimidos.Bytes);
            }
            bloqueCompleto = new BloqueImagen(new BloqueBytes(auxImg));
            bloqueCompleto.Id = (short)posicion;
            bloqueCompleto.DatosDescomprimidos.Bytes = auxImg;
            bloqueCompleto.Offset = new OffsetRom(rom, offsetImgTraseraPokemon).Offset;
            //pongo las nuevas imagenes
            BloqueImagen.SetBloqueImagen(rom, offsetImgTraseraPokemon, bloqueCompleto);

        }
        public static void SetTraseros(RomGba rom, IList<Traseros> traseros)
        {
            //borro las imagenes
            int total = Huella.GetTotal(rom);
            int offsetImgTraseraPokemon = Zona.GetOffsetRom(ZonaImgTrasera, rom).Offset;
            for(int i=0;i<total;i++)
            {
                try
                {
                    BloqueImagen.Remove(rom, offsetImgTraseraPokemon);
                }
                catch { }
                rom.Data.Remove(offsetImgTraseraPokemon, BloqueImagen.LENGTHHEADERCOMPLETO);

                offsetImgTraseraPokemon += BloqueImagen.LENGTHHEADERCOMPLETO;
            }
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaImgTrasera, rom), rom.Data.SearchEmptyBytes(traseros.Count * BloqueImagen.LENGTHHEADERCOMPLETO));
            //pongo los datos
            for (int i = 0; i < traseros.Count; i++)
                SetTraseros(rom, i, traseros[i]);
        }
    }
}
