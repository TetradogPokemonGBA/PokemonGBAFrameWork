/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 15:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//informacion de stats sacada de Pokemon Game Editor ->Créditos a Gamer2020
namespace PokemonGBAFrameWork
{
	/// <summary>
	/// Description of Pokemon.
	/// </summary>
	public class Pokemon
	{
		//en construccion
		public enum LongitudCampos
		{
			Nombre = 11,
			TotalStats = 28,
		}
		public enum NivelEvs
		{
			Ninguno,
			Bajo,
			Normal,
			Maximo
		}
		public enum PasosEclosion
		{
			AlInstante=0,
			P1280=5,
			P2560= 10, 
			P3840= 15,
			P5120= 20,
			P6400= 25,
			P7680= 30,
			P8960= 35,
			P10240= 40,
			P20480= 80,
			P30720= 120
		}
		public enum RatioCrecimiento
		{
			Exp1000000,//si fuesen nombres mejor pero como no hay nada CANON de momento se queda asi...lleva Exp porque las enumeraciones no aceptan empezar por numero
			Exp600000,
			Exp1640000,
			Exp1059860,
			Exp800000,
			Exp1250000
		}
		public enum RatioGenero{
			Macho100= 0,//no se puede poner el caracter %
			Macho87= 31,
			Macho75= 63,
			Macho65= 89,
			Macho50Hembra= 127,
			Hembra65= 165,
			Hembra75= 191,
			Hembra87= 223,
			Hembra100= 254,
			SinGenero= 255
		}
		public enum Felicidad
		{
			LaMasBaja = 0,
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
		const byte FACELEFT = 128;
		//forma parte de un stat junto con la de color en el mismo byte numero 25

