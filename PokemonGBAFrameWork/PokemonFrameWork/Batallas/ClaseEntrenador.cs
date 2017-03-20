/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 21:54
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of SpriteEntrenador.
	/// </summary>
	public class ClaseEntrenador
	{
		enum Longitud
		{
			Nombre = 0xD,
			RateMoney=4,
		}
		public static readonly Zona ZonaImgSprite;
		public static readonly Zona ZonaPaletaSprite;
		public static readonly Zona ZonaNombres;
		public static readonly Zona ZonaRatesMoney;
		
		byte rateMoney;
		BloqueImagen blSprite;
		BloqueString blNombre;
		
		static ClaseEntrenador()
		{
			ZonaImgSprite = new Zona("Sprite Entrenador Img");
			ZonaPaletaSprite = new Zona("Sprite Entrenador Paleta");
			ZonaNombres = new Zona("Nombre Clase Entrenador");
			ZonaRatesMoney = new Zona("Rate Money Entrenador");
			//añado las zonas :)
			ZonaRatesMoney.Add(0x4E6A8,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaRatesMoney.Add(0x2593C,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaRatesMoney.Add(EdicionPokemon.RojoFuegoUsa, 0x259CC,0x259E0);
			ZonaRatesMoney.Add(EdicionPokemon.VerdeHojaUsa, 0x259CC, 0x259E0);
			//falta rate money Rubi y Zafiro???no hay?????
			//añado las zonas :D
			ZonaNombres.Add(EdicionPokemon.RubiUsa, 0xF7088, 0xF70A8);
			ZonaNombres.Add(EdicionPokemon.ZafiroUsa, 0xF7088, 0xF70A8);

			ZonaNombres.Add(EdicionPokemon.VerdeHojaUsa, 0xD8074, 0xD8088);
			ZonaNombres.Add(EdicionPokemon.RojoFuegoUsa, 0xD80A0, 0xD80B4);

			ZonaNombres.Add(0x183B4,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);

			ZonaNombres.Add(0x40FE8,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaNombres.Add(EdicionPokemon.RojoFuegoEsp,0xD7BF4);
			ZonaNombres.Add(EdicionPokemon.VerdeHojaEsp,0xD7BC8);

			
			//pongo las zonas :D
			//img
			ZonaImgSprite.Add(0x34628,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaImgSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3473C, 0x34750);
			ZonaImgSprite.Add(EdicionPokemon.VerdeHojaUsa, 0x3473C, 0x34750);

			ZonaImgSprite.Add( 0x31ADC,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaImgSprite.Add( 0x31CA8,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);

			ZonaImgSprite.Add( 0x5DF78,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);


			//paletas
			ZonaPaletaSprite.Add(0x34638,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaPaletaSprite.Add(EdicionPokemon.RojoFuegoUsa, 0x3474C, 0x34760);
			ZonaPaletaSprite.Add(EdicionPokemon.VerdeHojaUsa,0x3474C, 0x34760);

			ZonaPaletaSprite.Add( 0x31AF0,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaPaletaSprite.Add( 0x31CBC,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);

			ZonaPaletaSprite.Add( 0x5B784,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			

		}
		public ClaseEntrenador()
		{
			blSprite=new BloqueImagen();
			blNombre=new BloqueString((int)Longitud.Nombre);
		}

		public byte RateMoney {
			get {
				return rateMoney;
			}
			set {
				rateMoney = value;
			}
		}

		public BloqueImagen Sprite {
			get {
				return blSprite;
			}
		}

		public BloqueString Nombre {
			get {
				return blNombre;
			}
		}
		public override string ToString()
		{
			return Nombre;
		}
		public static ClaseEntrenador GetClaseEntrenador(RomData rom,int index)
		{
			return GetClaseEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		public static ClaseEntrenador GetClaseEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index)
		{
			int offsetRateMoney;
			int offsetSpriteImg=Zona.GetOffsetRom(rom,ZonaImgSprite,edicion,compilacion).Offset+index*BloqueImagen.LENGTHHEADERCOMPLETO;
			int offsetSpritePaleta=Zona.GetOffsetRom(rom,ZonaPaletaSprite,edicion,compilacion).Offset+index*Paleta.LENGTHHEADERCOMPLETO;
			int offsetNombre=Zona.GetOffsetRom(rom,ZonaNombres,edicion,compilacion).Offset+(index)*(int)Longitud.Nombre;
			ClaseEntrenador claseCargada=new ClaseEntrenador();
			
			claseCargada.blNombre=BloqueString.GetString(rom,offsetNombre);
			claseCargada.blSprite=BloqueImagen.GetBloqueImagen(rom,offsetSpriteImg);
			claseCargada.Sprite.Paletas.Add(Paleta.GetPaleta(rom,offsetSpritePaleta));
			
			if(edicion.AbreviacionRom!=AbreviacionCanon.AXP&&edicion.AbreviacionRom!=AbreviacionCanon.AXV)
			{
				offsetRateMoney=Zona.GetOffsetRom(rom,ZonaRatesMoney,edicion,compilacion).Offset+index*(int)Longitud.RateMoney;
				claseCargada.RateMoney=rom.Data[offsetRateMoney];
			}
			return claseCargada;
		}
		public static ClaseEntrenador[] GetClasesEntrenador(RomData rom)
		{
			return GetClasesEntrenador(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static ClaseEntrenador[] GetClasesEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			ClaseEntrenador[] clases=new ClaseEntrenador[GetNumeroDeClasesDeEntrenador(rom,edicion,compilacion)];
			for(int i=0;i<clases.Length;i++)
				clases[i]=GetClaseEntrenador(rom,edicion,compilacion,i);
			return clases;
		}
		public static int GetNumeroDeClasesDeEntrenador(RomData rom)
		{
			return GetNumeroDeClasesDeEntrenador(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetNumeroDeClasesDeEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int offsetTablaEntrenadorImg = Zona.GetOffsetRom(rom,ZonaImgSprite, edicion, compilacion).Offset;
			int offsetTablaEntrenadorPaleta = Zona.GetOffsetRom(rom,ZonaPaletaSprite, edicion, compilacion).Offset;
			int imgActual = offsetTablaEntrenadorImg, paletaActual = offsetTablaEntrenadorPaleta;
			int numero = 0;
			while (BloqueImagen.IsHeaderOk(rom,imgActual)&&Paleta.IsHeaderOk(rom,paletaActual))
			{
				numero++;
				imgActual += BloqueImagen.LENGTHHEADERCOMPLETO;
				paletaActual += Paleta.LENGTHHEADERCOMPLETO;
			}
			return numero;
		}
		
		public static void SetClaseEntrenador(RomData rom,ClaseEntrenador claseEntrenador,int index)
		{
			SetClaseEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,claseEntrenador,index);
			
		}
		public static void SetClaseEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,ClaseEntrenador claseEntrenador,int index)
		{
			if (rom == null|| edicion == null||compilacion==null ||claseEntrenador==null)
				throw new ArgumentNullException();

			int offsetInicioNombre;
			int offsetInicioImg;
			int offsetInicioPaleta;
			int offsetInicioRateMoney;
			
			offsetInicioNombre = Zona.GetOffsetRom(rom, ZonaNombres, edicion, compilacion).Offset+index*(int)Longitud.Nombre;
			offsetInicioImg = Zona.GetOffsetRom(rom, ZonaImgSprite, edicion, compilacion).Offset+index*OffsetRom.LENGTH;
			offsetInicioPaleta = Zona.GetOffsetRom(rom, ZonaPaletaSprite, edicion, compilacion).Offset+index*OffsetRom.LENGTH;

			//pongo los datos
			
			BloqueImagen.SetBloqueImagen(rom,offsetInicioImg, claseEntrenador.Sprite);
			Paleta.SetPaleta(rom,offsetInicioPaleta,claseEntrenador.Sprite.Paletas[0]);
			BloqueString.Remove(rom,offsetInicioNombre);
			BloqueString.SetString(rom,offsetInicioNombre,claseEntrenador.Nombre);
	
			if (edicion.AbreviacionRom != AbreviacionCanon.AXV && edicion.AbreviacionRom != AbreviacionCanon.AXP){
				offsetInicioRateMoney= Zona.GetOffsetRom(rom, ZonaRatesMoney, edicion, compilacion).Offset+(index * (int)Longitud.RateMoney);
				rom.Data.SetArray(offsetInicioRateMoney, Serializar.GetBytes(claseEntrenador.RateMoney).AddArray(new byte[] { 0x0, 0x0 }));
			}
			
		}
		public static void SetClasesEntrenador(RomData rom)
		{
			SetClasesEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,rom.ClasesEntrenadores);
		}
		public static void SetClasesEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<ClaseEntrenador> clasesEntrenador)
		{
			OffsetRom offsetInicioNombre;
			OffsetRom offsetInicioImg;
			OffsetRom offsetInicioPaleta;
			OffsetRom offsetInicioRateMoney=null;

			int offsetActualImg;
			int offsetActualPaleta;
			bool rateMoneyCompatible;
			int totalActual=GetNumeroDeClasesDeEntrenador(rom,edicion,compilacion);
			if(clasesEntrenador.Count!=totalActual)
			{
				rateMoneyCompatible=edicion.AbreviacionRom != AbreviacionCanon.AXV && edicion.AbreviacionRom != AbreviacionCanon.AXP;
				offsetInicioNombre = Zona.GetOffsetRom(rom, ZonaNombres, edicion, compilacion);
				rom.Data.Remove(offsetInicioNombre.Offset,totalActual*(int)Longitud.Nombre);
				offsetInicioImg = Zona.GetOffsetRom(rom, ZonaImgSprite, edicion, compilacion);
				offsetActualImg=offsetInicioImg.Offset;
				offsetInicioPaleta = Zona.GetOffsetRom(rom, ZonaPaletaSprite, edicion, compilacion);
				offsetActualPaleta=offsetInicioPaleta.Offset;
				
				if (rateMoneyCompatible){
					offsetInicioRateMoney= Zona.GetOffsetRom(rom, ZonaRatesMoney, edicion, compilacion);
					rom.Data.Remove(offsetInicioRateMoney.Offset,totalActual*(int)Longitud.RateMoney);
				}
				for(int i=0;i<totalActual;i++)
				{
					BloqueImagen.Remove(rom,offsetActualImg);
					offsetActualImg+=BloqueImagen.LENGTHHEADERCOMPLETO;
					Paleta.Remove(rom,offsetActualPaleta);
					offsetActualPaleta+=Paleta.LENGTHHEADERCOMPLETO;
					
				}
				//borro los datos
				if(clasesEntrenador.Count>totalActual)
				{
					//le busco sitio
					OffsetRom.SetOffset(rom,offsetInicioNombre,rom.Data.SearchEmptyBytes(clasesEntrenador.Count*(int)Longitud.Nombre));
					OffsetRom.SetOffset(rom,offsetInicioImg,rom.Data.SearchEmptyBytes(clasesEntrenador.Count*BloqueImagen.LENGTHHEADERCOMPLETO));
					OffsetRom.SetOffset(rom,offsetInicioPaleta,rom.Data.SearchEmptyBytes(clasesEntrenador.Count*BloqueImagen.LENGTHHEADERCOMPLETO));
					if(rateMoneyCompatible)
						OffsetRom.SetOffset(rom,offsetInicioRateMoney,rom.Data.SearchEmptyBytes(clasesEntrenador.Count*(int)Longitud.RateMoney));
					
				}
			}
			for(int i=0;i<clasesEntrenador.Count;i++)
				SetClaseEntrenador(rom,edicion,compilacion,clasesEntrenador[i],i);
		}
	}
}
