namespace PokemonGBAFramework.Core.Mapa
{
    public class NombreMapa
	{

		public static readonly byte[] MuestraAlgoritmoEsmeralda = { 0x44, 0xA1, 0x03, 0x02, 0x33 };
		public static readonly int IndexRelativoEsmeralda = -MuestraAlgoritmoEsmeralda.Length - OffsetRom.LENGTH;

		public static readonly byte[] MuestraAlgoritmoKanto = { 0x46, 0x42, 0x0F, 0x01, 0x02, 0x00 };
		public const int IndexRelativoKanto = 0;

		public static readonly byte[] MuestraAlgoritmoRubiYZafiro = { 0x29, 0x2D, 0x06, 0xD0, 0x33, 0x2D };
		public static readonly int IndexRelativoRubiYZafiro = -MuestraAlgoritmoRubiYZafiro.Length - OffsetRom.LENGTH;


		public const string CaracterEspecialRubiYZafiro = "\\c";

		public BloqueString Texto { get; set; }

		public override string ToString()
		{
			return Texto.ToString().Replace(CaracterEspecialRubiYZafiro,"");
		}
		public static NombreMapa Get(RomGba rom, int index, OffsetRom offsetMapsLabels = default)
		{
			const int DATALENGTHHOENN = 4;

			if (Equals(offsetMapsLabels, default))
				offsetMapsLabels = GetOffset(rom);

			int offset;
			int lengthHeader;

			if (rom.Edicion.EsHoenn)
			{
				lengthHeader = OffsetRom.LENGTH;


				lengthHeader += DATALENGTHHOENN;

				offset = offsetMapsLabels + index * lengthHeader;


				offset += DATALENGTHHOENN;
			}
			else
			{
				offset = offsetMapsLabels + (((index & 0xFF) - 0x58) * 4);
			}
			return new NombreMapa() { Texto = BloqueString.Get(rom, new OffsetRom(rom, offset)) };
		}
		public static NombreMapa[] Get(RomGba rom, OffsetRom offsetMapsLabels = default)
		{
			if (Equals(offsetMapsLabels, default))
				offsetMapsLabels = GetOffset(rom);

			NombreMapa[] mapLabels = new NombreMapa[GetTotal(rom, offsetMapsLabels)];

			for (int i = 0; i < mapLabels.Length; i++)
				mapLabels[i] = Get(rom, i, offsetMapsLabels);

			return mapLabels;
		}
		public static int GetTotal(RomGba rom, OffsetRom offsetMapsLabels = default)
		{
			const int DATALENGTHHOENN = 4;

			if (Equals(offsetMapsLabels, default))
				offsetMapsLabels = GetOffset(rom);
			
			int offset;
			int lengthHeader=OffsetRom.LENGTH;
			int offsetTabla = offsetMapsLabels;
			int total = 0;

			if (rom.Edicion.EsHoenn)
				lengthHeader += DATALENGTHHOENN;

			do
			{
				
				offset = offsetTabla + total * lengthHeader;

				if (rom.Edicion.EsHoenn)
					offset += DATALENGTHHOENN;

				total++;

			} while (OffsetRom.Check(rom,offset));

			return total-1;
		}
		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static Zona GetZona(RomGba rom)
		{
			Zona zona;

			if (rom.Edicion.EsKanto)
			{
				zona = new Zona(rom.Data.SearchArray(new OffsetRom(Zona.Search(rom, MuestraAlgoritmoKanto, IndexRelativoKanto).Offset).BytesPointer));

			}
			else if (rom.Edicion.EsEsmeralda)
			{
				zona = Zona.Search(rom, MuestraAlgoritmoEsmeralda, IndexRelativoEsmeralda);
			}
			else
			{
				zona = Zona.Search(rom, MuestraAlgoritmoRubiYZafiro, IndexRelativoRubiYZafiro);
			}
			return zona;
		}
	}

}
