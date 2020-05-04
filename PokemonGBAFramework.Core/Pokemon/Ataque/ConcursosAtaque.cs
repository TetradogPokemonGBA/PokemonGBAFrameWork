using Gabriel.Cat.S.Extension;
using System;

namespace PokemonGBAFramework.Core
{
    public class ConcursosAtaque
    {
        enum ValoresLimitadoresFin
        {
            AnimacionesConcurso = 0xE0,
            AtaqueConcurso = 0
        }
        public const int LENGTH = 15;

        static readonly byte[] BytesDesLimitadoAtaquesConcurso;
        static readonly byte[] BytesDesLimitadoAnimacionAtaques;

        public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x14, 0x49, 0x40, 0x18, 0x09, 0x21, 0x0F };
        public static readonly int InicioRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 16;
        public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x00, 0x5E, 0x00, 0x03, 0x00, 0x2B, 0x09 };
        public static readonly int InicioRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;
        static ConcursosAtaque()
        {
            byte[] valoresUnLimitedAtaque = { 0 };
            byte[] valoresUnLimitedAnimacion = (((Gabriel.Cat.S.Utilitats.Hex)(int)ValoresLimitadoresFin.AnimacionesConcurso));

            BytesDesLimitadoAtaquesConcurso = new byte[Ataque.LENGTHLIMITADOR];
            BytesDesLimitadoAtaquesConcurso.SetArray(Ataque.LENGTHLIMITADOR - valoresUnLimitedAtaque.Length, valoresUnLimitedAtaque);
            BytesDesLimitadoAnimacionAtaques = new byte[Ataque.LENGTHLIMITADOR];
            BytesDesLimitadoAnimacionAtaques.SetArray(Ataque.LENGTHLIMITADOR - valoresUnLimitedAnimacion.Length, valoresUnLimitedAnimacion);

        }
        public ConcursosAtaque()
        {
            DatosConcursosHoenn = new BloqueBytes(LENGTH);
        }
        public BloqueBytes DatosConcursosHoenn { get; set; }
        public static ConcursosAtaque Get(RomGba rom, int posicionAtaque,OffsetRom offsetInicioConcursoAtaque=default)
        {
            ConcursosAtaque concursosAtaque=new ConcursosAtaque();
            if(rom.Edicion.EsHoenn)
            {
                if (Equals(offsetInicioConcursoAtaque, default))
                    offsetInicioConcursoAtaque = GetOffset(rom);
                concursosAtaque.DatosConcursosHoenn.Bytes = rom.Data.Bytes.SubArray(offsetInicioConcursoAtaque + posicionAtaque * LENGTH, LENGTH);
            }
            return concursosAtaque;
        }
        public static ConcursosAtaque[] Get(RomGba rom,OffsetRom offsetConcursosAtaque=default) => DescripcionAtaque.GetAll<ConcursosAtaque>(rom, Get,Equals(offsetConcursosAtaque,default)? GetOffset(rom):offsetConcursosAtaque);
      
        public static OffsetRom GetOffset(RomGba rom)
        {
            return new OffsetRom(rom, GetZona(rom)); 
        }

        public static Zona GetZona(RomGba rom)
        {
            if (rom.Edicion.EsKanto)
                throw new Exception("Esta parte es solo de la region de Hoenn");
            return Zona.Search(rom, rom.Edicion.EsEsmeralda ? MuestraAlgoritmoEsmeralda : MuestraAlgoritmoRubiYZafiro, rom.Edicion.EsEsmeralda ? InicioRelativoEsmeralda : InicioRelativoRubiYZafiro);
        }
    }
}