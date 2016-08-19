/*
 * Created by SharpDevelop.
 * User: pc
 * Date: 19/08/2016
 * Time: 15:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

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
		BloqueString nombre;
		byte[] stats;
		int objeto1,objeto2;//me falta saber el total para poder hacerlo...
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
				stats=value;
			}
		}
		#region Interpretando stats
		//hpUD 0
		public byte Hp {
			get { return Stats[0]; }
			set {
				stats[0] = value;

			}
		}
		//ataqueud 1
		public byte Ataque {
			get { return Stats[1]; }
			set {
				stats[1] = value;
			}
		}
		//defensaud 2
		public byte Defensa {
			get { return Stats[2]; }
			set {
				stats[2] = value;
			}
		}
		//velocidadud 3
		public byte Velocidad {
			get { return Stats[3]; }
			set {
				stats[3] = value;
			}
		}

		//sp.a.ud 4
		public byte AtaqueEspecial {
			get { return Stats[4]; }
			set {
				stats[4] = value;
			}
		}
		//sp.d.ud 5
		public byte DefensaEspecial {
			get { return Stats[5]; }
			set {
				stats[5] = value;
			}
		}
		public int TotalStatsBase {
			get {
				int totalStatsBase = 0;
				for (int i = 0; i < 6; i++)
					totalStatsBase += stats[i];
				return totalStatsBase;

			}
		}
		//tipo 6,7
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
				
				stats[7] =value;
				
			}
		}
		//catch_rate 8??
		public byte RatioCaptura {
			get { return stats[8]; }
			set {
				stats[8] = value;
			}
		}
		//expyield 9??
		public byte ExpBase {
			get { return stats[9]; }
			set {
				stats[9] = value;
			}
		}
		//evs 10??//tambien???11??evs = get_bytes_string_from_hex_string(s[10]+s[11])
		public byte Evs {
			get { return stats[10]; }
			set {
				stats[10] = value;
			}
		}
		//habilidad 22,23
		
		//item1 indexItems([12](%)+[13](/)*256);
		public int Objeto1 {
			get {
				return objeto1;
			}
			set {
				if(value<0||value>short.MaxValue)throw new ArgumentOutOfRangeException();
				objeto1=value;
			}
		}
		//item2 indexItems([14](%)+[15](/)*256);
		public int Objeto2 {
			get {
				return objeto2;
			}
			set {
				if(value<0||value>short.MaxValue)throw new ArgumentOutOfRangeException();
				objeto2=value;
			}
		}
		//GenderUD 16
		public byte Genero {
			get { return stats[16]; }
			set {
				stats[16] = value;
			}
		}
		//HatchIngStps 17
		public byte PasosEclosion {
			get { return stats[17]; }
			set {
				stats[17] = value;
			}
		}
		//FrindShip 18
		public byte Amistad {
			get { return stats[18]; }
			set {
				stats[18] = value;
			}
		}
		//LevelUpType 19??que es eso??
		public byte LevelUpType {//no se que es..de momento lo dejo con este nombre
			get { return stats[19]; }
			set {
				stats[19] = value;
			}
		}
		//EggGroup1 20
		public byte GrupoHuevo1 {
			get { return stats[20]; }
			set {
				stats[20] = value;
			}
		}
		//EggGroup2 21
		public byte GrupoHuevo2 {
			get { return stats[21]; }
			set {
				stats[21] = value;
			}
		}
		//RunRate 24
		public byte RunRate {//dejo este nombre porque no se que es
			get { return stats[24]; }
			set {
				stats[24] = value;
			}
		}
		//Color 25??que es eso??
		public byte Color {//no se que es
			get { return stats[25]; }
			set {
				stats[25] = value;
			}
		}
		//[26,27]???
		#endregion
	}
}
