/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 16:46
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Gabriel.Cat;
using Gabriel.Cat.Extension;

namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of EquipoPokemonEntrenador.
	/// </summary>
	public class EquipoPokemonEntrenador{
		enum Posicion
		{
			Ivs = 0,
			//posible byte en blanco
			Nivel = 1,
			//byte en blanco
			Especie = 4,
			Item = 6,

			Move1 = 8,
			Move2 = 10,
			Move3 = 12,
			Move4 = 14
		}
		enum Longitud
		{
			Nivel=2,
			Item=2,
			Ataque=2,
			PokemonIndex = 2,
		}
		public const int MAXPOKEMONENTRENADOR=6;
		
		PokemonEntrenador[] equipoPokemon;
		public EquipoPokemonEntrenador()
		{
			equipoPokemon=new PokemonEntrenador[MAXPOKEMONENTRENADOR];
		}
		public int OffsetToDataPokemon {
			get;
			set;
		}

		public PokemonEntrenador[] Equipo {
			get {
				return equipoPokemon;
			}
			set {
				
				if(value==null||value.Length==0)
				{
					value=new PokemonEntrenador[MAXPOKEMONENTRENADOR];
					value[0]=new PokemonEntrenador();
				}
				else if(value.Length>MAXPOKEMONENTRENADOR)
					value=value.SubList(0,MAXPOKEMONENTRENADOR).ToArray();
				else
					equipoPokemon.SetIList(value);
				equipoPokemon = value;
			}
		}

		public int Total
		{
			get{return equipoPokemon.Length;}
		}
		public int NumeroPokemon
		{
			get
			{
				int num = 0;
				for (int i = 0; i < equipoPokemon.Length; i++)
					if (equipoPokemon[i] != null)
						num++;
				return num;
			}
		}
		public bool HayAtaquesCustom()
		{
			const byte NOASIGNADO = 0x0;
			bool hayAtaquesCustom = false;
			for (int i = 0; i < equipoPokemon.Length && !hayAtaquesCustom; i++)
				if (equipoPokemon[i] != null)
					hayAtaquesCustom = equipoPokemon[i].Move1 != NOASIGNADO || equipoPokemon[i].Move2 != NOASIGNADO || equipoPokemon[i].Move3 != NOASIGNADO || equipoPokemon[i].Move4 != NOASIGNADO;
			return hayAtaquesCustom;
		}
		public bool HayObjetosEquipados()
		{
			const byte NOASIGNADO = 0x0;
			bool hayObjetosEquipados = false;
			for (int i = 0; i < equipoPokemon.Length && !hayObjetosEquipados; i++)
				if (equipoPokemon[i] != null)
					hayObjetosEquipados = equipoPokemon[i].Item != NOASIGNADO;
			return hayObjetosEquipados;
		}

		
		public static EquipoPokemonEntrenador GetEquipo(RomGba rom,EdicionPokemon edicion,Compilacion compilacion, int indexEntrenador)
		{
			return GetEquipo(rom,Entrenador.GetBytesEntrenador(rom,edicion,compilacion, indexEntrenador));
		}
		public static EquipoPokemonEntrenador GetEquipo(RomGba rom,BloqueBytes bloqueEntrenador)
		{
			if (rom == null || bloqueEntrenador == null)
				throw new ArgumentNullException();

			byte[] bytesPokemonEquipo;
			EquipoPokemonEntrenador equipoCargado = new EquipoPokemonEntrenador();
			bool hayItems = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] & 0x2) != 0;
			bool hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
			int tamañoPokemon = hayAtaquesCustom ? 16 : 8;
			BloqueBytes bloqueDatosEquipo = BloqueBytes.GetBytes(rom.Data,new OffsetRom(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData).Offset, bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon);
			equipoCargado.OffsetToDataPokemon = bloqueDatosEquipo.OffsetInicio;
			
			for (int i = 0, f = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons]; i < f; i++)
			{
				bytesPokemonEquipo = bloqueDatosEquipo.Bytes.SubArray(i * tamañoPokemon, tamañoPokemon);
				equipoCargado.Equipo[i] = new PokemonEntrenador();
				equipoCargado.Equipo[i].Especie =(ushort) (Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Especie, (int)Longitud.PokemonIndex)));
				equipoCargado.Equipo[i].Nivel = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Nivel, (int)Longitud.Nivel).ReverseArray());
				equipoCargado.Equipo[i].Ivs = bytesPokemonEquipo[(int)Posicion.Ivs];
				if (hayItems)
					equipoCargado.Equipo[i].Item = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Item, (int)Longitud.Item));
				if (hayAtaquesCustom)
				{
					equipoCargado.Equipo[i].Move1 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move1, (int)Longitud.Ataque).ReverseArray()); 
					equipoCargado.Equipo[i].Move2 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move2, (int)Longitud.Ataque).ReverseArray());
					equipoCargado.Equipo[i].Move3 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move3, (int)Longitud.Ataque).ReverseArray());
					equipoCargado.Equipo[i].Move4 = Serializar.ToUShort(bytesPokemonEquipo.SubArray((int)Posicion.Move4, (int)Longitud.Ataque).ReverseArray());
				}
			}

			return equipoCargado;
		}
		public static void SetEquipo(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int indexEntrenador,EquipoPokemonEntrenador equipo)
		{
			SetEquipo(rom,Entrenador.GetBytesEntrenador(rom,edicion,compilacion, indexEntrenador), equipo);
		}
		public static void SetEquipo(RomGba rom, BloqueBytes bloqueEntrenador, EquipoPokemonEntrenador equipo)
		{
			if (rom == null || bloqueEntrenador == null || equipo == null)
				throw new ArgumentNullException();
			if (equipo.NumeroPokemon == 0)
				throw new ArgumentException("Se necesita como minimo poner un pokemon en el equipo del entrenador!");

			bool habiaAtaquesCustom;
			bool hayAtaquesCustom;
			BloqueBytes bloqueEquipo;
			BloqueBytes bloquePokemon;
			int tamañoEquipoPokemon;
			int tamañoPokemon;
			int offsetEquipoOffset=new OffsetRom(bloqueEntrenador.Bytes, (int)Entrenador.Posicion.PointerPokemonData).Offset;
			habiaAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
			tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
			tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
			//borro los datos antiguos de los pokemon :D
			rom.Data.Remove(offsetEquipoOffset, tamañoEquipoPokemon);
			//el numero de pokemon
			bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] = (byte)equipo.NumeroPokemon;
			//me falta saber como va eso de las operaciones AND, XOR,OR y sus negaciones
			//hasHeldItem
			bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasHeldITem] = (byte)(equipo.HayObjetosEquipados() ? 0x2 : 0x0);

			//hasCustmoMoves
			if (equipo.HayAtaquesCustom())
				bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves]++;
			//es calculado aqui :D
			BloqueBytes.SetBytes(rom.Data, bloqueEntrenador.OffsetInicio,bloqueEntrenador);//guardo los cambios
			hayAtaquesCustom = (bloqueEntrenador.Bytes[(int)Entrenador.Posicion.HasCustomMoves] & 0x1) != 0;
			tamañoPokemon = (habiaAtaquesCustom ? 16 : 8);
			tamañoEquipoPokemon = bloqueEntrenador.Bytes[(int)Entrenador.Posicion.NumeroPokemons] * tamañoPokemon;
			bloqueEquipo = new BloqueBytes(tamañoEquipoPokemon);
			//pongo los pokemon en el bloque :D
			for (int i = 0, index = 0; i < equipo.Equipo.Length; i++)
			{
				if (equipo.equipoPokemon[i] != null)//como pueden ser null pues
				{
					bloquePokemon = new BloqueBytes(tamañoPokemon);
					//pongo los datos
					bloquePokemon.Bytes.SetArray((int)Posicion.Especie, Serializar.GetBytes(equipo.Equipo[i].Especie).ReverseArray());
					bloquePokemon.Bytes.SetArray((int)Posicion.Nivel, Serializar.GetBytes(equipo.Equipo[i].Nivel).ReverseArray());
					bloquePokemon.Bytes.SetArray((int)Posicion.Item, Serializar.GetBytes(equipo.Equipo[i].Item));
					bloquePokemon.Bytes[(int)Posicion.Ivs] = equipo.Equipo[i].Ivs;
					if (hayAtaquesCustom)
					{
						bloquePokemon.Bytes.SetArray((int)Posicion.Move1, Serializar.GetBytes(equipo.Equipo[i].Move1).ReverseArray());
						bloquePokemon.Bytes.SetArray((int)Posicion.Move2, Serializar.GetBytes(equipo.Equipo[i].Move2).ReverseArray());
						bloquePokemon.Bytes.SetArray((int)Posicion.Move3, Serializar.GetBytes(equipo.Equipo[i].Move3).ReverseArray());
						bloquePokemon.Bytes.SetArray((int)Posicion.Move4, Serializar.GetBytes(equipo.Equipo[i].Move4).ReverseArray());
					}
					bloqueEquipo.Bytes.SetArray(index * tamañoPokemon, bloquePokemon.Bytes);
					index++;
				}
			}
			
			OffsetRom.SetOffset(rom,new OffsetRom(bloqueEntrenador,bloqueEntrenador.Bytes.Length - OffsetRom.LENGTH), BloqueBytes.SetBytes(rom.Data, bloqueEquipo));//actualizo el offset de los datos :D
		}

	}
}
