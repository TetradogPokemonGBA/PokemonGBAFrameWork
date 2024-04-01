﻿using System;
using Gabriel.Cat.S.Utilitats;

using Gabriel.Cat.S.Extension;
using System.Linq;


namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of PaletasMinis.
	/// </summary>
	public class PaletasMinisMapa 
	{

		public static readonly byte[] MuestraAlgoritmo = { 0xD0, 0x00, 0x80, 0x19, 0x80, 0x88, 0xA8 };
		public static readonly int IndexRelativo = 9;

		public PaletasMinisMapa()
		{
			PaletasMinis = new Llista<Paleta>();
		}

        public Llista<Paleta> PaletasMinis { get; set; }

		public Paleta this[byte idPaleta]
		{
			get
			{
				return PaletasMinis.Where((p) => p.SortID == idPaleta).First();
			}
		}

		public static PaletasMinisMapa Get(RomGba rom,OffsetRom offsetPaletasMinis=default)
		{
			PaletasMinisMapa paletas = new PaletasMinisMapa();
			//obtengo la paleta
			offsetPaletasMinis = Equals(offsetPaletasMinis,default)?GetOffset(rom):offsetPaletasMinis;
			try
			{
				while (true)
					paletas.PaletasMinis.Add(Get(rom, paletas.PaletasMinis.Count, offsetPaletasMinis));
			}
			catch { }
			paletas.PaletasMinis.SortByQuickSort();

			return paletas;
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static int GetZona(RomGba rom)
		{
			return Zona.Search(rom, MuestraAlgoritmo, IndexRelativo);
		}

		public static Paleta Get(RomGba rom, int posicion, OffsetRom offsetPaletasMinis = default)
		{

			if (Equals(offsetPaletasMinis,default))
				offsetPaletasMinis = GetOffset(rom);

			Paleta paleta = Paleta.Get(rom, offsetPaletasMinis + posicion * Paleta.LENGTHHEADERCOMPLETO);

			return paleta;
		}
	
	}
}

