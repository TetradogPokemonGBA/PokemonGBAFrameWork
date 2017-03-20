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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.Extension;
using Gabriel.Cat;

namespace PokemonGBAFrameWork
{
	public static class Shinyzer
	{
		const int LENGTH=144;
		//ademas de los bytes de la rutina hay otros y son diferentes para cada idioma y en algunos en la edicion
		static readonly byte[] RutinaShinyzerHeader1RubiAndZafiroEsp={1,80};
		static readonly byte[] RutinaShinyzerHeader1OtrasEsp={80};
		
		static readonly int offset1RubiAndZafiroEsp=251576;
		static readonly int offset1RojoAndVerdeEsp=263617;
		static readonly int offset1EsmeraldaEsp=438177;
		
		static readonly byte[] RutinaShinyzerHeader1And2Usa={136};
		static readonly byte[] RutinaShinyzerHeader3Usa={4};
		static readonly byte[] RutinaShinyzerHeader4Usa={1,72};
		static readonly byte[] RutinaShinyzerHeader5Usa={240,20,250,1,224,81,5,241,8};
		
		static readonly int offset1RubiAndZafiroUsa=251101;
		static readonly int offset1RojoAndVerdeUsa=263897;
		static readonly int offset1EsmeraldaUsa=438161;
		
		static readonly int offset2RubiAndZafiroUsa=251103;
		static readonly int offset2RojoAndVerdeUsa=263899;
		static readonly int offset2EsmeraldaUsa=438163;
		
		static readonly int offset3RubiAndZafiroUsa=251105;
		static readonly int offset3RojoAndVerdeUsa=263901;
		static readonly int offset3EsmeraldaUsa=438165;
		
		static readonly int offset4RubiAndZafiroUsa=251108;
		static readonly int offset4RojoAndVerdeUsa=263904;
		static readonly int offset4EsmeraldaUsa=438171;
		
		static readonly int offset5RubiAndZafiroUsa=251111;
		static readonly int offset5RojoAndVerdeUsa=263907;
		static readonly int offset5EsmeraldaUsa=438171;
		
		static readonly byte[] RutinaShinyzerBase =new byte[]
		{   			/* créditos a HackMew por la rutina :)  */
			0x20, 0x0E, 0x03, 0x28, 0x03, 0xD1, 0x20, 0x48, 0x00, 0x78, 0x00, 0x28,
			0x00, 0xD1, 0x70, 0x47, 0x3C, 0xB5, 0x43, 0x1E, 0x1C, 0x48, 0x03, 0x70,
			0x44, 0x78, 0x00, 0x2C, 0x26, 0xD1, 0x0C, 0x1C, 0x1A, 0x4A, 0x00, 0xF0,
			0x2A, 0xF8, 0x07, 0x23, 0x18, 0x40, 0x03, 0x1C, 0x17, 0x4A, 0x00, 0xF0,
			0x24, 0xF8, 0x05, 0x04, 0x05, 0x43, 0x5D, 0x40, 0x65, 0x40, 0x70, 0xB4,
			0x29, 0x0C, 0x28, 0x04, 0xDB, 0x43, 0x1B, 0x0C, 0x0D, 0x4C, 0x0E, 0x4D,
			0x06, 0x1C, 0x66, 0x43, 0x76, 0x19, 0x32, 0x0C, 0x8A, 0x42, 0x04, 0xD0,
			0x01, 0x30, 0x01, 0x3B, 0x00, 0x2B, 0xF5, 0xD1, 0x04, 0xE0, 0x09, 0x4A,
			0x16, 0x60, 0x62, 0xBC, 0x3D, 0x60, 0x3C, 0xBD, 0x70, 0xBC, 0xD9, 0xE7,
			0x01, 0x25, 0x9D, 0x40, 0x2C, 0x40, 0x00, 0x2C, 0x00, 0xD0, 0x39, 0x68,
			0xF5, 0xE7, 0x10, 0x47, 0x6D, 0x4E, 0xC6, 0x41, 0x73, 0x60, 0x00, 0x00,
		};

		static readonly byte[]    RutinaShinyzerLastLineRubiAndZafiroEsp = {
			0x28, 0x48, 0x00, 0x03, 0xCA, 0xE8, 0x02, 0x02, 0xA5, 0x12, 0x04, 0x08
		};

