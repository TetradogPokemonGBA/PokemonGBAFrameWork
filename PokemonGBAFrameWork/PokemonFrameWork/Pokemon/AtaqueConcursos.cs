using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork.Ataque
{
    public class Concursos:PokemonFrameWorkItem
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
        public const byte ID = 0x15;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Concursos>();

        public static readonly Zona ZonaDatosConcursosHoenn;
        public static readonly Variable VariableAtaqueConcurso;
        public static readonly Variable VariableAnimacionAtaqueConcurso;


        static readonly byte[] BytesDesLimitadoAtaquesConcurso;
        static readonly byte[] BytesDesLimitadoAnimacionAtaques;
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;

        public BloqueBytes DatosConcursosHoenn { get; set; }

        static Concursos()
        {
            byte[] valoresUnLimitedAtaque = { 0 };
            byte[] valoresUnLimitedAnimacion = (((Hex)(int)ValoresLimitadoresFin.AnimacionesConcurso));

            ZonaDatosConcursosHoenn = new Zona("Datos concursos hoenn");
            VariableAtaqueConcurso = new Variable("Variable Ataque concurso");
            VariableAnimacionAtaqueConcurso = new Variable("Variable Animacion Ataque concurso");


            //añado la parte de los concursos de hoenn
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaEsp10, 0xD8248);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.EsmeraldaUsa10, 0xD85F0);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiEsp10, 0xA04C4);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroEsp10, 0xA04C4);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.RubiUsa10, 0xA0290, 0XA02B0);
            ZonaDatosConcursosHoenn.Add(EdicionPokemon.ZafiroUsa10, 0xA0290, 0XA02B0);
            //pongo las variables ataques
            VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp10, 0xD8248);
            VariableAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa10, 0xD8F0C);//puesta
            VariableAtaqueConcurso.Add(EdicionPokemon.RubiEsp10, 0xA04C4);
            VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp10, 0xA04C4);
            VariableAtaqueConcurso.Add(EdicionPokemon.RubiUsa10, 0xA0290, 0XA02B0);
            VariableAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa10, 0xA0290, 0XA02B0);
            //pongo las variables animaciones
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaEsp10, 0xD8248);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.EsmeraldaUsa10, 0xD8F0C);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiEsp10, 0xA04C4);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroEsp10, 0xA04C4);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.RubiUsa10, 0xA0290, 0XA02B0);
            VariableAnimacionAtaqueConcurso.Add(EdicionPokemon.ZafiroUsa10, 0xA0290, 0XA02B0);



            BytesDesLimitadoAtaquesConcurso = new byte[AtaqueCompleto.LENGTHLIMITADOR];
            BytesDesLimitadoAtaquesConcurso.SetArray(AtaqueCompleto.LENGTHLIMITADOR - valoresUnLimitedAtaque.Length, valoresUnLimitedAtaque);
            BytesDesLimitadoAnimacionAtaques = new byte[AtaqueCompleto.LENGTHLIMITADOR];
            BytesDesLimitadoAnimacionAtaques.SetArray(AtaqueCompleto.LENGTHLIMITADOR - valoresUnLimitedAnimacion.Length, valoresUnLimitedAnimacion);

        }
        public Concursos()
        {
            DatosConcursosHoenn = new BloqueBytes((int)LongitudCampos.DatosConcurso);
        }
        public static Concursos GetConcursos(RomGba rom, int posicion)
        {
            Concursos concursos = new Concursos();
            if (((EdicionPokemon)rom.Edicion).RegionHoenn)
            {
                //pongo los datos de los concursos de hoenn
                concursos.DatosConcursosHoenn.Bytes = BloqueBytes.GetBytes(rom.Data, Zona.GetOffsetRom(ZonaDatosConcursosHoenn, rom).Offset + posicion * OffsetRom.LENGTH, (int)LongitudCampos.DatosConcurso).Bytes;
                concursos.IdElemento = (ushort)posicion;
                concursos.IdFuente = EdicionPokemon.IDHOENN;
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
    
    }
}
