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


namespace PokemonGBAFrameWork.Mini
{
	/// <summary>
	/// Description of PaletasMinis.
	/// </summary>
	public class Paletas
	{
		public static readonly Zona ZonaMiniSpritesPaleta;
		Llista<Paleta> paletas;
		static Paletas()
		{
			ZonaMiniSpritesPaleta=new Zona("Mini sprites OverWorld-Paleta");
			//Esmeralda
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.EsmeraldaUsa,0x8E8BC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.EsmeraldaEsp,0x8E8D0);
			//Rojo y Verde
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoUsa,0x5F4D8,0x5F4EC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.VerdeHojaUsa,0x5F4D8,0x5F4EC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoEsp,0x5F5AC);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.VerdeHojaEsp,0x5F5AC);
			//Rubi y Zafiro
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RubiUsa,0x5BE20,0x5BE40);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.ZafiroUsa,0x5BE24,0x5BE44);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RubiEsp,0x5C25C);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.ZafiroEsp,0x5C260);
			
			
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

		public static Paletas GetPaletasMinis(RomGba rom)
		{
			Paletas paletas=new Paletas();
			//obtengo la paleta
			int	offsetTablaPaleta=Zona.GetOffsetRom(ZonaMiniSpritesPaleta, rom).Offset;
			try{
				while(true)
					paletas.PaletasMinis.Add(Paleta.GetPaleta(rom,offsetTablaPaleta+paletas.PaletasMinis.Count*Paleta.LENGTHHEADERCOMPLETO));
			}catch{}
			paletas.PaletasMinis.SortByQuickSort();
			return paletas;
		}
        //falta set
	}
}
