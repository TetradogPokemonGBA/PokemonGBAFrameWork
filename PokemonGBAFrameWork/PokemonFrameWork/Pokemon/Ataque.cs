/*
 * Created by SharpDevelop.
 * User: Pikachu240
 * Date: 11/03/2017
 * Time: 5:52
 * 
 * Código bajo licencia GNU
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Gabriel.Cat.S.Binaris;
using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats;
using PokemonGBAFrameWork.Ataque;
using System;
using System.Collections.Generic;

namespace PokemonGBAFrameWork
{
    /// <summary>
    /// Description of Ataque.
    /// </summary>
    public class AtaqueCompleto:PokemonFrameWorkItem,IComparable
	{
        public const byte ID = 0x14;
        //son 9 bits en total de alli el 511 :) asi en 2 bytes hay ataque y nivel :)
        public const int MAXATAQUESSINASM = 511;//hasta que no sepa como se cambia para poner más se queda este maximo :) //hay un tutorial de como hacerlo pero se necesita insertar una rutina ASM link:http://www.pokecommunity.com/showthread.php?t=263479
        public static readonly ElementoBinario Serializador = ElementoBinario.GetSerializador<AtaqueCompleto>();

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
			Ataque =0x13E0,
		}



		public static readonly Zona ZonaScriptBatalla;
		public static readonly Zona ZonaAnimacion;

        public static readonly Variable VariableLimitadoAtaques;

        static readonly byte[] BytesDesLimitadoAtaques;

        public const int LENGTHLIMITADOR = 16;

        Nombre nombre;
		Descripcion descripcion;
		Datos datos;
		Concursos datosConcursosHoenn;
		
		static AtaqueCompleto()
		{


            byte[] valoresUnLimited = (((Hex)(int)ValoresLimitadoresFin.Ataque));

            ZonaScriptBatalla = new Zona("ScriptAtaqueBatalla");
			ZonaAnimacion=new Zona("AnimaciónAtaque");
            VariableLimitadoAtaques = new Variable("VariableLimitadorAtaque");

            //por investigar!!!
            //efectos el offset tiene que acabar en 0,4,8,C
            //de momento se tiene que investigar...lo que habia antes eran animaciones...

            //script batalla
            ZonaScriptBatalla.Add(0x162D4,EdicionPokemon.RojoFuegoEsp10,EdicionPokemon.VerdeHojaEsp10);
			ZonaScriptBatalla.Add(EdicionPokemon.RojoFuegoUsa10, 0x16364, 0x16378);
			ZonaScriptBatalla.Add(EdicionPokemon.VerdeHojaUsa10, 0x16364, 0x16378);

			ZonaScriptBatalla.Add(0x148B0,EdicionPokemon.RubiEsp10,EdicionPokemon.ZafiroEsp10);
			ZonaScriptBatalla.Add(0x3E854,EdicionPokemon.EsmeraldaUsa10,EdicionPokemon.EsmeraldaEsp10);
			ZonaScriptBatalla.Add(0x146E4,EdicionPokemon.RubiUsa10,EdicionPokemon.ZafiroUsa10);

			//animacion CON ESTO PUEDO DIFERENCIAR LAS VERSIONES ZAFIRO Y RUBI USA :D
			ZonaAnimacion.Add(0x72608,EdicionPokemon.RojoFuegoEsp10,EdicionPokemon.VerdeHojaEsp10);
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
        public AtaqueCompleto()
		{
				descripcion=new Descripcion();
			datos=new Datos();
            datosConcursosHoenn = new Concursos();
		}
		#region Propiedades
		public Nombre Nombre {
			get {
				return nombre;
			}
            set
            {
                nombre = value;
            }
		}

		public Descripcion Descripcion {
			get {
				return descripcion;
			}
            set
            {
                descripcion = value;
            }
		}

		public Datos Datos {
			get {
				return datos;
			}
            set { datos = value; }
		}

		public Concursos Concursos {
			get {
				return datosConcursosHoenn;
			}
            set { datosConcursosHoenn = value; }
		}
        public override byte IdTipo { get => ID; set => base.IdTipo = value; }
        public override ElementoBinario Serialitzer => Serializador;
        #region IComparable implementation


        public int CompareTo(object obj)
		{
            int compareTo;
            AtaqueCompleto ataque = obj as AtaqueCompleto;
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
		
		public static AtaqueCompleto GetAtaque(RomGba rom,int posicionAtaque)
		{//por mirar la obtenxion del offset descripcion

			
			AtaqueCompleto ataque=new AtaqueCompleto();

            ataque.Nombre = Nombre.GetNombre(rom, posicionAtaque);
            ataque.Descripcion = Descripcion.GetDescripcion(rom, posicionAtaque);
			
			ataque.Datos=Datos.GetDatos(rom,posicionAtaque);

            ataque.Concursos = Concursos.GetConcursos(rom, posicionAtaque);
            ataque.IdElemento = (ushort)posicionAtaque;
            ataque.IdFuente = EdicionPokemon.IDMINRESERVADO;
			return ataque;
		}

		public static AtaqueCompleto[] GetAtaques(RomGba rom)
		{
			AtaqueCompleto[] ataques=new AtaqueCompleto[Descripcion.GetTotal(rom)];
			for(int i=0;i<ataques.Length;i++)
				ataques[i]=GetAtaque(rom,i);
			return ataques;
		}

		public static void SetAtaque(RomGba rom,int posicionAtaque,AtaqueCompleto ataqueAPoner)
		{


            Nombre.SetNombre(rom, posicionAtaque, ataqueAPoner.Nombre);
            Descripcion.SetDescripcion(rom, posicionAtaque, ataqueAPoner.Descripcion);
			//pongo los datos
			Datos.SetDatos(rom,posicionAtaque,ataqueAPoner.Datos);

            Concursos.SetConcursos(rom, posicionAtaque, ataqueAPoner.Concursos);
			QuitarLimite(rom,posicionAtaque);
		}

		public static void SetAtaques(RomGba rom, IList<AtaqueCompleto> ataques)
        {
            List<Datos> datos = new List<Datos>();
            List<Concursos> concursos = new List<Concursos>();
            List<Descripcion> descripcions = new List<Descripcion>();
            List<Nombre> nombres = new List<Nombre>();
            for(int i=0;i<ataques.Count;i++)
            {
                datos.Add(ataques[i].Datos);
                concursos.Add(ataques[i].Concursos);
                descripcions.Add(ataques[i].Descripcion);
                nombres.Add(ataques[i].Nombre);
            }
            Datos.SetDatos(rom, datos);
            Concursos.SetConcursos(rom, concursos);
            Descripcion.SetDescripcion(rom, descripcions);
            Nombre.SetNombre(rom, nombres);

		}



		public static void QuitarLimite(RomGba rom,int posicion)
		{
			//quito el limite
			if(posicion>Descripcion.GetTotal(rom))
            {
				rom.Data.SetArray(Variable.GetVariable(VariableLimitadoAtaques, rom.Edicion),BytesDesLimitadoAtaques);

                Concursos.QuitarLimite(rom, posicion);
			}
		}
	}
}
