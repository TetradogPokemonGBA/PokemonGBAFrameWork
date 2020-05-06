using Gabriel.Cat.S.Utilitats;
using System;
using System.Drawing;

namespace PokemonGBAFramework.Core
{
	/// <summary>
	/// Description of MiniSprites.
	/// </summary>
	public class MiniSpriteMapa
	{

		const int TAMAÑOHEADER = 36;

		public static readonly byte[] MuestraAlgoritmo = {	 0x21, 0x03, 0x48, 0x89, 0x00 };
		public static readonly int IndexRelativo = -MuestraAlgoritmo.Length-1 + 16;


		public MiniSpriteMapa()
		{
			Minis = new Llista<BloqueSprite>();
		}
        public Llista<BloqueSprite> Minis { get; set; }
        public int LengthData => Height * Width / 2;
		public int Height { get; set; }
        public int Width { get; set; }
        public Paleta Paleta { get; set; }

        public OffsetRom OffsetImage { get; set; }
        public OffsetRom Pt1 { get; set; }
        public OffsetRom Pt2 { get; set; }
        public OffsetRom Pt3 { get; set; }
        public OffsetRom Pt5 { get; set; }

        public Bitmap this[int indexMini]
		{
			get
			{
				return Minis[indexMini].GetBitmap(Paleta);
			}
		}
		bool IsOk()
		{
			return Pt1.IsAPointer && Pt2.IsAPointer && Pt3.IsAPointer && OffsetImage.IsAPointer && Pt5.IsAPointer && Height <= (int)BloqueSprite.Medidas.MuyGrande && Width <= (int)BloqueSprite.Medidas.MuyGrande;
		}

		public static MiniSpriteMapa Get(RomGba rom, int posicion, PaletasMinisMapa paletas = default,OffsetRom offsetMiniSpritesMapaData=default,OffsetRom offsetPaletaMiniSpriteMapa=default,int totalMinis=-1)
		{
			MiniSpriteMapa sprite;
			TwoKeys<OffsetRom, int> inicioYTotal = GetTablaYTotal(rom,totalMinis,offsetMiniSpritesMapaData);
			if (Equals(paletas, default))
				paletas = PaletasMinisMapa.Get(rom, inicioYTotal.Key1);
			sprite = Get(rom, posicion, paletas, inicioYTotal.Key1, inicioYTotal.Key2);

			return sprite;
		}
		static TwoKeys<OffsetRom, int> GetTablaYTotal(RomGba rom, int totalMinis = -1, OffsetRom offsetMiniSpritesMapaData = default)
		{
			offsetMiniSpritesMapaData = Equals(offsetMiniSpritesMapaData, default) ? GetOffset(rom) : offsetMiniSpritesMapaData;
			if (totalMinis < 0)
				totalMinis =totalMinis<0? GetTotal(rom, offsetMiniSpritesMapaData) :totalMinis;


			return new TwoKeys<OffsetRom, int>(offsetMiniSpritesMapaData, totalMinis);
		}

		public static OffsetRom GetOffset(RomGba rom)
		{
			return new OffsetRom(rom, GetZona(rom));
		}

		public static int GetZona(RomGba rom)
		{
			return Zona.Search(rom, MuestraAlgoritmo, IndexRelativo);
		}

		static MiniSpriteMapa Get(RomGba rom, int posicion, PaletasMinisMapa paletas, OffsetRom offsetMiniSpritesMapaData , int totalMinis)
		{

			int offsetSprites;
			MiniSpriteMapa mini = GetDatos(rom, posicion, paletas,offsetMiniSpritesMapaData);
			//mirar de obtenerlos a todos
			offsetSprites = mini.OffsetImage.Offset;
			for (int i = 0, f = GetTotalFrames(rom, mini, offsetMiniSpritesMapaData, totalMinis); i < f; i++)
				mini.Minis.Add(BloqueSprite.Get(rom, mini.Paleta, new OffsetRom(rom, offsetSprites + i * BloqueImagen.LENGTHHEADERCOMPLETO).Offset, mini.Width, mini.Height));

			return mini;

		}

