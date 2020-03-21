using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Nombre:IComparable
    {
        enum LongitudCampos
        {
            Nombre = 13
        }
        public const byte ID = 0x18;
        public static readonly Zona ZonaNombre;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public BloqueString Texto { get; set; }


        static Nombre()
        {
            ZonaNombre = new Zona("NombreAtaque");
            //nombres
            ZonaNombre.Add(0x148, EdicionPokemon.EsmeraldaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
            ZonaNombre.Add(0x2e18c, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);

        }
        public Nombre()
        {
            Texto = new BloqueString((int)LongitudCampos.Nombre);

        }
        public override string ToString()
        {
            return Texto.ToString();
        }
        public static PokemonGBAFramework.Pokemon.Ataque.NombreAtaque GetNombre(RomGba rom,int posicionAtaque)
        {
            Nombre nombre = new Nombre();
            int offsetNombre = Zona.GetOffsetRom(ZonaNombre, rom).Offset + posicionAtaque * (int)LongitudCampos.Nombre;
           return new PokemonGBAFramework.Pokemon.Ataque.NombreAtaque() { Nombre = BloqueString.GetString(rom, offsetNombre, (int)LongitudCampos.Nombre).Texto };

      
        }
        public static PokemonGBAFramework.Paquete GetNombre(RomGba rom)
        {
            return Poke.Extension.GetPaquete(rom,"Nombres Ataques",(r,i)=>GetNombre(r,i),Descripcion.GetTotal(rom));
        }
 
        public int CompareTo(object obj)
        {
            Nombre other = obj as Nombre;
            int compareTo;
            if (other != null)
            {
                compareTo = Texto.Texto.CompareTo(other.Texto.Texto);
            }
            else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
            return compareTo;
        }
    }
}
