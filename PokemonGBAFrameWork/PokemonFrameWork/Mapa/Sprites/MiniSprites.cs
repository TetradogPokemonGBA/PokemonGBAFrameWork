/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 22/05/2017
 * Hora: 15:54
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFramework;
using PokemonGBAFramework.Mapa.Sprites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PokemonGBAFrameWork.Mini
{
	/// <summary>
	/// Description of MiniSprites.
	/// </summary>
	public class Sprite
	{
        public const byte ID = 0x12;
        const int TAMAÑOHEADER=36;
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<Sprite>();

        public static readonly Zona ZonaMiniSpritesData;
		OffsetRom pt1,pt2,pt3, pt5;//de momento solo se que el pt4 es para los frames...los demás deben de ser para algo pero no lo sé...
		Llista<BloqueSprite> blSprites;

		static Sprite()
		{
			ZonaMiniSpritesData=new Zona("Mini sprites OverWorld-Data");
			//añadir todas las zonas :D
			ZonaMiniSpritesData.Add(EdicionPokemon.RojoFuegoUsa10,0x5F2F4,0x5F308);
			ZonaMiniSpritesData.Add(EdicionPokemon.VerdeHojaUsa10,0x5F2F4,0x5F308);
			ZonaMiniSpritesData.Add(EdicionPokemon.RojoFuegoEsp10,0x5F3C8);
			ZonaMiniSpritesData.Add(EdicionPokemon.VerdeHojaEsp10,0x5F3C8);
			
			ZonaMiniSpritesData.Add(EdicionPokemon.RubiUsa10,0x5BC3C,0x5BC5C);
			ZonaMiniSpritesData.Add(EdicionPokemon.ZafiroUsa10,0x5BC40,0x5BC60);
			ZonaMiniSpritesData.Add(EdicionPokemon.RubiEsp10,0x5C078);
			ZonaMiniSpritesData.Add(EdicionPokemon.ZafiroEsp10,0x5C07C);
			
			ZonaMiniSpritesData.Add(EdicionPokemon.EsmeraldaUsa10,0x8E6D8);
			ZonaMiniSpritesData.Add(EdicionPokemon.EsmeraldaEsp10,0x8E6EC);
		}
		public Sprite()
		{
			blSprites=new Llista<BloqueSprite>();
		}
		public Llista<BloqueSprite> Minis{
			
			get{return blSprites;}
			
		}
		public int LengthData{
			get{return Height*Width/2;}
		}
        public int Height { get; set; }
        public int Width { get; set; }
        public Paleta Paleta { get; set; }

        public OffsetRom OffsetImage { get; set; }

        public Bitmap this[int indexMini]
		{
			get{
				return blSprites[indexMini].GetBitmap(Paleta);
			}
		}
		bool IsOk()
		{
			return pt1.IsAPointer&&pt2.IsAPointer&&pt3.IsAPointer&&OffsetImage.IsAPointer&&pt5.IsAPointer&&Height<=(int)BloqueSprite.Medidas.MuyGrande&&Width<=(int)BloqueSprite.Medidas.MuyGrande;
		}

		public static SpriteMini GetMiniSprite(RomGba rom,int posicion,List<PaletaMini> paletas)
		{
            if (paletas == null)
                paletas = Paletas.GetPaletas(rom);
            TwoKeys<int,int> inicioYFin=GetTablaYTotal(rom);
			//obtengo el inicio y el fin
			Sprite sprite= GetMiniSprite(rom,posicion,paletas,inicioYFin.Key1,inicioYFin.Key2);

            return new SpriteMini() { Minis = sprite.Minis.Select((mini)=>mini.GetBitmap(mini.Paleta)).ToArray() };
		}
		static TwoKeys<int,int> GetTablaYTotal(RomGba rom,int totalMinis=-1)
		{
			if(totalMinis<0)
				totalMinis=GetTotal(rom);
			
			int offsetInicioTabla=Zona.GetOffsetRom(ZonaMiniSpritesData, rom).Offset;
			return new TwoKeys<int, int>(offsetInicioTabla,totalMinis);
		}
		static Sprite GetMiniSprite(RomGba rom,int posicion,List<PaletaMini> paletas,int offsetTablaMinis,int totalMinis)
		{

			int offsetSprites;
			Sprite mini = CargarDatosMini(rom, posicion, paletas);
			//mirar de obtenerlos a todos
			offsetSprites=mini.OffsetImage.Offset;
				for(int i=0,f=GetTotalFrames(rom,mini,offsetTablaMinis,totalMinis);i<f;i++)
					mini.blSprites.Add(BloqueSprite.GetSprite(rom,mini.Paleta,new OffsetRom(rom,offsetSprites+i*BloqueImagen.LENGTHHEADERCOMPLETO).Offset,mini.Width,mini.Height));
			
			return mini;
			
		}

		static int GetTotalFrames(RomGba rom, Sprite mini,int offsetTablaMinis,int totalMinis)
		{
			const int POSICIONPOINTERIMG=16 + OffsetRom.LENGTH * 3;
			//coger los offsets de la tabla porque es donde hay los offsets de los headers....
			int total=0;

			//busco un offset que su pointer este en la rom :) como marca fin ;)
			int offsetActual=mini.OffsetImage.Offset;
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
		static Sprite CargarDatosMini(RomGba rom, int posicion, List<PaletaMini> paletas)
		{
			int offsetHeader = new OffsetRom(rom, Zona.GetOffsetRom(ZonaMiniSpritesData, rom).Offset + (posicion * OffsetRom.LENGTH)).Offset;
			byte[] bytesHeader = rom.Data.Bytes;
			Sprite mini = new Sprite();

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
			mini.Paleta =new Paleta(paletas[bytesHeader[offsetHeader+2]].Paleta);
			mini.pt1 = new OffsetRom(bytesHeader, offsetHeader+16);
			mini.pt2 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH);
			mini.pt3 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 2);
			mini.OffsetImage = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 3);
			mini.pt5 = new OffsetRom(bytesHeader,offsetHeader+ 16 + OffsetRom.LENGTH * 4);
			return mini;
		}


		public static Paquete GetMiniSprite(RomGba rom)
		{
			SpriteMini[] minis= GetMiniSprite(rom,Paletas.GetPaletas(rom));
			Elemento elemento;
			Paquete paquete = new Paquete() {Nombre= "MiniSprites" };
			for(int i=0;i<minis.Length;i++)
			{
				elemento = new Elemento(minis[i]);
				paquete.ElementosNuevos.Add(elemento.Id,elemento);
			}
			return Poke.Extension.SetRomData(rom, paquete);
		}
		public static SpriteMini[] GetMiniSprite(RomGba rom,List<PaletaMini> paletas)
		{
			TwoKeys<int,int> inicioYFin=GetTablaYTotal(rom);
			SpriteMini[] minis=new SpriteMini[GetTotal(rom)];
			for(int i=0;i<minis.Length;i++)
				minis[i] =  new SpriteMini() { Minis = GetMiniSprite(rom, i, paletas, inicioYFin.Key1, inicioYFin.Key2).Minis.Select((mini) => mini.GetBitmap(mini.Paleta)).ToArray() }; 
			
			return minis;
		}

		public static int GetTotal(RomGba rom)
		{
			//los coge bien :3
			int offsetTabla=Zona.GetOffsetRom(ZonaMiniSpritesData, rom).Offset;
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
		//falta set
	}
}