		BloqueString nombre;
		byte[] stats;
		int objeto1, objeto2;
		//me falta saber el total para poder hacerlo...
		int ordenPokedexLocal;
		int ordenPokedexNacional;
		DescripcionPokedex descripcion;
		Sprite sprites;
		//falta huella
		//falts miniSprites
		//falta Cry
		//falta ataques, mt y mo

		
		public byte[] Stats {
			get {
				return stats;
				;
			}

			set {
				if (value == null || value.Length != (int)LongitudCampos.TotalStats)
					throw new ArgumentException();
				stats = value;
			}
		}
		#region Interpreta	Stats
		public int TotalStatsBase {
			get {
				int totalStatsBase = 0;
				for (int i = 0; i < 6; i++)
					totalStatsBase += stats[i];
				return totalStatsBase;

			}
		}
		
		
		public byte Hp {
			get { return Stats[0]; }
			set {
				stats[0] = value;

			}
		}
		public byte Ataque {
			get { return Stats[1]; }
			set {
				stats[1] = value;
			}
		}
		public byte Defensa {
			get { return Stats[2]; }
			set {
				stats[2] = value;
			}
		}
		public byte Velocidad {
			get { return Stats[3]; }
			set {
				stats[3] = value;
			}
		}
		public byte AtaqueEspecial {
			get { return Stats[4]; }
			set {
				stats[4] = value;
			}
		}
		public byte DefensaEspecial {
			get { return Stats[5]; }
			set {
				stats[5] = value;
			}
		}
		public byte Tipo1 {
			get {

				return stats[6];
			}
			set {
				
				stats[6] = value;
				
			}
		}
		public byte Tipo2 {
			get {
				return stats[7];
			}
			set {
				
				stats[7] = value;
				
			}
		}
		public byte RatioCaptura {
			get { return stats[8]; }
			set {
				stats[8] = value;
			}
		}
		public byte ExperienciaBase {
			get { return stats[9]; }
			set {
				stats[9] = value;
			}
		}
		//hacer enumeracion Evs porque en 2 bytes estan todos!!
		public byte EvsParte1 {
			get { return stats[10]; }//se pone la posicion en la tabla 
/*{
"Nothing",
"1 HP", 
"2 HP", 
"3 HP",
 "1 Atk", 
"1 HP, 1 Atk",
 "2 HP, 1 Atk",
 "3 HP, 1 Atk", 
"2 Atk",
 "1 HP, 2 Atk",
 "2 HP, 2 Atk", 
"3 HP, 2 Atk", 
"3 Atk",
 "1 HP, 3 Atk",
 "2 HP, 3 Atk", 
"3 HP, 3 Atk",
 "1 Def", 
"1 Def, 1 HP",
 "1 Def, 2 HP",
 "1 Def, 3 HP",
 "1 Def, 1 Atk",
 "1 Def, 1 HP, 1 Atk",
 "1 Def, 2 HP, 1 Atk",
 "1 Def, 3 HP, 1 Atk",
 "1 Def, 2 Atk",
 "1 Def, 1 HP, 2 Atk",
 "1 Def, 2 HP, 2 Atk",
 "1 Def, 3 HP, 2 Atk",
 "1 Def, 3 Atk",
 "1 Def, 1 HP, 3 Atk",
 "1 Def, 2 HP, 3 Atk", 
"1 Def, 3 HP, 3 Atk",
 "2 Def", 
"2 Def, 1 HP",
 "2 Def, 2 HP",
 "2 Def, 3 HP",
 "2 Def, 1 Atk",
 "2 Def, 1 HP, 1 Atk",
 "2 Def, 2 HP, 1 Atk",
 "2 Def, 3 HP, 1 Atk",
 "2 Def, 2 Atk", 
"2 Def, 1 HP, 2 Atk", 
"2 Def, 2 HP, 2 Atk",
 "2 Def, 3 HP, 2 Atk",
 "2 Def, 3 Atk",
 "2 Def, 1 HP, 3 Atk",
 "2 Def, 2 HP, 3 Atk",
 "2 Def, 3 HP, 3 Atk",
 "3 Def", "3 Def, 1 HP",
 "3 Def, 2 HP", 
"3 Def, 3 Hp",
 "3 Def, 1 Atk",
 "3 Def, 1 HP, 1 Atk",
 "3 Def, 2 HP, 1 Atk",
 "3 Def, 3 HP, 1 Atk",
 "3 Def, 2 Atk",
 "3 Def, 1 HP, 2 Atk",
 "3 Def, 2 HP, 2 Atk", 
"3 Def, 3 HP, 2 Atk", 
"3 Def, 3 Atk",
 "3 Def, 1 HP, 3 Atk",
 "3 Def, 2 HP, 3 Atk",
 "3 Def, 3 HP, 3 Atk",
 "1 Spe", "1 Spe, 1 HP",
 "1 Spe, 2 HP",
 "1 Spe, 3 HP", 
"1 Spe, 1 Atk", 
"1 Spe, 1 HP, 1 Atk",
 "1 Spe, 2 HP, 1 Atk",
 "1 Spe, 3 HP, 1 Atk",
 "1 Spe, 2 Atk",
 "1 Spe, 1 HP, 2 Atk",
 "1 Spe, 2 HP, 2 Atk",
 "1 Spe, 3 HP, 2 Atk",
 "1 Spe, 3 Atk", 
"1 Spe, 1 HP, 3 Atk", 
"1 Spe, 2 HP, 3 Atk",
 "1 Spe, 3 HP, 3 Atk",
 "1 Spe, 1 Def",
 "1 Spe, 1 HP, 1 Def", 
"1 Spe, 2 HP, 1 Def", 
"1 Spe, 3 HP, 1 Def",
 "1 Spe, 1 Atk, 1 Def", 
"1 Spe, 1 HP, 1 Atk, 1 Def", 
"1 Spe, 2 HP, 1 Atk, 1 Def",
 "1 Spe, 3 HP, 1 Atk, 1 Def",
 "1 Spe, 2 Atk, 1 Def",
 "1 Spe, 1 HP, 2 Atk, 1 Def",
 "1 Spe, 2 HP, 2 Atk, 1 Def", 
"1 Spe, 3 HP, 2 Atk, 1 Def", "" +
"1 Spe, 3 Atk, 1 Def", 
"1 Spe, 1 HP, 3 Atk, 1 Def", 
"1 Spe, 2 HP, 3 Atk, 1 Def", 
"1 Spe, 3 HP, 3 Atk, 1 Def", 
"1 Spe, 2 Def", 
"1 Spe, 1 HP, 2 Def",
 "1 Spe, 2 HP, 2 Def",
 "1 Spe, 3 HP, 2 Def",
 "1 Spe, 1 Atk, 2 Def",
 "1 Spe, 1 HP, 1 Atk, 2 Def",
 "1 Spe, 2 HP, 1 Atk, 2 Def",
 "1 Spe, 3 HP, 1 Atk, 2 Def",
 "1 Spe, 2 Atk, 2 Def", 
"1 Spe, 1 HP, 2 Atk, 2 Def",
 "1 Spe, 2 HP, 2 Atk, 2 Def", 
"1 Spe, 3 HP, 2 Atk, 2 Def", 
"1 Spe, 3 Atk, 2 Def",
 "1 Spe, 1 HP, 3 Atk, 2 Def",
 "1 Spe, 2 HP, 3 Atk, 2 Def",
 "1 Spe, 3 HP, 3 Atk, 2 Def",
 "1 Spe, 3 Def", 
"1 Spe, 1 HP, 3 Def", 
"1 Spe, 2 HP, 3 Def", 
"1 Spe, 3 HP, 3 Def", 
"1 Spe, 1 Atk, 3 Def",
 "1 Spe, 1 HP, 1 Atk, 3 Def", 
"1 Spe, 2 HP, 1 Atk, 3 Def", 
"1 Spe, 3 HP, 1 Atk, 3 Def", 
"1 Spe, 2 Atk, 3 Def", 
"1 Spe, 1 HP, 2 Atk, 3 Def", 
"1 Spe, 2 HP, 2 Atk, 3 Def", 
"1 Spe, 3 HP, 2 Atk, 3 Def", 
"1 Spe, 3 Atk, 3 Def",
 "1 Spe, 1 HP, 3 Atk, 3 Def", 
"1 Spe, 2 HP, 3 Atk, 3 Def", 
"1 Spe, 3 HP, 3 Atk, 3 Def", 
"2 Spe", "2 Spe, 1 HP", 
"2 Spe, 2 HP", 
"2 Spe, 3 HP", 
"2 Spe, 1 Atk",
 "2 Spe, 1 HP, 1 Atk",
 "2 Spe, 2 HP, 1 Atk",
 "2 Spe, 3 HP, 1 Atk",
 "2 Spe, 2 Atk",
 "2 Spe, 1 HP, 2 Atk", 
"2 Spe, 2 HP, 2 Atk", 
"2 Spe, 3 HP, 2 Atk", 
"2 Spe, 3 Atk", 
"2 Spe, 1 HP, 3 Atk",
 "2 Spe, 2 HP, 3 Atk",
 "2 Spe, 3 HP, 3 Atk",
 "2 Spe, 1 Def",
 "2 Spe, 1 HP, 1 Def",
 "2 Spe, 2 HP, 1 Def", 
"2 Spe, 3 HP, 1 Def", 
"2 Spe, 1 Atk, 1 Def",
 "2 Spe, 1 HP, 1 Atk, 1 Def",
 "2 Spe, 2 HP, 1 Atk, 1 Def",
 "2 Spe, 3 HP, 1 Atk, 1 Def",
 "2 SPe, 2 Atk, 1 Def",
 "2 Spe, 1 HP, 2 Atk, 1 Def",
 "2 Spe, 2 HP, 2 Atk, 1 Def", 
"2 Spe, 3 HP, 2 Atk, 1 Def",
 "2 Spe, 3 Atk, 1 Def",
 "2 Spe, 1 HP, 3 Atk, 1 Def",
 "2 Spe, 2 HP, 3 Atk, 1 Def",
 "2 Spe, 3 HP, 3 Atk, 1 Def",
 "2 Spe, 2 Def",
 "2 Spe, 1 HP, 2 Def",
 "2 Spe, 2 HP, 2 Def",
 "2 Spe, 3 HP, 2 Def",
 "2 Spe, 1 Atk, 2 Def",
 "2 Spe, 1 HP, 1 Atk, 2 Def",
 "2 Spe, 2 HP, 1 Atk, 2 Def",
 "2 Spe, 3 HP, 1 Atk, 2 Def",
 "2 Spe, 2 Atk, 2 Def",
 "2 Spe, 1 HP, 2 Atk, 2 Def",
 "2 Spe, 2 HP, 2 Atk, 2 Def",
 "2 Spe, 3 HP, 2 Atk, 2 Def",
 "2 Spe, 3 Atk, 2 Def",
 "2 Spe, 1 HP, 3 Atk, 2 Def",
 "2 Spe, 2 HP, 3 Atk, 2 Def",
 "2 Spe, 3 HP, 3 Atk, 2 Def",
 "2 Spe, 3 Def",
 "2 Spe, 1 HP, 3 Def",
 "2 Spe, 2 HP, 3 Def",
 "2 Spe, 3 HP, 3 Def",
 "2 Spe, 1 Atk, 3 Def",
 "2 Spe, 1 HP, 1 Atk, 3 Def",
 "2 Spe, 2 HP, 1 Atk, 3 Def",
 "2 Spe, 3 HP, 1 Atk, 3 Def",
 "2 Spe, 2 Atk, 3 Def",
 "2 Spe, 1 HP, 2 Atk, 3 Def",
 "2 Spe, 2 HP, 2 Atk, 3 Def",
 "2 Spe, 3 HP, 2 Atk, 3 Def",
 "2 Spe, 3 Atk, 3 Def", 
"2 Spe, 1 HP, 3 Atk, 3 Def",
 "2 Spe, 2 HP, 3 Atk, 3 Def",
 "2 Spe, 3 HP, 3 Atk, 3 Def",
 "3 Spe", "3 Spe, 1 HP",
 "3 Spe, 2 HP", "3 Spe, 3 HP",
 "3 Spe, 1 Atk", 
"3 Spe, 1 HP, 1 Atk",
 "3 Spe, 2 HP, 1 Atk",
 "3 Spe, 3 HP, 1 Atk",
 "3 Spe, 2 Atk",
 "3 Spe, 1 HP, 2 Atk",
 "3 Spe, 2 HP, 2 Atk", 
"3 Spe, 3 HP, 2 Atk",
 "3 Spe, 3 Atk",
 "3 Spe, 1 HP, 3 Atk",
 "3 Spe, 2 HP, 3 Atk", 
"3 Spe, 3 HP, 3 Atk", 
"3 Spe, 1 Def", 
"3 Spe, 1 HP, 1 Def",
 "3 Spe, 2 HP, 1 Def",
 "3 Spe, 3 HP, 1 Def",
 "3 Spe, 1 Atk, 1 Def", 
"3 Spe, 1 HP, 1 Atk, 1 Def",
 "3 Spe, 2 HP, 1 Atk, 1 Def",
 "3 Spe, 3 HP, 1 Atk, 1 Def",
 "3 Spe, 2 Atk, 1 Def",
 "3 Spe, 1 HP, 2 Atk, 1 Def",
 "3 Spe, 2 HP, 2 Atk, 1 Def",
 "3 Spe, 3 HP, 2 Atk, 1 Def", 
"3 Spe, 3 Atk, 1 Def", 
"3 Spe, 1 HP, 3 Atk, 1 Def",
 "3 Spe, 2 HP, 3 Atk, 1 Def",
 "3 Spe, 3 HP, 3 Atk, 1 Def",
 "3 Spe, 2 Def",
 "3 Spe, 1 HP, 2 Def", 
"3 Spe, 2 HP, 2 Def",
 "3 Spe, 3 HP, 2 Def",
 "3 Spe, 1 Atk, 2 Def",
 "3 Spe, 1 HP, 1 Atk, 2 Def",
 "3 Spe, 2 HP, 1 Atk, 2 Def",
 "3 Spe, 3 HP, 1 Atk, 2 Def",
 "3 Spe, 2 Atk, 2 Def",
 "3 Spe, 1 HP, 2 Atk, 2 Def",
 "3 Spe, 2 HP, 2 Atk, 2 Def",
 "3 Spe, 3 HP, 2 Atk, 2 Def",
 "3 Spe, 3 Atk, 2 Def",
 "3 Spe, 1 HP, 3 Atk, 2 Def",
 "3 Spe, 2 HP, 3 Atk, 2 Def", 
"3 Spe, 3 HP, 3 Atk, 2 Def",
 "3 Spe, 3 Def",
 "3 Spe, 1 HP, 3 Def",
 "3 Spe, 2 HP, 3 Def", 
"3 Spe, 3 HP, 3 Def",
 "3 Spe, 1 Atk, 3 Def", 
"3 Spe, 1 HP, 1 Atk, 3 Def", 
"3 Spe, 2 HP, 1 Atk, 3 Def",
 "3 Spe, 3 HP, 1 ATk, 3 Def", 
"3 Spe, 2 Atk, 3 Def", 
"3 Spe, 1 HP, 2 Atk, 3 Def", 
"3 Spe, 2 HP, 2 Atk, 3 Def", 
"3 Spe, 3 HP, 2 ATk, 3 Def", 
"3 Spe, 3 Atk, 3 Def", 
"3 Spe, 1 HP, 3 Atk, 3 Def", 
"3 Spe, 2 HP, 3 Atk, 3 Def",
"3 Spe, 3 HP, 3 Atk, 3 Def"}//hp,atk,def,spe
			 */
			set {
				stats[10] = value;
			}
		}
		public byte EvsParte2 {
			get { return stats[11]; }//se pone la posicion en la tabla 
/*		
 {"Nothing",
 "1 SpA",
 "2 SpA",
 "3 SpA",
 "1 SpD",
 "1 SpD, 1 SpA",
 "1 SpD, 2 SpA",
 "1 SpD, 3 SpA",
 "2 SpD",
 "2 SpD, 1 SpA",
 "2 SpD, 2 SpA", 
"2 SpD, 3 SpA",
 "3 SpD",
 "3 SpD, 1 SpA",
 "3 SpD, 2 SpA",
 "3 SpD, 3 SpA"//spa,spd
}*/
			set {
				stats[11] = value;
			}
		}
		#region EvsStats
		public NivelEvs HpEvs
		{
			get{
			 return (NivelEvs)(stats[10]%4);
			}
		}
		public NivelEvs AtaqueEvs
		{
			get{
			 int cicloActual=stats[10]/4;
			 int posicion=stats[10]%4;
			 return (NivelEvs)posicion;//de momento no se como calcularlo pero se tiene que calcular sin depender de ninguno!!! para el set si pero para el get no porque sino habria recursividad infinita...
			}
		}
		
