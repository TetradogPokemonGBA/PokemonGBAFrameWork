using Gabriel.Cat.S.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core
{
   public class Ataque
    {
		public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479

		enum LongitudCampos
		{

			PointerEfecto = 4,
			ContestData,
			Descripcion = 4,//es un pointer al texto
			ScriptBatalla,
			Animacion
		}
		enum ValoresLimitadoresFin
		{
			Ataque = 0x13E0,
		}

		static readonly byte[] BytesDesLimitadoAtaques;

		public const int LENGTHLIMITADOR = 16;

		NombreAtaque nombre;
		DescripcionAtaque descripcion;
		DatosAtaque datos;
		ConcursosAtaque datosConcursosHoenn;

		static Ataque()
		{


			byte[] valoresUnLimited = (((Gabriel.Cat.S.Utilitats.Hex)(int)ValoresLimitadoresFin.Ataque));

			ZonaScriptBatalla = new Zona("ScriptAtaqueBatalla");
			ZonaAnimacion = new Zona("AnimaciónAtaque");
			VariableLimitadoAtaques = new Variable("VariableLimitadorAtaque");

			//por investigar!!!
			//efectos el offset tiene que acabar en 0,4,8,C
			//de momento se tiene que investigar...lo que habia antes eran animaciones...

			//script batalla
			ZonaScriptBatalla.Add(0x162D4, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
			ZonaScriptBatalla.Add(EdicionPokemon.RojoFuegoUsa10, 0x16364, 0x16378);
			ZonaScriptBatalla.Add(EdicionPokemon.VerdeHojaUsa10, 0x16364, 0x16378);

			ZonaScriptBatalla.Add(0x148B0, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
			ZonaScriptBatalla.Add(0x3E854, EdicionPokemon.EsmeraldaUsa10, EdicionPokemon.EsmeraldaEsp10);
			ZonaScriptBatalla.Add(0x146E4, EdicionPokemon.RubiUsa10, EdicionPokemon.ZafiroUsa10);

			//animacion CON ESTO PUEDO DIFERENCIAR LAS VERSIONES ZAFIRO Y RUBI USA :D
			ZonaAnimacion.Add(0x72608, EdicionPokemon.RojoFuegoEsp10, EdicionPokemon.VerdeHojaEsp10);
			ZonaAnimacion.Add(EdicionPokemon.RojoFuegoUsa10, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.VerdeHojaUsa10, 0x7250D0, 0x725E4);
			ZonaAnimacion.Add(EdicionPokemon.RubiUsa10, 0x75734, 0x75754);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroUsa10, 0x75738, 0x75758);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaUsa10, 0xA3A44);
			ZonaAnimacion.Add(EdicionPokemon.EsmeraldaEsp10, 0xA3A58);
			ZonaAnimacion.Add(EdicionPokemon.RubiEsp10, 0x75BF0);
			ZonaAnimacion.Add(EdicionPokemon.ZafiroEsp10, 0x75BF4);
			//añado la variable limitador
			VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaUsa10, 0xD75D0, 0xD75E4);
			VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoUsa10, 0xD75FC, 0xD7610);
			VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaUsa10, 0x14E504);
			VariableLimitadoAtaques.Add(EdicionPokemon.VerdeHojaEsp10, 0xD7858);
			VariableLimitadoAtaques.Add(EdicionPokemon.EsmeraldaEsp10, 0x14E138);
			VariableLimitadoAtaques.Add(EdicionPokemon.RojoFuegoEsp10, 0xD7884);
			VariableLimitadoAtaques.Add(0xAC8C2, EdicionPokemon.RubiEsp10, EdicionPokemon.ZafiroEsp10);
			VariableLimitadoAtaques.Add(EdicionPokemon.RubiUsa10, 0xAC676, 0xAC696);
			VariableLimitadoAtaques.Add(EdicionPokemon.ZafiroUsa10, 0xAC676, 0xAC696);

			BytesDesLimitadoAtaques = new byte[LENGTHLIMITADOR];
			BytesDesLimitadoAtaques.SetArray(LENGTHLIMITADOR - valoresUnLimited.Length, valoresUnLimited);


		}
		public Ataque()
		{
			descripcion = new DescripcionAtaque();
			datos = new DatosAtaque();
			datosConcursosHoenn = new ConcursosAtaque();
		}
		#region Propiedades
		public NombreAtaque Nombre
		{
			get
			{
				return nombre;
			}
			set
			{
				nombre = value;
			}
		}

		public DescripcionAtaque Descripcion
		{
			get
			{
				return descripcion;
			}
			set
			{
				descripcion = value;
			}
		}

		public DatosAtaque Datos
		{
			get
			{
				return datos;
			}
			set { datos = value; }
		}

		public ConcursosAtaque Concursos
		{
			get
			{
				return datosConcursosHoenn;
			}
			set { datosConcursosHoenn = value; }
		}

		#region IComparable implementation


		public int CompareTo(object obj)
		{
			int compareTo;
			Ataque ataque = obj as Ataque;
			if (ataque != null)
			{
				compareTo = Nombre.CompareTo(ataque.Nombre);
			}
			else compareTo = (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;
			return compareTo;
		}


		#endregion

		#endregion
		public override string ToString()
		{
			return Nombre.ToString();
		}

		public static Ataque GetAtaque(RomGba rom, int posicionAtaque)
		{//por mirar la obtenxion del offset descripcion


			Ataque ataque = new Ataque();

			ataque.Nombre = NombreAtaque.GetNombre(rom, posicionAtaque);
			ataque.Descripcion = DescripcionAtaque.GetDescripcion(rom, posicionAtaque);

			ataque.Datos = DatosAtaque.GetDatos(rom, posicionAtaque);

			ataque.Concursos = ConcursosAtaque.GetConcursos(rom, posicionAtaque);

			return ataque;
		}

		public static Ataque[] GetAtaques(RomGba rom)
		{
			Ataque[] ataques = new Ataque[DescripcionAtaque.GetTotal(rom)];
			for (int i = 0; i < ataques.Length; i++)
				ataques[i] = GetAtaque(rom, i);
			return ataques;
		}



	}
}

    }
}
