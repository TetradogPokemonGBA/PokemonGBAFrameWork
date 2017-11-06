/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 27/05/2017
 * Hora: 14:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Mapa.
	/// </summary>
	public class Mapa
	{
		
		public static readonly Zona ZonaLyouts;
		public static readonly Zona ZonaNombres;
		
		public static readonly Variable VariableTotalMapas;
		public static readonly Variable VariableTotalNombresMapas;
		
		
		BloqueString nombre;
		MapHeader header;
		
		
		static Mapa()
		{
			
			ZonaLyouts=new Zona("Mapa-Lyouts");
			ZonaNombres=new Zona("Mapa-Nombres");
			VariableTotalMapas=new Variable("Total mapas");
			VariableTotalNombresMapas=new Variable("Total nombres mapas");
			
			
			//lyouts Maps
			ZonaLyouts.Add(EdicionPokemon.RojoFuegoUsa,0x55194,0x551A8);
			ZonaLyouts.Add(EdicionPokemon.VerdeHojaUsa,0x55194,0x551A8);
			ZonaLyouts.Add(EdicionPokemon.VerdeHojaEsp,0x55288);
			ZonaLyouts.Add(EdicionPokemon.RojoFuegoEsp,0x55288);
			ZonaLyouts.Add(EdicionPokemon.EsmeraldaEsp,0x849E0);
			ZonaLyouts.Add(EdicionPokemon.EsmeraldaUsa,0x849CC);//por el formato puedo dar con el total
			ZonaLyouts.Add(EdicionPokemon.RubiUsa,0x5326C,0x5328C);
			ZonaLyouts.Add(EdicionPokemon.ZafiroUsa,0x5326C,0x5328C);
			ZonaLyouts.Add(EdicionPokemon.RubiEsp,0x536A8);
			ZonaLyouts.Add(EdicionPokemon.ZafiroEsp,0x536A8);
			
			//nombres //en algunos sitio hay un espacio y no se si afecta al numero de mapas...por mirar
			ZonaNombres.Add(EdicionPokemon.RojoFuegoUsa,0xC0C94,0xC0CA8);
			ZonaNombres.Add(EdicionPokemon.VerdeHojaUsa,0xC0C68,0xC0C7C);
			ZonaNombres.Add(EdicionPokemon.EsmeraldaUsa,0x123B44);//map labels
			ZonaNombres.Add(EdicionPokemon.RubiUsa,0xFB550,0xFB570);
			ZonaNombres.Add(EdicionPokemon.ZafiroUsa,0xFB550,0xFB570);
			ZonaNombres.Add(EdicionPokemon.EsmeraldaEsp,0x12375C);
			ZonaNombres.Add(EdicionPokemon.RubiEsp,0xFB9E0);
			ZonaNombres.Add(EdicionPokemon.ZafiroEsp,0xFB9E0);
			ZonaNombres.Add(EdicionPokemon.VerdeHojaEsp,0xC0F00);
			ZonaNombres.Add(EdicionPokemon.RojoFuegoEsp,0xC0F2C);
			
			//total nombres mapas
			VariableTotalNombresMapas.Add(EdicionPokemon.RojoFuegoUsa,0xC4D8A,0xC4D9E);
			VariableTotalNombresMapas.Add(EdicionPokemon.VerdeHojaUsa,0xC4D5E,0xC4D72);
			VariableTotalNombresMapas.Add(EdicionPokemon.VerdeHojaEsp,0xC4FDE);
			VariableTotalNombresMapas.Add(EdicionPokemon.RojoFuegoEsp,0xC500A);
			VariableTotalNombresMapas.Add(EdicionPokemon.EsmeraldaUsa,0x124584);
			VariableTotalNombresMapas.Add(EdicionPokemon.EsmeraldaEsp,0x12419C);
			VariableTotalNombresMapas.Add(EdicionPokemon.RubiUsa,0xFBFCC,0xFBFEC);
			VariableTotalNombresMapas.Add(EdicionPokemon.ZafiroUsa,0xFBFCC,0xFBFEC);
			VariableTotalNombresMapas.Add(EdicionPokemon.RubiEsp,0xFC45C);
			VariableTotalNombresMapas.Add(EdicionPokemon.ZafiroEsp,0xFC45C);
			//total mapas
			VariableTotalMapas.Add(EdicionPokemon.RojoFuegoUsa,0xC0BE6,0xC0BFA);
			VariableTotalMapas.Add(EdicionPokemon.VerdeHojaUsa,0xC0BBA,0xC0BCE);
			VariableTotalMapas.Add(EdicionPokemon.VerdeHojaEsp,0xC0E52);
			VariableTotalMapas.Add(EdicionPokemon.RojoFuegoEsp,0xC0E7E);
			
			
			//de momento nose que es pero es algo importante:)
			//.Add(EdicionPokemon.EsmeraldaEsp,0x84AB8);1E 00 00 00 1E 00 00 00 ptr ptr ptr//para poder buscar en mas roms
			//.Add(EdicionPokemon.EsmeraldaUsa,0x84AA4);
		}
		public Mapa()
		{
			nombre=new BloqueString();
		}

		public MapHeader Header {
			get {
				return header;
			}
		}

		public BloqueString Nombre {
			get {
				return nombre;
			}
			set{nombre=value;}
		}
		#region nombre
		public static int GetTotalNombres(RomData rom)
		{
			return GetTotalNombres(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotalNombres(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int total=-1;
			OffsetRom offsetActual;
			int offsetNombres=Zona.GetOffsetRom(rom,ZonaNombres,edicion,compilacion).Offset;//obtengo el offset a la tabla de nombres
			
			do{
				total++;
				offsetActual=GetOffsetNombre(rom,edicion,offsetNombres,total);//
				
			}while(offsetActual.IsAPointer);
			
			return total;
			
		}
		static OffsetRom GetOffsetNombre(RomGba rom,EdicionPokemon edicion,int offsetTabla,int index)
		{
			int header=0;
			switch (edicion.AbreviacionRom) {
				case AbreviacionCanon.AXV:
				case AbreviacionCanon.AXP:
					header=4;
					break;
				case AbreviacionCanon.BPE:
					header=4;
					break;
				case AbreviacionCanon.BPR:
				case AbreviacionCanon.BPG:
					header=0;
					break;
			}
			return new OffsetRom(rom,offsetTabla+header+index*(header+OffsetRom.LENGTH));
			
			
		}
		#endregion
		
		#region Lyout
		public static int GetTotalLyouts(RomData rom)
		{
			return GetTotalLyouts(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotalLyouts(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const int HEADERLYOUT=8;
			int offsetTablaPunterosLyouts=Zona.GetOffsetRom(rom,ZonaLyouts,edicion,compilacion).Offset;
			int offsetActual=0;
			int total=-1;
			OffsetRom offsetAComprobar;
			do{
				total++;
				offsetAComprobar=new OffsetRom(rom,offsetTablaPunterosLyouts+total*OffsetRom.LENGTH);
				if(offsetAComprobar.IsAPointer)
					offsetActual=offsetAComprobar.Offset+HEADERLYOUT;
			}while(new OffsetRom(rom,offsetActual).IsAPointer);//no se si es un bug pero en el esmeralda detecta 443 y tendrian que ser 441
			return total;
		}
		#endregion
		
		public static Mapa GetMapa(RomData rom,DWord bank,DWord indexMap)
		{
			return GetMapa(rom.Rom,rom.Edicion,rom.Compilacion,bank,indexMap);
		}
		public static Mapa GetMapa(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,DWord bank,DWord indexMap)
		{
			
			int offsetNombreMapa=GetOffsetNombre(rom,edicion,Zona.GetOffsetRom(rom,ZonaNombres,edicion,compilacion).Offset,(int)indexMap).Offset;//obtengo el offset del mapa
			Mapa mapa=new Mapa();
			mapa.Nombre=BloqueString.GetString(rom,offsetNombreMapa);
			mapa.header=MapHeader.GetMapHeader(rom,edicion,compilacion,bank,indexMap);
			return mapa;
		}
		public static IList<Mapa>  GetMapas(RomData rom)
		{
			return GetMapas(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static IList<Mapa>  GetMapas(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			List<Mapa> mapas=new List<Mapa>();
			for(int b=0,bf=MapHeader.GetTotalBanks(rom,edicion,compilacion);b<bf;b++)
				mapas.AddRange(GetMapasBank(rom,edicion,compilacion,new DWord(b)));
			return mapas;
		}
		public static IList<Mapa>  GetMapasBank(RomData rom,DWord bank)
		{
			return GetMapasBank(rom.Rom,rom.Edicion,rom.Compilacion,bank);
		}
		public static IList<Mapa> GetMapasBank(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,DWord bank)
		{
			List<Mapa> mapas=new List<Mapa>();
			for(int m=0,mf=1/*MapHeader.GetTotalMapsBank(rom,edicion,compilacion,bank)*/;m<mf;m++)
				mapas.Add(GetMapa(rom,edicion,compilacion,bank,new DWord(m)));
			return mapas;
		}
		
		
		
	}
}
