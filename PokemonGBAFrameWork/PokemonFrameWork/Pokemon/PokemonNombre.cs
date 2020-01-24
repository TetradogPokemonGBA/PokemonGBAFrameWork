using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
    public class Nombre:PokemonFrameWorkItem
    {
        public enum LongitudCampos
        {
            NombreCompilado = 11,
            Nombre = NombreCompilado + 2,//para los Nidoran[f] se guarda [f] como un caracter...de alli que le sume 2 '[',']'
        }
        public const byte ID = 0x21;
        public static readonly Zona ZonaNombre;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Nombre>();

        public BloqueString Texto { get; set; }
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        static Nombre()
        {
            ZonaNombre = new Zona("Nombre Pokemon");
            //nombre
            ZonaNombre.Add(0x144, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10, EdicionPokemon.RojoFuegoUsa10, EdicionPokemon.VerdeHojaUsa10, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
            ZonaNombre.Add(0xFA58, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);

        }
        public Nombre()
        {
            Texto = new BloqueString((int)LongitudCampos.Nombre);

        }
        public override string ToString()
        {
            return Texto.ToString();
        }
        public static Nombre GetNombre(RomGba rom,int posicionOrdenGameFreak)
        {
            Nombre nombre = new Nombre();
            nombre.Texto = BloqueString.GetString(rom, Zona.GetOffsetRom(ZonaNombre, rom).Offset + (posicionOrdenGameFreak * (int)LongitudCampos.NombreCompilado));
            nombre.IdFuente = EdicionPokemon.IDMINRESERVADO;
            nombre.IdElemento = (ushort)posicionOrdenGameFreak;
            return nombre;
        }
        public static Nombre[] GetNombre(RomGba rom)
        {
            Nombre[] nombres = new Nombre[Huella.GetTotal(rom)];
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = GetNombre(rom, i);
            return nombres;
        }
      }
}
