using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Objeto
{
    public class Sprite:PokemonFrameWorkItem
    {
        public const byte ID = 0x8;
        public static readonly Zona ZonaImagenesObjeto;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Sprite>();

        public BloqueImagen Imagen { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        static Sprite()
        {
            ZonaImagenesObjeto = new Zona("Imagen Y Paleta Objeto");
            //datos imagen y paleta
            ZonaImagenesObjeto.Add(EdicionPokemon.RojoFuegoUsa10, 0x9899C, 0x989B0);
            ZonaImagenesObjeto.Add(EdicionPokemon.VerdeHojaUsa10, 0x98970, 0x98984);
            ZonaImagenesObjeto.Add(EdicionPokemon.EsmeraldaUsa10, 0x1B0034);
            //  zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1294bc);//objetos base secreta

            ZonaImagenesObjeto.Add(EdicionPokemon.EsmeraldaEsp10, 0x1AFC54);
            //zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1290d4);//objetos base secreta
            ZonaImagenesObjeto.Add(EdicionPokemon.RojoFuegoEsp10, 0x98B74);
            ZonaImagenesObjeto.Add(EdicionPokemon.VerdeHojaEsp10, 0x98b48);
        }
        public static Sprite[] GetSprite(RomGba rom)
        {
            Sprite[] sprites = new Sprite[Objeto.Datos.GetTotal(rom)];
            for (int i = 0; i < sprites.Length; i++)
                sprites[i] = GetSprite(rom, i);
            return sprites;
        }
        public static Sprite GetSprite(RomGba rom,int index)
        {
            BloqueImagen blImg;
            int offsetImagenYPaleta;
            Sprite sprite = new Sprite();
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            if (!edicion.EsRubiOZafiro)
            {
                offsetImagenYPaleta = Zona.GetOffsetRom(ZonaImagenesObjeto, rom).Offset + index * (OffsetRom.LENGTH + OffsetRom.LENGTH);
                //esas ediciones no tienen imagen los objetos
                blImg = BloqueImagen.GetBloqueImagenSinHeader(rom, offsetImagenYPaleta);
                blImg.Paletas.Add(Paleta.GetPaletaSinHeader(rom, offsetImagenYPaleta + OffsetRom.LENGTH));
                sprite.Imagen = blImg;

                if (edicion.EsEsmeralda)
                    sprite.IdFuente = EdicionPokemon.IDESMERALDA;
                else if (edicion.EsRubiOZafiro)
                    sprite.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
                else
                    sprite.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

                sprite.IdElemento = (ushort)index;
            }
            return sprite;
        }
    
    }
}
