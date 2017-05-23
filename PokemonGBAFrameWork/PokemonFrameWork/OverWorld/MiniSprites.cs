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
		public static readonly Zona ZonaMiniSpritesPaleta;
		OffsetRom pt1,pt2,pt3,pt4,pt5;//de momento solo se que el pt4 es para los frames...los demás deben de ser para algo pero no lo sé...
		Llista<BloqueSprite> blSprites;
		Paleta paleta;
		
		static MiniSprite()
		{
			ZonaMiniSpritesData=new Zona("Mini sprites OverWorld-Data");
			ZonaMiniSpritesPaleta=new Zona("Mini sprites OverWorld-Paleta");
			//añadir todas las zonas :D
			ZonaMiniSpritesData.Add(EdicionPokemon.RojoFuegoUsa,0x5F2F4);
			ZonaMiniSpritesPaleta.Add(EdicionPokemon.RojoFuegoUsa,0x5F4D8);
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
		public static MiniSprite GetMiniSprite(RomData rom,int posicion)
		{
			return GetMiniSprite(rom.Rom,rom.Edicion,rom.Compilacion,posicion);
		}
		public static MiniSprite GetMiniSprite(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int posicion)
		{
			//header
			const int MAXPALETAS=18;
			
			int offsetHeader=new OffsetRom(rom, Zona.GetOffsetRom(rom, ZonaMiniSpritesData, edicion, compilacion).Offset+(posicion*OffsetRom.LENGTH)).Offset;
			byte[] bytesHeader=rom.Data.SubArray(offsetHeader,TAMAÑOHEADER);
			int offsetBloquePaleta=Zona.GetOffsetRom(rom,ZonaMiniSpritesPaleta,edicion,compilacion).Offset;
			MiniSprite mini=new MiniSprite();
			int width,height;
			int indexPaleta;
			int offsetTablaPaleta;
			int offsetSprites;
			//obtengo las medidas del minisprite
			
			width=Serializar.ToInt(new byte[]{bytesHeader[8],bytesHeader[9],0,0});
			height=Serializar.ToInt(new byte[]{bytesHeader[10],bytesHeader[11],0,0});
			indexPaleta=bytesHeader[2];
			//obtengo la paleta
			offsetTablaPaleta=Zona.GetOffsetRom(rom,ZonaMiniSpritesPaleta,edicion,compilacion).Offset;
			for(int i=0;i<MAXPALETAS&&mini.Paleta==null;i++)
			{
				if(rom.Data[offsetTablaPaleta+(i*Paleta.LENGTHHEADERCOMPLETO)+OffsetRom.LENGTH]==indexPaleta)
					mini.Paleta=Paleta.GetPaleta(rom,offsetTablaPaleta+(i*Paleta.LENGTHHEADERCOMPLETO));
			}
			
			
			
			mini.pt1=new OffsetRom(bytesHeader,16);
			mini.pt2=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH);
			mini.pt3=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*2);
			mini.pt4=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*3);
			mini.pt5=new OffsetRom(bytesHeader,16+OffsetRom.LENGTH*4);
			
			//mirar de obtenerlos a todos
			offsetSprites=mini.pt4.Offset;
			for(int i=0,f=8;i<f;i++)//necesito saber de donde saco el total!!!
				mini.blSprites.Add(BloqueSprite.GetSprite(rom,new OffsetRom(rom,offsetSprites+i*OffsetRom.LENGTH*2).Offset,width,height));
				
			
			return mini;
			
		}
		public static MiniSprite[] GetMiniSprites(RomData rom)
		{
			return GetMiniSprites(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static MiniSprite[] GetMiniSprites(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			MiniSprite[] minis=new MiniSprite[TotalMiniSprites(rom,edicion,compilacion)];
			for(int i=0;i<minis.Length;i++)
				minis[i]=GetMiniSprite(rom,edicion,compilacion,i);
		
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
				offset2Act=new OffsetRom(rom.Data.SubArray(offsetAct.Offset,TAMAÑOHEADER),16+OffsetRom.LENGTH*3);
				
			}while(offsetAct.IsAPointer&&offset2Act.IsAPointer&&new OffsetRom(rom,offset2Act.Offset).IsAPointer);
			return (offsetActual-offsetTabla)/OffsetRom.LENGTH;
		}
		
	}
}
