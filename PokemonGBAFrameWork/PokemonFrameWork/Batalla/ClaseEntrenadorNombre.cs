using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class Nombre : PokemonFrameWorkItem
    {
        enum Longitud
        {
            Nombre = 0xD,
        }
        public const byte ID = 0x1;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public static readonly Zona ZonaNombres;

        public BloqueString Text { get; set; }

        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        static Nombre()
        {
            ZonaNombres = new Zona("Nombre Clase Entrenador");
            ZonaNombres.Add(EdicionPokemon.RubiUsa10, 0xF7088, 0xF70A8);
            ZonaNombres.Add(EdicionPokemon.ZafiroUsa10, 0xF7088, 0xF70A8);

            ZonaNombres.Add(EdicionPokemon.VerdeHojaUsa10, 0xD8074, 0xD8088);
            ZonaNombres.Add(EdicionPokemon.RojoFuegoUsa10, 0xD80A0, 0xD80B4);

            ZonaNombres.Add(0x183B4, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);

            ZonaNombres.Add(0x40FE8, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            ZonaNombres.Add(EdicionPokemon.RojoFuegoEsp10, 0xD7BF4);
            ZonaNombres.Add(EdicionPokemon.VerdeHojaEsp10, 0xD7BC8);

        }
        public Nombre()
        {
            Text = new BloqueString((int)Longitud.Nombre);
        }
        public override string ToString()
        {
            return Text.ToString();
        }
        public static Nombre[] GetNombre(RomGba rom)
        {
            Nombre[] nombres = new Nombre[ClaseEntrenador.Sprite.GetTotal(rom)];
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = GetNombre(rom, i);
            return nombres;
        }
        public static Nombre GetNombre(RomGba rom,int index)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;

            int offsetNombre = Zona.GetOffsetRom(ZonaNombres, rom).Offset + (index) * (int)Longitud.Nombre;
            Nombre nombre=new Nombre();
            nombre.Text = BloqueString.GetString(rom, offsetNombre);
            if (edicion.EsEsmeralda)
                nombre.IdFuente = EdicionPokemon.IDESMERALDA;
            else if (edicion.EsRubiOZafiro)
                nombre.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
            else
                nombre.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

            nombre.IdElemento = (ushort)index;
            return nombre;
        }
        public static void SetNombre(RomGba rom,int index,Nombre nombre)
        {
            int offsetInicioNombre = Zona.GetOffsetRom(ZonaNombres, rom).Offset + index * (int)Longitud.Nombre;
            BloqueString.Remove(rom, offsetInicioNombre);
            BloqueString.SetString(rom, offsetInicioNombre, nombre.Text);

        }
        public static void SetNombre(RomGba rom,IList<Nombre> nombres)
        {
            OffsetRom offsetInicioNombre;
            int totalActual = Sprite.GetTotal(rom);
            offsetInicioNombre = Zona.GetOffsetRom(ZonaNombres, rom);
            rom.Data.Remove(offsetInicioNombre.Offset, totalActual * (int)Longitud.Nombre);
            OffsetRom.SetOffset(rom, offsetInicioNombre, rom.Data.SearchEmptyBytes(nombres.Count * (int)Longitud.Nombre));
            for (int i = 0; i < nombres.Count; i++)
                SetNombre(rom, i, nombres[i]);
        }
    }
}
