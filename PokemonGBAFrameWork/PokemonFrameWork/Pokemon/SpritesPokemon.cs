/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 12/03/2017
 * Time: 14:59
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Utilitats;
using System;
using System.ComponentModel;
using System.Drawing;


namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Sprite.
	/// </summary>
	public class SpritesPokemon
	{
		public static readonly Zona ZonaImgFrontal;
		public static readonly Zona ZonaImgTrasera;
		public static readonly Zona ZonaPaletaNormal;
		public static readonly Zona ZonaPaletaShiny;
		
		private static readonly Paleta PaletaAnimacion;
		
		public const int LONGITUDLADO=64;
		public const int TAMAÑOIMAGENDESCOMPRIMIDA = 2048;
		
		Llista<BloqueImagen> spritesFrontales;
		Llista<BloqueImagen> spritesTraseros;
		Paleta paletaNormal;
		Paleta paletaShiny;
		
		static SpritesPokemon()
		{
			ZonaImgFrontal=new Zona("Imagen Frontal Pokemon");
			ZonaImgTrasera=new Zona("Imagen Trasera Pokemon");
			ZonaPaletaNormal=new Zona("Paleta Normal");
			ZonaPaletaShiny=new Zona("Paleta Shiny");
			//Rubi y Zafiro USA tienen otras zonas
			ZonaImgFrontal.Add(0xD324,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaImgTrasera.Add(0xD3D8,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaPaletaNormal.Add(EdicionPokemon.RubiUsa,0x40954,0x40974);
			ZonaPaletaNormal.Add(EdicionPokemon.ZafiroUsa,0x40954,0x40974);
			ZonaPaletaShiny.Add(EdicionPokemon.RubiUsa,0x4098C,0x409AC);
			ZonaPaletaShiny.Add(EdicionPokemon.ZafiroUsa,0x4098C,0x409AC);
			//los demas todos iguales :)
			ZonaImgFrontal.Add(0x128,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);
			ZonaImgTrasera.Add(0x12C,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);
			ZonaPaletaNormal.Add(0x130,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);
			ZonaPaletaShiny.Add(0x134,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp,EdicionPokemon.EsmeraldaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.VerdeHojaUsa);

			
			PaletaAnimacion = new Paleta();

            for (int i = 1; i < Paleta.LENGTH; i++)
            {

            	PaletaAnimacion.Colores[i] =System.Drawing.Color.Black;
            }

            PaletaAnimacion.Colores[0] = System.Drawing.Color.White;
		}
		
		public SpritesPokemon()
		{
			spritesFrontales=new Llista<BloqueImagen>();
			spritesTraseros=new Llista<BloqueImagen>();
			paletaNormal=new Paleta();
			paletaShiny=new Paleta();
		}

		public Llista<BloqueImagen> SpritesFrontales {
			get {
				return spritesFrontales;
			}
		}

		public Llista<BloqueImagen> SpritesTraseros {
			get {
				return spritesTraseros;
			}
		}

		public Paleta PaletaNormal {
			get {
				return paletaNormal;
			}
		}

		public Paleta PaletaShiny {
			get {
				return paletaShiny;
			}
		}
		
	    public  BitmapAnimated GetAnimacionImagenFrontal(bool isShiny = false)
        {
            Bitmap[] gifAnimated = new Bitmap[spritesFrontales.Count+ 2];
            int[] delay = new int[gifAnimated.Length];
            Paleta paleta = isShiny ? PaletaShiny : PaletaNormal;
            gifAnimated[1] = spritesFrontales[0]+PaletaAnimacion;
            delay[1] = 200;
            for (int i = 2, j = 0; i < gifAnimated.Length; i++, j++)
            {
                gifAnimated[i] = spritesFrontales[j] + paleta;
                delay[i] = 500;
            }
            gifAnimated[0] = gifAnimated[2];
            BitmapAnimated bmpAnimated = gifAnimated.ToAnimatedBitmap(false, delay);
            
            return bmpAnimated;
        }
	    
	    
		public static SpritesPokemon GetSpritesPokemon(RomData rom,int indexOrdenGameFreakPokemon)
		{
			return GetSpritesPokemon(rom.Rom,rom.Edicion,rom.Compilacion,indexOrdenGameFreakPokemon);
		}
		public static SpritesPokemon GetSpritesPokemon(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int indexOrdenGameFreakPokemon)
		{
			byte[] auxImg;
			
			int offsetImgFrontalPokemon=Zona.GetOffsetRom(rom,ZonaImgFrontal,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetImgTraseraPokemon=Zona.GetOffsetRom(rom,ZonaImgTrasera,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaNormalPokemon=Zona.GetOffsetRom(rom,ZonaPaletaNormal,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaShinyPokemon=Zona.GetOffsetRom(rom,ZonaPaletaShiny,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			
			Paleta paletaNormal=Paleta.GetPaleta(rom,offsetPaletaNormalPokemon);
			Paleta paletaShiny=Paleta.GetPaleta(rom,offsetPaletaShinyPokemon);
			BloqueImagen bloqueImgFrontal=BloqueImagen.GetBloqueImagen(rom,offsetImgFrontalPokemon);
			BloqueImagen bloqueImgTrasera=BloqueImagen.GetBloqueImagen(rom,offsetImgTraseraPokemon);
			
			SpritesPokemon spritePokemon=new SpritesPokemon();
			
			
			spritePokemon.paletaNormal=paletaNormal;
			spritePokemon.paletaShiny=paletaShiny;
			
			auxImg=bloqueImgFrontal.DatosDescomprimidos.Bytes;
			for(int i=0,f=auxImg.Length/TAMAÑOIMAGENDESCOMPRIMIDA,pos=0;i<f;i++,pos+=TAMAÑOIMAGENDESCOMPRIMIDA)
			{
				spritePokemon.spritesFrontales.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos,TAMAÑOIMAGENDESCOMPRIMIDA)),paletaNormal,paletaShiny));
			}
			
			auxImg=bloqueImgTrasera.DatosDescomprimidos.Bytes;
			for(int i=0,f=auxImg.Length/TAMAÑOIMAGENDESCOMPRIMIDA,pos=0;i<f;i++,pos+=TAMAÑOIMAGENDESCOMPRIMIDA)
			{
				spritePokemon.spritesTraseros.Add(new BloqueImagen(new BloqueBytes(auxImg.SubArray(pos,TAMAÑOIMAGENDESCOMPRIMIDA)),paletaNormal,paletaShiny));
			}
			
			return spritePokemon;
		}
		
		public static void SetSpritesPokemon(RomData rom,int indexOrdenGameFreakPokemon,SpritesPokemon spritesPokemon)
		{
			 SetSpritesPokemon(rom.Rom,rom.Edicion,rom.Compilacion,indexOrdenGameFreakPokemon,spritesPokemon);
		}
		public static void SetSpritesPokemon(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int indexOrdenGameFreakPokemon,SpritesPokemon spritesPokemon)
		{
			byte[] auxImg;
			
			int offsetImgFrontalPokemon=Zona.GetOffsetRom(rom,ZonaImgFrontal,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetImgTraseraPokemon=Zona.GetOffsetRom(rom,ZonaImgTrasera,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaNormalPokemon=Zona.GetOffsetRom(rom,ZonaPaletaNormal,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaShinyPokemon=Zona.GetOffsetRom(rom,ZonaPaletaShiny,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			BloqueImagen bloqueCompleto;

			auxImg=new byte[spritesPokemon.SpritesFrontales.Count*TAMAÑOIMAGENDESCOMPRIMIDA];
			for(int i=0,pos=0;i<spritesPokemon.SpritesFrontales.Count;i++,pos+=TAMAÑOIMAGENDESCOMPRIMIDA)
			{
				auxImg.SetArray(pos,spritesPokemon.SpritesFrontales[i].DatosDescomprimidos.Bytes);
			}
			
			bloqueCompleto=new BloqueImagen(new BloqueBytes(auxImg));
			bloqueCompleto.Id=(short)indexOrdenGameFreakPokemon;
			bloqueCompleto.Offset= new OffsetRom(rom, offsetImgFrontalPokemon).Offset;
			//pongo las nuevas imagenes
			BloqueImagen.SetBloqueImagen(rom,offsetImgFrontalPokemon,bloqueCompleto);
			
			auxImg=new byte[spritesPokemon.SpritesTraseros.Count*TAMAÑOIMAGENDESCOMPRIMIDA];
			for(int i=0,pos=0;i<spritesPokemon.SpritesTraseros.Count;i++,pos+=TAMAÑOIMAGENDESCOMPRIMIDA)
			{
				auxImg.SetArray(pos,spritesPokemon.SpritesTraseros[i].DatosDescomprimidos.Bytes);
			}
			
			bloqueCompleto.DatosDescomprimidos.Bytes=auxImg;
			bloqueCompleto.Offset= new OffsetRom(rom, offsetImgTraseraPokemon).Offset;
			//pongo las nuevas imagenes
			BloqueImagen.SetBloqueImagen(rom,offsetImgTraseraPokemon,bloqueCompleto);
            //pongo las paletas :)
            spritesPokemon.PaletaNormal.Id =(short) indexOrdenGameFreakPokemon;
            spritesPokemon.PaletaShiny.Id = (short)indexOrdenGameFreakPokemon;
            Paleta.SetPaleta(rom,spritesPokemon.PaletaNormal);
			Paleta.SetPaleta(rom,spritesPokemon.PaletaShiny);
			
			
		}
		
		public static void Remove(RomGba rom, EdicionPokemon edicion, Compilacion compilacion, int indexOrdenGameFreakPokemon)
		{
			int offsetImgFrontalPokemon=Zona.GetOffsetRom(rom,ZonaImgFrontal,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetImgTraseraPokemon=Zona.GetOffsetRom(rom,ZonaImgTrasera,edicion,compilacion).Offset+BloqueImagen.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaNormalPokemon=Zona.GetOffsetRom(rom,ZonaPaletaNormal,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			int offsetPaletaShinyPokemon=Zona.GetOffsetRom(rom,ZonaPaletaShiny,edicion,compilacion).Offset+Paleta.LENGTHHEADERCOMPLETO*indexOrdenGameFreakPokemon;
			//borro los datos
			try{
			BloqueImagen.Remove(rom,offsetImgFrontalPokemon);
			}catch{}
			try{
			BloqueImagen.Remove(rom,offsetImgTraseraPokemon);
			}catch{}
			try{
			Paleta.Remove(rom,offsetPaletaNormalPokemon);
			}catch{}
			try{
			Paleta.Remove(rom,offsetPaletaShinyPokemon);
			}catch{}
			//borro los headers
			
			rom.Data.Remove(offsetImgFrontalPokemon,BloqueImagen.LENGTHHEADERCOMPLETO);
			rom.Data.Remove(offsetImgTraseraPokemon,BloqueImagen.LENGTHHEADERCOMPLETO);
			rom.Data.Remove(offsetPaletaNormalPokemon,Paleta.LENGTHHEADERCOMPLETO);
			rom.Data.Remove(offsetPaletaShinyPokemon,Paleta.LENGTHHEADERCOMPLETO);
		}
	}
}
