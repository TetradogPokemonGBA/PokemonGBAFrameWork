using Gabriel.Cat.S.Binaris;
using Poke;
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
        public const byte ID = 0x1;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public static readonly Zona ZonaNombres;

        public BloqueString Text { get; set; }


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
        public static PokemonGBAFramework.Paquete GetNombre(RomGba rom)
        {
            return rom.GetPaquete("Nombres Clases Entrenador", (r, i) => GetNombre(r, i), ClaseEntrenador.Sprite.GetTotal(rom));
        }
        public static PokemonGBAFramework.Batalla.NombreClaseEntrenador GetNombre(RomGba rom,int index)
        {
            int offsetNombre = Zona.GetOffsetRom(ZonaNombres, rom).Offset + (index) * (int)Longitud.Nombre;
          
            PokemonGBAFramework.Batalla.NombreClaseEntrenador nombre =new PokemonGBAFramework.Batalla.NombreClaseEntrenador();
            nombre.Nombre = BloqueString.GetString(rom, offsetNombre).Texto;

            return nombre;
        }
     
    }
}
