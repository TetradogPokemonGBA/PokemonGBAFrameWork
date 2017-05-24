/*
 * Creado por SharpDevelop.
 * Usuario: tetra
 * Fecha: 22/05/2017
 * Hora: 15:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using Gabriel.Cat;

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

		public Paleta Paleta {
			get {
				return paleta;
			}
			private set{paleta=value;}
		}

		public Bitmap this[int indexMini]
		{
			get{
				return blSprites[indexMini].GetBitmap(paleta);
			}
		}
		public static MiniSprite GetMiniSprite(RomData rom,int posicion,PaletasMinis paletas,int totalFrames=8)
		{
			return GetMiniSprite(rom.Rom,rom.Edicion,rom.Compilacion,posicion,paletas,totalFrames);
		}
		public static MiniSprite GetMiniSprite(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicion,PaletasMinis paletas,int totalFrames=8)
		{
			//header
			
			
			int offsetHeader=new OffsetRom(rom, Zona.GetOffsetRom(rom, ZonaMiniSpritesData, edicion, compilacion).Offset+(posicion*OffsetRom.LENGTH)).Offset;
			byte[] bytesHeader=rom.Data.SubArray(offsetHeader,TAMAÑOHEADER);
			MiniSprite mini=new MiniSprite();
			int width,height;
			int offsetSprites;
			//obtengo las medidas del minisprite
			
			width=Serializar.ToInt(new byte[]{bytesHeader[8],bytesHeader[9],0,0});
			height=Serializar.ToInt(new byte[]{bytesHeader[10],bytesHeader[11],0,0});
			if(posicion==151)
				System.Diagnostics.Debugger.Break();
			mini.Paleta=paletas[bytesHeader[2]];
			
			
			
			
			mini.pt1=new OffsetRom(bytesHeader,16);
			mini.pt2=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH);
			mini.pt3=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*2);
			mini.pt4=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*3);
			mini.pt5=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*4);
			
			//mirar de obtenerlos a todos
			offsetSprites=mini.pt4.Offset;
			for(int i=0;i<totalFrames;i++)//necesito saber de donde saco el total!!!
				mini.blSprites.Add(BloqueSprite.GetSprite(rom,new OffsetRom(rom,offsetSprites+i*BloqueImagen.LENGTHHEADERCOMPLETO).Offset,width,height));
			
			
			return mini;
			
		}
		public static MiniSprite[] GetMiniSprites(RomData rom)
		{
			return GetMiniSprites(rom,PaletasMinis.GetPaletasMinis(rom));
		}
		public static MiniSprite[] GetMiniSprites(RomData rom,PaletasMinis paletas)
		{
			return GetMiniSprites(rom.Rom,rom.Edicion,rom.Compilacion,paletas);
		}
		public static MiniSprite[] GetMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			return GetMiniSprites(rom,edicion,compilacion,PaletasMinis.GetPaletasMinis(rom,edicion,compilacion));
		}
		public static MiniSprite[] GetMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,PaletasMinis paletas)
		{
			MiniSprite[] minis=new MiniSprite[TotalMiniSprites(rom,edicion,compilacion)];
			for(int i=0;i<minis.Length;i++)
				minis[i]=GetMiniSprite(rom,edicion,compilacion,i,paletas);
			
			return minis;
		}
		public static int TotalMiniSprites(RomData rom)
		{
			return TotalMiniSprites(rom.Rom,rom.Edicion,rom.Compilacion);
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
				offset2Act=new OffsetRom(rom.Data.SubArray(offsetAct.Offset,TAMAÑOHEADER),16+OffsetRom.LENGTH*3);
				
			}while(offsetAct.IsAPointer&&offset2Act.IsAPointer&&new OffsetRom(rom,offset2Act.Offset).IsAPointer);
			return (offsetActual-offsetTabla)/OffsetRom.LENGTH;
		}
		
	}
}
