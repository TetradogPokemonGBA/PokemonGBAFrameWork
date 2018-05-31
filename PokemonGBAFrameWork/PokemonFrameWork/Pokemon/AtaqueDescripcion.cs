using Gabriel.Cat.S.Binaris;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Descripcion:PokemonFrameWorkItem
    {
        public const byte ID = 0x17;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Descripcion>();

        public static readonly Zona ZonaDescripcion;
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public BloqueString Texto { get; set; }

        static Descripcion()
        {
            ZonaDescripcion = new Zona("DescripciónAtaque");
            //descripcion con ellas calculo el total :D
            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoUsa10, 0xE5440, 0xE5454);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaUsa10, 0xE5418, 0xE542C);
            ZonaDescripcion.Add(EdicionPokemon.RubiUsa10, 0xA0494, 0xA04B4);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroUsa10, 0xA0494, 0xA04B4);
            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaUsa10, 0x1C3EFC);
            ZonaDescripcion.Add(EdicionPokemon.EsmeraldaEsp10, 0x1C3B1C);
            ZonaDescripcion.Add(EdicionPokemon.RubiEsp10, 0xA06C8);
            ZonaDescripcion.Add(EdicionPokemon.ZafiroEsp10, 0xA06C8);
            ZonaDescripcion.Add(EdicionPokemon.RojoFuegoEsp10, 0xE574C);
            ZonaDescripcion.Add(EdicionPokemon.VerdeHojaEsp10, 0XE5724);

        }
        public static int GetTotal(RomGba rom)
        {
            int offsetDescripciones = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset;
            int total = 1;//el primero no tiene
            while (new OffsetRom(rom, offsetDescripciones).IsAPointer)
            {
                offsetDescripciones += OffsetRom.LENGTH;//avanzo hasta la proxima descripcion :)
                total++;
            }
            return total;
        }
        public static Descripcion GetDescripcion(RomGba rom,int posicion)
        {
            EdicionPokemon edicion = (EdicionPokemon)rom.Edicion;
            Descripcion descripcion;
            int offsetDescripcion;
            if (posicion != 0)//el primero no tiene
            {
                offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + (posicion - 1)*OffsetRom.LENGTH).Offset;
                descripcion = new Descripcion();
                descripcion.Texto = BloqueString.GetString(rom, offsetDescripcion);
                if (edicion.Idioma == Idioma.Ingles)
                    descripcion.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Español;
                else descripcion.IdFuente = EdicionPokemon.IDMINRESERVADO - (int)Idioma.Ingles;
                descripcion.IdElemento = (ushort)posicion;
            }
            else descripcion = null;
            return descripcion;
        }
        public static Descripcion[] GetDescripcion(RomGba rom)
        {
            Descripcion[] descripcions = new Descripcion[GetTotal(rom)];
            for (int i = 0; i < descripcions.Length; i++)
                descripcions[i] = GetDescripcion(rom, i);
            return descripcions;
        }
        public static void SetDescripcion(RomGba rom,int posicion,Descripcion descripcion)
        {
            int offsetDescripcion;

            if (posicion != 0)
            {
                offsetDescripcion = offsetDescripcion = new OffsetRom(rom, Zona.GetOffsetRom(ZonaDescripcion, rom).Offset + (posicion - 1) * OffsetRom.LENGTH).Offset;
                BloqueString.Remove(rom, offsetDescripcion);
                rom.Data.SetArray(offsetDescripcion, new OffsetRom(BloqueString.SetString(rom, descripcion.Texto)).BytesPointer);
            }
        }
        public static void SetDescripcion(RomGba rom,IList<Descripcion> descripcions)
        {
            if (descripcions.Count > AtaqueCompleto.MAXATAQUESSINASM)//mas adelante adapto el hack de Jambo
                throw new ArgumentOutOfRangeException("descripcions");
            //borro los datos
            int offset = Zona.GetOffsetRom(ZonaDescripcion, rom).Offset;
            int total = GetTotal(rom)-1;//el primero no tiene...

            if (total < descripcions.Count)
                AtaqueCompleto.QuitarLimite(rom, descripcions.Count);

            for (int i=0;i<total;i++)
            {
                BloqueString.Remove(rom, new OffsetRom(rom, offset).Offset);
                total += OffsetRom.LENGTH;
            }
            //reubico
            OffsetRom.SetOffset(rom, Zona.GetOffsetRom(ZonaDescripcion, rom), rom.Data.SearchEmptyBytes(descripcions.Count * OffsetRom.LENGTH));
            //pongo los datos
            for (int i = 0; i < descripcions.Count; i++)
                SetDescripcion(rom, i, descripcions[i]);
        }
    }
}
