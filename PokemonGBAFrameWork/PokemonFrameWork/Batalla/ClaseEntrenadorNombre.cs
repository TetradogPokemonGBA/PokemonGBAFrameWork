using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.ClaseEntrenador
{
    public class Nombre
    {
        enum Longitud
        {
            Nombre = 0xD,
        }
        public static readonly Zona ZonaNombres;
        BloqueString blNombre;

        public BloqueString Text { get => blNombre; set => blNombre = value; }

        static Nombre()
        {
            ZonaNombres = new Zona("Nombre Clase Entrenador");
            ZonaNombres.Add(EdicionPokemon.RubiUsa, 0xF7088, 0xF70A8);
            ZonaNombres.Add(EdicionPokemon.ZafiroUsa, 0xF7088, 0xF70A8);

            ZonaNombres.Add(EdicionPokemon.VerdeHojaUsa, 0xD8074, 0xD8088);
            ZonaNombres.Add(EdicionPokemon.RojoFuegoUsa, 0xD80A0, 0xD80B4);

            ZonaNombres.Add(0x183B4, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);

            ZonaNombres.Add(0x40FE8, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);
            ZonaNombres.Add(EdicionPokemon.RojoFuegoEsp, 0xD7BF4);
            ZonaNombres.Add(EdicionPokemon.VerdeHojaEsp, 0xD7BC8);

        }
        public Nombre()
        {
            Text = new BloqueString((int)Longitud.Nombre);
        }
        public override string ToString()
        {
            return Text.ToString();
        }
        public static Nombre GetNombre(RomGba rom,int index)
        {
            int offsetNombre = Zona.GetOffsetRom(ZonaNombres, rom).Offset + (index) * (int)Longitud.Nombre;
            Nombre nombre=new Nombre();
            nombre.Text = BloqueString.GetString(rom, offsetNombre);
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
            int totalActual = Sprite.GatTotal(rom);
            offsetInicioNombre = Zona.GetOffsetRom(ZonaNombres, rom);
            rom.Data.Remove(offsetInicioNombre.Offset, totalActual * (int)Longitud.Nombre);
            OffsetRom.SetOffset(rom, offsetInicioNombre, rom.Data.SearchEmptyBytes(nombres.Count * (int)Longitud.Nombre));
            for (int i = 0; i < nombres.Count; i++)
                SetNombre(rom, i, nombres[i]);
        }
    }
}