		static readonly byte[]    RutinaShinyzerLastLineEsmeraldaEsp = {

			0x80, 0x5D, 0x00, 0x03, 0xDE, 0x75, 0x03, 0x02, 0xC9, 0xF5, 0x06, 0x08
		};


		static readonly byte[]    RutinaShinyzerLastLineRojoAndVerdeUsa = {

			0x00, 0x50, 0x00, 0x03, 0xBE, 0x70, 0x03, 0x02, 0xC9, 0x4E, 0x04, 0x08
		};


		static readonly byte[]    RutinaShinyzerLastLineRojoAndVerdeEsp = {

			0x50, 0x4F, 0x00, 0x03, 0xBE, 0x70, 0x03, 0x02, 0xA1, 0x4E, 0x04, 0x08
		};



		static readonly byte[]    RutinaShinyzerLastLineEsmeraldaUsa = {

			0x80, 0x5D, 0x00, 0x03, 0xDE, 0x75, 0x03, 0x02, 0xCD, 0xF5, 0x06, 0x08
		};

		static readonly byte[]    RutinaShinyzerLastLineRubiAndZafiroUsa = {

			0x18, 0x48, 0x00, 0x03, 0xCA, 0xE8, 0x02, 0x02, 0x85, 0x0E, 0x04, 0x08
		};



		public static string VariableShinytzer = "0x8003";

