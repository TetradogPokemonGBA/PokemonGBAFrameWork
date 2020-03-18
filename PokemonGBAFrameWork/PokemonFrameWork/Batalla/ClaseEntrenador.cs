using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.ClaseEntrenador;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class ClaseEntrenadorCompleto : PokemonFrameWorkItem
    {
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<ClaseEntrenadorCompleto>();
        public const byte ID = 0x0;
        public override byte IdTipo => ID; 
        public override ElementoBinario Serialitzer => Serializador;
        public RateMoney RateMoney { get; set; }

        public Sprite Sprite { get; set; }

        public Nombre Nombre { get; set; }




        public ClaseEntrenadorCompleto()
        {
            Sprite = new Sprite();
            Nombre = new Nombre();
            RateMoney = new RateMoney();
        }



        public override string ToString()
        {
            return Nombre.ToString();
        }

        public static ClaseEntrenadorCompleto GetClaseEntrenador(RomGba rom, int index)
        {
            ClaseEntrenadorCompleto claseCargada = new ClaseEntrenadorCompleto();
            EdicionPokemon edicion =(EdicionPokemon) rom.Edicion;
            claseCargada.Sprite = Sprite.GetSprite(rom, index);
            claseCargada.Nombre = Nombre.GetNombre(rom, index);
            claseCargada.RateMoney = RateMoney.GetRateMoney(rom, index);

            claseCargada.IdElemento = (ushort)index;
            //como cambia dependiendo de la edición Rubi&Zafrio,RojoFuego&VerdeHoja,Esmeralda
            if (edicion.EsEsmeralda)
                claseCargada.IdFuente = EdicionPokemon.IDESMERALDA;
            else if (edicion.EsRubiOZafiro)
                claseCargada.IdFuente = EdicionPokemon.IDRUBIANDZAFIRO;
            else
                claseCargada.IdFuente = EdicionPokemon.IDROJOFUEGOANDVERDEHOJA;

            return claseCargada;
        }

        public static ClaseEntrenadorCompleto[] GetClasesEntrenador(RomGba rom)
        {
            ClaseEntrenadorCompleto[] clases = new ClaseEntrenadorCompleto[Sprite.GetTotal(rom)];
            for (int i = 0; i < clases.Length; i++)
                clases[i] = GetClaseEntrenador(rom, i);
            return clases;
        }



    }
}
