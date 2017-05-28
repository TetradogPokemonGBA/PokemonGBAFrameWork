/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 27/05/2017
 * Hora: 14:49
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Mapa.
	/// </summary>
	public class Mapa
	{
		public static readonly Zona ZonaBanks;
		public static readonly Zona ZonaLyouts;
		public static readonly Zona ZonaNombres;
		
		public static readonly Variable VariableTotalMapas;
		public static readonly Variable VariableTotalNombresMapas;
		BloqueString nombre;
		
		static Mapa()
		{
			ZonaBanks=new Zona("Mapa-Banks");
			ZonaLyouts=new Zona("Mapa-Lyouts");
			ZonaNombres=new Zona("Mapa-Nombres");
			VariableTotalMapas=new Variable("Total mapas");
			VariableTotalNombresMapas=new Variable("Total nombres mapas");
			
			//banks
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
			//lyouts
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
			ZonaNombres.Add(EdicionPokemon.EsmeraldaUsa,0x123B44);
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
		#region Bank
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
		
		public static Mapa GetMapa(RomData rom,int index)
		{
			return GetMapa(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		public static Mapa GetMapa(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index)
		{
			
			int offsetNombreMapa=GetOffsetNombre(rom,edicion,Zona.GetOffsetRom(rom,ZonaNombres,edicion,compilacion).Offset,index).Offset;//obtengo el offset del mapa
			Mapa mapa=new Mapa();
			mapa.Nombre=BloqueString.GetString(rom,offsetNombreMapa);
			return mapa;
		}
		public static Mapa[] GetMapas(RomData rom)
		{
			return GetMapas(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Mapa[] GetMapas(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Mapa[] mapas=new Mapa[GetTotalNombres(rom,edicion,compilacion)];
			for(int i=0;i<mapas.Length;i++)
				mapas[i]=GetMapa(rom,edicion,compilacion,i);
			return mapas;
		}
		
	}
}
