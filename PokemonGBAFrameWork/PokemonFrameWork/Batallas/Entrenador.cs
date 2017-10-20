/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 10/03/2017
 * Time: 10:53
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
	/// Description of Entrenador.
	/// </summary>
	public class Entrenador:ObjectAutoId
	{
		public enum Posicion
		{
			HasCustomMoves = 0,
			HasHeldITem = 0,
			MoneyClass = 1,
			EsChica = 2,
			Musica = 2,
			Sprite = 3,
			Nombre = 4,
			//faltan [14,15]
			Item1 = 16,
			Item2 = 18,
			Item3 = 20,
			Item4 = 22,
			//faltan [23,27]
			Inteligencia = 28,
			NumeroPokemons = 32,
			PointerPokemonData = 36

		}
		enum Longitud
		{
			Nombre=10,
			Inteligencia=4,
			Item = 2,
		}
		public const byte MAXMUSIC = 0x7F;
		public const byte LENGTH = 0x28;
		public static readonly  Zona ZonaEntrenador;
		
		byte trainerClass;
		bool esUnaEntrenadora;
		byte musicaBatalla;
		byte spriteIndex;
		BloqueString nombre;//max 10
		//faltan 2 bytes [14,15]
		ushort item1;
		ushort item2;
		ushort item3;
		ushort item4;
		//faltan 4 bytes
		int inteligencia;
		
		EquipoPokemonEntrenador equipo;
		static Entrenador()
		{
			ZonaEntrenador = new Zona("Entrenador");
			//añado las zonas :D
			ZonaEntrenador.Add(0x3587C,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaEntrenador.Add(0xD890,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaEntrenador.Add(0xDA5C,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaEntrenador.Add(EdicionPokemon.VerdeHojaUsa, 0xFC00,0xFC14);
			ZonaEntrenador.Add(EdicionPokemon.RojoFuegoUsa, 0xFC00,0xFC14);
			ZonaEntrenador.Add(0xFB70,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
		}
		public Entrenador()
		{
			nombre=new BloqueString((int)Longitud.Nombre);
		}

		public BloqueString Nombre {
			get {
				return nombre;
			}
		}
		public EquipoPokemonEntrenador EquipoPokemon {
			get {
				return equipo;
			}
			set {
				equipo = value;
			}
		}

		public byte TrainerClass {
			get {
				return trainerClass;
			}
			set {
				trainerClass = value;
			}
		}

		public bool EsUnaEntrenadora {
			get {
				return esUnaEntrenadora;
			}
			set {
				esUnaEntrenadora = value;
			}
		}

		public byte MusicaBatalla {
			get {
				return musicaBatalla;
			}
			set {
				musicaBatalla = value;
			}
		}

		public byte SpriteIndex {
			get {
				return spriteIndex;
			}
			set {
				spriteIndex = value;
			}
		}

		public ushort Item1 {
			get {
				return item1;
			}
			set {
				item1 = value;
			}
		}

		public ushort Item2 {
			get {
				return item2;
			}
			set {
				item2 = value;
			}
		}

		public ushort Item3 {
			get {
				return item3;
			}
			set {
				item3 = value;
			}
		}

		public ushort Item4 {
			get {
				return item4;
			}
			set {
				item4 = value;
			}
		}

		public int Inteligencia {
			get {
				return inteligencia;
			}
			set {
				inteligencia = value;
			}
		}
		public uint CalcularDinero(RomGba rom)
		{
			uint tamañoPokemonBytes = 8;
			if (EquipoPokemon.HayAtaquesCustom())
			{
				tamañoPokemonBytes = 16;
			}
			return (TrainerClass * (uint)(rom.Data.Bytes[((uint)EquipoPokemon.NumeroPokemon * tamañoPokemonBytes + EquipoPokemon.OffsetToDataPokemon - tamañoPokemonBytes + 2)] << 2));
		}
		public override string ToString()
		{
			return Nombre;
		}
		public static int GetNumeroDeEntrenadores(RomData rom)
		{
			return GetNumeroDeEntrenadores(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetNumeroDeEntrenadores(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			const byte POSICIONPOINTERDATOS= 0x24;

			ushort num = 1;
			int posicionEntrenadores = Zona.GetOffsetRom(rom, ZonaEntrenador, edicion, compilacion).Offset;
			int posicionActual = posicionEntrenadores + POSICIONPOINTERDATOS+LENGTH;
			while (new OffsetRom(rom, posicionActual).IsAPointer)
			{
				num++;
				posicionActual += LENGTH;
			}
			return num-1;
		}
		public static Entrenador GetEntrenador(RomData rom,int index)
		{
			return GetEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		public static Entrenador GetEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index)
		{

			BloqueBytes bytesEntrenador = GetBytesEntrenador(rom,edicion,compilacion, index);
			Entrenador entranadorCargado = new Entrenador();
			
			//le pongo los datos
			entranadorCargado.EsUnaEntrenadora = (bytesEntrenador.Bytes[(int)Posicion.EsChica] & 0x80) != 0;
			entranadorCargado.MusicaBatalla =(byte)(bytesEntrenador.Bytes[(int)Posicion.Musica] & MAXMUSIC);
			entranadorCargado.TrainerClass = bytesEntrenador.Bytes[(int)Posicion.MoneyClass];//quizas es la clase de entrenador :D y no el rango de dinero que da...
			entranadorCargado.Nombre.Texto = BloqueString.GetString(bytesEntrenador, (int)Posicion.Nombre, (int)Longitud.Nombre);
			entranadorCargado.Inteligencia = DWord.GetDWord(bytesEntrenador.Bytes,(int)Posicion.Inteligencia);//mirar si es asi :D
			entranadorCargado.Item1= Word.GetWord(bytesEntrenador.Bytes,(int)Posicion.Item1);//mirar si es asi :D
			entranadorCargado.Item2 = Word.GetWord(bytesEntrenador.Bytes,(int)Posicion.Item2);//mirar si es asi :D
			entranadorCargado.Item3 = Word.GetWord(bytesEntrenador.Bytes,(int)Posicion.Item3);//mirar si es asi :D
			entranadorCargado.Item4 = Word.GetWord(bytesEntrenador.Bytes,(int)Posicion.Item4);//mirar si es asi :D
			entranadorCargado.SpriteIndex = bytesEntrenador.Bytes[(int)Posicion.Sprite];
			entranadorCargado.EquipoPokemon = EquipoPokemonEntrenador.GetEquipo(rom, bytesEntrenador);
			
			
			return entranadorCargado;

		}
		public static BloqueBytes GetBytesEntrenador(RomData rom, int index)
		{
			return GetBytesEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,index);
		}
		public static BloqueBytes GetBytesEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion, int index)
		{
			const byte TAMAÑOENTRENADOR = 0x28;
			int posicionEntrenadores = Zona.GetOffsetRom(rom, ZonaEntrenador,edicion, compilacion).Offset;
			int poscionEntrenador = posicionEntrenadores + TAMAÑOENTRENADOR * (index+1);//el primero me lo salto porque no es un entrenador...
			return BloqueBytes.GetBytes(rom.Data, poscionEntrenador, TAMAÑOENTRENADOR);
		}
		public static Entrenador[] GetEntrenadores(RomData rom)
		{
			return GetEntrenadores(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Entrenador[] GetEntrenadores(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Entrenador[] entrenadores = new Entrenador[GetNumeroDeEntrenadores(rom,edicion,compilacion)];
			for (int i = 0; i < entrenadores.Length; i++)
				entrenadores[i] = GetEntrenador(rom,edicion,compilacion, i);

			
			return entrenadores;
		}
		public static void SetEntrenador(RomData rom,int index,Entrenador entrenador)
		{
			SetEntrenador(rom.Rom,rom.Edicion,rom.Compilacion,index,entrenador);
		}
		public static void SetEntrenador(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int index,Entrenador entrenador)
		{
			BloqueBytes bloqueEntrenador = GetBytesEntrenador(rom,edicion,compilacion, index);
			bloqueEntrenador.Bytes[(int)Posicion.Musica] = entrenador.MusicaBatalla;

			if (entrenador.EsUnaEntrenadora)
			{
				bloqueEntrenador.Bytes[(int)Posicion.EsChica] += 0x80;//va asi???por mirar
			}
			
			bloqueEntrenador.Bytes[(int)Posicion.MoneyClass]= entrenador.TrainerClass;
			BloqueString.SetString(rom,bloqueEntrenador.OffsetInicio + (int)Posicion.Nombre, entrenador.Nombre);
			DWord.SetDWord(bloqueEntrenador.Bytes,(int)Posicion.Inteligencia,entrenador.Inteligencia);//mirar si va asi :D
			Word.SetWord(bloqueEntrenador.Bytes,(int)Posicion.Item1,entrenador.Item1);//mirar si va asi :D
			Word.SetWord(bloqueEntrenador.Bytes,(int)Posicion.Item2,entrenador.Item2);//mirar si va asi :D
			Word.SetWord(bloqueEntrenador.Bytes,(int)Posicion.Item3,entrenador.Item3);//mirar si va asi :D
			Word.SetWord(bloqueEntrenador.Bytes,(int)Posicion.Item4,entrenador.Item4);//mirar si va asi :D
			bloqueEntrenador.Bytes[(int)Posicion.Sprite]= entrenador.SpriteIndex;
			//pongo los datos
			EquipoPokemonEntrenador.SetEquipo(rom, bloqueEntrenador,entrenador.EquipoPokemon);
			rom.Data.SetArray(bloqueEntrenador.OffsetInicio,bloqueEntrenador.Bytes);
		}
		public static void SetEntrenadores(RomData romData)
		{
			SetEntrenadores(romData.Rom,romData.Edicion,romData.Compilacion,romData.Entrenadores);
		}
		public static void SetEntrenadores(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<Entrenador> entrenadores)
		{
			int totalActual=GetNumeroDeEntrenadores(rom,edicion,compilacion);
			OffsetRom offsetData;
			if(entrenadores.Count!=totalActual)
			{
				offsetData=Zona.GetOffsetRom(rom,ZonaEntrenador,edicion,compilacion);
				
				rom.Data.Remove(offsetData.Offset,totalActual*LENGTH);
				//borro los datos
				if(entrenadores.Count>totalActual)
				{
					//busco un nuevo lugar para los datos
					OffsetRom.SetOffset(rom,offsetData,rom.Data.SearchEmptyBytes(LENGTH*entrenadores.Count));
				}
			}
			for (int i = 0; i < entrenadores.Count; i++)
				SetEntrenador(rom,edicion,compilacion, i, entrenadores[i]);
		}
	}
}
