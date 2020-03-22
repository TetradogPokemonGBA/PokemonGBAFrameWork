/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 24/05/2017
 * Hora: 2:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 *
 */
using System;
using Gabriel.Cat.S.Utilitats;

using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Binaris;
using PokemonGBAFramework.Mapa.Sprites;
using PokemonGBAFramework;
using System.Collections.Generic;

namespace PokemonGBAFrameWork.Mini
{
    /// <summary>
    /// Description of PaletasMinis.
    /// </summary>
    public class Paletas 
    {

        public const byte ID = 0x13;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Paletas>();

        public static readonly Zona ZonaMiniSpritesPaleta;
		Llista<Paleta> paletas;
		static Paletas()
		{
			ZonaMiniSpritesPaleta=new Zona("Mini sprites OverWorld-Paleta");
			//Esmeralda
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.EsmeraldaUsa10,0x8E8BC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.EsmeraldaEsp10,0x8E8D0);
			//Rojo y Verde
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoUsa10,0x5F4D8,0x5F4EC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.VerdeHojaUsa10,0x5F4D8,0x5F4EC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoEsp10,0x5F5AC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.VerdeHojaEsp10,0x5F5AC);
			//Rubi y Zafiro
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RubiUsa10,0x5BE20,0x5BE40);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.ZafiroUsa10,0x5BE24,0x5BE44);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RubiEsp10,0x5C25C);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.ZafiroEsp10,0x5C260);
			
			
		}
		public Paletas()
		{
			paletas=new Llista<Paleta>();
		}

		public Llista<Paleta> PaletasMinis {
			get {
				return paletas;
			}
		}

  
        public Paleta this[byte idPaleta]
		{
			get{
				return paletas.Filtra((p)=>p.SortID==idPaleta)[0];
			}
		}

		public static Paquete GetPaletasMinis(RomGba rom)
		{
			Paquete paquete=new Paquete() { Nombre = "Paletas Minis" };
			Elemento elemento;
			//obtengo la paleta

			foreach (PaletaMini paleta in GetPaletas(rom))
			{
				elemento = new Elemento(paleta);
				paquete.ElementosNuevos.Add(elemento.Id, elemento);
			}
		
            return Poke.Extension.SetRomData(rom,paquete);
		}

		public static List<PaletaMini> GetPaletas(RomGba rom)
		{
			List<PaletaMini> paletas=new List<PaletaMini>();
			//obtengo la paleta
			int offsetTablaPaleta = Zona.GetOffsetRom(ZonaMiniSpritesPaleta, rom).Offset;
			try
			{
				while (true)
				{
					paletas.Add(GetPaletaMinis(rom, paletas.Count, offsetTablaPaleta));
				}
			}
			catch { }


			return paletas;
		}
		public static PaletaMini GetPaletaMinis(RomGba rom,int posicion,int offsetTablaPaleta = -1)
        {
            
            if (offsetTablaPaleta<0)
                offsetTablaPaleta= Zona.GetOffsetRom(ZonaMiniSpritesPaleta, rom).Offset;


            Paleta paleta= Paleta.GetPaleta(rom, offsetTablaPaleta + posicion * Paleta.LENGTHHEADERCOMPLETO);

            return new PaletaMini() { Paleta = paleta.Colores };
        }
        //falta set
	}
}
