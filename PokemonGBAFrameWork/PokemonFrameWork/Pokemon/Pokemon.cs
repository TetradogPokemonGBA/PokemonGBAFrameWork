/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 14/03/2017
 * Time: 18:01
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
	/// Description of Pokemon.
	/// </summary>
	public class Pokemon:IComparable
	{
		public enum LongitudCampos
		{
			NombreCompilado = 11,
			Nombre = NombreCompilado + 2,//para los Nidoran[f] se guarda [f] como un caracter...de alli que le sume 2 '[',']'
			TotalStats = 28,
		}
		public enum OrdenPokemon
		{
			GameFreak,
			Local,
			Nacional
		}
		public enum NivelEvs
		{
			Cero,
			Uno,
			Dos,
			Tres
		}
		public enum GrupoHuevo
		{ Ninguno, Monstruo, Agua1, Bicho, Volador, Campo, Hada, Hierba, FormaHumana, Agua3, Mineral, Amorfo, Agua2, Ditto, Dragón, Desconocido }
		public enum StatEvs
		{
			Hp,
			Ataque,
			Velocidad,
			Defensa,
			AtaqueEspecial,
			DefensaEspecial
		}

		public enum RatioCrecimiento
		{
			Exp1000000,
			Exp600000,
			Exp1640000,
			Exp1059860,
			Exp800000,
			Exp1250000
		}

		public enum RatioGenero
		{
			Macho100 = 0,
			//no se puede poner el caracter %
			Macho87 = 31,
			Macho75 = 63,
			Macho65 = 89,
			Macho50Hembra = 127,
			Hembra65 = 165,
			Hembra75 = 191,
			Hembra87 = 223,
			Hembra100 = 254,
			SinGenero = 255
		}
		public enum Felicidad
		{
			Minima = 0,
			Baja = 35,
			Normal = 70,
			MediAlta = 90,
			Alta = 100,
			MuyAlta = 140,
			Maxima = 255

		}
		public enum Color
		{
			Rojo,
			Azul,
			Amarillo,
			Verde,
			Negro,
			Marron,
			Púrpura,
			Gris,
			Blanco,
			Rosa

		}
		
		
		const int PASOSCICLOECLOSION = 256, MAXIMOSPASOSECLOSION = PASOSCICLOECLOSION * PASOSCICLOECLOSION;
		const int LENGTHNIVELEVS = 4;
		/// <summary>
		/// forma parte de un stat junto con la de color en el mismo byte numero 25
		/// </summary>
		const byte FACELEFT = 128;
		public static OrdenPokemon Orden = OrdenPokemon.Nacional;
		public static readonly Zona ZonaOrdenLocal;
		public static readonly Zona ZonaOrdenNacional;
		public static readonly Zona ZonaNombre;
		public static readonly Zona ZonaStats;
		
		int ordenNacional;
		int ordenLocal;
		int ordenGameFreak;
		
		int objeto1, objeto2;
		
		BloqueString blNombre;
		DescripcionPokedex descripcion;
		SpritesPokemon sprites;
		BloqueBytes blStats;
		Cry cry;
		Growl growl;
		Huella huella;
		AtaquesAprendidos ataquesAprendidos;
		
		static Pokemon()
		{
			ZonaOrdenLocal = new Zona("Orden Local");
			ZonaOrdenNacional = new Zona("Orden Nacional");
			ZonaNombre = new Zona("Nombre Pokemon");
			ZonaStats = new Zona("Stats Pokemon");
			//orden local
			ZonaOrdenLocal.Add(0x3F9BC,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaOrdenLocal.Add(0x3F7F0,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaOrdenLocal.Add(0x430DC,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaOrdenLocal.Add(EdicionPokemon.RojoFuegoUsa, 0x431F0, 0x43204);
			ZonaOrdenLocal.Add(EdicionPokemon.VerdeHojaUsa, 0x431F0, 0x43204);
			ZonaOrdenLocal.Add( 0x6D3FC,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);

			//orden nacional
			ZonaOrdenNacional.Add(0x3FA08,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);
			ZonaOrdenNacional.Add(0x3F83C,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaOrdenNacional.Add(0x43128,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp);
			ZonaOrdenNacional.Add(EdicionPokemon.RojoFuegoUsa, 0x4323C, 0x43250);
			ZonaOrdenNacional.Add(EdicionPokemon.VerdeHojaUsa, 0x4323C, 0x43250);
			ZonaOrdenNacional.Add(0x6D448,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);

			//nombre
			ZonaNombre.Add(0x144,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaNombre.Add(0xFA58,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);

			//stats
			ZonaStats.Add(0x1BC,EdicionPokemon.RojoFuegoUsa,EdicionPokemon.VerdeHojaUsa,EdicionPokemon.RojoFuegoEsp,EdicionPokemon.VerdeHojaEsp,EdicionPokemon.EsmeraldaUsa,EdicionPokemon.EsmeraldaEsp);
			ZonaStats.Add(0x10B64,EdicionPokemon.RubiUsa,EdicionPokemon.ZafiroUsa);
			ZonaStats.Add(0x10D30,EdicionPokemon.RubiEsp,EdicionPokemon.ZafiroEsp);

		}
		public Pokemon()
		{
			blNombre=new BloqueString((int)LongitudCampos.Nombre);
			blStats=new BloqueBytes((int)LongitudCampos.TotalStats);
			descripcion=new DescripcionPokedex();
			sprites=new SpritesPokemon();
			cry=new Cry();
			growl=new Growl();
			huella=new Huella();
			ataquesAprendidos=new AtaquesAprendidos();
		}
		
		public int OrdenNacional
		{
			get{return ordenNacional;}
			set{ordenNacional=value;}
		}

		public int OrdenLocal {
			get {
				return ordenLocal;
			}
			set {
				ordenLocal = value;
			}
		}

		public int OrdenGameFreak {
			get {
				return ordenGameFreak;
			}
			set {
				ordenGameFreak = value;
			}
		}
		
		public BloqueString Nombre {
			get {
				return blNombre;
			}
			set {
				blNombre = value;
			}
		}

		public DescripcionPokedex Descripcion {
			get {
				return descripcion;
			}
			set {
				descripcion = value;
			}
		}

		public SpritesPokemon Sprites {
			get {
				return sprites;
			}
			set {
				sprites = value;
			}
		}

		public BloqueBytes Stats {
			get {
				return blStats;
			}
			set {
				blStats = value;
			}
		}
		#region Interpreta	Stats
		public int TotalStatsBase
		{
			get
			{
				int totalStatsBase = 0;
				for (int i = 0; i < 6; i++)
					totalStatsBase += blStats.Bytes[i];
				return totalStatsBase;

			}
		}


		public byte Hp
		{
			get { return blStats.Bytes[0]; }
			set
			{
				blStats.Bytes[0] = value;

			}
		}
		public byte Ataque
		{
			get { return blStats.Bytes[1]; }
			set
			{
				blStats.Bytes[1] = value;
			}
		}


		public byte Defensa
		{
			get { return blStats.Bytes[2]; }
			set
			{
				blStats.Bytes[2] = value;
			}
		}
		public byte Velocidad
		{
			get { return blStats.Bytes[3]; }
			set
			{
				blStats.Bytes[3] = value;
			}
		}
		public byte AtaqueEspecial
		{
			get { return blStats.Bytes[4]; }
			set
			{
				blStats.Bytes[4] = value;
			}
		}
		public byte DefensaEspecial
		{
			get { return blStats.Bytes[5]; }
			set
			{
				blStats.Bytes[5] = value;
			}
		}
		public byte Tipo1
		{
			get
			{

				return blStats.Bytes[6];
			}
			set
			{

				blStats.Bytes[6] = value;

			}
		}
		public byte Tipo2
		{
			get
			{
				return blStats.Bytes[7];
			}
			set
			{

				blStats.Bytes[7] = value;

			}
		}
		public byte RatioCaptura
		{
			get { return blStats.Bytes[8]; }
			set
			{
				blStats.Bytes[8] = value;
			}
		}
		public byte ExperienciaBase
		{
			get { return blStats.Bytes[9]; }
			set
			{
				blStats.Bytes[9] = value;
			}
		}

		//SPE,DEF,ATK,HP
		#region EvsStats se tiene que testear :)
		public NivelEvs HpEvs
		{
			get
			{
				int posicion = (blStats.Bytes[10] - ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) - ((int)AtaqueEvs) * LENGTHNIVELEVS) / LENGTHNIVELEVS;//le quito lo anterior
				return (NivelEvs)posicion;
			}
			set
			{
				if (value < NivelEvs.Uno || value > NivelEvs.Tres)
					throw new ArgumentOutOfRangeException("value");
				blStats.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)value);

			}
		}
		public NivelEvs AtaqueEvs
		{
			get
			{
				int posicion = ((blStats.Bytes[10] - (((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)))) - ((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3))) / LENGTHNIVELEVS;//le quito lo anterior
				return (NivelEvs)posicion;
			}
			set
			{
				if (value < NivelEvs.Uno || value > NivelEvs.Tres)
					throw new ArgumentOutOfRangeException("value");
				blStats.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)value) * LENGTHNIVELEVS + (int)HpEvs);

			}

		}
		public NivelEvs DefensaEvs
		{
			get
			{
				int posicion = (blStats.Bytes[10] - (((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)))) / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2));//le quito lo anterior
				return (NivelEvs)posicion;
			}
			set
			{
				if (value < NivelEvs.Uno || value > NivelEvs.Tres)
					throw new ArgumentOutOfRangeException("value");
				blStats.Bytes[11] = (byte)(((int)VelocidadEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

			}

		}
		public NivelEvs VelocidadEvs
		{
			get
			{
				int posicion = blStats.Bytes[10] / Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3));//le quito lo anterior
				return (NivelEvs)posicion;
			}
			set
			{
				if (value < NivelEvs.Uno || value > NivelEvs.Tres)
					throw new ArgumentOutOfRangeException("value");
				blStats.Bytes[11] = (byte)(((int)value) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 3)) + ((int)DefensaEvs) * Convert.ToInt32(Math.Pow(LENGTHNIVELEVS, 2)) + ((int)AtaqueEvs) * LENGTHNIVELEVS + (int)HpEvs);

			}

		}
		public NivelEvs AtaqueEspecialEvs
		{
			get
			{
				int posicion = blStats.Bytes[11] - ((int)DefensaEspecialEvs) * LENGTHNIVELEVS;
				return (NivelEvs)posicion;
			}
			set
			{
				blStats.Bytes[11] = (byte)(((int)DefensaEspecialEvs) * LENGTHNIVELEVS + (int)value);
			}
		}
		public NivelEvs DefensaEspecialEvs
		{
			get
			{
				int posicion = blStats.Bytes[11] / LENGTHNIVELEVS;
				return (NivelEvs)posicion;
			}
			set
			{
				blStats.Bytes[11] = (byte)(((int)value) * LENGTHNIVELEVS + (int)AtaqueEspecialEvs);
			}
		}
		#endregion

		//item1 indexItems([12](%)+[13](/)*256);
		public int Objeto1
		{
			get
			{
				return objeto1;
			}
			set
			{
				if (value < 0 || value > short.MaxValue)
					throw new ArgumentOutOfRangeException();
				objeto1 = value;
			}
		}
		//item2 indexItems([14](%)+[15](/)*256);
		public int Objeto2
		{
			get
			{
				return objeto2;
			}
			set
			{
				if (value < 0 || value > short.MaxValue)
					throw new ArgumentOutOfRangeException();
				objeto2 = value;
			}
		}

		public RatioGenero RatioSexo
		{
			get { return (RatioGenero)(int)blStats.Bytes[16]; }
			set
			{
				if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
					throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
				blStats.Bytes[16] = (byte)(int)value;
			}
		}
		/// <summary>
		/// Se usan multiplos de 256 ya que se guarda en un byte
		/// </summary>
		public int PasosParaEclosionarHuevo
		{
			get { return (int)blStats.Bytes[17] * PASOSCICLOECLOSION; }
			set
			{
				if ((int)value > MAXIMOSPASOSECLOSION || (int)value < 0)
					throw new ArgumentOutOfRangeException("value", "el valor no puede más pequeño que 0 o ser más grande que " + MAXIMOSPASOSECLOSION);
				blStats.Bytes[17] = (byte)(value / PASOSCICLOECLOSION);
			}
		}


		/// <summary>
		/// Se puede poner valores de 0 a FF
		/// </summary>
		public Felicidad BaseAmistad
		{
			get { return (Felicidad)(int)blStats.Bytes[18]; }
			set
			{
				if ((int)value > byte.MaxValue || (int)value < byte.MinValue)
					throw new ArgumentOutOfRangeException("value", "el valor se guarda en un byte!");
				blStats.Bytes[18] = (byte)(int)value;
			}
		}
		public RatioCrecimiento Crecimiento
		{
			get { return (RatioCrecimiento)(int)blStats.Bytes[19]; }//solo se usa la posicion de la enumeracion para determinar su crecimiento
			set
			{
				if (value < RatioCrecimiento.Exp1000000 || value > RatioCrecimiento.Exp1250000)
					throw new ArgumentOutOfRangeException(String.Format("el valor no puede superar el numero {0} y tiene que ser positivo", Enum.GetNames(typeof(RatioCrecimiento)).Length - 1));
				blStats.Bytes[19] = (byte)(int)value;
			}
		}
		public GrupoHuevo GrupoHuevo1
		{
			get { return (GrupoHuevo)blStats.Bytes[20]; }
			set
			{
				blStats.Bytes[20] = (byte)value;
			}
		}
		public GrupoHuevo GrupoHuevo2
		{
			get { return (GrupoHuevo)blStats.Bytes[21]; }
			set
			{
				blStats.Bytes[21] = (byte)value;
			}
		}
		public byte Habilidad1
		{
			get { return blStats.Bytes[22]; }
			set
			{
				blStats.Bytes[22] = value;
			}
		}



		public byte Habilidad2
		{
			get { return blStats.Bytes[23]; }
			set
			{
				blStats.Bytes[23] = value;
			}
		}
		public byte RatioDeEscaparZonaSafari
		{
			get { return blStats.Bytes[24]; }
			set
			{
				blStats.Bytes[24] = value;
			}
		}
		#region Por mirar InGame
		//Color parece que se usa en la pokedex...
		public Color ColorBaseStat
		{//Hex(128 + Clr1.SelectedIndex) FaceLeft se el suma 128 al stat del color...si no esta es FaceRight
			get { return IsFaceRight ? (Color)(int)blStats.Bytes[25] : (Color)(int)(FACELEFT - blStats.Bytes[25]); }
			set
			{
				if (value < Color.Rojo || value > Color.Rosa)
					throw new ArgumentOutOfRangeException();
				bool isFaceLeft = !IsFaceRight;
				blStats.Bytes[25] = (byte)(int)value;
				if (isFaceLeft)
					blStats.Bytes[25] += FACELEFT;

			}
		}
		/// <summary>
		/// Dirección de la imagen en la pantalla de estado
		/// </summary>
		public bool IsFaceRight
		{
			get { return blStats.Bytes[25] < FACELEFT; }
			set
			{
				if (value)
				{
					if (!IsFaceRight)
						blStats.Bytes[25] -= FACELEFT;
				}
				else
				{
					if (IsFaceRight)
						blStats.Bytes[25] += FACELEFT;
				}
			}
		}
		//PadBase 26,27??que es eso?? no se usa???
		public ushort PadBase
		{
			get
			{
				return Gabriel.Cat.Serializar.ToUShort(new byte[] {
				                                       	blStats.Bytes[26],
				                                       	blStats.Bytes[27]
				                                       });
			}
			set
			{
				byte[] bytesPadBase = Gabriel.Cat.Serializar.GetBytes(value);
				blStats.Bytes[26] = bytesPadBase[0];
				blStats.Bytes[27] = bytesPadBase[1];
			}
		}


		#endregion

		#endregion
		
		public Cry Cry {
			get {
				return cry;
			}
			set {
				cry = value;
			}
		}

		public Growl Growl {
			get {
				return growl;
			}
			set {
				growl = value;
			}
		}

		public Huella Huella {
			get {
				return huella;
			}
			set {
				huella = value;
			}
		}

		public AtaquesAprendidos AtaquesAprendidos {
			get {
				return ataquesAprendidos;
			}
			set {
				ataquesAprendidos = value;
			}
		}
		
		public void SetObjetosEnLosStats(int totalObjetos)
		{
			if (totalObjetos < 0 || totalObjetos > short.MaxValue)
				throw new ArgumentOutOfRangeException();

			blStats.Bytes[12] = (byte)(Objeto1 % totalObjetos);
			blStats.Bytes[14] = (byte)(Objeto2 % totalObjetos);

			blStats.Bytes[13] = (byte)(Objeto1 / totalObjetos);
			blStats.Bytes[13] = (byte)(Objeto2 / totalObjetos);
		}
		public void GetObjetosDeLosStats()
		{
			Objeto1 = blStats.Bytes[12] + blStats.Bytes[13] * 256;
			Objeto2 = blStats.Bytes[14] + blStats.Bytes[15] * 256;
		}
		public NivelEvs GetEvs(StatEvs stat)
		{
			NivelEvs nivel;
			switch (stat)
			{
					case StatEvs.Hp: nivel = HpEvs; break;
					case StatEvs.Ataque: nivel = AtaqueEvs; break;
					case StatEvs.Defensa: nivel = DefensaEspecialEvs; break;
					case StatEvs.Velocidad: nivel = VelocidadEvs; break;
					case StatEvs.AtaqueEspecial: nivel = AtaqueEspecialEvs; break;
					case StatEvs.DefensaEspecial: nivel = DefensaEspecialEvs; break;
					default: throw new ArgumentOutOfRangeException();
			}
			return nivel;
		}
		public void SetEvs(StatEvs stat, NivelEvs nivel)
		{

			switch (stat)
			{
					case StatEvs.Hp: HpEvs = nivel; break;
					case StatEvs.Ataque: AtaqueEvs = nivel; break;
					case StatEvs.Defensa: DefensaEspecialEvs = nivel; break;
					case StatEvs.Velocidad: VelocidadEvs = nivel; break;
					case StatEvs.AtaqueEspecial: AtaqueEspecialEvs = nivel; break;
					case StatEvs.DefensaEspecial: DefensaEspecialEvs = nivel; break;

			}

		}
		
		#region IComparable implementation


		public int CompareTo(object obj)
		{
			Pokemon other=obj as Pokemon;
			int compareTo;
			if(other!=null)
			{
				switch (Orden) {
					case OrdenPokemon.GameFreak:
						compareTo=OrdenGameFreak.CompareTo(other.OrdenGameFreak);
						break;
					case OrdenPokemon.Local:
						compareTo=OrdenLocal.CompareTo(other.OrdenLocal);
						break;
					case OrdenPokemon.Nacional:
						compareTo=OrdenNacional.CompareTo(other.OrdenNacional);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				
				
			}else compareTo=(int)Gabriel.Cat.CompareTo.Inferior;
			return compareTo;
		}
		public int CalculaHp(int nivel,int evs=0,int ivs=0)
		{
			return (((ivs+2*Hp+(evs/4)+100) * nivel)/100)+10;
		}
		#endregion
		public override string ToString()
		{
			return Nombre+"  #"+OrdenNacional;
		}
		public static Pokemon GetPokemon(RomData rom,int ordenGameFreak,int totalEntradasPokedex)
		{
			return GetPokemon(rom.Rom,rom.Edicion,rom.Compilacion,ordenGameFreak,totalEntradasPokedex);
		}
		public static Pokemon GetPokemon(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,int ordenGameFreak,int totalEntradasPokedex)
		{
			Pokemon pokemon=new Pokemon();
			pokemon.OrdenGameFreak=ordenGameFreak;
			try{
				pokemon.OrdenLocal=Word.GetWord(rom, Zona.GetOffsetRom(rom, ZonaOrdenLocal, edicion, compilacion).Offset + (pokemon.OrdenGameFreak-1) * 2);
			}catch{pokemon.OrdenLocal=-1;}
			try{
				pokemon.OrdenNacional=Word.GetWord(rom, Zona.GetOffsetRom(rom, ZonaOrdenNacional, edicion, compilacion).Offset + (pokemon.OrdenGameFreak-1) * 2);
			}catch{pokemon.OrdenNacional=-1;}
			pokemon.Sprites=SpritesPokemon.GetSpritesPokemon(rom,edicion,compilacion,ordenGameFreak);
			pokemon.Nombre=BloqueString.GetString(rom,Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion).Offset+(ordenGameFreak*(int)LongitudCampos.NombreCompilado));
			pokemon.blStats=BloqueBytes.GetBytes(rom.Data,Zona.GetOffsetRom(rom,ZonaStats,edicion,compilacion).Offset+(ordenGameFreak*(int)LongitudCampos.TotalStats),(int)LongitudCampos.TotalStats);
			pokemon.GetObjetosDeLosStats();
			
			if(pokemon.OrdenNacional>=0&&pokemon.OrdenNacional<=totalEntradasPokedex&&false){
				pokemon.Cry=Cry.GetCry(rom,pokemon.OrdenNacional);
				pokemon.Growl=Growl.GetGrowl(rom,pokemon.OrdenNacional);
			}
			pokemon.Huella=Huella.GetHuella(rom,edicion,compilacion,ordenGameFreak);
			pokemon.AtaquesAprendidos=AtaquesAprendidos.GetAtaquesAprendidos(rom,edicion,compilacion,ordenGameFreak);
			if(pokemon.OrdenNacional>0&&pokemon.OrdenNacional<totalEntradasPokedex)
				pokemon.Descripcion=DescripcionPokedex.GetDescripcionPokedex(rom,edicion,compilacion,pokemon.OrdenNacional);
			return pokemon;
			
		}
		public static Pokemon[] GetPokedex(RomData rom)
		{
			return GetPokedex(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static Pokemon[] GetPokedex(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			Pokemon[] pokedex=new Pokemon[GetTotalPokemon(rom,edicion,compilacion)];
			int totalEntradasPokedex=DescripcionPokedex.GetTotalEntradas(rom,edicion,compilacion);
			for(int i=0;i<pokedex.Length;i++)
				pokedex[i]=Pokemon.GetPokemon(rom,edicion,compilacion,i,totalEntradasPokedex);
			return pokedex;
			
		}
		public static void SetPokemon(RomData rom,Pokemon pokemon)
		{
			SetPokemon(rom.Rom,rom.Edicion,rom.Compilacion,pokemon,rom.Pokedex.Filtra(p=>p.Descripcion!=null).Count,rom.Objetos.Count,rom.DicAtaquesPokemon);
			
			
		}
		public static void SetPokemon(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,Pokemon pokemon,int totalEntradasPokedex,int totalObjetos,LlistaOrdenadaPerGrups<int,AtaquesAprendidos> dicAtaquesPokemon)
		{

			int offsetNombre=Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion).Offset+pokemon.OrdenGameFreak*(int)LongitudCampos.NombreCompilado;
			
			pokemon.SetObjetosEnLosStats(totalObjetos);
			
			Word.SetWord(rom,Zona.GetOffsetRom(rom,ZonaOrdenLocal,edicion,compilacion).Offset+pokemon.OrdenGameFreak*Word.LENGTH,(short)pokemon.OrdenLocal);
			Word.SetWord(rom,Zona.GetOffsetRom(rom,ZonaOrdenNacional,edicion,compilacion).Offset+pokemon.OrdenGameFreak*Word.LENGTH,(short)pokemon.OrdenNacional);
			
			BloqueString.Remove(rom,offsetNombre);
			BloqueString.SetString(rom,offsetNombre,pokemon.Nombre);
			
			rom.Data.SetArray(new OffsetRom(Zona.GetOffsetRom(rom,ZonaStats,edicion,compilacion).Offset+pokemon.OrdenGameFreak*OffsetRom.LENGTH).Offset,pokemon.Stats.Bytes);
			if(pokemon.Descripcion!=null&&pokemon.OrdenNacional>0&&pokemon.OrdenNacional<totalEntradasPokedex)
				DescripcionPokedex.SetDescripcionPokedex(rom,edicion,compilacion,pokemon.Descripcion,pokemon.OrdenNacional);

			if (pokemon.AtaquesAprendidos!=null)
				AtaquesAprendidos.SetAtaquesAprendidos(rom,edicion,compilacion,pokemon.OrdenGameFreak,pokemon.AtaquesAprendidos,dicAtaquesPokemon);

			if (pokemon.Huella!=null)
				Huella.SetHuella(rom,edicion,compilacion,pokemon.Huella,pokemon.OrdenGameFreak);

			if (pokemon.Sprites!=null)
				SpritesPokemon.SetSpritesPokemon(rom,edicion,compilacion,pokemon.OrdenGameFreak,pokemon.Sprites);

			//falta hacer los setCry y setGrowl
		}
		public static void SetPokedex(RomData rom)
		{
			SetPokedex(rom.Rom,rom.Edicion,rom.Compilacion,rom.Pokedex,rom.Objetos.Count,AtaquesAprendidos.GetAtaquesAprendidosDic(rom));
		}
		public static void SetPokedex(RomGba rom,EdicionPokemon edicion,Compilacion compilacion,IList<Pokemon> pokedex,int totalObjetos,LlistaOrdenadaPerGrups<int,AtaquesAprendidos> dicAtaquesPokemon)
		{
			OffsetRom offsetNombre;
			OffsetRom offsetOrdenLocal;
			OffsetRom offsetOrdenNacional;
			OffsetRom offsetStats;
			
			int totalActual=GetTotalPokemon(rom,edicion,compilacion);
			int totalEntradasPokedex=DescripcionPokedex.GetTotalEntradas(rom,edicion,compilacion);
			if(pokedex.Count!=totalActual)
			{
				offsetNombre=Zona.GetOffsetRom(rom,ZonaNombre,edicion,compilacion);
				offsetOrdenLocal=Zona.GetOffsetRom(rom,ZonaOrdenLocal,edicion,compilacion);
				offsetOrdenNacional=Zona.GetOffsetRom(rom,ZonaOrdenNacional,edicion,compilacion);
				offsetStats=Zona.GetOffsetRom(rom,ZonaStats,edicion,compilacion);
				rom.Data.Remove(offsetNombre.Offset,totalActual*(int)LongitudCampos.NombreCompilado);
				rom.Data.Remove(offsetOrdenLocal.Offset,totalActual*Word.LENGTH);
				rom.Data.Remove(offsetOrdenNacional.Offset,totalActual*Word.LENGTH);
				rom.Data.Remove(offsetStats.Offset,totalActual*(int)LongitudCampos.TotalStats);
				//borro los datos
				rom.Data.Remove(Zona.GetOffsetRom(rom,Huella.ZonaHuella,edicion,compilacion).Offset,totalActual*Huella.LENGHT);
				for(int i=0;i<totalActual;i++)
				{
					//descripcionPokedex
					DescripcionPokedex.Remove(rom,edicion,compilacion,i);
					//Sprites
					SpritesPokemon.Remove(rom,edicion,compilacion,i);
					//ataquesAprendidos
					AtaquesAprendidos.Remove(rom,edicion,compilacion,i);
				}
				if(pokedex.Count>totalActual)
				{
					//busco espacio y cambio las zonas
					OffsetRom.SetOffset(rom,offsetNombre,rom.Data.SearchEmptyBytes(pokedex.Count*(int)LongitudCampos.NombreCompilado));
					OffsetRom.SetOffset(rom,offsetOrdenLocal,rom.Data.SearchEmptyBytes(pokedex.Count*Word.LENGTH));
					OffsetRom.SetOffset(rom,offsetOrdenNacional,rom.Data.SearchEmptyBytes(pokedex.Count*Word.LENGTH));
					OffsetRom.SetOffset(rom,offsetStats,rom.Data.SearchEmptyBytes(pokedex.Count*(int)LongitudCampos.TotalStats));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, DescripcionPokedex.ZonaDescripcion,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*DescripcionPokedex.LongitudDescripcion(edicion)));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, SpritesPokemon.ZonaImgFrontal,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*BloqueImagen.LENGTHHEADERCOMPLETO));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, SpritesPokemon.ZonaImgTrasera,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*BloqueImagen.LENGTHHEADERCOMPLETO));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, SpritesPokemon.ZonaPaletaNormal,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*Paleta.LENGTHHEADERCOMPLETO));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, SpritesPokemon.ZonaPaletaShiny,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*Paleta.LENGTHHEADERCOMPLETO));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, Huella.ZonaHuella,edicion,compilacion),rom.Data.SearchEmptyBytes(pokedex.Count*Huella.LENGHT));
					OffsetRom.SetOffset(rom,Zona.GetOffsetRom(rom, AtaquesAprendidos.ZonaAtaquesAprendidos,edicion,compilacion),rom.Data.SearchEmptyBytes(EspacioOcupadoAtaquesAprendidos(pokedex)));
					
				}
			}
			for(int i=0;i<pokedex.Count;i++)
				Pokemon.SetPokemon(rom,edicion,compilacion,pokedex[i],totalEntradasPokedex,totalObjetos,dicAtaquesPokemon);
		}

		static int EspacioOcupadoAtaquesAprendidos(IList<Pokemon> pokedex)
		{
			int total=0;
			for(int i=0;i<pokedex.Count;i++)
				total+=pokedex[i].AtaquesAprendidos.ToBytesGBA().Length;
			return total;
		}
		public static int GetTotalPokemon(RomData rom)
		{
			return GetTotalPokemon(rom.Rom,rom.Edicion,rom.Compilacion);
		}
		public static int GetTotalPokemon(RomGba rom,EdicionPokemon edicion,Compilacion compilacion)
		{
			int total=0;
			int offsetHuella=Zona.GetOffsetRom(rom,Huella.ZonaHuella,edicion,compilacion).Offset;
			while(new OffsetRom(rom,offsetHuella).IsAPointer)
			{
				offsetHuella+=OffsetRom.LENGTH;
				total++;
			}
			return total-1;
		}
		
	}
}
