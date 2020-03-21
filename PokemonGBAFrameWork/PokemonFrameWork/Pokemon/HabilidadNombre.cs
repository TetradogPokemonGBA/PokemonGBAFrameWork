using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;
using Poke;
namespace PokemonGBAFrameWork.Habilidad
{
    public class Nombre:IElementoBinarioComplejo
    {
        public const byte ID = 0x1E;
        public const int LENGTHNOMBRE = 13;
        public static readonly Zona ZonaNombreHabilidad;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public BloqueString Text { get; set; }

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        static Nombre()
        {
            ZonaNombreHabilidad = new Zona("Zona nombre habilidad");

            ZonaNombreHabilidad.Add(0x1C0, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
            ZonaNombreHabilidad.Add(EdicionPokemon.RubiUsa10, 0x9FE64, 0x9FE84);
            ZonaNombreHabilidad.Add(EdicionPokemon.ZafiroUsa10, 0x9FE64, 0x9FE84);
            ZonaNombreHabilidad.Add(0xA0098, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);

        }
        public Nombre()
        {
            Text = new BloqueString(LENGTHNOMBRE);
        }
        public override string ToString()
        {
            return Text;
        }
        public static PokemonGBAFramework.Paquete GetNombre(RomGba rom)
        {
            return rom.GetPaquete("Nombres Habilidades",(r,i)=>GetNombre(r,i),Habilidad.Descripcion.GetTotal(rom));
        }
        public static PokemonGBAFramework.Pokemon.NombreHabilidad GetNombre(RomGba rom, int index)
        {
            int offsetNombre = Zona.GetOffsetRom(ZonaNombreHabilidad, rom).Offset + index * LENGTHNOMBRE;

            return new PokemonGBAFramework.Pokemon.NombreHabilidad() { Nombre = BloqueString.GetString(rom, offsetNombre, LENGTHNOMBRE).Texto };
  
        }
    }
}