		public static bool EstaActivado(RomGba rom)
		{
			return PosicionRutinaShinyzer(rom) > 0;
		}
		public static int Activar(RomData rom)
		{
			return Activar(rom.Rom,rom.Edicion);
		}
		public static int Activar(RomGba rom,EdicionPokemon edicion)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			byte[] lastLine;
			int offset1,offset2,offset3,offset4,offset5;
			if(posicion<0)
			{
				if(edicion.Idioma==Idioma.Español)
				{
					if(edicion.AbreviacionRom==AbreviacionCanon.AXP||edicion.AbreviacionRom==AbreviacionCanon.AXV)
					{
						rom.Data.SetArray(offset1RubiAndZafiroEsp,RutinaShinyzerHeader1RubiAndZafiroEsp);
					}else{
						if(edicion.AbreviacionRom==AbreviacionCanon.BPE)
							rom.Data.SetArray(offset1EsmeraldaEsp,RutinaShinyzerHeader1OtrasEsp);
						else rom.Data.SetArray(offset1RojoAndVerdeEsp,RutinaShinyzerHeader1OtrasEsp);
					}
				}
				else{
					switch (edicion.AbreviacionRom) {
						case AbreviacionCanon.AXV:
						case AbreviacionCanon.AXP:
							offset1=offset1RubiAndZafiroUsa;
							offset2=offset2RubiAndZafiroUsa;
							offset3=offset3RubiAndZafiroUsa;
							offset4=offset4RubiAndZafiroUsa;
							offset5=offset5RubiAndZafiroUsa;
							break;
						case AbreviacionCanon.BPE:
							offset1=offset1EsmeraldaUsa;
							offset2=offset2EsmeraldaUsa;
							offset3=offset3EsmeraldaUsa;
							offset4=offset4EsmeraldaUsa;
							offset5=offset5EsmeraldaUsa;
							break;
						case AbreviacionCanon.BPR:
						case AbreviacionCanon.BPG:
							offset1=offset1RojoAndVerdeUsa;
							offset2=offset2RojoAndVerdeUsa;
							offset3=offset3RojoAndVerdeUsa;
							offset4=offset4RojoAndVerdeUsa;
							offset5=offset5RojoAndVerdeUsa;
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					rom.Data.SetArray(offset1,RutinaShinyzerHeader1And2Usa);
					rom.Data.SetArray(offset2,RutinaShinyzerHeader1And2Usa);
					rom.Data.SetArray(offset3,RutinaShinyzerHeader3Usa);
					rom.Data.SetArray(offset4,RutinaShinyzerHeader4Usa);
					rom.Data.SetArray(offset5,RutinaShinyzerHeader5Usa);
					
				}
				switch (edicion.AbreviacionRom) {
					case AbreviacionCanon.AXV:
					case AbreviacionCanon.AXP:
						if(edicion.Idioma==Idioma.Español)
						{
							lastLine=RutinaShinyzerLastLineRubiAndZafiroEsp;
							
						}
						else{
							lastLine=RutinaShinyzerLastLineRubiAndZafiroUsa;
						}
						break;
					case AbreviacionCanon.BPE:
						if(edicion.Idioma==Idioma.Español)
						{
							lastLine=RutinaShinyzerLastLineEsmeraldaEsp;
						}
						else{
							lastLine=RutinaShinyzerLastLineEsmeraldaUsa;
						}
						
						break;
					case AbreviacionCanon.BPR:
					case AbreviacionCanon.BPG:
						if(edicion.Idioma==Idioma.Español)
						{
							lastLine=RutinaShinyzerLastLineRojoAndVerdeEsp;
						}
						else{
							lastLine=RutinaShinyzerLastLineRojoAndVerdeUsa;
						}
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				
				posicion =rom.Data.SetArray(RutinaShinyzerBase.AddArray(lastLine));
			}
			return posicion;

		}
		public static void Desactivar(RomGba rom)
		{
			int posicion = PosicionRutinaShinyzer(rom);
			if (posicion > 0)
			{
				rom.Data.Remove( posicion, LENGTH);
			}
		}
		/// <summary>
		/// Permite saber donde esta la rutina insertada
		/// </summary>
		/// <param name="rom"></param>
		/// <returns>devuelve -1 si no lo ha encontrado</returns>
		public  static int PosicionRutinaShinyzer(RomGba rom)
		{
			if (rom == null)
				throw new ArgumentNullException();
			return rom.Data.SearchArray(RutinaShinyzerBase);
		}
		public static string ScriptLineaPokemonShiny(short numPokemon)
		{
			if (numPokemon<0)
				throw new ArgumentOutOfRangeException();

			return "setvar " + VariableShinytzer + ((Hex)numPokemon).ByteString;
		}
		/// <summary>
		/// Crear fácilmente el script shinytzer para que el entrenador al ser llamado tenga esos pokemon
		/// </summary>
		/// <param name="entrenador"></param>
		/// <param name="pokemon">primer,segundo,tercero,cuarto...</param>
		/// <returns></returns>
		public static string ScriptLineaPokemonShinyEntrenador(Entrenador entrenador,params bool[] pokemon)
		{
			if (entrenador == null)
				throw new ArgumentNullException();

			const int MAXPOKEMONENTRENADOR = 6,BITSBYTE=8;

			Hex numPokemonShiny;
			bool[] pokemonFinal;

			if (pokemon.Length < MAXPOKEMONENTRENADOR)
			{
				pokemonFinal = new bool[MAXPOKEMONENTRENADOR];
				for (int i =0; i< pokemon.Length;  i++)
					pokemonFinal[i] = pokemon[i];
				
			}
			else if (pokemon.Length > MAXPOKEMONENTRENADOR)
				pokemonFinal = pokemon.SubArray(0, MAXPOKEMONENTRENADOR);
			else pokemonFinal = pokemon;

			pokemon = pokemonFinal;
			pokemonFinal = new bool[BITSBYTE];
			for (int i = 2,j=0; j<pokemon.Length; i++,j++)
				pokemonFinal[i] = pokemon[j];

			numPokemonShiny = pokemonFinal.ToByte();
			
			return "setvar " + VariableShinytzer + " 0x" + numPokemonShiny.ToString().PadLeft(2, '0') + entrenador.EquipoPokemon.NumeroPokemon.ToString().PadLeft(2,'0');
		}
		public static string SimpleScriptBattleShinyTrainer(int indexEntrenador,Entrenador  entrenador,params bool[] pokemon)
		{
			StringBuilder strScript=new StringBuilder();
			strScript.Append("#dynamic 0x800000 \r\n#org @ScriptTrainer");
			strScript.Append(entrenador.Nombre);
			strScript.Append("Shiny\r\nlock\r\nfaceplayer\r\n");
			strScript.Append(ScriptLineaPokemonShinyEntrenador(entrenador,pokemon));
			strScript.Append("\r\ntrainerbattle 0x0 ");
			strScript.Append(((Hex)(indexEntrenador+1)).ByteString);
			strScript.Append(" 0x0 0x0 0x0\r\nrelease\r\nend");
			return strScript.ToString();
		}
	}
}
