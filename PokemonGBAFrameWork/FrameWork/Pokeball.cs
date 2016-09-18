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

            zonaIcono.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x477DC);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x477DC);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x473BC, 0x473DC, 0x473DC);
            zonaIcono.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x473BC, 0x473DC, 0x473DC);

            zonaPaletas.AddOrReplaceZonaOffset(Edicion.RubiEsp, 0x477E0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.ZafiroEsp, 0x477E0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.ZafiroUsa, 0x473C0, 0x473E0, 0x473E0);
            zonaPaletas.AddOrReplaceZonaOffset(Edicion.RubiUsa, 0x473C0, 0x473E0, 0x473E0);

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
          return new PokeballBattle(BloqueImagen.GetBloqueImagen(rom, BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.IconoPokeball, edicion, compilacion), index), BloqueImagen.GetOffsetPointerImg(Zona.GetOffset(rom, Variables.PaletaPokeball, edicion, compilacion), index)));
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
