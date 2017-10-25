/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 28/05/2017
 * Hora: 21:32
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	///MapHeader or Bank
	/// </summary>
	public class MapHeader
	{
		public static readonly Creditos Creditos;
		public static readonly Zona ZonaBanks;
		
		DWord bank;
		DWord indexMap;
		OffsetRom offsetHeader;
		OffsetRom offsetMap;
		OffsetRom offsetSprites;
		OffsetRom offsetScript;
		OffsetRom offsetConnector;
		
		Word hSong;
		Word hMap;
		
		byte bLabelID;
		byte bFlash;
		byte bWeather;
		byte bType;
		byte bUnused1;
		byte bUnused2;
		byte bLabelToggle;
		byte bUnused3;
		
		
		static MapHeader()
		{
			ZonaBanks=new Zona("Mapa-Banks");
			//banks maps headers
			ZonaBanks.Add(EdicionPokemon.RojoFuegoUsa,0x5524C,0x55260);
			ZonaBanks.Add(EdicionPokemon.VerdeHojaUsa,0x5524C,0x55260);
			ZonaBanks.Add(EdicionPokemon.VerdeHojaEsp,0x55340);
			ZonaBanks.Add(EdicionPokemon.RojoFuegoEsp,0x55340);
			
			ZonaBanks.Add(EdicionPokemon.EsmeraldaEsp,0x84AB8);
			ZonaBanks.Add(EdicionPokemon.EsmeraldaUsa,0x84AA4);
			
			ZonaBanks.Add(EdicionPokemon.RubiUsa,0x53324,0x53344);
			ZonaBanks.Add(EdicionPokemon.ZafiroUsa,0x53304,0x53344);
			ZonaBanks.Add(EdicionPokemon.ZafiroEsp,0x53760);
			ZonaBanks.Add(EdicionPokemon.RubiEsp,0x53760);
			
			//créditos
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.GITHUB],"shinyquagsire23","adaptado de MEH/src/us/plxhack/MEH/IO/MapHeader.java");
		}

		public int Bank {
			get {
				return bank;
			}
			set {
				bank = value;
			}
		}

		public int IndexMap {
			get {
				return indexMap;
			}
			set {
				indexMap = value;
			}
		}

		public static MapHeader GetMapHeader(RomGba rom, EdicionPokemon edicion,Compilacion compilacion,DWord bank,DWord indexMap)
		{
			int offsetActual;
			MapHeader mapa=new MapHeader();
			mapa.indexMap=indexMap;
			mapa.bank=bank;
			mapa.offsetHeader	=new OffsetRom(rom,new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaBanks,edicion,compilacion).Offset+bank*OffsetRom.LENGTH).Offset+indexMap*OffsetRom.LENGTH);
			offsetActual=mapa.offsetHeader.Offset;
			mapa.offsetHeader=new OffsetRom(offsetActual);
			mapa.offsetMap=new OffsetRom(rom,offsetActual);
			offsetActual+=OffsetRom.LENGTH;
			mapa.offsetSprites=new OffsetRom(rom,offsetActual);
			offsetActual+=OffsetRom.LENGTH;
			mapa.offsetScript=new OffsetRom(rom,offsetActual);
			offsetActual+=OffsetRom.LENGTH;
			mapa.offsetConnector=new OffsetRom(rom,offsetActual);
			offsetActual+=OffsetRom.LENGTH;
			mapa.hSong=new Word(rom,offsetActual);
			offsetActual+=Word.LENGTH;
			mapa.hMap=new Word(rom,offsetActual);
			offsetActual+=Word.LENGTH;
			mapa.bLabelID=rom.Data[offsetActual++];
			mapa.bFlash=rom.Data[offsetActual++];
			mapa.bWeather=rom.Data[offsetActual++];
			mapa.bType=rom.Data[offsetActual++];
			mapa.bUnused1=rom.Data[offsetActual++];
			mapa.bUnused2=rom.Data[offsetActual++];
			mapa.bLabelToggle=rom.Data[offsetActual++];
			mapa.bUnused3=rom.Data[offsetActual++];
			
			return mapa;

		}
		public static void SetMapHeader(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,MapHeader mapHeader)
		{
			int offsetMap	=new OffsetRom(rom,new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaBanks,edicion,compilacion).Offset+(int)mapHeader.bank*OffsetRom.LENGTH).Offset+(int)mapHeader.indexMap*OffsetRom.LENGTH).Offset;
		
			rom.Data.SetArray(offsetMap,mapHeader.offsetMap.BytesPointer);
			offsetMap+=OffsetRom.LENGTH;
			rom.Data.SetArray(offsetMap,mapHeader.offsetSprites.BytesPointer);
			offsetMap+=OffsetRom.LENGTH;
			rom.Data.SetArray(offsetMap,mapHeader.offsetScript.BytesPointer);
			offsetMap+=OffsetRom.LENGTH;
			rom.Data.SetArray(offsetMap,mapHeader.offsetConnector.BytesPointer);
			offsetMap+=OffsetRom.LENGTH;
			Word.SetWord(rom,offsetMap,mapHeader.hSong);
			offsetMap+=Word.LENGTH;
			Word.SetWord(rom,offsetMap,mapHeader.hMap);
			offsetMap+=Word.LENGTH;
			rom.Data.SetArray(offsetMap,new byte[]{mapHeader.bLabelID,mapHeader.bFlash,mapHeader.bWeather,mapHeader.bType,mapHeader.bUnused1,mapHeader.bUnused2,mapHeader.bLabelToggle,mapHeader.bUnused3});
		}
		
		public static int GetTotalBanks(RomData rom)
		{
			return GetTotalBanks(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotalBanks(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int offsetTablaBanks=Zona.GetOffsetRom(rom,ZonaBanks,edicion,compilacion).Offset;
			int total=-1;
			do{
				total++;
			}while(new OffsetRom(rom,offsetTablaBanks+total*OffsetRom.LENGTH).IsAPointer);
			return total;
		}
		public static int GetTotalMapsBank(RomData rom,int bank)
		{
			return GetTotalMapsBank(rom.Rom,rom.Edicion,rom.Compilacion,bank);
		}
		public static int GetTotalMapsBank(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int bank)
		{
			int offsetMapasBank=new OffsetRom(rom,Zona.GetOffsetRom(rom,ZonaBanks,edicion,compilacion).Offset+bank*OffsetRom.LENGTH).Offset;
			int total=-1;
			do{
				total++;
			}while(new OffsetRom(rom,offsetMapasBank+total*OffsetRom.LENGTH).IsAPointer);
			return total;
		}
	}
}
