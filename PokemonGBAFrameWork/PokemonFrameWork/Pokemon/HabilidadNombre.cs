using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Habilidad
{
    public class Nombre
    {
        public const int LENGTHNOMBRE = 13;
        public static readonly Zona ZonaNombreHabilidad;
        BloqueString text;
        static Nombre()
        {
            ZonaNombreHabilidad = new Zona("Zona nombre habilidad");

            ZonaNombreHabilidad.Add(0x1C0, EdicionPokemon.RojoFuegoUsa, EdicionPokemon.VerdeHojaUsa, EdicionPokemon.RojoFuegoEsp, EdicionPokemon.VerdeHojaEsp, EdicionPokemon.EsmeraldaUsa, EdicionPokemon.EsmeraldaEsp);
            ZonaNombreHabilidad.Add(EdicionPokemon.RubiUsa, 0x9FE64, 0x9FE84);
            ZonaNombreHabilidad.Add(EdicionPokemon.ZafiroUsa, 0x9FE64, 0x9FE84);
            ZonaNombreHabilidad.Add(0xA0098, EdicionPokemon.RubiEsp, EdicionPokemon.ZafiroEsp);

        }
        public Nombre()
        {
            text = new BloqueString(LENGTHNOMBRE);
        }
        public override string ToString()
        {
            return text;
        }
        public static Nombre GetNombre(RomGba rom, int index)
        {
            Nombre nombre = new Nombre();
            int offsetNombre = Zona.GetOffsetRom(ZonaNombreHabilidad, rom).Offset + index * LENGTHNOMBRE;
            nombre.text.Texto = BloqueString.GetString(rom, offsetNombre, LENGTHNOMBRE).Texto;
            return nombre;
        }
        public static void SetNombre(RomGba rom, int index, Nombre nombre)
        {
            int offsetNombre;
            offsetNombre = Zona.GetOffsetRom(ZonaNombreHabilidad, rom).Offset + index * LENGTHNOMBRE;
            BloqueString.Remove(rom, offsetNombre);
            BloqueString.SetString(rom, offsetNombre, nombre.text);

        }
        public static void SetNombre(RomGba rom, IList<Nombre> nombres)
        {
            OffsetRom offsetNombre;
            int totalActual = HabilidadCompleta.GetTotal(rom);
            offsetNombre = Zona.GetOffsetRom(ZonaNombreHabilidad, rom);
            rom.Data.Remove(offsetNombre.Offset, totalActual * LENGTHNOMBRE);
            if (totalActual < nombres.Count)
            {
                //reubico
                OffsetRom.SetOffset(rom, offsetNombre, rom.Data.SearchEmptyBytes(nombres.Count * LENGTHNOMBRE));
            }
            for (int i = 0; i < nombres.Count; i++)
                SetNombre(rom, i, nombres[i]);
        }
    }
}
