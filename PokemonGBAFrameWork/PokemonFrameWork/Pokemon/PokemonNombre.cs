using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Pokemon
{
    public class Nombre:IElementoBinarioComplejo
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

        ElementoBinario IElementoBinarioComplejo.Serialitzer => Serializador;

        static Nombre()
        {
            ZonaNombre = new Zona("Nombre Pokemon");
            //nombre
            ZonaNombre.Add(0x144, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);
            ZonaNombre.Add(0xFA58, EdicionPokemon.RubiUsa, EdicionPokemon.ZafiroUsa);

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
            return nombre;
        }
        public static Nombre[] GetNombre(RomGba rom)
        {
            Nombre[] nombres = new Nombre[Huella.GetTotal(rom)];
            for (int i = 0; i < nombres.Length; i++)
                nombres[i] = GetNombre(rom, i);
            return nombres;
        }
        public static void SetNombre(RomGba rom, int posicionOrdenGameFreak,Nombre nombre)
        {
            int offsetNombre = Zona.GetOffsetRom(ZonaNombre, rom).Offset + posicionOrdenGameFreak * (int)LongitudCampos.NombreCompilado;

            BloqueString.Remove(rom, offsetNombre);
            BloqueString.SetString(rom, offsetNombre,nombre.Texto);

        }
        public static void SetNombre(RomGba rom,IList<Nombre> nombres)
        {
            //quito los datos
            rom.Data.Remove(Zona.GetOffsetRom(ZonaNombre, rom).Offset, Huella.GetTotal(rom) * (int)LongitudCampos.NombreCompilado);
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaNombre, rom),rom.Data.SearchEmptyBytes(nombres.Count * (int)LongitudCampos.NombreCompilado));
            //pongo los datos
            for (int i = 0; i < nombres.Count; i++)
                SetNombre(rom, i, nombres[i]);
        }
    }
}
