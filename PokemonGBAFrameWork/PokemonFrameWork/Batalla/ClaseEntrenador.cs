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
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
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


        public static void SetClaseEntrenador(RomGba rom, ClaseEntrenadorCompleto claseEntrenador, int index)
        {
            if (rom == null || claseEntrenador == null)
                throw new ArgumentNullException();

            Nombre.SetNombre(rom, index, claseEntrenador.Nombre);
            Sprite.SetSprite(rom, index, claseEntrenador.Sprite);
            RateMoney.SetRateMoney(rom, index, claseEntrenador.RateMoney);

        }

        public static void SetClasesEntrenador(RomGba rom, IList<ClaseEntrenadorCompleto> clasesEntrenador)
        {
            IList<Sprite> sprites = new List<Sprite>();
            IList<Nombre> nombres = new List<Nombre>();
            IList<RateMoney> rates = new List<RateMoney>();


            for (int i = 0; i < clasesEntrenador.Count; i++)
            {
                sprites.Add(clasesEntrenador[i].Sprite);
                nombres.Add(clasesEntrenador[i].Nombre);
                rates.Add(clasesEntrenador[i].RateMoney);
            }
            Sprite.SetSprite(rom, sprites);
            Nombre.SetNombre(rom, nombres);
            RateMoney.SetRateMoney(rom, rates);
        }
    }
}
