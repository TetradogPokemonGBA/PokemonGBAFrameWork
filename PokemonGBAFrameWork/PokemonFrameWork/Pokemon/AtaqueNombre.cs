using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Nombre:PokemonFrameWorkItem,IComparable
    {
        enum LongitudCampos
        {
            Nombre = 13
        }
        public const byte ID = 0x18;
        public static readonly Zona ZonaNombre;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public BloqueString Texto { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

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
        public static Nombre GetNombre(RomGba rom,int posicionAtaque)
        {
            Nombre nombre = new Nombre();
            int offsetNombre = Zona.GetOffsetRom(ZonaNombre, rom).Offset + posicionAtaque * (int)LongitudCampos.Nombre;
            nombre.Texto.Texto= BloqueString.GetString(rom, offsetNombre, (int)LongitudCampos.Nombre).Texto;
            if (((EdicionPokemon)rom.Edicion).Idioma == Idioma.Ingles)
                nombre.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Español;
            else nombre.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Ingles;
            nombre.IdElemento = (ushort)posicionAtaque;
            return nombre;
        }
        public static Nombre[] GetNombre(RomGba rom)
        {
            Nombre[] nombres = new Nombre[Descripcion.GetTotal(rom)];
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = GetNombre(rom, i);
            return nombres;
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
