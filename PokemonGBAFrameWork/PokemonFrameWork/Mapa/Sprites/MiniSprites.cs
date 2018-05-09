/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 22/05/2017
 * Hora: 15:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Utilitats;
using System;
using System.Drawing;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of MiniSprites.
	/// </summary>
	public class MiniSprite
	{
		const int TAMAÑOHEADER=36;
		public static readonly Zona ZonaMiniSpritesData;
		OffsetRom pt1,pt2,pt3,pt4,pt5;//de momento solo se que el pt4 es para los frames...los demás deben de ser para algo pero no lo sé...
		Llista<BloqueSprite> blSprites;
		Paleta paleta;
		int width,height;
		static MiniSprite()
		{
			ZonaMiniSpritesData=new Zona("Mini sprites OverWorld-Data");
			//añadir todas las zonas :D
			ZonaMiniSpritesData.Add(EdicionPokemon.RojoFuegoUsa,0x5F2F4,0x5F308);
			ZonaMiniSpritesData.Add(EdicionPokemon.VerdeHojaUsa,0x5F2F4,0x5F308);
			ZonaMiniSpritesData.Add(EdicionPokemon.RojoFuegoEsp,0x5F3C8);
			ZonaMiniSpritesData.Add(EdicionPokemon.VerdeHojaEsp,0x5F3C8);
			
			ZonaMiniSpritesData.Add(EdicionPokemon.RubiUsa,0x5BC3C,0x5BC5C);
			ZonaMiniSpritesData.Add(EdicionPokemon.ZafiroUsa,0x5BC40,0x5BC60);
			ZonaMiniSpritesData.Add(EdicionPokemon.RubiEsp,0x5C078);
			ZonaMiniSpritesData.Add(EdicionPokemon.ZafiroEsp,0x5C07C);
			
			ZonaMiniSpritesData.Add(EdicionPokemon.EsmeraldaUsa,0x8E6D8);
			ZonaMiniSpritesData.Add(EdicionPokemon.EsmeraldaEsp,0x8E6EC);
		}
		public MiniSprite()
		{
			blSprites=new Llista<BloqueSprite>();
		}
		public Llista<BloqueSprite> Minis{
			
			get{return blSprites;}
			
		}
		public int LengthData{
			get{return height*width/2;}
		}
		public int Height {
			get {
				return height;
			}
		}
		public int Width {
			get {
				return width;
			}
		}
		public Paleta Paleta {
			get {
				return paleta;
			}
			private set{paleta=value;}
		}

		public OffsetRom OffsetImage {
			get {
				return pt4;
			}
		}
		public Bitmap this[int indexMini]
		{
			get{
				return blSprites[indexMini].GetBitmap(paleta);
			}
		}
		bool IsOk()
		{
			return pt1.IsAPointer&&pt2.IsAPointer&&pt3.IsAPointer&&pt4.IsAPointer&&pt5.IsAPointer&&height<=(int)BloqueSprite.Medidas.MuyGrande&&width<=(int)BloqueSprite.Medidas.MuyGrande;
		}
		public static MiniSprite GetMiniSprite(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicion,PaletasMinis paletas)
		{
			TwoKeys<int,int> inicioYFin=GetTablaYTotal(rom,edicion,compilacion);
			//obtengo el inicio y el fin
			return GetMiniSprite(rom,edicion,compilacion,posicion,paletas,inicioYFin.Key1,inicioYFin.Key2);
		}
		static TwoKeys<int,int> GetTablaYTotal(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int totalMinis=-1)
		{
			if(totalMinis<0)
				totalMinis=TotalMiniSprites(rom,edicion,compilacion);
			
			int offsetInicioTabla=Zona.GetOffsetRom(rom, ZonaMiniSpritesData, edicion, compilacion).Offset;
			return new TwoKeys<int, int>(offsetInicioTabla,totalMinis);
		}
		static MiniSprite GetMiniSprite(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicion,PaletasMinis paletas,int offsetTablaMinis,int totalMinis)
		{

			int offsetSprites;
			MiniSprite mini = CargarDatosMini(rom, edicion, compilacion, posicion, paletas);
			//mirar de obtenerlos a todos
			offsetSprites=mini.pt4.Offset;
				for(int i=0,f=GetTotalFrames(rom,mini,offsetTablaMinis,totalMinis);i<f;i++)
					mini.blSprites.Add(BloqueSprite.GetSprite(rom,mini.Paleta,new OffsetRom(rom,offsetSprites+i*BloqueImagen.LENGTHHEADERCOMPLETO).Offset,mini.width,mini.height));
			
			return mini;
			
		}

		static int GetTotalFrames(RomGba rom, MiniSprite mini,int offsetTablaMinis,int totalMinis)
		{
			const int POSICIONPOINTERIMG=16 + OffsetRom.LENGTH * 3;
			//coger los offsets de la tabla porque es donde hay los offsets de los headers....
			int total=0;

			//busco un offset que su pointer este en la rom :) como marca fin ;)
			int offsetActual=mini.pt4.Offset;
			OffsetRom offsetImg;
			bool acabado=false;
			int offsetHeaderActual;
			do
			{
				total++;
				offsetActual+=BloqueImagen.LENGTHHEADERCOMPLETO;
				//miro en cada offset de la tabla el header y si el cuarto pointer es el offset actual se ha acabado
				acabado=!BloqueImagen.IsHeaderOk(rom,offsetActual);
				for(int i=0;i<totalMinis&&!acabado;i++)
				{
					offsetHeaderActual=new OffsetRom(rom,offsetTablaMinis+i*OffsetRom.LENGTH).Offset;
					//miro mini a mini si el cuarto pointer es el actual si lo es se acaba :D
					offsetImg=new OffsetRom(rom.Data.Bytes,offsetHeaderActual+ POSICIONPOINTERIMG);
					acabado=offsetImg.Offset==offsetActual;
					
				}
				
			
				
			}while(!acabado);
			return total;
		}
		static MiniSprite CargarDatosMini(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int posicion, PaletasMinis paletas)
		{
			int offsetHeader = new OffsetRom(rom, Zona.GetOffsetRom(rom, ZonaMiniSpritesData, edicion, compilacion).Offset + (posicion * OffsetRom.LENGTH)).Offset;
			byte[] bytesHeader = rom.Data.Bytes;
			MiniSprite mini = new MiniSprite();

			//obtengo las medidas del minisprite
			mini.width = Serializar.ToInt(new byte[] {
			                              	bytesHeader[offsetHeader+8],
			                              	bytesHeader[offsetHeader+9],
			                              	0,
			                              	0
			                              });
			mini.height = Serializar.ToInt(new byte[] {
			                               	bytesHeader[offsetHeader+10],
			                               	bytesHeader[offsetHeader+11],
			                               	0,
			                               	0
			                               });
			mini.Paleta = paletas[bytesHeader[offsetHeader+2]];
			mini.pt1 = new OffsetRom(bytesHeader, offsetHeader+16);
			mini.pt2 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH);
			mini.pt3 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 2);
			mini.pt4 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 3);
			mini.pt5 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 4);
			return mini;
		}


		public static MiniSprite[] GetMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return GetMiniSprites(rom,edicion,compilacion,PaletasMinis.GetPaletasMinis(rom,edicion,compilacion));
		}
		public static MiniSprite[] GetMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,PaletasMinis paletas)
		{
			TwoKeys<int,int> inicioYFin=GetTablaYTotal(rom,edicion,compilacion);
			MiniSprite[] minis=new MiniSprite[TotalMiniSprites(rom,edicion,compilacion)];
			for(int i=0;i<minis.Length;i++)
				minis[i]=GetMiniSprite(rom,edicion,compilacion,i,paletas,inicioYFin.Key1,inicioYFin.Key2);
			
			return minis;
		}

		public static int TotalMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			//los coge bien :3
			int offsetTabla=Zona.GetOffsetRom(rom, ZonaMiniSpritesData, edicion, compilacion).Offset;
			int offsetActual=offsetTabla;
			OffsetRom offsetAct,offset2Act;
			//para que esté bien inicializado tengo que restar para hacer este tipo de bucle
			offsetActual-=OffsetRom.LENGTH;
			do
			{
				offsetActual+=OffsetRom.LENGTH;
				offsetAct=new OffsetRom(rom,offsetActual);
				offset2Act=new OffsetRom(rom.Data,offsetAct.Offset+16+OffsetRom.LENGTH*3);
				
			}while(offsetAct.IsAPointer&&offset2Act.IsAPointer&&new OffsetRom(rom,offset2Act.Offset).IsAPointer);
			return (offsetActual-offsetTabla)/OffsetRom.LENGTH;
		}
		
	}
}