		static int GetTotalFrames(RomGba rom, MiniSpriteMapa mini, OffsetRom offsetMiniSpritesMapaData, int totalMinis)
		{
			const int POSICIONPOINTERIMG = 16 + OffsetRom.LENGTH * 3;
			
			OffsetRom offsetImg;
			bool acabado;
			int offsetHeaderActual;
			//coger los offsets de la tabla porque es donde hay los offsets de los headers....
			int total = 0;

			//busco un offset que su pointer este en la rom :) como marca fin ;)
			int offsetActual = mini.OffsetImage.Offset;

			do
			{
				total++;
				offsetActual += BloqueImagen.LENGTHHEADERCOMPLETO;
				//miro en cada offset de la tabla el header y si el cuarto pointer es el offset actual se ha acabado
				acabado = !BloqueImagen.IsHeaderOk(rom, offsetActual);
				for (int i = 0; i < totalMinis && !acabado; i++)
				{
					offsetHeaderActual = new OffsetRom(rom, offsetMiniSpritesMapaData + i * OffsetRom.LENGTH).Offset;
					//miro mini a mini si el cuarto pointer es el actual si lo es se acaba :D
					offsetImg = new OffsetRom(rom.Data.Bytes, offsetHeaderActual + POSICIONPOINTERIMG);
					acabado = offsetImg.Offset == offsetActual;

				}



			} while (!acabado);
			return total;
		}
		static MiniSpriteMapa GetDatos(RomGba rom, int posicion, PaletasMinisMapa paletas,OffsetRom offsetMiniSpritesMapaData=default)
		{
			int offsetHeader = new OffsetRom(rom, (Equals(offsetMiniSpritesMapaData, default) ? GetOffset(rom) : offsetMiniSpritesMapaData) + (posicion * OffsetRom.LENGTH)).Offset;
			byte[] bytesHeader = rom.Data.Bytes;
			MiniSpriteMapa mini = new MiniSpriteMapa();

			//obtengo las medidas del minisprite
			mini.Width = Serializar.ToInt(new byte[] {
											  bytesHeader[offsetHeader+8],
											  bytesHeader[offsetHeader+9],
											  0,
											  0
										  });
			mini.Height = Serializar.ToInt(new byte[] {
											   bytesHeader[offsetHeader+10],
											   bytesHeader[offsetHeader+11],
											   0,
											   0
										   });
			mini.Paleta = paletas[bytesHeader[offsetHeader + 2]];
			mini.Pt1 = new OffsetRom(bytesHeader, offsetHeader + 16);
			mini.Pt2 = new OffsetRom(bytesHeader, offsetHeader + 16 + OffsetRom.LENGTH);
			mini.Pt3 = new OffsetRom(bytesHeader, offsetHeader + 16 + OffsetRom.LENGTH * 2);
			mini.OffsetImage = new OffsetRom(bytesHeader, offsetHeader + 16 + OffsetRom.LENGTH * 3);
			mini.Pt5 = new OffsetRom(bytesHeader, offsetHeader + 16 + OffsetRom.LENGTH * 4);
			return mini;
		}


		public static MiniSpriteMapa[] Get(RomGba rom, PaletasMinisMapa paletas=default,int totalMinis=-1,OffsetRom offsetMinisSpriteDataMapa=default, OffsetRom offsetPaletaMiniSpriteMapa = default)
		{
			if (Equals(offsetMinisSpriteDataMapa, default))
				offsetMinisSpriteDataMapa = GetOffset(rom);
			if (Equals(offsetPaletaMiniSpriteMapa, default))
				offsetPaletaMiniSpriteMapa = PaletasMinisMapa.GetOffset(rom);
			if (Equals(paletas, default))
				paletas = PaletasMinisMapa.Get(rom, offsetPaletaMiniSpriteMapa);

			TwoKeys<OffsetRom, int> inicioYTotal = GetTablaYTotal(rom,totalMinis, offsetMinisSpriteDataMapa);
			MiniSpriteMapa[] minis = new MiniSpriteMapa[totalMinis<0?GetTotal(rom,inicioYTotal.Key1):totalMinis];

			for (int i = 0; i < minis.Length; i++)
				minis[i] = Get(rom, i, paletas, inicioYTotal.Key1, inicioYTotal.Key2);

			return minis;
		}

		public static int GetTotal(RomGba rom,OffsetRom offsetMiniSpritesMapaData=default)
		{
			//los coge bien :3
			int offsetTabla = Equals(offsetMiniSpritesMapaData, default) ? GetOffset(rom) : offsetMiniSpritesMapaData;
			int offsetActual = offsetTabla;
			OffsetRom offsetAct, offset2Act;
			//para que esté bien inicializado tengo que restar para hacer este tipo de bucle
			offsetActual -= OffsetRom.LENGTH;
			do
			{
				offsetActual += OffsetRom.LENGTH;
				offsetAct = new OffsetRom(rom, offsetActual);
				offset2Act = new OffsetRom(rom.Data, offsetAct.Offset + 16 + OffsetRom.LENGTH * 3);

			} while (offsetAct.IsAPointer && offset2Act.IsAPointer && new OffsetRom(rom, offset2Act.Offset).IsAPointer);
			return (offsetActual - offsetTabla) / OffsetRom.LENGTH;
		}

	}
}