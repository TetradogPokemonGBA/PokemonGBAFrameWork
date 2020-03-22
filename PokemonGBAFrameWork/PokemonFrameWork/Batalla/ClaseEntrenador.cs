using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using Poke;
using PokemonGBAFramework;
using PokemonGBAFrameWork.ClaseEntrenador;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFrameWork
{
    public class ClaseEntrenadorCompleto 
    {
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<ClaseEntrenadorCompleto>();
        public const byte ID = 0x0;

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

        public static PokemonGBAFramework.Batalla.ClaseEntrenador GetClaseEntrenador(RomGba rom, int index)
        {
            PokemonGBAFramework.Batalla.ClaseEntrenador claseCargada = new PokemonGBAFramework.Batalla.ClaseEntrenador();
            claseCargada.Sprite = Sprite.GetSprite(rom, index);
            claseCargada.Nombre = Nombre.GetNombre(rom, index);
            claseCargada.RateMoney = RateMoney.GetRateMoney(rom, index);

            return claseCargada;
        }

        public static Paquete GetClasesEntrenador(RomGba rom)
        {
            return rom.GetPaquete("Clases Entrenador",(r,i)=>GetClaseEntrenador(r,i),Sprite.GetTotal(rom));
        }



    }
}
