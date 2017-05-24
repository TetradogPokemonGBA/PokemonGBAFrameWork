/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 24/05/2017
 * Hora: 2:46
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PaletasMinis.
	/// </summary>
	public class PaletasMinis
	{
		public static readonly Zona ZonaMiniSpritesPaleta;
		Llista<Paleta> paletas;
		static PaletasMinis()
		{
			ZonaMiniSpritesPaleta=new Zona("Mini sprites OverWorld-Paleta");
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoUsa,0x5F4D8);
		}
		public PaletasMinis()
		{
			paletas=new Llista<Paleta>();
		}

		public Llista<Paleta> Paletas {
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
		public static PaletasMinis GetPaletasMinis(RomData rom)
		{
			return GetPaletasMinis(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static PaletasMinis GetPaletasMinis(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			PaletasMinis paletas=new PaletasMinis();
			//obtengo la paleta
			int	offsetTablaPaleta=Zona.GetOffsetRom(rom,ZonaMiniSpritesPaleta,edicion,compilacion).Offset;
			try{
				while(true)
					paletas.Paletas.Add(Paleta.GetPaleta(rom,offsetTablaPaleta+paletas.Paletas.Count*Paleta.LENGTHHEADERCOMPLETO));
			}catch{}
			paletas.Paletas.Sort();
			return paletas;
		}
	}
}