		public NivelEvs AtaqueEspecialEvs
		{
			get{
			 return (NivelEvs)(stats[11]%4);
			}
		}
		#endregion
		
		//item1 indexItems([12](%)+[13](/)*256);
		public int Objeto1 {
			get {
				return objeto1;
			}
			set {
				if (value < 0 || value > short.MaxValue)
					throw new ArgumentOutOfRangeException();
				objeto1 = value;
			}
		}
		//item2 indexItems([14](%)+[15](/)*256);
		public int Objeto2 {
			get {
				return objeto2;
			}
			set {
				if (value < 0 || value > short.MaxValue)
					throw new ArgumentOutOfRangeException();
				objeto2 = value;
			}
		}

		public RatioGenero RatioSexo {
			get { return (RatioGenero)(int)stats[16]; }
			set {
				if((int)value>byte.MaxValue||(int)value<byte.MinValue)throw new ArgumentOutOfRangeException("el valor se guarda en un byte!");
				stats[16] =(byte)(int)value;
			}
		}

		public PasosEclosion PasosParaEclosionarHuevo {
			get { return (PasosEclosion)(int)stats[17]; }
			set {
				if((int)value>byte.MaxValue||(int)value<byte.MinValue)throw new ArgumentOutOfRangeException("el valor se guarda en un byte!");
				stats[17] = (byte)(int)value;
			}
		}
		/// <summary>
		/// Se puede poner valores de 0 a FF
		/// </summary>
		public Felicidad BaseAmistad {
			get { return (Felicidad)(int)stats[18]; }
			set {
				if((int)value>byte.MaxValue||(int)value<byte.MinValue)throw new ArgumentOutOfRangeException("el valor se guarda en un byte!");
				stats[18] = (byte)(int)value;
			}
		}
		public RatioCrecimiento Crecimiento {
			get { return (RatioCrecimiento)(int)stats[19]; }//solo se usa la posicion de la enumeracion para determinar su crecimiento
			set {
				if(value<RatioCrecimiento.Exp1000000||value>RatioCrecimiento.Exp800000)throw new ArgumentOutOfRangeException(String.Format("el valor no puede superar el numero {0} y tiene que ser positivo",Enum.GetName(typeof(RatioCrecimiento)).Length-1));
				stats[19] = (byte)(int)value;
			}
		}
		public byte GrupoHuevo1 {
			get { return stats[20]; }
			set {
				stats[20] = value;
			}
		}
		public byte GrupoHuevo2 {
			get { return stats[21]; }
			set {
				stats[21] = value;
			}
		}
		public byte Habilidad1 {
			get { return stats[22]; }
			set {
				stats[22] = value;
			}
		}
		public byte Habilidad2 {
			get { return stats[23]; }
			set {
				stats[23] = value;
			}
		}
		public byte RatioDeEscapar {
			get { return stats[24]; }
			set {
				stats[24] = value;
			}
		}
		#region Por mirar InGame
		//Color parece que se usa en la pokedex...
		public Color ColorBaseStat {//Hex(128 + Clr1.SelectedIndex) FaceLeft se el suma 128 al stat del color...si no esta es FaceRight
			get { return IsFaceRight ? (Color)(int)stats[25] : (Color)(int)(FACELEFT - stats[25]); }
			set {
				bool isFaceLeft = !IsFaceRight;
				stats[25] = (byte)(int)value;
				if (isFaceLeft)
					stats[25] += FACELEFT;
				
			}
		}
		//creo que se usa en la imagen de los stats...
		public bool IsFaceRight {
			get{ return stats[25] < FACELEFT; }
			set {
				if (value) {
					if (!IsFaceRight)
						stats[25] -= FACELEFT;
				} else {
					if (IsFaceRight)
						stats[25] += FACELEFT;
				}
			}
		}
		//PadBase 26,27??que es eso?? no se usa???
		public ushort PadBase {
			get { return Gabriel.Cat.Serializar.ToUShort(new byte[] {
			                                             	stats[26],
			                                             	stats[27]
			                                             }); }
			set {
				byte[] bytesPadBase = Gabriel.Cat.Serializar.GetBytes(value);
				stats[26] = bytesPadBase[0];
				stats[27] = bytesPadBase[1];
			}
		}
		#endregion
		#endregion
	}
}
