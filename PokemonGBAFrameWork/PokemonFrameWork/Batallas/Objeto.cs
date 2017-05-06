﻿/*
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
		
		BloqueString blNombre;
		short index;
		short price;
		byte holdEffect;
		byte parameter;
		BloqueString blDescripcion;
		int pointerFieldUsage;
		byte keyItemValue;
		byte bagKeyItem;
		BolsilloObjetos bolsillo;
		byte tipo;
		int battleUsage;
		int pointerBattleUsage;
		int extraParameter;
		BloqueImagen blImagenObjeto;


		static Objeto()
		{
			ZonaDatosObjeto = new Zona("Datos Objeto");
			ZonaImagenesObjeto = new Zona("Imagen Y Paleta Objeto");
			
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

		public short Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}

		public short Price {
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
		public int PointerFieldUsage {
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

		public int BattleUsage {
			get {
				return battleUsage;
			}
			set {
				battleUsage = value;
			}
		}
		public int PointerBattleUsage {
			get {
				return pointerBattleUsage;
			}
			set{
				pointerBattleUsage=value;
			}
		}

		public int ExtraParameter {
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
			
			BloqueBytes blDatos=BloqueBytes.GetBytes(rom.Data,offsetDatos,(int)LongitudCampos.Total);
			Objeto objACargar=new Objeto();
			
			objACargar.Nombre.Texto=BloqueString.GetString(blDatos,0);
			objACargar.index=Word.GetWord(blDatos.Bytes,14);
			objACargar.price=Word.GetWord(blDatos.Bytes,16);
			objACargar.holdEffect=blDatos.Bytes[17];
			objACargar.parameter=blDatos.Bytes[18];
			objACargar.blDescripcion=BloqueString.GetString(rom,new OffsetRom(blDatos, 20).Offset);
			objACargar.keyItemValue=blDatos.Bytes[24];
			objACargar.bagKeyItem=blDatos.Bytes[25];
			objACargar.bolsillo=(BolsilloObjetos)blDatos.Bytes[26];
			objACargar.tipo=blDatos.Bytes[27];
			try{
				objACargar.pointerBattleUsage=new OffsetRom(blDatos,28).Offset;
			}catch{}
			objACargar.battleUsage=DWord.GetDWord(blDatos.Bytes,32);
			try{
				objACargar.pointerFieldUsage=new OffsetRom(blDatos,36).Offset;
			}catch{}
			objACargar.extraParameter=DWord.GetDWord(blDatos.Bytes,40);
			
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
			Word.SetWord(rom,offsetDatos,objeto.Index);
			offsetDatos+=(int)LongitudCampos.Index;
			Word.SetWord(rom,offsetDatos,objeto.Price);
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
			if(objeto.PointerBattleUsage>0)
				rom.Data.SetArray(offsetDatos,new OffsetRom(objeto.PointerBattleUsage).BytesPointer);
			offsetDatos+=OffsetRom.LENGTH;
			DWord.SetDWord(rom,offsetDatos,objeto.BattleUsage);
			offsetDatos+=DWord.LENGTH;
			if(objeto.PointerBattleUsage>0)
				rom.Data.SetArray(offsetDatos,new OffsetRom(objeto.PointerFieldUsage).BytesPointer);
			offsetDatos+=OffsetRom.LENGTH;
			DWord.SetDWord(rom,offsetDatos,objeto.ExtraParameter);
			
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