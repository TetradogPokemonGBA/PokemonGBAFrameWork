using Gabriel.Cat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonGBAFrameWork
{
   public class PokeballBattle
    {
        //no son las pokeballs de los items son de la animacion de capturar :)
        public enum Variables
        {
            IconoPokeball,PaletaPokeball
        }
        static PokeballBattle()
        {
            Zona zonaIcono=new Zona(Variables.IconoPokeball);
            Zona zonaPaletas = new Zona(Variables.PaletaPokeball);

            zonaIcono.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.RojoFuegoUsa, 0x1D4);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.VerdeHojaUsa, 0x1D4);

            zonaIcono.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.RojoFuegoEsp, 0x1D4);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.VerdeHojaEsp, 0x1D4);

            zonaIcono.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1D4);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1D0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1D4);

            Zona.DiccionarioOffsetsZonas.Añadir(new Zona[] { zonaPaletas, zonaIcono });
        }
        BloqueImagen icono;

        public PokeballBattle(BloqueImagen icono)
        {
            this.icono = icono;
        }
        public BloqueImagen Icono
        {
            get
            {
                return icono;
            }

            private set
            {
                icono = value;
            }
        }
        public static PokeballBattle GetPokeballBattle(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion,Hex index)
        {
          return new PokeballBattle(BloqueImagen.GetBloqueImagen(rom, BloqueImagen.GetOffsetImg(rom,Zona.GetOffset(rom, Variables.IconoPokeball, edicion, compilacion),index), BloqueImagen.LongitudImagen.L16, Paleta.GetPaleta(rom, BloqueImagen.GetOffsetImg(rom, Zona.GetOffset(rom, Variables.PaletaPokeball, edicion, compilacion), index))));
        }
        public static PokeballBattle[] GetPokeballsBattle(RomGBA rom, Edicion edicion, CompilacionRom.Compilacion compilacion)
        {
            List<PokeballBattle> pokeballs = new List<PokeballBattle>();
            Hex index=0;
            try
            {
                while (true)
                    pokeballs.Add(GetPokeballBattle(rom, edicion, compilacion, index++));
            } catch { }
            return pokeballs.ToArray();
        }
    }
}
