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
		
		BloqueString nombre;
		
		static Mapa()
		{
			ZonaBanks=new Zona("Mapa-Banks");
			ZonaLyouts=new Zona("Mapa-Lyouts");
			ZonaNombres=new Zona("Mapa-Nombres");
			
			ZonaBanks.Add(EdicionPokemon.RojoFuegoUsa,0x5524C);
			ZonaLyouts.Add(EdicionPokemon.RojoFuegoUsa,0x55194);
			
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
		public static int GetTotal(RomData rom)
		{
			return GetTotal(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotal(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int total=-1;
			const int HEADER=4;
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
			Mapa[] mapas=new Mapa[GetTotal(rom,edicion,compilacion)];
			for(int i=0;i<mapas.Length;i++)
				mapas[i]=GetMapa(rom,edicion,compilacion,i);
			return mapas;
		}
		
	}
}
