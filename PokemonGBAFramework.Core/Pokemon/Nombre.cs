using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{//funciona perfectamente :)
    public class Nombre
    {
        public static readonly byte[] MuestraAlgoritmo = { 0x93, 0x53, 0x46, 0x01, 0x93 };
        public const int InicioRelativo = 16;

        public const int LENGTH = 11;
        public BloqueString Texto { get; set; }

        public override string ToString()
        {
            return Texto.ToString();
        }
        public static Nombre Get(RomGba rom,int posicionPokemonGameFreak,OffsetRom offsetInicioNombre = default)
        {
            if (Equals(offsetInicioNombre, default))
                offsetInicioNombre =GetOffset(rom);
            return new Nombre() { Texto = BloqueString.Get(rom, offsetInicioNombre + (posicionPokemonGameFreak * LENGTH), LENGTH) };
        }
        public static Nombre[] Get(RomGba rom, OffsetRom offsetNombre = default) =>Huella.GetAll<Nombre>(rom, Nombre.Get, Equals(offsetNombre, default) ? GetOffset(rom) : offsetNombre);
        public static Nombre[] GetOrdenLocal(RomGba rom, OffsetRom offsetNombre = default) => OrdenLocal.GetOrdenados<Nombre>(rom, (r, o) => Nombre.Get(r), Equals(offsetNombre, default) ? GetOffset(rom) : offsetNombre);
        public static Nombre[] GetOrdenNacional(RomGba rom,OffsetRom offsetNombre=default) => OrdenNacional.GetOrdenados<Nombre>(rom, (r, o) => Nombre.Get(r), Equals(offsetNombre, default) ? GetOffset(rom) : offsetNombre);
        public static Zona GetZona(RomGba rom)
        {
            return Zona.Search(rom.Data.Bytes, MuestraAlgoritmo,InicioRelativo);
        }
        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom));
        }
    }
}
