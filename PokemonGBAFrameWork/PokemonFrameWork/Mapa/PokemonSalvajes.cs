/*
 * Creado por SharpDevelop.
 * Usuario: pikachu240
 * Fecha: 29/05/2017
 * Hora: 7:01
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of PokemonSalvajes.
	/// </summary>
	public class PokemonSalvajes
	{
		public enum ZonaDondeAparecen
		{
			Hierba,Agua,Arboles,Pescando
		}
		public static readonly Creditos Creditos;
		private static readonly int[] NumPokemonPorZona = new int[] { 12, 5, 5, 10 };
		const byte DNACTIVADO=0x1;
		/// <summary>
		/// Es el total de diferentes
		/// </summary>
		public const byte DNMAXINDEX=4;
		ZonaDondeAparecen zonaDondeAparecen;
		OffsetRom data;//quizas los offsets no los pongo porque no son utiles...
		OffsetRom pokemonData;
		byte ratio;
		PokemonSalvaje[][] pokemonSalvajes;//la tabla es para cada situación del parche dia y noche
		
		static PokemonSalvajes()
		{
			Creditos=new Creditos();
			Creditos.Add(Creditos.Comunidades[Creditos.GITHUB],"shinyQuagsire","por hacer MEH");
		}
		public PokemonSalvajes(ZonaDondeAparecen zona=ZonaDondeAparecen.Hierba,bool dnActivado=false)
		{
			const int NORMAL=1;
			List<PokemonSalvaje[]> aux=new List<PokemonSalvaje[]>();
			for(int i=0,f=dnActivado?DNMAXINDEX:NORMAL,numPokemon=NumPokemonPorZona[(int)zona];i<f;i++)
				aux.Add(new PokemonSalvaje[numPokemon]);
			pokemonSalvajes=aux.ToArray();
			zonaDondeAparecen=zona;
		}
		public PokemonSalvajes(PokemonSalvajes pokemonSalvajesToConvert,bool dnActivado=true):this(pokemonSalvajesToConvert.zonaDondeAparecen,dnActivado)
		{
			if(dnActivado)
			{
				for(int i=0;i<pokemonSalvajesToConvert.pokemonSalvajes.Length;i++)
					pokemonSalvajes[i]=pokemonSalvajesToConvert.pokemonSalvajes[i];
			}else{
				pokemonSalvajes[0]=pokemonSalvajesToConvert.pokemonSalvajes[0];
			}
		}
		public bool DNActivado
		{
			get{return pokemonSalvajes.Length>1;}
		}
		/// <summary>
		/// Si esta activado el sistema de dia y noche permite coger los pokemon salvajes que tiene
		/// </summary>
		public PokemonSalvaje[] this[int index]
		{
			get{return pokemonSalvajes[index];}
		}
		public static PokemonSalvajes GetPokemonSalvajes(RomGba rom,IList<Pokemon> pokedex,int offsetPokemonSalvajes,ZonaDondeAparecen zona)
		{
			const int NORMAL=1;
			PokemonSalvajes pokemonSalvajes;
			int offsetZonaActual;
			
			pokemonSalvajes=new PokemonSalvajes(zona,rom.Data[offsetPokemonSalvajes+1]==DNACTIVADO);
			pokemonSalvajes.ratio=rom.Data[offsetPokemonSalvajes];
			offsetPokemonSalvajes+=4;//ratio, dn,0,0->lo de dn si no estuviese seria otro 0
			pokemonSalvajes.pokemonData=new OffsetRom(rom,offsetPokemonSalvajes);
			
			for(int i=0;i<DNMAXINDEX&&pokemonSalvajes.DNActivado||i<NORMAL;i++){
				offsetZonaActual=new OffsetRom(rom,offsetPokemonSalvajes).Offset;
				offsetPokemonSalvajes+=OffsetRom.LENGTH;
				for(int j=0;j<pokemonSalvajes.pokemonSalvajes[i].Length;j++)
					pokemonSalvajes.pokemonSalvajes[i][j]=PokemonSalvaje.GetPokemonSalvaje(rom,pokedex,offsetZonaActual+j*PokemonSalvaje.LENGHT);
				
			}
			return pokemonSalvajes;
			
		}
	
	}
}
