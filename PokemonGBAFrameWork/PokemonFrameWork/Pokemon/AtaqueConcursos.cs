using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Concursos
    {
        enum ValoresLimitadoresFin
        {
            AnimacionesConcurso = 0xE0,
            AtaqueConcurso = 0
        }
        enum LongitudCampos
        {
            DatosConcurso = 15
        }
        public static readonly Zona ZonaDatosConcursosHoenn;
        public static readonly Variable VariableAtaqueConcurso;
        public static readonly Variable VariableAnimacionAtaqueConcurso;


        static readonly byte[] BytesDesLimitadoAtaquesConcurso;
        static readonly byte[] BytesDesLimitadoAnimacionAtaques;

        BloqueBytes datosConcursosHoenn;

        static Concursos()
        {
            byte[] valoresUnLimitedAtaque = { 0 };
            byte[] valoresUnLimitedAnimacion = (((Hex)(int)ValoresLimitadoresFin.AnimacionesConcurso));

            ZonaDatosConcursosHoenn = new Zona("Datos concursos hoenn");
            VariableAtaqueConcurso = new Variable("Variable Ataque concurso");
            VariableAnimacionAtaqueConcurso = new Variable("Variable Animacion Ataque concurso");


            //añado la parte de los concursos de hoenn
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaUsa, 0xD85F0);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiEsp, 0xA04C4);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiUsa, 0xA0290, 0XA02B0);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);
            //pongo las variables ataques
            VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
            VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa, 0xD8F0C);//puesta
            VariableAtaqueConcurso.Add(EdicionPokemon.RubiEsp, 0xA04C4);
            VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
            VariableAtaqueConcurso.Add(EdicionPokemon.RubiUsa, 0xA0290, 0XA02B0);
            VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);
            //pongo las variables animaciones
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp, 0xD8248);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa, 0xD8F0C);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiEsp, 0xA04C4);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp, 0xA04C4);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiUsa, 0xA0290, 0XA02B0);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa, 0xA0290, 0XA02B0);



            BytesDesLimitadoAtaquesConcurso = new byte[AtaqueCompleto.LENGTHLIMITADOR];
            BytesDesLimitadoAtaquesConcurso.SetArray(AtaqueCompleto.LENGTHLIMITADOR - valoresUnLimitedAtaque.Length, valoresUnLimitedAtaque);
            BytesDesLimitadoAnimacionAtaques = new byte[AtaqueCompleto.LENGTHLIMITADOR];
            BytesDesLimitadoAnimacionAtaques.SetArray(AtaqueCompleto.LENGTHLIMITADOR - valoresUnLimitedAnimacion.Length, valoresUnLimitedAnimacion);

        }
        public Concursos()
        {
            datosConcursosHoenn = new BloqueBytes((int)LongitudCampos.DatosConcurso);
        }
        public static Concursos GetConcursos(RomGba rom, int posicion)
        {
            Concursos concursos = new Concursos();
            if (((EdicionPokemon)rom.Edicion).RegionHoenn)
            {
                //pongo los datos de los concursos de hoenn
                concursos.datosConcursosHoenn.Bytes = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(ZonaDatosConcursosHoenn, rom).Offset + posicion * OffsetRom.LENGTH, (int)LongitudCampos.DatosConcurso).Bytes;
            }
            return concursos;
        }
        public static Concursos[] GetConcursos(RomGba rom)
        {
            Concursos[] concursos = new Concursos[Descripcion.GetTotal(rom)];
            for (int i = 0; i < concursos.Length; i++)
                concursos[i] = GetConcursos(rom, i);
            return concursos;
        }
        public static void SetConcursos(RomGba rom, int posicion, Concursos concursos)
        {
            OffsetRom offsetDatosConcurso;

            int offsetPointerDatos;

            if (((EdicionPokemon)rom.Edicion).RegionHoenn)
            {
                //pongo los datos de los concursos de hoenn

                offsetPointerDatos = Zona.GetOffsetRom(ZonaDatosConcursosHoenn, rom).Offset + posicion * (int)LongitudCampos.DatosConcurso;
                offsetDatosConcurso = new OffsetRom(rom, offsetPointerDatos);
                if (offsetDatosConcurso.IsAPointer)
                    rom.Data.Remove(offsetDatosConcurso.Offset, (int)LongitudCampos.DatosConcurso);//quito los viejos
                OffsetRom.SetOffset(rom, offsetDatosConcurso, rom.Data.SearchEmptySpaceAndSetArray(concursos.datosConcursosHoenn.Bytes));//pongo los nuevos
            }
        }
        public static void SetConcursos(RomGba rom, IList<Concursos> concursos)
        {
            if (((EdicionPokemon)rom.Edicion).RegionHoenn)
            {
                if (concursos.Count > AtaqueCompleto.MAXATAQUESSINASM)//mas adelante adapto el hack de Jambo
                    throw new ArgumentOutOfRangeException("concursos");
                if (Descripcion.GetTotal(rom) < concursos.Count)
                    AtaqueCompleto.QuitarLimite(rom, concursos.Count);
                //elimino los datos
                //int total = Descripcion.GetTotal(rom);
                //int offsetOffsetsConcursos = Zona.GetOffsetRom(ZonaDatosConcursosHoenn, rom).Offset;
                //int offset = offsetOffsetsConcursos;
                //for (int i = 0; i < total; i++)
                //{
                //    rom.Data.Remove(offset, (int)LongitudCampos.DatosConcurso);
                //    offset += OffsetRom.LENGTH;
                //}
                //rom.Data.Remove(offsetOffsetsConcursos, total * OffsetRom.LENGTH);
                //reubico
                //pongo los datos
                for (int i = 0; i < concursos.Count; i++)
                    SetConcursos(rom, i, concursos[i]);
            }
        }
        public static void QuitarLimite(RomGba rom, int posicion)
        {
            if (((EdicionPokemon)rom.Edicion).RegionHoenn)
            {
                //quito la limitacion de los concursos de hoenn
                rom.Data.SetArray(Variable.GetVariable(VariableAtaqueConcurso, rom.Edicion), BytesDesLimitadoAtaquesConcurso);
                rom.Data.SetArray(Variable.GetVariable(VariableAnimacionAtaqueConcurso, rom.Edicion), BytesDesLimitadoAnimacionAtaques);

            }
        }
    }
}
