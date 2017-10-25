/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 15/03/2017
 * Time: 17:51
 * 
 * Código bajo licencia GNU
 * creditos a Gamer2020 por hacer PGE y asi poder interpretar los bytes de los objetos :D
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Objeto.
	/// </summary>
	public class Objeto
	{
		public enum LongitudCampos
		{
			Total=44,
			Nombre=25,
			//explicacion de los 44 bytes
			NombreCompilado=14,
			Index = 2,
			Price=2,
			HoldEffect=1,
			Parameter=1,
			Descripcion=4,//es un pointer a una zona que acaba en FF
			pointerFieldUsage=4,
			KeyItemValue=1,
			BagKeyItem=1,
			Bolsillo=1,
			Tipo=1,
			BattleUsage=4,
			pointerBattleUsage=4,
			extraParameter = 4
				
		}
		
		
		public static readonly Zona ZonaDatosObjeto;
		public static readonly Zona ZonaImagenesObjeto;
		//estas dos zonas descubiertas con LSAs Complete Item Editor usando los offsets que da para facilitar la investigación
		//creditos a LocksmithArmy por la app :)
		public static readonly Zona ZonaFieldEffect;
		public static readonly Zona ZonaBattleScript;
		
		BloqueString blNombre;
		Word index;
		Word price;
		byte holdEffect;
		byte parameter;
		BloqueString blDescripcion;
		OffsetRom pointerFieldUsage;
		byte keyItemValue;
		byte bagKeyItem;
		BolsilloObjetos bolsillo;
		byte tipo;
		DWord battleUsage;
		OffsetRom pointerBattleUsage;
		DWord extraParameter;
		BloqueImagen blImagenObjeto;


		static Objeto()
		{
			ZonaDatosObjeto = new Zona("Datos Objeto");
			ZonaImagenesObjeto = new Zona("Imagen Y Paleta Objeto");
			ZonaFieldEffect=new Zona("Field effect");
			ZonaBattleScript=new Zona("Battle Script");
			
			//datos item
			ZonaDatosObjeto.Add(EdicionPokemon.ZafiroEsp, 0xA9B3C);
			ZonaDatosObjeto.Add(EdicionPokemon.RubiEsp, 0xA9B3C);
			ZonaDatosObjeto.Add(EdicionPokemon.RojoFuegoEsp, 0x1C8);
			ZonaDatosObjeto.Add(EdicionPokemon.VerdeHojaEsp, 0x1C8);
			ZonaDatosObjeto.Add(EdicionPokemon.EsmeraldaEsp, 0x1C8);
			
			ZonaDatosObjeto.Add(EdicionPokemon.ZafiroUsa, 0xA98F0, 0xA9910, 0xA9910);
			ZonaDatosObjeto.Add(EdicionPokemon.RubiUsa, 0xA98F0, 0xA9910, 0xA9910);
			ZonaDatosObjeto.Add(EdicionPokemon.EsmeraldaUsa, 0x1C8);
			ZonaDatosObjeto.Add(EdicionPokemon.RojoFuegoUsa, 0x1C8);
			ZonaDatosObjeto.Add(EdicionPokemon.VerdeHojaUsa, 0x1C8);
			//datos imagen y paleta
			ZonaImagenesObjeto.Add(EdicionPokemon.RojoFuegoUsa, 0x9899C, 0x989B0);
			ZonaImagenesObjeto.Add(EdicionPokemon.VerdeHojaUsa, 0x98970, 0x98984);
			ZonaImagenesObjeto.Add(EdicionPokemon.EsmeraldaUsa, 0x1B0034);
			//  zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaUsa, 0x1294bc);//objetos base secreta

			ZonaImagenesObjeto.Add(EdicionPokemon.EsmeraldaEsp, 0x1AFC54);
			//zonaImagenesObjetos.AddOrReplaceZonaOffset(Edicion.EsmeraldaEsp, 0x1290d4);//objetos base secreta
			ZonaImagenesObjeto.Add(EdicionPokemon.RojoFuegoEsp, 0x98B74);
			ZonaImagenesObjeto.Add(EdicionPokemon.VerdeHojaEsp, 0x98b48);
			
			//field effect
			ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoUsa,0x3DC510,0x3DC580);
			ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaUsa,0x3DC34C,0x3DC3BC);
			ZonaFieldEffect.Add(EdicionPokemon.VerdeHojaEsp,0x3D6274);
			ZonaFieldEffect.Add(EdicionPokemon.RojoFuegoEsp,0x3D6438);
			ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaUsa,0x584E88);
			ZonaFieldEffect.Add(EdicionPokemon.EsmeraldaEsp,0x587884);
			ZonaFieldEffect.Add(EdicionPokemon.RubiUsa,0x3C5580,0x3C559C);
			ZonaFieldEffect.Add(EdicionPokemon.ZafiroUsa,0x3C55D8,0x3C55F8);
			ZonaFieldEffect.Add(EdicionPokemon.RubiEsp,0x3C9018);
			ZonaFieldEffect.Add(EdicionPokemon.ZafiroEsp,0x3C8D54);
			//Battle script
			ZonaBattleScript.Add(EdicionPokemon.EsmeraldaUsa,0x5839F0);
			ZonaBattleScript.Add(EdicionPokemon.EsmeraldaEsp,0x5863EC);
			//No estoy seguro me guio por un pointer que lleva a unos datos que luego busco en las roms y vuelvo a atrás :)
			ZonaBattleScript.Add(EdicionPokemon.RojoFuegoUsa,0x3DEF08,0x3DEF78);
			ZonaBattleScript.Add(EdicionPokemon.VerdeHojaUsa,0x3DED44,0x3DEDB4);
			ZonaBattleScript.Add(EdicionPokemon.VerdeHojaEsp,0x3D8C6C);
			ZonaBattleScript.Add(EdicionPokemon.RojoFuegoEsp,0x3D8E30);
			ZonaBattleScript.Add(EdicionPokemon.RubiUsa,0x3C55B4,0x3C55D0);
			ZonaBattleScript.Add(EdicionPokemon.ZafiroUsa,0x3C560C,0x3C562C);
			ZonaBattleScript.Add(EdicionPokemon.ZafiroEsp,0xC9554);
			ZonaBattleScript.Add(EdicionPokemon.RubiEsp,0xC9554);
		}
		public Objeto()
		{
			blNombre=new BloqueString((int)LongitudCampos.NombreCompilado);
			blDescripcion=new BloqueString();
		}
		#region Propiedades
		public BloqueString Nombre {
			get {
				return blNombre;
			}
		}

		public Word Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}

		public Word Price {
			get {
				return price;
			}
			set {
				price = value;
			}
		}

		public byte HoldEffect {
			get {
				return holdEffect;
			}
			set {
				holdEffect = value;
			}
		}

		public byte Parameter {
			get {
				return parameter;
			}
			set {
				parameter = value;
			}
		}

		public byte BagKeyItem {
			get {
				return bagKeyItem;
			}
			set {
				bagKeyItem = value;
			}
		}
		public BloqueString Descripcion {
			get {
				return blDescripcion;
			}
		}
		public OffsetRom PointerFieldUsage {
			get {
				return pointerFieldUsage;
			}
			set{
				pointerFieldUsage=value;
			}
		}
		public byte KeyItemValue {
			get {
				return keyItemValue;
			}
			set {
				keyItemValue = value;
			}
		}

		public BolsilloObjetos Bolsillo {
			get {
				return bolsillo;
			}
			set {
				bolsillo = value;
			}
		}

		public byte Tipo {
			get {
				return tipo;
			}
			set {
				tipo = value;
			}
		}

		public DWord BattleUsage {
			get {
				return battleUsage;
			}
			set {
				battleUsage = value;
			}
		}
		public OffsetRom PointerBattleUsage {
			get {
				return pointerBattleUsage;
			}
			set{
				pointerBattleUsage=value;
			}
		}

		public DWord ExtraParameter {
			get {
				return extraParameter;
			}
			set {
				extraParameter = value;
			}
		}
		public BloqueImagen Sprite {
			get {
				return blImagenObjeto;
			}
		}

		#endregion

		
		public override string ToString()
		{
			return Nombre;
		}
		public static int GetTotal(RomData rom)
		{
			return GetTotal(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotal(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const byte MARCAFINNOMBRE = 0xFF,EMPTYBYTENAME=0x0;
			
			bool nombreComprobadoCorrectamente;
			BloqueBytes datosItem;
			int posicionDesripcionObjeto = (int)LongitudCampos.NombreCompilado+(int)LongitudCampos.Index+(int)LongitudCampos.Price+(int)LongitudCampos.HoldEffect+(int)LongitudCampos.Parameter;

			int totalItems = 0;
			int offsetInicio = Zona.GetOffsetRom(rom, ZonaDatosObjeto, edicion, compilacion).Offset;
			int offsetActual = offsetInicio;
			//cada objeto como minimo tiene un pointer si no lo tiene es que no tiene el formato bien :) ademas el nombre si no llega al final acaba en FF :D
			bool acabado = false;
			
			do
			{
				//mirar de actualizarlo para validar los pointers en otro lado...
				datosItem = BloqueBytes.GetBytes(rom.Data, offsetActual, (int)LongitudCampos.Total);
				//miro que el nombre acaba bien :)
				nombreComprobadoCorrectamente = false;
				acabado = new OffsetRom(rom, offsetActual).IsAPointer;//si lo que leo no es un pointer continuo
				for (int i = 0; i < (int)LongitudCampos.NombreCompilado && !acabado; i++)
				{
					if (datosItem.Bytes[i] == MARCAFINNOMBRE)
					{
						if (!nombreComprobadoCorrectamente) nombreComprobadoCorrectamente = true;

					}
					
					else if (nombreComprobadoCorrectamente&& datosItem.Bytes[i] !=EMPTYBYTENAME)
						acabado = true;//si continua es que esta mal :D
				}
				//miro el pointer
				if (!acabado)
				{
					if (new OffsetRom(datosItem.Bytes, posicionDesripcionObjeto).IsAPointer)
					{
						totalItems++;//lo ha leido bien :D
						offsetActual += (int)LongitudCampos.Total;
					}
					else acabado = true;
				}
			} while (!acabado);
			return totalItems;
		}
		public static Objeto GetObjeto(RomData rom,int index)
		{
			return GetObjeto(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		public static Objeto GetObjeto(RomGba rom,EdicionPokemon edicion,Compilacion compilacion, int index)
		{
			BloqueImagen blImg;
			int offsetImagenYPaleta;
			int offsetDatos=Zona.GetOffsetRom(rom,ZonaDatosObjeto,edicion,compilacion).Offset+index*(int)LongitudCampos.Total;
			OffsetRom aux;
			byte[] blDatos=rom.Data.SubArray(offsetDatos,(int)LongitudCampos.Total);
			Objeto objACargar=new Objeto();
			
			objACargar.Nombre.Texto=BloqueString.GetString(blDatos,0);
			objACargar.index=new Word(blDatos,14);
			objACargar.price=new Word(blDatos,16);
			objACargar.holdEffect=blDatos[17];
			objACargar.parameter=blDatos[18];
			objACargar.blDescripcion=BloqueString.GetString(rom,new OffsetRom(blDatos, 20).Offset);
			objACargar.keyItemValue=blDatos[24];
			objACargar.bagKeyItem=blDatos[25];
			objACargar.bolsillo=(BolsilloObjetos)blDatos[26];
			objACargar.tipo=blDatos[27];
			aux=new OffsetRom(blDatos,28);
			if(aux.IsAPointer)
				objACargar.pointerBattleUsage=aux;
			
			objACargar.battleUsage=new DWord(blDatos,32);
			
			aux=new OffsetRom(blDatos,36);
			if(aux.IsAPointer)
				objACargar.pointerFieldUsage=aux;
			//lo que hacia que fuera tan lento cargando era el uso de try catch!!
			objACargar.extraParameter=new DWord(blDatos,40);
			
			if(edicion.AbreviacionRom!=AbreviacionCanon.AXP&&edicion.AbreviacionRom!=AbreviacionCanon.AXV){
				offsetImagenYPaleta=Zona.GetOffsetRom(rom,ZonaImagenesObjeto,edicion,compilacion).Offset+index*(OffsetRom.LENGTH+OffsetRom.LENGTH);
				//esas ediciones no tienen imagen los objetos
				blImg=BloqueImagen.GetBloqueImagenSinHeader(rom,offsetImagenYPaleta);
				blImg.Paletas.Add(Paleta.GetPaletaSinHeader(rom,offsetImagenYPaleta+OffsetRom.LENGTH));
				objACargar.blImagenObjeto=blImg;
			}
			
			return objACargar;
			
		}
		public static Objeto[] GetObjetos(RomData rom)
		{
			return GetObjetos(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Objeto[] GetObjetos(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Objeto[] objetos=new Objeto[GetTotal(rom,edicion,compilacion)];
			for(int i=0;i<objetos.Length;i++)
				objetos[i]=GetObjeto(rom,edicion,compilacion,i);
			return objetos;
		}
		public static void SetObjeto(RomData rom,int index,Objeto objeto)
		{
			SetObjeto(rom.Rom,rom.Edicion,rom.Compilacion,index,objeto);
		}
		public static void SetObjeto(RomGba rom,EdicionPokemon edicion,Compilacion compilacion, int index,Objeto objeto)
		{
			int offsetImagenYPaleta;
			int offsetDatos=Zona.GetOffsetRom(rom,ZonaDatosObjeto,edicion,compilacion).Offset+index*(int)LongitudCampos.Total;
			
			
			try{
				BloqueString.Remove(rom,offsetDatos);
			}catch{}
			
			BloqueString.SetString(rom,offsetDatos,objeto.Nombre);
			offsetDatos+=(int)LongitudCampos.NombreCompilado;
			Word.SetWord(rom,offsetDatos,objeto.Index);//por mirar
	
			offsetDatos+=(int)LongitudCampos.Index;
			Word.SetWord(rom,offsetDatos,objeto.Index);//por mirar
			offsetDatos+=(int)LongitudCampos.Price;
			rom.Data[offsetDatos]=objeto.HoldEffect;
			offsetDatos++;
			rom.Data[offsetDatos]=objeto.Parameter;
			offsetDatos++;
			try{
				BloqueString.Remove(rom,new OffsetRom(rom,offsetDatos).Offset);
			}catch{}
			rom.Data.SetArray(offsetDatos, new	OffsetRom(BloqueString.SetString(rom,objeto.Descripcion)).BytesPointer);
			offsetDatos+=OffsetRom.LENGTH;
			rom.Data[offsetDatos]=objeto.KeyItemValue;
			offsetDatos++;
			rom.Data[offsetDatos]=objeto.BagKeyItem;
			offsetDatos++;
			rom.Data[offsetDatos]=(byte)objeto.Bolsillo;
			offsetDatos++;
			rom.Data[offsetDatos]=objeto.Tipo;
			if(objeto.PointerBattleUsage!=null)
				OffsetRom.SetOffset(rom.Data.Bytes,offsetDatos,objeto.PointerBattleUsage);
			offsetDatos+=OffsetRom.LENGTH;
			DWord.SetDword(rom,offsetDatos,objeto.BattleUsage);
			offsetDatos+=DWord.LENGTH;
			if(objeto.PointerBattleUsage!=null)
				OffsetRom.SetOffset(rom.Data.Bytes,offsetDatos,objeto.PointerFieldUsage);
			offsetDatos+=OffsetRom.LENGTH;
			DWord.SetDword(rom,offsetDatos,objeto.ExtraParameter);
			
			if(edicion.AbreviacionRom!=AbreviacionCanon.AXP&&edicion.AbreviacionRom!=AbreviacionCanon.AXV){
				offsetImagenYPaleta=Zona.GetOffsetRom(rom,ZonaImagenesObjeto,edicion,compilacion).Offset+index*(OffsetRom.LENGTH+OffsetRom.LENGTH);
				//esas ediciones no tienen imagen los objetos
				BloqueImagen.SetBloqueImagenSinHeader(rom,offsetImagenYPaleta,objeto.Sprite);
				Paleta.SetPaletaSinHeader(rom,offsetImagenYPaleta+OffsetRom.LENGTH,objeto.Sprite.Paletas[0]);
			}
			
			
			
		}
		public static void SetObjetos(RomData rom)
		{
			SetObjetos(rom.Rom,rom.Edicion,rom.Compilacion,rom.Objetos);
		}
		public static void SetObjetos(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<Objeto> objetos)
		{
			OffsetRom offsetData;
			OffsetRom offsetImg;
			int offsetActualImg;
			int totalActual=GetTotal(rom,edicion,compilacion);
			if(totalActual!=objetos.Count)
			{
				offsetData=Zona.GetOffsetRom(rom,ZonaDatosObjeto,edicion,compilacion);
				offsetImg=Zona.GetOffsetRom(rom,ZonaImagenesObjeto,edicion,compilacion);
				//borrar los datos
				rom.Data.Remove(offsetData.Offset,totalActual*(int)LongitudCampos.Total);
				//borro las imagenes y sus paletas
				offsetActualImg=offsetImg.Offset;
				for(int i=0;i<totalActual;i++)
				{
					BloqueImagen.Remove(rom,offsetActualImg);
					offsetActualImg+=OffsetRom.LENGTH;
					Paleta.Remove(rom,offsetActualImg);
					offsetActualImg+=OffsetRom.LENGTH;
				}
				//borro los punteros de las imagenes y las paletas :D
				rom.Data.Remove(offsetImg.Offset,totalActual*(OffsetRom.LENGTH*2));

				if(totalActual<objetos.Count)
				{
					//reubico las zonas :)
					OffsetRom.SetOffset(rom,offsetImg,rom.Data.SearchEmptyBytes(objetos.Count*(int)LongitudCampos.Total));
					OffsetRom.SetOffset(rom,offsetImg,rom.Data.SearchEmptyBytes(objetos.Count*OffsetRom.LENGTH*2));
				}
			}
			for(int i=0;i<objetos.Count;i++)
				SetObjeto(rom,edicion,compilacion,i,objetos[i]);
			
		}
		
		
	}
	public enum BolsilloObjetos
	{
		
		Desconocido,
		Items,
		Pokeballs,
		MTMO,
		Bayas,
		ObjetosClave
	}
	
}
