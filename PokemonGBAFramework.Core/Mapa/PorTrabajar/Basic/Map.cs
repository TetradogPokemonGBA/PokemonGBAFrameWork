using Gabriel.Cat.S.Drawing;
using PokemonGBAFramework.Core.Mapa.Basic.Render;
using PokemonGBAFramework.Core.Mapa.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class Map
	{
		//en el archivo pone Maps pero ahora no se muy bien...
		public static readonly byte[] MuestraAlgoritmoRubiYZafiro= {0x09, 0x48, 0x41, 0x68, 0x00, 0x68, 0x58, 0x60};
		public static readonly int IndexRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - 16;

		public static readonly byte[] MuestraAlgoritmoKanto = { 0x59, 0x60, 0x09, 0x48, 0x41, 0x68 };
		public static readonly int IndexRelativoKanto = -MuestraAlgoritmoKanto.Length - 16;

		public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x59, 0x60, 0x09, 0x48, 0x41, 0x68, 0x00, 0x68 };
		public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - 16;


		public MapHeader MapHeader { get; set; }

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static Zona GetZona(RomGba rom)
		{
			byte[] algoritmo;
			int index;
			if (rom.Edicion.EsEsmeralda)
			{
				algoritmo = MuestraAlgoritmoEsmeralda;
				index = IndexRelativoEsmeralda;

			}else if (rom.Edicion.EsKanto)
			{
				algoritmo = MuestraAlgoritmoKanto;
				index = IndexRelativoKanto;
			}
			else
			{
				algoritmo = MuestraAlgoritmoRubiYZafiro;
				index = IndexRelativoRubiYZafiro;
			}

			return Zona.Search(rom, algoritmo, index);
		}
	}

}
